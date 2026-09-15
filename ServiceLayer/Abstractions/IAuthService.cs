namespace ServiceLayer.Abstractions;

public interface IAuthService
{
    Task<Guid?> ResolveUserAsync(Guid authId, string PreferredName, CancellationToken ct);
}
