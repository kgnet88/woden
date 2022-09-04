using KgNet88.Woden.Account.Api.Middleware;

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
    public async Task POST_register_user_bad_username_Fail_400_Async()
    {
        var client = await this.InitTestAsync();

        var response = await client.PostAsJsonAsync(
            "api/auth/register",
            new RegisterRequest()
            {
                Username = "pe",
                Email = "peter@cando.de",
                Password = "Password123!"
            });

        _ = response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        _ = response.Content.Headers.ContentType!.MediaType.Should().Be("application/problem+json");

        var message = await response.Content.ReadFromJsonAsync<MyValidationProblemDetails>();
        _ = message!.Status.Should().Be(400);
        _ = message!.Detail.Should().Be("One or more errors occured!");
        _ = message!.Errors["Username"][0].Should().Be("username is too short!");

        response = await client.PostAsJsonAsync(
            "api/auth/register",
            new RegisterRequest()
            {
                Username = "",
                Email = "peter@cando.de",
                Password = "Password123!"
            });

        _ = response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        _ = response.Content.Headers.ContentType!.MediaType.Should().Be("application/problem+json");

        message = await response.Content.ReadFromJsonAsync<MyValidationProblemDetails>();
        _ = message!.Status.Should().Be(400);
        _ = message!.Detail.Should().Be("One or more errors occured!");
        _ = message!.Errors["Username"][0].Should().Be("username should not be empty!");
    }

    [Fact]
    public async Task POST_register_user_ok_then_bad_username_already_existed_400_Async()
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

        _ = response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        _ = response.Content.Headers.ContentType!.MediaType.Should().Be("application/problem+json");

        var message = await response.Content.ReadFromJsonAsync<MyValidationProblemDetails>();
        _ = message!.Status.Should().Be(400);
        _ = message!.Detail.Should().Be("One or more errors occured!");
        _ = message!.Errors["username"][0].Should().Be("A user peter already exists!");
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