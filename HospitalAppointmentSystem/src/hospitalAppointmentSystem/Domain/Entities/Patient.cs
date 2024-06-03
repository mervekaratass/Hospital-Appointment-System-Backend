
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities;
public class Patient : User
{
    public Patient()
    {
    }

    public Patient(int age, float height, float weight)
    {

        Age = age;
        Height = height;
        Weight = weight;
    }

    public int Age { get; set; }
    public float Height { get; set; }
    public float Weight { get; set; }

    public virtual ICollection<Appointment> Appointments { get; set; } = new HashSet<Appointment>();



}
