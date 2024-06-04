using NArchitecture.Core.Application.Responses;

namespace Application.Features.Notifications.Commands.Update;

public class UpdatedNotificationResponse : IResponse
{
    public int Id { get; set; }
    public int AppointmentID { get; set; }
    public string Message { get; set; }
    public bool EmailStatus { get; set; }
    public bool SmsStatus { get; set; }
}