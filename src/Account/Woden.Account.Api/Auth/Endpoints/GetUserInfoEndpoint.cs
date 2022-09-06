namespace KgNet88.Woden.Account.Api.Auth.Endpoints;

public class GetUserInfoEndpoint : EndpointWithoutRequest
{
    private readonly ISender _mediator;
    private readonly ProblemDetailsFactory _problemDetailsFactory;
    private readonly MapsterMapper.IMapper _mapper;

    public GetUserInfoEndpoint(
        ISender mediator,
        ProblemDetailsFactory problemDetailsFactory,
        MapsterMapper.IMapper mapper)
    {
        this._mediator = mediator;
        this._problemDetailsFactory = problemDetailsFactory;
        this._mapper = mapper;
    }

    public override void Configure()
    {
        this.Get("auth/user/{Username}");
    }

    public override async Task HandleAsync(CancellationToken ct)
    {
        string username = this.Route<string>("Username") ?? "";

        var command = new GetUserInfoQuery()
        {
            Username = username,
        };

        var result = await this._mediator.Send(command, ct);

        if (result.IsError)
        {
            await result.SendProblemDetailsAsync(this.HttpContext, this._problemDetailsFactory);
            return;
        }

        var response = this._mapper.Map<GetUserInfoResponse>(result.Value);

        await this.SendOkAsync(response, ct);
    }
}