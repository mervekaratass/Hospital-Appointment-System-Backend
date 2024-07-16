using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Threading;
using System.Threading.Tasks;
using Application.Features.Appointments.Constants;
using Application.Features.Appointments.Rules;
using Application.Services.Encryptions;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using MailKit.Net.Smtp;
using MailKit.Security;
using MediatR;
using MimeKit;
using NArchitecture.Core.Application.Pipelines.Authorization;
using static Application.Features.Appointments.Constants.AppointmentsOperationClaims;

using NArchitecture.Core.Security.Entities;
using Application.Features.Patients.Constants;
using Application.Features.Doctors.Constants;
using Application.Services.Doctors;
using Application.Services.Patients;
using Application.Services.Branches;

namespace Application.Features.Appointments.Commands.Create
{
    public class CreateAppointmentCommand : IRequest<CreatedAppointmentResponse>, ISecuredRequest
    {
        public DateOnly Date { get; set; }
        public TimeOnly Time { get; set; }
        public bool Status { get; set; }
        public Guid DoctorID { get; set; }
        public Guid PatientID { get; set; }


        public string[] Roles => [Admin, Write, PatientsOperationClaims.Update,DoctorsOperationClaims.Update];

        public bool BypassCache { get; }
        public string? CacheKey { get; }
        public string[]? CacheGroupKey => ["GetAppointments"];


        public class CreateAppointmentCommandHandler : IRequestHandler<CreateAppointmentCommand, CreatedAppointmentResponse>
        {
            private readonly IMapper _mapper;
            private readonly IAppointmentRepository _appointmentRepository;
            private readonly IDoctorService _doctorService;
            private readonly IPatientService _patientService;
            private readonly IBranchService _branchService;
            private readonly AppointmentBusinessRules _appointmentBusinessRules;

            public CreateAppointmentCommandHandler(IMapper mapper, IAppointmentRepository appointmentRepository, IDoctorService doctorService, IPatientService patientService, IBranchService branchService, AppointmentBusinessRules appointmentBusinessRules)
            {
                _mapper = mapper;
                _appointmentRepository = appointmentRepository;
                _doctorService = doctorService;
                _patientService = patientService;
                _branchService = branchService;
                _appointmentBusinessRules = appointmentBusinessRules;
            }

            public async Task<CreatedAppointmentResponse> Handle(CreateAppointmentCommand request, CancellationToken cancellationToken)
            {

                // Yeni randevu oluþtur
                Appointment appointment = _mapper.Map<Appointment>(request);

                // Doctor bilgisini al
                Doctor doctor = await _doctorService.GetAsync(d => d.Id == request.DoctorID);
                appointment.Doctor = doctor;


                // Patient bilgisini al
                Patient patient = await _patientService.GetAsync(p => p.Id == request.PatientID);
                appointment.Patient = patient;



                // Branþ bilgisini al
                Branch branch = await _branchService.GetAsync(p => p.Id == doctor.BranchID);
                doctor.Branch = branch;


                // Hasta ayný doktordan ayný güne ait randevusu olup olmadýðýný kontrol et
                await _appointmentBusinessRules.PatientCannotHaveMultipleAppointmentsOnSameDayWithSameDoctor(request.PatientID, request.DoctorID, request.Date);



                  // Ayný doktor ve tarihte silinmiþ randevu var mý kontrol et
                    Appointment result = await _appointmentBusinessRules.CheckForExistingDeletedAppointment(request,appointment);

                    await _appointmentBusinessRules.SendAppointmentConfirmationMail(result);
                    CreatedAppointmentResponse response = _mapper.Map<CreatedAppointmentResponse>(result);
                    return response;
               
                }
            }

        
        }
    }
