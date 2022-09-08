namespace KgNet88.Woden.Account.Api.Test.Endpoints.Commands;

[Collection("Sequential")]
public class DeleteUserByNameCommandEndpointTests : AbstractEndpointTestTemplate
{
    public DeleteUserByNameCommandEndpointTests(TestApplicationFactory<AccountService> application) : base(application)
    {
    }

    // happy path test (status: 200)
    [Fact]
    public async Task POST_delete_user_OK_200_Async()
    {
        var client = await this.InitTestAsync(true);

        var response = await client.DeleteAsync($"api/auth/user/{TestUsername}");

        _ = response.StatusCode.Should().Be(HttpStatusCode.OK);
    }

    // unauthorized failure test (status: 401)
    [Fact]
    public async Task POST_delete_user_unauthenticated_Fail_401_Async()
    {
        var client = await this.InitTestAsync();

        var response = await client.DeleteAsync($"api/auth/user/{TestUsername}");
        _ = response.StatusCode.Should().Be(HttpStatusCode.Unauthorized);
    }

    // user does not exists failure test (status: 404)
    [Fact]
    public async Task POST_delete_user_does_not_exist_404_Async()
    {
        var client = await this.InitTestAsync(true);

        var response = await client.DeleteAsync($"api/auth/user/{TestUsername + "2"}");
        _ = response.StatusCode.Should().Be(HttpStatusCode.NotFound);
    }
}