using EatWise.Common.Application.Contracts;

namespace EatWise.Users.Application.Users.RegisterUser;

public sealed record RegisterUserCommand(string Email, string Password, string FirstName, string LastName) : ICommand<Guid>;
