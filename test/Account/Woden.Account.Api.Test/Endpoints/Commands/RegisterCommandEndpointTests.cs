namespace KgNet88.Woden.Account.Api.Test.Endpoints.Commands;

[Collection("Sequential")]
public class RegisterCommandEndpointTests : AbstractEndpointTestTemplate
{
    public RegisterCommandEndpointTests(TestApplicationFactory<AccountService> application) : base(application)
    {
    }

    // happy path test (status: 200)
    [Fact]
    public async Task POST_register_user_OK_200_Async()
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
    }

    // validation failure test (status: 400)
    [Fact]
    public async Task POST_register_user_validation_Fail_400_Async()
    {
        var client = await this.InitTestAsync();

        var response = await client.PostAsJsonAsync(
            "api/auth/register",
            new RegisterRequest()
            {
                Username = "",
                Email = TestEmail,
                Password = TestPassword
            });

        _ = response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        _ = response.Content.Headers.ContentType!.MediaType.Should().Be("application/problem+json");

        var message = await response.Content.ReadFromJsonAsync<ValidationProblemDetails>();
        _ = message!.Status.Should().Be(400);
        _ = message!.Title.Should().Be("One or more validation errors occurred.");
        _ = message!.Errors["Username"][0].Should().Be("username should not be empty.");

        response = await client.PostAsJsonAsync(
            "api/auth/register",
            new RegisterRequest()
            {
                Username = "",
                Email = "",
                Password = TestPassword
            });

        _ = response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        _ = response.Content.Headers.ContentType!.MediaType.Should().Be("application/problem+json");

        message = await response.Content.ReadFromJsonAsync<ValidationProblemDetails>();
        _ = message!.Status.Should().Be(400);
        _ = message!.Title.Should().Be("One or more validation errors occurred.");
        _ = message!.Errors["Username"][0].Should().Be("username should not be empty.");
        _ = message!.Errors["Email"][0].Should().Be("email should not be empty.");

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
    public async Task POST_register_user_invalid_password_Fail_400_Async()
    {
        var client = await this.InitTestAsync();

        var response = await client.PostAsJsonAsync(
            "api/auth/register",
            new RegisterRequest()
            {
                Username = TestUsername,
                Email = TestEmail,
                Password = "shorty"
            });

        _ = response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        _ = response.Content.Headers.ContentType!.MediaType.Should().Be("application/problem+json");

        var message = await response.Content.ReadFromJsonAsync<ValidationProblemDetails>();
        _ = message!.Status.Should().Be(400);
        _ = message!.Title.Should().Be("One or more validation errors occurred.");
        _ = message!.Errors["Password"].Length.Should().Be(4);
        _ = message!.Errors["Password"][0].Should().Be("password must be at least 8 characters long.");
        _ = message!.Errors["Password"][1].Should().Be("password must contain at least one capital letter.");
        _ = message!.Errors["Password"][2].Should().Be("password must contain at least one number.");
        _ = message!.Errors["Password"][3].Should().Be("password must contain at least one special character.");

        response = await client.PostAsJsonAsync(
            "api/auth/register",
            new RegisterRequest()
            {
                Username = TestUsername,
                Email = TestEmail,
                Password = "SHORTY!"
            });

        _ = response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        _ = response.Content.Headers.ContentType!.MediaType.Should().Be("application/problem+json");

        message = await response.Content.ReadFromJsonAsync<ValidationProblemDetails>();
        _ = message!.Status.Should().Be(400);
        _ = message!.Title.Should().Be("One or more validation errors occurred.");
        _ = message!.Errors["Password"].Length.Should().Be(3);
        _ = message!.Errors["Password"][0].Should().Be("password must be at least 8 characters long.");
        _ = message!.Errors["Password"][1].Should().Be("password must contain at least one lowercase letter.");
        _ = message!.Errors["Password"][2].Should().Be("password must contain at least one number.");
    }

    // conflict failure test (status: 409)
    [Fact]
    public async Task POST_register_user_conflict_username_409_Async()
    {
        var client = await this.InitTestAsync(true);

        var response = await client.PostAsJsonAsync(
            "api/auth/register",
            new RegisterRequest()
            {
                Username = TestUsername,
                Email = TestEmail,
                Password = TestPassword
            });

        _ = response.StatusCode.Should().Be(HttpStatusCode.Conflict);
        _ = response.Content.Headers.ContentType!.MediaType.Should().Be("application/problem+json");

        var message = await response.Content.ReadFromJsonAsync<ProblemDetails>();
        _ = message!.Status.Should().Be(409);
        _ = message!.Title.Should().Be(AuthErrors.User.UsernameAlreadyExists.Description);
    }

    [Fact]
    public async Task POST_register_user_conflict_email_409_Async()
    {
        var client = await this.InitTestAsync(true);

        var response = await client.PostAsJsonAsync(
            "api/auth/register",
            new RegisterRequest()
            {
                Username = TestUsername + "2",
                Email = TestEmail,
                Password = TestPassword
            });

        _ = response.StatusCode.Should().Be(HttpStatusCode.Conflict);
        _ = response.Content.Headers.ContentType!.MediaType.Should().Be("application/problem+json");

        var message = await response.Content.ReadFromJsonAsync<ProblemDetails>();
        _ = message!.Status.Should().Be(409);
        _ = message!.Title.Should().Be(AuthErrors.User.EmailAlreadyExists.Description);
    }
}
