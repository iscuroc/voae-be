using Application.Features.Authentication.Commands;
using Mediator;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Web.Extensions;

namespace Web.Controllers.V1;

[AllowAnonymous]
public class RegisterController(ISender sender) : BaseController
{
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IResult> PostAsync(RegisterUserCommand command)
    {
        var result = await sender.Send(command);
        return result.IsSuccess ? Results.Ok() : result.ToProblemDetails();
    }
}
