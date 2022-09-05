global using System.Net;
global using System.Net.Http.Headers;
global using System.Net.Http.Json;
global using System.Text.Json;

global using FluentAssertions;

global using KgNet88.Woden.Account.Contracts.Auth.Requests;
global using KgNet88.Woden.Account.Contracts.Auth.Responses;
global using KgNet88.Woden.Account.Domain.Auth.Errors;
global using KgNet88.Woden.Account.Infrastructure.Auth.Persistence.Database;

global using Microsoft.AspNetCore.Hosting;
global using Microsoft.AspNetCore.Mvc;
global using Microsoft.AspNetCore.Mvc.Testing;
global using Microsoft.EntityFrameworkCore;
global using Microsoft.Extensions.Configuration;
global using Microsoft.Extensions.DependencyInjection;
global using Microsoft.Extensions.Logging;

global using NodaTime;
global using NodaTime.Serialization.SystemTextJson;

global using Xunit;
