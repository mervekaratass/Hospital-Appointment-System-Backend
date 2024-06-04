using NArchitecture.Core.Application.Responses;

namespace Application.Features.Branches.Commands.Create;

public class CreatedBranchResponse : IResponse
{
    public int Id { get; set; }
    public string Name { get; set; }
}