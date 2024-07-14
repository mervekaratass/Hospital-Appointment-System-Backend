using Application.Features.Feedbacks.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using NArchitecture.Core.Application.Pipelines.Authorization;
using NArchitecture.Core.Application.Pipelines.Caching;
using NArchitecture.Core.Application.Pipelines.Logging;
using NArchitecture.Core.Application.Pipelines.Transaction;
using MediatR;
using static Application.Features.Feedbacks.Constants.FeedbacksOperationClaims;
using Application.Features.Doctors.Constants;
using Application.Features.Patients.Constants;
using Application.Features.Feedbacks.Constants;

namespace Application.Features.Feedbacks.Commands.Update;

public class UpdateFeedbackCommand : IRequest<UpdatedFeedbackResponse>,  ILoggableRequest, ITransactionalRequest
{
    public int Id { get; set; }
    public required Guid UserID { get; set; }
    public required string Text { get; set; }
    public bool IsApproved { get; set; }

    public string[] Roles => [Admin, Write, FeedbacksOperationClaims.Update, PatientsOperationClaims.Update, DoctorsOperationClaims.Update];

    public bool BypassCache { get; }
    public string? CacheKey { get; }
    public string[]? CacheGroupKey => ["GetFeedbacks"];

    public class UpdateFeedbackCommandHandler : IRequestHandler<UpdateFeedbackCommand, UpdatedFeedbackResponse>
    {
        private readonly IMapper _mapper;
        private readonly IFeedbackRepository _feedbackRepository;
        private readonly FeedbackBusinessRules _feedbackBusinessRules;

        public UpdateFeedbackCommandHandler(IMapper mapper, IFeedbackRepository feedbackRepository,
                                         FeedbackBusinessRules feedbackBusinessRules)
        {
            _mapper = mapper;
            _feedbackRepository = feedbackRepository;
            _feedbackBusinessRules = feedbackBusinessRules;
        }

        public async Task<UpdatedFeedbackResponse> Handle(UpdateFeedbackCommand request, CancellationToken cancellationToken)
        {
            Feedback? feedback = await _feedbackRepository.GetAsync(predicate: f => f.Id == request.Id, cancellationToken: cancellationToken);
            await _feedbackBusinessRules.FeedbackShouldExistWhenSelected(feedback);
            feedback = _mapper.Map(request, feedback);

            await _feedbackRepository.UpdateAsync(feedback!);

            UpdatedFeedbackResponse response = _mapper.Map<UpdatedFeedbackResponse>(feedback);
            return response;
        }
    }
}