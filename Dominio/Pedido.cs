using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio
{
    public class Pedido
    {
        public int Id { get; private set; }
        public string NumeroPedido { get; set; }
        public DateTime Fecha { get; set; }
        public string Estado { get; set; }
        public string MetodoPago { get; set; }
        public string MetodoEnvio { get; set; }
        public decimal Subtotal { get; set; }
        public decimal TotalEnvio { get; set; }
        public decimal Total { get; set; }
        public string DireccionDeEnvio { get; set; }
    }
}
