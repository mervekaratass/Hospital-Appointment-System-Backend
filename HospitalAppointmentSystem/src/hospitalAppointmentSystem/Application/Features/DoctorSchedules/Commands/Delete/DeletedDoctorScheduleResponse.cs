using NArchitecture.Core.Application.Responses;

namespace Application.Features.DoctorSchedules.Commands.Delete;

public class DeletedDoctorScheduleResponse : IResponse
{
    public int Id { get; set; }
}