using NArchitecture.Core.Application.Responses;

namespace Application.Features.Managers.Queries.GetById;

public class GetByIdManagerResponse : IResponse
{
    public Guid Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public DateOnly DateOfBirth { get; set; }
    public string NationalIdentity { get; set; }
    public string Phone { get; set; }
    public string Address { get; set; }
}