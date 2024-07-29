﻿using Application.Features.Authentication.Commands;
using Application.Features.Authentication.Models;
using Mediator;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Web.Extensions;

namespace Web.Controllers.V1;

[AllowAnonymous]
public class AuthController(ISender sender) : BaseController
{
    [HttpPost("register")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IResult> PostAsync(RegisterUserCommand command)
    {
        var result = await sender.Send(command);
        return result.IsSuccess ? Results.Ok() : result.ToProblemDetails();
    }
    
    [HttpPost("confirm")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IResult> PostAsync(ConfirmUserCommand command)
    {
        var result = await sender.Send(command);
        return result.IsSuccess ? Results.Ok() : result.ToProblemDetails();
    }
    
    [HttpPost("login")]
    [ProducesResponseType<TokenResponse>(StatusCodes.Status200OK)]
    public async Task<IResult> PostAsync(LoginCommand command)
    {
        var result = await sender.Send(command);
        return result.IsSuccess ? Results.Ok(result.Value) : result.ToProblemDetails();
    }
    
    
    [HttpPost("forgot-password")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IResult> PostAsync(ForgotPasswordCommand command)
    {
        var result = await sender.Send(command);
        return result.IsSuccess ? Results.Ok() : result.ToProblemDetails();
    }
    
    [HttpPost("reset-password")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IResult> PostAsync(ResetPasswordCommand command)
    {
        var result = await sender.Send(command);
        return result.IsSuccess ? Results.Ok() : result.ToProblemDetails();
    }
}