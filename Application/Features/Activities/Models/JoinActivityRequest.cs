using Domain.Enums;

namespace Application.Features.Activities.Models;

public record JoinActivityRequest(
    List<ActivityScopes> Scopes
    );