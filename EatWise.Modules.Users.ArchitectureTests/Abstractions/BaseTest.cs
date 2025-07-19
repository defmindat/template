using System.Reflection;
using EatWise.Users.Domain.Users;
using EatWise.Users.Infrastructure;

namespace EatWise.Modules.Users.ArchitectureTests.Abstractions;

#pragma warning disable CA1515
public abstract class BaseTest
{
    protected static readonly Assembly ApplicationAssembly = typeof(EatWise.Users.Application.AssemblyReference).Assembly;

    protected static readonly Assembly DomainAssembly = typeof(User).Assembly;

    protected static readonly Assembly InfrastructureAssembly = typeof(UsersModule).Assembly;

    protected static readonly Assembly PresentationAssembly = typeof(EatWise.Users.Presentation.AssemblyReference).Assembly;
}
#pragma warning restore CA1515
