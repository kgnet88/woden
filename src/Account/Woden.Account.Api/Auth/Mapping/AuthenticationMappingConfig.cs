namespace KgNet88.Woden.Account.Api.Auth.Mapping;

public class AuthenticationMappingConfig : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        _ = config.NewConfig<RegisterRequest, RegisterCommand>();

        _ = config.NewConfig<LoginRequest, LoginQuery>();

        _ = config.NewConfig<LoginResult, LoginResponse>()
            .Map(dest => dest.AccessToken, src => src.Token)
            .Map(dest => dest.ValidTo, src => src.ValidTo.ToInstant());
    }
}