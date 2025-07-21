using EatWise.Common.Application.Messaging;

namespace EatWise.Users.Application.Users.RegisterUser;

public sealed record RegisterUserCommand(string Email, string Password, string FirstName, string LastName) : ICommand<Guid>;
