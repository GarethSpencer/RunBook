using Microsoft.AspNetCore.Authentication;
using Serilog;
using Serilog.Debugging;
using ServiceLayer;
using Utilities;
using WebApi.Middleware;
using WebApi.ProgramExtensions;
using WebApi.Transformers;

var builder = WebApplication.CreateBuilder(args);

builder.Services.ConfigureControllers();
builder.Services.ConfigureApiVersioning()
    .ConfigureOpenApi();
builder.Services.AddScoped<ExceptionHandlingMiddleware>();
builder.Services.AddScoped<UserResolutionMiddleware>();
builder.Services.ConfigureCors();
builder.Services.ConfigureAuthentication(builder.Configuration);
builder.Services.ConfigureAuthorization();
builder.Services.ConfigureHealthChecks(builder.Configuration);
builder.Services.AddScoped<IClaimsTransformation, TokenDataClaimsTransformer>();

builder.Services.AddServiceLayer(builder.Configuration);
builder.Services.AddUtilities();

builder.Host.UseSerilog((hostingContext, configuration) =>
{
    configuration.ReadFrom.Configuration(hostingContext.Configuration);
});

var app = builder.Build();

app.ApplyMigrations();

if (app.Environment.IsDevelopment())
{
    SelfLog.Enable(Console.Error);
    app.ConfigureOpenApi(builder.Configuration);
}

app.UseMiddleware<ExceptionHandlingMiddleware>();
app.UseHttpsRedirection();
app.UseCors("DevelopmentCorsPolicy");
app.UseSerilogRequestLogging();
app.UseAuthentication();
app.UseAuthorization();
app.UseMiddleware<UserResolutionMiddleware>();
app.MapControllers();
app.MapHealthChecks("/health").AllowAnonymous();

app.Run();
