using Dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocio
{
    public class FacturaNegocio
    {
        public void CrearFactura(Factura factura)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearConsulta(@"
                INSERT INTO FACTURAS
                (IdPedido, Nombre, Apellido, Direccion, Barrio, Ciudad, CP, Depto)
                VALUES
                (@IdPedido, @Nombre, @Apellido, @Direccion, @Barrio, @Ciudad, @CP, @Depto)
            ");

                datos.setearParametro("@IdPedido", factura.IdPedido);
                datos.setearParametro("@Nombre", factura.Nombre);
                datos.setearParametro("@Apellido", factura.Apellido);
                datos.setearParametro("@Direccion", factura.Direccion);
                datos.setearParametro("@Barrio", factura.Barrio);
                datos.setearParametro("@Ciudad", factura.Ciudad);
                datos.setearParametro("@CP", factura.CP);
                datos.setearParametro("@Depto", factura.Depto);

                datos.ejecutarAccion();
            }
            finally
            {
                datos.cerrarConexion();
            }
        }

        public Factura ObtenerFacturaPorPedido(int idPedido)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearConsulta(@"
                SELECT * FROM FACTURAS
                WHERE IdPedido = @IdPedido
            ");

                datos.setearParametro("@IdPedido", idPedido);
                datos.ejecutarLectura();

                if (datos.Lector.Read())
                {
                    return new Factura
                    {
                        Id = (int)datos.Lector["Id"],
                        IdPedido = idPedido,
                        Fecha = (DateTime)datos.Lector["Fecha"],
                        Nombre = datos.Lector["Nombre"].ToString(),
                        Apellido = datos.Lector["Apellido"].ToString(),
                        Direccion = datos.Lector["Direccion"].ToString(),
                        Barrio = datos.Lector["Barrio"].ToString(),
                        Ciudad = datos.Lector["Ciudad"].ToString(),
                        CP = datos.Lector["CP"].ToString(),
                        Depto = datos.Lector["Depto"].ToString()
                    };
                }

                return null;
            }
            finally
            {
                datos.cerrarConexion();
            }
        }
    }
}
