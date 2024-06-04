using FluentValidation;

namespace Application.Features.Doctors.Commands.Delete;

public class DeleteDoctorCommandValidator : AbstractValidator<DeleteDoctorCommand>
{
    public DeleteDoctorCommandValidator()
    {
        RuleFor(c => c.Id).NotEmpty();
    }
}