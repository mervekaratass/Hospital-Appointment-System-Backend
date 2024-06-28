using NArchitecture.Core.Application.Responses;

namespace Application.Features.Appointments.Commands.Create;

public class CreatedAppointmentResponse : IResponse
{
    public int Id { get; set; }
    public DateOnly Date { get; set; }
    public TimeOnly Time { get; set; }
    public bool Status { get; set; }
    public Guid DoctorID { get; set; }
    public Guid PatientID { get; set; }
    public string DoctorBranch { get; set; }
    public string DoctorFirstName { get; set; }
    public string DoctorLastName { get; set; }
    public string PatientFirstName { get; set; }
    public string PatientLastName { get; set; }
}