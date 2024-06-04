using NArchitecture.Core.Application.Dtos;

namespace Application.Features.Appointments.Queries.GetList;

public class GetListAppointmentListItemDto : IDto
{
    public int Id { get; set; }
    public DateOnly Date { get; set; }
    public TimeOnly Time { get; set; }
    public bool Status { get; set; }
    public Guid DoctorID { get; set; }
    public Guid PatientID { get; set; }
}