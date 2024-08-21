using Domain.Enums;

namespace Application.Features.Users.Models;

public record UserActivitiesResponse(
    int Id,
    string Name,
    string Description,
    DateTime StartDate,
    DateTime EndDate,
    string ActivityStatus
);