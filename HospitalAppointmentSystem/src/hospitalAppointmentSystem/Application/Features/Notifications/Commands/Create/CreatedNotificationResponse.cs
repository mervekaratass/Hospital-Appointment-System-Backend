using NArchitecture.Core.Application.Responses;

namespace Application.Features.Notifications.Commands.Create;

public class CreatedNotificationResponse : IResponse
{
    public int Id { get; set; }
    public int AppointmentID { get; set; }
    public string Message { get; set; }
    public bool EmailStatus { get; set; }
    public bool SmsStatus { get; set; }
}