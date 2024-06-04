using FluentValidation;

namespace Application.Features.Patients.Commands.Update;

public class UpdatePatientCommandValidator : AbstractValidator<UpdatePatientCommand>
{
    public UpdatePatientCommandValidator()
    {
        RuleFor(c => c.Id).NotEmpty();
        RuleFor(c => c.Age).NotEmpty();
        RuleFor(c => c.Height).NotEmpty();
        RuleFor(c => c.Weight).NotEmpty();
        RuleFor(c => c.BloodGroup).NotEmpty();
    }
}