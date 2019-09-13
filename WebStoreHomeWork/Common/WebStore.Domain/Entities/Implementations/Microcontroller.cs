using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using WebStore.Domain.Interfaces;

namespace WebStore.Domain.Implementations
{
    [Table("Microcontrollers")]
    public class Microcontroller : ProductBase
    {

    }
}
