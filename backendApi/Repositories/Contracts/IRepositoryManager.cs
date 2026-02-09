using System;
using System.Collections.Generic;
using System.Text;

namespace Repositories.Contracts
{
    public interface IRepositoryManager
    {//Unit of work
        IProductRepository Product { get; }
        void Save();
    }
}
