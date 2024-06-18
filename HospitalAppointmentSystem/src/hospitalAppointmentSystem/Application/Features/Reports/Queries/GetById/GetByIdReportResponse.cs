using NArchitecture.Core.Application.Responses;

namespace Application.Features.Reports.Queries.GetById;

public class GetByIdReportResponse : IResponse
{
    public int Id { get; set; }
    public int AppointmentID { get; set; }
    public string Text { get; set; }


    public Guid DoctorID { get; set; }
    public string DoctorFirstName { get; set; }
    public string DoctorLastName { get; set; }
    public string DoctorTitle { get; set; }
    public Guid PatientID { get; set; }
    public string PatientFirstName { get; set; }
    public string PatientLastName { get; set; }

    public string PatientIdentity { get; set; }

    public DateOnly AppointmentDate { get; set; }
    public TimeOnly AppointmentTime { get; set; }

    public DateTime ReportDate { get; set; }

}