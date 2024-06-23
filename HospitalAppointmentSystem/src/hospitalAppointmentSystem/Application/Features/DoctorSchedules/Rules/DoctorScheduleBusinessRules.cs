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
    private readonly IAppointmentRepository _appointmentRepository;
    private readonly ILocalizationService _localizationService;

    public DoctorScheduleBusinessRules(IDoctorScheduleRepository doctorScheduleRepository, IAppointmentRepository appointmentRepository, ILocalizationService localizationService)
    {
        _appointmentRepository = appointmentRepository;
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

    //o randevu seçilmiþse hastalrdan silmesine izin vermemek için oluþturulan bir kural
    public async Task DoctorScheduleShouldNotBeDeletedIfAppointmentsExist(int doctorScheduleId, CancellationToken cancellationToken)
    {
        DoctorSchedule? doctorSchedule = await _doctorScheduleRepository.GetAsync(
            predicate: ds => ds.Id == doctorScheduleId,
            enableTracking: false,
            cancellationToken: cancellationToken
        );

        if (doctorSchedule == null)
            await throwBusinessException(DoctorSchedulesBusinessMessages.DoctorScheduleNotExists);

        bool hasAppointments = await _appointmentRepository.AnyAsync(
            predicate: a => a.DoctorID == doctorSchedule.DoctorID && a.Date == doctorSchedule.Date,
            cancellationToken: cancellationToken
        );

        if (hasAppointments)
            await throwBusinessException(DoctorSchedulesBusinessMessages.DoctorScheduleCannotBeDeletedDueToExistingAppointments);
    }


    public async Task CheckIfDoctorScheduleDateIsUniqueForDoctor(Guid doctorId, DateOnly date)
    {
        var existingSchedule = await _doctorScheduleRepository.GetAsync(ds => ds.DoctorID == doctorId && ds.Date == date && ds.DeletedDate == null);

        if (existingSchedule != null)
        {
            throw new BusinessException(DoctorSchedulesBusinessMessages.DoctorScheduleAlreadyExistsForThisDate);
        }
    }

    public async Task CheckIfDoctorScheduleDateIsUniqueForDoctorOnUpdate(int doctorScheduleId, Guid doctorId, DateOnly date, CancellationToken cancellationToken)
    {
        var existingSchedule = await _doctorScheduleRepository.GetAsync(
            predicate: ds => ds.DoctorID == doctorId && ds.Date == date && ds.Id != doctorScheduleId && ds.DeletedDate == null,
            enableTracking: false,
            cancellationToken: cancellationToken
        );

        if (existingSchedule != null)
        {
            throw new BusinessException(DoctorSchedulesBusinessMessages.DoctorScheduleAlreadyExistsForThisDate);
        }
    }

    public async Task<DoctorSchedule?> CheckAndRetrieveSoftDeletedSchedule(Guid doctorId, DateOnly date)
    {
        return await _doctorScheduleRepository.GetAsync(ds => ds.DoctorID == doctorId && ds.Date == date && ds.DeletedDate != null);
    }

    //güncelleme iþlemi için soft delete
    public async Task DoctorScheduleShouldNotBeUpdatedIfSoftDeleted(int doctorScheduleId, CancellationToken cancellationToken)
    {
        var existingSchedule = await _doctorScheduleRepository.GetAsync(
            predicate: ds => ds.Id == doctorScheduleId,
            enableTracking: false,
            cancellationToken: cancellationToken
        );

        if (existingSchedule == null)
        {
            throw new BusinessException(DoctorSchedulesBusinessMessages.DoctorScheduleNotExists);
        }

        if (existingSchedule.DeletedDate != null)
        {
            throw new BusinessException(DoctorSchedulesBusinessMessages.DoctorScheduleIsSoftDeletedAndCannotBeUpdated);
        }
    }



}