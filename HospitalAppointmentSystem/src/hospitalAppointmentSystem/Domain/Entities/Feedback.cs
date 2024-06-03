using NArchitecture.Core.Persistence.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities;
public class Feedback : Entity<int>
{
    public Feedback()
    {
    }

    public Feedback(int id, string text)
    {
        Id = id;

        Text = text;
    }


    public string Text { get; set; }

    public virtual User? User { get; set; }
}

