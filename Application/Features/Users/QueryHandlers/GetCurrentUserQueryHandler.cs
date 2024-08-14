using Mediator;
using Application.Features.Users.Models;
using Shared;
using Application.Contracts;
using Application.Features.Users.Mappers;

namespace Application.Features.Users.Queries
{
    public class GetCurrentUserQueryHandler : IQueryHandler<GetCurrentUserQuery, Result<UserResponse>>
    {
        private readonly ICurrentUserService _currentUser;

        public GetCurrentUserQueryHandler(ICurrentUserService currentUser)
        {
            _currentUser = currentUser;
        }

        public async ValueTask<Result<UserResponse>> Handle(GetCurrentUserQuery query, CancellationToken cancellationToken)
        {
            var user = await _currentUser.GetCurrentUserAsync();

            return Result<UserResponse>.Success(user.ToResponse());
        }
    }
}
