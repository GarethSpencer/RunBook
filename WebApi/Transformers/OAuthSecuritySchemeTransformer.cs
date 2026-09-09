using Microsoft.AspNetCore.OpenApi;
using Microsoft.OpenApi;

namespace WebApi.Transformers
{
    public class OAuthSecuritySchemeTransformer(IConfiguration configuration) : IOpenApiDocumentTransformer
    {
        public Task TransformAsync(OpenApiDocument document, OpenApiDocumentTransformerContext context, CancellationToken cancellationToken)
        {
            var scope = "api://" + configuration["AzureAd:ClientId"] + "/access_as_user";

            document.Components ??= new();
            document.Components.SecuritySchemes ??= new Dictionary<string, IOpenApiSecurityScheme>();
            document.Components.SecuritySchemes["oauth2"] = new OpenApiSecurityScheme
            {
                Type = SecuritySchemeType.OAuth2,
                Flows = new OpenApiOAuthFlows
                {
                    AuthorizationCode = new OpenApiOAuthFlow
                    {
                        AuthorizationUrl = new Uri("https://login.microsoftonline.com/" + configuration["AzureAd:TenantId"] + "/oauth2/v2.0/authorize"),
                        TokenUrl = new Uri("https://login.microsoftonline.com/" + configuration["AzureAd:TenantId"] + "/oauth2/v2.0/token"),
                        Scopes = new Dictionary<string, string>
                        {
                            [scope] = "Access WebApi as user"
                        }
                    }
                }
            };

            foreach (var operation in document.Paths.Values.SelectMany(p => p.Operations?.Values ?? Enumerable.Empty<OpenApiOperation>()))
            {
                operation.Security ??= [];
                operation.Security.Add(new OpenApiSecurityRequirement
                {
                    [new OpenApiSecuritySchemeReference("oauth2", document)] = [scope]
                });
            }

            return Task.CompletedTask;
        }
    }
}
