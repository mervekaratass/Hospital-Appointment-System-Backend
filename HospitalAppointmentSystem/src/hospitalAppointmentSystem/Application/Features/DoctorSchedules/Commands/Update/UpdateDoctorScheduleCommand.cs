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
            private readonly IAppointmentRepository _appointmentRepository;

            public UpdateDoctorScheduleCommandHandler(IMapper mapper, IDoctorScheduleRepository doctorScheduleRepository,
                                             DoctorScheduleBusinessRules doctorScheduleBusinessRules,IAppointmentRepository appointmentRepository)
            {
                _mapper = mapper;
                _doctorScheduleRepository = doctorScheduleRepository;
                _doctorScheduleBusinessRules = doctorScheduleBusinessRules;
                _appointmentRepository = appointmentRepository;
            }

            public async Task<UpdatedDoctorScheduleResponse> Handle(UpdateDoctorScheduleCommand request, CancellationToken cancellationToken)
            {
                // Ýlk olarak güncellemek istediðimiz mevcut kaydý alalým
                var existingSchedule = await _doctorScheduleRepository.GetAsync(ds => ds.Id == request.Id && ds.DeletedDate==null);
                if (existingSchedule == null)
                {
                    throw new BusinessException("Doktor takviminizde böyle bir kayýt bulunmamaktadýr.");
                  
                }

                // Güncellenmek istenen tarih ve doktor ID'si ile silinmiþ bir kayýt var mý diye kontrol edelim
                var conflictingSchedule = await _doctorScheduleRepository.GetAsync(ds => ds.DoctorID == request.DoctorID && ds.Date == request.Date);

                var appointment=await _appointmentRepository.GetAsync(x=>x.DoctorID==request.DoctorID && x.Date==request.Date &&x.DeletedDate==null);
                if (appointment != null)
                {
                    throw new BusinessException("Bu tarihe ait hastalar tarafýnda alýnmýþ randevular bulunmaktadýr.Tarihi güncelleyemezsiniz");
                }
                else
                {
                    if (conflictingSchedule != null && conflictingSchedule.Id != request.Id)
                    {
                        if (conflictingSchedule.DeletedDate == null)
                        {
                            // Silinmemiþ bir kayýtta çakýþma var, hata fýrlatalým
                            throw new BusinessException("Bu doktorun belirtilen tarihteki programý zaten mevcut.");
                        }
                        else
                        {
                            // Silinmiþ bir kayýtta çakýþma var, bu kaydý güncelleyelim
                            conflictingSchedule.Date = request.Date;
                            conflictingSchedule.StartTime = request.StartTime;
                            conflictingSchedule.EndTime = request.EndTime;
                            conflictingSchedule.UpdatedDate = null;
                            conflictingSchedule.DeletedDate = null;
                            //_mapper.Map(request, conflictingSchedule);
                            //conflictingSchedule.DeletedDate = null; // DeletedDate'i null yap
                            await _doctorScheduleRepository.UpdateAsync(conflictingSchedule);

                            // Ýstek yapýlan kaydý silindi olarak iþaretleyelim
                            existingSchedule.DeletedDate = DateTime.UtcNow;
                            await _doctorScheduleRepository.UpdateAsync(existingSchedule);

                            // Güncellenen veriyi response olarak dönelim
                            UpdatedDoctorScheduleResponse updatedResponse = _mapper.Map<UpdatedDoctorScheduleResponse>(conflictingSchedule);
                            return updatedResponse;
                        }
                    }
                    else
                    {
                        // Çakýþan bir kayýt yoksa mevcut kaydý güncelleyelim
                        _mapper.Map(request, existingSchedule);
                        await _doctorScheduleRepository.UpdateAsync(existingSchedule);
                        // Güncellenen veriyi response olarak dönelim
                        UpdatedDoctorScheduleResponse updatedResponse = _mapper.Map<UpdatedDoctorScheduleResponse>(existingSchedule);
                        return updatedResponse;
                    }
                }
            }
        }
    }
}
