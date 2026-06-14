using Xunit;

namespace Renting.Microservice.FunctionalTests.Infrastructure
{
    [CollectionDefinition(TestCollections.Functional)]
    public class CompositionRootCollectionFixture : ICollectionFixture<CompositionRootTestFixture>
    {
    }
}
