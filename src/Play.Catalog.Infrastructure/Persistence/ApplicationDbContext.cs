using MongoDB.Driver;
using Play.Catalog.Application.Common.Interfaces;
using Play.Catalog.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Play.Catalog.Infrastructure.Persistence
{
    public class ApplicationDbContext : IApplicationDbContext
    {
        private const string CollectionName = "items";

        private readonly IMongoCollection<Item> Items;

        private readonly FilterDefinitionBuilder<Item> FilterBuilder = Builders<Item>.Filter;        

        public ApplicationDbContext()
        {
            var client = new MongoClient("mongodb://localhost:17017");
            var database = client.GetDatabase("Catalog");            
            this.Items = database.GetCollection<Item>(CollectionName); 
        }

        public async Task<IList<Item>> GetAllAsync()
        {
            return await this.Items.Find(FilterBuilder.Empty).ToListAsync();
        }

        public async Task<Item> GetAsync(Guid id)
        {
            FilterDefinition<Item>? filter = FilterBuilder.Eq(entity => entity.Id, id);
            return await this.Items.Find(filter).FirstOrDefaultAsync();
        }

        public async Task CreateAsync(Item item)
        {
            if(item == null)
            {
                throw new ArgumentNullException(nameof(item));
            }

            await this.Items.InsertOneAsync(item);
        }

        public async Task UpdateAsync(Item item)
        {
            if (item == null)
            {
                throw new ArgumentNullException(nameof(item));
            }            

            FilterDefinition<Item>? filter = FilterBuilder.Eq(entity => entity.Id, item.Id);

            await this.Items.ReplaceOneAsync(filter, item);
        }

        public async Task DeleteAsync(Guid id)
        {
            FilterDefinition<Item>? filter = FilterBuilder.Eq(entity => entity.Id, id);
            await this.Items.DeleteOneAsync(filter);
        }
    }
}
