using DAL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using static Utilities.Enums.ExerciseTypeIntensity;

namespace DAL.Data.Configuration;

public class ExerciseTypeConfiguration : IEntityTypeConfiguration<ExerciseType>
{
    public void Configure(EntityTypeBuilder<ExerciseType> builder)
    {
        builder.Property(et => et.Name).HasMaxLength(60);

        builder.HasData(
            new ExerciseType { ExerciseTypeId = 1, Name = "Walking", Intensity = Easy, CaloriesPerMinute = 5 },
            new ExerciseType { ExerciseTypeId = 2, Name = "Jogging", Intensity = Moderate, CaloriesPerMinute = 8 },
            new ExerciseType { ExerciseTypeId = 3, Name = "Running", Intensity = Vigorous, CaloriesPerMinute = 12 },
            new ExerciseType { ExerciseTypeId = 4, Name = "Rowing Machine", Intensity = Moderate, CaloriesPerMinute = 7 },
            new ExerciseType { ExerciseTypeId = 5, Name = "Rowing Machine", Intensity = Vigorous, CaloriesPerMinute = 11 },
            new ExerciseType { ExerciseTypeId = 6, Name = "Cross Trainer", Intensity = Moderate, CaloriesPerMinute = 8 },
            new ExerciseType { ExerciseTypeId = 7, Name = "Stationary Bike", Intensity = Moderate, CaloriesPerMinute = 7 },
            new ExerciseType { ExerciseTypeId = 8, Name = "Stationary Bike", Intensity = Vigorous, CaloriesPerMinute = 11 },
            new ExerciseType { ExerciseTypeId = 9, Name = "Weight Training", Intensity = Moderate, CaloriesPerMinute = 4 },
            new ExerciseType { ExerciseTypeId = 10, Name = "Weight Training", Intensity = Vigorous, CaloriesPerMinute = 7 },
            new ExerciseType { ExerciseTypeId = 11, Name = "Swimming", Intensity = Moderate, CaloriesPerMinute = 8 }
        );
    }
}
