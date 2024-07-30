using Application.Features.Careers.Models;
using Application.Features.Careers.Queries;
using Mediator;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Web.Extensions;

namespace Web.Controllers.V1;

[AllowAnonymous]
public class CareersController(ISender sender) : BaseController
{
    [HttpGet]
    [ProducesResponseType<List<CareerResponse>>(StatusCodes.Status200OK)]
    public async Task<IResult> GetAsync(CancellationToken cancellationToken)
    {
        var result = await sender.Send(new GetCareersQuery(), cancellationToken);
        
        return result.IsSuccess ? Results.Ok(result.Value) : result.ToProblemDetails();
    }

    [HttpGet("{id}/students")]
    [ProducesResponseType<List<StudentResponse>>(StatusCodes.Status200OK)]
    public async Task<IResult> GetStudentsByIdAsync(int id, [FromQuery] string? query, CancellationToken cancellationToken)
    {
        var result = await sender.Send(new GetStudentsByIdQuery(id, query), cancellationToken);

        return result.IsSuccess ? Results.Ok(result.Value) : result.ToProblemDetails();
    }

}