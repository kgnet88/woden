namespace KgNet88.Woden.Account.Api.Test.Endpoints.Queries;

[Collection("Sequential")]
public class GetUserInfoQueryEndpointTests : AbstractEndpointTestTemplate
{
    public GetUserInfoQueryEndpointTests(TestApplicationFactory<AccountService> application) : base(application)
    {
    }

    // happy path test (status: 200)
    [Fact]
    public async Task GET_authorized_user_infos_OK_200_Async()
    {
        var client = await this.InitTestAsync(true);

        var response = await client.GetAsync($"api/auth/user/{TestUsername}");
        _ = response.StatusCode.Should().Be(HttpStatusCode.OK);

        var info = await response.Content.ReadFromJsonAsync<GetUserInfoResponse>(GetJsonOptions());
        _ = info.Should().NotBeNull();
        _ = info!.Username.Should().Be(TestUsername);
        _ = info!.Email.Should().Be(TestEmail);
    }

    // unauthorized failure test (status: 401)
    [Fact]
    public async Task GET_unauthorized_user_infos_Fail_401_Async()
    {
        var client = await this.InitTestAsync();

        var response = await client.GetAsync($"api/auth/user/{TestUsername}");
        _ = response.StatusCode.Should().Be(HttpStatusCode.Unauthorized);
    }

    // user does not exists failure test (status: 404)
    [Fact]
    public async Task GET_not_existing_user_infos_Fail_404_Async()
    {
        var client = await this.InitTestAsync(true);

        var response = await client.GetAsync($"api/auth/user/{TestUsername + "2"}");
        _ = response.StatusCode.Should().Be(HttpStatusCode.NotFound);

        var message = await response.Content.ReadFromJsonAsync<ProblemDetails>();
        _ = message!.Status.Should().Be(404);
        _ = message!.Title.Should().Be(AuthErrors.User.DoesNotExist.Description);
    }
}
