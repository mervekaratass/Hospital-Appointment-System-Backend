using Domain.Entities;
using NArchitecture.Core.Application.Responses;

namespace Application.Features.Branches.Queries.GetByName;

public class GetByNameBranchResponse : IResponse
{
    public int Id { get; set; }
    public string Name { get; set; }
    public virtual ICollection<Doctor> Doctors { get; set; }
}