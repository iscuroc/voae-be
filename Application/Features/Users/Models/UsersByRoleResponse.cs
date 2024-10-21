using Domain.Enums;

namespace Application.Features.Users.Models;

public record UsersByRoleResponse(
    int Id,
    string? Names,
    string Email,
    Role Role,
    string? CareerName
);