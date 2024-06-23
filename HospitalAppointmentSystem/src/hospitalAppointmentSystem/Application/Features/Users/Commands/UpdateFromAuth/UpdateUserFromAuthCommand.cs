using Application.Features.Users.Rules;
using Application.Services.AuthService;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using MediatR;
using NArchitecture.Core.Security.Hashing;

namespace Application.Features.Users.Commands.UpdateFromAuth;

public class UpdateUserFromAuthCommand : IRequest<UpdatedUserFromAuthResponse>
{
    public Guid Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public DateOnly DateOfBirth { get; set; }
    public string NationalIdentity { get; set; }
    public string Phone { get; set; }
    public string Address { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public string? NewPassword { get; set; }

    public UpdateUserFromAuthCommand()
    {
        FirstName = string.Empty;
        LastName = string.Empty;
        DateOfBirth = new DateOnly(1, 1, 1);
        NationalIdentity = string.Empty;
        Phone = string.Empty;
        Address = string.Empty;
        Email = string.Empty;
        Password = string.Empty;
    }

    public UpdateUserFromAuthCommand(Guid id, string firstName, string lastName, DateOnly dateOfBirth, string nationalIdentity, string phone, string address, string email, string password, string? newPassword)
    {
        Id = id;
        FirstName = firstName;
        LastName = lastName;
        DateOfBirth = dateOfBirth;
        NationalIdentity = nationalIdentity;
        Phone = phone;
        Address = address;
        Email = email;
        Password = password;
        NewPassword = newPassword;
    }

    public class UpdateUserFromAuthCommandHandler : IRequestHandler<UpdateUserFromAuthCommand, UpdatedUserFromAuthResponse>
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        private readonly UserBusinessRules _userBusinessRules;
        private readonly IAuthService _authService;

        public UpdateUserFromAuthCommandHandler(
            IUserRepository userRepository,
            IMapper mapper,
            UserBusinessRules userBusinessRules,
            IAuthService authService
        )
        {
            _userRepository = userRepository;
            _mapper = mapper;
            _userBusinessRules = userBusinessRules;
            _authService = authService;
        }

        public async Task<UpdatedUserFromAuthResponse> Handle(
            UpdateUserFromAuthCommand request,
            CancellationToken cancellationToken
        )
        {
            User? user = await _userRepository.GetAsync(
                predicate: u => u.Id.Equals(request.Id),
                cancellationToken: cancellationToken
            );

            if (user == null)
            {
                throw new Exception("Kullanıcı bulunamadı.");
            }

            await _userBusinessRules.UserShouldBeExistsWhenSelected(user);
            await _userBusinessRules.UserPasswordShouldBeMatched(user, request.Password);
            await _userBusinessRules.UserEmailShouldNotExistsWhenUpdate(user.Id, user.Email);

            // Mapping request to user entity
            _mapper.Map(request, user);

            // Check if new password is provided and update the password accordingly
            if (!string.IsNullOrWhiteSpace(request.NewPassword))
            {
                HashingHelper.CreatePasswordHash(
                    request.NewPassword,
                    passwordHash: out byte[] passwordHash,
                    passwordSalt: out byte[] passwordSalt
                );
                user.PasswordHash = passwordHash;
                user.PasswordSalt = passwordSalt;
            }

            // Update user entity
            User updatedUser = await _userRepository.UpdateAsync(user);

            // Mapping updated user to response object
            UpdatedUserFromAuthResponse response = _mapper.Map<UpdatedUserFromAuthResponse>(updatedUser);
            response.AccessToken = await _authService.CreateAccessToken(updatedUser);

            return response;
        }
    }
}
