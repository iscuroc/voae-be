using Application.Contracts;
using Application.Features.Users.Mappers;
using Application.Features.Users.Models;
using Application.Features.Users.Queries;
using Mediator;
using Shared;

namespace Application.Features.Users.QueryHandlers
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

            return user.ToResponse();
        }
    }
}
