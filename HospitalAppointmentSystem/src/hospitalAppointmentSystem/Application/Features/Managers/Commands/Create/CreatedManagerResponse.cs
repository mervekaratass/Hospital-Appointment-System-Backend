using NArchitecture.Core.Application.Responses;

namespace Application.Features.Managers.Commands.Create;

public class CreatedManagerResponse : IResponse
{
    public Guid Id { get; set; }

    public string FirstName { get; set; }
    public string LastName { get; set; }
    public DateOnly DateOfBirth { get; set; }
    public string NationalIdentity { get; set; }
    public string Phone { get; set; }
    public string Address { get; set; }
}