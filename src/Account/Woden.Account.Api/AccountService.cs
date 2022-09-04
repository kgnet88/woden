namespace KgNet88.Woden.Account.Api;

[System.Diagnostics.CodeAnalysis.SuppressMessage("Roslynator", "RCS1102:Make class static.", Justification = "Reflection")]
public class AccountService
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.

        _ = builder.Services.RegisterApplicationServices();
        _ = builder.Services.RegisterInfrastructureServices(builder.Configuration);

        _ = builder.Services.AddSingleton<AbstractProblemDetailsFactory, CommonProblemDetailsFactory>();

        _ = builder.Services.AddFastEndpoints();

        _ = builder.Services.AddSwaggerDoc(settings =>
        {
            settings.Title = "Woden Auth API";
            settings.Version = "v1";
        },
        serializer =>
        {
            serializer.PropertyNamingPolicy = null;
            serializer.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
        }, shortSchemaNames: true);

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        _ = app.UseMiddleware<ValidationExceptionMiddleware>();

        _ = app.UseAuthentication();
        _ = app.UseAuthorization();

        _ = app.UseFastEndpoints(c =>
        {
            c.Serializer.Options.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
            _ = c.Serializer.Options.ConfigureForNodaTime(DateTimeZoneProviders.Tzdb);
            c.Endpoints.RoutePrefix = "api";
            c.Endpoints.ShortNames = true;
            c.Errors.ResponseBuilder = (errors, _) =>
            {
                var problemDetails = new MyValidationProblemDetails()
                {
                    Status = StatusCodes.Status400BadRequest,
                    Type = "https://tools.ietf.org/html/rfc7231#section-6.5.1",
                    Title = "Validation Error",
                    Detail = "One or more errors occured!"
                };

                foreach (var failure in errors)
                {
                    if (problemDetails.Errors.TryGetValue(failure.PropertyName, out var errorList))
                    {
                        errorList!.Add(failure.ErrorMessage);
                    }
                    else
                    {
                        problemDetails.Errors.Add(failure.PropertyName, new List<string> { failure.ErrorMessage });
                    }
                }
                return problemDetails;
            };
        });

        _ = app.UseOpenApi();
        _ = app.UseSwaggerUi3(s => s.ConfigureDefaults());

        app.Run();
    }
}