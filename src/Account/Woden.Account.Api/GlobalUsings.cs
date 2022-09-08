global using System.Diagnostics;
global using System.Reflection;
global using System.Text.Json;
global using System.Text.Json.Serialization;

global using ErrorOr;

global using FastEndpoints;
global using FastEndpoints.Swagger;

global using KgNet88.Woden.Account.Api.Auth.Mapping;
global using KgNet88.Woden.Account.Api.Middleware;
global using KgNet88.Woden.Account.Application;
global using KgNet88.Woden.Account.Application.Auth.Commands.ChangeEmail;
global using KgNet88.Woden.Account.Application.Auth.Commands.ChangePassword;
global using KgNet88.Woden.Account.Application.Auth.Commands.DeleteUserByName;
global using KgNet88.Woden.Account.Application.Auth.Commands.Register;
global using KgNet88.Woden.Account.Application.Auth.Queries.GetUserInfo;
global using KgNet88.Woden.Account.Application.Auth.Queries.Login;
global using KgNet88.Woden.Account.Contracts.Auth.Requests;
global using KgNet88.Woden.Account.Contracts.Auth.Responses;
global using KgNet88.Woden.Account.Infrastructure;

global using Mapster;

global using MapsterMapper;

global using MediatR;

global using Microsoft.AspNetCore.Mvc;
global using Microsoft.AspNetCore.Mvc.Infrastructure;
global using Microsoft.AspNetCore.Mvc.ModelBinding;
global using Microsoft.Extensions.Options;

global using NodaTime;
global using NodaTime.Serialization.SystemTextJson;
