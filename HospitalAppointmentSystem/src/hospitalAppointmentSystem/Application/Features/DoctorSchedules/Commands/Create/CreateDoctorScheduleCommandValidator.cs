using FluentValidation;

namespace Application.Features.DoctorSchedules.Commands.Create;

public class CreateDoctorScheduleCommandValidator : AbstractValidator<CreateDoctorScheduleCommand>
{
    public CreateDoctorScheduleCommandValidator()
    {
        RuleFor(c => c.DoctorID).NotEmpty();
        RuleFor(c => c.Date).NotEmpty();
        RuleFor(c => c.StartTime).NotEmpty();
        RuleFor(c => c.EndTime).NotEmpty();
    }
}