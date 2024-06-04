using NArchitecture.Core.Application.Responses;

namespace Application.Features.DoctorSchedules.Queries.GetById;

public class GetByIdDoctorScheduleResponse : IResponse
{
    public int Id { get; set; }
    public Guid DoctorID { get; set; }
    public DateOnly Date { get; set; }
    public TimeOnly StartTime { get; set; }
    public TimeOnly EndTime { get; set; }
}