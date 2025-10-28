using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio
{
    public class Carrito
    {
        public int? IdCliente { get; set; }
        public List<ItemCarrito> Items { get; set; } = new List<ItemCarrito>();
        public decimal Total => Items.Sum(i => i.Subtotal);
    }
}