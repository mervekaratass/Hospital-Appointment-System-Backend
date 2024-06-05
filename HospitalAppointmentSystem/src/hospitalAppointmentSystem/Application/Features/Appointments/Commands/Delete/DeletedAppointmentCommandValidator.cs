using FluentValidation;

namespace Application.Features.Appointments.Commands.Delete;

public class DeleteAppointmentCommandValidator : AbstractValidator<DeleteAppointmentCommand>
{
    public DeleteAppointmentCommandValidator()
    {
        RuleFor(c => c.Id).NotEmpty().WithMessage("Id alaný boþ býrakýlamaz");
    }
}