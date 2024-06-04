using Application.Services.Repositories;
using Domain.Entities;
using NArchitecture.Core.Persistence.Repositories;
using Persistence.Contexts;

namespace Persistence.Repositories;

public class DoctorScheduleRepository : EfRepositoryBase<DoctorSchedule, int, BaseDbContext>, IDoctorScheduleRepository
{
    public DoctorScheduleRepository(BaseDbContext context) : base(context)
    {
    }
}