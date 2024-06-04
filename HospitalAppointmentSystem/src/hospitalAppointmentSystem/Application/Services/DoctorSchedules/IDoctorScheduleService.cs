using NArchitecture.Core.Persistence.Paging;
using Domain.Entities;
using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;

namespace Application.Services.DoctorSchedules;

public interface IDoctorScheduleService
{
    Task<DoctorSchedule?> GetAsync(
        Expression<Func<DoctorSchedule, bool>> predicate,
        Func<IQueryable<DoctorSchedule>, IIncludableQueryable<DoctorSchedule, object>>? include = null,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    );
    Task<IPaginate<DoctorSchedule>?> GetListAsync(
        Expression<Func<DoctorSchedule, bool>>? predicate = null,
        Func<IQueryable<DoctorSchedule>, IOrderedQueryable<DoctorSchedule>>? orderBy = null,
        Func<IQueryable<DoctorSchedule>, IIncludableQueryable<DoctorSchedule, object>>? include = null,
        int index = 0,
        int size = 10,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    );
    Task<DoctorSchedule> AddAsync(DoctorSchedule doctorSchedule);
    Task<DoctorSchedule> UpdateAsync(DoctorSchedule doctorSchedule);
    Task<DoctorSchedule> DeleteAsync(DoctorSchedule doctorSchedule, bool permanent = false);
}
