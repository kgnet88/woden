using Microsoft.AspNetCore.Diagnostics;

namespace KgNet88.Woden.Account.Api;

[System.Diagnostics.CodeAnalysis.SuppressMessage("Roslynator", "RCS1102:Make class static.", Justification = "Reflection")]
public class AccountService
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        _ = builder.Services
            .AddPresentation()
            .AddApplication()
            .AddInfrastructure(builder.Configuration);

        var app = builder.Build();

        // catch all Exception handler (only unexpected exceptions go through here)
        _ = app.UseExceptionHandler(appError => appError.Run(async context =>
        {
            var factory = app.Services.GetService<ProblemDetailsFactory>();

            var contextFeature = context.Features.Get<IExceptionHandlerFeature>();

            if (contextFeature != null)
            {
                ErrorOr<Success> error = Error.Unexpected("General.Unexpected", contextFeature.Error.Message);
                await error.SendProblemDetailsAsync(context, factory!);
            }
        }));

        _ = app.UseAuthentication();
        _ = app.UseAuthorization();

        _ = app.UseFastEndpoints(c =>
        {
            c.Serializer.Options.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
            _ = c.Serializer.Options.ConfigureForNodaTime(DateTimeZoneProviders.Tzdb);
            c.Endpoints.RoutePrefix = "api";
            c.Endpoints.ShortNames = true;
        });

        _ = app.UseOpenApi();
        _ = app.UseSwaggerUi3(s => s.ConfigureDefaults());

        app.Run();
    }
}