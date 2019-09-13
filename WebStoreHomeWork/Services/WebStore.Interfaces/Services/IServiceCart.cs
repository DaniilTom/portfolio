using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebStore.Domain.Interfaces;

namespace WebStore.Interfaces.Services
{
    public interface IServiceCart
    {
        void AddToCart(IProduct p);
        void RemoveFromCart(IProduct p);
        void DecrementProduct(IProduct p);
        void RemoveAll();
    }
}
