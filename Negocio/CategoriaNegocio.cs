using Dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocio
{
    public class CategoriaNegocio
    {
        public List<Categoria> Listar()
        {
            List<Categoria> lista = new List<Categoria>();
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearConsulta("SELECT Id, Nombre, Activo FROM CATEGORIAS WHERE Activo = 1 ORDER BY Nombre;");
                datos.ejecutarLectura();

                while (datos.Lector.Read())
                {
                    Categoria cat = new Categoria();
                    cat.Id = (int)datos.Lector["Id"];
                    cat.Nombre = datos.Lector["Nombre"].ToString();
                    cat.Activo = datos.Lector["Activo"] != DBNull.Value && Convert.ToBoolean(datos.Lector["Activo"]);
                    lista.Add(cat);
                }
            }
            finally
            {
                datos.cerrarConexion();
            }

            return lista;
        }
    }
}
