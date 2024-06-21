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
using Application.Features.Patients.Constants;
using Application.Features.Doctors.Constants;

namespace Application.Features.Feedbacks.Commands.Create;

public class CreateFeedbackCommand : IRequest<CreatedFeedbackResponse>, ICacheRemoverRequest, ILoggableRequest, ITransactionalRequest
{
    public required Guid UserID { get; set; }
    public required string Text { get; set; }

    public string[] Roles => [Admin, Write, FeedbacksOperationClaims.Create, PatientsOperationClaims.Update,DoctorsOperationClaims.Update];

    public bool BypassCache { get; }
    public string? CacheKey { get; }
    public string[]? CacheGroupKey => ["GetFeedbacks"];

    public class CreateFeedbackCommandHandler : IRequestHandler<CreateFeedbackCommand, CreatedFeedbackResponse>
    {
        private readonly IMapper _mapper;
        private readonly IFeedbackRepository _feedbackRepository;
        private readonly FeedbackBusinessRules _feedbackBusinessRules;

        public CreateFeedbackCommandHandler(IMapper mapper, IFeedbackRepository feedbackRepository,
                                         FeedbackBusinessRules feedbackBusinessRules)
        {
            _mapper = mapper;
            _feedbackRepository = feedbackRepository;
            _feedbackBusinessRules = feedbackBusinessRules;
        }

        public async Task<CreatedFeedbackResponse> Handle(CreateFeedbackCommand request, CancellationToken cancellationToken)
        {
            Feedback feedback = _mapper.Map<Feedback>(request);

            await _feedbackRepository.AddAsync(feedback);

            CreatedFeedbackResponse response = _mapper.Map<CreatedFeedbackResponse>(feedback);
            return response;
        }
    }
}