using NArchitecture.Core.Application.Responses;

namespace Application.Features.Branches.Commands.Update;

public class UpdatedBranchResponse : IResponse
{
    public int Id { get; set; }
    public string Name { get; set; }
}