using Azure;
using Entities.Models;
using Microsoft.AspNetCore.Mvc;
using Repositories.Contracts;
using Repositories.EFCore;
using System.Diagnostics.Eventing.Reader;
namespace backendApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IRepositoryManager _manager;

        public ProductController(IRepositoryManager manager)//Neden RepositoryManager değil? Ahmet ustayı istemek yerine müsait ustayı çağırır!
        {
            _manager = manager;
        }
        [HttpGet]
        public IActionResult GetAllProducts()
        {
            try
            {
                var products = _manager.Product.GetAllProduct(false);
                return Ok(products);
            }
            catch (Exception ex) { throw new Exception(ex.Message); }
        }
        [HttpGet("{id:int}")]
        public IActionResult GetOneProduct([FromRoute(Name = "id")] int id)
        {
            try
            {
                var product = _manager
                    .Product
                    .GetOneProductById(id, false);
                if (product is null)
                    return NotFound(); //404
                return Ok(product);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        [HttpPost]
        public IActionResult CreateOneProduct([FromBody] Product product)
        {
            try
            {
                if (product is null)
                    return BadRequest(); //400

                _manager.Product.Create(product);

                return StatusCode(201, product);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }
        [HttpPut("{id:int}")]

        public IActionResult UpdateOneProduct([FromRoute(Name = "id")] int id,
            [FromBody] Product product)
        {
            try
            {
                var entity = _manager
                    .Product
                    .GetOneProductById(id, true);
                if (entity is null)
                    return NotFound(); //404
                else if (id != product.Id)
                    return BadRequest(); //400
                entity.Title = product.Title;
                entity.Price = product.Price;

                _manager.Save();
                return Ok(product);
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }

        }

        [HttpDelete("{id:int}")]

        public IActionResult DeleteOneProduct([FromRoute(Name = "id")] int id)
        {
            try
            {
                var entity = _manager
                    .Product
                    .GetOneProductById(id, true);
                if (entity is null)
                    return NotFound(new { StatusCode = 404, message = $"Id ile eşleşen product yok:{id}" });
                _manager.Product.DeleteOneProduct(entity);
                _manager.Save();

                return NoContent();
            }
            catch (Exception)
            {

                throw;
            }
        }

        [HttpPatch("{id:int}")]
        public IActionResult PartiallyUpdateOneProduct([FromRoute(Name = "id")] int id,
            [FromBody] JsonPatchDocument<Product> product)
        {
            try
            {
                var entity = _manager
                            .Product
                            .GetOneProductById(id, true);
                if (entity is null)
                    return NotFound(new { StatusCode = 404, message = $"Id ile eşleşen product yok:{id}" });

                product.ApplyTo(entity);
                _manager.Product.Update(entity);
                _manager.Save();
                return NoContent();

            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
