using FluentValidation;

namespace Application.Features.Notifications.Commands.Create;

public class CreateNotificationCommandValidator : AbstractValidator<CreateNotificationCommand>
{
    public CreateNotificationCommandValidator()
    {
        RuleFor(c => c.AppointmentID).NotEmpty();
        RuleFor(c => c.Message).NotEmpty();
        RuleFor(c => c.EmailStatus).NotEmpty();
        RuleFor(c => c.SmsStatus).NotEmpty();
    }
}