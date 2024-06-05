using Application.Features.Doctors.Constants;
using Application.Features.Doctors.Constants;
using Application.Features.Doctors.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using NArchitecture.Core.Application.Pipelines.Authorization;
using NArchitecture.Core.Application.Pipelines.Caching;
using NArchitecture.Core.Application.Pipelines.Logging;
using NArchitecture.Core.Application.Pipelines.Transaction;
using MediatR;
using static Application.Features.Doctors.Constants.DoctorsOperationClaims;

namespace Application.Features.Doctors.Commands.Delete;

public class DeleteDoctorCommand : IRequest<DeletedDoctorResponse>, ICacheRemoverRequest, ILoggableRequest, ITransactionalRequest
{
    public Guid Id { get; set; }

    public string[] Roles => [Admin, Write]; // DoctorsOperationClaims.Delete

    public bool BypassCache { get; }
    public string? CacheKey { get; }
    public string[]? CacheGroupKey => ["GetDoctors"];

    public class DeleteDoctorCommandHandler : IRequestHandler<DeleteDoctorCommand, DeletedDoctorResponse>
    {
        private readonly IMapper _mapper;
        private readonly IDoctorRepository _doctorRepository;
        private readonly DoctorBusinessRules _doctorBusinessRules;

        public DeleteDoctorCommandHandler(IMapper mapper, IDoctorRepository doctorRepository,
                                         DoctorBusinessRules doctorBusinessRules)
        {
            _mapper = mapper;
            _doctorRepository = doctorRepository;
            _doctorBusinessRules = doctorBusinessRules;
        }

        public async Task<DeletedDoctorResponse> Handle(DeleteDoctorCommand request, CancellationToken cancellationToken)
        {
            Doctor? doctor = await _doctorRepository.GetAsync(predicate: d => d.Id == request.Id, cancellationToken: cancellationToken);
            await _doctorBusinessRules.DoctorShouldExistWhenSelected(doctor);

            await _doctorRepository.DeleteAsync(doctor!);

            DeletedDoctorResponse response = _mapper.Map<DeletedDoctorResponse>(doctor);
            return response;
        }
    }
}