using Application.Services.Repositories;
using Domain.Entities;
using NArchitecture.Core.Persistence.Repositories;
using Persistence.Contexts;

namespace Persistence.Repositories;

public class FeedbackRepository : EfRepositoryBase<Feedback, int, BaseDbContext>, IFeedbackRepository
{
    public FeedbackRepository(BaseDbContext context) : base(context)
    {
    }
}