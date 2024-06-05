using System.Text.RegularExpressions;
using FluentValidation;

namespace Application.Features.Users.Commands.UpdateFromAuth;

public class UpdateUserFromAuthCommandValidator : AbstractValidator<UpdateUserFromAuthCommand>
{
    public UpdateUserFromAuthCommandValidator()
    {
        RuleFor(c => c.FirstName).NotEmpty().WithMessage("Kullanıcı adı alanı boş olamaz")
             .MinimumLength(2).WithMessage("Kullanıcı adı en az 2 karakter olmalıdır");

        RuleFor(c => c.LastName).NotEmpty().WithMessage("Kullanıcı soyadı alanı boş olamaz")
            .MinimumLength(2).WithMessage("Kullanıcı soyadı en az 2 karakter olmalıdır");

        RuleFor(c => c.Password).NotEmpty().WithMessage("Şifre alanı boş olamaz").MinimumLength(8).WithMessage("Şifre en az 8 karakter olmalı");

        RuleFor(c => c.Password).NotEmpty().WithMessage("Şifre alanı boş olamaz").MinimumLength(8).WithMessage("Şifre en az 8 karakter olmalı")
           .MaximumLength(15).WithMessage("Şifre en az 15 karakter olmalı").Must(StrongPassword).WithMessage(
               "Şifre en az bir büyük harf, bir küçük harf, bir rakam ve bir özel karakter içermelidir."
           );
    }

    private bool StrongPassword(string arg)
    {
        Regex regex = new("/^(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])(?=.*?[#?!@$%^&*-]).{8,}$/");
        return regex.IsMatch(arg);
    }
}
