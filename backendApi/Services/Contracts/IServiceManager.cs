using System;
using System.Collections.Generic;
using System.Text;

namespace Services.Contracts
{
    public interface IServiceManager
    {
        IProductService ProductService { get; }
    }
}
