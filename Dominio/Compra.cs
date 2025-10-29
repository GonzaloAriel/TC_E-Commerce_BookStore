using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio
{
    public class Compra
    {
        public int Id { get; private set; }
        public Cliente ClienteCompra { get; set; }
        public DateTime Fecha { get; set; }
        public List<CompraDetalle> Detalles { get; set; } = new List<CompraDetalle>();
        public decimal Total => Detalles.Sum(d => d.Subtotal);

    }
}
