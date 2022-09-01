namespace KgNet88.Woden.Account.Api.Auth.Mappings;

public static class ApiContractToDomainMapper
{
    public static User ToUser(this RegisterRequest request)
    {
        return new User
        {
            Email = request.Email,
            Username = request.Username
        };
    }
}