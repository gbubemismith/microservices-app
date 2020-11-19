using System.Collections.Generic;
using System.Threading.Tasks;
using Catalog.API.Entities;

namespace Catalog.API.Repositories.Interfaces
{
    public interface IProductRepository
    {
        Task<IEnumerable<ProductModel>> GetProducts();
        Task<ProductModel> GetProduct(string id);
        Task<IEnumerable<ProductModel>> GetProductByName(string name);
        Task<IEnumerable<ProductModel>> GetProductByCategory(string category);

        Task Create(ProductModel product);
        Task<bool> Update(ProductModel product);
        Task<bool> Delete(string id);

    }
}