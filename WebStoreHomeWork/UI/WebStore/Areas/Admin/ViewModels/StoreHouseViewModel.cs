using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebStore.Domain.DTO;
using WebStore.Domain.Implementations;

namespace WebStore.Areas.Admin.ViewModels
{
    public class StoreHouseViewModel
    {
        public ProductDTO Product { get; set; }

        public Category Category { get; set; }
    }
}
