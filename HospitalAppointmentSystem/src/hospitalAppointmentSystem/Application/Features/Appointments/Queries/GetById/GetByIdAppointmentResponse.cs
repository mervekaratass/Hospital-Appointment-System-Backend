using NArchitecture.Core.Application.Responses;

namespace Application.Features.Appointments.Queries.GetById;

public class GetByIdAppointmentResponse : IResponse
{
    public int Id { get; set; }
    public DateOnly Date { get; set; }
    public TimeOnly Time { get; set; }
    public bool Status { get; set; }


    public Guid DoctorID { get; set; }
    public string DoctorFirstName { get; set; }
    public string DoctorLastName { get; set; }
    public string DoctorTitle { get; set; }



    public Guid PatientID { get; set; }
    public string PatientFirstName { get; set; } //burda gerekli konfigürasyonu yap
    public string PatientLastName { get; set; }
}