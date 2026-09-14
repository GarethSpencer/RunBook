using DAL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DAL.Data.Configuration;

public class StepsConfiguration : IEntityTypeConfiguration<Steps>
{
    public void Configure(EntityTypeBuilder<Steps> builder)
    {
        builder.HasOne(co => co.User)
            .WithMany(g => g.Steps)
            .HasForeignKey(co => co.UserId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
