using FluentValidation;

namespace Application.Features.Reports.Commands.Delete;

public class DeleteReportCommandValidator : AbstractValidator<DeleteReportCommand>
{
    public DeleteReportCommandValidator()
    {
        RuleFor(c => c.Id).NotEmpty();
    }
}