namespace KgNet88.Woden.Account.Api.Auth.Mappings;

public static class DomainToDtoMapper
{
    public static UserDto ToUserDto(this User user)
    {
        return new UserDto
        {
            Username = user.Username,
            Email = user.Email
        };
    }
}
