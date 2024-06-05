using FluentValidation;

namespace Application.Features.Branches.Commands.Create;

public class CreateBranchCommandValidator : AbstractValidator<CreateBranchCommand>
{
    public CreateBranchCommandValidator()
    {

        RuleFor(c => c.Name).NotEmpty().WithMessage("Ýsim alaný boþ olamaz.");
        RuleFor(c => c.Name).MinimumLength(5).WithMessage("Ýsim alaný minimum 5 karakter olmalý.");

        RuleFor(c => c.Name)
            .NotEmpty().WithMessage("Ýsim alaný boþ olamaz.")
            .MinimumLength(5).WithMessage("Ýsim alaný minimum 5 karakter olmalýdýr.");

    }
}