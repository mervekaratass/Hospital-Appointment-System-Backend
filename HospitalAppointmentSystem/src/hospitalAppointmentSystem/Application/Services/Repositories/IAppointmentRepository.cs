using Domain.Entities;
using NArchitecture.Core.Persistence.Repositories;

namespace Application.Services.Repositories;

public interface IAppointmentRepository : IAsyncRepository<Appointment, int>, IRepository<Appointment, int>
{
}