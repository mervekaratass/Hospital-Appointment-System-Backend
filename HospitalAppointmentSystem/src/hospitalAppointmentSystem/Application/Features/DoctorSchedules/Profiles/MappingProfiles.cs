using Application.Features.DoctorSchedules.Commands.Create;
using Application.Features.DoctorSchedules.Commands.Delete;
using Application.Features.DoctorSchedules.Commands.Update;
using Application.Features.DoctorSchedules.Queries.GetById;
using Application.Features.DoctorSchedules.Queries.GetList;
using AutoMapper;
using NArchitecture.Core.Application.Responses;
using Domain.Entities;
using NArchitecture.Core.Persistence.Paging;
using Application.Features.DoctorSchedules.Queries.GetListByDoctorId;

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

        CreateMap<DoctorSchedule, GetListByDoctorIdDto>().ForMember(x=>x.DoctorID , opt => opt.MapFrom(dto => dto.Doctor.Id))
            .ForMember(x => x.DoctorFirstName, opt => opt.MapFrom(dto => dto.Doctor.FirstName))
            .ForMember(x => x.DoctorLastName, opt => opt.MapFrom(dto => dto.Doctor.LastName))
            .ForMember(x => x.Date, opt => opt.MapFrom(dto => dto.Date))
            .ForMember(x => x.StartTime, opt => opt.MapFrom(dto => dto.StartTime))
            .ForMember(x => x.EndTime, opt => opt.MapFrom(dto => dto.EndTime));


        CreateMap<IPaginate<DoctorSchedule>, GetListResponse<GetListByDoctorIdDto>>();
  
    }
}