using Xunit;

namespace Renting.Microservice.InfrastructureTests.Infrastructure
{
    [CollectionDefinition(TestCollections.TestServer)]
    public class TestServerCollectionFixture : ICollectionFixture<GenericInfrastructureTestServerFixture>
    {
    }
}
