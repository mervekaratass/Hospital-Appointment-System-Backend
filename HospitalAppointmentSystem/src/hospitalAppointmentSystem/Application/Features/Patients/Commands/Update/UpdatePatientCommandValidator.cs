using FluentValidation;
using System.Text.RegularExpressions;

namespace Application.Features.Patients.Commands.Update;

public class UpdatePatientCommandValidator : AbstractValidator<UpdatePatientCommand>
{
    public UpdatePatientCommandValidator()
    {
        RuleFor(c => c.Id).NotEmpty().WithMessage("Id alaný boþ olamaz");

        RuleFor(c => c.Age)
           .NotEmpty().WithMessage("Yaþ alaný boþ olamaz.");

        RuleFor(c => c.Height)
            .NotEmpty().WithMessage("Boy alaný boþ olamaz.");

        RuleFor(c => c.Weight)
            .NotEmpty().WithMessage("Kilo alaný boþ olamaz.");

        RuleFor(c => c.BloodGroup)
            .NotEmpty().WithMessage("Kan grubu alaný boþ olamaz.");

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

        //RuleFor(c => c.Password).NotEmpty().WithMessage("Þifre alaný boþ olamaz").MinimumLength(8).WithMessage("Þifre en az 8 karakter olmalý")
        //    .MaximumLength(15).WithMessage("Þifre en az 15 karakter olmalý").Must(StrongPassword).WithMessage(
        //        "Þifre en az bir büyük harf, bir küçük harf, bir rakam ve bir özel karakter içermelidir."
        //    );
    }
    private bool StrongPassword(string value)
    {
        Regex strongPasswordRegex = new("^(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])(?=.*?[#?!@$%^&.*-]).{8,}$", RegexOptions.Compiled);

        return strongPasswordRegex.IsMatch(value);
    }
}