namespace KgNet88.Woden.Account.Api.Test.Endpoints;

public abstract class AbstractEndpointTestTemplate : IClassFixture<TestApplicationFactory<AccountService>>
{
    protected readonly TestApplicationFactory<AccountService> _application;
    protected static string TestUsername => "peter";
    protected static string TestEmail => "peter@cando.de";
    protected static string TestPassword => "Password123!";

    protected AbstractEndpointTestTemplate(TestApplicationFactory<AccountService> application)
    {
        this._application = application;
    }

    protected static JsonSerializerOptions GetJsonOptions()
    {
        var options = new JsonSerializerOptions().ConfigureForNodaTime(DateTimeZoneProviders.Tzdb);
        options.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;

        return options;
    }

    protected async Task<HttpClient> InitTestAsync(bool registerAndLoginUser = false)
    {
        await this._application.ResetDatabaseAsync();
        var client = this._application.CreateClient();

        if (registerAndLoginUser)
        {
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

            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token!.AccessToken);
        }

        return client;
    }
}
