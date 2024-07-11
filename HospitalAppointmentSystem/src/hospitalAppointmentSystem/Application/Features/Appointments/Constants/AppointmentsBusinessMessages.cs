namespace Application.Features.Appointments.Constants;

public static class AppointmentsBusinessMessages
{
    public const string SectionName = "Appointment";

    public const string AppointmentNotExists = "Böyle bir randevu bulunmamaktadýr";

    public const string PatientCannotHaveMultipleAppointmentsOnSameDayWithSameDoctor = "Bu doktor için ayný güne ait randevunuz zaten bulunmaktadýr.";
}