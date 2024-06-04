using Application.Features.Reports.Rules;
using Application.Services.Repositories;
using NArchitecture.Core.Persistence.Paging;
using Domain.Entities;
using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;

namespace Application.Services.Reports;

public class ReportManager : IReportService
{
    private readonly IReportRepository _reportRepository;
    private readonly ReportBusinessRules _reportBusinessRules;

    public ReportManager(IReportRepository reportRepository, ReportBusinessRules reportBusinessRules)
    {
        _reportRepository = reportRepository;
        _reportBusinessRules = reportBusinessRules;
    }

    public async Task<Report?> GetAsync(
        Expression<Func<Report, bool>> predicate,
        Func<IQueryable<Report>, IIncludableQueryable<Report, object>>? include = null,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    )
    {
        Report? report = await _reportRepository.GetAsync(predicate, include, withDeleted, enableTracking, cancellationToken);
        return report;
    }

    public async Task<IPaginate<Report>?> GetListAsync(
        Expression<Func<Report, bool>>? predicate = null,
        Func<IQueryable<Report>, IOrderedQueryable<Report>>? orderBy = null,
        Func<IQueryable<Report>, IIncludableQueryable<Report, object>>? include = null,
        int index = 0,
        int size = 10,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    )
    {
        IPaginate<Report> reportList = await _reportRepository.GetListAsync(
            predicate,
            orderBy,
            include,
            index,
            size,
            withDeleted,
            enableTracking,
            cancellationToken
        );
        return reportList;
    }

    public async Task<Report> AddAsync(Report report)
    {
        Report addedReport = await _reportRepository.AddAsync(report);

        return addedReport;
    }

    public async Task<Report> UpdateAsync(Report report)
    {
        Report updatedReport = await _reportRepository.UpdateAsync(report);

        return updatedReport;
    }

    public async Task<Report> DeleteAsync(Report report, bool permanent = false)
    {
        Report deletedReport = await _reportRepository.DeleteAsync(report);

        return deletedReport;
    }
}
