using Application.Features.Managers.Constants;
using Application.Features.Managers.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using NArchitecture.Core.Application.Pipelines.Authorization;
using MediatR;
using static Application.Features.Managers.Constants.ManagersOperationClaims;
//using Application.Services.Encryptions;
using static Nest.JoinField;

namespace Application.Features.Managers.Queries.GetById;

public class GetByIdManagerQuery : IRequest<GetByIdManagerResponse>
{
    public Guid Id { get; set; }

    public string[] Roles => [Admin, Read];

    public class GetByIdManagerQueryHandler : IRequestHandler<GetByIdManagerQuery, GetByIdManagerResponse>
    {
        private readonly IMapper _mapper;
        private readonly IManagerRepository _managerRepository;
        private readonly ManagerBusinessRules _managerBusinessRules;

        public GetByIdManagerQueryHandler(IMapper mapper, IManagerRepository managerRepository, ManagerBusinessRules managerBusinessRules)
        {
            _mapper = mapper;
            _managerRepository = managerRepository;
            _managerBusinessRules = managerBusinessRules;
        }

        public async Task<GetByIdManagerResponse> Handle(GetByIdManagerQuery request, CancellationToken cancellationToken)
        {
            Manager? manager = await _managerRepository.GetAsync(predicate: m => m.Id == request.Id, cancellationToken: cancellationToken);
            await _managerBusinessRules.ManagerShouldExistWhenSelected(manager);

            //sinem encryptions þifrelenmiþ veriyi okuma. decrypt þifreyi çözer
            //manager.FirstName = CryptoHelper.Decrypt(manager.FirstName);
            //manager.LastName = CryptoHelper.Decrypt(manager.LastName);
            //manager.NationalIdentity = CryptoHelper.Decrypt(manager.NationalIdentity);
            //manager.Phone = CryptoHelper.Decrypt(manager.Phone);
            //manager.Address = CryptoHelper.Decrypt(manager.Address);

            // yazdýðým yer bitti

            GetByIdManagerResponse response = _mapper.Map<GetByIdManagerResponse>(manager);
            return response;
        }
    }
}