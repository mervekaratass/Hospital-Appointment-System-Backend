using Application.Features.Doctors.Constants;
using Application.Features.Doctors.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using NArchitecture.Core.Application.Pipelines.Authorization;
using NArchitecture.Core.Application.Pipelines.Caching;
using NArchitecture.Core.Application.Pipelines.Logging;
using NArchitecture.Core.Application.Pipelines.Transaction;
using MediatR;
using static Application.Features.Doctors.Constants.DoctorsOperationClaims;
using Application.Services.Encryptions;
using Application.Features.Patients.Rules;
using static Nest.JoinField;
using NArchitecture.Core.CrossCuttingConcerns.Exception.Types;

namespace Application.Features.Doctors.Commands.Update;

public class UpdateDoctorCommand : IRequest<UpdatedDoctorResponse>,  ILoggableRequest, ITransactionalRequest
{
    public Guid Id { get; set; }
    public required string Title { get; set; }
    public required string SchoolName { get; set; }
    public required int BranchID { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public DateOnly DateOfBirth { get; set; }
    public string NationalIdentity { get; set; }
    public string Phone { get; set; }
    public string Address { get; set; }
    public string Email { get; set; }

    public string[] Roles => [Admin, Write, DoctorsOperationClaims.Update];

    public bool BypassCache { get; }
    public string? CacheKey { get; }
    public string[]? CacheGroupKey => ["GetDoctors"];

    public class UpdateDoctorCommandHandler : IRequestHandler<UpdateDoctorCommand, UpdatedDoctorResponse>
    {
        private readonly IMapper _mapper;
        private readonly IDoctorRepository _doctorRepository;
        private readonly DoctorBusinessRules _doctorBusinessRules;

        public UpdateDoctorCommandHandler(IMapper mapper, IDoctorRepository doctorRepository,
                                         DoctorBusinessRules doctorBusinessRules)
        {
            _mapper = mapper;
            _doctorRepository = doctorRepository;
            _doctorBusinessRules = doctorBusinessRules;
        }

        public async Task<UpdatedDoctorResponse> Handle(UpdateDoctorCommand request, CancellationToken cancellationToken)
        {
            //MERNIS VALIDATION
            await _doctorBusinessRules.ValidateNationalIdentityAndBirthYearWithMernis(request.NationalIdentity, request.FirstName, request.LastName, request.DateOfBirth.Year);

            Doctor? doctor = await _doctorRepository.GetAsync(predicate: d => d.Id == request.Id, cancellationToken: cancellationToken);
            await _doctorBusinessRules.DoctorShouldExistWhenSelected(doctor);
            doctor = _mapper.Map(request, doctor);
            //sinem
            doctor.FirstName = CryptoHelper.Encrypt(doctor.FirstName);
            doctor.LastName = CryptoHelper.Encrypt(doctor.LastName);
            doctor.NationalIdentity = CryptoHelper.Encrypt(doctor.NationalIdentity);
            doctor.Phone = CryptoHelper.Encrypt(doctor.Phone);
            doctor.Address = CryptoHelper.Encrypt(doctor.Address);
            doctor.Email = CryptoHelper.Encrypt(doctor.Email);

            await _doctorBusinessRules.UserNationalIdentityShouldBeNotExists(request.Id, doctor.NationalIdentity);
            
            await _doctorRepository.UpdateAsync(doctor!);

            UpdatedDoctorResponse response = _mapper.Map<UpdatedDoctorResponse>(doctor);
            return response;
        }
    }
}