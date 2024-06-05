using FluentValidation;

namespace Application.Features.Appointments.Commands.Update;

public class UpdateAppointmentCommandValidator : AbstractValidator<UpdateAppointmentCommand>
{
    public UpdateAppointmentCommandValidator()
    {
        RuleFor(c => c.Id).NotEmpty().WithMessage("Id alaný boþ býrakýlamaz.");
        RuleFor(c => c.Date).NotEmpty().WithMessage("Tarih alaný boþ býrakýlamaz.");
        RuleFor(c => c.Time).NotEmpty().WithMessage("Saat alaný boþ býrakýlamaz.");
        RuleFor(c => c.Status).NotEmpty().WithMessage("Durum alaný boþ býrakýlamaz.");
        RuleFor(c => c.DoctorID).NotEmpty().WithMessage("Doktor Id alaný boþ býrakýlamaz.");
        RuleFor(c => c.PatientID).NotEmpty().WithMessage("Hasta Id alaný boþ býrakýlamaz.");

    }
}