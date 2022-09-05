namespace KgNet88.Woden.Account.Api.Auth.Endpoints;

public class LoginEndpoint : Endpoint<LoginRequest, LoginResponse>
{
    private readonly ISender _mediator;
    private readonly ProblemDetailsFactory _problemDetailsFactory;
    private readonly MapsterMapper.IMapper _mapper;

    public LoginEndpoint(ISender mediator, ProblemDetailsFactory problemDetailsFactory, MapsterMapper.IMapper mapper)
    {
        this._mediator = mediator;
        this._problemDetailsFactory = problemDetailsFactory;
        this._mapper = mapper;
    }

    public override void Configure()
    {
        this.Post("auth/login");
        this.AllowAnonymous();
    }

    public override async Task HandleAsync(LoginRequest request, CancellationToken ct)
    {
        var query = this._mapper.Map<LoginQuery>(request);
        var result = await this._mediator.Send(query, ct);

        if (result.IsError)
        {
            await result.SendProblemDetailsAsync(this.HttpContext, this._problemDetailsFactory);
            return;
        }

        var response = this._mapper.Map<LoginResponse>(result.Value);

        await this.SendOkAsync(response!, ct);
    }
}
