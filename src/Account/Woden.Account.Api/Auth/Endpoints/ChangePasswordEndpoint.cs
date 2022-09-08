namespace KgNet88.Woden.Account.Api.Auth.Endpoints;

public class ChangePasswordEndpoint : Endpoint<ChangePasswordRequest, ErrorOr<Success>>
{
    private readonly ISender _mediator;
    private readonly ProblemDetailsFactory _problemDetailsFactory;
    private readonly MapsterMapper.IMapper _mapper;

    public ChangePasswordEndpoint(ISender mediator, ProblemDetailsFactory problemDetailsFactory, MapsterMapper.IMapper mapper)
    {
        this._mediator = mediator;
        this._problemDetailsFactory = problemDetailsFactory;
        this._mapper = mapper;
    }

    public override void Configure()
    {
        this.Post("auth/user/password");
    }

    public override async Task HandleAsync(ChangePasswordRequest request, CancellationToken ct)
    {
        var command = this._mapper.Map<ChangePasswordCommand>(request);

        var result = await this._mediator.Send(command, ct);

        if (result.IsError)
        {
            await result.SendProblemDetailsAsync(this.HttpContext, this._problemDetailsFactory);
            return;
        }

        await this.SendOkAsync(ct);
    }
}
