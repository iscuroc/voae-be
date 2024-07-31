using System.Collections;
using Application.Features.Careers.Models;
using Domain.Entities;
using Domain.Enums;


namespace Application.Features.Careers.Extensions
{
    public static class TeacherMapper
    {
        public static TeacherResponse ToResponse(this User user)
        {
            return new TeacherResponse
            {
                Id = user.Id,
                Names = user.Names ,
                LastNames = user.Lastnames,
                CareerId = (int)user.CareerId
            };
        }
        
        public static List<TeacherResponse> ToResponse(this IEnumerable<User> users)
        {
            return users
                .Select(user => user.ToResponse())
                .ToList();
        }
    }
}