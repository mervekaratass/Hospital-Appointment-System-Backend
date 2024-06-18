using Application.Features.Doctors.Constants;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using MediatR;
using NArchitecture.Core.Application.Pipelines.Caching;
using NArchitecture.Core.Application.Requests;
using NArchitecture.Core.Application.Responses;
using NArchitecture.Core.Persistence.Paging;
using static Application.Features.Appointments.Constants.AppointmentsOperationClaims;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.Reports.Queries.GetListByDoctor;
public class GetListByDoctorQuery : IRequest<GetListResponse<GetListByDoctorDto>>, ICachableRequest

{
    public PageRequest PageRequest { get; set; }
    public Guid DoctorId { get; set; }

    public string[] Roles => [Admin, Read, DoctorsOperationClaims.Update];

    public bool BypassCache { get; }
    public string? CacheKey => $"GetListReports({PageRequest.PageIndex},{PageRequest.PageSize})";
    public string? CacheGroupKey => "GetReports";
    public TimeSpan? SlidingExpiration { get; }

    public class GetListByDoctorQueryHandler : IRequestHandler<GetListByDoctorQuery, GetListResponse<GetListByDoctorDto>>
    {
        private readonly IReportRepository _reportRepository;
        private readonly IMapper _mapper;

        public GetListByDoctorQueryHandler(IReportRepository reportRepository, IMapper mapper)
        {
            _reportRepository = reportRepository;
            _mapper = mapper;
        }

        public async Task<GetListResponse<GetListByDoctorDto>> Handle(
            GetListByDoctorQuery request,
            CancellationToken cancellationToken
        )
        {
            IPaginate<Report> appointments = await _reportRepository.GetListAsync(
               index: request.PageRequest.PageIndex,
               size: request.PageRequest.PageSize,
               cancellationToken: cancellationToken,
                  orderBy: x => x.OrderByDescending(y => y.CreatedDate),
               include: x => x.Include(x=>x.Appointment).Include(x => x.Appointment.Doctor).Include(x=>x.Appointment.Patient),
                  predicate: x => x.Appointment.DoctorID == request.DoctorId
           );

            GetListResponse<GetListByDoctorDto> response = _mapper.Map<GetListResponse<GetListByDoctorDto>>(appointments);
            return response;

        }
    }
}
