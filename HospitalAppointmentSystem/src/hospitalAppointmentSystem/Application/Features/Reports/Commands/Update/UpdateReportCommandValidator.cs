using FluentValidation;

namespace Application.Features.Reports.Commands.Update;

public class UpdateReportCommandValidator : AbstractValidator<UpdateReportCommand>
{
    public UpdateReportCommandValidator()
    {
        RuleFor(c => c.Id).NotEmpty().WithMessage("Id alaný boþ olamaz");
        //RuleFor(c => c.AppointmentID).NotEmpty().WithMessage("Randevu Id alaný boþ olamaz");
        RuleFor(c => c.Text)
            .NotEmpty().WithMessage("Metin boþ olamaz")
            .MaximumLength(500).WithMessage("Metin en fazla 500 karakter olmalýdýr");
    }
}