using Application.Features.Careers.Models;
using Domain.Entities;

namespace Application.Mappers
{
    public static class UserCareerMapper
    {
        public static CareerResponse ToCareerResponse(this Career career)
        {
            return new CareerResponse(
                career.Id,
                career.Name,
                new FacultyResponse(career.Faculty.Id, career.Faculty.Name)
            );
        }
    }
}
