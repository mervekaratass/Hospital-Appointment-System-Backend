using Application.Features.Patients.Rules;
using Application.Services.Repositories;
using NArchitecture.Core.Persistence.Paging;
using Domain.Entities;
using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;

namespace Application.Services.Patients;

public class PatientManager : IPatientService
{
    private readonly IPatientRepository _patientRepository;
    private readonly PatientBusinessRules _patientBusinessRules;

    public PatientManager(IPatientRepository patientRepository, PatientBusinessRules patientBusinessRules)
    {
        _patientRepository = patientRepository;
        _patientBusinessRules = patientBusinessRules;
    }

    public async Task<Patient?> GetAsync(
        Expression<Func<Patient, bool>> predicate,
        Func<IQueryable<Patient>, IIncludableQueryable<Patient, object>>? include = null,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    )
    {
        Patient? patient = await _patientRepository.GetAsync(predicate, include, withDeleted, enableTracking, cancellationToken);
        return patient;
    }

    public async Task<IPaginate<Patient>?> GetListAsync(
        Expression<Func<Patient, bool>>? predicate = null,
        Func<IQueryable<Patient>, IOrderedQueryable<Patient>>? orderBy = null,
        Func<IQueryable<Patient>, IIncludableQueryable<Patient, object>>? include = null,
        int index = 0,
        int size = 10,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    )
    {
        IPaginate<Patient> patientList = await _patientRepository.GetListAsync(
            predicate,
            orderBy,
            include,
            index,
            size,
            withDeleted,
            enableTracking,
            cancellationToken
        );
        return patientList;
    }

    public async Task<Patient> AddAsync(Patient patient)
    {
        Patient addedPatient = await _patientRepository.AddAsync(patient);

        return addedPatient;
    }

    public async Task<Patient> UpdateAsync(Patient patient)
    {
        Patient updatedPatient = await _patientRepository.UpdateAsync(patient);

        return updatedPatient;
    }

    public async Task<Patient> DeleteAsync(Patient patient, bool permanent = false)
    {
        Patient deletedPatient = await _patientRepository.DeleteAsync(patient);

        return deletedPatient;
    }
}
