using Application.Features.Authentication.Commands;
using Application.Features.Authentication.Models;
using Mediator;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Web.Extensions;

namespace Web.Controllers.V1;

[AllowAnonymous]
public class LoginController(ISender sender) : BaseController
{
    [HttpPost]
    [ProducesResponseType<TokenResponse>(StatusCodes.Status200OK)]
    public async Task<IResult> PostAsync(LoginCommand command)
    {
        var result = await sender.Send(command);
        return result.IsSuccess ? Results.Ok(result.Value) : result.ToProblemDetails();
    }
} 