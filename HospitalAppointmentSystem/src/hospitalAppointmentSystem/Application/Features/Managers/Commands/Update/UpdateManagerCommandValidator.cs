using FluentValidation;

namespace Application.Features.Managers.Commands.Update;

public class UpdateManagerCommandValidator : AbstractValidator<UpdateManagerCommand>
{
    public UpdateManagerCommandValidator()
    {
        RuleFor(c => c.Id).NotEmpty().WithMessage("Id alaný boþ olamaz");
    }
}