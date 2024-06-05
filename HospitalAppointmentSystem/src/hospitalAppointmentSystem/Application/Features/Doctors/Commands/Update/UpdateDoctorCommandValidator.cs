using FluentValidation;
using System.Text.RegularExpressions;

namespace Application.Features.Doctors.Commands.Update;

public class UpdateDoctorCommandValidator : AbstractValidator<UpdateDoctorCommand>
{
    public UpdateDoctorCommandValidator()
    {
        RuleFor(c => c.Id).NotEmpty().WithMessage("Id alaný boþ olamaz");

        RuleFor(c => c.BranchID).NotEmpty().WithMessage("Branþ alaný boþ olamaz");

        RuleFor(c => c.Title)
            .NotEmpty().WithMessage("Uzmanlýk alaný boþ olamaz")
            .Length(2, 10).WithMessage("Uzmanlýk alaný en az 2, en fazla 10 karakter olmalýdýr.");

        RuleFor(c => c.SchoolName)
             .NotEmpty().WithMessage("Okul adý boþ olamaz")
             .Length(3, 50).WithMessage("Okul adý en az 3, en fazla 50 karakter olabilir");

        RuleFor(c => c.FirstName).NotEmpty().WithMessage("Kullanýcý adý alaný boþ olamaz")
           .MinimumLength(2).WithMessage("Kullanýcý adý en az 2 karakter olmalýdýr");

        RuleFor(c => c.LastName).NotEmpty().WithMessage("Kullanýcý soyadý alaný boþ olamaz")
            .MinimumLength(2).WithMessage("Kullanýcý soyadý en az 2 karakter olmalýdýr");

        RuleFor(c => c.DateOfBirth).NotEmpty().WithMessage("Doðum tarihi alaný boþ olamaz");

        RuleFor(c => c.NationalIdentity).NotEmpty().WithMessage("T.C. Kimlik numarasý alaný boþ olamaz").
            MinimumLength(11).WithMessage("T.C. Kimlik numarasý minimum 11 karakter olmalýdýr").MaximumLength(11).WithMessage("T.C. Kimlik numarasý alaný maksimum 11 karakter olmalýdýr");

        RuleFor(c => c.Email).NotEmpty().WithMessage("E-posta alaný boþ olamaz").EmailAddress().WithMessage("Girdiðiniz e-posta adresi istenen formatta deðil!");

        RuleFor(c => c.Phone).NotEmpty().WithMessage("Telefon numarasý alaný boþ olamaz").MinimumLength(11).WithMessage("Telefon numarasý minimum 11 karakter olmalýdýr");

        RuleFor(c => c.Address).NotEmpty().WithMessage("Adres alaný boþ olamaz").MinimumLength(3).WithMessage("Adres en az 3 karakter olmalýdýr");

        RuleFor(c => c.Password).NotEmpty().WithMessage("Þifre alaný boþ olamaz").MinimumLength(8).WithMessage("Þifre en az 8 karakter olmalý")
            .MaximumLength(15).WithMessage("Þifre en az 15 karakter olmalý").Must(StrongPassword).WithMessage(
                "Þifre en az bir büyük harf, bir küçük harf, bir rakam ve bir özel karakter içermelidir."
            );

    }
    private bool StrongPassword(string value)
    {
        Regex strongPasswordRegex = new("^(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])(?=.*?[#?!@$%^&.*-]).{8,}$", RegexOptions.Compiled);

        return strongPasswordRegex.IsMatch(value);
    }
}