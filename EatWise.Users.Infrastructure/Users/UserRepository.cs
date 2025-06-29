using EatWise.Users.Domain.Users;
using EatWise.Users.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;

namespace EatWise.Users.Infrastructure.Users;

internal sealed class UserRepository(UsersDbContext context) : IUserRepository
{
    public Task<User?> GetAsync(Guid id, CancellationToken cancellationToken = default) =>
        context.Users.SingleOrDefaultAsync(u => u.Id == id, cancellationToken);
    
    public void Insert(User user)
    {
        context.Users.Add(user);
    }
}

