using Application.Features.Users.Models;
using Application.Shared;
using Domain.Enums;
using Mediator;
using Shared;

namespace Application.Features.Users.Queries;

public record GetMyRequestsQuery:  IQuery<Result<List<MyActivityResponse>>>;