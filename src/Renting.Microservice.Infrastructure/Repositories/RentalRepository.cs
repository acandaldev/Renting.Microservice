using System;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Driver;
using Renting.Microservice.Domain.Entities;
using Renting.Microservice.Domain.Interfaces;
using Renting.Microservice.Domain.ValueObjects;
using Renting.Microservice.Infrastructure.MongoDb;

namespace Renting.Microservice.Infrastructure.Repositories
{
    public sealed class RentalRepository : IRentalRepository
    {
        private const string CollectionName = "rentals";
        private readonly IMongoCollection<BsonDocument> collection;

        public RentalRepository(MongoService mongoService)
        {
            ArgumentNullException.ThrowIfNull(mongoService);
            collection = mongoService.Database.GetCollection<BsonDocument>(CollectionName);
        }

        public async Task Add(Rental rental)
        {
            ArgumentNullException.ThrowIfNull(rental);

            var doc = new BsonDocument
            {
                { "_id", rental.Id.ToString() },
                { "vehicleId", rental.VehicleId.Value.ToString() },
                { "renterId", rental.RenterId.Value },
                { "startDate", rental.StartDate },
                { "isActive", rental.IsActive },
            };

            await collection.InsertOneAsync(doc);
        }

        public async Task<bool> HasActiveRental(RenterId renterId)
        {
            ArgumentNullException.ThrowIfNull(renterId);

            var filter = Builders<BsonDocument>.Filter.Eq("renterId", renterId.Value)
                         & Builders<BsonDocument>.Filter.Eq("isActive", true);

            return await collection.Find(filter).AnyAsync();
        }

        public async Task<Rental> GetActiveByVehicleId(Guid vehicleId)
        {
            var filter = Builders<BsonDocument>.Filter.Eq("vehicleId", vehicleId.ToString())
                         & Builders<BsonDocument>.Filter.Eq("isActive", true);

            var doc = await collection.Find(filter).FirstOrDefaultAsync();

            return doc == null ? null : MapToRental(doc);
        }

        public async Task Update(Rental rental)
        {
            ArgumentNullException.ThrowIfNull(rental);

            var filter = Builders<BsonDocument>.Filter.Eq("_id", rental.Id.ToString());
            var update = Builders<BsonDocument>.Update
                .Set("isActive", rental.IsActive)
                .Set("endDate", rental.EndDate);

            await collection.UpdateOneAsync(filter, update);
        }

        private static Rental MapToRental(BsonDocument doc)
        {
            var rental = new Rental(
                Guid.Parse(doc["_id"].AsString),
                new VehicleId(Guid.Parse(doc["vehicleId"].AsString)),
                new RenterId(doc["renterId"].AsString));

            if (!doc["isActive"].AsBoolean)
            {
                rental.Complete();
            }

            return rental;
        }
    }
}
