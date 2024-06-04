using Application.Features.Reports.Commands.Create;
using Application.Features.Reports.Commands.Delete;
using Application.Features.Reports.Commands.Update;
using Application.Features.Reports.Queries.GetById;
using Application.Features.Reports.Queries.GetList;
using AutoMapper;
using NArchitecture.Core.Application.Responses;
using Domain.Entities;
using NArchitecture.Core.Persistence.Paging;

namespace Application.Features.Reports.Profiles;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        CreateMap<CreateReportCommand, Report>();
        CreateMap<Report, CreatedReportResponse>();

        CreateMap<UpdateReportCommand, Report>();
        CreateMap<Report, UpdatedReportResponse>();

        CreateMap<DeleteReportCommand, Report>();
        CreateMap<Report, DeletedReportResponse>();

        CreateMap<Report, GetByIdReportResponse>();

        CreateMap<Report, GetListReportListItemDto>();
        CreateMap<IPaginate<Report>, GetListResponse<GetListReportListItemDto>>();
    }
}