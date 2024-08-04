namespace Application.Features.Careers.Models;

public record struct TeacherResponse(
    int Id,
    string Names,
    string LastNames,
    int CareerId
);