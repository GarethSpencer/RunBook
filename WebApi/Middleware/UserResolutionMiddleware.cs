using Microsoft.AspNetCore.Authorization;
using ServiceLayer.Abstractions;
using Utilities.Models.Token;

namespace WebApi.Middleware;

internal class UserResolutionMiddleware(ITokenData tokenData, IAuthService authService) : IMiddleware
{
    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        var endpoint = context.GetEndpoint();
        var allowsAnonymous = endpoint?.Metadata.GetMetadata<IAllowAnonymous>() != null;

        if (!allowsAnonymous && context.User.Identity?.IsAuthenticated == true)
        {
            var userId = await authService.ResolveUserAsync(tokenData.AuthId, tokenData.PreferredName, context.RequestAborted);
            tokenData.UserId = userId;
        }

        await next(context);
    }
}
