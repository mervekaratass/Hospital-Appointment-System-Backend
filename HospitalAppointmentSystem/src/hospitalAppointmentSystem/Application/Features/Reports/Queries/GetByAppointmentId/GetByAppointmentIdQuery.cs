using Application.Features.Doctors.Constants;
using Application.Features.Reports.Queries.GetById;
using Application.Features.Reports.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using MediatR;
using NArchitecture.Core.Application.Pipelines.Authorization;
using static Application.Features.Reports.Constants.ReportsOperationClaims;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Application.Features.Patients.Constants;

namespace Application.Features.Reports.Queries.GetByAppointmentId;
public class GetByAppointmentIdQuery : IRequest<GetByAppointmentIdResponse>, ISecuredRequest
{
    public int Id { get; set; }

    public string[] Roles => [Admin, Read, DoctorsOperationClaims.Update, PatientsOperationClaims.Update];

    public class GetByAppointmentIdQueryHandler : IRequestHandler<GetByAppointmentIdQuery, GetByAppointmentIdResponse>
    {
        private readonly IMapper _mapper;
        private readonly IReportRepository _reportRepository;
        private readonly ReportBusinessRules _reportBusinessRules;

        public GetByAppointmentIdQueryHandler(IMapper mapper, IReportRepository reportRepository, ReportBusinessRules reportBusinessRules)
        {
            _mapper = mapper;
            _reportRepository = reportRepository;
            _reportBusinessRules = reportBusinessRules;
        }

        public async Task<GetByAppointmentIdResponse> Handle(GetByAppointmentIdQuery request, CancellationToken cancellationToken)
        {
            Report? report = await _reportRepository.GetAsync(predicate: r => r.AppointmentID == request.Id && r.DeletedDate==null , include: x => x.Include(x => x.Appointment).Include(x => x.Appointment.Patient).Include(x => x.Appointment.Doctor),
                cancellationToken: cancellationToken);
         



            GetByAppointmentIdResponse response = _mapper.Map<GetByAppointmentIdResponse>(report);

            return response;
        }
    }
}
