using Application.Features.Appointments.Queries.GetList;
using Application.Features.Patients.Constants;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using MediatR;
using NArchitecture.Core.Application.Pipelines.Authorization;
using NArchitecture.Core.Application.Pipelines.Caching;
using NArchitecture.Core.Application.Requests;
using NArchitecture.Core.Application.Responses;
using NArchitecture.Core.Persistence.Paging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Application.Features.Appointments.Rules;
using static Application.Features.Appointments.Constants.AppointmentsOperationClaims;
using Microsoft.EntityFrameworkCore;
using Application.Services.Encryptions;

namespace Application.Features.Appointments.Queries.GetByPatientId;

public class GetListByPatientQuery:IRequest<GetListResponse<GetListByPatientDto>>,ISecuredRequest

{
    public PageRequest PageRequest { get; set; }
    public Guid PatientId { get; set; }

    public string[] Roles => [Admin, Read,PatientsOperationClaims.Update];

    public bool BypassCache { get; }
    public string? CacheKey => $"GetListAppointments({PageRequest.PageIndex},{PageRequest.PageSize})";
    public string? CacheGroupKey => "GetAppointments";
    public TimeSpan? SlidingExpiration { get; }

    public class GetListByPatientQueryHandler : IRequestHandler<GetListByPatientQuery, GetListResponse<GetListByPatientDto>>
    {
        private readonly IAppointmentRepository _appointmentRepository;
        private readonly IMapper _mapper;

        public GetListByPatientQueryHandler(IAppointmentRepository appointmentRepository, IMapper mapper)
        {
            _appointmentRepository = appointmentRepository;
            _mapper = mapper;
        }

        public async Task<GetListResponse<GetListByPatientDto>> Handle(
            GetListByPatientQuery request,
            CancellationToken cancellationToken
        )
        {
            IPaginate<Appointment> appointments = await _appointmentRepository.GetListAsync(
               index: request.PageRequest.PageIndex,
               size: request.PageRequest.PageSize,
               cancellationToken: cancellationToken,
                  orderBy: x => x.OrderByDescending(y => y.Date),
               include: x => x.Include(x => x.Doctor).Include(x => x.Patient).Include(x => x.Doctor.Branch),
                  predicate: x => x.PatientID == request.PatientId && x.DeletedDate==null

           );
            for (int i = 0; i < appointments.Items.Count; i++)
            {
                appointments.Items[i].Doctor.FirstName = CryptoHelper.Decrypt(appointments.Items[i].Doctor.FirstName);
                appointments.Items[i].Doctor.LastName = CryptoHelper.Decrypt(appointments.Items[i].Doctor.LastName);
                appointments.Items[i].Patient.FirstName = CryptoHelper.Decrypt(appointments.Items[i].Patient.FirstName);
                appointments.Items[i].Patient.LastName = CryptoHelper.Decrypt(appointments.Items[i].Patient.LastName);
            }

            GetListResponse<GetListByPatientDto> response = _mapper.Map<GetListResponse<GetListByPatientDto>>(appointments);
            return response;
           
        }
    }
}