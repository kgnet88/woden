namespace KgNet88.Woden.Account.Api.Test.Endpoints.Commands;

[Collection("Sequential")]
public class ChangePasswordCommandEndpointTests : AbstractEndpointTestTemplate
{
    public ChangePasswordCommandEndpointTests(TestApplicationFactory<AccountService> application) : base(application)
    {
    }

    // happy path test (status: 200)
    [Fact]
    public async Task POST_change_password_OK_200_Async()
    {
        var client = await this.InitTestAsync(true);
        const string newPassword = "Test234!";

        var response = await client.PostAsJsonAsync(
            "api/auth/user/password",
            new ChangePasswordRequest()
            {
                Username = TestUsername,
                OldPassword = TestPassword,
                NewPassword = newPassword
            });

        _ = response.StatusCode.Should().Be(HttpStatusCode.OK);

        response = await client.PostAsJsonAsync(
            "api/auth/login",
            new LoginRequest()
            {
                Username = TestUsername,
                Password = newPassword
            });

        _ = response.StatusCode.Should().Be(HttpStatusCode.OK);
    }

    // validation failure test (status: 400)
    [Fact]
    public async Task POST_change_password_validation_Fail_400_Async()
    {
        var client = await this.InitTestAsync(true);
        const string newPassword = "Test234!";

        var response = await client.PostAsJsonAsync(
            "api/auth/user/password",
            new ChangePasswordRequest()
            {
                Username = "",
                OldPassword = TestPassword,
                NewPassword = newPassword
            });

        _ = response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        _ = response.Content.Headers.ContentType!.MediaType.Should().Be("application/problem+json");

        var message = await response.Content.ReadFromJsonAsync<ValidationProblemDetails>();
        _ = message!.Status.Should().Be(400);
        _ = message!.Title.Should().Be("One or more validation errors occurred.");
        _ = message!.Errors.Count.Should().Be(1);
        _ = message!.Errors["Username"][0].Should().Be("username should not be empty.");

        response = await client.PostAsJsonAsync(
            "api/auth/user/password",
            new ChangePasswordRequest()
            {
                Username = "",
                OldPassword = "",
                NewPassword = newPassword
            });

        _ = response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        _ = response.Content.Headers.ContentType!.MediaType.Should().Be("application/problem+json");

        message = await response.Content.ReadFromJsonAsync<ValidationProblemDetails>();
        _ = message!.Status.Should().Be(400);
        _ = message!.Title.Should().Be("One or more validation errors occurred.");
        _ = message!.Errors.Count.Should().Be(2);
        _ = message!.Errors["Username"][0].Should().Be("username should not be empty.");
        _ = message!.Errors["OldPassword"][0].Should().Be("old password should not be empty.");

        response = await client.PostAsJsonAsync(
            "api/auth/user/password",
            new ChangePasswordRequest()
            {
                Username = "",
                OldPassword = "",
                NewPassword = ""
            });

        _ = response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        _ = response.Content.Headers.ContentType!.MediaType.Should().Be("application/problem+json");

        message = await response.Content.ReadFromJsonAsync<ValidationProblemDetails>();
        _ = message!.Status.Should().Be(400);
        _ = message!.Title.Should().Be("One or more validation errors occurred.");
        _ = message!.Errors.Count.Should().Be(3);
        _ = message!.Errors["Username"][0].Should().Be("username should not be empty.");
        _ = message!.Errors["OldPassword"][0].Should().Be("old password should not be empty.");
        _ = message!.Errors["NewPassword"][0].Should().Be("new password should not be empty.");
    }

    [Fact]
    public async Task POST_register_user_invalid_password_Fail_400_Async()
    {
        var client = await this.InitTestAsync(true);

        var response = await client.PostAsJsonAsync(
            "api/auth/user/password",
            new ChangePasswordRequest()
            {
                Username = TestUsername,
                OldPassword = TestPassword,
                NewPassword = "shorty"
            });

        _ = response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        _ = response.Content.Headers.ContentType!.MediaType.Should().Be("application/problem+json");

        var message = await response.Content.ReadFromJsonAsync<ValidationProblemDetails>();
        _ = message!.Status.Should().Be(400);
        _ = message!.Title.Should().Be("One or more validation errors occurred.");
        _ = message!.Errors["NewPassword"].Length.Should().Be(4);
        _ = message!.Errors["NewPassword"][0].Should().Be("password must be at least 8 characters long.");
        _ = message!.Errors["NewPassword"][1].Should().Be("password must contain at least one capital letter.");
        _ = message!.Errors["NewPassword"][2].Should().Be("password must contain at least one number.");
        _ = message!.Errors["NewPassword"][3].Should().Be("password must contain at least one special character.");

        response = await client.PostAsJsonAsync(
            "api/auth/user/password",
            new ChangePasswordRequest()
            {
                Username = TestUsername,
                OldPassword = TestPassword,
                NewPassword = "SHORTY!"
            });

        _ = response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        _ = response.Content.Headers.ContentType!.MediaType.Should().Be("application/problem+json");

        message = await response.Content.ReadFromJsonAsync<ValidationProblemDetails>();
        _ = message!.Status.Should().Be(400);
        _ = message!.Title.Should().Be("One or more validation errors occurred.");
        _ = message!.Errors["NewPassword"].Length.Should().Be(3);
        _ = message!.Errors["NewPassword"][0].Should().Be("password must be at least 8 characters long.");
        _ = message!.Errors["NewPassword"][1].Should().Be("password must contain at least one lowercase letter.");
        _ = message!.Errors["NewPassword"][2].Should().Be("password must contain at least one number.");
    }

    // unauthorized failure test (status: 401)
    [Fact]
    public async Task POST_delete_user_unauthenticated_Fail_401_Async()
    {
        var client = await this.InitTestAsync();
        const string newPassword = "Test234!";

        var response = await client.PostAsJsonAsync(
            "api/auth/user/password",
            new ChangePasswordRequest()
            {
                Username = TestUsername,
                OldPassword = TestPassword,
                NewPassword = newPassword
            });

        _ = response.StatusCode.Should().Be(HttpStatusCode.Unauthorized);
    }

    // user does not exists failure test (status: 404)
    [Fact]
    public async Task POST_change_password__user_does_not_exist_404_Async()
    {
        var client = await this.InitTestAsync(true);
        const string newPassword = "Test234!";

        var response = await client.PostAsJsonAsync(
            "api/auth/user/password",
            new ChangePasswordRequest()
            {
                Username = TestUsername + "2",
                OldPassword = TestPassword,
                NewPassword = newPassword
            });

        _ = response.StatusCode.Should().Be(HttpStatusCode.NotFound);
    }
}