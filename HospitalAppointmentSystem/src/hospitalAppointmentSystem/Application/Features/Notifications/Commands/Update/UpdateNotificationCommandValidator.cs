using FluentValidation;

namespace Application.Features.Notifications.Commands.Update;

public class UpdateNotificationCommandValidator : AbstractValidator<UpdateNotificationCommand>
{
    public UpdateNotificationCommandValidator()
    {
        RuleFor(c => c.Id).NotEmpty();
        RuleFor(c => c.AppointmentID).NotEmpty();
        RuleFor(c => c.Message).NotEmpty();
        RuleFor(c => c.EmailStatus).NotEmpty();
        RuleFor(c => c.SmsStatus).NotEmpty();
    }
}