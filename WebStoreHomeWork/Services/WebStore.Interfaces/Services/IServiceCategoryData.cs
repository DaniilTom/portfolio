using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebStore.Domain.Implementations;

namespace WebStore.Interfaces.Services
{
    public interface IServiceCategoryData
    {
        IEnumerable<Category> Categories { get; }
    }
}
