namespace Utilities.Models.Token;

public class TokenData : ITokenData
{
    public Guid? UserId { get; set; }
    public Guid AuthId { get; set; }
    public required string PreferredName { get; set; } = string.Empty;
    public IEnumerable<string> Roles { get; set; } = [];
    public bool IsAdmin { get; set; }
}
