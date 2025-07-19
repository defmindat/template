using EatWise.Harvester.ArchitectureTesting.Abstractions;
using MassTransit;
using NetArchTest.Rules;
using Xunit;

namespace EatWise.Harvester.ArchitectureTesting.PresentationTests;

public class PresentationTests : BaseTest
{
    [Fact]
    public void IntegrationEventHandler_Should_BeSealed()
    {
        Types.InAssembly(PresentationAssembly)
            .That()
            .ImplementInterface(typeof(IConsumer<>))
            .Should()
            .BeSealed()
            .GetResult()
            .ShouldBeSuccessful();
    }

    [Fact]
    public void IntegrationEventHandler_ShouldHave_NameEndingWith_DomainEventHandler()
    {
        Types.InAssembly(PresentationAssembly)
            .That()
            .ImplementInterface(typeof(IConsumer<>))
            .Should()
            .HaveNameEndingWith("Consumer")
            .GetResult()
            .ShouldBeSuccessful();
    }
}
