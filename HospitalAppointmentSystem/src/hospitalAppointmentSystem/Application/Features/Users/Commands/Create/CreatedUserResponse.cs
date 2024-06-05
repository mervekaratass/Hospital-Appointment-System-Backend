using NArchitecture.Core.Application.Responses;

namespace Application.Features.Users.Commands.Create;

public class CreatedUserResponse : IResponse
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

    

    public CreatedUserResponse()
    {
        FirstName = string.Empty;
        LastName = string.Empty;
        DateOfBirth = new DateOnly(1, 1, 1); // Varsayýlan olarak 1 Ocak 0001
        NationalIdentity = string.Empty;
        Phone = string.Empty;
        Address = string.Empty;
        Email = string.Empty;
        Password = string.Empty;
        Status = true;
    }

    public CreatedUserResponse(Guid ýd, string firstName, string lastName, DateOnly dateOfBirth, string nationalIdentity, string phone, string address, string email, string password, bool status)
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
       
    }
}
