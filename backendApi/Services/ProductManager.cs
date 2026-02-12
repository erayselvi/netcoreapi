using Entities.Models;
using Repositories.Contracts;
using Services.Contracts;
using System;
using System.Collections.Generic;
using System.Text;

namespace Services
{
    public class ProductManager : IProductService
    {
        private readonly IRepositoryManager _manager;

        public ProductManager(IRepositoryManager manager) //constractor dependency injection
        {  _manager = manager; }
        public Product CreateOneProduct(Product product)
        {
            if (product is null)
                throw new ArgumentNullException(nameof(product));
            _manager.Product.CreateOneProduct(product);
            _manager.Save();
            return product;
        }

        public void DeleteOneProduct(int id, bool trackChanges)
        {
            //check entity
            var entity = _manager.Product.GetOneProductById(id, trackChanges);
            if (entity is null)
                throw new Exception("Id eşleşen ürün bulunamadı.");
            _manager.Product.DeleteOneProduct(entity);
            _manager.Save();
        }

        public IEnumerable<Product> GetAllProducts(bool trackChanges) //salt okunur return sağlar.
        {
            return _manager.Product.GetAllProduct(trackChanges);
        }

        public Product GetOneProductById(int id, bool trackChanges)
        {
            //check entity
            return _manager.Product.GetOneProductById(id, trackChanges);
        }

        public void UpdateOneProduct(int id, Product product,bool trackChanges)
        {
            //check params
            var entity = _manager.Product.GetOneProductById(id, trackChanges);
            if (entity is null)
                throw new Exception("Id eşleşen ürün bulunamadı.");

            if (product is null)
                throw new ArgumentNullException(nameof(product));

            entity.Title = product.Title;
            entity.Price = product.Price;
            _manager.Product.Update(entity);
            _manager.Save();
        }
    }
}
