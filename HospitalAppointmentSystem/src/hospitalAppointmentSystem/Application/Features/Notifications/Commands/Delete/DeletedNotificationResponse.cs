using NArchitecture.Core.Application.Responses;

namespace Application.Features.Notifications.Commands.Delete;

public class DeletedNotificationResponse : IResponse
{
    public int Id { get; set; }
}