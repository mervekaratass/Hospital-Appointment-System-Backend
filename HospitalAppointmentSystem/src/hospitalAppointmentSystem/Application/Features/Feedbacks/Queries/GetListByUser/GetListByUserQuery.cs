using Application.Features.Feedbacks.Queries.GetList;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using NArchitecture.Core.Application.Pipelines.Caching;
using NArchitecture.Core.Application.Requests;
using NArchitecture.Core.Application.Responses;
using NArchitecture.Core.Persistence.Paging;
using static Application.Features.Feedbacks.Commands.Constants.FeedbacksOperationClaims;
using System;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using Application.Features.Doctors.Constants;
using Application.Features.Patients.Constants;

namespace Application.Features.Feedbacks.Queries.GetListByUser
{
    // Kullanıcıya ait geri bildirimleri listeleyen sorgu
    public class GetListByUserQuery : IRequest<GetListResponse<GetListByUserDto>>
    {
        public PageRequest PageRequest { get; set; }
        public Guid UserId { get; set; }

        public string[] Roles => [Admin, Read,DoctorsOperationClaims.Update,PatientsOperationClaims.Update];

        public bool BypassCache { get; set; }
        public string? CacheKey => $"GetListFeedbacks({PageRequest.PageIndex},{PageRequest.PageSize})";
        public string? CacheGroupKey => "GetFeedbacks";
        public TimeSpan? SlidingExpiration { get; set; }

        // Sorgu işleyicisi
        public class GetListByUserQueryHandler : IRequestHandler<GetListByUserQuery, GetListResponse<GetListByUserDto>>
        {
            private readonly IFeedbackRepository _feedbackRepository;
            private readonly IMapper _mapper;

            public GetListByUserQueryHandler(IFeedbackRepository feedbackRepository, IMapper mapper)
            {
                _feedbackRepository = feedbackRepository;
                _mapper = mapper;
            }

            public async Task<GetListResponse<GetListByUserDto>> Handle(GetListByUserQuery request, CancellationToken cancellationToken)
            {
                IPaginate<Feedback> feedbacks = await _feedbackRepository.GetListAsync(
                    index: request.PageRequest.PageIndex,
                    size: request.PageRequest.PageSize,
                    cancellationToken: cancellationToken,
                    include: x => x.Include(x => x.User),
                    predicate: x => x.UserID == request.UserId && x.DeletedDate == null 
                );

                // DTO'ya dönüştür
                GetListResponse<GetListByUserDto> response = _mapper.Map<GetListResponse<GetListByUserDto>>(feedbacks);
                return response;
            }
        }
    }
}
