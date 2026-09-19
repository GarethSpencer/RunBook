namespace DAL.Entities.Abstractions;

public interface IRecording<TValue>
{
    public int RecordingId { get; set; }
    public Guid UserId { get; set; }
    public DateOnly Date { get; set; }
    public TValue RecordedValue { get; set; }
}
