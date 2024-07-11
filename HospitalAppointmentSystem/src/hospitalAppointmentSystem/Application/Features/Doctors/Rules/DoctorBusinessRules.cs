using Application.Features.Doctors.Constants;
using Application.Services.Repositories;
using NArchitecture.Core.Application.Rules;
using NArchitecture.Core.CrossCuttingConcerns.Exception.Types;
using NArchitecture.Core.Localization.Abstraction;
using Domain.Entities;
using Application.Features.Patients.Constants;
using Application.Services.Appointments;

namespace Application.Features.Doctors.Rules;

public class DoctorBusinessRules : BaseBusinessRules
{
    private readonly IDoctorRepository _doctorRepository;
    private readonly IAppointmentService _appointmentService;
    private readonly ILocalizationService _localizationService;

    public DoctorBusinessRules(IDoctorRepository doctorRepository, ILocalizationService localizationService,IAppointmentService appointmentService)
    {
        _appointmentService = appointmentService;
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

    public async Task UserNationalIdentityShouldBeNotExists(string identity)
    {
        bool doesExists = await _doctorRepository.AnyAsync(predicate: u => u.NationalIdentity == identity);
        if (doesExists)
            await throwBusinessException(DoctorsBusinessMessages.UserIdentityAlreadyExists);
    }

    public async Task HasFutureAppointments(Guid doctorId, DateOnly currentDate)
    {
        var hasFutureAppointments= await _appointmentService.HasFutureAppointments(doctorId,currentDate);

        if (hasFutureAppointments)
        {
            throw new BusinessException(DoctorsBusinessMessages.HasFutureAppointments);
        }

    }

}