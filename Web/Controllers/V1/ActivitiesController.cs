using Application.Features.Activities.Commands;
using Application.Features.Activities.Models;
using Application.Features.Activities.Queries;
using Mediator;
using Microsoft.AspNetCore.Mvc;
using Web.Extensions;

namespace Web.Controllers.V1;

public class ActivitiesController(ISender sender) : BaseController
{
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IResult> PostAsync([FromBody] CreateActivityCommand command, CancellationToken cancellationToken)
    {
        var result = await sender.Send(command, cancellationToken);
        return result.IsSuccess ? Results.Ok() : result.ToProblemDetails();
    }

    [HttpGet]
    [ProducesResponseType<List<ActivityResponse>>(StatusCodes.Status200OK)]
    public async Task<IResult> GetAsync([FromQuery] GetAllActivitiesQuery query, CancellationToken cancellationToken)
    {
        var result = await sender.Send(query, cancellationToken);
        return result.IsSuccess ? Results.Ok(result.Value) : result.ToProblemDetails();
    }
    
    [HttpGet("by-slug/{slug}")]
    [ProducesResponseType<ActivityResponse>(StatusCodes.Status200OK)]
    public async Task<IResult> GetBySlugAsync(string slug, CancellationToken cancellationToken)
    {
        var result = await sender.Send(new GetActivityBySlugQuery(slug), cancellationToken);

        return result.IsSuccess ? Results.Ok(result.Value) : result.ToProblemDetails();
    }

    [HttpPost("reject/{id:int}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IResult> PostAsync(int id, [FromBody] RejectActivityRequest request, CancellationToken cancellationToken)
    {
        var command = new RejectActivityCommand(id.ToString(), request.ReviewerObservation);
        var result = await sender.Send(command, cancellationToken);
        return result.IsSuccess ? Results.Ok() : result.ToProblemDetails();
    }
}