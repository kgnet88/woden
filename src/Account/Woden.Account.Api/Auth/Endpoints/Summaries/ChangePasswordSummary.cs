namespace KgNet88.Woden.Account.Api.Auth.Endpoints.Summaries;

public class ChangePasswordSummary : Summary<ChangePasswordEndpoint, ChangePasswordRequest>
{
    public ChangePasswordSummary()
    {
        this.Summary = "Change the users password";

        this.Description = "The auth service tries to change the password of an authorized user via " +
            "credentials and new password. The service returns ok, if the password was changed.";

        this.ExampleRequest = new ChangePasswordRequest() { Username = "peter", OldPassword = "Password123!", NewPassword = "Password234!" };

        this.RequestParam(x => x.Username, "username for login");
        this.RequestParam(x => x.OldPassword, "old password for login");
        this.RequestParam(x => x.NewPassword, "new password for login");

        this.Response(200, "ok response, password was successfully changed");
        this.Response(400, "error - invalid credentials");
        this.Response(401, "error - user is not authorized");
        this.Response(404, "error - user could not be found");
    }
}