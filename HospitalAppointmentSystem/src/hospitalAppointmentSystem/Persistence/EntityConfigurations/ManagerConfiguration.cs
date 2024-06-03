using Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.EntityConfigurations;
public class ManagerConfiguration : IEntityTypeConfiguration<Manager>
{

    public void Configure(EntityTypeBuilder<Manager> builder)
    {


        builder.ToTable("Manager");

        builder.Property(p => p.Id).HasColumnName("Id").IsRequired();
        builder.Property(p => p.CreatedDate).HasColumnName("CreatedDate").IsRequired();
        builder.Property(p => p.UpdatedDate).HasColumnName("UpdatedDate");
        builder.Property(p => p.DeletedDate).HasColumnName("DeletedDate");
        builder.HasOne<User>()
                    .WithOne()
                    .HasForeignKey<Manager>(d => d.Id)
                    .OnDelete(DeleteBehavior.Cascade);
    }
}