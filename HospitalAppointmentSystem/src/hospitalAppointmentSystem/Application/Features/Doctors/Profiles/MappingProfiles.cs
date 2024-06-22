using Application.Features.Doctors.Commands.Create;
using Application.Features.Doctors.Commands.Delete;
using Application.Features.Doctors.Commands.Update;
using Application.Features.Doctors.Queries.GetById;
using Application.Features.Doctors.Queries.GetList;
using AutoMapper;
using NArchitecture.Core.Application.Responses;
using Domain.Entities;
using NArchitecture.Core.Persistence.Paging;

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
    }
}