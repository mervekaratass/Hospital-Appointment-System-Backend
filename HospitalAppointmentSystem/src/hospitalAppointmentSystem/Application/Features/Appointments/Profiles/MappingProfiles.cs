using Application.Features.Appointments.Commands.Create;
using Application.Features.Appointments.Commands.Delete;
using Application.Features.Appointments.Commands.Update;
using Application.Features.Appointments.Queries.GetById;
using Application.Features.Appointments.Queries.GetList;
using AutoMapper;
using NArchitecture.Core.Application.Responses;
using Domain.Entities;
using NArchitecture.Core.Persistence.Paging;
using Application.Features.Appointments.Queries.GetByPatientId;
using Application.Features.Appointments.Queries.GetListByDoctor;
using Application.Features.Appointments.Queries.GetListByDoctorDate;

namespace Application.Features.Appointments.Profiles;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        CreateMap<CreateAppointmentCommand, Appointment>();
        CreateMap<Appointment, CreatedAppointmentResponse>()
            .ForMember(x => x.PatientFirstName, opt => opt.MapFrom(src => src.Patient.FirstName))
            .ForMember(x => x.PatientLastName, opt => opt.MapFrom(src => src.Patient.LastName))
            .ForMember(x => x.DoctorFirstName, opt => opt.MapFrom(src => src.Doctor.FirstName))
            .ForMember(x => x.DoctorLastName, opt => opt.MapFrom(src => src.Doctor.LastName))
            .ForMember(x => x.DoctorBranch, opt => opt.MapFrom(src => src.Doctor.Branch))
            ; 

        CreateMap<UpdateAppointmentCommand, Appointment>();
        CreateMap<Appointment, UpdatedAppointmentResponse>();

        CreateMap<DeleteAppointmentCommand, Appointment>();
        CreateMap<Appointment, DeletedAppointmentResponse>()
            .ForMember(x => x.PatientFirstName, opt => opt.MapFrom(src => src.Patient.FirstName))
            .ForMember(x => x.PatientLastName, opt => opt.MapFrom(src => src.Patient.LastName))
            .ForMember(x => x.DoctorFirstName, opt => opt.MapFrom(src => src.Doctor.FirstName))
            .ForMember(x => x.DoctorLastName, opt => opt.MapFrom(src => src.Doctor.LastName))
            .ForMember(x => x.DoctorBranch, opt => opt.MapFrom(src => src.Doctor.Branch))
            ;

        CreateMap<Appointment, GetByIdAppointmentResponse>();

        CreateMap<Appointment, GetListAppointmentListItemDto>();
        CreateMap<IPaginate<Appointment>, GetListResponse<GetListAppointmentListItemDto>>();

        CreateMap<Appointment, GetListByPatientDto>().ForMember(x=>x.BranchName,opt=>opt.MapFrom(src=>src.Doctor.Branch.Name));
        CreateMap<IPaginate<Appointment>, GetListResponse<GetListByPatientDto>>();

        CreateMap<Appointment, GetListByDoctorDto>();
        CreateMap<IPaginate<Appointment>, GetListResponse<GetListByDoctorDto>>();


        CreateMap<Appointment, GetListByDoctorDateDto>();
        CreateMap<IPaginate<Appointment>, GetListResponse<GetListByDoctorDateDto>>();



    }
}