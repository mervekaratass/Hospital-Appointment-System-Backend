using NArchitecture.Core.Application.Responses;

namespace Application.Features.Patients.Commands.Create;

public class CreatedPatientResponse : IResponse
{
    public Guid Id { get; set; }
    public int Age { get; set; }
    public double Height { get; set; }
    public double Weight { get; set; }
    public string BloodGroup { get; set; }


    public string FirstName { get; set; }
    public string LastName { get; set; }
    public DateOnly DateOfBirth { get; set; }
    public string NationalIdentity { get; set; }
    public string Phone { get; set; }
    public string Address { get; set; }
}