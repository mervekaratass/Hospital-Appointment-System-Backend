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
        RuleFor(c => c.DoctorForRegisterDto.Email).NotEmpty().WithMessage("E-posta alanı boş olamaz.");

     

        RuleFor(c => c.DoctorForRegisterDto.BranchID).NotEmpty().WithMessage("Branş alanı boş olamaz");

        RuleFor(c => c.DoctorForRegisterDto.Title)
            .NotEmpty().WithMessage("Uzmanlık alanı boş olamaz")
            .Length(2, 10).WithMessage("Uzmanlık alanı en az 2, en fazla 10 karakter olmalıdır.");


        RuleFor(c => c.DoctorForRegisterDto.SchoolName)
            .NotEmpty().WithMessage("Okul adı boş olamaz")
            .Length(3, 50).WithMessage("Okul adı en az 3, en fazla 50 karakter olabilir");

        RuleFor(c => c.DoctorForRegisterDto.FirstName).NotEmpty().WithMessage("Kullanıcı adı alanı boş olamaz")
            .MinimumLength(2).WithMessage("Kullanıcı adı en az 2 karakter olmalıdır");

        RuleFor(c => c.DoctorForRegisterDto.LastName).NotEmpty().WithMessage("Kullanıcı soyadı alanı boş olamaz")
            .MinimumLength(2).WithMessage("Kullanıcı soyadı en az 2 karakter olmalıdır");

        RuleFor(c => c.DoctorForRegisterDto.DateOfBirth).NotEmpty().WithMessage("Doğum tarihi alanı boş olamaz");

        RuleFor(c => c.DoctorForRegisterDto.NationalIdentity).NotEmpty().WithMessage("T.C. Kimlik numarası alanı boş olamaz").
            MinimumLength(11).WithMessage("T.C. Kimlik numarası minimum 11 karakter olmalıdır").MaximumLength(11).WithMessage("T.C. Kimlik numarası alanı maksimum 11 karakter olmalıdır");

        RuleFor(c => c.DoctorForRegisterDto.Email).EmailAddress().WithMessage("Girdiğiniz e-posta adresi istenen formatta değil!");

        RuleFor(c => c.DoctorForRegisterDto.Phone).NotEmpty().WithMessage("Telefon numarası alanı boş olamaz").MinimumLength(11).WithMessage("Telefon numarası minimum 11 karakter olmalıdır");

        RuleFor(c => c.DoctorForRegisterDto.Address).NotEmpty().WithMessage("Adres alanı boş olamaz").MinimumLength(3).WithMessage("Adres en az 3 karakter olmalıdır");

        RuleFor(c => c.DoctorForRegisterDto.Password).NotEmpty().WithMessage("Şifre alanı boş olamaz").MinimumLength(8).WithMessage("Şifre en az 8 karakter olmalı")
            .MaximumLength(15).WithMessage("Şifre en fazla 15 karakter olmalı").Must(StrongPassword).WithMessage(
                "Şifre en az bir büyük harf, bir küçük harf, bir rakam ve bir özel karakter içermelidir."
            );
    }

    private bool StrongPassword(string value)
    {
        Regex strongPasswordRegex = new("^(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])(?=.*?[#?!@$%^&*-]).{8,}$", RegexOptions.Compiled);

        return strongPasswordRegex.IsMatch(value);
    }
}
