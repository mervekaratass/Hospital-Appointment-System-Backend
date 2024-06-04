using Domain.Entities;
using NArchitecture.Core.Persistence.Repositories;

namespace Application.Services.Repositories;

public interface IPatientRepository : IAsyncRepository<Patient, Guid>, IRepository<Patient, Guid>
{
}