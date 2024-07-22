using Application.Features.Authentication.Commands;
using Mediator;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Web.Extensions;

namespace Web.Controllers.V1;

[AllowAnonymous]
public class PasswordResetController(ISender sender) : BaseController
{
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IResult> PostAsync(ResetPasswordCommand command)
    {
        var result = await sender.Send(command);
        return result.IsSuccess ? Results.Ok() : result.ToProblemDetails();
    }
}