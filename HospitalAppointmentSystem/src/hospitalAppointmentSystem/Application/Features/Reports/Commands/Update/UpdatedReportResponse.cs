using NArchitecture.Core.Application.Responses;

namespace Application.Features.Reports.Commands.Update;

public class UpdatedReportResponse : IResponse
{
    public int Id { get; set; }
    public int AppointmentID { get; set; }
    public string Text { get; set; }
}