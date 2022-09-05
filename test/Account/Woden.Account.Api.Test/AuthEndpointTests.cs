using KgNet88.Woden.Account.Domain.Auth.Errors;

using Microsoft.AspNetCore.Mvc;

namespace KgNet88.Woden.Account.Api.Test;

public sealed class AuthEndpointTests : IClassFixture<TestApplicationFactory<AccountService>>
{
    private readonly TestApplicationFactory<AccountService> _application;

    public AuthEndpointTests(TestApplicationFactory<AccountService> application)
    {
        this._application = application;
    }

    [Fact]
    public async Task POST_delete_user_unauthenticated_Fail_401_Async()
    {
        var client = await this.InitTestAsync();

        var response = await client.DeleteAsync("api/auth/user/peter");
        _ = response.StatusCode.Should().Be(HttpStatusCode.Unauthorized);
    }

    [Fact]
    public async Task POST_register_user_OK_200_Async()
    {
        var client = await this.InitTestAsync();

        var response = await client.PostAsJsonAsync(
            "api/auth/register",
            new RegisterRequest()
            {
                Username = "peter",
                Email = "peter@cando.de",
                Password = "Password123!"
            });

        _ = response.StatusCode.Should().Be(HttpStatusCode.OK);
    }

    [Fact]
    public async Task POST_register_user_validation_Fail_400_Async()
    {
        var client = await this.InitTestAsync();

        var response = await client.PostAsJsonAsync(
            "api/auth/register",
            new RegisterRequest()
            {
                Username = "",
                Email = "peter@cando.de",
                Password = "Password123!"
            });

        _ = response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        _ = response.Content.Headers.ContentType!.MediaType.Should().Be("application/problem+json");

        var message = await response.Content.ReadFromJsonAsync<ValidationProblemDetails>();
        _ = message!.Status.Should().Be(400);
        _ = message!.Title.Should().Be("One or more validation errors occurred.");
        _ = message!.Errors["Username"][0].Should().Be("username should not be empty.");

        client = await this.InitTestAsync();

        response = await client.PostAsJsonAsync(
            "api/auth/register",
            new RegisterRequest()
            {
                Username = "",
                Email = "",
                Password = "Password123!"
            });

        _ = response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        _ = response.Content.Headers.ContentType!.MediaType.Should().Be("application/problem+json");

        message = await response.Content.ReadFromJsonAsync<ValidationProblemDetails>();
        _ = message!.Status.Should().Be(400);
        _ = message!.Title.Should().Be("One or more validation errors occurred.");
        _ = message!.Errors["Username"][0].Should().Be("username should not be empty.");
        _ = message!.Errors["Email"][0].Should().Be("email should not be empty.");

        client = await this.InitTestAsync();

        response = await client.PostAsJsonAsync(
            "api/auth/register",
            new RegisterRequest()
            {
                Username = "",
                Email = "",
                Password = ""
            });

        _ = response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        _ = response.Content.Headers.ContentType!.MediaType.Should().Be("application/problem+json");

        message = await response.Content.ReadFromJsonAsync<ValidationProblemDetails>();
        _ = message!.Status.Should().Be(400);
        _ = message!.Title.Should().Be("One or more validation errors occurred.");
        _ = message!.Errors["Username"][0].Should().Be("username should not be empty.");
        _ = message!.Errors["Email"][0].Should().Be("email should not be empty.");
        _ = message!.Errors["Password"][0].Should().Be("password should not be empty.");
    }

    [Fact]
    public async Task POST_register_user_ok_then_bad_username_already_existed_409_Async()
    {
        var client = await this.InitTestAsync();

        var response = await client.PostAsJsonAsync(
            "api/auth/register",
            new RegisterRequest()
            {
                Username = "peter",
                Email = "peter@cando.de",
                Password = "Password123!"
            });

        _ = response.StatusCode.Should().Be(HttpStatusCode.OK);

        response = await client.PostAsJsonAsync(
            "api/auth/register",
            new RegisterRequest()
            {
                Username = "peter",
                Email = "peter@cando.de",
                Password = "Password123!"
            });

        _ = response.StatusCode.Should().Be(HttpStatusCode.Conflict);
        _ = response.Content.Headers.ContentType!.MediaType.Should().Be("application/problem+json");

        var message = await response.Content.ReadFromJsonAsync<ProblemDetails>();
        _ = message!.Status.Should().Be(409);
        _ = message!.Title.Should().Be(Errors.User.UsernameAlreadyExists.Description);
    }

    [Fact]
    public async Task POST_register_login_delete_user_OK_200_Async()
    {
        var client = await this.InitTestAsync();

        var response = await client.PostAsJsonAsync(
            "api/auth/register",
            new RegisterRequest()
            {
                Username = "peter",
                Email = "peter@cando.de",
                Password = "Password123!"
            });

        _ = response.StatusCode.Should().Be(HttpStatusCode.OK);

        response = await client.PostAsJsonAsync(
            "api/auth/login",
            new LoginRequest()
            {
                Username = "peter",
                Password = "Password123!"
            });

        _ = response.StatusCode.Should().Be(HttpStatusCode.OK);

        var options = new JsonSerializerOptions().ConfigureForNodaTime(DateTimeZoneProviders.Tzdb);
        options.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
        var token = await response.Content.ReadFromJsonAsync<LoginResponse>(options);

        _ = token.Should().NotBeNull();
        _ = token!.AccessToken.Should().NotBeNullOrEmpty();
        _ = token!.ValidTo.Should().BeGreaterThan(SystemClock.Instance.GetCurrentInstant());

        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token.AccessToken);
        response = await client.DeleteAsync("api/auth/user/peter");

        _ = response.StatusCode.Should().Be(HttpStatusCode.OK);
    }

    public async Task<HttpClient> InitTestAsync()
    {
        await this._application.ResetDatabaseAsync();
        return this._application.CreateClient();
    }
}