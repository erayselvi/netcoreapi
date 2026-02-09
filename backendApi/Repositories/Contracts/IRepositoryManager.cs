using System;
using System.Collections.Generic;
using System.Text;

namespace Repositories.Contracts
{
    internal interface IRepositoryManager
    {//Unit of work
        IProductRepository Product { get; }
        void Save();
    }
}
