using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebStore.Domain.DTO;
using WebStore.Domain.Implementations;

namespace WebStore.Interfaces.Services
{
    // очень похож на IServiceEmployeeData, м.б. стоит использовать
    // какой-нибудь один интерфейс
    public interface IServiceProductData
    {
        IEnumerable<ProductDTO> Products { get; }

        IEnumerable<MCDescriptionDTO> DetailedDescription { get; }

        void AddNewProduct(ProductDTO product);

        void AddNewDescription(MCDescriptionDTO description);

        void DeleteProduct(int id);

        ProductBase GetProductById(int Id);
    }
}
