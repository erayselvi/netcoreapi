using Entities.Models;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Services.Contracts;
using System;
using System.Collections.Generic;
using System.Text;

namespace Presentation.Controllers
{
    [ApiController]
    [Route("api/products")]
    public class ProductController : ControllerBase
    {
        private readonly IServiceManager _manager;

        public ProductController(IServiceManager manager)
        {
            _manager = manager;
        }


        [HttpGet]
        public IActionResult GetAllProducts()
        {
            try
            {
                var products = _manager.ProductService.GetAllProducts(false);
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
                    .ProductService
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

                _manager.ProductService.CreateOneProduct(product);

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
                if (product is null)
                    return BadRequest(); //400

                _manager
                    .ProductService
                    .UpdateOneProduct(id, product, true);

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
                _manager.ProductService.DeleteOneProduct(id, false);
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
                            .ProductService
                            .GetOneProductById(id, true);
                if (entity is null)
                    return NotFound(new { StatusCode = 404, message = $"Id ile eşleşen product yok:{id}" });

                product.ApplyTo(entity);
                _manager.ProductService.UpdateOneProduct(id, entity, true);
                return NoContent();

            }
            catch (Exception)
            {

                throw;
            }
        }
    }

}
