using FluentValidation;

namespace Application.Features.Reports.Commands.Update;

public class UpdateReportCommandValidator : AbstractValidator<UpdateReportCommand>
{
    public UpdateReportCommandValidator()
    {
        RuleFor(c => c.Id).NotEmpty();
        RuleFor(c => c.AppointmentID).NotEmpty();
        RuleFor(c => c.Text).NotEmpty();
    }
}