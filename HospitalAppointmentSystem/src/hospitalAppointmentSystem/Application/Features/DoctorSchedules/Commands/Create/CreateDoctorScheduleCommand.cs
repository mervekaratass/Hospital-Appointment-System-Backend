using Application.Features.DoctorSchedules.Constants;
using Application.Features.DoctorSchedules.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using NArchitecture.Core.Application.Pipelines.Authorization;
using NArchitecture.Core.Application.Pipelines.Caching;
using NArchitecture.Core.Application.Pipelines.Logging;
using NArchitecture.Core.Application.Pipelines.Transaction;
using MediatR;
using static Application.Features.DoctorSchedules.Constants.DoctorSchedulesOperationClaims;

namespace Application.Features.DoctorSchedules.Commands.Create;

public class CreateDoctorScheduleCommand : IRequest<CreatedDoctorScheduleResponse>, ISecuredRequest, ICacheRemoverRequest, ILoggableRequest, ITransactionalRequest
{
    public required Guid DoctorID { get; set; }
    public required DateOnly Date { get; set; }
    public required TimeOnly StartTime { get; set; }
    public required TimeOnly EndTime { get; set; }

    public string[] Roles => [Admin, Write, DoctorSchedulesOperationClaims.Create];

    public bool BypassCache { get; }
    public string? CacheKey { get; }
    public string[]? CacheGroupKey => ["GetDoctorSchedules"];

    public class CreateDoctorScheduleCommandHandler : IRequestHandler<CreateDoctorScheduleCommand, CreatedDoctorScheduleResponse>
    {
        private readonly IMapper _mapper;
        private readonly IDoctorScheduleRepository _doctorScheduleRepository;
        private readonly DoctorScheduleBusinessRules _doctorScheduleBusinessRules;

        public CreateDoctorScheduleCommandHandler(IMapper mapper, IDoctorScheduleRepository doctorScheduleRepository,
                                         DoctorScheduleBusinessRules doctorScheduleBusinessRules)
        {
            _mapper = mapper;
            _doctorScheduleRepository = doctorScheduleRepository;
            _doctorScheduleBusinessRules = doctorScheduleBusinessRules;
        }

        public async Task<CreatedDoctorScheduleResponse> Handle(CreateDoctorScheduleCommand request, CancellationToken cancellationToken)
        {
            DoctorSchedule doctorSchedule = _mapper.Map<DoctorSchedule>(request);

            await _doctorScheduleRepository.AddAsync(doctorSchedule);

            CreatedDoctorScheduleResponse response = _mapper.Map<CreatedDoctorScheduleResponse>(doctorSchedule);
            return response;
        }
    }
}