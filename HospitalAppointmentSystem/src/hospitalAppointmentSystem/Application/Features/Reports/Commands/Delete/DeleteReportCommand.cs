using Application.Features.Reports.Constants;
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

namespace Application.Features.Reports.Commands.Delete;

public class DeleteReportCommand : IRequest<DeletedReportResponse>, ISecuredRequest, ICacheRemoverRequest, ILoggableRequest, ITransactionalRequest
{
    public int Id { get; set; }

    public string[] Roles => [Admin, Write, ReportsOperationClaims.Delete,DoctorsOperationClaims.Update];

    public bool BypassCache { get; }
    public string? CacheKey { get; }
    public string[]? CacheGroupKey => ["GetReports"];

    public class DeleteReportCommandHandler : IRequestHandler<DeleteReportCommand, DeletedReportResponse>
    {
        private readonly IMapper _mapper;
        private readonly IReportRepository _reportRepository;
        private readonly ReportBusinessRules _reportBusinessRules;

        public DeleteReportCommandHandler(IMapper mapper, IReportRepository reportRepository,
                                         ReportBusinessRules reportBusinessRules)
        {
            _mapper = mapper;
            _reportRepository = reportRepository;
            _reportBusinessRules = reportBusinessRules;
        }

        public async Task<DeletedReportResponse> Handle(DeleteReportCommand request, CancellationToken cancellationToken)
        {
            Report? report = await _reportRepository.GetAsync(predicate: r => r.Id == request.Id, cancellationToken: cancellationToken);
            await _reportBusinessRules.ReportShouldExistWhenSelected(report);

            report.DeletedDate= DateTime.UtcNow;
         
            await _reportRepository.UpdateAsync(report!);
            

            DeletedReportResponse response = _mapper.Map<DeletedReportResponse>(report);
            return response;
        }
    }
}