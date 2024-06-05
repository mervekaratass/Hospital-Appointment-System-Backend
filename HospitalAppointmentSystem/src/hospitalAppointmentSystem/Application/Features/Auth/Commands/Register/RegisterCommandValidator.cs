using System.Text.RegularExpressions;
using FluentValidation;

namespace Application.Features.Auth.Commands.Register;

public class RegisterCommandValidator : AbstractValidator<RegisterCommand>
{
    public RegisterCommandValidator()
    {
        RuleFor(c => c.UserForRegisterDto.Email).NotEmpty().WithMessage("E-posta alanı boş olamaz.").EmailAddress().WithMessage("Geçerli bir e-posta adresi giriniz.");
        RuleFor(c => c.UserForRegisterDto.Password)
            .NotEmpty()
            .MinimumLength(8)
            .MaximumLength(15)
            .Must(StrongPassword)
            .WithMessage(
                "Şifre en az bir büyük harf, bir küçük harf, bir rakam ve bir özel karakter içermelidir."
            );
    }

    private bool StrongPassword(string value)
    {
        Regex strongPasswordRegex = new("^(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])(?=.*?[#?!@$%^&*-]).{8,}$", RegexOptions.Compiled);

        return strongPasswordRegex.IsMatch(value);
    }
}
