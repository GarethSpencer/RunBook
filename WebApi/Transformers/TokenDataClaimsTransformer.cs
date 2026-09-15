using Microsoft.AspNetCore.Authentication;
using System.Security.Claims;
using Utilities.Models.Token;

namespace WebApi.Transformers;

public class TokenDataClaimsTransformer(ITokenData tokenData) : IClaimsTransformation
{
    private readonly ITokenData _tokenData = tokenData;

    public Task<ClaimsPrincipal> TransformAsync(ClaimsPrincipal principal)
    {
        if (principal.Identity?.IsAuthenticated == true)
        {
            _tokenData.AuthId = Guid.Parse(principal.FindFirstValue("oid")!);
            _tokenData.PreferredName = principal.FindFirstValue("preferred_username")!;
            _tokenData.Roles = [.. principal.FindAll("roles").Select(c => c.Value)];
            _tokenData.IsAdmin = _tokenData.Roles.Contains("Admin");
        }

        return Task.FromResult(principal);
    }
}
