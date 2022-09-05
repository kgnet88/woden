using KgNet88.Woden.Account.Application.Auth.Queries.Login;

using MediatR;

using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.ModelBinding;

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
            var modelStateDictionary = new ModelStateDictionary();

            foreach (var failure in result.Errors)
            {
                modelStateDictionary.AddModelError(failure.Code, failure.Description);
            }

            var problemDetails = this._problemDetailsFactory.CreateValidationProblemDetails(this.HttpContext, modelStateDictionary);

            this.HttpContext.Response.StatusCode = (int)problemDetails.Status!;

            await this.HttpContext.Response.WriteAsJsonAsync(problemDetails, options: null, contentType: "application/problem+json");

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
