using NArchitecture.Core.Application.Responses;

namespace Application.Features.Reports.Commands.Create;

public class CreatedReportResponse : IResponse
{
    public int Id { get; set; }
    public int AppointmentID { get; set; }
    public string Text { get; set; }
}