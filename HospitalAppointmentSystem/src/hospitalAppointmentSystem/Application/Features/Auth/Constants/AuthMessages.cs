namespace Application.Features.Auth.Constants;

public static class AuthMessages
{
    public const string SectionName = "Auth";

    public const string EmailAuthenticatorDontExists = "Böyle bir e-posta doðrulayýcý yok";
    public const string OtpAuthenticatorDontExists = "Böyle bir OTP doðrulayýcý yok";
    public const string AlreadyVerifiedOtpAuthenticatorIsExists = "Bu OTP doðrulayýcý zaten doðrulandý";
    public const string EmailActivationKeyDontExists = "Böyle bir e-posta aktivasyon anahtarý yok";
    public const string UserDontExists = "Böyle bir kullanýcý bulunmamaktadýr";
    public const string UserHaveAlreadyAAuthenticator = "Kullanýcýnýn zaten bir doðrulayýcýsý var";
    public const string RefreshDontExists = "Böyle bir yenileme yok";
    public const string InvalidRefreshToken = "Geçersiz yenileme belirteci";
    public const string UserMailAlreadyExists = "Böyle bir mail adresi zaten var";
    public const string PasswordDontMatch = "Þifreler eþleþmiyor";
    public static string EmailActivationKeyExpired = "Aktivasyon kodunun süresi 15 dakikadýr. Lütfen tekrar üye olun!";
}
