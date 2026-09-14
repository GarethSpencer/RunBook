using DAL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DAL.Data.Configuration;

public class TrackedExerciseConfiguration : IEntityTypeConfiguration<TrackedExercise>
{
    public void Configure(EntityTypeBuilder<TrackedExercise> builder)
    {
        builder.HasOne(co => co.User)
            .WithMany(g => g.TrackedExercises)
            .HasForeignKey(co => co.UserId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(co => co.ExerciseType)
            .WithMany(u => u.TrackedExercises)
            .HasForeignKey(co => co.ExerciseTypeId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
