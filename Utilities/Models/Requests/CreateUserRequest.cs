namespace Utilities.Models.Requests;

public record CreateUserRequest
{
    public required string DisplayName { get; set; }
    public required string Email { get; set; }
    public required bool Admin { get; set; }
}
