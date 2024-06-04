using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.EntityConfigurations;

public class ReportConfiguration : IEntityTypeConfiguration<Report>
{
    public void Configure(EntityTypeBuilder<Report> builder)
    {
        builder.ToTable("Reports");

        builder.Property(d => d.Id).HasColumnName("Id").IsRequired();
        builder.Property(d => d.AppointmentID).HasColumnName("AppointmentID").IsRequired();
        builder.Property(d => d.Text).HasColumnName("Text").IsRequired();

        builder.Property(d => d.CreatedDate).HasColumnName("CreatedDate").IsRequired();
        builder.Property(d => d.UpdatedDate).HasColumnName("UpdatedDate");
        builder.Property(d => d.DeletedDate).HasColumnName("DeletedDate");

        builder.HasOne(r => r.Appointment)
            .WithOne(a => a.Reports)
            .HasForeignKey<Report>(r => r.AppointmentID)
            .OnDelete(DeleteBehavior.Cascade);
    }
}