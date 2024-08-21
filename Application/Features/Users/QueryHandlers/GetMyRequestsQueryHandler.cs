using Application.Contracts;
using Application.Features.Users.Mappers;
using Application.Features.Users.Models;
using Application.Features.Users.Queries;
using Application.Shared;
using Domain.Contracts;
using Mediator;
using Shared;

namespace Application.Features.Users.QueryHandlers;

public class GetMyRequestsQueryHandler(
    ICurrentUserService currentUser,
    IUserRepository userRepository
) : IQueryHandler<GetMyRequestsQuery, Result<List<MyActivityResponse>>>
{
    public async ValueTask<Result<List<MyActivityResponse>>> Handle(GetMyRequestsQuery query,
        CancellationToken cancellationToken)
    {
        var user = await currentUser.GetCurrentUserAsync(cancellationToken);

        var activities = await userRepository.GetRequestsAsync(
            user.Id,
            cancellationToken: cancellationToken
        );
        return activities.Select(activity => activity.ToMyActivityResponse()).ToList();
    }
}
