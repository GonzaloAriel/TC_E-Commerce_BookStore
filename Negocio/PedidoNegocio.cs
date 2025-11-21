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
                   SELECT 
    p.Id, p.NumeroPedido, p.Fecha, p.Estado,
    p.Subtotal,
    p.DireccionDeEnvio,
    e.Precio AS PrecioEnvio,
    (SELECT TOP 1 Metodo FROM PAGOS WHERE IdPedido = p.Id ORDER BY Fecha DESC) AS MetodoPago
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
        public int CrearPedido(Pedido pedido)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearConsulta(@"
            INSERT INTO PEDIDOS
                (NumeroPedido, Fecha, Estado, Subtotal, Total, DireccionDeEnvio, IdCliente)
            OUTPUT INSERTED.Id
            VALUES
                (@NumeroPedido, @Fecha, @Estado, @Subtotal, @Total, @DireccionDeEnvio, @IdCliente)
        ");

                datos.setearParametro("@NumeroPedido", pedido.NumeroPedido);
                datos.setearParametro("@Fecha", pedido.Fecha);
                datos.setearParametro("@Estado", pedido.Estado);
                datos.setearParametro("@Subtotal", pedido.Subtotal);
                datos.setearParametro("@Total", pedido.Total);
                datos.setearParametro("@DireccionDeEnvio", pedido.DireccionDeEnvio);
                datos.setearParametro("@IdCliente", pedido.Cliente.Id);

                datos.ejecutarLectura();

                if (datos.Lector.Read())
                    return (int)datos.Lector[0];

                return 0;
            }
            finally
            {
                datos.cerrarConexion();
            }
        }

        public string GenerarNumeroPedido()
        {
            // Ejemplo simple: PED-638293847293
            return "PED-" + DateTime.Now.Ticks;
        }

        public void CrearDetallePedido(int idPedido, ItemCarrito item)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearConsulta(@"
            INSERT INTO PEDIDOS_DETALLE
            (IdPedido, IdLibro, Cantidad, PrecioUnitario)
            VALUES (@IdPedido, @IdLibro, @Cantidad, @PrecioUnitario)
        ");

                datos.setearParametro("@IdPedido", idPedido);
                datos.setearParametro("@IdLibro", item.IdLibro);
                datos.setearParametro("@Cantidad", item.Cantidad);
                datos.setearParametro("@PrecioUnitario", item.PrecioUnitario);

                datos.ejecutarAccion();
            }
            finally
            {
                datos.cerrarConexion();
            }
        }
        public void RegistrarPago(int idPedido, decimal monto, string metodo)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearConsulta(@"
            INSERT INTO PAGOS
            (IdPedido, Monto, Metodo, Estado)
            VALUES (@IdPedido, @Monto, @Metodo, @Estado)
        ");

                datos.setearParametro("@IdPedido", idPedido);
                datos.setearParametro("@Monto", monto);
                datos.setearParametro("@Metodo", metodo);
                datos.setearParametro("@Estado", "Pendiente");

                datos.ejecutarAccion();
            }
            finally
            {
                datos.cerrarConexion();
            }
        }

        public void RegistrarEnvio(int idPedido, string metodo, decimal precio,
                            string barrio, string ciudad, string departamento)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearConsulta(@"
            INSERT INTO ENVIOS
            (IdPedido, MetodoDeEnvio, Precio, EstadoEnvio, Barrio, Ciudad, Departamento)
            VALUES (@IdPedido, @MetodoDeEnvio, @Precio, @EstadoEnvio, @Barrio, @Ciudad, @Departamento)
        ");

                datos.setearParametro("@IdPedido", idPedido);
                datos.setearParametro("@MetodoDeEnvio", metodo);
                datos.setearParametro("@Precio", precio);
                datos.setearParametro("@EstadoEnvio", "Pendiente");
                datos.setearParametro("@Barrio", barrio);
                datos.setearParametro("@Ciudad", ciudad);
                datos.setearParametro("@Departamento", departamento);

                datos.ejecutarAccion();
            }
            finally
            {
                datos.cerrarConexion();
            }
        }

        public string ObtenerMetodoPago(int idPedido)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setearConsulta("SELECT TOP 1 Metodo FROM PAGOS WHERE IdPedido=@id ORDER BY Fecha DESC");
                datos.setearParametro("@id", idPedido);
                datos.ejecutarLectura();

                if (datos.Lector.Read())
                    return datos.Lector["Metodo"].ToString();

                return "";
            }
            finally
            {
                datos.cerrarConexion();
            }
        }

    }
}
