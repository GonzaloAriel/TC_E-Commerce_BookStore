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
                datos.setearConsulta(@"
                   SELECT p.Id, p.NumeroPedido, p.Fecha, p.Estado, p.Subtotal, p.DireccionDeEnvio,
                   e.Precio AS PrecioEnvio
                   FROM PEDIDOS p
                   LEFT JOIN ENVIOS e ON e.IdPedido = p.Id
                   WHERE p.IdCliente = @IdCliente
                   ORDER BY p.Fecha DESC
                ");
                datos.setearParametro("@IdCliente", idCliente);
                datos.ejecutarLectura();

                while (datos.Lector.Read())
                {
                    decimal subtotal = Convert.ToDecimal(datos.Lector["Subtotal"]);
                    decimal precioEnvio = datos.Lector["PrecioEnvio"] != DBNull.Value
                        ? Convert.ToDecimal(datos.Lector["PrecioEnvio"])
                        : 0;

                    Pedido pedido = new Pedido
                    {
                        Id = Convert.ToInt32(datos.Lector["Id"]),
                        NumeroPedido = datos.Lector["NumeroPedido"].ToString(),
                        Fecha = (DateTime)datos.Lector["Fecha"],
                        Estado = datos.Lector["Estado"].ToString(),
                        Subtotal = subtotal,
                        Total = subtotal + precioEnvio,
                        DireccionDeEnvio = datos.Lector["DireccionDeEnvio"].ToString(),
                        Cliente = new Cliente { Id = idCliente }
                    };

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

        public Pedido ObtenerPedidoPorNumero(string numeroPedido)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setearConsulta(@"
                   SELECT p.Id, p.NumeroPedido, p.Fecha, p.Estado, p.Subtotal, p.DireccionDeEnvio,
                   e.Precio AS PrecioEnvio
                   FROM PEDIDOS p
                   LEFT JOIN ENVIOS e ON e.IdPedido = p.Id
                   WHERE p.NumeroPedido = @NumeroPedido
                ");
                datos.setearParametro("@NumeroPedido", numeroPedido);
                datos.ejecutarLectura();

                if (datos.Lector.Read())
                {
                    decimal subtotal = Convert.ToDecimal(datos.Lector["Subtotal"]);
                    decimal precioEnvio = datos.Lector["PrecioEnvio"] != DBNull.Value
                        ? Convert.ToDecimal(datos.Lector["PrecioEnvio"])
                        : 0;

                    return new Pedido
                    {
                        Id = Convert.ToInt32(datos.Lector["Id"]),
                        NumeroPedido = datos.Lector["NumeroPedido"].ToString(),
                        Fecha = (DateTime)datos.Lector["Fecha"],
                        Estado = datos.Lector["Estado"].ToString(),
                        Subtotal = subtotal,
                        Total = subtotal + precioEnvio,
                        DireccionDeEnvio = datos.Lector["DireccionDeEnvio"].ToString()
                    };
                }

                return null;
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
