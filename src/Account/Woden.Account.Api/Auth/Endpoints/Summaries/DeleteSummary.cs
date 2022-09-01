namespace KgNet88.Woden.Account.Api.Auth.Endpoints.Summaries;

public class DeleteSummary : Summary<DeleteUserByNameEndpoint>
{
    public DeleteSummary()
    {
        this.Summary = "Registers a user";

        this.Description = "The auth service tries to delete the user via " +
            "username. The service returns ok, if the request is valid.";

        this.Response(200, "ok response, user was successfull deleted");
        this.Response(400, "error - user could not be deleted");
        this.Response(403, "error - you are not allowed to delete other users");
    }
}