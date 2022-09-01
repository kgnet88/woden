namespace Goedde88.Woden.Account.Api.Auth.Endpoints.Summaries;

public class LoginSummary : Summary<LoginEndpoint, LoginRequest>
{
    public LoginSummary()
    {
        this.Summary = "Authenticates a user and produces necessary access token";

        this.Description = "The auth service tries to authenticate the user via " +
            "username and password. The service returns an access token, if the request is valid.";

        this.ExampleRequest = new LoginRequest() { Username = "peter", Password = "Password123!" };

        this.RequestParam(x => x.Username, "username for authentication");
        this.RequestParam(x => x.Password, "password for authentication");

        this.Response<LoginResponse>(200, "ok response with access token and timestamp");
        this.Response(400, "error - user could not log in");
    }
}