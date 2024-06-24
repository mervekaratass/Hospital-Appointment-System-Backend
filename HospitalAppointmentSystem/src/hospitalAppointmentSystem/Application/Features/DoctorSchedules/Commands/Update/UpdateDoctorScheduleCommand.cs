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
using Application.Features.Doctors.Constants;

namespace Application.Features.DoctorSchedules.Commands.Update;

public class UpdateDoctorScheduleCommand : IRequest<UpdatedDoctorScheduleResponse>, ISecuredRequest,  ILoggableRequest, ITransactionalRequest
{
    public int Id { get; set; }
    public required Guid DoctorID { get; set; }
    public required DateOnly Date { get; set; }
    public required TimeOnly StartTime { get; set; }
    public required TimeOnly EndTime { get; set; }

    public string[] Roles => [Admin, Write, DoctorSchedulesOperationClaims.Update, DoctorsOperationClaims.Update];

    public bool BypassCache { get; }
    public string? CacheKey { get; }
    public string[]? CacheGroupKey => ["GetDoctorSchedules"];

    public class UpdateDoctorScheduleCommandHandler : IRequestHandler<UpdateDoctorScheduleCommand, UpdatedDoctorScheduleResponse>
    {
        private readonly IMapper _mapper;
        private readonly IDoctorScheduleRepository _doctorScheduleRepository;
        private readonly DoctorScheduleBusinessRules _doctorScheduleBusinessRules;

        public UpdateDoctorScheduleCommandHandler(IMapper mapper, IDoctorScheduleRepository doctorScheduleRepository,
                                         DoctorScheduleBusinessRules doctorScheduleBusinessRules)
        {
            _mapper = mapper;
            _doctorScheduleRepository = doctorScheduleRepository;
            _doctorScheduleBusinessRules = doctorScheduleBusinessRules;
        }

        public async Task<UpdatedDoctorScheduleResponse> Handle(UpdateDoctorScheduleCommand request, CancellationToken cancellationToken)
        {

            await _doctorScheduleBusinessRules.DoctorScheduleShouldNotBeUpdatedIfSoftDeleted(request.Id, cancellationToken);
            await _doctorScheduleBusinessRules.CheckIfDoctorScheduleDateIsUniqueForDoctorOnUpdate(request.Id, request.DoctorID, request.Date, cancellationToken);

            DoctorSchedule doctorSchedule = await _doctorScheduleRepository.GetAsync(predicate: ds => ds.Id == request.Id, cancellationToken: cancellationToken);
            _mapper.Map(request, doctorSchedule);

            await _doctorScheduleRepository.UpdateAsync(doctorSchedule);

            UpdatedDoctorScheduleResponse response = _mapper.Map<UpdatedDoctorScheduleResponse>(doctorSchedule);
            return response;
        }

    }
}