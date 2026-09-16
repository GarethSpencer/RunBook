namespace DAL.Entities.Abstractions;

public interface IRecording
{
    public int RecordingId { get; set; }
    public Guid UserId { get; set; }
    public DateOnly Date { get; set; }
    public bool RecordedValue { get; set; }
}
