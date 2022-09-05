using ErrorOr;

using KgNet88.Woden.Account.Application.Auth.Commands.Register;

using MediatR;

using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace KgNet88.Woden.Account.Api.Auth.Endpoints;

public class RegisterEndpoint : Endpoint<RegisterRequest, ErrorOr<Created>>
{
    private readonly ISender _mediator;
    private readonly ProblemDetailsFactory _problemDetailsFactory;

    public RegisterEndpoint(ISender mediator, ProblemDetailsFactory problemDetailsFactory)
    {
        this._mediator = mediator;
        this._problemDetailsFactory = problemDetailsFactory;
    }

    public override void Configure()
    {
        this.Post("auth/register");
        this.AllowAnonymous();
    }

    public override async Task HandleAsync(RegisterRequest request, CancellationToken ct)
    {
        var command = new RegisterCommand()
        {
            Username = request.Username,
            Email = request.Email,
            Password = request.Password
        };

        var result = await this._mediator.Send(command, ct);

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

        await this.SendOkAsync(ct);

    }
}
