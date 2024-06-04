using Application.Services.Repositories;
using Domain.Entities;
using NArchitecture.Core.Persistence.Repositories;
using Persistence.Contexts;

namespace Persistence.Repositories;

public class ReportRepository : EfRepositoryBase<Report, int, BaseDbContext>, IReportRepository
{
    public ReportRepository(BaseDbContext context) : base(context)
    {
    }
}