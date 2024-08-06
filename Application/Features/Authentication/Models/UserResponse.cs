using Domain.Enums;

namespace Application.Features.Authentication.Models;

public record UserResponse(
    int Id,
    string Names,
    string LastNames,
    Role Role
);