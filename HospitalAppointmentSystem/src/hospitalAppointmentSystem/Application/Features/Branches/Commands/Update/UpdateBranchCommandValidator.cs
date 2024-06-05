using FluentValidation;

namespace Application.Features.Branches.Commands.Update;

public class UpdateBranchCommandValidator : AbstractValidator<UpdateBranchCommand>
{
    public UpdateBranchCommandValidator()
    {
<<<<<<< Updated upstream
        RuleFor(c => c.Id).NotEmpty().WithMessage("Id deðeri boþ olamaz");
        RuleFor(c => c.Name).NotEmpty().WithMessage("Ýsim alaný boþ olamaz");
        RuleFor(c => c.Name).MinimumLength(5).WithMessage("Ýsim alaný minimum 5 karakter olmalý.");
=======
        RuleFor(c => c.Id).NotEmpty().WithMessage("Id alaný boþ olamaz.");
        RuleFor(c => c.Name)
            .NotEmpty().WithMessage("Ýsim alaný boþ olamaz.")
            .MinimumLength(5).WithMessage("Ýsim alaný minimum 5 karakter olmalýdýr.");
>>>>>>> Stashed changes
    }
}