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
                datos.setearConsulta(@"
            SELECT 
                L.Id,
                L.Titulo,
                L.Descripcion,
                L.ISBN,
                L.Idioma,
                L.AnioEdicion,
                L.Paginas,
                L.Stock,
                L.Activo,
                L.PrecioCompra,
                L.PrecioVenta,
                L.PorcentajeGanancia,
                L.ImagenUrl,
                L.Editorial,
                L.Autor,
                C.Id AS IdCategoria,
                C.Nombre AS NombreCategoria
            FROM Libros L
            INNER JOIN Categorias C ON L.IdCategoria = C.Id;
        ");
                datos.ejecutarLectura();

                while (datos.Lector.Read())
                {
                    Libro lib = new Libro
                    {
                        Id = (int)datos.Lector["Id"],
                        Titulo = datos.Lector["Titulo"].ToString(),
                        Descripcion = datos.Lector["Descripcion"].ToString(),
                        ISBN = datos.Lector["ISBN"].ToString(),
                        Idioma = datos.Lector["Idioma"].ToString(),
                        AnioEdicion = (int)datos.Lector["AnioEdicion"],
                        Paginas = (int)datos.Lector["Paginas"],
                        Stock = (int)datos.Lector["Stock"],
                        Activo = (bool)datos.Lector["Activo"],
                        PrecioCompra = (decimal)datos.Lector["PrecioCompra"],
                        PrecioVenta = (decimal)datos.Lector["PrecioVenta"],
                        PorcentajeGanancia = (decimal)datos.Lector["PorcentajeGanancia"],
                        ImagenUrl = datos.Lector["ImagenUrl"].ToString(),
                        Editorial = datos.Lector["Editorial"].ToString(),
                        Autor = datos.Lector["Autor"].ToString(),
                        Categoria = new Categoria
                        {
                            Id = datos.Lector["IdCategoria"] != DBNull.Value ? (int)datos.Lector["IdCategoria"] : 0,
                            Nombre = datos.Lector["NombreCategoria"] != DBNull.Value ? datos.Lector["NombreCategoria"].ToString() : "Sin categoría"
                        }
                    };
                    lista.Add(lib);
                }

                return lista;
            }
            catch (Exception ex)
            {

                throw new Exception("Error al listar libros: " + ex.Message);
            }
            finally
            {
                datos.cerrarConexion();
            }
        }
    }
}
