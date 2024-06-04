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
        // Diðer alanlar için varsayýlan deðerler burada atanabilir
        DateOfBirth = new DateOnly(); // Varsayýlan tarih 0001-01-01 olacaktýr
        NationalIdentity = string.Empty;
        Phone = string.Empty;
        Address = string.Empty;
        Password = string.Empty;
    }

    public GetByIdUserResponse(Guid id, string firstName, string lastName, string email, bool status)
    {
        Id = id;
        FirstName = firstName;
        LastName = lastName;
        Email = email;
        Status = status;
        // Diðer alanlar için varsayýlan deðerler burada atanabilir
        DateOfBirth = new DateOnly(); // Varsayýlan tarih 0001-01-01 olacaktýr
        NationalIdentity = string.Empty;
        Phone = string.Empty;
        Address = string.Empty;
        Password = string.Empty;
    }
}
