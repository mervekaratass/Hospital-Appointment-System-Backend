using Quartz;
using System;
using System.Linq;
using System.Threading.Tasks;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using MailKit.Security;
using MimeKit;
using MailKit.Net.Smtp;
using NArchitecture.Core.Persistence.Paging;
using Microsoft.EntityFrameworkCore;
using NArchitecture.Core.Application.Responses;
using Application.Features.Appointments.Queries.GetList;
using NArchitecture.Core.Application.Requests;
using Application.Services.Encryptions;

public class ReminderAppointmentJob : IJob
{
    private readonly IAppointmentRepository _appointmentRepository;
    private readonly IMapper _mapper;

    public ReminderAppointmentJob(IAppointmentRepository appointmentRepository, IMapper mapper)
    {
        _appointmentRepository = appointmentRepository;
        _mapper = mapper;
    }

    public async Task Execute(IJobExecutionContext context)
    {
        await KontrolEtVeMailGonder();
    }

    private async Task KontrolEtVeMailGonder()
    {
        var pageRequest = new PageRequest { PageIndex = 0, PageSize = 100 };
        IPaginate<Appointment> appointments = await _appointmentRepository.GetListAsync(
             predicate: x => x.DeletedDate == null
                             && x.Date == DateOnly.FromDateTime(DateTime.Today.AddDays(1)), // Yarınki randevular için
             index: pageRequest.PageIndex,
             size: pageRequest.PageSize,
             orderBy: x => x.OrderByDescending(y => y.Date),
             include: x => x.Include(x => x.Doctor).Include(x => x.Patient).Include(x => x.Doctor.Branch)
         );

        foreach (var appointment in appointments.Items)
        {
            await MailGonder(appointment);
        }
    }

    private async Task MailGonder(Appointment appointment)
    {
        var randevu = _mapper.Map<GetListAppointmentListItemDto>(appointment);

        randevu.DoctorFirstName = CryptoHelper.Decrypt(randevu.DoctorFirstName);
        randevu.DoctorLastName = CryptoHelper.Decrypt(randevu.DoctorLastName);
        randevu.PatientFirstName = CryptoHelper.Decrypt(randevu.PatientFirstName);
        randevu.PatientLastName = CryptoHelper.Decrypt(randevu.PatientLastName);


        var mailMessage = new MimeMessage();
        mailMessage.From.Add(new MailboxAddress("Pair 5 Hastanesi", "fatmabireltr@gmail.com")); // Gönderen bilgisi
        mailMessage.To.Add(new MailboxAddress("Pair 5 Hastanesi", "fatmabrl11@gmail.com")); // Alıcı bilgisi 
        mailMessage.Subject = "Randevu Bilgilendirme"; // Mail konusu

        var bodyBuilder = new BodyBuilder
        {
            HtmlBody = $@"
        <html>
            <head>
                <style>
                    body {{ font-family: Arial, sans-serif; }}
                    .container {{ border: 1px solid red; padding: 10px; }}
                </style>
            </head>
            <body>
                <div class='container'>
                <p>Sayın {randevu.PatientFirstName} {randevu.PatientLastName},</p>
                <p>{randevu.Date} tarihinde, saat {randevu.Time} için bir randevunuz bulunmaktadır. Randevunuza gelemeyecekseniz iptal etmenizi rica ederiz.</p>
                <p>Doktor:{randevu.DoctorTitle} {randevu.DoctorFirstName} {randevu.DoctorLastName}</p>
                <p>Branş: {randevu.BranchName}</p>
            </div>
            </body>
        </html>"
        };

        mailMessage.Body = bodyBuilder.ToMessageBody();

        try
        {
            using (var smtp = new SmtpClient())
            {
                await smtp.ConnectAsync("smtp.gmail.com", 587, SecureSocketOptions.StartTls);
                await smtp.AuthenticateAsync("fatmabireltr@gmail.com", "rxuv hpfv wlqq htpa");
                await smtp.SendAsync(mailMessage);
                await smtp.DisconnectAsync(true);
            }
        }
        catch (Exception ex)
        {
            // Hata yönetimi
            Console.WriteLine($"Mail gönderme hatası: {ex.Message}");
        }
    }
}
