using DAL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DAL.Data.Configuration;

public class DailyExerciseConfiguration : IEntityTypeConfiguration<DailyExercise>
{
    public void Configure(EntityTypeBuilder<DailyExercise> builder)
    {
        builder.HasOne(co => co.User)
            .WithMany(g => g.DailyExercises)
            .HasForeignKey(co => co.UserId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
