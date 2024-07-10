using Application.Features.Auth.Constants;
using Application.Services.Repositories;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using NArchitecture.Core.Application.Rules;
using NArchitecture.Core.CrossCuttingConcerns.Exception.Types;
using NArchitecture.Core.Localization.Abstraction;
using NArchitecture.Core.Security.Enums;
using NArchitecture.Core.Security.Hashing;

namespace Application.Features.Auth.Rules;

public class AuthBusinessRules : BaseBusinessRules
{
    private readonly IUserRepository _userRepository;
    private readonly IPatientRepository _patientRepository;
    private readonly ILocalizationService _localizationService;

    public AuthBusinessRules(IUserRepository userRepository,  ILocalizationService localizationService, IPatientRepository patientRepository)
    {
        _userRepository = userRepository;
        _localizationService = localizationService;
        _patientRepository = patientRepository;
    }

    private async Task throwBusinessException(string messageKey)
    {
        string message = await _localizationService.GetLocalizedAsync(messageKey, AuthMessages.SectionName);
        throw new BusinessException(message);
    }

    public async Task EmailAuthenticatorShouldBeExists(EmailAuthenticator? emailAuthenticator)
    {
        if (emailAuthenticator is null)
            await throwBusinessException(AuthMessages.EmailAuthenticatorDontExists);
    }

    public async Task OtpAuthenticatorShouldBeExists(OtpAuthenticator? otpAuthenticator)
    {
        if (otpAuthenticator is null)
            await throwBusinessException(AuthMessages.OtpAuthenticatorDontExists);
    }

    public async Task OtpAuthenticatorThatVerifiedShouldNotBeExists(OtpAuthenticator? otpAuthenticator)
    {
        if (otpAuthenticator is not null && otpAuthenticator.IsVerified)
            await throwBusinessException(AuthMessages.AlreadyVerifiedOtpAuthenticatorIsExists);
    }

    public async Task EmailAuthenticatorActivationKeyShouldBeExists(EmailAuthenticator emailAuthenticator)
    {
        if (emailAuthenticator.ActivationKey is null)
            await throwBusinessException(AuthMessages.EmailActivationKeyDontExists);
    }
    public async Task EmailAuthenticatorActivationKeyShouldNotBeExpired(EmailAuthenticator emailAuthenticator)
    {
        if (emailAuthenticator.CreatedDate.AddMinutes(15) < DateTime.UtcNow)
            await throwBusinessException(AuthMessages.EmailActivationKeyExpired);
    }

    public async Task UserShouldBeExistsWhenSelected(User? user)
    {
        if (user == null)
            await throwBusinessException(AuthMessages.UserDontExists);
    }

    public async Task UserShouldNotBeHaveAuthenticator(User user)
    {
        if (user.AuthenticatorType != AuthenticatorType.None)
            await throwBusinessException(AuthMessages.UserHaveAlreadyAAuthenticator);
    }

    public async Task RefreshTokenShouldBeExists(RefreshToken? refreshToken)
    {
        if (refreshToken == null)
            await throwBusinessException(AuthMessages.RefreshDontExists);
    }

    public async Task RefreshTokenShouldBeActive(RefreshToken refreshToken)
    {
        if (refreshToken.RevokedDate != null && DateTime.UtcNow >= refreshToken.ExpirationDate)
            await throwBusinessException(AuthMessages.InvalidRefreshToken);
    }

    public async Task UserEmailShouldBeNotExists(string email)
    {
        bool doesExists = await _userRepository.AnyAsync(predicate: u => u.Email == email);
        if (doesExists)
            await throwBusinessException(AuthMessages.UserMailAlreadyExists);
    }

    public async Task UserPasswordShouldBeMatch(User user, string password)
    {
        if (!HashingHelper.VerifyPasswordHash(password, user!.PasswordHash, user.PasswordSalt))
            await throwBusinessException(AuthMessages.PasswordDontMatch);
    }

    public async Task UserEmailShouldNotBeExpiredAuthenticator(string email)
    {
        // User ve EmailAuthenticators'ý yükle
        var user = await _userRepository.Query()
                                        .Include(u => u.EmailAuthenticators)
                                        .FirstOrDefaultAsync(u => u.Email == email && u.DeletedDate == null);

        if (user != null)
        {
            var emailAuthenticator = user.EmailAuthenticators?.FirstOrDefault();
            if (emailAuthenticator != null)
            {
                if (emailAuthenticator.CreatedDate.AddMinutes(15) < DateTime.UtcNow)
                {
                    await _userRepository.DeleteAuthenticatorCode(user.Id);
                    // Ýliþkili Patient kaydýný manuel olarak sorgulayýn
                    var patient = await _patientRepository.GetAsync(p => p.Id == user.Id);
                    if (patient != null)
                    {
                        // Soft delete iþlemi için patient ve iliþkili diðer tablolardan veriyi iþaretle
                        patient.DeletedDate = DateTime.UtcNow;
                        await _patientRepository.UpdateAsync(patient);                        
                    }

                    // Kullanýcýyý soft delete ile iþaretleyin
                    user.DeletedDate = DateTime.UtcNow;
                    await _userRepository.UpdateAsync(user);
                }
            }
        }
    }




}

