using KgNet88.Woden.Account.Application.Auth.Commands.DeleteUserByName;

using MediatR;

using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace KgNet88.Woden.Account.Api.Auth.Endpoints;

public class DeleteUserByNameEndpoint : EndpointWithoutRequest
{
    private readonly ISender _mediator;
    private readonly ProblemDetailsFactory _problemDetailsFactory;

    public DeleteUserByNameEndpoint(ISender mediator, ProblemDetailsFactory problemDetailsFactory)
    {
        this._mediator = mediator;
        this._problemDetailsFactory = problemDetailsFactory;
    }

    public override void Configure()
    {
        this.Delete("auth/user/{Username}");
    }

    public override async Task HandleAsync(CancellationToken ct)
    {
        string username = this.Route<string>("Username") ?? "";

        var command = new DeleteUserByNameCommand()
        {
            Username = username,
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