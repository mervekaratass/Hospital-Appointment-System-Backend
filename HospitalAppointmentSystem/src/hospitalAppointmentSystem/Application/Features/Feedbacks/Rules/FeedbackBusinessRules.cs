using Application.Services.Repositories;
using NArchitecture.Core.Application.Rules;
using NArchitecture.Core.CrossCuttingConcerns.Exception.Types;
using NArchitecture.Core.Localization.Abstraction;
using Domain.Entities;
using Application.Features.Feedbacks.Commands.Constants;

namespace Application.Features.Feedbacks.Rules;

public class FeedbackBusinessRules : BaseBusinessRules
{
    private readonly IFeedbackRepository _feedbackRepository;
    private readonly ILocalizationService _localizationService;

    public FeedbackBusinessRules(IFeedbackRepository feedbackRepository, ILocalizationService localizationService)
    {
        _feedbackRepository = feedbackRepository;
        _localizationService = localizationService;
    }

    private async Task throwBusinessException(string messageKey)
    {
        string message = await _localizationService.GetLocalizedAsync(messageKey, FeedbacksBusinessMessages.SectionName);
        throw new BusinessException(message);
    }

    public async Task FeedbackShouldExistWhenSelected(Feedback? feedback)
    {
        if (feedback == null)
            await throwBusinessException(FeedbacksBusinessMessages.FeedbackNotExists);
    }

    public async Task FeedbackIdShouldExistWhenSelected(int id, CancellationToken cancellationToken)
    {
        Feedback? feedback = await _feedbackRepository.GetAsync(
            predicate: f => f.Id == id,
            enableTracking: false,
            cancellationToken: cancellationToken
        );
        await FeedbackShouldExistWhenSelected(feedback);
    }
}