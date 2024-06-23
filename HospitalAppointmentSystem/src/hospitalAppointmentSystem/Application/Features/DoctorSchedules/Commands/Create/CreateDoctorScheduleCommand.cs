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

namespace Application.Features.DoctorSchedules.Commands.Create;

public class CreateDoctorScheduleCommand : IRequest<CreatedDoctorScheduleResponse>,/* ISecuredRequest,*/ ICacheRemoverRequest, ILoggableRequest, ITransactionalRequest
{
    public required Guid DoctorID { get; set; }
    public required DateOnly Date { get; set; }
    public required TimeOnly StartTime { get; set; }
    public required TimeOnly EndTime { get; set; }

    public string[] Roles => [Admin, Write, DoctorSchedulesOperationClaims.Create,DoctorsOperationClaims.Update];

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
            CreatedDoctorScheduleResponse response;

            var softDeletedSchedule = await _doctorScheduleBusinessRules.CheckAndRetrieveSoftDeletedSchedule(request.DoctorID, request.Date);

            if (softDeletedSchedule != null)
            {
                softDeletedSchedule.DeletedDate = null; // Soft delete tarihini kaldýrarak kaydý aktif hale getiriyoruz.
                softDeletedSchedule.StartTime = request.StartTime;
                softDeletedSchedule.EndTime = request.EndTime;

                await _doctorScheduleRepository.UpdateAsync(softDeletedSchedule);

                response = _mapper.Map<CreatedDoctorScheduleResponse>(softDeletedSchedule);
            }
            else
            {
                await _doctorScheduleBusinessRules.CheckIfDoctorScheduleDateIsUniqueForDoctor(request.DoctorID, request.Date);

                DoctorSchedule doctorSchedule = _mapper.Map<DoctorSchedule>(request);
                await _doctorScheduleRepository.AddAsync(doctorSchedule);

                response = _mapper.Map<CreatedDoctorScheduleResponse>(doctorSchedule);
            }

            return response;
        }

    }
}