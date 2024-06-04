using Application.Services.Repositories;
using Domain.Entities;
using NArchitecture.Core.Persistence.Repositories;
using Persistence.Contexts;

namespace Persistence.Repositories;

public class ManagerRepository : EfRepositoryBase<Manager, Guid, BaseDbContext>, IManagerRepository
{
    public ManagerRepository(BaseDbContext context) : base(context)
    {
    }
}