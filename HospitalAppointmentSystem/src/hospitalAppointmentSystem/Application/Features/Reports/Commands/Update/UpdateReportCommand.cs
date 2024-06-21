using Application.Features.Reports.Constants;
using Application.Features.Reports.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using NArchitecture.Core.Application.Pipelines.Authorization;
using NArchitecture.Core.Application.Pipelines.Caching;
using NArchitecture.Core.Application.Pipelines.Logging;
using NArchitecture.Core.Application.Pipelines.Transaction;
using MediatR;
using static Application.Features.Reports.Constants.ReportsOperationClaims;
using Application.Features.Doctors.Constants;

namespace Application.Features.Reports.Commands.Update;

public class UpdateReportCommand : IRequest<UpdatedReportResponse>, ISecuredRequest, ICacheRemoverRequest, ILoggableRequest, ITransactionalRequest
{
    public int Id { get; set; }
   // public required int AppointmentID { get; set; }
    public required string Text { get; set; }

    public string[] Roles => [Admin, Write, ReportsOperationClaims.Update,DoctorsOperationClaims.Update];

    public bool BypassCache { get; }
    public string? CacheKey { get; }
    public string[]? CacheGroupKey => ["GetReports"];

    public class UpdateReportCommandHandler : IRequestHandler<UpdateReportCommand, UpdatedReportResponse>
    {
        private readonly IMapper _mapper;
        private readonly IReportRepository _reportRepository;
        private readonly ReportBusinessRules _reportBusinessRules;

        public UpdateReportCommandHandler(IMapper mapper, IReportRepository reportRepository,
                                         ReportBusinessRules reportBusinessRules)
        {
            _mapper = mapper;
            _reportRepository = reportRepository;
            _reportBusinessRules = reportBusinessRules;
        }

        public async Task<UpdatedReportResponse> Handle(UpdateReportCommand request, CancellationToken cancellationToken)
        {
            Report? report = await _reportRepository.GetAsync(predicate: r => r.Id == request.Id, cancellationToken: cancellationToken);
            await _reportBusinessRules.ReportShouldExistWhenSelected(report);
            report = _mapper.Map(request, report);

            await _reportRepository.UpdateAsync(report!);

            UpdatedReportResponse response = _mapper.Map<UpdatedReportResponse>(report);
            return response;
        }
    }
}