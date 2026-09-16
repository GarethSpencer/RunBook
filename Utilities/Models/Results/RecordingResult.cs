namespace Utilities.Models.Results;

public record RecordingResult
{
    public required int RecordingId { get; init; }
    public required DateOnly Date { get; init; }
    public required bool RecordedValue { get; init; }
}
