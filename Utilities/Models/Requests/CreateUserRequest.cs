namespace Utilities.Models.Requests;

public record CreateUserRequest
{
    public required Guid AuthId { get; set; }
    public required string PreferredName { get; set; }
}
