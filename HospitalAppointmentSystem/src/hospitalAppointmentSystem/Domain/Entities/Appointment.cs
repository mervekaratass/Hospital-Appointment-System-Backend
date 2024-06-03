using NArchitecture.Core.Persistence.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities;
public class Appointment : Entity<int>
{
    public Appointment()
    {
    }

    public Appointment(int id,DateOnly date, TimeOnly time, bool status, Guid doctorID, Guid patientID)
    {
        Id = id;
        Date = date;
        Time = time;
        Status = status;
        DoctorID = doctorID;
        PatientID = patientID;
    }

    public DateOnly Date { get; set; }
    public TimeOnly Time { get; set; }
    public bool Status { get; set; }
    public Guid DoctorID { get; set; }
    public Guid PatientID { get; set; }


    public virtual Doctor? Doctor { get; set; }
    public virtual Patient? Patient { get; set; }
    public virtual Report? Reports { get; set; }
    public virtual ICollection<Notification> Notifications { get; set; } = new HashSet<Notification>();

}

