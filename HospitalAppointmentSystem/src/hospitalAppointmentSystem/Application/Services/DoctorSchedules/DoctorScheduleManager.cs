using Application.Features.DoctorSchedules.Rules;
using Application.Services.Repositories;
using NArchitecture.Core.Persistence.Paging;
using Domain.Entities;
using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;

namespace Application.Services.DoctorSchedules;

public class DoctorScheduleManager : IDoctorScheduleService
{
    private readonly IDoctorScheduleRepository _doctorScheduleRepository;
    private readonly DoctorScheduleBusinessRules _doctorScheduleBusinessRules;

    public DoctorScheduleManager(IDoctorScheduleRepository doctorScheduleRepository, DoctorScheduleBusinessRules doctorScheduleBusinessRules)
    {
        _doctorScheduleRepository = doctorScheduleRepository;
        _doctorScheduleBusinessRules = doctorScheduleBusinessRules;
    }

    public async Task<DoctorSchedule?> GetAsync(
        Expression<Func<DoctorSchedule, bool>> predicate,
        Func<IQueryable<DoctorSchedule>, IIncludableQueryable<DoctorSchedule, object>>? include = null,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    )
    {
        DoctorSchedule? doctorSchedule = await _doctorScheduleRepository.GetAsync(predicate, include, withDeleted, enableTracking, cancellationToken);
        return doctorSchedule;
    }

    public async Task<IPaginate<DoctorSchedule>?> GetListAsync(
        Expression<Func<DoctorSchedule, bool>>? predicate = null,
        Func<IQueryable<DoctorSchedule>, IOrderedQueryable<DoctorSchedule>>? orderBy = null,
        Func<IQueryable<DoctorSchedule>, IIncludableQueryable<DoctorSchedule, object>>? include = null,
        int index = 0,
        int size = 10,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    )
    {
        IPaginate<DoctorSchedule> doctorScheduleList = await _doctorScheduleRepository.GetListAsync(
            predicate,
            orderBy,
            include,
            index,
            size,
            withDeleted,
            enableTracking,
            cancellationToken
        );
        return doctorScheduleList;
    }

    public async Task<DoctorSchedule> AddAsync(DoctorSchedule doctorSchedule)
    {
        DoctorSchedule addedDoctorSchedule = await _doctorScheduleRepository.AddAsync(doctorSchedule);

        return addedDoctorSchedule;
    }

    public async Task<DoctorSchedule> UpdateAsync(DoctorSchedule doctorSchedule)
    {
        DoctorSchedule updatedDoctorSchedule = await _doctorScheduleRepository.UpdateAsync(doctorSchedule);

        return updatedDoctorSchedule;
    }

    public async Task<DoctorSchedule> DeleteAsync(DoctorSchedule doctorSchedule, bool permanent = false)
    {
        DoctorSchedule deletedDoctorSchedule = await _doctorScheduleRepository.DeleteAsync(doctorSchedule);

        return deletedDoctorSchedule;
    }
}
