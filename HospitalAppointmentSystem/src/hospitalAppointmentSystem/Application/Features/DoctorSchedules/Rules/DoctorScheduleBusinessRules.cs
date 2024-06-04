using Application.Features.DoctorSchedules.Constants;
using Application.Services.Repositories;
using NArchitecture.Core.Application.Rules;
using NArchitecture.Core.CrossCuttingConcerns.Exception.Types;
using NArchitecture.Core.Localization.Abstraction;
using Domain.Entities;

namespace Application.Features.DoctorSchedules.Rules;

public class DoctorScheduleBusinessRules : BaseBusinessRules
{
    private readonly IDoctorScheduleRepository _doctorScheduleRepository;
    private readonly ILocalizationService _localizationService;

    public DoctorScheduleBusinessRules(IDoctorScheduleRepository doctorScheduleRepository, ILocalizationService localizationService)
    {
        _doctorScheduleRepository = doctorScheduleRepository;
        _localizationService = localizationService;
    }

    private async Task throwBusinessException(string messageKey)
    {
        string message = await _localizationService.GetLocalizedAsync(messageKey, DoctorSchedulesBusinessMessages.SectionName);
        throw new BusinessException(message);
    }

    public async Task DoctorScheduleShouldExistWhenSelected(DoctorSchedule? doctorSchedule)
    {
        if (doctorSchedule == null)
            await throwBusinessException(DoctorSchedulesBusinessMessages.DoctorScheduleNotExists);
    }

    public async Task DoctorScheduleIdShouldExistWhenSelected(int id, CancellationToken cancellationToken)
    {
        DoctorSchedule? doctorSchedule = await _doctorScheduleRepository.GetAsync(
            predicate: ds => ds.Id == id,
            enableTracking: false,
            cancellationToken: cancellationToken
        );
        await DoctorScheduleShouldExistWhenSelected(doctorSchedule);
    }
}