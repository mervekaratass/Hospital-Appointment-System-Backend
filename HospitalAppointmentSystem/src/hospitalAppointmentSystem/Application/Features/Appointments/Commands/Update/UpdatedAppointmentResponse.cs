using NArchitecture.Core.Application.Responses;

namespace Application.Features.Appointments.Commands.Update;

public class UpdatedAppointmentResponse : IResponse
{
    public int Id { get; set; }
    public DateOnly Date { get; set; }
    public TimeOnly Time { get; set; }
    public bool Status { get; set; }
    public Guid DoctorID { get; set; }
    public Guid PatientID { get; set; }
}