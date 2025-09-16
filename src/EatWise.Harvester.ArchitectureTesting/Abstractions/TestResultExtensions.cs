using FluentAssertions;
using NetArchTest.Rules;

namespace EatWise.Harvester.ArchitectureTesting.Abstractions;

internal static class TestResultExtensions
{
    internal static void ShouldBeSuccessful(this TestResult testResult)
    {
        testResult.FailingTypes?.Should().BeEmpty();
    }
}
