using System.Collections;
using Application.Features.Careers.Models;
using Domain.Entities;

namespace Application.Features.Careers.Extensions;

public static class CareerMapper
{
    public static CareerResponse ToResponse(this Career career)
    {
        return new CareerResponse
        {
            Id = career.Id,
            Name = career.Name,
            Faculty = new FacultyResponse
            {
                Id = career.Faculty.Id,
                Name = career.Faculty.Name
            }
        };
    }
    
    public static List<CareerResponse> ToResponse(this IEnumerable<Career> careers)
    {
        return careers.Select(career => career.ToResponse()).ToList();
    }
}