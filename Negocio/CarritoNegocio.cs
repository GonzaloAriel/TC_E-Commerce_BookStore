using Dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocio
{
    public class CarritoNegocio
    {
        public decimal ObtenerTotalCarrito(int idCliente)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                string consulta = @"
                    SELECT SUM(ci.Cantidad * ci.PrecioUnitario)
                    FROM CARRITO_ITEMS ci
                    INNER JOIN CARRITOS c ON c.Id = ci.IdCarrito
                    WHERE c.IdCliente = @idCliente AND c.Activo = 1";

                datos.setearConsulta(consulta);
                datos.setearParametro("@idCliente", idCliente);
                datos.ejecutarLectura();

                if (datos.Lector.Read() && datos.Lector[0] != DBNull.Value)
                    return Convert.ToDecimal(datos.Lector[0]);

                return 0m;
            }
            finally
            {
                datos.cerrarConexion();
            }
        }
        public int AsegurarCarritoActivo(int idCliente)
        {
            // Buscar existente
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setearConsulta("SELECT Id FROM CARRITOS WHERE IdCliente=@c AND Activo=1");
                datos.setearParametro("@c", idCliente);
                datos.ejecutarLectura();
                if (datos.Lector.Read())
                    return Convert.ToInt32(datos.Lector[0]);
            }
            finally { datos.cerrarConexion(); }

            // Crear si no existe
            //AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setearConsulta("INSERT INTO CARRITOS (IdCliente,Activo) VALUES (@c,1)");
                datos.setearParametro("@c", idCliente);
                datos.ejecutarAccion();
            }
            catch
            {
                datos.cerrarConexion();
                return 0;
            }
            finally { datos.cerrarConexion(); }

            // Devolver Id creado
            //AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setearConsulta("SELECT TOP 1 Id FROM CARRITOS WHERE IdCliente=@c AND Activo=1 ORDER BY Id DESC");
                datos.setearParametro("@c", idCliente);
                datos.ejecutarLectura();
                return datos.Lector.Read() ? Convert.ToInt32(datos.Lector[0]) : 0;
            }
            finally { datos.cerrarConexion(); }
        }
    }
}
