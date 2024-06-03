using NArchitecture.Core.Persistence.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities;
public class Report : Entity<int>
{
    public Report()
    {
    }

    public Report(int id, string text)
    {
        Id = id;
        Text = text;
    }


    public string Text { get; set; }

    public virtual Appointment? Appointment { get; set; }
}