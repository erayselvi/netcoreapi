using Repositories.Contracts;
using System;
using System.Collections.Generic;
using System.Text;

namespace Repositories.EFCore
{
    public class RepositoryManager : IRepositoryManager
    {
        private readonly RepositoryContext _context;

        public RepositoryManager(RepositoryContext context)
        {
            _context = context;
        }

        public IProductRepository Product => new ProductRepository(_context);

        public void Save()
        {
            _context.SaveChanges();
        }
    }
}
