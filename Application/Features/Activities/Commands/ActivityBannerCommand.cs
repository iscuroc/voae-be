using Mediator;
using Microsoft.AspNetCore.Http;
using Shared;

namespace Application.Features.Activities.Commands;

public record ActivityBannerCommand(
    int ActivityId,
    IFormFile Banner
) : ICommand<Result>;

public record UpdateBannerRequest(
    IFormFile Banner
);