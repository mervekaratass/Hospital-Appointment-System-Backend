using System.Text.RegularExpressions;
using FluentValidation;

namespace Application.Features.Auth.Commands.Register.UserRegister;

public class RegisterCommandValidator : AbstractValidator<RegisterCommand>
{
    public RegisterCommandValidator()
    {
        RuleFor(c => c.UserForRegisterDto.Email).NotEmpty().EmailAddress();
        RuleFor(c => c.UserForRegisterDto.Password)
            .NotEmpty()
            .MinimumLength(6)
            .Must(StrongPassword)
            .WithMessage(
                "Şifre en az bir büyük harf, bir küçük harf, bir rakam ve bir özel karakter içermelidir."
            );
        RuleFor(c => c.UserForRegisterDto.Email).EmailAddress().WithMessage("Geçerli bir email adresi giriniz.");

        RuleFor(c => c.UserForRegisterDto.FirstName).NotEmpty().WithMessage("İsim alanı boş geçilemez");

        RuleFor(c => c.UserForRegisterDto.LastName).NotEmpty().WithMessage("Soyisim alanı boş geçilemez");

        RuleFor(c=>c.UserForRegisterDto.Phone).NotEmpty().WithMessage("Telefon alanı boş geçilemez");

    }

    private bool StrongPassword(string value)
    {
        Regex strongPasswordRegex = new("^(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])(?=.*?[#?!@$%^&*-]).{8,}$", RegexOptions.Compiled);

        return strongPasswordRegex.IsMatch(value);
    }
}