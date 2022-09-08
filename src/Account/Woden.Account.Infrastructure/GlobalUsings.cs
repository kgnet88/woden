global using System.IdentityModel.Tokens.Jwt;
global using System.Security.Claims;

global using FastEndpoints.Security;

global using KgNet88.Woden.Account.Application.Common.Interfaces;
global using KgNet88.Woden.Account.Application.Users.Interfaces;
global using KgNet88.Woden.Account.Domain.Users.Entities;
global using KgNet88.Woden.Account.Infrastructure.Common.Implementations;
global using KgNet88.Woden.Account.Infrastructure.Users.Implementations;
global using KgNet88.Woden.Account.Infrastructure.Users.Persistence.Database;
global using KgNet88.Woden.Account.Infrastructure.Users.Persistence.Database.Model;

global using Microsoft.AspNetCore.Identity;
global using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
global using Microsoft.EntityFrameworkCore;
global using Microsoft.Extensions.Configuration;
global using Microsoft.Extensions.DependencyInjection;
global using Microsoft.Extensions.Options;

global using NodaTime;
global using NodaTime.Extensions;
