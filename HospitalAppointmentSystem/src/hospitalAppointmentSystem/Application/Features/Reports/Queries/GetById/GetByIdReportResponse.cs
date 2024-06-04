using NArchitecture.Core.Application.Responses;

namespace Application.Features.Reports.Queries.GetById;

public class GetByIdReportResponse : IResponse
{
    public int Id { get; set; }
    public int AppointmentID { get; set; }
    public string Text { get; set; }
}