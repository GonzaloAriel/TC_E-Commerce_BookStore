using Dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocio
{
    public class AutorNegocio
    {
        public List<Autor> Listar()
        {
            List<Autor> lista = new List<Autor>();
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearConsulta("SELECT Id, Nombre FROM AUTORES");
                datos.ejecutarLectura();

                while (datos.Lector.Read())
                {
                    Autor autor = new Autor
                    {
                        Id = (int)datos.Lector["Id"],
                        Nombre = datos.Lector["Nombre"].ToString()
                    };

                    lista.Add(autor);
                }

                return lista;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al listar autores: " + ex.Message);
            }
            finally
            {
                datos.cerrarConexion();
            }
        }

    }
}
