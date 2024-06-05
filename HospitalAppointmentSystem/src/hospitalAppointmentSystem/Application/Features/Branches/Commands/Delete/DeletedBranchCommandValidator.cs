using FluentValidation;

namespace Application.Features.Branches.Commands.Delete;

public class DeleteBranchCommandValidator : AbstractValidator<DeleteBranchCommand>
{
    public DeleteBranchCommandValidator()
    {
<<<<<<< Updated upstream
        RuleFor(c => c.Id).NotEmpty().WithMessage("Id boþ olamaz");
=======
        RuleFor(c => c.Id).NotEmpty().WithMessage("Id alaný boþ olamaz.");
>>>>>>> Stashed changes
    }
}