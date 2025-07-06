using EatWise.Common.Application.Contracts;

namespace EatWise.Users.Application.Users.UpdateUser;

public sealed record UpdateUserCommand(Guid UserId, string FirstName, string LastName): ICommand;

