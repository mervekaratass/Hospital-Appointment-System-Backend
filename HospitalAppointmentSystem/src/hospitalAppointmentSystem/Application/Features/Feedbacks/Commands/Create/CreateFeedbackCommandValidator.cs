using FluentValidation;

namespace Application.Features.Feedbacks.Commands.Create;

public class CreateFeedbackCommandValidator : AbstractValidator<CreateFeedbackCommand>
{
    public CreateFeedbackCommandValidator()
    {
        RuleFor(c => c.UserID).NotEmpty();
        RuleFor(c => c.Text).NotEmpty();
    }
}