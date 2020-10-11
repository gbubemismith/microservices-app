using Catalog.API.Data.Interfaces;
using Catalog.API.Models;
using Catalog.API.Settings;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace Catalog.API.Data
{
    public class CatalogContext : ICatalogContext
    {
        
        public CatalogContext(IOptions<CatalogDatabaseSettings> settings)
        {
            var client = new MongoClient(settings.Value.ConnectionString);
            var database = client.GetDatabase(settings.Value.DatabaseName);

            Products = database.GetCollection<ProductModel>(settings.Value.CollectionName);
            CatalogContextSeed.SeedData(Products);

        }
        public IMongoCollection<ProductModel> Products {get; }
    }
}