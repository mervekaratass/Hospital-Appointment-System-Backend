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

namespace Application.Features.Doctors.Commands.Create;

public class CreateDoctorCommand : IRequest<CreatedDoctorResponse>, ISecuredRequest, ICacheRemoverRequest, ILoggableRequest, ITransactionalRequest
{
    public string Title { get; set; }
    public string SchoolName { get; set; }
    public int BranchID { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public DateOnly DateOfBirth { get; set; }
    public string NationalIdentity { get; set; }
    public string Phone { get; set; }
    public string Address { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
<<<<<<< Updated upstream
    public string[] Roles => [Admin, Write, DoctorsOperationClaims.Create];
=======

    public string[] Roles => [Admin, Write];
>>>>>>> Stashed changes

    public bool BypassCache { get; }
    public string? CacheKey { get; }
    public string[]? CacheGroupKey => ["GetDoctors"];

    public class CreateDoctorCommandHandler : IRequestHandler<CreateDoctorCommand, CreatedDoctorResponse>
    {
        private readonly IMapper _mapper;
        private readonly IDoctorRepository _doctorRepository;
        private readonly DoctorBusinessRules _doctorBusinessRules;

        public CreateDoctorCommandHandler(IMapper mapper, IDoctorRepository doctorRepository,
                                         DoctorBusinessRules doctorBusinessRules)
        {
            _mapper = mapper;
            _doctorRepository = doctorRepository;
            _doctorBusinessRules = doctorBusinessRules;
        }

        public async Task<CreatedDoctorResponse> Handle(CreateDoctorCommand request, CancellationToken cancellationToken)
        {
            Doctor doctor = _mapper.Map<Doctor>(request);

            await _doctorRepository.AddAsync(doctor);

            CreatedDoctorResponse response = _mapper.Map<CreatedDoctorResponse>(doctor);
            return response;
        }
    }
}