﻿using MongoDB.Driver;
using System.Linq;
using System.Threading.Tasks;

namespace ToDo.Common.Mongo
{
    public class MongoDbSeeder : IMongoDbSeeder
    {
        protected readonly IMongoDatabase Database;

        public MongoDbSeeder(IMongoDatabase database)
        {
            Database = database;
        }

        public async Task SeedAsync()
        {

            await CustomSeedAsync();
        }

        protected virtual async Task CustomSeedAsync()
        {
            var cursor = await Database.ListCollectionsAsync();
            var collections = await cursor.ToListAsync();
            if (collections.Any())
            {
                return;
            }
            await Task.CompletedTask;
        }
    }
}