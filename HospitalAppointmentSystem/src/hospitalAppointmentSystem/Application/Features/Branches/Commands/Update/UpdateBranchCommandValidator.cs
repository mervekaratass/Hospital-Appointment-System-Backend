using FluentValidation;

namespace Application.Features.Branches.Commands.Update;

public class UpdateBranchCommandValidator : AbstractValidator<UpdateBranchCommand>
{
    public UpdateBranchCommandValidator()
    {
        RuleFor(c => c.Id).NotEmpty().WithMessage("Id deðeri boþ olamaz");
        RuleFor(c => c.Name).NotEmpty().WithMessage("Ýsim alaný boþ olamaz");
        RuleFor(c => c.Name).MinimumLength(5).WithMessage("Ýsim alaný minimum 5 karakter olmalý.");
    }
}