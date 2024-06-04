using Application.Features.Notifications.Commands.Create;
using Application.Features.Notifications.Commands.Delete;
using Application.Features.Notifications.Commands.Update;
using Application.Features.Notifications.Queries.GetById;
using Application.Features.Notifications.Queries.GetList;
using AutoMapper;
using NArchitecture.Core.Application.Responses;
using Domain.Entities;
using NArchitecture.Core.Persistence.Paging;

namespace Application.Features.Notifications.Profiles;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        CreateMap<CreateNotificationCommand, Notification>();
        CreateMap<Notification, CreatedNotificationResponse>();

        CreateMap<UpdateNotificationCommand, Notification>();
        CreateMap<Notification, UpdatedNotificationResponse>();

        CreateMap<DeleteNotificationCommand, Notification>();
        CreateMap<Notification, DeletedNotificationResponse>();

        CreateMap<Notification, GetByIdNotificationResponse>();

        CreateMap<Notification, GetListNotificationListItemDto>();
        CreateMap<IPaginate<Notification>, GetListResponse<GetListNotificationListItemDto>>();
    }
}