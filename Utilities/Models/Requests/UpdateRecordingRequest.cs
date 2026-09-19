namespace Utilities.Models.Requests;

public record UpdateRecordingRequest<TValue>
{
    public required TValue RecordedValue { get; set; }
}
