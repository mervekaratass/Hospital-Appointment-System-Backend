using NArchitecture.Core.Application.Dtos;

namespace Application.Features.Reports.Queries.GetList;

public class GetListReportListItemDto : IDto
{
    public int Id { get; set; }
    public int AppointmentID { get; set; }

    public string DoctorFirstName { get; set; }
    public string DoctorLastName { get; set; }



    public string PatientFirstName { get; set; }
    public string PatientLastName { get; set; }


    public string Text { get; set; }
}