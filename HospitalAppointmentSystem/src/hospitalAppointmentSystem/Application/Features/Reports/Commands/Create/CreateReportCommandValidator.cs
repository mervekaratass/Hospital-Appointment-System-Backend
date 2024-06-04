using FluentValidation;

namespace Application.Features.Reports.Commands.Create;

public class CreateReportCommandValidator : AbstractValidator<CreateReportCommand>
{
    public CreateReportCommandValidator()
    {
        RuleFor(c => c.AppointmentID).NotEmpty();
        RuleFor(c => c.Text).NotEmpty();
    }
}