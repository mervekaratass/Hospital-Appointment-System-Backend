using Application.Features.Appointments.Queries.GetList;
using Application.Features.Patients.Constants;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using MediatR;
using NArchitecture.Core.Application.Pipelines.Authorization;
using NArchitecture.Core.Application.Pipelines.Caching;
using NArchitecture.Core.Application.Requests;
using NArchitecture.Core.Application.Responses;
using NArchitecture.Core.Persistence.Paging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Application.Features.Appointments.Rules;
using static Application.Features.Appointments.Constants.AppointmentsOperationClaims;
using Microsoft.EntityFrameworkCore;
using Application.Features.Appointments.Queries.GetListByDoctor;
using Application.Features.Appointments.Queries.GetByPatientId;
using Application.Features.Doctors.Constants;
//using Application.Services.Encryptions;

namespace Application.Features.Appointments.Queries.GetListByDoctorId;

public class GetListByDoctorQuery : IRequest<GetListResponse<GetListByDoctorDto>>

{
    public PageRequest PageRequest { get; set; }
    public Guid DoctorId { get; set; }

    public string[] Roles => [Admin, Read, DoctorsOperationClaims.Update];

    public bool BypassCache { get; }
    public string? CacheKey => $"GetListAppointments({PageRequest.PageIndex},{PageRequest.PageSize})";
    public string? CacheGroupKey => "GetAppointments";
    public TimeSpan? SlidingExpiration { get; }

    public class GetListByDoctorQueryHandler : IRequestHandler<GetListByDoctorQuery, GetListResponse<GetListByDoctorDto>>
    {
        private readonly IAppointmentRepository _appointmentRepository;
        private readonly IMapper _mapper;

        public GetListByDoctorQueryHandler(IAppointmentRepository appointmentRepository, IMapper mapper)
        {
            _appointmentRepository = appointmentRepository;
            _mapper = mapper;
        }

        public async Task<GetListResponse<GetListByDoctorDto>> Handle(
            GetListByDoctorQuery request,
            CancellationToken cancellationToken
        )
        {
            IPaginate<Appointment> appointments = await _appointmentRepository.GetListAsync(
               index: request.PageRequest.PageIndex,
               size: request.PageRequest.PageSize,
               cancellationToken: cancellationToken,
                  orderBy: x => x.OrderByDescending(y => y.Date),
               include: x => x.Include(x => x.Doctor).Include(x => x.Patient).Include(x => x.Doctor.Branch),
                  predicate: x => x.DoctorID == request.DoctorId
                  
           );

            // SİNEM Foreach ile döndurunce  Ipaginat ekleme işlemine izin vermiyor ,hata veriyor .

            //for (int i = 0; i < appointments.Items.Count; i++)
            //{
            //    appointments.Items[i].Patient.FirstName = CryptoHelper.Decrypt(appointments.Items[i].Patient.FirstName);
            //    appointments.Items[i].Patient.LastName = CryptoHelper.Decrypt(appointments.Items[i].Patient.LastName);
            //    appointments.Items[i].Patient.NationalIdentity = CryptoHelper.Decrypt(appointments.Items[i].Patient.NationalIdentity);
            //    appointments.Items[i].Patient.Phone = CryptoHelper.Decrypt(appointments.Items[i].Patient.Phone);
            //    appointments.Items[i].Patient.Address = CryptoHelper.Decrypt(appointments.Items[i].Patient.Address);
            //}
          // yaptığım bitti



            GetListResponse<GetListByDoctorDto> patiens = _mapper.Map<GetListResponse<GetListByDoctorDto>>(appointments);

            //SİNEM doktorun hastalarını yazdırıken, bir sefer hastayı yazdırması için

            GetListResponse<GetListByDoctorDto> patiensDistinct = new GetListResponse<GetListByDoctorDto>  
            {
                Count = patiens.Count,
                HasNext = patiens.HasNext,
                HasPrevious = patiens.HasPrevious,
                Index = patiens.Index,
                Pages = patiens.Pages,
                Size = patiens.Size,
                Items=new List<GetListByDoctorDto>()
            };

            foreach (var item in patiens.Items)
            {
                if (!patiensDistinct.Items.Any(x=>x.PatientID == item.PatientID))
                {
                    patiensDistinct.Items.Add(item);
                }
            }
            return patiensDistinct;

        }
    }
}