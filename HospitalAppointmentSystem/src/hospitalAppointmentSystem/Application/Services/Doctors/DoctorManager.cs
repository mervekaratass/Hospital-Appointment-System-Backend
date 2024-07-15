using Application.Features.Doctors.Rules;
using Application.Services.Repositories;
using NArchitecture.Core.Persistence.Paging;
using Domain.Entities;
using Microsoft.EntityFrameworkCore.Query;
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Services.Doctors
{
    public class DoctorManager : IDoctorService
    {
        private readonly IDoctorRepository _doctorRepository;

        public DoctorManager(IDoctorRepository doctorRepository)
        {
            _doctorRepository = doctorRepository;
        }

        public async Task<Doctor?> GetAsync(
            Expression<Func<Doctor, bool>> predicate,
            Func<IQueryable<Doctor>, IIncludableQueryable<Doctor, object>>? include = null,
            bool withDeleted = false,
            bool enableTracking = true,
            CancellationToken cancellationToken = default
        )
        {
            return await _doctorRepository.GetAsync(predicate, include, withDeleted, enableTracking, cancellationToken);
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
            return await _doctorRepository.GetListAsync(predicate, orderBy, include, index, size, withDeleted, enableTracking, cancellationToken);
        }

        public async Task<Doctor> AddAsync(Doctor doctor)
        {
            return await _doctorRepository.AddAsync(doctor);
        }

        public async Task<Doctor> UpdateAsync(Doctor doctor)
        {
            return await _doctorRepository.UpdateAsync(doctor);
        }

        public async Task<Doctor> DeleteAsync(Doctor doctor, bool permanent = false)
        {
            return await _doctorRepository.DeleteAsync(doctor, permanent);
        }

        public async Task<bool> AnyDoctorsInBranch(int branchId)
        {
            return await _doctorRepository.AnyAsync(d => d.BranchID == branchId);
        }

    }
}
