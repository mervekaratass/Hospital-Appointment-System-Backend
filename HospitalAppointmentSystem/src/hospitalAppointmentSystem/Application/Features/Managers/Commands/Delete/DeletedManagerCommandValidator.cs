using FluentValidation;

namespace Application.Features.Managers.Commands.Delete;

public class DeleteManagerCommandValidator : AbstractValidator<DeleteManagerCommand>
{
    public DeleteManagerCommandValidator()
    {
        RuleFor(c => c.Id).NotEmpty();
    }
}