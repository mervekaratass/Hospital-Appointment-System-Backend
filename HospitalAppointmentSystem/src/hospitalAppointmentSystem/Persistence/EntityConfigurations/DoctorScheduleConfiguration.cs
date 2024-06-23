using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Reflection.Emit;

namespace Persistence.EntityConfigurations;

public class DoctorScheduleConfiguration : IEntityTypeConfiguration<DoctorSchedule>
{
    public void Configure(EntityTypeBuilder<DoctorSchedule> builder)
    {

        builder.ToTable("DoctorSchedules");

        builder.HasIndex(ds => new { ds.DoctorID, ds.Date }).IsUnique();

        builder.Property(d => d.Id).HasColumnName("Id").IsRequired();
        builder.Property(d => d.Date).HasColumnName("Date").IsRequired();
        builder.Property(d => d.StartTime).HasColumnName("StartTime").IsRequired();
        builder.Property(d => d.EndTime).HasColumnName("EndTime").IsRequired();
        builder.Property(d => d.DoctorID).HasColumnName("DoctorID").IsRequired();

        builder.Property(d => d.CreatedDate).HasColumnName("CreatedDate").IsRequired();
        builder.Property(d => d.UpdatedDate).HasColumnName("UpdatedDate");
        builder.Property(d => d.DeletedDate).HasColumnName("DeletedDate");

        builder.HasOne(a => a.Doctor)
                .WithMany(d => d.DoctorSchedules)
                .HasForeignKey(a => a.DoctorID)
                .OnDelete(DeleteBehavior.Cascade);
    }
}