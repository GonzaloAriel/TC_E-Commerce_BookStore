using Dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocio
{
    public class LibroNegocio
    {
        public List<Libro> Listar()
        {
            List<Libro> lista = new List<Libro>();
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearConsulta(@"
SELECT  L.Id, L.Titulo, L.PrecioVenta, L.ImagenUrl
FROM    LIBROS L
WHERE   L.Activo = 1
ORDER BY L.Titulo;");
                datos.ejecutarLectura();

                while (datos.Lector.Read())
                {
                    Libro libro = new Libro();
                    libro.Id = (int)datos.Lector["Id"];
                    libro.Titulo = datos.Lector["Titulo"].ToString();
                    libro.PrecioVenta = datos.Lector["PrecioVenta"] == DBNull.Value ? 0 : Convert.ToDecimal(datos.Lector["PrecioVenta"]);
                    libro.ImagenUrl = datos.Lector["ImagenUrl"].ToString();
                    lista.Add(libro);
                }
            }
            finally
            {
                datos.cerrarConexion();
            }
            return lista;
        }

        public List<Libro> ListarPorCategoria(int idCategoria)
        {
            List<Libro> lista = new List<Libro>();
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearConsulta(@"
SELECT  L.Id, L.Titulo, L.PrecioVenta, L.ImagenUrl
FROM    LIBROS L
WHERE   L.Activo = 1 AND L.IdCategoria = @id
ORDER BY L.Titulo;");
                datos.setearParametro("@id", idCategoria);
                datos.ejecutarLectura();

                while (datos.Lector.Read())
                {
                    Libro libro = new Libro();
                    libro.Id = (int)datos.Lector["Id"];
                    libro.Titulo = datos.Lector["Titulo"].ToString();
                    libro.PrecioVenta = datos.Lector["PrecioVenta"] == DBNull.Value ? 0 : Convert.ToDecimal(datos.Lector["PrecioVenta"]);
                    libro.ImagenUrl = datos.Lector["ImagenUrl"].ToString();
                    lista.Add(libro);
                }
            }
            finally
            {
                datos.cerrarConexion();
            }
            return lista;
        }

        public List<Libro> Buscar(string termino)
        {
            List<Libro> lista = new List<Libro>();
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearConsulta(@"
SELECT  L.Id, L.Titulo, L.PrecioVenta, L.ImagenUrl
FROM    LIBROS L
WHERE   L.Activo = 1
  AND  (L.Titulo LIKE @q OR L.Autor LIKE @q OR L.ISBN LIKE @q OR L.Editorial LIKE @q)
ORDER BY L.Titulo;");
                datos.setearParametro("@q", "%" + termino + "%");
                datos.ejecutarLectura();

                while (datos.Lector.Read())
                {
                    Libro libro = new Libro();
                    libro.Id = (int)datos.Lector["Id"];
                    libro.Titulo = datos.Lector["Titulo"].ToString();
                    libro.PrecioVenta = datos.Lector["PrecioVenta"] == DBNull.Value ? 0 : Convert.ToDecimal(datos.Lector["PrecioVenta"]);
                    libro.ImagenUrl = datos.Lector["ImagenUrl"].ToString();
                    lista.Add(libro);
                }
            }
            finally
            {
                datos.cerrarConexion();
            }
            return lista;
        }

        public List<Libro> listarGrilla()
        {
            List<Libro> lista = new List<Libro>();
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearConsulta("SELECT Id, Titulo, ISBN, Stock, PrecioCompra, PrecioVenta, PorcentajeGanancia, Activo FROM LIBROS;");
                datos.ejecutarLectura();

                while (datos.Lector.Read())
                {
                    Libro lib = new Libro
                    {
                        Id = (int)datos.Lector["Id"],
                        Titulo = datos.Lector["Titulo"].ToString(),
                        ISBN = datos.Lector["ISBN"].ToString(),
                        Stock = (int)datos.Lector["Stock"],
                        PrecioCompra = (decimal)datos.Lector["PrecioCompra"],
                        PrecioVenta = (decimal)datos.Lector["PrecioVenta"],
                        PorcentajeGanancia = (decimal)datos.Lector["PorcentajeGanancia"],
                        Activo = (bool)datos.Lector["Activo"]
                    };
                    lista.Add(lib);
                }

                return lista;
            }
            catch (Exception ex)
            {

                throw new Exception("Error al listar Articulos: " + ex.Message);
            }
            finally
            {
                datos.cerrarConexion();
            }
        }
    }
}
