using Application.Features.Patients.Constants;
using Application.Services.Repositories;
using NArchitecture.Core.Application.Rules;
using NArchitecture.Core.CrossCuttingConcerns.Exception.Types;
using NArchitecture.Core.Localization.Abstraction;
using Domain.Entities;
using NArchitecture.Core.Persistence.Paging;

namespace Application.Features.Patients.Rules;

public class PatientBusinessRules : BaseBusinessRules
{
    private readonly IPatientRepository _patientRepository;
    private readonly ILocalizationService _localizationService;

    public PatientBusinessRules(IPatientRepository patientRepository, ILocalizationService localizationService)
    {
        _patientRepository = patientRepository;
        _localizationService = localizationService;
    }

    private async Task throwBusinessException(string messageKey)
    {
        string message = await _localizationService.GetLocalizedAsync(messageKey, PatientsBusinessMessages.SectionName);
        throw new BusinessException(message);
    }

    public async Task PatientShouldExistWhenSelected(Patient? patient)
    {
        if (patient == null)
            await throwBusinessException(PatientsBusinessMessages.PatientNotExists);
    }

    public async Task PatientShouldExistWhenSelected(IPaginate<Patient> patients)
    {
        if (patients == null && patients.Count == 0)
            await throwBusinessException(PatientsBusinessMessages.PatientNotExists);
    }

    public async Task PatientIdShouldExistWhenSelected(Guid id, CancellationToken cancellationToken)
    {
        Patient? patient = await _patientRepository.GetAsync(
            predicate: p => p.Id == id,
            enableTracking: false,
            cancellationToken: cancellationToken
        );
        await PatientShouldExistWhenSelected(patient);
    }
}