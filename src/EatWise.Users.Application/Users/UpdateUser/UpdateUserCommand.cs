using EatWise.Common.Application.Messaging;

namespace EatWise.Users.Application.Users.UpdateUser;

public sealed record UpdateUserCommand(Guid UserId, string FirstName, string LastName): ICommand;

