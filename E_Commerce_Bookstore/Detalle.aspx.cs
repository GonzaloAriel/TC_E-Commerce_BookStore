using Dominio;
using Negocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace E_Commerce_Bookstore
{
    public partial class Detalle : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (int.TryParse(Request.QueryString["id"], out int idLibro))
                {
                    CargarDetalleLibro(idLibro);
                }
                else
                {
                    lblTitulo.Text = "Libro no disponible";
                    lblDescripcion.Text = "No se recibió un identificador válido para mostrar el detalle.";
                    btnAgregarCarrito.Visible = false;
                    grupoCantidad.Visible = false;
                    pnlSugerencias.Visible = false;
                }
            }
        }

        protected void btnVolver_Click(object sender, EventArgs e)
        {
            Response.Redirect("Catalogo.aspx", false);
        }

        protected void btnAgregarCarrito_Click(object sender, EventArgs e)
        {
            Response.Redirect("Carrito.aspx", false);
        }
        private void CargarDetalleLibro(int idLibro)
        {
            try
            {
                DetalleNegocio negocio = new DetalleNegocio();
                Libro libro = negocio.ObtenerPorId(idLibro);

                if (libro != null && libro.Activo)
                {
                    lblTitulo.Text = libro.Titulo;
                    lblAutor.Text = libro.Autor;
                    lblEditorial.Text = libro.Editorial;
                    lblCategoria.Text = libro.Categoria?.Nombre ?? "Sin categoría";
                    lblDescripcion.Text = libro.Descripcion;
                    lblIdioma.Text = libro.Idioma;
                    lblAnioEdicion.Text = libro.AnioEdicion.ToString();
                    lblPagina.Text = libro.Paginas.ToString();
                    lblPrecio.Text = libro.PrecioVenta.ToString("N2");
                    lblStock.Text = libro.Stock.ToString();
                    if (libro.Stock > 0)
                    {
                        lblStock.Text = libro.Stock.ToString();
                        pStock.Attributes["class"] = "text-success";
                        btnAgregarCarrito.Enabled = true;
                        grupoCantidad.Visible = true;
                        badgeStock.Visible = libro.Stock == 1;
                    }
                    else
                    {
                        lblStock.Text = "Sin stock";
                        pStock.Attributes["class"] = "text-danger";
                        btnAgregarCarrito.Visible = false;
                        grupoCantidad.Visible = false;
                        badgeStock.Visible = false;
                    }
                    imgLibro.ImageUrl = libro.ImagenUrl;

                    var sugeridos = negocio.ListarSugeridosPorCategoria(libro.Categoria.Id, libro.Id);
                    repSugeridos.DataSource = sugeridos;
                    repSugeridos.DataBind();



                    pnlSugerencias.Visible = sugeridos.Count > 0;
                }
                else
                {
                    lblTitulo.Text = "Libro no disponible";
                    lblDescripcion.Text = "Este libro no se encuentra activo o no existe.";
                    btnAgregarCarrito.Enabled = false;
                    txtCantidad.Enabled = false;
                    pnlSugerencias.Visible = false;

                }
            }
            catch (Exception)
            {
                lblTitulo.Text = "Error al cargar el libro";
                lblDescripcion.Text = "Ocurrió un problema al intentar mostrar los datos.";
                btnAgregarCarrito.Enabled = false;
                txtCantidad.Enabled = false;
                pnlSugerencias.Visible = false;
            }
        }
    }
}