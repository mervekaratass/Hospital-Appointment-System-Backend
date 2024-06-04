using FluentValidation;

namespace Application.Features.Managers.Commands.Create;

public class CreateManagerCommandValidator : AbstractValidator<CreateManagerCommand>
{
    public CreateManagerCommandValidator()
    {
    }
}