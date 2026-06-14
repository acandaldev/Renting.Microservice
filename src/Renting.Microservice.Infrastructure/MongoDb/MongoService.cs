using Microsoft.Extensions.Options;
using MongoDB.Driver;
using Renting.Microservice.Infrastructure.MongoDb.Settings;

namespace Renting.Microservice.Infrastructure.MongoDb
{
    public class MongoService
    {
        public MongoService(IOptions<MongoDbSettings> options)
        {
            MongoClient = new MongoClient(options.Value.ConnectionString);
            Database = MongoClient.GetDatabase(options.Value.MongoDbDatabaseName);
        }

        public MongoClient MongoClient { get; }

        public IMongoDatabase Database { get; }
    }
}
