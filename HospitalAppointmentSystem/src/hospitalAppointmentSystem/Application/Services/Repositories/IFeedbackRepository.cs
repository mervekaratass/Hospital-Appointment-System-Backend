using Domain.Entities;
using NArchitecture.Core.Persistence.Repositories;

namespace Application.Services.Repositories;

public interface IFeedbackRepository : IAsyncRepository<Feedback, int>, IRepository<Feedback, int>
{
}