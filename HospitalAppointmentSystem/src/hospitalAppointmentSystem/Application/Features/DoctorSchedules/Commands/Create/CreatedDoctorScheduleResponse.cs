using NArchitecture.Core.Application.Responses;

namespace Application.Features.DoctorSchedules.Commands.Create;

public class CreatedDoctorScheduleResponse : IResponse
{
    public int Id { get; set; }
    public Guid DoctorID { get; set; }
    public DateOnly Date { get; set; }
    public TimeOnly StartTime { get; set; }
    public TimeOnly EndTime { get; set; }
}