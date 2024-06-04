using Application.Features.DoctorSchedules.Commands.Create;
using Application.Features.DoctorSchedules.Commands.Delete;
using Application.Features.DoctorSchedules.Commands.Update;
using Application.Features.DoctorSchedules.Queries.GetById;
using Application.Features.DoctorSchedules.Queries.GetList;
using AutoMapper;
using NArchitecture.Core.Application.Responses;
using Domain.Entities;
using NArchitecture.Core.Persistence.Paging;

namespace Application.Features.DoctorSchedules.Profiles;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        CreateMap<CreateDoctorScheduleCommand, DoctorSchedule>();
        CreateMap<DoctorSchedule, CreatedDoctorScheduleResponse>();

        CreateMap<UpdateDoctorScheduleCommand, DoctorSchedule>();
        CreateMap<DoctorSchedule, UpdatedDoctorScheduleResponse>();

        CreateMap<DeleteDoctorScheduleCommand, DoctorSchedule>();
        CreateMap<DoctorSchedule, DeletedDoctorScheduleResponse>();

        CreateMap<DoctorSchedule, GetByIdDoctorScheduleResponse>();

        CreateMap<DoctorSchedule, GetListDoctorScheduleListItemDto>();
        CreateMap<IPaginate<DoctorSchedule>, GetListResponse<GetListDoctorScheduleListItemDto>>();
    }
}