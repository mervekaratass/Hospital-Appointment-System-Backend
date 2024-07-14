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

    public Feedback(int id,Guid userID, string text,bool isApproved)
    {
        Id = id;
        UserID = userID;
        Text = text;
        IsApproved = isApproved;
    }

    public Guid UserID { get; set; }
    public string Text { get; set; }
    public bool IsApproved { get; set; } = false; 
    public virtual User? User { get; set; }
}

