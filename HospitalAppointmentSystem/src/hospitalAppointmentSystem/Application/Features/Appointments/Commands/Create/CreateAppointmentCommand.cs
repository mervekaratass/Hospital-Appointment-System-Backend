using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using MailKit.Net.Smtp;
using MailKit.Security;
using MediatR;
using MimeKit;

namespace Application.Features.Appointments.Commands.Create
{
    public class CreateAppointmentCommand : IRequest<CreatedAppointmentResponse>
    {
        public DateOnly Date { get; set; }
        public TimeOnly Time { get; set; }
        public bool Status { get; set; }
        public Guid DoctorID { get; set; }
        public Guid PatientID { get; set; }

    }

    public class CreateAppointmentCommandHandler : IRequestHandler<CreateAppointmentCommand, CreatedAppointmentResponse>
    {
        private readonly IMapper _mapper;
        private readonly IAppointmentRepository _appointmentRepository;
        private readonly IDoctorRepository _doctorRepository;
        private readonly IPatientRepository _patientRepository;
        private readonly IBranchRepository _branchRepository;

        public CreateAppointmentCommandHandler(IMapper mapper, IAppointmentRepository appointmentRepository, IDoctorRepository doctorRepository, IPatientRepository patientRepository, IBranchRepository branchRepository)
        {
            _mapper = mapper;
            _appointmentRepository = appointmentRepository;
            _doctorRepository = doctorRepository;
            _patientRepository = patientRepository;
            _branchRepository = branchRepository;
        }

        public async Task<CreatedAppointmentResponse> Handle(CreateAppointmentCommand request, CancellationToken cancellationToken)
        {
            Appointment appointment = _mapper.Map<Appointment>(request);

            // Doctor bilgisini al
            Doctor doctor = await _doctorRepository.GetAsync(d => d.Id == request.DoctorID);
            appointment.Doctor = doctor;

            // Patient bilgisini al
            Patient patient = await _patientRepository.GetAsync(p => p.Id == request.PatientID);
            appointment.Patient = patient;

            //Branþ bilgisini al
            Branch branch = await _branchRepository.GetAsync(p => p.Id == doctor.BranchID);
            doctor.Branch = branch;

            await _appointmentRepository.AddAsync(appointment);

            // Oluþturulan randevu bilgilerini mail olarak gönder
            await SendAppointmentConfirmationMail(appointment);

            CreatedAppointmentResponse response = _mapper.Map<CreatedAppointmentResponse>(appointment);
            return response;
        }

        private async Task SendAppointmentConfirmationMail(Appointment appointment)
        {
            // Mail içeriðini hazýrla
            var mailMessage = new MimeMessage();
            mailMessage.From.Add(new MailboxAddress("Pair 5 Hastanesi", "fatmabireltr@gmail.com")); // Gönderen bilgisi
            mailMessage.To.Add(new MailboxAddress("Pair 5 Hastanesi", appointment.Patient.Email)); // Alýcý bilgisi 
            mailMessage.Subject = "Randevu Bilgilendirme"; // Mail konusu

            // HTML ve CSS içeriði oluþtur
            var bodyBuilder = new BodyBuilder();
            bodyBuilder.HtmlBody = $@"
       <html>
        <head>
            <style>
                body {{ font-family: Arial, sans-serif; }}
                .container {{ border: 1px solid red; padding: 10px; }}
            </style>
        </head>
        <body>
            <div class='container'>
                <p>Sayýn {appointment.Patient.FirstName} {appointment.Patient.LastName},</p>
                <p>{appointment.Date} tarihinde, saat {appointment.Time} için bir randevu aldýnýz.</p>
                <p>Doktor:{appointment.Doctor.Title} {appointment.Doctor.FirstName} {appointment.Doctor.LastName}</p>
                <p>Branþ: {appointment.Doctor.Branch.Name}</p>
            </div>
        </body>
        </html>";

            // MimeKit'e gövdeyi ayarla
            mailMessage.Body = bodyBuilder.ToMessageBody();

            // SMTP ile baðlantý kur ve maili gönder
            using (var smtp = new SmtpClient())
            {
                smtp.Connect("smtp.gmail.com", 587, SecureSocketOptions.StartTls);
                smtp.Authenticate("fatmabireltr@gmail.com", "rxuv hpfv wlqq htpa");
                await smtp.SendAsync(mailMessage);
                smtp.Disconnect(true);
            }
        }
    }
}
