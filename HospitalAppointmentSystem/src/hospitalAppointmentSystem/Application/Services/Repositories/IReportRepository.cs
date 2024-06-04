using Domain.Entities;
using NArchitecture.Core.Persistence.Repositories;

namespace Application.Services.Repositories;

public interface IReportRepository : IAsyncRepository<Report, int>, IRepository<Report, int>
{
}