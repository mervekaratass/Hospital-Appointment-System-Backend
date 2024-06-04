using NArchitecture.Core.Application.Responses;

namespace Application.Features.Doctors.Commands.Create;

public class CreatedDoctorResponse : IResponse
{
    public Guid Id { get; set; }
    public string Title { get; set; }
    public string SchoolName { get; set; }
    public int BranchID { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public DateOnly DateOfBirth { get; set; }
    public string NationalIdentity { get; set; }
    public string Phone { get; set; }
    public string Address { get; set; }
}