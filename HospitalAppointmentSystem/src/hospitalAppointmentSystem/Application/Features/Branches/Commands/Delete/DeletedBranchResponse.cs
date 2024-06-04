using NArchitecture.Core.Application.Responses;

namespace Application.Features.Branches.Commands.Delete;

public class DeletedBranchResponse : IResponse
{
    public int Id { get; set; }
}