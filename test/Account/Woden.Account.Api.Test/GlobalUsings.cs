global using System.Net;
global using System.Net.Http.Headers;
global using System.Net.Http.Json;
global using System.Text.Json;

global using FastEndpoints;

global using FluentAssertions;

global using KgNet88.Woden.Account.Api.Auth.Contracts.Requests;
global using KgNet88.Woden.Account.Api.Auth.Contracts.Responses;
global using KgNet88.Woden.Account.Api.Auth.Infrastructure.Database;
global using KgNet88.Woden.Account.Api.Auth.Services;

global using Microsoft.AspNetCore.Hosting;
global using Microsoft.AspNetCore.Mvc.Testing;
global using Microsoft.EntityFrameworkCore;
global using Microsoft.Extensions.Configuration;
global using Microsoft.Extensions.DependencyInjection;
global using Microsoft.Extensions.Logging;

global using NodaTime;
global using NodaTime.Serialization.SystemTextJson;

global using Xunit;
