using Application.Features.Feedbacks.Rules;
using Application.Services.Repositories;
using NArchitecture.Core.Persistence.Paging;
using Domain.Entities;
using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;

namespace Application.Services.Feedbacks;

public class FeedbackManager : IFeedbackService
{
    private readonly IFeedbackRepository _feedbackRepository;
    private readonly FeedbackBusinessRules _feedbackBusinessRules;

    public FeedbackManager(IFeedbackRepository feedbackRepository, FeedbackBusinessRules feedbackBusinessRules)
    {
        _feedbackRepository = feedbackRepository;
        _feedbackBusinessRules = feedbackBusinessRules;
    }

    public async Task<Feedback?> GetAsync(
        Expression<Func<Feedback, bool>> predicate,
        Func<IQueryable<Feedback>, IIncludableQueryable<Feedback, object>>? include = null,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    )
    {
        Feedback? feedback = await _feedbackRepository.GetAsync(predicate, include, withDeleted, enableTracking, cancellationToken);
        return feedback;
    }

    public async Task<IPaginate<Feedback>?> GetListAsync(
        Expression<Func<Feedback, bool>>? predicate = null,
        Func<IQueryable<Feedback>, IOrderedQueryable<Feedback>>? orderBy = null,
        Func<IQueryable<Feedback>, IIncludableQueryable<Feedback, object>>? include = null,
        int index = 0,
        int size = 10,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    )
    {
        IPaginate<Feedback> feedbackList = await _feedbackRepository.GetListAsync(
            predicate,
            orderBy,
            include,
            index,
            size,
            withDeleted,
            enableTracking,
            cancellationToken
        );
        return feedbackList;
    }

    public async Task<Feedback> AddAsync(Feedback feedback)
    {
        Feedback addedFeedback = await _feedbackRepository.AddAsync(feedback);

        return addedFeedback;
    }

    public async Task<Feedback> UpdateAsync(Feedback feedback)
    {
        Feedback updatedFeedback = await _feedbackRepository.UpdateAsync(feedback);

        return updatedFeedback;
    }

    public async Task<Feedback> DeleteAsync(Feedback feedback, bool permanent = false)
    {
        Feedback deletedFeedback = await _feedbackRepository.DeleteAsync(feedback);

        return deletedFeedback;
    }
}
