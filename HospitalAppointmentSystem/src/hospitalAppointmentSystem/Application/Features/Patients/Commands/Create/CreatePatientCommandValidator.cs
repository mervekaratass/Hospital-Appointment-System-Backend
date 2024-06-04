using FluentValidation;

namespace Application.Features.Patients.Commands.Create;

public class CreatePatientCommandValidator : AbstractValidator<CreatePatientCommand>
{
    public CreatePatientCommandValidator()
    {
        RuleFor(c => c.Age).NotEmpty();
        RuleFor(c => c.Height).NotEmpty();
        RuleFor(c => c.Weight).NotEmpty();
        RuleFor(c => c.BloodGroup).NotEmpty();
    }
}