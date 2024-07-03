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
using Application.Services.Encryptions;

namespace Application.Features.Managers.Commands.Update;

public class UpdateManagerCommand : IRequest<UpdatedManagerResponse>, ISecuredRequest,  ILoggableRequest, ITransactionalRequest
{
    public Guid Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public DateOnly DateOfBirth { get; set; }
    public string NationalIdentity { get; set; }
    public string Phone { get; set; }
    public string Address { get; set; }

    public string[] Roles => [Admin, Write, ManagersOperationClaims.Update];

    public bool BypassCache { get; }
    public string? CacheKey { get; }
    public string[]? CacheGroupKey => ["GetManagers"];

    public class UpdateManagerCommandHandler : IRequestHandler<UpdateManagerCommand, UpdatedManagerResponse>
    {
        private readonly IMapper _mapper;
        private readonly IManagerRepository _managerRepository;
        private readonly ManagerBusinessRules _managerBusinessRules;

        public UpdateManagerCommandHandler(IMapper mapper, IManagerRepository managerRepository,
                                         ManagerBusinessRules managerBusinessRules)
        {
            _mapper = mapper;
            _managerRepository = managerRepository;
            _managerBusinessRules = managerBusinessRules;
        }

        public async Task<UpdatedManagerResponse> Handle(UpdateManagerCommand request, CancellationToken cancellationToken)
        {
            Manager? manager = await _managerRepository.GetAsync(predicate: m => m.Id == request.Id, cancellationToken: cancellationToken);
            await _managerBusinessRules.ManagerShouldExistWhenSelected(manager);
            manager = _mapper.Map(request, manager);
            //sinem
            manager.FirstName = CryptoHelper.Encrypt(manager.FirstName);
            manager.LastName = CryptoHelper.Encrypt(manager.LastName);
            manager.NationalIdentity = CryptoHelper.Encrypt(manager.NationalIdentity);
            manager.Phone = CryptoHelper.Encrypt(manager.Phone);
            manager.Address = CryptoHelper.Encrypt(manager.Address);
            manager.Email = CryptoHelper.Encrypt(manager.Email);


            await _managerRepository.UpdateAsync(manager!);

            UpdatedManagerResponse response = _mapper.Map<UpdatedManagerResponse>(manager);
            return response;
        }
    }
}