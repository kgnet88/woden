namespace KgNet88.Woden.Account.Api.Auth.Endpoints;

public class RegisterEndpoint : Endpoint<RegisterRequest, ErrorOr<Created>>
{
    private readonly ISender _mediator;
    private readonly ProblemDetailsFactory _problemDetailsFactory;
    private readonly MapsterMapper.IMapper _mapper;

    public RegisterEndpoint(ISender mediator, ProblemDetailsFactory problemDetailsFactory, MapsterMapper.IMapper mapper)
    {
        this._mediator = mediator;
        this._problemDetailsFactory = problemDetailsFactory;
        this._mapper = mapper;
    }

    public override void Configure()
    {
        this.Post("auth/register");
        this.AllowAnonymous();
    }

    public override async Task HandleAsync(RegisterRequest request, CancellationToken ct)
    {
        var command = this._mapper.Map<RegisterCommand>(request);

        var result = await this._mediator.Send(command, ct);

        if (result.IsError)
        {
            await result.SendProblemDetailsAsync(this.HttpContext, this._problemDetailsFactory);
            return;
        }

        await this.SendOkAsync(ct);
    }
}
