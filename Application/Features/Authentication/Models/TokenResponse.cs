using Domain.Enums;

namespace Application.Features.Authentication.Models;

public record TokenResponse(string Email, Role Role, string AccessToken);