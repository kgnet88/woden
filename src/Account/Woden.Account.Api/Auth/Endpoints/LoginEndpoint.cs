using KgNet88.Woden.Account.Application.Auth.Queries.Login;

using MediatR;

using Microsoft.AspNetCore.Mvc.Infrastructure;

namespace KgNet88.Woden.Account.Api.Auth.Endpoints;

public class LoginEndpoint : Endpoint<LoginRequest, LoginResponse>
{
    private readonly ISender _mediator;
    private readonly ProblemDetailsFactory _problemDetailsFactory;

    public LoginEndpoint(ISender mediator, ProblemDetailsFactory problemDetailsFactory)
    {
        this._mediator = mediator;
        this._problemDetailsFactory = problemDetailsFactory;
    }

    public override void Configure()
    {
        this.Post("auth/login");
        this.AllowAnonymous();
    }

    public override async Task HandleAsync(LoginRequest request, CancellationToken ct)
    {
        var query = new LoginQuery()
        {
            Username = request.Username,
            Password = request.Password
        };

        var result = await this._mediator.Send(query, ct);

        if (result.IsError)
        {
            await result.SendProblemDetailsAsync(this.HttpContext, this._problemDetailsFactory);
            return;
        }

        var response = new LoginResponse()
        {
            AccessToken = result.Value.Token,
            ValidTo = result.Value.ValidTo.ToInstant()
        };

        await this.SendOkAsync(response, ct);
    }
}
