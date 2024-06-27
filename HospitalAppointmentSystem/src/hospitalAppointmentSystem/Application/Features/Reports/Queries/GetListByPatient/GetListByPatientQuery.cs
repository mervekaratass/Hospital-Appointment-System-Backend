using Application.Features.Doctors.Constants;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using MediatR;
using NArchitecture.Core.Application.Pipelines.Authorization;
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
using Application.Features.Patients.Constants;

namespace Application.Features.Reports.Queries.GetListByPatient;
public class GetListByPatientQuery : IRequest<GetListResponse<GetListByPatientDto>>, ISecuredRequest
{
    public PageRequest PageRequest { get; set; }
    public Guid PatientId { get; set; }

    public string[] Roles => [Admin, Read, PatientsOperationClaims.Update];

    public bool BypassCache { get; }
    public string? CacheKey => $"GetListReports({PageRequest.PageIndex},{PageRequest.PageSize})";
    public string? CacheGroupKey => "GetReports";
    public TimeSpan? SlidingExpiration { get; }

    public class GetListByPatientQueryHandler : IRequestHandler<GetListByPatientQuery, GetListResponse<GetListByPatientDto>>
    {
        private readonly IReportRepository _reportRepository;
        private readonly IMapper _mapper;

        public GetListByPatientQueryHandler(IReportRepository reportRepository, IMapper mapper)
        {
            _reportRepository = reportRepository;
            _mapper = mapper;
        }

        public async Task<GetListResponse<GetListByPatientDto>> Handle(
            GetListByPatientQuery request,
            CancellationToken cancellationToken
        )
        {
            IPaginate<Report> reports = await _reportRepository.GetListAsync(
               index: request.PageRequest.PageIndex,
               size: request.PageRequest.PageSize,
               cancellationToken: cancellationToken,
               orderBy: x => x.OrderByDescending(y => y.CreatedDate),
               include: x => x.Include(x => x.Appointment).Include(x => x.Appointment.Doctor).Include(x => x.Appointment.Doctor.Branch).Include(x => x.Appointment.Patient),
               predicate: x => x.Appointment.PatientID == request.PatientId && x.DeletedDate == null

           );

            GetListResponse<GetListByPatientDto> response = _mapper.Map<GetListResponse<GetListByPatientDto>>(reports);
            return response;

        }
    }
}
