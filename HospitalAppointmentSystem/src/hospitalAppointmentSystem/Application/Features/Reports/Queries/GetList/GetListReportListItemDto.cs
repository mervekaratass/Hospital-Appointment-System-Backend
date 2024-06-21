using NArchitecture.Core.Application.Dtos;

namespace Application.Features.Reports.Queries.GetList;

public class GetListReportListItemDto : IDto
{
    public int Id { get; set; }
    public int AppointmentID { get; set; }

    public DateOnly AppointmentDate { get; set; }
    public TimeOnly AppointmentTime { get; set; }
    public Guid DoctorID { get; set; }
    public string DoctorTitle { get; set; }

    public string DoctorBranch { get; set; }
    public string DoctorFirstName { get; set; }
    public string DoctorLastName { get; set; }



    public Guid PatientID { get; set; }
    public string PatientFirstName { get; set; }
    public string PatientLastName { get; set; }
    public string PatientIdentity { get; set; }


    public string Text { get; set; }


    public DateTime ReportDate { get; set; }
}