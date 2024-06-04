using NArchitecture.Core.Application.Dtos;

namespace Application.Features.DoctorSchedules.Queries.GetList;

public class GetListDoctorScheduleListItemDto : IDto
{
    public int Id { get; set; }
    public Guid DoctorID { get; set; }
    public DateOnly Date { get; set; }
    public TimeOnly StartTime { get; set; }
    public TimeOnly EndTime { get; set; }
}