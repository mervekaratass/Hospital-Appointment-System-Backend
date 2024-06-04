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

namespace Application.Features.Reports.Commands.Create;

public class CreateReportCommand : IRequest<CreatedReportResponse>, ISecuredRequest, ICacheRemoverRequest, ILoggableRequest, ITransactionalRequest
{
    public required int AppointmentID { get; set; }
    public required string Text { get; set; }

    public string[] Roles => [Admin, Write, ReportsOperationClaims.Create];

    public bool BypassCache { get; }
    public string? CacheKey { get; }
    public string[]? CacheGroupKey => ["GetReports"];

    public class CreateReportCommandHandler : IRequestHandler<CreateReportCommand, CreatedReportResponse>
    {
        private readonly IMapper _mapper;
        private readonly IReportRepository _reportRepository;
        private readonly ReportBusinessRules _reportBusinessRules;

        public CreateReportCommandHandler(IMapper mapper, IReportRepository reportRepository,
                                         ReportBusinessRules reportBusinessRules)
        {
            _mapper = mapper;
            _reportRepository = reportRepository;
            _reportBusinessRules = reportBusinessRules;
        }

        public async Task<CreatedReportResponse> Handle(CreateReportCommand request, CancellationToken cancellationToken)
        {
            Report report = _mapper.Map<Report>(request);

            await _reportRepository.AddAsync(report);

            CreatedReportResponse response = _mapper.Map<CreatedReportResponse>(report);
            return response;
        }
    }
}