using Application.Features.Doctors.Rules;
using Application.Services.Repositories;
using NArchitecture.Core.Persistence.Paging;
using Domain.Entities;
using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;
using Application.Services.Encryptions;
using NArchitecture.Core.Security.Entities;

namespace Application.Services.Doctors;

public class DoctorManager : IDoctorService
{
    private readonly IDoctorRepository _doctorRepository;
    private readonly DoctorBusinessRules _doctorBusinessRules;

    public DoctorManager(IDoctorRepository doctorRepository, DoctorBusinessRules doctorBusinessRules)
    {
        _doctorRepository = doctorRepository;
        _doctorBusinessRules = doctorBusinessRules;
    }

    public async Task<Doctor?> GetAsync(
        Expression<Func<Doctor, bool>> predicate,
        Func<IQueryable<Doctor>, IIncludableQueryable<Doctor, object>>? include = null,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    )
    {
        Doctor? doctor = await _doctorRepository.GetAsync(predicate, include, withDeleted, enableTracking, cancellationToken);

        return doctor;
    }

    public async Task<IPaginate<Doctor>?> GetListAsync(
        Expression<Func<Doctor, bool>>? predicate = null,
        Func<IQueryable<Doctor>, IOrderedQueryable<Doctor>>? orderBy = null,
        Func<IQueryable<Doctor>, IIncludableQueryable<Doctor, object>>? include = null,
        int index = 0,
        int size = 10,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    )
    {
        IPaginate<Doctor> doctorList = await _doctorRepository.GetListAsync(
            predicate,
            orderBy,
            include,
            index,
            size,
            withDeleted,
            enableTracking,
            cancellationToken
        );
        return doctorList;
    }

    public async Task<Doctor> AddAsync(Doctor doctor)
    {
        

        Doctor addedDoctor = await _doctorRepository.AddAsync(doctor);

        


        return addedDoctor;
    }

    public async Task<Doctor> UpdateAsync(Doctor doctor)
    {
        Doctor updatedDoctor = await _doctorRepository.UpdateAsync(doctor);

        return updatedDoctor;
    }

    public async Task<Doctor> DeleteAsync(Doctor doctor, bool permanent = false)
    { 


        Doctor deletedDoctor = await _doctorRepository.DeleteAsync(doctor);

        return deletedDoctor;
    }
}
