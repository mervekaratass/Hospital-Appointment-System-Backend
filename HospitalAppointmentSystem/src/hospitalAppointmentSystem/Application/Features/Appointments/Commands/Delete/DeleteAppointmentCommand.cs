using Application.Features.Appointments.Constants;
using Application.Features.Appointments.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using NArchitecture.Core.Application.Pipelines.Authorization;
using NArchitecture.Core.Application.Pipelines.Caching;
using NArchitecture.Core.Application.Pipelines.Logging;
using NArchitecture.Core.Application.Pipelines.Transaction;
using MediatR;
using static Application.Features.Appointments.Constants.AppointmentsOperationClaims;
using Application.Features.Patients.Constants;
using Application.Features.Doctors.Constants;
using MailKit.Security;
using MimeKit;
using MailKit.Net.Smtp;
using Org.BouncyCastle.Asn1.Ocsp;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.Appointments.Commands.Delete;

public class DeleteAppointmentCommand : IRequest<DeletedAppointmentResponse>, ISecuredRequest, ILoggableRequest, ITransactionalRequest
{
    public int Id { get; set; }
    public DateOnly Date { get; set; }
    public TimeOnly Time { get; set; }
    public Guid DoctorID { get; set; }
    public Guid PatientID { get; set; }

    public string[] Roles => [Admin, Write, AppointmentsOperationClaims.Delete, PatientsOperationClaims.Update, DoctorsOperationClaims.Update];

    public bool BypassCache { get; }
    public string? CacheKey { get; }
    public string[]? CacheGroupKey => ["GetAppointments"];

    public class DeleteAppointmentCommandHandler : IRequestHandler<DeleteAppointmentCommand, DeletedAppointmentResponse>
    {
        private readonly IMapper _mapper;
        private readonly IAppointmentRepository _appointmentRepository;
        private readonly IDoctorRepository _doctorRepository;
        private readonly IPatientRepository _patientRepository;
        private readonly IBranchRepository _branchRepository;
        private readonly AppointmentBusinessRules _appointmentBusinessRules;

        public DeleteAppointmentCommandHandler(IMapper mapper, IAppointmentRepository appointmentRepository,
                                         AppointmentBusinessRules appointmentBusinessRules, IDoctorRepository doctorRepository, IPatientRepository patientRepository, IBranchRepository branchRepository)
        {
            _mapper = mapper;
            _appointmentRepository = appointmentRepository;
            _appointmentBusinessRules = appointmentBusinessRules;
            _doctorRepository = doctorRepository;
            _patientRepository = patientRepository;
            _branchRepository = branchRepository;
        }

        public async Task<DeletedAppointmentResponse> Handle(DeleteAppointmentCommand request, CancellationToken cancellationToken)
        {
            Appointment? appointment = await _appointmentRepository.GetAsync(predicate: a => a.Id == request.Id &&a.DeletedDate==null,
             include: a => a .Include(a => a.Doctor) .ThenInclude(d => d.Branch)
            .Include(a => a.Patient), cancellationToken: cancellationToken);
            await _appointmentBusinessRules.AppointmentShouldExistWhenSelected(appointment);
            await _appointmentRepository.DeleteAsync(appointment!);

            // Silinen randevu bilgilerini mail olarak gönder
            await SendAppointmentConfirmationMail(appointment);

            DeletedAppointmentResponse response = _mapper.Map<DeletedAppointmentResponse>(appointment);
            return response;
        }
        private async Task SendAppointmentConfirmationMail(Appointment appointment)
        {

            // Mail içeriðini hazýrla
            var mailMessage = new MimeMessage();
            mailMessage.From.Add(new MailboxAddress("Pair 5 Hastanesi", "fatmabireltr@gmail.com")); // Gönderen bilgisi
            mailMessage.To.Add(new MailboxAddress("Pair 5 Hastanesi", appointment.Patient.Email)); // Alýcý bilgisi 
            mailMessage.Subject = "Randevu Bilgilendirme"; // Mail konusu

            // HTML ve CSS içeriði oluþtur
            var bodyBuilder = new BodyBuilder();
            bodyBuilder.HtmlBody = $@"
       <html>
        <head>
            <style>
                body {{ font-family: Arial, sans-serif; }}
                .container {{ border: 1px solid red; padding: 10px; }}
            </style>
        </head>
        <body>
            <div class='container'>
                <p>Sayýn {appointment.Patient.FirstName} {appointment.Patient.LastName},</p>
                <p>{appointment.Date} tarihinde, saat {appointment.Time} olan randevunuz iptal edildi.</p>
                <p>Doktor:{appointment.Doctor.Title} {appointment.Doctor.FirstName} {appointment.Doctor.LastName}</p>
                <p>Branþ: {appointment.Doctor.Branch.Name}</p>
            </div>
        </body>
        </html>";

            // MimeKit'e gövdeyi ayarla
            mailMessage.Body = bodyBuilder.ToMessageBody();

            // SMTP ile baðlantý kur ve maili gönder
            using (var smtp = new SmtpClient())
            {
                smtp.Connect("smtp.gmail.com", 587, SecureSocketOptions.StartTls);
                smtp.Authenticate("fatmabireltr@gmail.com", "rxuv hpfv wlqq htpa");
                await smtp.SendAsync(mailMessage);
                smtp.Disconnect(true);
            }
        }

    }
}