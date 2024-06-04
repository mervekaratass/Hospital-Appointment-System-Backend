using NArchitecture.Core.Application.Responses;

namespace Application.Features.Branches.Queries.GetById;

public class GetByIdBranchResponse : IResponse
{
    public int Id { get; set; }
    public string Name { get; set; }
}