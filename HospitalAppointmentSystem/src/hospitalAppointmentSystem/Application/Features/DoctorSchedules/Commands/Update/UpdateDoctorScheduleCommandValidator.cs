using FluentValidation;

namespace Application.Features.DoctorSchedules.Commands.Update;

public class UpdateDoctorScheduleCommandValidator : AbstractValidator<UpdateDoctorScheduleCommand>
{
    public UpdateDoctorScheduleCommandValidator()
    {
        RuleFor(c => c.Id).NotEmpty().WithMessage("Id alaný boþ olamaz");

        RuleFor(c => c.DoctorID).NotEmpty().WithMessage("Doktor Id alaný boþ olamaz");

        RuleFor(c => c.Date)
            .NotEmpty().WithMessage("Tarih alaný boþ olamaz.");

        RuleFor(c => c.StartTime)
            .NotEmpty().WithMessage("Baþlangýç saati alaný boþ olamaz.");

        RuleFor(c => c.EndTime)
            .NotEmpty().WithMessage("Bitiþ saati alaný boþ olamaz.");
    }
}