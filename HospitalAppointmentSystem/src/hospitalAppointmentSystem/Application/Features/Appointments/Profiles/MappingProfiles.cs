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

namespace Application.Features.Appointments.Profiles;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        CreateMap<CreateAppointmentCommand, Appointment>();
        CreateMap<Appointment, CreatedAppointmentResponse>();

        CreateMap<UpdateAppointmentCommand, Appointment>();
        CreateMap<Appointment, UpdatedAppointmentResponse>();

        CreateMap<DeleteAppointmentCommand, Appointment>();
        CreateMap<Appointment, DeletedAppointmentResponse>();

        CreateMap<Appointment, GetByIdAppointmentResponse>();

        CreateMap<Appointment, GetListAppointmentListItemDto>();
        CreateMap<IPaginate<Appointment>, GetListResponse<GetListAppointmentListItemDto>>();

        CreateMap<Appointment, GetListByPatientDto>();
        CreateMap<IPaginate<Appointment>, GetListResponse<GetListByPatientDto>>();



    }
}