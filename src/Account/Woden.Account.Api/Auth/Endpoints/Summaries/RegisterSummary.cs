namespace Goedde88.Woden.Account.Api.Auth.Endpoints.Summaries;

public class RegisterSummary : Summary<RegisterEndpoint, RegisterRequest>
{
    public RegisterSummary()
    {
        this.Summary = "Registers a user";

        this.Description = "The auth service tries to register the user via " +
            "username, email and password. The service returns ok, if the request is valid.";

        this.ExampleRequest = new RegisterRequest() { Username = "peter", Email = "peter@cando.de", Password = "Password123!" };

        this.RequestParam(x => x.Username, "username for login");
        this.RequestParam(x => x.Email, "email for validation");
        this.RequestParam(x => x.Password, "password for login");

        this.Response(200, "ok response, user was successfull registered");
        this.Response(400, "error - user could not be registered");
    }
}