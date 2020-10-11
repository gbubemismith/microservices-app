using Catalog.API.Models;
using MongoDB.Driver;

namespace Catalog.API.Data.Interfaces
{
    public interface ICatalogContext
    {
         IMongoCollection<ProductModel> Products {get; }
    }
}