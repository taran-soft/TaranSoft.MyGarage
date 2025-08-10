namespace GarageSpace.IntegrationTests
{
    [CollectionDefinition("IntegrationTests Collection")]
    public class IntegrationTestsCollection : ICollectionFixture<DatabaseFixture>
    {
        // This class has no code, and is never created. Its purpose is just to be the place to apply [CollectionDefinition]
    }
}
