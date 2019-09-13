using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebStore.Domain.DTO;
using WebStore.Domain.Implementations;

namespace WebStore.Domain.ViewModel
{
    public class MicrocontrollerViewModel
    {
        public ProductDTO ProductBase { get; set; }
        public MCDescriptionDTO MCDescription { get; set; }
    }
}
