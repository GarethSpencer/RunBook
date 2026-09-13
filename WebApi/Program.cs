using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Identity.Web;
using Serilog;
using Serilog.Debugging;
using WebApi.Middleware;
using WebApi.Transformers;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddScoped<ExceptionHandlingMiddleware>();
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddMicrosoftIdentityWebApi(builder.Configuration.GetSection("AzureAd"));

builder.Services.AddControllers();
builder.Services.AddOpenApi(options =>
{
    options.AddDocumentTransformer<OAuthSecuritySchemeTransformer>();
});

builder.Host.UseSerilog((hostingContext, configuration) =>
{
    configuration.ReadFrom.Configuration(hostingContext.Configuration);
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    SelfLog.Enable(Console.Error);
    app.MapOpenApi();
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/openapi/v1.json", "RunBook API v1");
        options.OAuthClientId(builder.Configuration["AzureAd:ClientId"]);
        options.OAuthUsePkce();
        options.EnablePersistAuthorization();
    });
}

app.UseMiddleware<ExceptionHandlingMiddleware>();
app.UseHttpsRedirection();

app.UseSerilogRequestLogging();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
