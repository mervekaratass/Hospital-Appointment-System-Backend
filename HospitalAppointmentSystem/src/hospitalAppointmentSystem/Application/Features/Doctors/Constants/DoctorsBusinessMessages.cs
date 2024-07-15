namespace Application.Features.Doctors.Constants;

public static class DoctorsBusinessMessages
{
    public const string SectionName = "Doctor";

    public const string DoctorNotExists = "Böyle bir doktor bulunamadý";

    public const string UserIdentityAlreadyExists = "Böyle bir kimlik numarasý zaten var";

    public const string HasFutureAppointments = "Doktorun ileri tarihlerde randevularý bulunduðundan silinemez";

    public const string InvalidIdentity = "Geçersiz TC kimlik numarasý veya kimlik bilgileri";
}