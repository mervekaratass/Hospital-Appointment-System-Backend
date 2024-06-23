using Application.Features.Branches.Constants;
using Application.Features.Branches.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using NArchitecture.Core.Application.Pipelines.Authorization;
using MediatR;
using static Application.Features.Branches.Constants.BranchesOperationClaims;

namespace Application.Features.Branches.Queries.GetByName;

public class GetByNameBranchQuery : IRequest<GetByNameBranchResponse>, ISecuredRequest
{
    public string Name { get; set; }

    public string[] Roles => [Admin, Read];

    public class GetByNameBranchQueryHandler : IRequestHandler<GetByNameBranchQuery, GetByNameBranchResponse>
    {
        private readonly IMapper _mapper;
        private readonly IBranchRepository _branchRepository;
        private readonly BranchBusinessRules _branchBusinessRules;

        public GetByNameBranchQueryHandler(IMapper mapper, IBranchRepository branchRepository, BranchBusinessRules branchBusinessRules)
        {
            _mapper = mapper;
            _branchRepository = branchRepository;
            _branchBusinessRules = branchBusinessRules;
        }

        public async Task<GetByNameBranchResponse> Handle(GetByNameBranchQuery request, CancellationToken cancellationToken)
        {
            Branch? branch = await _branchRepository.GetAsync(predicate: b => b.Name == request.Name, cancellationToken: cancellationToken);
            await _branchBusinessRules.BranchShouldExistWhenSelected(branch);

            GetByNameBranchResponse response = _mapper.Map<GetByNameBranchResponse>(branch);
            return response;
        }
    }
}