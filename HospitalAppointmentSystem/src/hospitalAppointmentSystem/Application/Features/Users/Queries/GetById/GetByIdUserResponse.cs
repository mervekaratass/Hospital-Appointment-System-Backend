using NArchitecture.Core.Application.Responses;

namespace Application.Features.Users.Queries.GetById;

public class GetByIdUserResponse : IResponse
{
    public Guid Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public DateOnly DateOfBirth { get; set; }
    public string NationalIdentity { get; set; }
    public string Phone { get; set; }
    public string Address { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public bool Status { get; set; }

    public GetByIdUserResponse()
    {
        FirstName = string.Empty;
        LastName = string.Empty;
        Email = string.Empty;

        DateOfBirth = new DateOnly(); // Varsayýlan tarih 0001-01-01 olacaktýr
        NationalIdentity = string.Empty;
        Phone = string.Empty;
        Address = string.Empty;
        Password = string.Empty;
    }

    public GetByIdUserResponse(Guid ýd, string firstName, string lastName, DateOnly dateOfBirth, string nationalIdentity, string phone, string address, string email, string password, bool status)
    {
        Id = ýd;
        FirstName = firstName;
        LastName = lastName;
        DateOfBirth = dateOfBirth;
        NationalIdentity = nationalIdentity;
        Phone = phone;
        Address = address;
        Email = email;
        Password = password;
        Status = status;
        // Diðer alanlar için varsayýlan deðerler burada atanabilir
        DateOfBirth = new DateOnly(); // Varsayýlan tarih 0001-01-01 olacaktýr
        NationalIdentity = string.Empty;
        Phone = string.Empty;
        Address = string.Empty;
        Password = string.Empty;
    }
}
