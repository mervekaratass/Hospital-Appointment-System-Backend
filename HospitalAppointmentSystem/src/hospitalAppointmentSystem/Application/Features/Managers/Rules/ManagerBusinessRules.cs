using Application.Features.Managers.Constants;
using Application.Services.Repositories;
using NArchitecture.Core.Application.Rules;
using NArchitecture.Core.CrossCuttingConcerns.Exception.Types;
using NArchitecture.Core.Localization.Abstraction;
using Domain.Entities;

namespace Application.Features.Managers.Rules;

public class ManagerBusinessRules : BaseBusinessRules
{
    private readonly IManagerRepository _managerRepository;
    private readonly ILocalizationService _localizationService;

    public ManagerBusinessRules(IManagerRepository managerRepository, ILocalizationService localizationService)
    {
        _managerRepository = managerRepository;
        _localizationService = localizationService;
    }

    private async Task throwBusinessException(string messageKey)
    {
        string message = await _localizationService.GetLocalizedAsync(messageKey, ManagersBusinessMessages.SectionName);
        throw new BusinessException(message);
    }

    public async Task ManagerShouldExistWhenSelected(Manager? manager)
    {
        if (manager == null)
            await throwBusinessException(ManagersBusinessMessages.ManagerNotExists);
    }

    public async Task ManagerIdShouldExistWhenSelected(Guid id, CancellationToken cancellationToken)
    {
        Manager? manager = await _managerRepository.GetAsync(
            predicate: m => m.Id == id,
            enableTracking: false,
            cancellationToken: cancellationToken
        );
        await ManagerShouldExistWhenSelected(manager);
    }
}