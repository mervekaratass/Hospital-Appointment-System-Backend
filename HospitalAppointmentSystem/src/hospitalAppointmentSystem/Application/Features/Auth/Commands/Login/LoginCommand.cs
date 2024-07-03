using Application.Features.Auth.Rules;
using Application.Services.AuthenticatorService;
using Application.Services.AuthService;
using Application.Services.Encryptions;
using Application.Services.UsersService;
using Domain.Entities;
using MediatR;
using NArchitecture.Core.Application.Dtos;
using NArchitecture.Core.CrossCuttingConcerns.Exception.Types;
using NArchitecture.Core.Security.Enums;
using NArchitecture.Core.Security.JWT;

namespace Application.Features.Auth.Commands.Login;

public class LoginCommand : IRequest<LoggedResponse>
{
    public UserForLoginDto UserForLoginDto { get; set; }
    public string IpAddress { get; set; }

    public LoginCommand()
    {
        UserForLoginDto = null!;
        IpAddress = string.Empty;
    }

    public LoginCommand(UserForLoginDto userForLoginDto, string ipAddress)
    {
        UserForLoginDto = userForLoginDto;
        IpAddress = ipAddress;
    }

    public class LoginCommandHandler : IRequestHandler<LoginCommand, LoggedResponse>
    {
        private readonly AuthBusinessRules _authBusinessRules;
        private readonly IAuthenticatorService _authenticatorService;
        private readonly IAuthService _authService;
        private readonly IUserService _userService;

        public LoginCommandHandler(
            IUserService userService,
            IAuthService authService,
            AuthBusinessRules authBusinessRules,
            IAuthenticatorService authenticatorService
        )
        {
            _userService = userService;
            _authService = authService;
            _authBusinessRules = authBusinessRules;
            _authenticatorService = authenticatorService;
        }

        public async Task<LoggedResponse> Handle(LoginCommand request, CancellationToken cancellationToken)
        {
            string emailToCheck;
            if(request.UserForLoginDto.Email == "fatmabireltr@gmail.com") 
            {
                emailToCheck = request.UserForLoginDto.Email;
            }
            else 
            {
                emailToCheck = CryptoHelper.Encrypt(request.UserForLoginDto.Email);
            }

            User? user = await _userService.GetAsync(
                predicate: u => u.Email == emailToCheck,
                cancellationToken: cancellationToken
            );

            if (user == null)
            {
                throw new BusinessException("Kullanıcı bulunamadı.");
            }

            await _authBusinessRules.UserShouldBeExistsWhenSelected(user);
            await _authBusinessRules.UserPasswordShouldBeMatch(user!, request.UserForLoginDto.Password);

            // Kullanıcı Doctor değilse e-posta doğrulaması yapılmış mı kontrol et
            if (user is not Doctor)
            {
                bool isEmailVerified = await _authenticatorService.IsEmailVerified(user!.Id);
                if (!isEmailVerified)
                {
                    throw new BusinessException("E-posta doğrulaması yapılmamış. Lütfen e-posta hesabınızı doğrulayın.");
                }
            }

            AccessToken createdAccessToken = await _authService.CreateAccessToken(user);

            Domain.Entities.RefreshToken createdRefreshToken = await _authService.CreateRefreshToken(user, request.IpAddress);
            Domain.Entities.RefreshToken addedRefreshToken = await _authService.AddRefreshToken(createdRefreshToken);
            await _authService.DeleteOldRefreshTokens(user.Id);

            LoggedResponse loggedResponse = new LoggedResponse
            {
                AccessToken = createdAccessToken,
                RefreshToken = addedRefreshToken
            };

            return loggedResponse;
        }
    }
}
