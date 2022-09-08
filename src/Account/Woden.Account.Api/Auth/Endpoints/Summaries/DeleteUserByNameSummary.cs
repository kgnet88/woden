namespace KgNet88.Woden.Account.Api.Auth.Endpoints.Summaries;

public class DeleteUserByNameSummary : Summary<DeleteUserByNameEndpoint>
{
    public DeleteUserByNameSummary()
    {
        this.Summary = "Deletes a user";

        this.Description = "The auth service tries to delete the user via " +
            "username. The service returns ok, if the request is valid and the user is authorized.";

        this.Params = new Dictionary<string, string>() { { "Username", "the given user to be deleted" } };

        this.Response(200, "ok response, user was successfull deleted");
        this.Response(400, "error - user could not be deleted");
        this.Response(401, "error - user is not authorizrd");
    }
}