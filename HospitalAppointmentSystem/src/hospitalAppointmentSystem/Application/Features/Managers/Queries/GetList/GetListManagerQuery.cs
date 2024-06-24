using Application.Features.Managers.Constants;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using NArchitecture.Core.Application.Pipelines.Authorization;
using NArchitecture.Core.Application.Pipelines.Caching;
using NArchitecture.Core.Application.Requests;
using NArchitecture.Core.Application.Responses;
using NArchitecture.Core.Persistence.Paging;
using MediatR;
using static Application.Features.Managers.Constants.ManagersOperationClaims;

namespace Application.Features.Managers.Queries.GetList;

public class GetListManagerQuery : IRequest<GetListResponse<GetListManagerListItemDto>>
{
    public PageRequest PageRequest { get; set; }

    public string[] Roles => [Admin, Read];

    public bool BypassCache { get; }
    public string? CacheKey => $"GetListManagers({PageRequest.PageIndex},{PageRequest.PageSize})";
    public string? CacheGroupKey => "GetManagers";
    public TimeSpan? SlidingExpiration { get; }

    public class GetListManagerQueryHandler : IRequestHandler<GetListManagerQuery, GetListResponse<GetListManagerListItemDto>>
    {
        private readonly IManagerRepository _managerRepository;
        private readonly IMapper _mapper;

        public GetListManagerQueryHandler(IManagerRepository managerRepository, IMapper mapper)
        {
            _managerRepository = managerRepository;
            _mapper = mapper;
        }

        public async Task<GetListResponse<GetListManagerListItemDto>> Handle(GetListManagerQuery request, CancellationToken cancellationToken)
        {
            IPaginate<Manager> managers = await _managerRepository.GetListAsync(
                index: request.PageRequest.PageIndex,
                size: request.PageRequest.PageSize, 
                cancellationToken: cancellationToken
            );

            GetListResponse<GetListManagerListItemDto> response = _mapper.Map<GetListResponse<GetListManagerListItemDto>>(managers);
            return response;
        }
    }
}