using Application.Features.Users.Models;
using Application.Features.Users.Queries;
using Mediator;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Web.Extensions;

namespace Web.Controllers.V1;

public class UsersController(ISender sender) : BaseController
{
    [HttpGet("{id:int}/currentUser")]
    [ProducesResponseType<UserResponse>(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IResult> GetCurrentUserAsync(int id, [FromQuery] string? query, CancellationToken cancellationToken)
    {
        var result = await sender.Send(new GetCurrentUserQuery(id, query), cancellationToken);

        return result.IsSuccess ? Results.Ok(result.Value.FirstOrDefault()) : result.ToProblemDetails();
    }
}
