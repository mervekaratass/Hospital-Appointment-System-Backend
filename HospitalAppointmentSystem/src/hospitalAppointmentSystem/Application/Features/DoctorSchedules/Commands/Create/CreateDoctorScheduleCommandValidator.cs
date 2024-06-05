using FluentValidation;

namespace Application.Features.DoctorSchedules.Commands.Create;

public class CreateDoctorScheduleCommandValidator : AbstractValidator<CreateDoctorScheduleCommand>
{
    public CreateDoctorScheduleCommandValidator()
    {
        RuleFor(c => c.DoctorID).NotEmpty().WithMessage("Id alaný boþ olamaz");

        RuleFor(c => c.Date)
            .NotEmpty().WithMessage("Tarih alaný boþ olamaz.");

        RuleFor(c => c.StartTime)
            .NotEmpty().WithMessage("Baþlangýç saati alaný boþ olamaz.");

        RuleFor(c => c.EndTime)
            .NotEmpty().WithMessage("Bitiþ saati alaný boþ olamaz.");
<<<<<<< Updated upstream

=======
>>>>>>> Stashed changes
    }
}