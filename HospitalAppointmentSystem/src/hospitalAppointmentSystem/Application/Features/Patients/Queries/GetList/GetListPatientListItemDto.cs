using NArchitecture.Core.Application.Dtos;

namespace Application.Features.Patients.Queries.GetList;

public class GetListPatientListItemDto : IDto
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
    public string Email  { get; set; } // sinem
    public string Address { get; set; }
    public string Password { get; set; } //sinem 

}