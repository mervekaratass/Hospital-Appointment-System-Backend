using FluentValidation;

namespace Application.Features.Appointments.Commands.Create;

public class CreateAppointmentCommandValidator : AbstractValidator<CreateAppointmentCommand>
{
    public CreateAppointmentCommandValidator()
    {
        RuleFor(c => c.Date).NotEmpty();
        RuleFor(c => c.Time).NotEmpty();
        RuleFor(c => c.Status).NotEmpty();
        RuleFor(c => c.DoctorID).NotEmpty();
        RuleFor(c => c.PatientID).NotEmpty();
    }
}