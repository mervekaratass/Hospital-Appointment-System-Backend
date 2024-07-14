using Domain.Entities;
using NArchitecture.Core.Persistence.Repositories;
using System.Threading.Tasks;

namespace Application.Services.Repositories
{
    public interface IUserRepository : IAsyncRepository<User, Guid>, IRepository<User, Guid>
    {
        Task DeleteAuthenticatorCode(Guid userId);
    }
}
