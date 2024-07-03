using Application.Features.Auth.Commands.Register.PatientRegister;
using Application.Features.Auth.Rules;
using Application.Services.AuthService;
using Application.Services.Doctors;
using Application.Services.Encryptions;
using Application.Services.OperationClaims;
using Application.Services.Patients;
using Application.Services.UserOperationClaims;
using Domain.Entities;
using MediatR;
using NArchitecture.Core.Application.Pipelines.Authorization;
using NArchitecture.Core.Application.Responses;
using NArchitecture.Core.Security.Hashing;
using NArchitecture.Core.Security.JWT;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Application.Features.Doctors.Constants.DoctorsOperationClaims;

namespace Application.Features.Auth.Commands.Register.DoctorRegister;

public class DoctorRegisterCommand : IRequest<DoctorRegisteredResponse>
{
    public DoctorForRegisterDto DoctorForRegisterDto { get; set; }
    public string IpAddress { get; set; }
    public string[] Roles => [Admin, Write];
    public DoctorRegisterCommand()
    {
        DoctorForRegisterDto = null!;
        IpAddress = string.Empty;
    }

    public DoctorRegisterCommand(DoctorForRegisterDto doctorForRegisterDto, string ipAddress)
    {
        DoctorForRegisterDto = doctorForRegisterDto;
        IpAddress = ipAddress;
    }

    public class DoctorRegisterCommandHandler : IRequestHandler<DoctorRegisterCommand, DoctorRegisteredResponse>
    {
        private readonly IDoctorService _doctorService;
        private readonly IAuthService _authService;
        private readonly IUserOperationClaimService _userOperationClaimService;
        private readonly IOperationClaimService _operationClaimService;
        private readonly AuthBusinessRules _authBusinessRules;

        public DoctorRegisterCommandHandler(
            IDoctorService doctorService,
            IAuthService authService,
            AuthBusinessRules authBusinessRules,
            IUserOperationClaimService userOperationClaimService,
            IOperationClaimService operationClaimService
        )
        {
            _doctorService = doctorService;
            _authService = authService;
            _authBusinessRules = authBusinessRules;
            _userOperationClaimService = userOperationClaimService;
            _operationClaimService = operationClaimService;
        }

        public async Task<DoctorRegisteredResponse> Handle(DoctorRegisterCommand request, CancellationToken cancellationToken)
        {
            await _authBusinessRules.UserEmailShouldBeNotExists(request.DoctorForRegisterDto.Email);

            HashingHelper.CreatePasswordHash(
                request.DoctorForRegisterDto.Password,
                passwordHash: out byte[] passwordHash,
                passwordSalt: out byte[] passwordSalt
            );

            Doctor newDoctor =
                new()
                {
                    
                    FirstName = request.DoctorForRegisterDto.FirstName,
                    LastName = request.DoctorForRegisterDto.LastName,
                    Phone = request.DoctorForRegisterDto.Phone,
                    Email = request.DoctorForRegisterDto.Email,
                    PasswordHash = passwordHash,
                    PasswordSalt = passwordSalt,
                    Title= request.DoctorForRegisterDto.Title,
                    SchoolName=request.DoctorForRegisterDto.SchoolName,
                    BranchID=request.DoctorForRegisterDto.BranchID,
                    DateOfBirth=request.DoctorForRegisterDto.DateOfBirth,
                    NationalIdentity=request.DoctorForRegisterDto.NationalIdentity,
                    Address=request.DoctorForRegisterDto.Address,

                };

         //sinem
            newDoctor.FirstName = CryptoHelper.Encrypt(newDoctor.FirstName);
            newDoctor.LastName = CryptoHelper.Encrypt(newDoctor.LastName);
            newDoctor.NationalIdentity = CryptoHelper.Encrypt(newDoctor.NationalIdentity);
            newDoctor.Phone = CryptoHelper.Encrypt(newDoctor.Phone);
            newDoctor.Address = CryptoHelper.Encrypt(newDoctor.Address);
            newDoctor.Email = CryptoHelper.Encrypt(newDoctor.Email);
            //burda bitti

            Doctor createdDoctor = await _doctorService.AddAsync(newDoctor);

            ICollection<UserOperationClaim> userOperationClaims = [];

            var operationClaims = await _operationClaimService.GetListAsync(x => x.Name.Contains("Doctors"));
            foreach (var item in operationClaims.Items)
            {
                userOperationClaims.Add(new UserOperationClaim() { UserId = createdDoctor.Id, OperationClaimId = item.Id });
            }

            userOperationClaims = await _userOperationClaimService.AddRangeAsync(userOperationClaims);

            AccessToken createdAccessToken = await _authService.CreateAccessToken(createdDoctor);

            Domain.Entities.RefreshToken createdRefreshToken = await _authService.CreateRefreshToken(
                createdDoctor,
                request.IpAddress
            );
            Domain.Entities.RefreshToken addedRefreshToken = await _authService.AddRefreshToken(createdRefreshToken);

            DoctorRegisteredResponse registeredResponse =
                new() { AccessToken = createdAccessToken, RefreshToken = addedRefreshToken };



            return registeredResponse;
        }
    }
}