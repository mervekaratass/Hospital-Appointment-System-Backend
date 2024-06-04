using FluentValidation;

namespace Application.Features.Reports.Commands.Create;

public class CreateReportCommandValidator : AbstractValidator<CreateReportCommand>
{
    public CreateReportCommandValidator()
    {
        RuleFor(c => c.AppointmentID).NotEmpty().WithMessage("Randevu Id alaný boþ olamaz");
        RuleFor(c => c.Text)
            .NotEmpty().WithMessage("Metin boþ olamaz")
            .MaximumLength(500).WithMessage("Metin en fazla 500 karakter olmalýdýr");

    }
}