namespace Utilities.Models.Results;

public record RecordingResult<TValue>
{
    public required int RecordingId { get; init; }
    public required DateOnly Date { get; init; }
    public required TValue RecordedValue { get; init; }
}
