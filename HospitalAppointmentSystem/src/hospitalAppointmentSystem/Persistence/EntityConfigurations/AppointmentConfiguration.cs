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
        builder.ToTable("Appointments");

        builder.Property(d => d.Id).HasColumnName("Id").IsRequired();
        builder.Property(d=>d.Date).HasColumnName("Date").IsRequired();
        builder.Property(d => d.Time).HasColumnName("Time").IsRequired();
        builder.Property(d => d.DoctorID).HasColumnName("DoctorID").IsRequired();
        builder.Property(d => d.PatientID).HasColumnName("PatientID").IsRequired();
        builder.Property(d => d.CreatedDate).HasColumnName("CreatedDate").IsRequired();
        builder.Property(d => d.UpdatedDate).HasColumnName("UpdatedDate");
        builder.Property(d => d.DeletedDate).HasColumnName("DeletedDate");


        builder.HasOne(a => a.Doctor)
              .WithMany(d => d.Appointments)
              .HasForeignKey(a => a.DoctorID)
              .OnDelete(DeleteBehavior.Restrict); // Cascade yerine Restrict veya NoAction kullanıyoruz

        builder.HasOne(a => a.Patient)
               .WithMany(p => p.Appointments)
               .HasForeignKey(a => a.PatientID)
               .OnDelete(DeleteBehavior.Restrict); // Cascade yerine Restrict veya NoAction kullanıyoruz


        builder.HasOne(a => a.Reports)
        .WithOne(r => r.Appointment)
        .HasForeignKey<Report>(r => r.AppointmentID)
        .OnDelete(DeleteBehavior.Cascade);
    }
}

