using FluentValidation;

namespace Application.Features.Doctors.Commands.Create;

public class CreateDoctorCommandValidator : AbstractValidator<CreateDoctorCommand>
{
    public CreateDoctorCommandValidator()
    {
        RuleFor(c => c.Title).NotEmpty();
        RuleFor(c => c.SchoolName).NotEmpty();
        RuleFor(c => c.BranchID).NotEmpty();
    }
}