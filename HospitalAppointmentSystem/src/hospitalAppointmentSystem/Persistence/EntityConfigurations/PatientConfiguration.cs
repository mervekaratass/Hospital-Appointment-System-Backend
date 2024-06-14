using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.EntityConfigurations;

public class PatientConfiguration : IEntityTypeConfiguration<Patient>
{
    public void Configure(EntityTypeBuilder<Patient> builder)
    {
        builder.ToTable("Patients");
        builder.HasIndex(u => u.Email).IsUnique();
        builder.Property(p => p.Id).HasColumnName("Id").IsRequired();
        builder.Property(p => p.Age).HasColumnName("Age");
        builder.Property(p => p.Height).HasColumnName("Height");
        builder.Property(p => p.Weight).HasColumnName("Weight");
        builder.Property(p => p.BloodGroup).HasColumnName("BloodGroup");
        builder.Property(p => p.CreatedDate).HasColumnName("CreatedDate").IsRequired();
        builder.Property(p => p.UpdatedDate).HasColumnName("UpdatedDate");
        builder.Property(p => p.DeletedDate).HasColumnName("DeletedDate");

        builder.HasOne<User>()
                    .WithOne()
                    .HasForeignKey<Patient>(d => d.Id)
                    .OnDelete(DeleteBehavior.Cascade);
    }
}