using FluentValidation;

namespace Application.Features.Feedbacks.Commands.Delete;

public class DeleteFeedbackCommandValidator : AbstractValidator<DeleteFeedbackCommand>
{
    public DeleteFeedbackCommandValidator()
    {
        RuleFor(c => c.Id).NotEmpty();
    }
}