using NArchitecture.Core.Application.Dtos;

namespace Application.Features.Doctors.Queries.GetList;

public class GetListDoctorListItemDto : IDto
{
    public Guid Id { get; set; }
    public string Title { get; set; }
    public string SchoolName { get; set; }   
    public int BranchID { get; set; }
    public string BranchName { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public DateOnly DateOfBirth { get; set; }
    public string NationalIdentity { get; set; }
    public string Phone { get; set; }
    public string Address { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
}