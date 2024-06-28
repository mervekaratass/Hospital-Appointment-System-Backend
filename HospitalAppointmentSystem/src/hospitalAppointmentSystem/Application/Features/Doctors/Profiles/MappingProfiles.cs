using Application.Features.Doctors.Commands.Create;
using Application.Features.Doctors.Commands.Delete;
using Application.Features.Doctors.Commands.Update;
using Application.Features.Doctors.Queries.GetById;
using Application.Features.Doctors.Queries.GetList;
using AutoMapper;
using NArchitecture.Core.Application.Responses;
using Domain.Entities;
using NArchitecture.Core.Persistence.Paging;
using Application.Features.DoctorSchedules.Queries.GetListByDoctorId;
using Application.Features.Doctors.Queries.GetListByBranchId;

namespace Application.Features.Doctors.Profiles;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        CreateMap<CreateDoctorCommand, Doctor>();
        CreateMap<Doctor, CreatedDoctorResponse>();

        CreateMap<UpdateDoctorCommand, Doctor>();
        CreateMap<Doctor, UpdatedDoctorResponse>();

        CreateMap<DeleteDoctorCommand, Doctor>();
        CreateMap<Doctor, DeletedDoctorResponse>();

        CreateMap<Doctor, GetByIdDoctorResponse>().ForMember(x => x.BranchName, opt => opt.MapFrom(dto => dto.Branch.Name));

        CreateMap<Doctor, GetListDoctorListItemDto>();
        CreateMap<IPaginate<Doctor>, GetListResponse<GetListDoctorListItemDto>>();

        CreateMap<Doctor, GetListByBranchIdDto>().ForMember(x => x.BranchName, opt => opt.MapFrom(dto => dto.Branch.Name));
        CreateMap<IPaginate<Doctor>, GetListResponse<GetListByBranchIdDto>>();



        CreateMap<DoctorSchedule, GetListByDoctorIdDto>().ForMember(x => x.DoctorID, opt => opt.MapFrom(dto => dto.Doctor.Id))
           .ForMember(x => x.DoctorFirstName, opt => opt.MapFrom(dto => dto.Doctor.FirstName))
           .ForMember(x => x.DoctorLastName, opt => opt.MapFrom(dto => dto.Doctor.LastName))
           .ForMember(x => x.Date, opt => opt.MapFrom(dto => dto.Date))
           .ForMember(x => x.StartTime, opt => opt.MapFrom(dto => dto.StartTime))
           .ForMember(x => x.EndTime, opt => opt.MapFrom(dto => dto.EndTime));
    }
}