using Dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocio
{
    public class PedidoNegocio
    {
        public List<Pedido> ListarPedidosPorCliente(int idCliente)
        {
            List<Pedido> lista = new List<Pedido>();
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearConsulta("SELECT Id, NumeroPedido, Fecha, Estado, Subtotal, TotalEnvio, Total FROM PEDIDOS WHERE IdCliente = @IdCliente ORDER BY Fecha DESC");
                datos.setearParametro("@IdCliente", idCliente);
                datos.ejecutarLectura();

                while (datos.Lector.Read())
                {
                    Pedido pedido = new Pedido();
                    //pedido.Id = (int)datos.Lector["Id"];
                    pedido.NumeroPedido = datos.Lector["NumeroPedido"].ToString();
                    pedido.Fecha = (DateTime)datos.Lector["Fecha"];
                    pedido.Estado = datos.Lector["Estado"].ToString();
                    pedido.Subtotal = Convert.ToDecimal(datos.Lector["Subtotal"]);
                    pedido.TotalEnvio = Convert.ToDecimal(datos.Lector["TotalEnvio"]);
                    pedido.Total = Convert.ToDecimal(datos.Lector["Total"]);
                    lista.Add(pedido);
                }

                return lista;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                datos.cerrarConexion();
            }
        }

        public int ObtenerIdClientePorEmail(string email)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setearConsulta("SELECT TOP 1 Id FROM CLIENTES WHERE Email = @Email");
                datos.setearParametro("@Email", email);
                datos.ejecutarLectura();

                if (datos.Lector.Read())
                    return (int)datos.Lector["Id"];
                else
                    return 0;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                datos.cerrarConexion();
            }
        }
    }
}
