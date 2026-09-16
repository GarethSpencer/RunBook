namespace Utilities.Models.Requests;

public record CreateRecordingRequest
{
    public required DateOnly Date { get; set; }
    public required bool RecordedValue { get; set; }
}
