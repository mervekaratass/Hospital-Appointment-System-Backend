using Application.Services.Repositories;
using Domain.Entities;
using NArchitecture.Core.Persistence.Repositories;
using Persistence.Contexts;

namespace Persistence.Repositories;

public class BranchRepository : EfRepositoryBase<Branch, int, BaseDbContext>, IBranchRepository
{
    public BranchRepository(BaseDbContext context) : base(context)
    {
    }
}