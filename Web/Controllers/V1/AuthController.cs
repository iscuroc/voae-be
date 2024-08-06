using Application.Features.Authentication.Commands;
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
    public async Task<IResult> PostAsync([FromBody] RegisterUserCommand command, CancellationToken cancellationToken)
    {
        var result = await sender.Send(command, cancellationToken);
        return result.IsSuccess ? Results.Ok() : result.ToProblemDetails();
    }
    
    [HttpPost("confirm")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IResult> PostAsync([FromBody] ConfirmUserCommand command, CancellationToken cancellationToken)
    {
        var result = await sender.Send(command, cancellationToken);
        return result.IsSuccess ? Results.Ok() : result.ToProblemDetails();
    }
    
    [HttpPost("login")]
    [ProducesResponseType<TokenResponse>(StatusCodes.Status200OK)]
    public async Task<IResult> PostAsync(LoginCommand command, CancellationToken cancellationToken)
    {
        var result = await sender.Send(command, cancellationToken);
        return result.IsSuccess ? Results.Ok(result.Value) : result.ToProblemDetails();
    }
    
    
    [HttpPost("forgot-password")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IResult> PostAsync([FromBody] ForgotPasswordCommand command, CancellationToken cancellationToken)
    {
        var result = await sender.Send(command, cancellationToken);
        return result.IsSuccess ? Results.Ok() : result.ToProblemDetails();
    }
    
    [HttpPost("reset-password")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IResult> PostAsync([FromBody] ResetPasswordCommand command, CancellationToken cancellationToken)
    {
        var result = await sender.Send(command, cancellationToken);
        return result.IsSuccess ? Results.Ok() : result.ToProblemDetails();
    }
}