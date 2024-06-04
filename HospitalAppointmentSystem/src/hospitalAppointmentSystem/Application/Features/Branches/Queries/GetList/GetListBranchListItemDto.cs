using NArchitecture.Core.Application.Dtos;

namespace Application.Features.Branches.Queries.GetList;

public class GetListBranchListItemDto : IDto
{
    public int Id { get; set; }
    public string Name { get; set; }
}