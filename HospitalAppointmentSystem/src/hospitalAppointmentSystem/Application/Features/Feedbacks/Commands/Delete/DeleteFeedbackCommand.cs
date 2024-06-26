using Application.Features.Feedbacks.Constants;
using Application.Features.Feedbacks.Constants;
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

namespace Application.Features.Feedbacks.Commands.Delete;

public class DeleteFeedbackCommand : IRequest<DeletedFeedbackResponse>,  ILoggableRequest, ITransactionalRequest
{
    public int Id { get; set; }

    public string[] Roles => [Admin, Write, FeedbacksOperationClaims.Delete, PatientsOperationClaims.Update, DoctorsOperationClaims.Update];

    public bool BypassCache { get; }
    public string? CacheKey { get; }
    public string[]? CacheGroupKey => ["GetFeedbacks"];

    public class DeleteFeedbackCommandHandler : IRequestHandler<DeleteFeedbackCommand, DeletedFeedbackResponse>
    {
        private readonly IMapper _mapper;
        private readonly IFeedbackRepository _feedbackRepository;
        private readonly FeedbackBusinessRules _feedbackBusinessRules;

        public DeleteFeedbackCommandHandler(IMapper mapper, IFeedbackRepository feedbackRepository,
                                         FeedbackBusinessRules feedbackBusinessRules)
        {
            _mapper = mapper;
            _feedbackRepository = feedbackRepository;
            _feedbackBusinessRules = feedbackBusinessRules;
        }

        public async Task<DeletedFeedbackResponse> Handle(DeleteFeedbackCommand request, CancellationToken cancellationToken)
        {
            Feedback? feedback = await _feedbackRepository.GetAsync(predicate: f => f.Id == request.Id, cancellationToken: cancellationToken);
            await _feedbackBusinessRules.FeedbackShouldExistWhenSelected(feedback);

            await _feedbackRepository.DeleteAsync(feedback!);

            DeletedFeedbackResponse response = _mapper.Map<DeletedFeedbackResponse>(feedback);
            return response;
        }
    }
}