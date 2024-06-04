using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.EntityConfigurations;

public class DoctorConfiguration : IEntityTypeConfiguration<Doctor>
{
    public void Configure(EntityTypeBuilder<Doctor> builder)
    {
        builder.ToTable("Doctors");

        builder.Property(d => d.Id).HasColumnName("Id").IsRequired();
        builder.Property(d => d.Title).HasColumnName("Title").IsRequired();
        builder.Property(d => d.SchoolName).HasColumnName("SchoolName").IsRequired();
        builder.Property(d => d.BranchID).HasColumnName("BranchID").IsRequired();
        builder.Property(d => d.CreatedDate).HasColumnName("CreatedDate").IsRequired();
        builder.Property(d => d.UpdatedDate).HasColumnName("UpdatedDate");
        builder.Property(d => d.DeletedDate).HasColumnName("DeletedDate");

        builder.HasOne<User>()
                .WithOne()
                .HasForeignKey<Doctor>(d => d.Id)
                .OnDelete(DeleteBehavior.Cascade);


        builder.HasOne(a => a.Branch)
                  .WithMany(d => d.Doctors)
                  .HasForeignKey(a => a.BranchID)
                  .OnDelete(DeleteBehavior.Restrict); 

    }
}