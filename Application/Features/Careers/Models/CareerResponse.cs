namespace Application.Features.Careers.Models;

public record struct CareerResponse(
    int Id,
    string Name,
    FacultyResponse Faculty
);

public record struct FacultyResponse(
    int Id,
    string Name
);