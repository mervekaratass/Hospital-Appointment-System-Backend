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
using NArchitecture.Core.Security.Hashing;
using NArchitecture.Core.Security.Entities;
using Application.Services.Encryptions;

namespace Application.Features.Doctors.Commands.Create;


public class CreateDoctorCommand : IRequest<CreatedDoctorResponse>,  ILoggableRequest, ITransactionalRequest, ISecuredRequest
{
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
    public string Password { get; set; }

    public string[] Roles => [Admin, Write];

    public bool BypassCache { get; }
    public string? CacheKey { get; }
    public string[]? CacheGroupKey => ["GetDoctors"];

    public class CreateDoctorCommandHandler : IRequestHandler<CreateDoctorCommand, CreatedDoctorResponse>
    {
        private readonly IMapper _mapper;
        private readonly IDoctorRepository _doctorRepository;
        private readonly DoctorBusinessRules _doctorBusinessRules;

        public CreateDoctorCommandHandler(IMapper mapper, IDoctorRepository doctorRepository,
                                         DoctorBusinessRules doctorBusinessRules)
        {
            _mapper = mapper;
            _doctorRepository = doctorRepository;
            _doctorBusinessRules = doctorBusinessRules;
        }

        public async Task<CreatedDoctorResponse> Handle(CreateDoctorCommand request, CancellationToken cancellationToken)
        {
            Doctor doctor = _mapper.Map<Doctor>(request);

            HashingHelper.CreatePasswordHash(
                request.Password,
                passwordHash: out byte[] passwordHash,
                passwordSalt: out byte[] passwordSalt
            );
            doctor.PasswordHash = passwordHash;
            doctor.PasswordSalt = passwordSalt;

            //sinem kullanýcý bilgilerini þifreleme. encrypt þifreleme yapýyor.


            doctor.FirstName = CryptoHelper.Encrypt(doctor.FirstName);
            doctor.LastName = CryptoHelper.Encrypt(doctor.LastName);
            doctor.NationalIdentity = CryptoHelper.Encrypt(doctor.NationalIdentity);
            doctor.Phone = CryptoHelper.Encrypt(doctor.Phone);
            doctor.Address = CryptoHelper.Encrypt(doctor.Address);
            doctor.Email = CryptoHelper.Encrypt(doctor.Email);


            //yazdýðým burda bitti 

            await _doctorRepository.AddAsync(doctor);

            CreatedDoctorResponse response = _mapper.Map<CreatedDoctorResponse>(doctor);
            return response;
        }
    }
}