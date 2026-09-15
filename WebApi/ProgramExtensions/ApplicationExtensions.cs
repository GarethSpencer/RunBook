using DAL.Data;
using Microsoft.EntityFrameworkCore;

namespace WebApi.ProgramExtensions;

public static class ApplicationExtensions
{
    public static void ApplyMigrations(this WebApplication application)
    {
        using var scope = application.Services.CreateScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<RunBookDbContext>();
        dbContext.Database.Migrate();
    }

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
            options.OAuthAdditionalQueryStringParams(new Dictionary<string, string>
            {
                { "prompt", "select_account" }
            });
        });
    }
}
