namespace EatWise.ArchitectureTests.Abstractions;

#pragma warning disable CA1515
public abstract class BaseTest
{
    protected const string UsersNamespace = "EatWise.Modules.Users";
    protected const string UsersIntegrationEventsNamespace = "EatWise.Modules.Users.IntegrationEvents";

    protected const string HarvesterNamespace = "EatWise.Modules.Harvester";
    protected const string HarvesterIntegrationEventsNamespace = "EatWise.Modules.Harvester.IntegrationEvents";
}
#pragma warning restore CA1515
