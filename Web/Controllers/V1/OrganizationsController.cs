using Application.Features.Organizations.Models;
using Application.Features.Organizations.Queries;
using Mediator;
using Microsoft.AspNetCore.Mvc;
using Web.Extensions;

namespace Web.Controllers.V1;

public class OrganizationsController(ISender sender) : BaseController
{
    [HttpGet]
    [ProducesResponseType<List<OrganizationResponse>>(StatusCodes.Status200OK)]
    public async Task<IResult> GetAsync(CancellationToken cancellationToken)
    {
        var result = await sender.Send(new GetOrganizationsQuery(), cancellationToken);
        return result.IsSuccess ? Results.Ok(result.Value) : result.ToProblemDetails();
    }
}