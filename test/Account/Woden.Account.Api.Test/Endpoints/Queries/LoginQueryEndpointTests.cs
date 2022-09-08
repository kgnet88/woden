namespace KgNet88.Woden.Account.Api.Test.Endpoints.Queries;

[Collection("Sequential")]
public class LoginQueryEndpointTests : AbstractEndpointTestTemplate
{
    public LoginQueryEndpointTests(TestApplicationFactory<AccountService> application) : base(application)
    {
    }

    // happy path test (status: 200)
    [Fact]
    public async Task POST_login_user_OK_200_Async()
    {
        var client = await this.InitTestAsync();

        var response = await client.PostAsJsonAsync(
            "api/auth/register",
            new RegisterRequest()
            {
                Username = TestUsername,
                Email = TestEmail,
                Password = TestPassword
            });

        _ = response.StatusCode.Should().Be(HttpStatusCode.OK);

        response = await client.PostAsJsonAsync(
            "api/auth/login",
            new LoginRequest()
            {
                Username = TestUsername,
                Password = TestPassword
            });

        _ = response.StatusCode.Should().Be(HttpStatusCode.OK);

        var token = await response.Content.ReadFromJsonAsync<LoginResponse>(GetJsonOptions());
        _ = token.Should().NotBeNull();
        _ = token!.AccessToken.Should().NotBeNullOrEmpty();
        _ = token!.ValidTo.Should().BeGreaterThan(SystemClock.Instance.GetCurrentInstant());
    }

    // validation failure test (status: 400)
    [Fact]
    public async Task POST_login_user_validation_Fail_400_Async()
    {
        var client = await this.InitTestAsync();

        var response = await client.PostAsJsonAsync(
            "api/auth/login",
            new LoginRequest()
            {
                Username = "",
                Password = TestPassword
            });

        _ = response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        _ = response.Content.Headers.ContentType!.MediaType.Should().Be("application/problem+json");

        var message = await response.Content.ReadFromJsonAsync<ValidationProblemDetails>();
        _ = message!.Status.Should().Be(400);
        _ = message!.Title.Should().Be("One or more validation errors occurred.");
        _ = message!.Errors["Username"][0].Should().Be("username should not be empty.");

        response = await client.PostAsJsonAsync(
            "api/auth/login",
            new LoginRequest()
            {
                Username = "",
                Password = ""
            });

        _ = response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        _ = response.Content.Headers.ContentType!.MediaType.Should().Be("application/problem+json");

        message = await response.Content.ReadFromJsonAsync<ValidationProblemDetails>();
        _ = message!.Status.Should().Be(400);
        _ = message!.Title.Should().Be("One or more validation errors occurred.");
        _ = message!.Errors["Username"][0].Should().Be("username should not be empty.");
        _ = message!.Errors["Password"][0].Should().Be("password should not be empty.");
    }

    [Fact]
    public async Task POST_login_user_invalid_credentials_Fail_400_Async()
    {
        var client = await this.InitTestAsync(true);

        var response = await client.PostAsJsonAsync(
            "api/auth/login",
            new LoginRequest()
            {
                Username = TestUsername,
                Password = TestPassword + "2"
            });

        _ = response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        _ = response.Content.Headers.ContentType!.MediaType.Should().Be("application/problem+json");

        var message = await response.Content.ReadFromJsonAsync<ProblemDetails>();
        _ = message!.Status.Should().Be(400);
        _ = message!.Title.Should().Be(AuthErrors.User.InvalidCredentials.Description);
    }

    // user does not exists failure test (status: 404)
    [Fact]
    public async Task POST_login_user_does_not_exist_Fail_404_Async()
    {
        var client = await this.InitTestAsync(true);

        var response = await client.PostAsJsonAsync(
            "api/auth/login",
            new LoginRequest()
            {
                Username = TestUsername + "2",
                Password = TestPassword
            });

        _ = response.StatusCode.Should().Be(HttpStatusCode.NotFound);
        _ = response.Content.Headers.ContentType!.MediaType.Should().Be("application/problem+json");

        var message = await response.Content.ReadFromJsonAsync<ProblemDetails>();
        _ = message!.Status.Should().Be(404);
        _ = message!.Title.Should().Be(AuthErrors.User.DoesNotExist.Description);
    }
}