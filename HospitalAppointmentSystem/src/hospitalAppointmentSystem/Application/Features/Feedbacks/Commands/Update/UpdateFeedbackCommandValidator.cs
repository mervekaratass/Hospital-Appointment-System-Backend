using FluentValidation;

namespace Application.Features.Feedbacks.Commands.Update;

public class UpdateFeedbackCommandValidator : AbstractValidator<UpdateFeedbackCommand>
{
    public UpdateFeedbackCommandValidator()
    {
        RuleFor(c => c.Id).NotEmpty();
        RuleFor(c => c.UserID).NotEmpty();
        RuleFor(c => c.Text).NotEmpty();
    }
}