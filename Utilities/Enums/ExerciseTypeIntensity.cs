using System.Text.Json.Serialization;

namespace Utilities.Enums;

[JsonConverter(typeof(JsonStringEnumConverter))]
public enum ExerciseTypeIntensity
{
    Easy = 0,
    Moderate = 1,
    Vigorous = 2,
}
