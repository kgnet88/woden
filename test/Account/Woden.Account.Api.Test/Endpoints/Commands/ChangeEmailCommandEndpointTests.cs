namespace KgNet88.Woden.Account.Api.Test.Endpoints.Commands;

[Collection("Sequential")]
public class ChangeEmailCommandEndpointTests : AbstractEndpointTestTemplate
{
    public ChangeEmailCommandEndpointTests(TestApplicationFactory<AccountService> application) : base(application)
    {
    }

    // happy path test (status: 200)
    [Fact]
    public async Task POST_change_email_OK_200_Async()
    {
        var client = await this.InitTestAsync(true);
        const string newEmail = "peter@newmail.io";

        var response = await client.PostAsJsonAsync(
            "api/auth/user/email",
            new ChangeEmailRequest()
            {
                Username = TestUsername,
                NewEmail = newEmail
            });

        _ = response.StatusCode.Should().Be(HttpStatusCode.OK);
    }

    // validation failure test (status: 400)
    [Fact]
    public async Task POST_change_email_validation_Fail_400_Async()
    {
        var client = await this.InitTestAsync(true);
        const string newEmail = "peter@newmail.io";

        var response = await client.PostAsJsonAsync(
            "api/auth/user/email",
            new ChangeEmailRequest()
            {
                Username = "",
                NewEmail = newEmail
            });

        _ = response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        _ = response.Content.Headers.ContentType!.MediaType.Should().Be("application/problem+json");

        var message = await response.Content.ReadFromJsonAsync<ValidationProblemDetails>();
        _ = message!.Status.Should().Be(400);
        _ = message!.Title.Should().Be("One or more validation errors occurred.");
        _ = message!.Errors.Count.Should().Be(1);
        _ = message!.Errors["Username"][0].Should().Be("username should not be empty.");

        response = await client.PostAsJsonAsync(
            "api/auth/user/email",
            new ChangeEmailRequest()
            {
                Username = "",
                NewEmail = ""
            });

        _ = response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        _ = response.Content.Headers.ContentType!.MediaType.Should().Be("application/problem+json");

        message = await response.Content.ReadFromJsonAsync<ValidationProblemDetails>();
        _ = message!.Status.Should().Be(400);
        _ = message!.Title.Should().Be("One or more validation errors occurred.");
        _ = message!.Errors.Count.Should().Be(2);
        _ = message!.Errors["Username"][0].Should().Be("username should not be empty.");
        _ = message!.Errors["NewEmail"][0].Should().Be("new email should not be empty.");
    }

    // unauthorized failure test (status: 401)
    [Fact]
    public async Task POST_delete_user_unauthenticated_Fail_401_Async()
    {
        var client = await this.InitTestAsync();
        const string newEmail = "peter@newmail.io";

        var response = await client.PostAsJsonAsync(
            "api/auth/user/email",
            new ChangeEmailRequest()
            {
                Username = TestUsername,
                NewEmail = newEmail
            });

        _ = response.StatusCode.Should().Be(HttpStatusCode.Unauthorized);
    }

    // user does not exists failure test (status: 404)
    [Fact]
    public async Task POST_change_password__user_does_not_exist_404_Async()
    {
        var client = await this.InitTestAsync(true);
        const string newEmail = "peter@newmail.io";

        var response = await client.PostAsJsonAsync(
            "api/auth/user/email",
            new ChangeEmailRequest()
            {
                Username = TestUsername + "2",
                NewEmail = newEmail
            });

        _ = response.StatusCode.Should().Be(HttpStatusCode.NotFound);
    }

    // conflict failure test (status: 409)
    [Fact]
    public async Task POST_change_email_conflict_email_409_Async()
    {
        var client = await this.InitTestAsync(true);

        var response = await client.PostAsJsonAsync(
            "api/auth/user/email",
            new ChangeEmailRequest()
            {
                Username = TestUsername + "2",
                NewEmail = TestEmail
            });

        _ = response.StatusCode.Should().Be(HttpStatusCode.Conflict);
        _ = response.Content.Headers.ContentType!.MediaType.Should().Be("application/problem+json");

        var message = await response.Content.ReadFromJsonAsync<ProblemDetails>();
        _ = message!.Status.Should().Be(409);
        _ = message!.Title.Should().Be(Errors.User.EmailAlreadyExists.Description);
    }
}