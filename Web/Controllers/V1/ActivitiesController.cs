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
    public async Task<IResult> PostAsync([FromBody] CreateActivityCommand command)
    {
        var result = await sender.Send(command);
        return result.IsSuccess ? Results.Ok() : result.ToProblemDetails();
    }

    [HttpGet]
    [ProducesResponseType<List<ActivityResponse>>(StatusCodes.Status200OK)]
    public async Task<IResult> GetAsync([FromQuery] GetAllActivitiesQuery query)
    {
        var result = await sender.Send(query);
        return result.IsSuccess ? Results.Ok(result.Value) : result.ToProblemDetails();
    }
}