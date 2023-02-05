using Catalog.API.Entities;
using Catalog.API.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Catalog.API.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class CatalogController : ControllerBase
    {
        private readonly IProductRepository  _productRepository;
        private readonly  ILogger<CatalogController> _logger;

        public CatalogController(IProductRepository productRepository, ILogger<CatalogController> logger)
        {
            _productRepository = productRepository ?? throw new ArgumentNullException(nameof(productRepository));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<Product>),(int)HttpStatusCode.OK)]

        public async Task<ActionResult<IEnumerable<Product>>> GetProducts()
        {
            var result= await _productRepository.GetProducts();

            return Ok(result);
        }

        
        [HttpGet("{id:length(24)}",Name = "GetProductById")]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(Product), (int)HttpStatusCode.OK)]

        public async Task<ActionResult<Product>> GetProductById(string id)
        {
            var result = await _productRepository.GetProductById(id);

            if (result == null)
            {
                _logger.LogError($"Product with id: {id} , is not found");
                return NotFound();
            }

            return Ok(result);
        }

        [Route("[action]/{category}", Name = "GetProductByCategory")]
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<Product>), (int)HttpStatusCode.OK)]

        public async Task<ActionResult<IEnumerable<Product>>> GetProductByCategory(string category)
        {
            var result = await _productRepository.GetProductByCategory(category);

            if (result == null)
            {
                _logger.LogError($"Product with Category: {category} , is not found");
                return NotFound();
            }
            else
                return Ok(result);
        }



        [Route("[action]/{name}", Name = "GetProductByName")]
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<Product>), (int)HttpStatusCode.OK)]

        public async Task<ActionResult<IEnumerable<Product>>> GetProductByName(string name)
        {
            var result = await _productRepository.GetProductByName(name);

            if (result == null)
            {
                _logger.LogError($"Product with Name: {name} , is not found");
                return NotFound();
            }
            else
                return Ok(result);
        }

        [HttpPost]
        [ProducesResponseType(typeof(Product), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<Product>> CreateProduct([FromBody]Product product)
        {

            await _productRepository.CreateProduct(product);

            return CreatedAtRoute("GetProductById", new { id = product.Id }, product);
        }

        [HttpPut]
        [ProducesResponseType(typeof(Product), (int)HttpStatusCode.OK)]

        public async Task<ActionResult<Product>> UpdateProduct([FromBody] Product product)
        {

            return Ok(await  _productRepository.UpdateProduct(product));

           // return CreatedAtRoute("GetProductById", new { id = product.Id }, product);
        }

        [HttpDelete("{id:length(24)}", Name = "DeleteProduct")]
        [ProducesResponseType(typeof(Product), (int)HttpStatusCode.OK)]

        public async Task<ActionResult<Product>> DeleteProduct(string id)
        {

            return Ok(await _productRepository.DeleteProduct(id));
                       
        }

    }
}
