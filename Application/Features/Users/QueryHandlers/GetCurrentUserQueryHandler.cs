using Mediator;
using Application.Features.Users.Models;
using Shared;
using Application.Contracts;

namespace Application.Features.Users.Queries
{
    public class GetCurrentUserQueryHandler : IQueryHandler<GetCurrentUserQuery, Result<List<UserResponse>>>
    {
        private readonly ICurrentUserService _currentUser;

        public GetCurrentUserQueryHandler(ICurrentUserService currentUser)
        {
            _currentUser = currentUser;
        }

        public async ValueTask<Result<List<UserResponse>>> Handle(GetCurrentUserQuery query, CancellationToken cancellationToken)
        {
            var user = await _currentUser.GetCurrentUserAsync();
            
            if (user is null)
            {
                //return Result<List<UserResponse>>.Failure("Usuario no encontrado");
            }

            return Result<List<UserResponse>>.Success(new List<UserResponse> { user.Value });
        }
    }
}
