using NArchitecture.Core.Application.Responses;

namespace Application.Features.Appointments.Commands.Delete;

public class DeletedAppointmentResponse : IResponse
{
    public int Id { get; set; }
    public Guid DoctorID { get; set; }
    public Guid PatientID { get; set; }
    public string DoctorBranch { get; set; }
    public string DoctorFirstName { get; set; }
    public string DoctorLastName { get; set; }
    public string PatientFirstName { get; set; }
    public string PatientLastName { get; set; }
}