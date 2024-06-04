using FluentValidation;

namespace Application.Features.Feedbacks.Commands.Update;

public class UpdateFeedbackCommandValidator : AbstractValidator<UpdateFeedbackCommand>
{
    public UpdateFeedbackCommandValidator()
    {
        RuleFor(c => c.Id).NotEmpty().WithMessage("Id alaný boþ olamaz");
        RuleFor(c => c.UserID).NotEmpty().WithMessage("User Id alaný boþ olamaz");
        RuleFor(c => c.Text)
         .NotEmpty().WithMessage("Metin alaný boþ olamaz.")
         .Length(5, 500).WithMessage("Metin en az 5, en fazla 500 karakter olmalýdýr.");
    }
}