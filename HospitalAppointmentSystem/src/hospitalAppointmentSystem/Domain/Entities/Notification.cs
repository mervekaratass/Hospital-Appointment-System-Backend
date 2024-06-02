using NArchitecture.Core.Persistence.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities;
public class Notification : Entity<Guid>
{
    public Notification()
    {
    }

    public Notification(Guid id, string message, bool emailStatus, bool smsStatus)
    {
        Id = id;

        Message = message;
        EmailStatus = emailStatus;
        SmsStatus = smsStatus;
    }


    public string Message { get; set; }
    public bool EmailStatus { get; set; }
    public bool SmsStatus { get; set; }

    public virtual Appointment Appointment { get; set; }

}

