using NArchitecture.Core.Application.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Appointments.Queries.GetByPatientId;
public class GetListByPatientDto:IDto
{

    public int Id { get; set; }
    public DateOnly Date { get; set; }
    public TimeOnly Time { get; set; }
    public bool Status { get; set; }

    public Guid DoctorID { get; set; }
    public string DoctorFirstName { get; set; }
    public string DoctorLastName { get; set; }
    public string DoctorTitle { get; set; }

    public Guid PatientID { get; set; }
    public string PatientFirstName { get; set; } //burda gerekli konfigürasyonu yap
    public string PatientLastName { get; set; }

}