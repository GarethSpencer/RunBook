namespace Utilities.Models.Token;

public interface ITokenData
{
    Guid? UserId { get; set; }
    Guid AuthId { get; set; }
    string PreferredName { get; set; }
    IEnumerable<string> Roles { get; set; }
    bool IsAdmin { get; set; }
}
