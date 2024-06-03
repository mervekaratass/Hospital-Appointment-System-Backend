using Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.EntityConfigurations;
public class DoctorConfiguration : IEntityTypeConfiguration<Doctor>
{

    public void Configure(EntityTypeBuilder<Doctor> builder)
    {

        builder.ToTable("Doctors");

        builder.Property(d => d.Id).HasColumnName("Id").IsRequired();
        builder.Property(d => d.Title).HasColumnName("Title").IsRequired();
        builder.Property(d => d.SchoolName).HasColumnName("SchoolName").IsRequired();
        builder.Property(d => d.CreatedDate).HasColumnName("CreatedDate").IsRequired();
        builder.Property(d => d.UpdatedDate).HasColumnName("UpdatedDate");
        builder.Property(d => d.DeletedDate).HasColumnName("DeletedDate");

        //builder.HasQueryFilter(d => !d.DeletedDate.HasValue);  --SİLİNECEK!!
       
        builder.HasOne<User>()
                    .WithOne()
                    .HasForeignKey<Doctor>(d => d.Id)
                    .OnDelete(DeleteBehavior.Cascade);
    }
}

