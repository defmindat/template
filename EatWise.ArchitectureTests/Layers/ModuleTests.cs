using System.Reflection;
using EatWise.ArchitectureTests.Abstractions;
using EatWise.Harvester.Infrastructure;
using EatWise.Recipes;
using EatWise.Users.Domain.Users;
using EatWise.Users.Infrastructure;
using NetArchTest.Rules;
using Xunit;

namespace EatWise.ArchitectureTests.Layers;

public class ModuleTests : BaseTest
{
    [Fact]
    public void UserModule_ShouldNotHaveDependenciesOn_AnyOtherModule()
    {
        string[] otherModules = [HarvesterNamespace];
        string[] integrationEventsModules = [HarvesterIntegrationEventsNamespace];

        List<Assembly> userAssemblies =
        [
            typeof(User).Assembly,
            Users.Application.AssemblyReference.Assembly,
            Users.Presentation.AssemblyReference.Assembly,
            typeof(UsersModule).Assembly
        ];
        
        Types.InAssemblies(userAssemblies)
            .That()
            .DoNotHaveDependencyOnAny(integrationEventsModules)
            .Should()
            .NotHaveDependencyOnAny(otherModules)
            .GetResult()
            .ShouldBeSuccessful();
    }

    [Fact]
    public void HarvesterModule_ShouldNotHaveDependenciesOn_AnyOtherModule()
    {
        string[] otherModules = [UsersNamespace];
        string[] integrationEventsModules = [UsersIntegrationEventsNamespace];

        List<Assembly> harvesterAssemblies =
        [
            typeof(Recipe).Assembly,
            Harvester.Application.AssemblyReference.Assembly,
            Harvester.Presentation.AssemblyReference.Assembly,
            typeof(HarvesterModule).Assembly
        ];
        
        Types.InAssemblies(harvesterAssemblies)
            .That()
            .DoNotHaveDependencyOnAny(integrationEventsModules)
            .Should()
            .NotHaveDependencyOnAny(otherModules)
            .GetResult()
            .ShouldBeSuccessful();
    }
}
