using NArchitecture.Core.Application.Responses;

namespace Application.Features.Doctors.Commands.Update;

public class UpdatedDoctorResponse : IResponse
{
    public Guid Id { get; set; }
    public required string Title { get; set; }
    public required string SchoolName { get; set; }
    public required int BranchID { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public DateOnly DateOfBirth { get; set; }
    public string NationalIdentity { get; set; }
    public string Phone { get; set; }
    public string Address { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
}