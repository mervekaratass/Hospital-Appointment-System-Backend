
using NArchitecture.Core.Persistence.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities;
public class Doctor :User
{
    public Doctor()
    {
    }

    public Doctor(Guid id,string title, string schoolName,int branchID)
    {
        Id= id;
        BranchID = branchID;
        Title = title;
        SchoolName = schoolName;
    }



    public string Title { get; set; }
    public string SchoolName { get; set; }
    public int BranchID { get; set; }
    public virtual Branch? Branch { get; set; }

    public virtual ICollection<DoctorSchedule> DoctorSchedules { get; set; } = new HashSet<DoctorSchedule>();

    public virtual ICollection<Appointment> Appointments { get; set; } = new HashSet<Appointment>();
}
