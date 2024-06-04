using Application.Features.Managers.Rules;
using Application.Services.Repositories;
using NArchitecture.Core.Persistence.Paging;
using Domain.Entities;
using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;

namespace Application.Services.Managers;

public class ManagerManager : IManagerService
{
    private readonly IManagerRepository _managerRepository;
    private readonly ManagerBusinessRules _managerBusinessRules;

    public ManagerManager(IManagerRepository managerRepository, ManagerBusinessRules managerBusinessRules)
    {
        _managerRepository = managerRepository;
        _managerBusinessRules = managerBusinessRules;
    }

    public async Task<Manager?> GetAsync(
        Expression<Func<Manager, bool>> predicate,
        Func<IQueryable<Manager>, IIncludableQueryable<Manager, object>>? include = null,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    )
    {
        Manager? manager = await _managerRepository.GetAsync(predicate, include, withDeleted, enableTracking, cancellationToken);
        return manager;
    }

    public async Task<IPaginate<Manager>?> GetListAsync(
        Expression<Func<Manager, bool>>? predicate = null,
        Func<IQueryable<Manager>, IOrderedQueryable<Manager>>? orderBy = null,
        Func<IQueryable<Manager>, IIncludableQueryable<Manager, object>>? include = null,
        int index = 0,
        int size = 10,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    )
    {
        IPaginate<Manager> managerList = await _managerRepository.GetListAsync(
            predicate,
            orderBy,
            include,
            index,
            size,
            withDeleted,
            enableTracking,
            cancellationToken
        );
        return managerList;
    }

    public async Task<Manager> AddAsync(Manager manager)
    {
        Manager addedManager = await _managerRepository.AddAsync(manager);

        return addedManager;
    }

    public async Task<Manager> UpdateAsync(Manager manager)
    {
        Manager updatedManager = await _managerRepository.UpdateAsync(manager);

        return updatedManager;
    }

    public async Task<Manager> DeleteAsync(Manager manager, bool permanent = false)
    {
        Manager deletedManager = await _managerRepository.DeleteAsync(manager);

        return deletedManager;
    }
}
