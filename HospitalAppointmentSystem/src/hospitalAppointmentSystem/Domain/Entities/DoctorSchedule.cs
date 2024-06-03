using NArchitecture.Core.Persistence.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities;
public class DoctorSchedule : Entity<int>
{
    public DoctorSchedule()
    {
    }

    public DoctorSchedule(int id,Guid doctorID, DateOnly date, TimeOnly startTime, TimeOnly endTime)
    {
        Id = id;
        DoctorID = doctorID;
        Date = date;
        StartTime = startTime;
        EndTime = endTime;
    }

    public Guid DoctorID { get; set; }
    public DateOnly Date { get; set; }
    public TimeOnly StartTime { get; set; }
    public TimeOnly EndTime { get; set; }

    public virtual Doctor? Doctor { get; set; }
}
