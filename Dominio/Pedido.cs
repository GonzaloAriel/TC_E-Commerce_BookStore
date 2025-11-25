using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio
{
    public class Pedido
    {
        public int Id { get; set; }
        public Cliente Cliente { get; set; }
        public string NumeroPedido { get; set; }
        public DateTime Fecha { get; set; }
        public string Estado { get; set; }
        public decimal Subtotal { get; set; }
        public decimal Total { get; set; }
        public string DireccionEnvio { get; set; }
        public string NombreFacturacion { get; set; }
        public string ApellidoFacturacion { get; set; }
        public string DireccionFacturacion { get; set; }
        public string BarrioFacturacion { get; set; }
        public string CiudadFacturacion { get; set; }
        public string DeptoFacturacion { get; set; }
        public string CPFacturacion { get; set; }

    }
}
