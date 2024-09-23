using Application.Features.Activities.Models;
using Application.Features.Users.Models;
using Domain.Entities;
using Domain.Enums;

namespace Application.Features.Users.Mappers;

public static class UserActivityMapper
{
    public static UserActivitiesResponse ToResponse(this ActivityMember member)
    {
        return  new UserActivitiesResponse(
            Id: member.ActivityId,
            Name: member.Activity.Name,
            Description: member.Activity.Description,
            MemberScopes: member.Scopes.Select(scope =>
                new ActivitiesScopeResponse(
                    Scope: scope.MemberScope,
                    Hours: scope.Hours
                )).ToList(),
            ActivityScopes: member.Activity.Scopes.Select(scope =>
                new ActivitiesScopeResponse(
                    Scope: scope.Scope,
                    Hours: scope.Hours
                )).ToList(),
            StartDate: member.Activity.StartDate,
            EndDate: member.Activity.EndDate,
            Slug: member.Activity.Slug,
            ActivityStatus: member.Activity.ActivityStatus
        );
    }

    public static List<UserActivitiesResponse> ToResponse(this IEnumerable<ActivityMember> members)
    {
        return members.Select(ToResponse).ToList();
    }
}