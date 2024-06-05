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
using static Nest.JoinField;

namespace Application.Features.Managers.Commands.Create;

public class CreateManagerCommand : IRequest<CreatedManagerResponse>, ISecuredRequest, ICacheRemoverRequest, ILoggableRequest, ITransactionalRequest
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public DateOnly DateOfBirth { get; set; }
    public string NationalIdentity { get; set; }
    public string Phone { get; set; }
    public string Address { get; set; }
    public string[] Roles => [Admin, Write, ManagersOperationClaims.Create];

    public bool BypassCache { get; }
    public string? CacheKey { get; }
    public string[]? CacheGroupKey => ["GetManagers"];

    public class CreateManagerCommandHandler : IRequestHandler<CreateManagerCommand, CreatedManagerResponse>
    {
        private readonly IMapper _mapper;
        private readonly IManagerRepository _managerRepository;
        private readonly ManagerBusinessRules _managerBusinessRules;

        public CreateManagerCommandHandler(IMapper mapper, IManagerRepository managerRepository,
                                         ManagerBusinessRules managerBusinessRules)
        {
            _mapper = mapper;
            _managerRepository = managerRepository;
            _managerBusinessRules = managerBusinessRules;
        }

        public async Task<CreatedManagerResponse> Handle(CreateManagerCommand request, CancellationToken cancellationToken)
        {
            Manager manager = _mapper.Map<Manager>(request);

            //sinem kullanýcý bilgilerini þifreleme. encrypt þifreleme yapýyor.

            manager.FirstName = CryptoHelper.Encrypt(manager.FirstName);
            manager.LastName = CryptoHelper.Encrypt(manager.LastName);
            manager.NationalIdentity = CryptoHelper.Encrypt(manager.NationalIdentity);
            manager.Phone = CryptoHelper.Encrypt(manager.Phone);
            manager.Address = CryptoHelper.Encrypt(manager.Address);

            //yazdýðým burda bitti

            await _managerRepository.AddAsync(manager);

            CreatedManagerResponse response = _mapper.Map<CreatedManagerResponse>(manager);
            return response;
        }
    }
}