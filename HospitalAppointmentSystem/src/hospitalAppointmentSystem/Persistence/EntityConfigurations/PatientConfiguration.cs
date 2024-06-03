using Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.EntityConfigurations;
public class PatientConfiguration : IEntityTypeConfiguration<Patient>
{

    public void Configure(EntityTypeBuilder<Patient> builder)
    {
        builder.ToTable("Patients");

        builder.Property(p => p.Id).HasColumnName("Id").IsRequired();
        builder.Property(p => p.Age).HasColumnName("Age").IsRequired();
        builder.Property(p => p.Height).HasColumnName("Height").IsRequired();
        builder.Property(p => p.Weight).HasColumnName("Weight").IsRequired();
        builder.Property(p => p.CreatedDate).HasColumnName("CreatedDate").IsRequired();
        builder.Property(p => p.UpdatedDate).HasColumnName("UpdatedDate");
        builder.Property(p => p.DeletedDate).HasColumnName("DeletedDate");

        builder.HasOne<User>()
                    .WithOne()
                    .HasForeignKey<Patient>(d => d.Id)
                    .OnDelete(DeleteBehavior.Cascade);
    }
}