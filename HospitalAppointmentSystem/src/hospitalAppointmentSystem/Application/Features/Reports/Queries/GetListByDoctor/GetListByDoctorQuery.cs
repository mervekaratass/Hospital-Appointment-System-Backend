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
using NArchitecture.Core.Application.Pipelines.Authorization;
using Application.Services.Encryptions;
using System.Numerics;

namespace Application.Features.Reports.Queries.GetListByDoctor;
public class GetListByDoctorQuery : IRequest<GetListResponse<GetListByDoctorDto>>, ISecuredRequest

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
            IPaginate<Report> reports = await _reportRepository.GetListAsync(
               index: request.PageRequest.PageIndex,
               size: request.PageRequest.PageSize,
               cancellationToken: cancellationToken,
                  orderBy: x => x.OrderByDescending(y => y.CreatedDate),
               include: x => x.Include(x => x.Appointment).Include(x => x.Appointment.Doctor).Include(x => x.Appointment.Patient),
                  predicate: x => x.Appointment.DoctorID == request.DoctorId  &&  x.DeletedDate==null

           );

            for (int i = 0; i < reports.Items.Count; i++)
            {
                reports.Items[i].Appointment.Doctor.FirstName = CryptoHelper.Decrypt(reports.Items[i].Appointment.Doctor.FirstName);
                reports.Items[i].Appointment.Doctor.LastName = CryptoHelper.Decrypt(reports.Items[i].Appointment.Doctor.LastName);
                reports.Items[i].Appointment.Patient.FirstName = CryptoHelper.Decrypt(reports.Items[i].Appointment.Patient.FirstName);
                reports.Items[i].Appointment.Patient.LastName = CryptoHelper.Decrypt(reports.Items[i].Appointment.Patient.LastName);
                reports.Items[i].Appointment.Patient.NationalIdentity = CryptoHelper.Decrypt(reports.Items[i].Appointment.Patient.NationalIdentity);
                reports.Items[i].Appointment.Patient.Email = CryptoHelper.Decrypt(reports.Items[i].Appointment.Patient.Email);
                reports.Items[i].Appointment.Patient.Phone = CryptoHelper.Decrypt(reports.Items[i].Appointment.Patient.Phone);

                reports.Items[i].Appointment.Doctor.Address = CryptoHelper.Decrypt(reports.Items[i].Appointment.Doctor.Address);
                reports.Items[i].Appointment.Doctor.Email = CryptoHelper.Decrypt(reports.Items[i].Appointment.Doctor.Email);
                reports.Items[i].Appointment.Doctor.NationalIdentity = CryptoHelper.Decrypt(reports.Items[i].Appointment.Doctor.NationalIdentity);
                reports.Items[i].Appointment.Doctor.Phone = CryptoHelper.Decrypt(reports.Items[i].Appointment.Doctor.Phone);


            }

            GetListResponse<GetListByDoctorDto> response = _mapper.Map<GetListResponse<GetListByDoctorDto>>(reports);
            return response;

        }
    }
}
