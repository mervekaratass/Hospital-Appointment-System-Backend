using Domain.Entities;
using NArchitecture.Core.Persistence.Repositories;

namespace Application.Services.Repositories;

public interface IDoctorScheduleRepository : IAsyncRepository<DoctorSchedule, int>, IRepository<DoctorSchedule, int>
{
}