using FluentValidation;

namespace Application.Features.Notifications.Commands.Update;

public class UpdateNotificationCommandValidator : AbstractValidator<UpdateNotificationCommand>
{
    public UpdateNotificationCommandValidator()
    {
        RuleFor(c => c.Id).NotEmpty().WithMessage("Id alaný boþ olamaz");

        RuleFor(c => c.AppointmentID)
            .NotEmpty().WithMessage("Randevu ID alaný boþ olamaz.");

        RuleFor(c => c.Message)
            .NotEmpty().WithMessage("Mesaj alaný boþ olamaz.");

        RuleFor(c => c.EmailStatus)
            .NotEmpty().WithMessage("E-posta durumu alaný boþ olamaz.");

        RuleFor(c => c.SmsStatus)
            .NotEmpty().WithMessage("SMS durumu alaný boþ olamaz.");
    }
}