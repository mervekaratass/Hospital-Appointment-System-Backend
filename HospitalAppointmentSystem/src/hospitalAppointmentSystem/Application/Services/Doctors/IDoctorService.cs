using NArchitecture.Core.Persistence.Paging;
using Domain.Entities;
using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;

namespace Application.Services.Doctors;

public interface IDoctorService
{
    Task<Doctor?> GetAsync(
        Expression<Func<Doctor, bool>> predicate,
        Func<IQueryable<Doctor>, IIncludableQueryable<Doctor, object>>? include = null,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    );
    Task<IPaginate<Doctor>?> GetListAsync(
        Expression<Func<Doctor, bool>>? predicate = null,
        Func<IQueryable<Doctor>, IOrderedQueryable<Doctor>>? orderBy = null,
        Func<IQueryable<Doctor>, IIncludableQueryable<Doctor, object>>? include = null,
        int index = 0,
        int size = 10,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    );
    Task<Doctor> AddAsync(Doctor doctor);
    Task<Doctor> UpdateAsync(Doctor doctor);
    Task<Doctor> DeleteAsync(Doctor doctor, bool permanent = false);
    Task<bool> AnyDoctorsInBranch(int branchId);

}
