using Application.Features.DoctorSchedules.Constants;
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

namespace Application.Features.DoctorSchedules.Commands.Delete;

public class DeleteDoctorScheduleCommand : IRequest<DeletedDoctorScheduleResponse>, ISecuredRequest, ICacheRemoverRequest, ILoggableRequest, ITransactionalRequest
{
    public int Id { get; set; }

    public string[] Roles => [Admin, Write, DoctorSchedulesOperationClaims.Delete];

    public bool BypassCache { get; }
    public string? CacheKey { get; }
    public string[]? CacheGroupKey => ["GetDoctorSchedules"];

    public class DeleteDoctorScheduleCommandHandler : IRequestHandler<DeleteDoctorScheduleCommand, DeletedDoctorScheduleResponse>
    {
        private readonly IMapper _mapper;
        private readonly IDoctorScheduleRepository _doctorScheduleRepository;
        private readonly DoctorScheduleBusinessRules _doctorScheduleBusinessRules;

        public DeleteDoctorScheduleCommandHandler(IMapper mapper, IDoctorScheduleRepository doctorScheduleRepository,
                                         DoctorScheduleBusinessRules doctorScheduleBusinessRules)
        {
            _mapper = mapper;
            _doctorScheduleRepository = doctorScheduleRepository;
            _doctorScheduleBusinessRules = doctorScheduleBusinessRules;
        }

        public async Task<DeletedDoctorScheduleResponse> Handle(DeleteDoctorScheduleCommand request, CancellationToken cancellationToken)
        {
            DoctorSchedule? doctorSchedule = await _doctorScheduleRepository.GetAsync(predicate: ds => ds.Id == request.Id, cancellationToken: cancellationToken);
            await _doctorScheduleBusinessRules.DoctorScheduleShouldExistWhenSelected(doctorSchedule);

            await _doctorScheduleRepository.DeleteAsync(doctorSchedule!);

            DeletedDoctorScheduleResponse response = _mapper.Map<DeletedDoctorScheduleResponse>(doctorSchedule);
            return response;
        }
    }
}