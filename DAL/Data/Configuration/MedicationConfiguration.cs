using DAL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DAL.Data.Configuration;

public class MedicationConfiguration : IEntityTypeConfiguration<Medication>
{
    public void Configure(EntityTypeBuilder<Medication> builder)
    {
        builder.HasOne(co => co.User)
            .WithMany(g => g.Medications)
            .HasForeignKey(co => co.UserId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
