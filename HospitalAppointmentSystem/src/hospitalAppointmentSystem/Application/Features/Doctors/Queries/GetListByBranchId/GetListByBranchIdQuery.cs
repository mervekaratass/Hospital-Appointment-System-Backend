using Application.Features.Doctors.Constants;
using Application.Features.DoctorSchedules.Queries.GetListByDoctorId;
using Application.Features.Patients.Constants;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using MediatR;
using NArchitecture.Core.Application.Pipelines.Authorization;
using NArchitecture.Core.Application.Requests;
using NArchitecture.Core.Application.Responses;
using static Application.Features.Doctors.Constants.DoctorsOperationClaims;
using NArchitecture.Core.Persistence.Paging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Application.Services.Encryptions;
using NArchitecture.Core.Security.Entities;

namespace Application.Features.Doctors.Queries.GetListByBranchId;
public class GetListByBranchIdQuery : IRequest<GetListResponse<GetListByBranchIdDto>>, ISecuredRequest
{
    public int BranchId { get; set; }
    public PageRequest PageRequest { get; set; }

    public string[] Roles => [Admin, Read, PatientsOperationClaims.Update];

    public bool BypassCache { get; }
    public string? CacheKey => $"GetListDoctors({PageRequest.PageIndex},{PageRequest.PageSize})";
    public string? CacheGroupKey => "GetDoctors";
    public TimeSpan? SlidingExpiration { get; }

    public class GetListByBranchIdQueryHandler : IRequestHandler<GetListByBranchIdQuery, GetListResponse<GetListByBranchIdDto>>
    {
        private readonly IDoctorRepository _doctorRepository;
        private readonly IMapper _mapper;

        public GetListByBranchIdQueryHandler(IDoctorRepository doctorRepository, IMapper mapper)
        {
            _doctorRepository = doctorRepository;
            _mapper = mapper;
        }

        public async Task<GetListResponse<GetListByBranchIdDto>> Handle(GetListByBranchIdQuery request, CancellationToken cancellationToken)
        {
            IPaginate<Doctor> doctors = await _doctorRepository.GetListAsync(
                index: request.PageRequest.PageIndex,
                size: request.PageRequest.PageSize,
                cancellationToken: cancellationToken,
                //orderBy: x => x.OrderByDescending(x => x.Date),
                include: x => x.Include(x => x.Branch),
                predicate: x => x.BranchID == request.BranchId && x.DeletedDate == null
            );

            for (int i = 0; i < doctors.Items.Count; i++)
            {
                doctors.Items[i].FirstName = CryptoHelper.Decrypt(doctors.Items[i].FirstName);
                doctors.Items[i].LastName = CryptoHelper.Decrypt(doctors.Items[i].LastName);
                doctors.Items[i].NationalIdentity = CryptoHelper.Decrypt(doctors.Items[i].NationalIdentity);
                doctors.Items[i].Phone = CryptoHelper.Decrypt(doctors.Items[i].Phone);
                doctors.Items[i].Address = CryptoHelper.Decrypt(doctors.Items[i].Address);
                doctors.Items[i].Email = CryptoHelper.Decrypt(doctors.Items[i].Email);
            }
            GetListResponse<GetListByBranchIdDto> response = _mapper.Map<GetListResponse<GetListByBranchIdDto>>(doctors);
            return response;
        }
    }
}