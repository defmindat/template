using EatWise.Common.Application.Contracts;

namespace EatWise.Users.Application.Users.GetUser;

public sealed record GetUserQuery(Guid UserId) : IQuery<UserResponse>;

