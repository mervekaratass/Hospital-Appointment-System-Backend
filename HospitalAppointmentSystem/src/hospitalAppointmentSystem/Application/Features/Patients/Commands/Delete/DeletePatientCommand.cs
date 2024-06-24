using Application.Features.Patients.Constants;
using Application.Features.Patients.Constants;
using Application.Features.Patients.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using NArchitecture.Core.Application.Pipelines.Authorization;
using NArchitecture.Core.Application.Pipelines.Caching;
using NArchitecture.Core.Application.Pipelines.Logging;
using NArchitecture.Core.Application.Pipelines.Transaction;
using MediatR;
using static Application.Features.Patients.Constants.PatientsOperationClaims;

namespace Application.Features.Patients.Commands.Delete;

public class DeletePatientCommand : IRequest<DeletedPatientResponse>,  ILoggableRequest, ITransactionalRequest, ISecuredRequest
{
    public Guid Id { get; set; }

    public string[] Roles => [Admin, Write, PatientsOperationClaims.Delete];

    public bool BypassCache { get; }
    public string? CacheKey { get; }
    public string[]? CacheGroupKey => ["GetPatients"];

    public class DeletePatientCommandHandler : IRequestHandler<DeletePatientCommand, DeletedPatientResponse>
    {
        private readonly IMapper _mapper;
        private readonly IPatientRepository _patientRepository;
        private readonly PatientBusinessRules _patientBusinessRules;

        public DeletePatientCommandHandler(IMapper mapper, IPatientRepository patientRepository,
                                         PatientBusinessRules patientBusinessRules)
        {
            _mapper = mapper;
            _patientRepository = patientRepository;
            _patientBusinessRules = patientBusinessRules;
        }

        public async Task<DeletedPatientResponse> Handle(DeletePatientCommand request, CancellationToken cancellationToken)
        {
            Patient? patient = await _patientRepository.GetAsync(predicate: p => p.Id == request.Id, cancellationToken: cancellationToken);
            await _patientBusinessRules.PatientShouldExistWhenSelected(patient);

            patient.DeletedDate = DateTime.Now;
            await _patientRepository.UpdateAsync(patient!);

            DeletedPatientResponse response = _mapper.Map<DeletedPatientResponse>(patient);
            return response;
        }
    }
}