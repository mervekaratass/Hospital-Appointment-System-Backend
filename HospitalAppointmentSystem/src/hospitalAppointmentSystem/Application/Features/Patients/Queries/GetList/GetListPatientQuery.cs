using Application.Features.Patients.Constants;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using NArchitecture.Core.Application.Pipelines.Authorization;
using NArchitecture.Core.Application.Pipelines.Caching;
using NArchitecture.Core.Application.Requests;
using NArchitecture.Core.Application.Responses;
using NArchitecture.Core.Persistence.Paging;
using MediatR;
using static Application.Features.Patients.Constants.PatientsOperationClaims;
using Application.Services.Encryptions;

namespace Application.Features.Patients.Queries.GetList;

public class GetListPatientQuery : IRequest<GetListResponse<GetListPatientListItemDto>>,  ISecuredRequest
{
    public PageRequest PageRequest { get; set; }

    public string[] Roles => [Admin, Read];

    public bool BypassCache { get; }
    public string? CacheKey => $"GetListPatients({PageRequest.PageIndex},{PageRequest.PageSize})";
    public string? CacheGroupKey => "GetPatients";
    public TimeSpan? SlidingExpiration { get; }

    public class GetListPatientQueryHandler : IRequestHandler<GetListPatientQuery, GetListResponse<GetListPatientListItemDto>>
    {
        private readonly IPatientRepository _patientRepository;
        private readonly IMapper _mapper;

        public GetListPatientQueryHandler(IPatientRepository patientRepository, IMapper mapper)
        {
            _patientRepository = patientRepository;
            _mapper = mapper;
        }

        public async Task<GetListResponse<GetListPatientListItemDto>> Handle(GetListPatientQuery request, CancellationToken cancellationToken)
        {
            IPaginate<Patient> patients = await _patientRepository.GetListAsync(
                index: request.PageRequest.PageIndex,
                size: request.PageRequest.PageSize, 
                cancellationToken: cancellationToken
            );



            // SÝNEM .

            for (int i = 0; i < patients.Items.Count; i++)
            {
                patients.Items[i].FirstName = CryptoHelper.Decrypt(patients.Items[i].FirstName);
                patients.Items[i].LastName = CryptoHelper.Decrypt(patients.Items[i].LastName);
                patients.Items[i].NationalIdentity = CryptoHelper.Decrypt(patients.Items[i].NationalIdentity);
                patients.Items[i].Phone = CryptoHelper.Decrypt(patients.Items[i].Phone);
                patients.Items[i].Address = CryptoHelper.Decrypt(patients.Items[i].Address);
                patients.Items[i].Email = CryptoHelper.Decrypt(patients.Items[i].Email);
            }



            GetListResponse<GetListPatientListItemDto> response = _mapper.Map<GetListResponse<GetListPatientListItemDto>>(patients);
            return response;
        }
    }
}