using Application.Contracts;
using Application.Features.Users.Mappers;
using Application.Features.Users.Models;
using Application.Features.Users.Queries;
using Application.Shared;
using Domain.Contracts;
using Mediator;
using Shared;

namespace Application.Features.Users.QueryHandlers;

public record GetUsersByRoleQueryHandler(
    IUserRepository UserRepository
) : IQueryHandler<GetUsersByRoleQuery, Result<PaginatedList<UsersByRoleResponse>>>
{
    public async ValueTask<Result<PaginatedList<UsersByRoleResponse>>> Handle(GetUsersByRoleQuery query,
        CancellationToken cancellationToken)
    {
        var users = await UserRepository.GetPagedAsync(
            query.Filters,
            cancellationToken
        );

        var totalUsers = await UserRepository.CountAsync(
            query.Filters,
            cancellationToken
        );

        var usersResponse = users.ToSimpleResponse();

        var paginatedList = new PaginatedList<UsersByRoleResponse>(
            usersResponse,
            totalUsers,
            query.Filters.PageNumber,
            query.Filters.PageSize
        );

        return paginatedList;
    }
}