using NArchitecture.Core.Application.Dtos;

namespace Application.Features.Reports.Queries.GetList;

public class GetListReportListItemDto : IDto
{
    public int Id { get; set; }
    public int AppointmentID { get; set; }
    public string Text { get; set; }
}