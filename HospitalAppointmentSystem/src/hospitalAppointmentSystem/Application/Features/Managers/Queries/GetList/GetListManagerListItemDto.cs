using NArchitecture.Core.Application.Dtos;

namespace Application.Features.Managers.Queries.GetList;

public class GetListManagerListItemDto : IDto
{
    public Guid Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public DateOnly DateOfBirth { get; set; }
    public string NationalIdentity { get; set; }
    public string Phone { get; set; }
    public string Address { get; set; }
}