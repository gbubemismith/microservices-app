using System.Collections.Generic;
using System.Threading.Tasks;
using Catalog.API.Data.Interfaces;
using Catalog.API.Entities;
using Catalog.API.Repositories.Interfaces;
using MongoDB.Driver;

namespace Catalog.API.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly ICatalogContext _context;
        public ProductRepository(ICatalogContext context)
        {
            _context = context;

        }

        public async Task<IEnumerable<ProductModel>> GetProducts()
        {
            return await _context.Products.Find(p => true).ToListAsync();
        }

        public async Task<ProductModel> GetProduct(string id)
        {
            return await _context.Products.Find(p => p.Id == id).FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<ProductModel>> GetProductByName(string name)
        {
            FilterDefinition<ProductModel> filter = Builders<ProductModel>.Filter.ElemMatch(p => p.Name, name);

            return await _context.Products.Find(filter).ToListAsync();
        }

        public async Task<IEnumerable<ProductModel>> GetProductByCategory(string category)
        {
            FilterDefinition<ProductModel> filter = Builders<ProductModel>.Filter.Eq(p => p.Category, category);

            return await _context.Products.Find(filter).ToListAsync();
        }

        public async Task Create(ProductModel product)
        {
            await _context.Products.InsertOneAsync(product);
        }

        public async Task<bool> Update(ProductModel product)
        {
            var updateResult = await _context.Products
                                            .ReplaceOneAsync(filter: g => g.Id == product.Id, replacement: product);

            return updateResult.IsAcknowledged && updateResult.ModifiedCount > 0;
        }

        public async Task<bool> Delete(string id)
        {
            FilterDefinition<ProductModel> filter = Builders<ProductModel>.Filter.Eq(p => p.Id, id);

            DeleteResult deleteResult = await _context.Products.DeleteOneAsync(filter);

            return deleteResult.IsAcknowledged && deleteResult.DeletedCount > 0;
        }


    }
}