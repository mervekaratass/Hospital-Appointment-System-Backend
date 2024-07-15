using TurkishCitizenIdValidator;
using Application.Features.Doctors.Constants;
using Application.Services.Repositories;
using NArchitecture.Core.Application.Rules;
using NArchitecture.Core.CrossCuttingConcerns.Exception.Types;
using NArchitecture.Core.Localization.Abstraction;
using Domain.Entities;
using Application.Services.Appointments;
using Application.Services.UsersService;

namespace Application.Features.Doctors.Rules
{
    public class DoctorBusinessRules : BaseBusinessRules
    {
        private readonly IDoctorRepository _doctorRepository;
        private readonly IUserService _userService;
        private readonly IAppointmentService _appointmentService;
        private readonly ILocalizationService _localizationService;

        public DoctorBusinessRules(IDoctorRepository doctorRepository, IUserService userService, ILocalizationService localizationService, IAppointmentService appointmentService)
        {
            _appointmentService = appointmentService;
            _doctorRepository = doctorRepository;
            _userService = userService;
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

        public async Task UserNationalIdentityShouldBeNotExists(Guid doctorId, string identity)
        {
            var doesExists = await _userService.UserNationalIdentityShouldBeNotExists(doctorId, identity);
            if (doesExists is not null) 
            {
                await throwBusinessException(DoctorsBusinessMessages.UserIdentityAlreadyExists);
            }            
        }

        public async Task HasFutureAppointments(Guid doctorId, DateOnly currentDate)
        {
            var hasFutureAppointments = await _appointmentService.HasFutureAppointments(doctorId, currentDate);

            if (hasFutureAppointments)
            {
               await throwBusinessException(DoctorsBusinessMessages.HasFutureAppointments);
            }
        }

        public async Task ValidateNationalIdentityAndBirthYearWithMernis(string nationalIdentity, string firstName, string lastName, int birthYear)
        {
            bool isValid = new TurkishCitizenIdentity(long.Parse(nationalIdentity), firstName, lastName, birthYear).IsValid();
            if (!isValid)
            {
                await throwBusinessException(DoctorsBusinessMessages.InvalidIdentity);
            }
        }
    }
}
