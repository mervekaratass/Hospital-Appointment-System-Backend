using NArchitecture.Core.Persistence.Paging;
using Domain.Entities;
using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;

namespace Application.Services.Appointments;

public interface IAppointmentService
{
    Task<Appointment?> GetAsync(
        Expression<Func<Appointment, bool>> predicate,
        Func<IQueryable<Appointment>, IIncludableQueryable<Appointment, object>>? include = null,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    );
    Task<IPaginate<Appointment>?> GetListAsync(
        Expression<Func<Appointment, bool>>? predicate = null,
        Func<IQueryable<Appointment>, IOrderedQueryable<Appointment>>? orderBy = null,
        Func<IQueryable<Appointment>, IIncludableQueryable<Appointment, object>>? include = null,
        int index = 0,
        int size = 10,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    );
    Task<Appointment> AddAsync(Appointment appointment);
    Task<Appointment> UpdateAsync(Appointment appointment);
    Task<Appointment> DeleteAsync(Appointment appointment, bool permanent = false);
}
