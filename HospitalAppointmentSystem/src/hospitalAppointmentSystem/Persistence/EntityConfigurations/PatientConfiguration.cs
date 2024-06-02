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
        builder.HasOne<User>()
                    .WithOne()
                    .HasForeignKey<Patient>(d => d.Id)
                    .OnDelete(DeleteBehavior.Cascade);
    }
}