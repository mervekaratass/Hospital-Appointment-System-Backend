using Application.Features.Managers.Constants;
using Application.Features.Managers.Constants;
using Application.Features.Managers.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using NArchitecture.Core.Application.Pipelines.Authorization;
using NArchitecture.Core.Application.Pipelines.Caching;
using NArchitecture.Core.Application.Pipelines.Logging;
using NArchitecture.Core.Application.Pipelines.Transaction;
using MediatR;
using static Application.Features.Managers.Constants.ManagersOperationClaims;

namespace Application.Features.Managers.Commands.Delete;

public class DeleteManagerCommand : IRequest<DeletedManagerResponse>, ISecuredRequest,  ILoggableRequest, ITransactionalRequest
{
    public Guid Id { get; set; }

    public string[] Roles => [Admin, Write, ManagersOperationClaims.Delete];

    public bool BypassCache { get; }
    public string? CacheKey { get; }
    public string[]? CacheGroupKey => ["GetManagers"];

    public class DeleteManagerCommandHandler : IRequestHandler<DeleteManagerCommand, DeletedManagerResponse>
    {
        private readonly IMapper _mapper;
        private readonly IManagerRepository _managerRepository;
        private readonly ManagerBusinessRules _managerBusinessRules;

        public DeleteManagerCommandHandler(IMapper mapper, IManagerRepository managerRepository,
                                         ManagerBusinessRules managerBusinessRules)
        {
            _mapper = mapper;
            _managerRepository = managerRepository;
            _managerBusinessRules = managerBusinessRules;
        }

        public async Task<DeletedManagerResponse> Handle(DeleteManagerCommand request, CancellationToken cancellationToken)
        {
            Manager? manager = await _managerRepository.GetAsync(predicate: m => m.Id == request.Id, cancellationToken: cancellationToken);
            await _managerBusinessRules.ManagerShouldExistWhenSelected(manager);

            await _managerRepository.DeleteAsync(manager!);

            DeletedManagerResponse response = _mapper.Map<DeletedManagerResponse>(manager);
            return response;
        }
    }
}