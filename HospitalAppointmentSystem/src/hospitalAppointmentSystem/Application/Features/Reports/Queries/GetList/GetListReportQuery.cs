using Application.Features.Reports.Constants;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using NArchitecture.Core.Application.Pipelines.Authorization;
using NArchitecture.Core.Application.Pipelines.Caching;
using NArchitecture.Core.Application.Requests;
using NArchitecture.Core.Application.Responses;
using NArchitecture.Core.Persistence.Paging;
using MediatR;
using static Application.Features.Reports.Constants.ReportsOperationClaims;
using Microsoft.EntityFrameworkCore;
using Application.Services.Encryptions;

namespace Application.Features.Reports.Queries.GetList;

public class GetListReportQuery : IRequest<GetListResponse<GetListReportListItemDto>>, ISecuredRequest
{
    public PageRequest PageRequest { get; set; }

    public string[] Roles => [Admin, Read];

    public bool BypassCache { get; }
    public string? CacheKey => $"GetListReports({PageRequest.PageIndex},{PageRequest.PageSize})";
    public string? CacheGroupKey => "GetReports";
    public TimeSpan? SlidingExpiration { get; }

    public class GetListReportQueryHandler : IRequestHandler<GetListReportQuery, GetListResponse<GetListReportListItemDto>>
    {
        private readonly IReportRepository _reportRepository;
        private readonly IMapper _mapper;

        public GetListReportQueryHandler(IReportRepository reportRepository, IMapper mapper)
        {
            _reportRepository = reportRepository;
            _mapper = mapper;
        }

        public async Task<GetListResponse<GetListReportListItemDto>> Handle(GetListReportQuery request, CancellationToken cancellationToken)
        {
            IPaginate<Report> reports = await _reportRepository.GetListAsync(
                index: request.PageRequest.PageIndex,
                size: request.PageRequest.PageSize,
                cancellationToken: cancellationToken,
                include: x => x.Include(x => x.Appointment).Include(x => x.Appointment.Patient).Include(x => x.Appointment.Doctor).Include(x => x.Appointment.Doctor.Branch),
                predicate:x=>x.DeletedDate==null
            );

            for (int i = 0; i < reports.Items.Count; i++)
            {
               
                reports.Items[i].Appointment.Patient.FirstName = CryptoHelper.Decrypt(reports.Items[i].Appointment.Patient.FirstName);
                reports.Items[i].Appointment.Patient.LastName = CryptoHelper.Decrypt(reports.Items[i].Appointment.Patient.LastName);
                reports.Items[i].Appointment.Patient.NationalIdentity = CryptoHelper.Decrypt(reports.Items[i].Appointment.Patient.NationalIdentity);
                //reports.Items[i].Appointment.Patient.Email = CryptoHelper.Decrypt(reports.Items[i].Appointment.Patient.Email);
                //reports.Items[i].Appointment.Patient.Phone = CryptoHelper.Decrypt(reports.Items[i].Appointment.Patient.Phone);
                reports.Items[i].Appointment.Doctor.FirstName = CryptoHelper.Decrypt(reports.Items[i].Appointment.Doctor.FirstName);
                reports.Items[i].Appointment.Doctor.LastName = CryptoHelper.Decrypt(reports.Items[i].Appointment.Doctor.LastName);
                //reports.Items[i].Appointment.Doctor.Address = CryptoHelper.Decrypt(reports.Items[i].Appointment.Doctor.Address);
                //reports.Items[i].Appointment.Doctor.Email = CryptoHelper.Decrypt(reports.Items[i].Appointment.Doctor.Email);
                //reports.Items[i].Appointment.Doctor.NationalIdentity = CryptoHelper.Decrypt(reports.Items[i].Appointment.Doctor.NationalIdentity);
                //reports.Items[i].Appointment.Doctor.Phone = CryptoHelper.Decrypt(reports.Items[i].Appointment.Doctor.Phone);


            }

            GetListResponse<GetListReportListItemDto> response = _mapper.Map<GetListResponse<GetListReportListItemDto>>(reports);
            return response;
        }
    }
}