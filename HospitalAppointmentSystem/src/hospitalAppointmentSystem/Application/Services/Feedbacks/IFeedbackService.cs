using NArchitecture.Core.Persistence.Paging;
using Domain.Entities;
using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;

namespace Application.Services.Feedbacks;

public interface IFeedbackService
{
    Task<Feedback?> GetAsync(
        Expression<Func<Feedback, bool>> predicate,
        Func<IQueryable<Feedback>, IIncludableQueryable<Feedback, object>>? include = null,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    );
    Task<IPaginate<Feedback>?> GetListAsync(
        Expression<Func<Feedback, bool>>? predicate = null,
        Func<IQueryable<Feedback>, IOrderedQueryable<Feedback>>? orderBy = null,
        Func<IQueryable<Feedback>, IIncludableQueryable<Feedback, object>>? include = null,
        int index = 0,
        int size = 10,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    );
    Task<Feedback> AddAsync(Feedback feedback);
    Task<Feedback> UpdateAsync(Feedback feedback);
    Task<Feedback> DeleteAsync(Feedback feedback, bool permanent = false);
}
