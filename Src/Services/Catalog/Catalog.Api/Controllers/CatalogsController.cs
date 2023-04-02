using Catalog.Api.Entities;
using Catalog.Api.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Catalog.Api.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class CatalogsController : ControllerBase
    {
        private readonly IProductRepository _productRepository;
        private readonly ILogger<CatalogsController> _logger;

        public CatalogsController(IProductRepository productRepository, ILogger<CatalogsController> logger)
        {
            _productRepository = productRepository ?? throw new ArgumentNullException(nameof(productRepository));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger)); ;
        }

        [ProducesResponseType(typeof(IEnumerable<Product>),(int)HttpStatusCode.OK)]
        [HttpGet]
        public async Task<IActionResult> GetProducts()
        {
            return Ok(await _productRepository.GetProductsAsync());
        }

        [HttpGet("{id:length(24)}",Name ="GetProduct")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType( typeof(Product),(int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> GetProductById(string id)
        {
            Product product =await _productRepository.GetProductAsync(id);
            if(product == null)
            {
                _logger.LogError($"product with id: {id} not found");
                return NotFound();
            }
            return Ok(product);
        }
        [Route("[action]/category",Name = "GetProductByCategory")]
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<Product>),(int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetProductByCategory(string category)
        {
            return Ok(await _productRepository.GetProductsByCategoryAsync(category));
        }

        [ProducesResponseType(typeof(IEnumerable<Product>), (int)HttpStatusCode.OK)]
        [HttpPost]
        public async Task<IActionResult> CreateProduct([FromBody] Product product)
        {
            await _productRepository.CreateProduct(product);

            return CreatedAtRoute("GetProduct",new {id=product.Id },product);
        }

        [ProducesResponseType(typeof(IEnumerable<Product>), (int)HttpStatusCode.OK)]
        [HttpPut]
        public async Task<IActionResult> UpdateProduct([FromBody] Product product)
        {
            return Ok(await _productRepository.UpdateProduct(product));
        }
        [HttpDelete("id:length(24)",Name ="DeleteProduct")]
        [ProducesResponseType(typeof(IEnumerable<Product>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> DeleteProductById(string id)
        {
            return Ok(await _productRepository.DeleteProduct(id));
        }


    }
}
