using Application.Features.Branches.Commands.Create;
using Application.Features.Branches.Commands.Delete;
using Application.Features.Branches.Commands.Update;
using Application.Features.Branches.Queries.GetById;
using Application.Features.Branches.Queries.GetList;
using AutoMapper;
using NArchitecture.Core.Application.Responses;
using Domain.Entities;
using NArchitecture.Core.Persistence.Paging;

namespace Application.Features.Branches.Profiles;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        CreateMap<CreateBranchCommand, Branch>();
        CreateMap<Branch, CreatedBranchResponse>();

        CreateMap<UpdateBranchCommand, Branch>();
        CreateMap<Branch, UpdatedBranchResponse>();

        CreateMap<DeleteBranchCommand, Branch>();
        CreateMap<Branch, DeletedBranchResponse>();

        CreateMap<Branch, GetByIdBranchResponse>();

        CreateMap<Branch, GetListBranchListItemDto>();
        CreateMap<IPaginate<Branch>, GetListResponse<GetListBranchListItemDto>>();
    }
}