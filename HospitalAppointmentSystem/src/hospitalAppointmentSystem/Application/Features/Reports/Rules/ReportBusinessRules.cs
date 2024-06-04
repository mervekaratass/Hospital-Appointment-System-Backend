using Application.Features.Reports.Constants;
using Application.Services.Repositories;
using NArchitecture.Core.Application.Rules;
using NArchitecture.Core.CrossCuttingConcerns.Exception.Types;
using NArchitecture.Core.Localization.Abstraction;
using Domain.Entities;

namespace Application.Features.Reports.Rules;

public class ReportBusinessRules : BaseBusinessRules
{
    private readonly IReportRepository _reportRepository;
    private readonly ILocalizationService _localizationService;

    public ReportBusinessRules(IReportRepository reportRepository, ILocalizationService localizationService)
    {
        _reportRepository = reportRepository;
        _localizationService = localizationService;
    }

    private async Task throwBusinessException(string messageKey)
    {
        string message = await _localizationService.GetLocalizedAsync(messageKey, ReportsBusinessMessages.SectionName);
        throw new BusinessException(message);
    }

    public async Task ReportShouldExistWhenSelected(Report? report)
    {
        if (report == null)
            await throwBusinessException(ReportsBusinessMessages.ReportNotExists);
    }

    public async Task ReportIdShouldExistWhenSelected(int id, CancellationToken cancellationToken)
    {
        Report? report = await _reportRepository.GetAsync(
            predicate: r => r.Id == id,
            enableTracking: false,
            cancellationToken: cancellationToken
        );
        await ReportShouldExistWhenSelected(report);
    }
}