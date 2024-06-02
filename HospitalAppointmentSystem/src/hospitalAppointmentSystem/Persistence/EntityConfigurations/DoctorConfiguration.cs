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
        builder.HasOne<User>()
                    .WithOne()
                    .HasForeignKey<Doctor>(d => d.Id)
                    .OnDelete(DeleteBehavior.Cascade);
    }
}

