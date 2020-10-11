using System;
using System.Collections.Generic;
using Catalog.API.Models;
using MongoDB.Driver;

namespace Catalog.API.Data
{
    public class CatalogContextSeed
    {
        public static void SeedData(IMongoCollection<ProductModel> productCollection)
        {
             bool existProduct = productCollection.Find(p => true).Any();

             if (!existProduct)
                productCollection.InsertManyAsync(GetPreConfiguredProducts());
        }

        private static IEnumerable<ProductModel> GetPreConfiguredProducts()
        {
            return new List<ProductModel>()
            {
                new ProductModel()
                {
                    Name = "Mango",
                    Summary = "A type of fruit",
                    Description = "A type of fruit",
                    ImageFile = "product-1.png",
                    Price = 200.00M,
                    Category = "Fruit"
                },
                new ProductModel()
                {
                    Name = "Cabbage",
                    Summary = "A type of vegetable",
                    Description = "A type of vegetable",
                    ImageFile = "product-2.png",
                    Price = 400.00M,
                    Category = "Vegetable"
                },
                new ProductModel()
                {
                    Name = "Yam",
                    Summary = "A type of tuber",
                    Description = "A type of tuber",
                    ImageFile = "product-3.png",
                    Price = 250.00M,
                    Category = "Tuber"
                },
                new ProductModel()
                {
                    Name = "Carrot",
                    Summary = "A type of vegetable",
                    Description = "A type of vegetable",
                    ImageFile = "product-4.png",
                    Price = 100.00M,
                    Category = "Vegetable"
                },
                new ProductModel()
                {
                    Name = "Rice",
                    Summary = "A type of grain",
                    Description = "A type of grain",
                    ImageFile = "product-5.png",
                    Price = 600.00M,
                    Category = "Grain"
                },
                new ProductModel()
                {
                    Name = "Maize",
                    Summary = "A type of grain",
                    Description = "A type of grain",
                    ImageFile = "product-6.png",
                    Price = 200.00M,
                    Category = "Grain"
                }
            };
        }
    }
}