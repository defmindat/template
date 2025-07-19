using System.Reflection;
using EatWise.Harvester.Infrastructure;
using EatWise.Recipes;

namespace EatWise.Harvester.ArchitectureTesting.Abstractions;

#pragma warning disable CA1515
public abstract class BaseTest
{
    protected static readonly Assembly ApplicationAssembly = typeof(EatWise.Harvester.Application.AssemblyReference).Assembly;

    protected static readonly Assembly DomainAssembly = typeof(Recipe).Assembly;

    protected static readonly Assembly InfrastructureAssembly = typeof(HarvesterModule).Assembly;

    protected static readonly Assembly PresentationAssembly = typeof(Presentation.AssemblyReference).Assembly;
}
#pragma warning restore CA1515
