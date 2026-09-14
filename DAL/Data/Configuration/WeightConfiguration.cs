using DAL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DAL.Data.Configuration;

public class WeightConfiguration : IEntityTypeConfiguration<Weight>
{
    public void Configure(EntityTypeBuilder<Weight> builder)
    {
        builder.Property(u => u.RecordedWeight).HasPrecision(6, 2);

        builder.HasOne(co => co.User)
            .WithMany(g => g.Weights)
            .HasForeignKey(co => co.UserId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
