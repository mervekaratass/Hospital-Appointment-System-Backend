using Application.Features.Feedbacks.Constants;
using Application.Features.Feedbacks.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using NArchitecture.Core.Application.Pipelines.Authorization;
using MediatR;
using static Application.Features.Feedbacks.Constants.FeedbacksOperationClaims;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.Feedbacks.Queries.GetById;

public class GetByIdFeedbackQuery : IRequest<GetByIdFeedbackResponse>
{
    public int Id { get; set; }

    public string[] Roles => [Admin, Read];

    public class GetByIdFeedbackQueryHandler : IRequestHandler<GetByIdFeedbackQuery, GetByIdFeedbackResponse>
    {
        private readonly IMapper _mapper;
        private readonly IFeedbackRepository _feedbackRepository;
        private readonly FeedbackBusinessRules _feedbackBusinessRules;

        public GetByIdFeedbackQueryHandler(IMapper mapper, IFeedbackRepository feedbackRepository, FeedbackBusinessRules feedbackBusinessRules)
        {
            _mapper = mapper;
            _feedbackRepository = feedbackRepository;
            _feedbackBusinessRules = feedbackBusinessRules;
        }

        public async Task<GetByIdFeedbackResponse> Handle(GetByIdFeedbackQuery request, CancellationToken cancellationToken)
        {
            Feedback? feedback = await _feedbackRepository.GetAsync(predicate: f => f.Id == request.Id, cancellationToken: cancellationToken, include: x => x.Include(x => x.User));
            await _feedbackBusinessRules.FeedbackShouldExistWhenSelected(feedback);

            GetByIdFeedbackResponse response = _mapper.Map<GetByIdFeedbackResponse>(feedback);
            return response;
        }
    }
}