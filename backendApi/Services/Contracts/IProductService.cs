using Entities.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Services.Contracts
{
    public interface IProductService
    {
        IEnumerable<Product> GetAllProducts(bool trackChanges);
        Product GetOneProductById(int id, bool trackChanges);
        Product CreateOneProduct(Product product);

        void UpdateOneProduct(int id, Product product,bool trackChanges);
        void DeleteOneProduct(int id, bool trackChanges);
    }
}
