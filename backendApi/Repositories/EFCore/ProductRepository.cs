using Entities.Models;
using Repositories.Contracts;
using System;
using System.Collections.Generic;
using System.Text;

namespace Repositories.EFCore
{
    public class ProductRepository : RepositoryBase<Product>, IProductRepository
    {
        public ProductRepository(RepositoryContext context) : base(context)
        {

        }

        public void CreateOneProduct(Product product) => Create(product);

        public void DeleteOneProduct(Product product)=> Delete(product);

        public void UpdateOneProduct(Product product)=> Update(product);
        public IQueryable<Product> GetAllProduct(bool trackChanges) =>
            FindAll(trackChanges)
            .OrderBy(p => p.Id);

        public IQueryable<Product> GetOneProductById(int id, bool trackChanges) =>
            FindByCondition(p  => p.Id.Equals(id), trackChanges);

    }
}