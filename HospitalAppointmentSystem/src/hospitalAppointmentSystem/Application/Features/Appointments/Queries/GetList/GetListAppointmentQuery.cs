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

namespace Application.Features.Appointments.Queries.GetList;

public class GetListAppointmentQuery : IRequest<GetListResponse<GetListAppointmentListItemDto>>, ISecuredRequest, ICachableRequest
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

            GetListResponse<GetListAppointmentListItemDto> response = _mapper.Map<GetListResponse<GetListAppointmentListItemDto>>(appointments);
            return response;
        }

    }
}