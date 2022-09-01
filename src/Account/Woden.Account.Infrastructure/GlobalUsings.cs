global using System.IdentityModel.Tokens.Jwt;
global using System.Security.Claims;

global using FastEndpoints.Security;

global using FluentValidation;
global using FluentValidation.Results;

global using KgNet88.Woden.Account.Application.Interfaces.Auth;
global using KgNet88.Woden.Account.Application.Interfaces.Common;
global using KgNet88.Woden.Account.Infrastructure.Auth;
global using KgNet88.Woden.Account.Infrastructure.Auth.Database;
global using KgNet88.Woden.Account.Infrastructure.Auth.Database.Model;
global using KgNet88.Woden.Account.Infrastructure.Auth.Repositories;
global using KgNet88.Woden.Account.Infrastructure.Common;

global using Microsoft.AspNetCore.Identity;
global using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
global using Microsoft.EntityFrameworkCore;
global using Microsoft.Extensions.Configuration;
global using Microsoft.Extensions.DependencyInjection;

global using NodaTime;
global using NodaTime.Extensions;
