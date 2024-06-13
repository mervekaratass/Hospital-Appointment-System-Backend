using Application.Features.Auth.Commands.Register.UserRegister;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Application.Features.Auth.Commands.Register.PatientRegister;
public class PatientRegisterCommandValidator : AbstractValidator<PatientRegisterCommand>
{
    public PatientRegisterCommandValidator()
    {
        RuleFor(c => c.PatientForRegisterDto.Email).NotEmpty().WithMessage("E-posta alanı boş olamaz.");
        RuleFor(c => c.PatientForRegisterDto.Password)
            .NotEmpty()
            .MinimumLength(6)
            .Must(StrongPassword)
            .WithMessage(
                "Şifre en az bir büyük harf, bir küçük harf, bir rakam ve bir özel karakter içermelidir."
            );

        RuleFor(c => c.PatientForRegisterDto.Email).EmailAddress().WithMessage("Geçerli bir email adresi giriniz.");
         
        RuleFor(c => c.PatientForRegisterDto.FirstName).NotEmpty().WithMessage("İsim alanı boş geçilemez");

        RuleFor(c => c.PatientForRegisterDto.LastName).NotEmpty().WithMessage("Soyisim alanı boş geçilemez");

        RuleFor(c => c.PatientForRegisterDto.Phone).NotEmpty().WithMessage("Telefon alanı boş geçilemez");

    }

    private bool StrongPassword(string value)
    {
        Regex strongPasswordRegex = new("^(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])(?=.*?[#?!@$%^&*-]).{8,}$", RegexOptions.Compiled);

        return strongPasswordRegex.IsMatch(value);
    }

}

