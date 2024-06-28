using Application.Features.Branches.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using MediatR;
using NArchitecture.Core.Application.Pipelines.Authorization;
using static Application.Features.Branches.Constants.BranchesOperationClaims;

namespace Application.Features.Branches.Queries.GetByName;
public class GetByNameBranchWithoutControlQuery : IRequest<GetByNameBranchResponse>, ISecuredRequest
{
    public string Name { get; set; }

    public string[] Roles => [Admin, Read];

    public class GetByNameBranchWithoutControlQueryHandler : IRequestHandler<GetByNameBranchWithoutControlQuery, GetByNameBranchResponse>
    {
        private readonly IMapper _mapper;
        private readonly IBranchRepository _branchRepository;
        private readonly BranchBusinessRules _branchBusinessRules;

        public GetByNameBranchWithoutControlQueryHandler(IMapper mapper, IBranchRepository branchRepository, BranchBusinessRules branchBusinessRules)
        {
            _mapper = mapper;
            _branchRepository = branchRepository;
            _branchBusinessRules = branchBusinessRules;
        }

        public async Task<GetByNameBranchResponse> Handle(GetByNameBranchWithoutControlQuery request, CancellationToken cancellationToken)
        {
           
                Branch? branch = await _branchRepository.GetAsync(predicate: b => b.Name == request.Name && b.DeletedDate==null, cancellationToken: cancellationToken);

                GetByNameBranchResponse? response = _mapper.Map<GetByNameBranchResponse>(branch);
                return response;
            
        }
    }
}