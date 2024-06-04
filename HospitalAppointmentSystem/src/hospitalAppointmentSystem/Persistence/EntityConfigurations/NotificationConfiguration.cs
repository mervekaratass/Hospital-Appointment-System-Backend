using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.EntityConfigurations;

public class NotificationConfiguration : IEntityTypeConfiguration<Notification>
{
    public void Configure(EntityTypeBuilder<Notification> builder)
    {
        builder.ToTable("Notifications");

        builder.Property(d => d.Id).HasColumnName("Id").IsRequired();
        builder.Property(d => d.AppointmentID).HasColumnName("AppointmentID").IsRequired();
        builder.Property(d => d.Message).HasColumnName("Message").IsRequired();
        builder.Property(d => d.EmailStatus).HasColumnName("EmailStatus").HasDefaultValue(0);
        builder.Property(d => d.SmsStatus).HasColumnName("SmsStatus").HasDefaultValue(0);
        builder.Property(d => d.CreatedDate).HasColumnName("CreatedDate").IsRequired();
        builder.Property(d => d.UpdatedDate).HasColumnName("UpdatedDate");
        builder.Property(d => d.DeletedDate).HasColumnName("DeletedDate");


        builder.HasOne(a => a.Appointment)
            .WithMany(d => d.Notifications)
            .HasForeignKey(a => a.AppointmentID)
            .OnDelete(DeleteBehavior.Cascade); 
    }
}