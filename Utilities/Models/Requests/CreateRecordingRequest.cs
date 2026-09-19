namespace Utilities.Models.Requests;

public record CreateRecordingRequest<TValue>
{
    public required DateOnly Date { get; set; }
    public required TValue RecordedValue { get; set; }
}
