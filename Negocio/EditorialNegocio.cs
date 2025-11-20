using Dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocio
{
    public class EditorialNegocio
    {
        public List<Editorial> Listar()
        {
            List<Editorial> lista = new List<Editorial>();
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearConsulta("SELECT Id, Nombre, Pais FROM EDITORIALES");
                datos.ejecutarLectura();

                while (datos.Lector.Read())
                {
                    Editorial editorial = new Editorial
                    {
                        Id = (int)datos.Lector["Id"],
                        Nombre = datos.Lector["Nombre"].ToString(),
                        Pais = datos.Lector["Pais"].ToString()
                    };

                    lista.Add(editorial);
                }

                return lista;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al listar editoriales: " + ex.Message);
            }
            finally
            {
                datos.cerrarConexion();
            }
        }

    }
}
