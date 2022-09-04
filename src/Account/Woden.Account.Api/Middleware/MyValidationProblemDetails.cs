using Microsoft.AspNetCore.Mvc;

namespace KgNet88.Woden.Account.Api.Middleware;

public class MyValidationProblemDetails : ProblemDetails
{
    public Dictionary<string, List<string>> Errors { get; init; } = new();
}
