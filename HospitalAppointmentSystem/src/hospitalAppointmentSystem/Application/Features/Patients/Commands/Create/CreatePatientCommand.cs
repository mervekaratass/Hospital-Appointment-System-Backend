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
using NArchitecture.Core.Security.Hashing;
using System.Numerics;
using Application.Services.Encryptions;
using System.Numerics;

namespace Application.Features.Patients.Commands.Create;

public class CreatePatientCommand : IRequest<CreatedPatientResponse>, ICacheRemoverRequest, ILoggableRequest, ITransactionalRequest, ISecuredRequest
{
    public int Age { get; set; }
    public double Height { get; set; }
    public double Weight { get; set; }
    public string BloodGroup { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public DateOnly DateOfBirth { get; set; }
    public string NationalIdentity { get; set; }
    public string Phone { get; set; }
    public string Address { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }

    public string[] Roles => [Admin, Write];

    public bool BypassCache { get; }
    public string? CacheKey { get; }
    public string[]? CacheGroupKey => ["GetPatients"];

    public class CreatePatientCommandHandler : IRequestHandler<CreatePatientCommand, CreatedPatientResponse>
    {
        private readonly IMapper _mapper;
        private readonly IPatientRepository _patientRepository;
        private readonly PatientBusinessRules _patientBusinessRules;

        public CreatePatientCommandHandler(IMapper mapper, IPatientRepository patientRepository,
                                         PatientBusinessRules patientBusinessRules)
        {
            _mapper = mapper;
            _patientRepository = patientRepository;
            _patientBusinessRules = patientBusinessRules;
        }

        public async Task<CreatedPatientResponse> Handle(CreatePatientCommand request, CancellationToken cancellationToken)
        {
            Patient patient = _mapper.Map<Patient>(request);

            HashingHelper.CreatePasswordHash(
               request.Password,
               passwordHash: out byte[] passwordHash,
               passwordSalt: out byte[] passwordSalt
           );
            patient.PasswordHash = passwordHash;
            patient.PasswordSalt = passwordSalt;

            //sinem kullanýcý bilgilerini þifreleme. encrypt þifreleme yapýyor.

            patient.FirstName = CryptoHelper.Encrypt(patient.FirstName);
            patient.LastName = CryptoHelper.Encrypt(patient.LastName);
            patient.NationalIdentity = CryptoHelper.Encrypt(patient.NationalIdentity);
            patient.Phone = CryptoHelper.Encrypt(patient.Phone);
            patient.Address = CryptoHelper.Encrypt(patient.Address);

            //yazdýðým burda bitti

            await _patientRepository.AddAsync(patient);

            CreatedPatientResponse response = _mapper.Map<CreatedPatientResponse>(patient);
            return response;
        }
    }
}