using FluentValidation;

namespace Application.Features.Auth.Commands.Login;

public class LoginCommandValidator : AbstractValidator<LoginCommand>
{
    public LoginCommandValidator()
    {
        RuleFor(c => c.UserForLoginDto.Email)
            .NotEmpty().WithMessage("E-posta alanı boş olamaz.")
            .EmailAddress().WithMessage("Geçerli bir e-posta adresi giriniz.");

        RuleFor(c => c.UserForLoginDto.Password)
            .NotEmpty().WithMessage("Şifre alanı boş olamaz.")
            .MinimumLength(8).WithMessage("Şifre en az 8 karakter uzunluğunda olmalıdır.")
            .MaximumLength(15).WithMessage("Şifre en fazla 15 karakter uzunluğunda olmalıdır.");
    }
}
