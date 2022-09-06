namespace KgNet88.Woden.Account.Api.Auth.Endpoints.Summaries;

public class ChangeEmailSummary : Summary<ChangeEmailEndpoint, ChangeEmailRequest>
{
    public ChangeEmailSummary()
    {
        this.Summary = "Change the users email";

        this.Description = "The auth service tries to change the email of an authorized user via " +
            "username and new email. The service returns ok, if the email was changed.";

        this.ExampleRequest = new ChangeEmailRequest() { Username = "peter", NewEmail = "peter@newmail.io" };

        this.RequestParam(x => x.Username, "username for login");
        this.RequestParam(x => x.NewEmail, "new email for user");

        this.Response(200, "ok response, email was successfully changed");
        this.Response(401, "error - user is not authorized");
        this.Response(404, "error - user could not be found");
        this.Response(409, "error - user with same email already existed");
    }
}