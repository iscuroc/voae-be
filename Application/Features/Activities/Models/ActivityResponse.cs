using Application.Features.Authentication.Models;
using Domain.Entities;
using Domain.Enums;

namespace Application.Features.Activities.Models;

public record ActivityResponse(
    int Id,
    string Name,
    string Description,
    string Location,
    string MainActivities,
    string Objectives,
    Career MainCareer,
    DateTime StartDate,
    DateTime EndDate,
    int TotalSpots,
    string? BannerLink,
    ICollection<ActivityScopeResponse> Scopes,
    ICollection<CareerResponse> ForeingCareers,
    int TeacherId,
    UserResponse Teacher,
    UserResponse Student,
    ActivityStatus ActivityStatus,
    DateTime RequestedAt,
    DateTime? ReviewDate
);