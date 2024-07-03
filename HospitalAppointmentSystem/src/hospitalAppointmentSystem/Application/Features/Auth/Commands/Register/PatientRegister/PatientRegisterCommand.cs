using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;
using MediatR;
using Application.Features.Auth.Rules;
using Application.Services.AuthService;
using NArchitecture.Core.Security.Hashing;
using NArchitecture.Core.Security.JWT;
using Application.Services.Patients;
using Application.Services.UserOperationClaims;
using Application.Services.OperationClaims;
using Application.Services.Encryptions;
using NArchitecture.Core.Security.Entities;
using System.Numerics;


namespace Application.Features.Auth.Commands.Register.PatientRegister;
public class PatientRegisterCommand : IRequest<PatientRegisteredResponse>
{
    public PatientForRegisterDto PatientForRegisterDto { get; set; }
    public string IpAddress { get; set; }

    public PatientRegisterCommand()
    {
        PatientForRegisterDto = null!;
        IpAddress = string.Empty;
    }

    public PatientRegisterCommand(PatientForRegisterDto patientForRegisterDto, string ipAddress)
    {
        PatientForRegisterDto = patientForRegisterDto;
        IpAddress = ipAddress;
    }

    public class PatientRegisterCommandHandler : IRequestHandler<PatientRegisterCommand, PatientRegisteredResponse>
    {
        private readonly IPatientService _patientService;
        private readonly IAuthService _authService;
        private readonly IUserOperationClaimService _userOperationClaimService;
        private readonly IOperationClaimService _operationClaimService;
        private readonly AuthBusinessRules _authBusinessRules;

        public PatientRegisterCommandHandler(
            IPatientService patientService,
            IAuthService authService,
            AuthBusinessRules authBusinessRules,
            IUserOperationClaimService userOperationClaimService,
            IOperationClaimService operationClaimService
        )
        {
            _patientService = patientService;
            _authService = authService;
            _authBusinessRules = authBusinessRules;
            _userOperationClaimService = userOperationClaimService;
            _operationClaimService = operationClaimService;
        }

        public async Task<PatientRegisteredResponse> Handle(PatientRegisterCommand request, CancellationToken cancellationToken)
        {
            await _authBusinessRules.UserEmailShouldBeNotExists(request.PatientForRegisterDto.Email);

            HashingHelper.CreatePasswordHash(
                request.PatientForRegisterDto.Password,
                passwordHash: out byte[] passwordHash,
                passwordSalt: out byte[] passwordSalt
            );

            Patient newPatient =
                new()
                {
                   
                    FirstName = request.PatientForRegisterDto.FirstName,
                    LastName = request.PatientForRegisterDto.LastName,
                    Phone =request.PatientForRegisterDto.Phone,
                    Email = request.PatientForRegisterDto.Email,
                    PasswordHash = passwordHash,
                    PasswordSalt = passwordSalt,
                };

            //sinem
            newPatient.FirstName = CryptoHelper.Encrypt(newPatient.FirstName);
            newPatient.LastName = CryptoHelper.Encrypt(newPatient.LastName);
            newPatient.NationalIdentity = CryptoHelper.Encrypt(newPatient.NationalIdentity);
            newPatient.Phone = CryptoHelper.Encrypt(newPatient.Phone);
            newPatient.Address = CryptoHelper.Encrypt(newPatient.Address);
            newPatient.Email = CryptoHelper.Encrypt(newPatient.Email);



            Patient createdPatient = await _patientService.AddAsync(newPatient);

            ICollection<UserOperationClaim> userOperationClaims = [];

            var operationClaims = await _operationClaimService.GetListAsync(x => x.Name.Contains("Patients"));
            foreach (var item in operationClaims.Items)
            {
                userOperationClaims.Add(new UserOperationClaim() { UserId = createdPatient.Id, OperationClaimId = item.Id });
            }

            userOperationClaims = await _userOperationClaimService.AddRangeAsync(userOperationClaims);

            AccessToken createdAccessToken = await _authService.CreateAccessToken(createdPatient);

            Domain.Entities.RefreshToken createdRefreshToken = await _authService.CreateRefreshToken(
                createdPatient,
                request.IpAddress
            );

            Domain.Entities.RefreshToken addedRefreshToken = await _authService.AddRefreshToken(createdRefreshToken);

            PatientRegisteredResponse registeredResponse =
                new() { AccessToken = createdAccessToken, RefreshToken = addedRefreshToken };
            return registeredResponse;
        }
    }
}
