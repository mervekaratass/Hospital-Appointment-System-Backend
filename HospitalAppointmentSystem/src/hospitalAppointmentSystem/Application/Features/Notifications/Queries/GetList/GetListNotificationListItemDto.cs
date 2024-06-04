using NArchitecture.Core.Application.Dtos;

namespace Application.Features.Notifications.Queries.GetList;

public class GetListNotificationListItemDto : IDto
{
    public int Id { get; set; }
    public int AppointmentID { get; set; }

    public DateOnly AppointmentDate { get; set; }
    public TimeOnly AppointmentTime { get; set; }
    public string DoctorFirstName { get; set; }
    public string DoctorLastName { get; set; }

    public string PatientFirstName { get; set; }
    public string PatientLastName { get; set; }

    public string Message { get; set; }
    public bool EmailStatus { get; set; }
    public bool SmsStatus { get; set; }
}