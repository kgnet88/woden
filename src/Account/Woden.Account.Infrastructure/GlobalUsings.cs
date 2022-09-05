global using System.IdentityModel.Tokens.Jwt;
global using System.Security.Claims;

global using ErrorOr;

global using FastEndpoints.Security;

global using KgNet88.Woden.Account.Application.Auth.Interfaces;
global using KgNet88.Woden.Account.Application.Auth.Persistence;
global using KgNet88.Woden.Account.Application.Common.Interfaces;
global using KgNet88.Woden.Account.Domain.Auth.Entities;
global using KgNet88.Woden.Account.Domain.Auth.Errors;
global using KgNet88.Woden.Account.Infrastructure.Auth.Implementations;
global using KgNet88.Woden.Account.Infrastructure.Auth.Persistence;
global using KgNet88.Woden.Account.Infrastructure.Auth.Persistence.Database;
global using KgNet88.Woden.Account.Infrastructure.Auth.Persistence.Database.Model;
global using KgNet88.Woden.Account.Infrastructure.Common.Implementations;

global using Microsoft.AspNetCore.Identity;
global using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
global using Microsoft.EntityFrameworkCore;
global using Microsoft.Extensions.Configuration;
global using Microsoft.Extensions.DependencyInjection;
global using Microsoft.Extensions.Options;

global using NodaTime;
global using NodaTime.Extensions;
