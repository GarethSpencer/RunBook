using DAL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DAL.Data.Configuration;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.Property(u => u.PreferredName).HasMaxLength(30);
        builder.Property(u => u.Admin).HasDefaultValue(false);
        builder.Property(u => u.Active).HasDefaultValue(true).HasSentinel(false);
        builder.Property(u => u.PrefersKg).HasDefaultValue(true).HasSentinel(false);
        builder.Property(u => u.DailyStepGoal).HasDefaultValue(10000);
        builder.Property(u => u.WeeklyExerciseGoal).HasDefaultValue(3);
        builder.Property(u => u.TargetWeight).HasPrecision(6, 2);

        builder.HasIndex(u => u.AuthId).HasDatabaseName("IX_User_AuthId").IsUnique();
    }
}
