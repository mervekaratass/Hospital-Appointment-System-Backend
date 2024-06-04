using Application.Features.Auth.Commands.Register.PatientRegister;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Application.Features.Auth.Commands.Register.DoctorRegister;
public class DoctorRegisterCommandValidator : AbstractValidator<DoctorRegisterCommand>
{
    public DoctorRegisterCommandValidator()
    {
        RuleFor(c => c.DoctorForRegisterDto.Email).NotEmpty().EmailAddress();
        RuleFor(c => c.DoctorForRegisterDto.Password)
            .NotEmpty()
            .MinimumLength(6)
            .Must(StrongPassword)
            .WithMessage(
                "Şifre en az bir büyük harf, bir küçük harf, bir rakam ve bir özel karakter içermelidir."
            );

        RuleFor(c => c.DoctorForRegisterDto.FirstName).NotEmpty().WithMessage("İsim alanı boş geçilemez");

        RuleFor(c => c.DoctorForRegisterDto.LastName).NotEmpty().WithMessage("Soyisim alanı boş geçilemez");

        RuleFor(c => c.DoctorForRegisterDto.Phone).NotEmpty().WithMessage("Telefon alanı boş geçilemez");

    }

    private bool StrongPassword(string value)
    {
        Regex strongPasswordRegex = new("^(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])(?=.*?[#?!@$%^&*-]).{8,}$", RegexOptions.Compiled);

        return strongPasswordRegex.IsMatch(value);
    }
}
