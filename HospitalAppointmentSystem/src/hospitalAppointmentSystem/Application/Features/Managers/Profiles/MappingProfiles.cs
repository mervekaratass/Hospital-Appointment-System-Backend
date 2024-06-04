using Application.Features.Managers.Commands.Create;
using Application.Features.Managers.Commands.Delete;
using Application.Features.Managers.Commands.Update;
using Application.Features.Managers.Queries.GetById;
using Application.Features.Managers.Queries.GetList;
using AutoMapper;
using NArchitecture.Core.Application.Responses;
using Domain.Entities;
using NArchitecture.Core.Persistence.Paging;

namespace Application.Features.Managers.Profiles;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        CreateMap<CreateManagerCommand, Manager>();
        CreateMap<Manager, CreatedManagerResponse>();

        CreateMap<UpdateManagerCommand, Manager>();
        CreateMap<Manager, UpdatedManagerResponse>();

        CreateMap<DeleteManagerCommand, Manager>();
        CreateMap<Manager, DeletedManagerResponse>();

        CreateMap<Manager, GetByIdManagerResponse>();

        CreateMap<Manager, GetListManagerListItemDto>();
        CreateMap<IPaginate<Manager>, GetListResponse<GetListManagerListItemDto>>();
    }
}