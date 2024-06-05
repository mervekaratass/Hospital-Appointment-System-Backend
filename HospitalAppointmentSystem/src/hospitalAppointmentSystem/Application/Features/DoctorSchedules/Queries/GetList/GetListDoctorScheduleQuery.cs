using Application.Features.DoctorSchedules.Constants;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using NArchitecture.Core.Application.Pipelines.Authorization;
using NArchitecture.Core.Application.Pipelines.Caching;
using NArchitecture.Core.Application.Requests;
using NArchitecture.Core.Application.Responses;
using NArchitecture.Core.Persistence.Paging;
using MediatR;
using static Application.Features.DoctorSchedules.Constants.DoctorSchedulesOperationClaims;
using Microsoft.EntityFrameworkCore;
using Application.Features.Doctors.Constants;

namespace Application.Features.DoctorSchedules.Queries.GetList;

public class GetListDoctorScheduleQuery : IRequest<GetListResponse<GetListDoctorScheduleListItemDto>>, ISecuredRequest, ICachableRequest
{
    public PageRequest PageRequest { get; set; }

    public string[] Roles => [Admin, Read, DoctorsOperationClaims.Update];

    public bool BypassCache { get; }
    public string? CacheKey => $"GetListDoctorSchedules({PageRequest.PageIndex},{PageRequest.PageSize})";
    public string? CacheGroupKey => "GetDoctorSchedules";
    public TimeSpan? SlidingExpiration { get; }

    public class GetListDoctorScheduleQueryHandler : IRequestHandler<GetListDoctorScheduleQuery, GetListResponse<GetListDoctorScheduleListItemDto>>
    {
        private readonly IDoctorScheduleRepository _doctorScheduleRepository;
        private readonly IMapper _mapper;

        public GetListDoctorScheduleQueryHandler(IDoctorScheduleRepository doctorScheduleRepository, IMapper mapper)
        {
            _doctorScheduleRepository = doctorScheduleRepository;
            _mapper = mapper;
        }

        public async Task<GetListResponse<GetListDoctorScheduleListItemDto>> Handle(GetListDoctorScheduleQuery request, CancellationToken cancellationToken)
        {
            IPaginate<DoctorSchedule> doctorSchedules = await _doctorScheduleRepository.GetListAsync(
                index: request.PageRequest.PageIndex,
                size: request.PageRequest.PageSize, 
                cancellationToken: cancellationToken,
                    include: x => x.Include(x => x.Doctor)
            );

            GetListResponse<GetListDoctorScheduleListItemDto> response = _mapper.Map<GetListResponse<GetListDoctorScheduleListItemDto>>(doctorSchedules);
            return response;
        }
    }
}