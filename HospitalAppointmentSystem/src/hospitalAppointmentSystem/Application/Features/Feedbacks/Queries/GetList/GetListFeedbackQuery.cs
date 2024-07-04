using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using NArchitecture.Core.Application.Pipelines.Authorization;
using NArchitecture.Core.Application.Pipelines.Caching;
using NArchitecture.Core.Application.Requests;
using NArchitecture.Core.Application.Responses;
using NArchitecture.Core.Persistence.Paging;
using MediatR;
using static Application.Features.Feedbacks.Commands.Constants.FeedbacksOperationClaims;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using Application.Services.Encryptions;

namespace Application.Features.Feedbacks.Queries.GetList;

public class GetListFeedbackQuery : IRequest<GetListResponse<GetListFeedbackListItemDto>>,  ISecuredRequest
{
    public PageRequest PageRequest { get; set; }

    public string[] Roles => [Admin, Read];

    public bool BypassCache { get; }
    public string? CacheKey => $"GetListFeedbacks({PageRequest.PageIndex},{PageRequest.PageSize})";
    public string? CacheGroupKey => "GetFeedbacks";
    public TimeSpan? SlidingExpiration { get; }

    public class GetListFeedbackQueryHandler : IRequestHandler<GetListFeedbackQuery, GetListResponse<GetListFeedbackListItemDto>>
    {
        private readonly IFeedbackRepository _feedbackRepository;
        private readonly IMapper _mapper;

        public GetListFeedbackQueryHandler(IFeedbackRepository feedbackRepository, IMapper mapper)
        {
            _feedbackRepository = feedbackRepository;
            _mapper = mapper;
        }

        public async Task<GetListResponse<GetListFeedbackListItemDto>> Handle(GetListFeedbackQuery request, CancellationToken cancellationToken)
        {
            // deletedDate null olan geri bildirimleri filtrelemek için bir lambda ifadesi tanýmlýyoruz
            Expression<Func<Feedback, bool>> filter = feedback => feedback.DeletedDate == null;
            IPaginate<Feedback> feedbacks = await _feedbackRepository.GetListAsync(
                index: request.PageRequest.PageIndex,
                size: request.PageRequest.PageSize, 
                cancellationToken: cancellationToken,
                 include: x => x.Include(x => x.User),
                 predicate: filter
            );


            for (int i = 0; i < feedbacks.Items.Count; i++)
            {
                feedbacks.Items[i].User.FirstName = CryptoHelper.Decrypt(feedbacks.Items[i].User.FirstName);
                feedbacks.Items[i].User.LastName = CryptoHelper.Decrypt(feedbacks.Items[i].User.LastName);

            }

            GetListResponse<GetListFeedbackListItemDto> response = _mapper.Map<GetListResponse<GetListFeedbackListItemDto>>(feedbacks);
            return response;
        }
    }
}