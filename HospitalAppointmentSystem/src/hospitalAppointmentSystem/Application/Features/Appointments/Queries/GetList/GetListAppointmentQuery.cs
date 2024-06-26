using Application.Features.Appointments.Constants;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using NArchitecture.Core.Application.Pipelines.Authorization;
using NArchitecture.Core.Application.Pipelines.Caching;
using NArchitecture.Core.Application.Requests;
using NArchitecture.Core.Application.Responses;
using NArchitecture.Core.Persistence.Paging;
using MediatR;
using static Application.Features.Appointments.Constants.AppointmentsOperationClaims;
using Microsoft.EntityFrameworkCore;
using Application.Features.Patients.Constants;
//using Application.Services.Encryptions;
using System.Numerics;

namespace Application.Features.Appointments.Queries.GetList;

public class GetListAppointmentQuery : IRequest<GetListResponse<GetListAppointmentListItemDto>>, ISecuredRequest
{
    public PageRequest PageRequest { get; set; }
    public bool? IncludeDeleted { get; set; } = false;
    public string[] Roles => [Admin, Read];

    public bool BypassCache { get; }
    public string? CacheKey => $"GetListAppointments({PageRequest.PageIndex},{PageRequest.PageSize})";
    public string? CacheGroupKey => "GetAppointments";
    public TimeSpan? SlidingExpiration { get; }

    public class GetListAppointmentQueryHandler : IRequestHandler<GetListAppointmentQuery, GetListResponse<GetListAppointmentListItemDto>>
    {
        private readonly IAppointmentRepository _appointmentRepository;
        private readonly IMapper _mapper;

        public GetListAppointmentQueryHandler(IAppointmentRepository appointmentRepository, IMapper mapper)
        {
            _appointmentRepository = appointmentRepository;
            _mapper = mapper;
        }

        public async Task<GetListResponse<GetListAppointmentListItemDto>> Handle(GetListAppointmentQuery request, CancellationToken cancellationToken)
        {
            IPaginate<Appointment> appointments = await _appointmentRepository.GetListAsync(
                predicate: x => x.DeletedDate == null,
                index: request.PageRequest.PageIndex,
                size: request.PageRequest.PageSize,
                cancellationToken: cancellationToken,
                orderBy: x => x.OrderByDescending(y => y.Date),
                include: x => x.Include(x => x.Doctor).Include(x => x.Patient).Include(x => x.Doctor.Branch)
            );

            //SÝNEM
            //for (int i = 0; i < appointments.Items.Count; i++)
            //{
            //    appointments.Items[i].Patient.FirstName = CryptoHelper.Decrypt(appointments.Items[i].Patient.FirstName);
            //    appointments.Items[i].Patient.LastName = CryptoHelper.Decrypt(appointments.Items[i].Patient.LastName);
            //    appointments.Items[i].Patient.NationalIdentity = CryptoHelper.Decrypt(appointments.Items[i].Patient.NationalIdentity);
            //    appointments.Items[i].Patient.Phone = CryptoHelper.Decrypt(appointments.Items[i].Patient.Phone);
            //    appointments.Items[i].Patient.Address = CryptoHelper.Decrypt(appointments.Items[i].Patient.Address);
            //    appointments.Items[i].Doctor.FirstName= CryptoHelper.Decrypt(appointments.Items[i].Patient.Address);
            //    appointments.Items[i].Doctor.LastName = CryptoHelper.Decrypt(appointments.Items[i].Patient.Address);
            //    appointments.Items[i].Doctor.NationalIdentity= CryptoHelper.Decrypt(appointments.Items[i].Patient.Address);
            //    appointments.Items[i].Doctor.Phone = CryptoHelper.Decrypt(appointments.Items[i].Patient.Address);
            //    appointments.Items[i].Doctor.Address = CryptoHelper.Decrypt(appointments.Items[i].Patient.Address);
            //}



            // ustte ve alttada deðiþiklik yaptým


            GetListResponse<GetListAppointmentListItemDto> response = _mapper.Map<GetListResponse<GetListAppointmentListItemDto>>(appointments);
            return response;
        }

    }
}