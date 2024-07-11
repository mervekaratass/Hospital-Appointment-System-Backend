namespace Application.Features.DoctorSchedules.Constants;

public static class DoctorSchedulesBusinessMessages
{
    public const string SectionName = "DoctorSchedule";


    public const string DoctorScheduleNotExists = "Böyle bir doktor takvimi bulunamadý";

    public const string DoctorScheduleCannotBeDeletedDueToExistingAppointments = "Bu takvim çizelgesi mevcut randevular nedeniyle silinemez.";

    public const string DoctorScheduleAlreadyExistsForThisDate = "Bu tarih için doktor takvim çizelgeniz zaten mevcut.";

    public const string DoctorScheduleIsSoftDeletedAndCannotBeUpdated = "Böyle bir doktor takvim çizelgesi bulunmamaktadýr.";

    public const string CheckIfAppointmentsExistOnDate = "Bu tarihe ait hastalar tarafýnda alýnmýþ randevular bulunmaktadýr.Tarihi güncelleyemezsiniz";
}





