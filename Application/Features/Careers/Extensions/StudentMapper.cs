using System.Collections;
using Application.Features.Careers.Models;
using Domain.Entities;


namespace Application.Features.Careers.Extensions
{
    public static class StudentMapper
    {
        public static StudentResponse ToResponse(this User user)
        {
            return new StudentResponse
            {
                Id = user.Id,
                Names = user.Names,
                Lastnames = user.Lastnames,
                Email = user.Email,
                AccountNumber = (long)user.AccountNumber,
                CareerId = (int)user.CareerId
            };
        }

        public static List<StudentResponse> ToResponse(this IEnumerable<User> users)
        {
            return users.Select(user => user.ToResponse()).ToList();
        }
    }
}