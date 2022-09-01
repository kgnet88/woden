namespace Goedde88.Woden.Account.Api.Middleware;

public class ValidationFailureResponse
{
    public List<string> Errors { get; init; } = new();
}