
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

    public Doctor(string title, string schoolName)
    {

        Title = title;
        SchoolName = schoolName;
    }



    public string Title { get; set; }
    public string SchoolName { get; set; }

    public virtual Branch? Branch { get; set; }

    public virtual ICollection<DoctorSchedule> Schedules { get; set; } = new HashSet<DoctorSchedule>();

    public virtual ICollection<Appointment> Appointments { get; set; } = new HashSet<Appointment>();
}
