using FluentValidation;
using System.Text.RegularExpressions;

namespace Application.Features.Users.Commands.Create;

public class CreateUserCommandValidator : AbstractValidator<CreateUserCommand>
{
    public CreateUserCommandValidator()
    {
        RuleFor(c => c.FirstName).NotEmpty().WithMessage("Kullanıcı adı alanı boş olamaz")
            .MinimumLength(2).WithMessage("Kullanıcı adı en az 2 karakter olmalıdır");

        RuleFor(c => c.LastName).NotEmpty().WithMessage("Kullanıcı soyadı alanı boş olamaz")
            .MinimumLength(2).WithMessage("Kullanıcı soyadı en az 2 karakter olmalıdır");

        RuleFor(c => c.DateOfBirth).NotEmpty().WithMessage("Doğum tarihi alanı boş olamaz");

        RuleFor(c => c.NationalIdentity).NotEmpty().WithMessage("T.C. Kimlik numarası alanı boş olamaz").
            MinimumLength(11).WithMessage("T.C. Kimlik numarası minimum 11 karakter olmalıdır").MaximumLength(11).WithMessage("T.C. Kimlik numarası alanı maksimum 11 karakter olmalıdır");

        RuleFor(c => c.Email).NotEmpty().WithMessage("E-posta alanı boş olamaz").EmailAddress().WithMessage("Girdiğiniz e-posta adresi istenen formatta değil!");

        RuleFor(c => c.Phone).NotEmpty().WithMessage("Telefon numarası alanı boş olamaz").MinimumLength(11).WithMessage("Telefon numarası minimum 11 karakter olmalıdır");

        RuleFor(c => c.Address).NotEmpty().WithMessage("Adres alanı boş olamaz").MinimumLength(3).WithMessage("Adres en az 3 karakter olmalıdır");

        RuleFor(c => c.Password).NotEmpty().WithMessage("Şifre alanı boş olamaz").MinimumLength(8).WithMessage("Şifre en az 8 karakter olmalı")
            .MaximumLength(15).WithMessage("Şifre en az 15 karakter olmalı").Must(StrongPassword).WithMessage(
                "Şifre en az bir büyük harf, bir küçük harf, bir rakam ve bir özel karakter içermelidir.");

    }
    private bool StrongPassword(string value)
    {
        Regex strongPasswordRegex = new("^(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])(?=.*?[#?!@$%^&.*-]).{8,}$", RegexOptions.Compiled);

        return strongPasswordRegex.IsMatch(value);
    }
}
