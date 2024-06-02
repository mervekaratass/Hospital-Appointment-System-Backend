using Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.EntityConfigurations;
public class AppointmentConfiguration : IEntityTypeConfiguration<Appointment>
{

    public void Configure(EntityTypeBuilder<Appointment> builder)
    {
        builder.HasOne(a => a.Doctor)
              .WithMany(d => d.Appointments)
              .HasForeignKey(a => a.Id)
              .OnDelete(DeleteBehavior.Restrict); // Cascade yerine Restrict veya NoAction kullanıyoruz

        builder.HasOne(a => a.Patient)
               .WithMany(p => p.Appointments)
               .HasForeignKey(a => a.Id)
               .OnDelete(DeleteBehavior.Restrict); // Cascade yerine Restrict veya NoAction kullanıyoruz
    }
}

