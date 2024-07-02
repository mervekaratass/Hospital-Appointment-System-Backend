using Application.Features.Doctors.Constants;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using NArchitecture.Core.Application.Pipelines.Authorization;
using NArchitecture.Core.Application.Pipelines.Caching;
using NArchitecture.Core.Application.Requests;
using NArchitecture.Core.Application.Responses;
using NArchitecture.Core.Persistence.Paging;
using MediatR;
using static Application.Features.Doctors.Constants.DoctorsOperationClaims;
using Microsoft.EntityFrameworkCore;
using Application.Services.Encryptions;
using System.Numerics;

namespace Application.Features.Doctors.Queries.GetList;

public class GetListDoctorQuery : IRequest<GetListResponse<GetListDoctorListItemDto>>
{
    public PageRequest PageRequest { get; set; }

    public string[] Roles => [Admin, Read];

    public bool BypassCache { get; }
    public string? CacheKey => $"GetListDoctors({PageRequest.PageIndex},{PageRequest.PageSize})";
    public string? CacheGroupKey => "GetDoctors";
    public TimeSpan? SlidingExpiration { get; }

    public class GetListDoctorQueryHandler : IRequestHandler<GetListDoctorQuery, GetListResponse<GetListDoctorListItemDto>>
    {
        private readonly IDoctorRepository _doctorRepository;
        private readonly IMapper _mapper;

        public GetListDoctorQueryHandler(IDoctorRepository doctorRepository, IMapper mapper)
        {
            _doctorRepository = doctorRepository;
            _mapper = mapper;
        }

        public async Task<GetListResponse<GetListDoctorListItemDto>> Handle(GetListDoctorQuery request, CancellationToken cancellationToken)
        {
            IPaginate<Doctor> doctors = await _doctorRepository.GetListAsync(
                index: request.PageRequest.PageIndex,
                size: request.PageRequest.PageSize, 
                cancellationToken: cancellationToken,
            include: x => x.Include(x => x.Branch)
            );

            // S�NEM Foreach ile d�ndurunce  Ipaginat ekleme i�lemine izin vermiyor ,hata veriyor .

            for (int i = 0; i < doctors.Items.Count; i++)
            {
                doctors.Items[i].FirstName = CryptoHelper.Decrypt(doctors.Items[i].FirstName);
                doctors.Items[i].LastName = CryptoHelper.Decrypt(doctors.Items[i].LastName);
                doctors.Items[i].NationalIdentity = CryptoHelper.Decrypt(doctors.Items[i].NationalIdentity);
                doctors.Items[i].Phone = CryptoHelper.Decrypt(doctors.Items[i].Phone);
                doctors.Items[i].Address = CryptoHelper.Decrypt(doctors.Items[i].Address);
                doctors.Items[i].Email = CryptoHelper.Decrypt(doctors.Items[i].Email);
            }

                
                GetListResponse<GetListDoctorListItemDto> response = _mapper.Map<GetListResponse<GetListDoctorListItemDto>>(doctors); 
            return response;
        }
    }
}