using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dominio;

namespace Negocio
{
    public class DetalleNegocio
    {
        public Libro ObtenerPorId(int id)
        {
            AccesoDatos datos = new AccesoDatos();
            Libro libro = null;

            try
            {
                datos.setearConsulta(@"SELECT l.Id, l.Titulo, l.Descripcion, l.ISBN, l.Idioma, l.AnioEdicion, l.Paginas, l.Stock, l.Activo,
                                              l.PrecioCompra, l.PrecioVenta, l.PorcentajeGanancia, l.ImagenUrl, l.Editorial, l.Autor,
                                              c.Id AS CategoriaId, c.Nombre AS CategoriaNombre
                                       FROM Libros l
                                       INNER JOIN Categorias c ON l.IdCategoria = c.Id
                                       WHERE l.Id = @id");

                datos.setearParametro("@id", id);
                datos.ejecutarLectura();

                if (datos.Lector.Read())
                {
                    libro = new Libro
                    {
                        Id = (int)datos.Lector["Id"],
                        Titulo = datos.Lector["Titulo"].ToString(),
                        Descripcion = datos.Lector["Descripcion"].ToString(),
                        ISBN = datos.Lector["ISBN"].ToString(),
                        Idioma = datos.Lector["Idioma"].ToString(),
                        AnioEdicion = Convert.ToInt32(datos.Lector["AnioEdicion"]),
                        Paginas = Convert.ToInt32(datos.Lector["Paginas"]),
                        Stock = Convert.ToInt32(datos.Lector["Stock"]),
                        Activo = Convert.ToBoolean(datos.Lector["Activo"]),
                        PrecioCompra = Convert.ToDecimal(datos.Lector["PrecioCompra"]),
                        PrecioVenta = Convert.ToDecimal(datos.Lector["PrecioVenta"]),
                        PorcentajeGanancia = Convert.ToDecimal(datos.Lector["PorcentajeGanancia"]),
                        ImagenUrl = datos.Lector["ImagenUrl"].ToString(),
                        Editorial = datos.Lector["Editorial"].ToString(),
                        Autor = datos.Lector["Autor"].ToString(),
                        Categoria = new Categoria
                        {
                            Id = Convert.ToInt32(datos.Lector["CategoriaId"]),
                            Nombre = datos.Lector["CategoriaNombre"].ToString()
                        }
                    };
                }

                return libro;
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
        public List<Libro> ListarSugeridosPorCategoria(int idCategoria, int idLibroActual)
        {
            List<Libro> lista = new List<Libro>();
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearConsulta(@"SELECT TOP 4 Id, Titulo, ImagenUrl 
                       FROM Libros 
                       WHERE IdCategoria = @idCategoria AND Id <> @idLibro AND Activo = 1");
                datos.setearParametro("@idCategoria", idCategoria);
                datos.setearParametro("@idLibro", idLibroActual);
                datos.ejecutarLectura();

                while (datos.Lector.Read())
                {
                    Libro libro = new Libro
                    {
                        Id = (int)datos.Lector["Id"],
                        Titulo = datos.Lector["Titulo"].ToString(),
                        ImagenUrl = datos.Lector["ImagenUrl"].ToString()
                    };

                    lista.Add(libro);
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
    }
}
