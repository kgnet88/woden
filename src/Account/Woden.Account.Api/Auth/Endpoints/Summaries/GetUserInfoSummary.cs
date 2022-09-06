namespace KgNet88.Woden.Account.Api.Auth.Endpoints.Summaries;

public class GetUserInfoSummary : Summary<GetUserInfoEndpoint>
{
    public GetUserInfoSummary()
    {
        this.Summary = "Returns the infos for the given user";

        this.Description = "The auth service tries to retrieve the user informations via " +
            "username. if the request is successful it returns the user informations.";

        this.Params = new Dictionary<string, string>() { { "Username", "the given user to get the informations about" } };

        this.Response<GetUserInfoResponse>(200, "ok response, user informations are returned");

        this.Response(401, "error - user is not authorized to see the informations");
        this.Response(404, "error - user could not be found");
    }
}