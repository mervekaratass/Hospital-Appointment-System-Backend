using FluentValidation;

namespace Application.Features.Appointments.Commands.Update;

public class UpdateAppointmentCommandValidator : AbstractValidator<UpdateAppointmentCommand>
{
    public UpdateAppointmentCommandValidator()
    {
        RuleFor(c => c.Id).NotEmpty();
        RuleFor(c => c.Date).NotEmpty();
        RuleFor(c => c.Time).NotEmpty();
        RuleFor(c => c.Status).NotEmpty();
        RuleFor(c => c.DoctorID).NotEmpty();
        RuleFor(c => c.PatientID).NotEmpty();
    }
}