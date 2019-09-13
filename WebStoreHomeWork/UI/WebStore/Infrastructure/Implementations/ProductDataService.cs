using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebStore.Data;
using WebStore.Interfaces.Services;
using WebStore.Domain.Implementations;
using WebStore.Domain.DTO;

namespace WebStore.Infrastructure.Implementations
{
    public class ProductDataService : IServiceProductData
    {
        public IEnumerable<ProductDTO> Products => TestData.Products.Select(p => new ProductDTO
                                                                                {
                                                                                    Id = p.Id,
                                                                                    CategoryId = p.CategoryId,
                                                                                    ImageUrl = p.ImageUrl,
                                                                                    Name = p.Name,
                                                                                    Price = p.Price
                                                                                });

        public IEnumerable<MCDescriptionDTO> DetailedDescription => TestData.MCDescriptions.Select(d => new MCDescriptionDTO
        {
            Id = d.Id,
            ProductId = d.ProductId,
            ProductName = d.Product.Name,
            DetailedDesription = String.Join(";", d.DetailedDesriptionList)
        });

        // все ниже пока не нужно
        // весь набор данных заранее предопределен
        // эта реализация только для получения данных
        public void AddNewProduct(ProductDTO product)
        {
            throw new NotImplementedException();
        }

        public void AddNewDescription(MCDescriptionDTO description)
        {
            throw new NotImplementedException();
        }

        public void DeleteProduct(int id)
        {
            throw new NotImplementedException();
        }

        public ProductBase GetProductById(int id)
        {
            throw new NotImplementedException();
        }

        public void SaveChanges()
        {
            throw new NotImplementedException();
        }
    }
}
