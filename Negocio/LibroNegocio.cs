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
                datos.setearConsulta(@"SELECT  L.Id, L.Titulo,L.ISBN,L.Autor, L.Stock, L.PrecioVenta, L.ImagenUrl
                                       FROM    LIBROS L
                                       WHERE   L.Activo = 1
                                       ORDER BY L.Titulo;");
                datos.ejecutarLectura();

                while (datos.Lector.Read())
                {
                    Libro libro = new Libro();
                    libro.Id = (int)datos.Lector["Id"];
                    libro.Titulo = datos.Lector["Titulo"].ToString();
                    libro.ISBN = datos.Lector["ISBN"].ToString();
                    libro.Autor = datos.Lector["Autor"].ToString();
                    libro.Stock = (int)datos.Lector["Stock"];
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
            SELECT L.Id, L.Titulo, L.ISBN, L.Autor, L.Editorial,
                   L.Stock, L.PrecioVenta, L.ImagenUrl,
                   L.IdCategoria, C.Nombre AS CategoriaNombre
            FROM LIBROS L
            LEFT JOIN Categorias C ON L.IdCategoria = C.Id
            WHERE L.Activo = 1 AND L.IdCategoria = @id
            ORDER BY L.Titulo;");
                datos.setearParametro("@id", idCategoria);
                datos.ejecutarLectura();

                while (datos.Lector.Read())
                {
                    Libro libro = new Libro();
                    libro.Id = (int)datos.Lector["Id"];
                    libro.Titulo = datos.Lector["Titulo"].ToString();
                    libro.ISBN = datos.Lector["ISBN"] == DBNull.Value ? "" : datos.Lector["ISBN"].ToString();
                    libro.Autor = datos.Lector["Autor"] == DBNull.Value ? "" : datos.Lector["Autor"].ToString();
                    libro.Editorial = datos.Lector["Editorial"] == DBNull.Value ? "" : datos.Lector["Editorial"].ToString();
                    libro.Stock = datos.Lector["Stock"] == DBNull.Value ? 0 : Convert.ToInt32(datos.Lector["Stock"]);
                    libro.PrecioVenta = datos.Lector["PrecioVenta"] == DBNull.Value ? 0 : Convert.ToDecimal(datos.Lector["PrecioVenta"]);
                    libro.ImagenUrl = datos.Lector["ImagenUrl"] == DBNull.Value ? "" : datos.Lector["ImagenUrl"].ToString();

                    libro.Categoria = new Categoria();
                    libro.Categoria.Id = datos.Lector["IdCategoria"] == DBNull.Value ? 0 : Convert.ToInt32(datos.Lector["IdCategoria"]);
                    libro.Categoria.Nombre = datos.Lector["CategoriaNombre"] == DBNull.Value ? "" : datos.Lector["CategoriaNombre"].ToString();

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
                datos.setearConsulta(@"SELECT  L.Id, L.Titulo, L.ISBN, L.Autor, L.Editorial, L.Stock, L.PrecioVenta,
                                       L.ImagenUrl, L.IdCategoria, c.Nombre AS CategoriaNombre
                                       FROM    LIBROS L
                                       LEFT JOIN Categorias C ON L.IdCategoria = C.Id
                                       WHERE   L.Activo = 1
                                       AND  (L.Titulo   LIKE @q OR L.Autor    LIKE @q
                                       OR L.ISBN     LIKE @q OR L.Editorial LIKE @q)
                                       ORDER BY L.Titulo;");
                datos.setearParametro("@q", "%" + termino + "%");
                datos.ejecutarLectura();

                while (datos.Lector.Read())
                {
                    Libro libro = new Libro();
                    libro.Id = (int)datos.Lector["Id"];
                    libro.Titulo = datos.Lector["Titulo"].ToString();
                    libro.ISBN = datos.Lector["ISBN"] == DBNull.Value ? "" : datos.Lector["ISBN"].ToString();
                    libro.Autor = datos.Lector["Autor"] == DBNull.Value ? "" : datos.Lector["Autor"].ToString();
                    libro.Editorial = datos.Lector["Editorial"] == DBNull.Value ? "" : datos.Lector["Editorial"].ToString();
                    libro.Stock = datos.Lector["Stock"] == DBNull.Value ? 0 : Convert.ToInt32(datos.Lector["Stock"]);
                    libro.PrecioVenta = datos.Lector["PrecioVenta"] == DBNull.Value ? 0 : Convert.ToDecimal(datos.Lector["PrecioVenta"]);
                    libro.ImagenUrl = datos.Lector["ImagenUrl"] == DBNull.Value ? "" : datos.Lector["ImagenUrl"].ToString();

                    libro.Categoria = new Categoria();
                    libro.Categoria.Id = datos.Lector["IdCategoria"] == DBNull.Value ? 0 : Convert.ToInt32(datos.Lector["IdCategoria"]);
                    libro.Categoria.Nombre = datos.Lector["CategoriaNombre"] == DBNull.Value ? "" : datos.Lector["CategoriaNombre"].ToString();

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

        public void AgregarLibro(Libro libro)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearConsulta(@"
            INSERT INTO LIBROS 
            (Titulo, Descripcion, ISBN, AnioEdicion, Idioma, Paginas, Stock, Activo, 
             PrecioCompra, PrecioVenta, PorcentajeGanancia, ImagenUrl, Editorial, Autor, IdCategoria)
            VALUES
            (@titulo, @descripcion, @isbn, @anioEdicion, @idioma, @paginas, @stock, 
             @activo, @precioCompra, @precioVenta, @porcentajeGanancia, @imagenUrl, 
             @editorial, @autor, @idCategoria);
        ");

                datos.setearParametro("@titulo", libro.Titulo);
                datos.setearParametro("@descripcion", libro.Descripcion);
                datos.setearParametro("@isbn", libro.ISBN);
                datos.setearParametro("@anioEdicion", libro.AnioEdicion);
                datos.setearParametro("@idioma", libro.Idioma);
                datos.setearParametro("@paginas", libro.Paginas);
                datos.setearParametro("@stock", libro.Stock);
                datos.setearParametro("@activo", libro.Activo);
                datos.setearParametro("@precioCompra", libro.PrecioCompra);
                datos.setearParametro("@precioVenta", libro.PrecioVenta);
                datos.setearParametro("@porcentajeGanancia", libro.PorcentajeGanancia);
                datos.setearParametro("@imagenUrl", libro.ImagenUrl);
                datos.setearParametro("@editorial", libro.Editorial);
                datos.setearParametro("@autor", libro.Autor);
                datos.setearParametro("@idCategoria", libro.Categoria != null ? libro.Categoria.Id : (object)DBNull.Value);


                datos.ejecutarAccion();
            }
            catch (Exception ex)
            {
                throw new Exception("Error al agregar libro: " + ex.Message);
            }
            finally
            {
                datos.cerrarConexion();
            }
        }

        public void ModificarLibro(Libro libro)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearConsulta(
                    "UPDATE LIBROS SET " +
                    "Titulo = @titulo, " +
                    "Descripcion = @descripcion, " +
                    "ISBN = @isbn, " +
                    "AnioEdicion = @anioEdicion, " +
                    "Idioma = @idioma, " +
                    "Paginas = @paginas, " +
                    "Stock = @stock, " +
                    "Activo = @activo, " +
                    "PrecioCompra = @precioCompra, " +
                    "PrecioVenta = @precioVenta, " +
                    "PorcentajeGanancia = @porcentajeGanancia, " +
                    "ImagenUrl = @imagenUrl, " +
                    "Editorial = @editorial, " +
                    "Autor = @autor, " +
                    "IdCategoria = @idCategoria " +
                    "WHERE Id = @id;"
                );

                datos.setearParametro("@id", libro.Id);
                datos.setearParametro("@titulo", libro.Titulo);
                datos.setearParametro("@descripcion", libro.Descripcion);
                datos.setearParametro("@isbn", libro.ISBN);
                datos.setearParametro("@anioEdicion", libro.AnioEdicion);
                datos.setearParametro("@idioma", libro.Idioma);
                datos.setearParametro("@paginas", libro.Paginas);
                datos.setearParametro("@stock", libro.Stock);
                datos.setearParametro("@activo", libro.Activo);
                datos.setearParametro("@precioCompra", libro.PrecioCompra);
                datos.setearParametro("@precioVenta", libro.PrecioVenta);
                datos.setearParametro("@porcentajeGanancia", libro.PorcentajeGanancia);
                datos.setearParametro("@imagenUrl", libro.ImagenUrl);
                datos.setearParametro("@editorial", libro.Editorial);
                datos.setearParametro("@autor", libro.Autor);
                datos.setearParametro("@idCategoria", libro.Categoria.Id);

                datos.ejecutarAccion();
            }
            catch (Exception ex)
            {
                throw new Exception("Error al modificar libro: " + ex.Message);
            }
            finally
            {
                datos.cerrarConexion();
            }
        }

        public void Eliminar(int id)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearConsulta("DELETE FROM LIBROS WHERE Id = @id");
                datos.setearParametro("@id", id);
                datos.ejecutarAccion();
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

        public void Desactivar(int id)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setearConsulta("UPDATE Libros SET Activo = 0 WHERE Id = @Id");
                datos.setearParametro("@Id", id);
                datos.ejecutarAccion();
            }
            finally { datos.cerrarConexion(); }
        }

        public List<Libro> Filtrar(string campo, string criterio, string filtro, string estado)
        {
            List<Libro> lista = new List<Libro>();
            AccesoDatos datos = new AccesoDatos();

            try
            {
                string consulta = "SELECT Id, Titulo, ISBN, Stock, PrecioCompra, PrecioVenta, PorcentajeGanancia, Activo FROM LIBROS WHERE 1=1";

                // Filtro por estado
                if (estado != "Todos")
                    consulta += " AND Activo = " + (estado == "True" ? "1" : "0");

                // Filtro dinámico
                if (!string.IsNullOrEmpty(campo) && !string.IsNullOrEmpty(criterio))
                {
                    switch (campo)
                    {
                        case "Titulo":
                        case "ISBN":
                            switch (criterio)
                            {
                                case "Contiene":
                                    consulta += $" AND {campo} LIKE '%{filtro}%'";
                                    break;
                                case "Empieza con":
                                    consulta += $" AND {campo} LIKE '{filtro}%'";
                                    break;
                                case "Termina con":
                                    consulta += $" AND {campo} LIKE '%{filtro}'";
                                    break;
                                case "Igual a":
                                    consulta += $" AND {campo} = '{filtro}'";
                                    break;
                            }
                            break;

                        case "PrecioVenta":
                        case "Stock":
                            if (decimal.TryParse(filtro, out decimal valor))
                            {
                                switch (criterio)
                                {
                                    case "Mayor que":
                                        consulta += $" AND {campo} > {valor}";
                                        break;
                                    case "Menor que":
                                        consulta += $" AND {campo} < {valor}";
                                        break;
                                    case "Igual a":
                                        consulta += $" AND {campo} = {valor}";
                                        break;
                                }
                            }
                            break;
                    }
                }

                datos.setearConsulta(consulta);
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
                throw new Exception("Error al filtrar libros: " + ex.Message);
            }
            finally
            {
                datos.cerrarConexion();
            }
        }
    }
}
