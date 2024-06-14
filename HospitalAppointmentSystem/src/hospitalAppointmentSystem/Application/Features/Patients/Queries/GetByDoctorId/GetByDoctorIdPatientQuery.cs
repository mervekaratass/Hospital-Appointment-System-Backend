//using Application.Features.Appointments.Queries.GetList;
//using Application.Features.Patients.Constants;
//using Application.Features.Patients.Queries.GetById;
//using Application.Features.Patients.Rules;
//using Application.Services.Repositories;
//using AutoMapper;
//using Domain.Entities;
//using MediatR;
//using Microsoft.EntityFrameworkCore;
//using NArchitecture.Core.Application.Pipelines.Authorization;
//using NArchitecture.Core.Application.Responses;
//using NArchitecture.Core.Persistence.Paging;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace Application.Features.Patients.Queries.GetByDoctorId;
//public class GetByDoctorIdPatientQuery : IRequest<GetByDoctorIdPatientResponse>, ISecuredRequest
//{
//    public Guid Id { get; set; }

//    public string[] Roles => [Admin, Read, PatientsOperationClaims.Update];

//    public class GetByDoctorIdPatientQueryHandler : IRequestHandler<GetByDoctorIdPatientQuery, GetByDoctorIdPatientResponse>
//    {
//        private readonly IMapper _mapper;
//        private readonly IPatientRepository _patientRepository;
//        private readonly PatientBusinessRules _patientBusinessRules;

//        public GetByDoctorIdPatientQueryHandler(IMapper mapper, IPatientRepository patientRepository, PatientBusinessRules patientBusinessRules)
//        {
//            _mapper = mapper;
//            _patientRepository = patientRepository;
//            _patientBusinessRules = patientBusinessRules;
//        }

//        //public async Task<List<GetByDoctorIdPatientResponse>> Handle(GetByDoctorIdPatientQuery request, CancellationToken cancellationToken)
//        //{
//        //    var patients = await _patientRepository.GetListAsync(predicate: p => p.Id == request.Id, cancellationToken: cancellationToken);
//        //    await _patientBusinessRules.PatientShouldExistWhenSelected(patients);

//        //    List<GetByDoctorIdPatientResponse> responseList = _mapper.Map<List<GetByDoctorIdPatientResponse>>(patients.Items);
//        //    return responseList;
//        //}

//        public async Task<GetListResponse<GetByDoctorIdPatientResponse>> Handle(GetListAppointmentQuery request, CancellationToken cancellationToken)
//        {
//            IPaginate<Patient> appointments = await _patientRepository.GetListAsync(
//                index: request.PageRequest.PageIndex,
//                size: request.PageRequest.PageSize,
//                cancellationToken: cancellationToken,
//                   orderBy: x => x.OrderByDescending(y => y.Age),
//                include: x => x.Include(x => x.).Include(x => x.Patient).Include(x => x.Doctor.Branch)
//            );

//            GetListResponse<GetListAppointmentListItemDto> response = _mapper.Map<GetListResponse<GetListAppointmentListItemDto>>(appointments);
//            return response;
//        }
//    }
//}
