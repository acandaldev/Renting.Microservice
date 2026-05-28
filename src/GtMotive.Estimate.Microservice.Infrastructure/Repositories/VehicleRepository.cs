using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GtMotive.Estimate.Microservice.Domain.Entities;
using GtMotive.Estimate.Microservice.Domain.Interfaces;
using GtMotive.Estimate.Microservice.Domain.ValueObjects;
using GtMotive.Estimate.Microservice.Infrastructure.MongoDb;
using MongoDB.Bson;
using MongoDB.Driver;

namespace GtMotive.Estimate.Microservice.Infrastructure.Repositories
{
    public sealed class VehicleRepository : IVehicleRepository
    {
        private const string CollectionName = "vehicles";
        private readonly IMongoCollection<BsonDocument> collection;

        public VehicleRepository(MongoService mongoService)
        {
            ArgumentNullException.ThrowIfNull(mongoService);
            collection = mongoService.Database.GetCollection<BsonDocument>(CollectionName);
        }

        public async Task Add(Vehicle vehicle)
        {
            ArgumentNullException.ThrowIfNull(vehicle);

            var doc = new BsonDocument
            {
                { "_id", vehicle.Id.Value.ToString() },
                { "licensePlate", vehicle.LicensePlate.Value },
                { "brand", vehicle.Brand.Value },
                { "model", vehicle.Model.Value },
                { "manufactureDate", vehicle.ManufactureDate.Value },
                { "isAvailable", vehicle.IsAvailable },
            };

            await collection.InsertOneAsync(doc);
        }

        public async Task<Vehicle> GetById(Guid id)
        {
            var filter = Builders<BsonDocument>.Filter.Eq("_id", id.ToString());
            var doc = await collection.Find(filter).FirstOrDefaultAsync();

            return doc == null ? null : MapToVehicle(doc);
        }

        public async Task<IReadOnlyList<Vehicle>> GetAvailable()
        {
            var filter = Builders<BsonDocument>.Filter.Eq("isAvailable", true);
            var docs = await collection.Find(filter).ToListAsync();

            return docs.Select(MapToVehicle).ToList().AsReadOnly();
        }

        public async Task Update(Vehicle vehicle)
        {
            ArgumentNullException.ThrowIfNull(vehicle);

            var filter = Builders<BsonDocument>.Filter.Eq("_id", vehicle.Id.Value.ToString());
            var update = Builders<BsonDocument>.Update
                .Set("isAvailable", vehicle.IsAvailable);

            await collection.UpdateOneAsync(filter, update);
        }

        private static Vehicle MapToVehicle(BsonDocument doc)
        {
            var vehicle = new Vehicle(
                new VehicleId(Guid.Parse(doc["_id"].AsString)),
                new LicensePlate(doc["licensePlate"].AsString),
                new Brand(doc["brand"].AsString),
                new Domain.ValueObjects.Model(doc["model"].AsString),
                new ManufactureDate(doc["manufactureDate"].ToUniversalTime()));

            if (!doc["isAvailable"].AsBoolean)
            {
                vehicle.MarkAsRented();
            }

            return vehicle;
        }
    }
}
