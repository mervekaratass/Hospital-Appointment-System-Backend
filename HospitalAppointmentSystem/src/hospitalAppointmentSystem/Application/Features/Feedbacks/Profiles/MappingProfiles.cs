using Application.Features.Feedbacks.Commands.Create;
using Application.Features.Feedbacks.Commands.Delete;
using Application.Features.Feedbacks.Commands.Update;
using Application.Features.Feedbacks.Queries.GetById;
using Application.Features.Feedbacks.Queries.GetList;
using AutoMapper;
using NArchitecture.Core.Application.Responses;
using Domain.Entities;
using NArchitecture.Core.Persistence.Paging;
using Application.Features.Feedbacks.Queries.GetListByUser;

namespace Application.Features.Feedbacks.Profiles;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        CreateMap<CreateFeedbackCommand, Feedback>();
        CreateMap<Feedback, CreatedFeedbackResponse>();

        CreateMap<UpdateFeedbackCommand, Feedback>();
        CreateMap<Feedback, UpdatedFeedbackResponse>();

        CreateMap<DeleteFeedbackCommand, Feedback>();
        CreateMap<Feedback, DeletedFeedbackResponse>();

        CreateMap<Feedback, GetByIdFeedbackResponse>();

        CreateMap<Feedback, GetListFeedbackListItemDto>();
        CreateMap<IPaginate<Feedback>, GetListResponse<GetListFeedbackListItemDto>>();

        CreateMap<Feedback, GetListByUserDto>();
        CreateMap<IPaginate<Feedback>, GetListResponse<GetListByUserDto>>();
    }
}