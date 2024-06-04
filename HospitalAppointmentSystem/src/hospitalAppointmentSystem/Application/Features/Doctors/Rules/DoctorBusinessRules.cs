using Application.Features.Doctors.Constants;
using Application.Services.Repositories;
using NArchitecture.Core.Application.Rules;
using NArchitecture.Core.CrossCuttingConcerns.Exception.Types;
using NArchitecture.Core.Localization.Abstraction;
using Domain.Entities;

namespace Application.Features.Doctors.Rules;

public class DoctorBusinessRules : BaseBusinessRules
{
    private readonly IDoctorRepository _doctorRepository;
    private readonly ILocalizationService _localizationService;

    public DoctorBusinessRules(IDoctorRepository doctorRepository, ILocalizationService localizationService)
    {
        _doctorRepository = doctorRepository;
        _localizationService = localizationService;
    }

    private async Task throwBusinessException(string messageKey)
    {
        string message = await _localizationService.GetLocalizedAsync(messageKey, DoctorsBusinessMessages.SectionName);
        throw new BusinessException(message);
    }

    public async Task DoctorShouldExistWhenSelected(Doctor? doctor)
    {
        if (doctor == null)
            await throwBusinessException(DoctorsBusinessMessages.DoctorNotExists);
    }

    public async Task DoctorIdShouldExistWhenSelected(Guid id, CancellationToken cancellationToken)
    {
        Doctor? doctor = await _doctorRepository.GetAsync(
            predicate: d => d.Id == id,
            enableTracking: false,
            cancellationToken: cancellationToken
        );
        await DoctorShouldExistWhenSelected(doctor);
    }
}