using Domain.Entities;
using NArchitecture.Core.Persistence.Repositories;

namespace Application.Services.Repositories;

public interface IManagerRepository : IAsyncRepository<Manager, Guid>, IRepository<Manager, Guid>
{
}