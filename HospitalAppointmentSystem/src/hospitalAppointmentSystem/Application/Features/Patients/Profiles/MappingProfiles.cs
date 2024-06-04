using Application.Features.Patients.Commands.Create;
using Application.Features.Patients.Commands.Delete;
using Application.Features.Patients.Commands.Update;
using Application.Features.Patients.Queries.GetById;
using Application.Features.Patients.Queries.GetList;
using AutoMapper;
using NArchitecture.Core.Application.Responses;
using Domain.Entities;
using NArchitecture.Core.Persistence.Paging;

namespace Application.Features.Patients.Profiles;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        CreateMap<CreatePatientCommand, Patient>();
        CreateMap<Patient, CreatedPatientResponse>();

        CreateMap<UpdatePatientCommand, Patient>();
        CreateMap<Patient, UpdatedPatientResponse>();

        CreateMap<DeletePatientCommand, Patient>();
        CreateMap<Patient, DeletedPatientResponse>();

        CreateMap<Patient, GetByIdPatientResponse>();

        CreateMap<Patient, GetListPatientListItemDto>();
        CreateMap<IPaginate<Patient>, GetListResponse<GetListPatientListItemDto>>();
    }
}