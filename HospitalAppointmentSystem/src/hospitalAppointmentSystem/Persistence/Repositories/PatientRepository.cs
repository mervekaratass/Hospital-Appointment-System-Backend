using Application.Services.Repositories;
using Domain.Entities;
using NArchitecture.Core.Persistence.Repositories;
using Persistence.Contexts;

namespace Persistence.Repositories;

public class PatientRepository : EfRepositoryBase<Patient, Guid, BaseDbContext>, IPatientRepository
{
    public PatientRepository(BaseDbContext context) : base(context)
    {
    }
}