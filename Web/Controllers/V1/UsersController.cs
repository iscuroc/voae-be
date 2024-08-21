using Application.Features.Activities.Models;
using Application.Features.Activities.Queries;
using Application.Features.Users.Models;
using Application.Features.Users.Queries;
using Mediator;
using Microsoft.AspNetCore.Mvc;
using Web.Extensions;

namespace Web.Controllers.V1;

public class UsersController(ISender sender) : BaseController
{
    [HttpGet("me")]
    [ProducesResponseType<UserResponse>(StatusCodes.Status200OK)]
    public async Task<IResult> GetAsync(CancellationToken cancellationToken)
    {
        var result = await sender.Send(new GetCurrentUserQuery(), cancellationToken); 
        return result.IsSuccess ? Results.Ok(result.Value) : result.ToProblemDetails();
    }

    [HttpGet("my-requests")]
    [ProducesResponseType<List<MyActivityResponse>>(StatusCodes.Status200OK)]
    public async Task<IResult> GetRequiestsAsync(CancellationToken cancellationToken)
    {
        var result = await sender.Send(new GetMyRequestsQuery(), cancellationToken);
        return result.IsSuccess ? Results.Ok(result.Value) : result.ToProblemDetails();
    }
}
