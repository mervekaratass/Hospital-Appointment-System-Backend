using Application.Features.Doctors.Constants;
using Application.Features.DoctorSchedules.Queries.GetList;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using MediatR;
using NArchitecture.Core.Application.Pipelines.Authorization;
using NArchitecture.Core.Application.Pipelines.Caching;
using NArchitecture.Core.Application.Requests;
using NArchitecture.Core.Application.Responses;
using NArchitecture.Core.Persistence.Paging;
using static Application.Features.DoctorSchedules.Constants.DoctorSchedulesOperationClaims;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Application.Features.Patients.Constants;

namespace Application.Features.DoctorSchedules.Queries.GetListByDoctorId;
public class GetListByDoctorIdQuery : IRequest<GetListResponse<GetListByDoctorIdDto>>, ISecuredRequest
{   
    public Guid DoctorId { get; set; }
    public PageRequest PageRequest { get; set; }

    public string[] Roles => [Admin, Read, DoctorsOperationClaims.Update,PatientsOperationClaims.Update]; //hastayı şuan ekledim bakıcam -merve

    public bool BypassCache { get; }
    public string? CacheKey => $"GetListDoctorSchedules({PageRequest.PageIndex},{PageRequest.PageSize})";
    public string? CacheGroupKey => "GetDoctorSchedules";
    public TimeSpan? SlidingExpiration { get; }

    public class GetListByDoctorIdQueryHandler : IRequestHandler<GetListByDoctorIdQuery, GetListResponse<GetListByDoctorIdDto>>
    {
        private readonly IDoctorScheduleRepository _doctorScheduleRepository;
        private readonly IMapper _mapper;

        public GetListByDoctorIdQueryHandler(IDoctorScheduleRepository doctorScheduleRepository, IMapper mapper)
        {
            _doctorScheduleRepository = doctorScheduleRepository;
            _mapper = mapper;
        }

        public async Task<GetListResponse<GetListByDoctorIdDto>> Handle(GetListByDoctorIdQuery request, CancellationToken cancellationToken)
        {
            IPaginate<DoctorSchedule> doctorSchedules = await _doctorScheduleRepository.GetListAsync(
                index: request.PageRequest.PageIndex,
                size: request.PageRequest.PageSize,
                cancellationToken: cancellationToken,
                orderBy: x => x.OrderByDescending(x => x.Date),
                include: x => x.Include(x => x.Doctor),
                predicate: x => x.DoctorID == request.DoctorId && x.DeletedDate==null

            );

            GetListResponse<GetListByDoctorIdDto> response = _mapper.Map<GetListResponse<GetListByDoctorIdDto>>(doctorSchedules);
            return response;
        }
    }
}

