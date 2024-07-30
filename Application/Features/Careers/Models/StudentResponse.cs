namespace Application.Features.Careers.Models;

public record struct StudentResponse(
    int Id,
    string Names,
    string Lastnames,
    string Email,
    long AccountNumber,
    int CareerId
);