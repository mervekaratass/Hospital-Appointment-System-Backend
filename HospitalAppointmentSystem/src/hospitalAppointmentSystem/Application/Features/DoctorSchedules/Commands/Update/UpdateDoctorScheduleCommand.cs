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
using NArchitecture.Core.CrossCuttingConcerns.Exception.Types;
using Application.Services.Appointments;
using Application.Services.DoctorSchedules;

namespace Application.Features.DoctorSchedules.Commands.Update
{
    public class UpdateDoctorScheduleCommand : IRequest<UpdatedDoctorScheduleResponse>, ISecuredRequest, ILoggableRequest, ITransactionalRequest
    {
        public int Id { get; set; }
        public Guid DoctorID { get; set; }
        public DateOnly Date { get; set; }
        public TimeOnly StartTime { get; set; }
        public TimeOnly EndTime { get; set; }

        public string[] Roles => new[] { Admin, Write, DoctorSchedulesOperationClaims.Update, DoctorsOperationClaims.Update };

        public bool BypassCache { get; }
        public string? CacheKey { get; }
        public string[]? CacheGroupKey => new[] { "GetDoctorSchedules" };

        public class UpdateDoctorScheduleCommandHandler : IRequestHandler<UpdateDoctorScheduleCommand, UpdatedDoctorScheduleResponse>
        {
            private readonly IMapper _mapper;
            private readonly IDoctorScheduleRepository _doctorScheduleRepository;
            private readonly DoctorScheduleBusinessRules _doctorScheduleBusinessRules;
            private readonly IAppointmentService _appointmentService;

            public UpdateDoctorScheduleCommandHandler(IMapper mapper, IDoctorScheduleRepository doctorScheduleRepository,
                                             DoctorScheduleBusinessRules doctorScheduleBusinessRules,IAppointmentService appointmentService)
            {
                _mapper = mapper;
                _doctorScheduleRepository = doctorScheduleRepository;
                _doctorScheduleBusinessRules = doctorScheduleBusinessRules;
                _appointmentService = appointmentService;
            }

            public async Task<UpdatedDoctorScheduleResponse> Handle(UpdateDoctorScheduleCommand request, CancellationToken cancellationToken)
            {
                // Ýlk olarak güncellemek istediðimiz mevcut kaydý alalým
                var existingSchedule = await _doctorScheduleBusinessRules.CheckIfDoctorScheduleExists(request.Id, cancellationToken);



                var appointment = await _doctorScheduleBusinessRules.CheckIfAppointmentsExistOnDateDoctor(request.DoctorID, existingSchedule.Date );

                // Güncellenmek istenen tarih ve doktor ID'si ile silinmiþ bir kayýt var mý diye kontrol edelim
                var conflictingSchedule = await _doctorScheduleRepository.GetAsync(ds => ds.DoctorID == request.DoctorID && ds.Date == request.Date);

                await _doctorScheduleBusinessRules.HandleConflictingSchedule(conflictingSchedule, existingSchedule, request);

                // Çakýþan bir kayýt yoksa mevcut kaydý güncelleyelim
                if (conflictingSchedule == null || conflictingSchedule.Id == request.Id)
                {
                    _mapper.Map(request, existingSchedule);
                  
                   await _doctorScheduleRepository.UpdateAsync(existingSchedule);
               
                }

                // Güncellenen veriyi response olarak dönelim
                UpdatedDoctorScheduleResponse updatedResponse = _mapper.Map<UpdatedDoctorScheduleResponse>(existingSchedule);
                return updatedResponse;


            }
          
        }
    }
}
