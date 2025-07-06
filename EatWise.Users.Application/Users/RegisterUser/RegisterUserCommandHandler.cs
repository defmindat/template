using EatWise.Common.Application.Contracts;
using EatWise.Common.Domain;
using EatWise.Users.Application.Abstractions.Data;
using EatWise.Users.Application.Abstractions.Identity;
using EatWise.Users.Domain.Users;

namespace EatWise.Users.Application.Users.RegisterUser;

internal sealed class RegisterUserCommandHandler(
    IIdentityProviderService identityProviderService,
    IUserRepository userRepository,
    IUnitOfWork unitOfWork)
    : ICommandHandler<RegisterUserCommand, Guid>
{
    public async Task<Result<Guid>> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
    {
        Result<string> result = await identityProviderService.RegisterUserAsync(
            new UserModel(request.Email, request.Password, request.FirstName, request.LastName),
            cancellationToken);

        if (result.IsFailure)
        {
            return Result.Failure<Guid>(result.Error);
        }
        
        var user = User.Create(request.Email, request.FirstName, request.LastName, result.Value);
        userRepository.Insert(user);
        
        await unitOfWork.SaveChangesAsync(cancellationToken);

        return user.Id;
    }
}
