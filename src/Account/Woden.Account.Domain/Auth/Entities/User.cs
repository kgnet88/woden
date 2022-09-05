﻿namespace KgNet88.Woden.Account.Domain.Auth.Entities;

public record User
{
    public required Guid Id { get; init; }
    public required string Username { get; init; }
    public required string Email { get; init; }
}
