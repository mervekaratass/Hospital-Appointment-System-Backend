namespace Domain.Entities;

public class User : NArchitecture.Core.Security.Entities.User<Guid>
{
    public User()
    {
    }
    public User(Guid id, string firstName, string lastName, DateOnly dateOfBirth, string nationalIdentity, string phone, string address)
    {
        Id = id;
        FirstName = firstName;
        LastName = lastName;
        DateOfBirth = dateOfBirth;
        NationalIdentity = nationalIdentity;
        Phone = phone;
        Address = address;
    }



    public string FirstName { get; set; }
    public string LastName { get; set; }
    public DateOnly DateOfBirth { get; set; } 
    public string NationalIdentity { get; set; } 
    public string Phone { get; set; }
    public string Address { get; set; }



    public virtual ICollection<UserOperationClaim> UserOperationClaims { get; set; } = default!;
    public virtual ICollection<RefreshToken> RefreshTokens { get; set; } = default!;
    public virtual ICollection<OtpAuthenticator> OtpAuthenticators { get; set; } = default!;
    public virtual ICollection<EmailAuthenticator> EmailAuthenticators { get; set; } = default!;
    public virtual ICollection<Feedback> Feedbacks { get; set; } = new HashSet<Feedback>();

}
