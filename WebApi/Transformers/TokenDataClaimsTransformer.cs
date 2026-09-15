using Microsoft.AspNetCore.Authentication;
using System.Security.Claims;
using Utilities.Models.Token;

namespace WebApi.Transformers;

public class TokenDataClaimsTransformer(ITokenData tokenData) : IClaimsTransformation
{
    public Task<ClaimsPrincipal> TransformAsync(ClaimsPrincipal principal)
    {
        if (principal.Identity?.IsAuthenticated == true)
        {
            tokenData.AuthId = Guid.Parse(principal.FindFirstValue("oid")!);
            tokenData.PreferredName = principal.FindFirstValue("preferred_username")!;
            tokenData.Roles = [.. principal.FindAll("roles").Select(c => c.Value)];
            tokenData.IsAdmin = tokenData.Roles.Contains("Admin");
        }

        return Task.FromResult(principal);
    }
}
