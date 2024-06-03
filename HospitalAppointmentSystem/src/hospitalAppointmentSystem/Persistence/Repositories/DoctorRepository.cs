using Application.Services.Repositories;
using Domain.Entities;
using NArchitecture.Core.Persistence.Repositories;
using Persistence.Contexts;

namespace Persistence.Repositories;

public class DoctorRepository : EfRepositoryBase<Doctor, Guid, BaseDbContext>, IDoctorRepository
{
    public DoctorRepository(BaseDbContext context) : base(context)
    {
    }
}