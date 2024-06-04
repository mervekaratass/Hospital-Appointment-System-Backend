using Application.Features.DoctorSchedules.Constants;
using Application.Features.DoctorSchedules.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using NArchitecture.Core.Application.Pipelines.Authorization;
using MediatR;
using static Application.Features.DoctorSchedules.Constants.DoctorSchedulesOperationClaims;

namespace Application.Features.DoctorSchedules.Queries.GetById;

public class GetByIdDoctorScheduleQuery : IRequest<GetByIdDoctorScheduleResponse>, ISecuredRequest
{
    public int Id { get; set; }

    public string[] Roles => [Admin, Read];

    public class GetByIdDoctorScheduleQueryHandler : IRequestHandler<GetByIdDoctorScheduleQuery, GetByIdDoctorScheduleResponse>
    {
        private readonly IMapper _mapper;
        private readonly IDoctorScheduleRepository _doctorScheduleRepository;
        private readonly DoctorScheduleBusinessRules _doctorScheduleBusinessRules;

        public GetByIdDoctorScheduleQueryHandler(IMapper mapper, IDoctorScheduleRepository doctorScheduleRepository, DoctorScheduleBusinessRules doctorScheduleBusinessRules)
        {
            _mapper = mapper;
            _doctorScheduleRepository = doctorScheduleRepository;
            _doctorScheduleBusinessRules = doctorScheduleBusinessRules;
        }

        public async Task<GetByIdDoctorScheduleResponse> Handle(GetByIdDoctorScheduleQuery request, CancellationToken cancellationToken)
        {
            DoctorSchedule? doctorSchedule = await _doctorScheduleRepository.GetAsync(predicate: ds => ds.Id == request.Id, cancellationToken: cancellationToken);
            await _doctorScheduleBusinessRules.DoctorScheduleShouldExistWhenSelected(doctorSchedule);

            GetByIdDoctorScheduleResponse response = _mapper.Map<GetByIdDoctorScheduleResponse>(doctorSchedule);
            return response;
        }
    }
}