global using System.Text.Json;
global using System.Text.Json.Serialization;

global using FastEndpoints;
global using FastEndpoints.Security;
global using FastEndpoints.Swagger;

global using FluentValidation;
global using FluentValidation.Results;

global using Goedde88.Woden.Account.Api.Auth.Contracts.Data;
global using Goedde88.Woden.Account.Api.Auth.Contracts.Requests;
global using Goedde88.Woden.Account.Api.Auth.Contracts.Responses;
global using Goedde88.Woden.Account.Api.Auth.Domain;
global using Goedde88.Woden.Account.Api.Auth.Infrastructure.Configuration;
global using Goedde88.Woden.Account.Api.Auth.Infrastructure.Database;
global using Goedde88.Woden.Account.Api.Auth.Infrastructure.Database.Model;
global using Goedde88.Woden.Account.Api.Auth.Mappings;
global using Goedde88.Woden.Account.Api.Auth.Repositories;
global using Goedde88.Woden.Account.Api.Auth.Services;
global using Goedde88.Woden.Account.Api.Middleware;

global using Microsoft.AspNetCore.Identity;
global using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
global using Microsoft.EntityFrameworkCore;

global using NodaTime;
global using NodaTime.Serialization.SystemTextJson;
