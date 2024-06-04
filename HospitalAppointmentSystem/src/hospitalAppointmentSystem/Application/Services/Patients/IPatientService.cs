using NArchitecture.Core.Persistence.Paging;
using Domain.Entities;
using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;

namespace Application.Services.Patients;

public interface IPatientService
{
    Task<Patient?> GetAsync(
        Expression<Func<Patient, bool>> predicate,
        Func<IQueryable<Patient>, IIncludableQueryable<Patient, object>>? include = null,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    );
    Task<IPaginate<Patient>?> GetListAsync(
        Expression<Func<Patient, bool>>? predicate = null,
        Func<IQueryable<Patient>, IOrderedQueryable<Patient>>? orderBy = null,
        Func<IQueryable<Patient>, IIncludableQueryable<Patient, object>>? include = null,
        int index = 0,
        int size = 10,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    );
    Task<Patient> AddAsync(Patient patient);
    Task<Patient> UpdateAsync(Patient patient);
    Task<Patient> DeleteAsync(Patient patient, bool permanent = false);
}
