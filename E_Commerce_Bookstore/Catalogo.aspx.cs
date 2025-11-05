using Negocio;
using Dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace E_Commerce_Bookstore
{
    public partial class Catalogo : System.Web.UI.Page
    {
        private LibroNegocio libroNegocio = new LibroNegocio();
        private CategoriaNegocio categoriaNegocio = new CategoriaNegocio();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack) 
            {
                Cargar();
            }
        }
        private void Cargar()
        {
            try
            {
                // 1) Traer todos los libros
                List<Libro> libros = libroNegocio.Listar();

                // 2) Filtrar por categoría si viene en la URL
                string idCategoriaStr = Request.QueryString["idCategoria"];
                int idCategoria;

                if (!string.IsNullOrEmpty(idCategoriaStr) && int.TryParse(idCategoriaStr, out idCategoria))
                {
                    List<Libro> librosFiltrados = libros.FindAll(
                        l => l.Categoria != null && l.Categoria.Id == idCategoria
                    );

                    libros = librosFiltrados;

                    if (libros.Count == 0)
                    {
                        lblMensaje.CssClass = "text-warning d-block mb-3";
                        lblMensaje.Text = "No hay libros para la categoría seleccionada.";
                    }
                    else
                    {
                        lblMensaje.CssClass = "text-success d-block mb-3";
                        lblMensaje.Text = "Filtrado por categoría.";
                    }
                }
                else
                {
                    lblMensaje.Text = string.Empty; // Ver todos
                }

                // 3) Bind
                repLibros.DataSource = libros;
                repLibros.DataBind();
            }
            catch (Exception ex)
            {
                lblMensaje.CssClass = "text-danger d-block mb-3";
                lblMensaje.Text = "Error al cargar catálogo: " + ex.Message;
            }
        }
        protected void btnBuscar_Click(object sender, EventArgs e)
        {
            
        }
        protected void repCategorias_ItemCommand(object source, System.Web.UI.WebControls.RepeaterCommandEventArgs e)
        {
           
        }
        protected void lnkVerTodo_Click(object sender, EventArgs e)
        {
           
        }

        protected void repLibros_ItemCommand(object source, System.Web.UI.WebControls.RepeaterCommandEventArgs e)
        {
          
        }

        protected void btnAccionCommand(object sender, CommandEventArgs e)
        {
            int id = Convert.ToInt32(e.CommandArgument);

            if (e.CommandName == "Detalle")
            {
                Response.Redirect("Detalle.aspx?id=" + id);
            }
            else if (e.CommandName == "Comprar")
            {
                Response.Redirect("Carrito.aspx?id=" + id);
            }
        }

    }
}