namespace WebApi.ProgramExtensions;

public static class ApplicationExtensions
{
    public static void ConfigureOpenApi(this WebApplication app, IConfiguration config)
    {
        app.MapOpenApi()
            .WithDocumentPerVersion()
            .AllowAnonymous();
        app.UseSwaggerUI(options =>
        {
            options.SwaggerEndpoint("/openapi/v1.json", "RunBook API v1");
            options.OAuthClientId(config["AzureAd:ClientId"]);
            options.OAuthUsePkce();
            options.EnablePersistAuthorization();
        });
    }
}
