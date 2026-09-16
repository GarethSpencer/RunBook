namespace Utilities.Models.Requests;

public record UpdateRecordingRequest
{
    public required DateOnly Date { get; set; }
    public required bool RecordedValue { get; set; }
}
