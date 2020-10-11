using System;
using System.Threading.Tasks;
using Catalog.API.Models;
using Catalog.API.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Catalog.API.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class CatalogController : ControllerBase
    {
        private readonly IProductRepository _repository;
        private readonly ILogger<CatalogController> _logger;
        public CatalogController(IProductRepository repository, ILogger<CatalogController> logger)
        {
            _logger = logger;
            _repository = repository;

        }

        [HttpGet]
        public async Task<IActionResult> GetProducts()
        {
            var products = await _repository.GetProducts();

            return Ok(products);
        }

        [HttpGet("{id:length(24)}", Name = "GetProduct")]
        public async Task<IActionResult> GetProductById(string id)
        {
            var proudct = await _repository.GetProduct(id);

            if (proudct == null)
            {
                _logger.LogError($"Product with id {id}, not found");
                return NotFound();
            }
                

            return Ok(proudct);

        }

        [HttpGet("{action}/{categoryName}")]
        public async Task<IActionResult> GetProductByCategory(string categoryName)
        {
            var proudcts = await _repository.GetProductByCategory(categoryName);

            return Ok(proudcts);

        }

        [HttpPost]
        public async Task<IActionResult> CreateProduct(ProductModel product)
        {
            await _repository.Create(product);

            return CreatedAtRoute("GetProduct", new { id = product.Id }, product);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateProduct(ProductModel product)
        {
            var status = await _repository.Update(product);

            if (!status)
                return BadRequest($"Updating product {product.Id} failed");

            return NoContent();
        }

        [HttpDelete("{id:length(24)}")]
        public async Task<IActionResult> DeleteProductById(string id)
        {
            var status = await _repository.Delete(id);

            if (!status)
                return BadRequest($"Failed to delete product, {id}");

            return Ok();
        }


    }
}