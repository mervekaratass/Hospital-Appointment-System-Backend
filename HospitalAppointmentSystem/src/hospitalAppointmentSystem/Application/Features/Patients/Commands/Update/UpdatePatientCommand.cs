using Application.Features.Patients.Constants;
using Application.Features.Patients.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using NArchitecture.Core.Application.Pipelines.Authorization;
using NArchitecture.Core.Application.Pipelines.Caching;
using NArchitecture.Core.Application.Pipelines.Logging;
using NArchitecture.Core.Application.Pipelines.Transaction;
using MediatR;
using static Application.Features.Patients.Constants.PatientsOperationClaims;
using Application.Services.Encryptions;
using Application.Features.Auth.Rules;

namespace Application.Features.Patients.Commands.Update;

public class UpdatePatientCommand : IRequest<UpdatedPatientResponse>,  ILoggableRequest, ITransactionalRequest
{
    public Guid Id { get; set; }
    public required int Age { get; set; }
    public required double Height { get; set; }
    public required double Weight { get; set; }
    public required string BloodGroup { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public DateOnly DateOfBirth { get; set; }
    public string NationalIdentity { get; set; }
    public string Phone { get; set; }
    public string Address { get; set; }
    public string Email { get; set; }
    //public string Password { get; set; }

    public string[] Roles => [Admin, Write, PatientsOperationClaims.Update];

    public bool BypassCache { get; }
    public string? CacheKey { get; }
    public string[]? CacheGroupKey => ["GetPatients"];

    public class UpdatePatientCommandHandler : IRequestHandler<UpdatePatientCommand, UpdatedPatientResponse>
    {
        private readonly IMapper _mapper;
        private readonly IPatientRepository _patientRepository;
        private readonly PatientBusinessRules _patientBusinessRules;

        public UpdatePatientCommandHandler(IMapper mapper, IPatientRepository patientRepository,
                                         PatientBusinessRules patientBusinessRules)
        {
            _mapper = mapper;
            _patientRepository = patientRepository;
            _patientBusinessRules = patientBusinessRules;
        }

        public async Task<UpdatedPatientResponse> Handle(UpdatePatientCommand request, CancellationToken cancellationToken)
        {
            Patient? patient = await _patientRepository.GetAsync(predicate: p => p.Id == request.Id, cancellationToken: cancellationToken);
            await _patientBusinessRules.PatientShouldExistWhenSelected(patient);
            patient = _mapper.Map(request, patient);
            //sinem
            patient.FirstName = CryptoHelper.Encrypt(patient.FirstName);
            patient.LastName = CryptoHelper.Encrypt(patient.LastName);
            patient.NationalIdentity = CryptoHelper.Encrypt(patient.NationalIdentity);
            patient.Phone = CryptoHelper.Encrypt(patient.Phone);
            patient.Address = CryptoHelper.Encrypt(patient.Address);
            patient.Email = CryptoHelper.Encrypt(patient.Email);
            request.NationalIdentity = CryptoHelper.Encrypt(request.NationalIdentity);

            await _patientBusinessRules.UserNationalIdentityShouldBeNotExists(request.NationalIdentity,patient.NationalIdentity);
            await _patientRepository.UpdateAsync(patient!);

            UpdatedPatientResponse response = _mapper.Map<UpdatedPatientResponse>(patient);
            return response;
        }
    }
}