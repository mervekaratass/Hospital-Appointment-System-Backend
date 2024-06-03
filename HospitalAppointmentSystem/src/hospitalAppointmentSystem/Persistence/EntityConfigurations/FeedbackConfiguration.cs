
using Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.EntityConfigurations;
public class FeedbackConfiguration : IEntityTypeConfiguration<Feedback>
{

    public void Configure(EntityTypeBuilder<Feedback> builder)
    {
        builder.ToTable("Feedbacks");

        builder.Property(d => d.Id).HasColumnName("Id").IsRequired();
        builder.Property(d => d.UserID).HasColumnName("UserID").IsRequired();
      
        builder.Property(d => d.CreatedDate).HasColumnName("CreatedDate").IsRequired();
        builder.Property(d => d.UpdatedDate).HasColumnName("UpdatedDate");
        builder.Property(d => d.DeletedDate).HasColumnName("DeletedDate");

        builder.HasOne(a => a.User)
                .WithMany(d => d.Feedbacks)
                .HasForeignKey(a => a.UserID)
                .OnDelete(DeleteBehavior.Cascade);

    }
}


