using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Dominio;
using Negocio;

namespace E_Commerce_Bookstore
{
    public partial class GestionProducto : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            LibroNegocio negocio = new LibroNegocio();
            dgvArticulo.DataSource = negocio.listarGrilla();
            dgvArticulo.DataBind();

            dgvArticulo.CssClass = "table table-striped table-info";
        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {

        }

        protected void dgvArticulo_SelectedIndexChanged(object sender, EventArgs e)
        {
            var seleccion = dgvArticulo.SelectedRow.Cells[0].Text;
            var id = dgvArticulo.SelectedDataKey.Value.ToString();

            LibroNegocio negocio = new LibroNegocio();

            if (!string.IsNullOrEmpty(id))
            {
                int IdLib = int.Parse(id);
                var listaTemporal = negocio.listarGrilla();
                Libro lib = listaTemporal.Find(x => x.Id == IdLib);

                if (lib != null)
                {

                    ddlCategoria.SelectedValue = lib.Categoria.Id.ToString();
                    txtId.Text = lib.Id.ToString();
                    txtTitulo.Text = lib.Titulo;
                    txtAutor.Text = lib.Autor;
                    txtDescripcion.Text = lib.Descripcion;
                    txtEditorial.Text = lib.Editorial;
                    txtIdioma.Text = lib.Idioma;
                    txtImagenUrl.Text = lib.ImagenUrl;
                    txtISBN.Text = lib.ISBN;
                    txtPaginas.Text = lib.Paginas.ToString();
                    txtPorcentajeGanancia.Text = lib.PorcentajeGanancia.ToString();
                    txtPrecioCompra.Text = lib.PrecioCompra.ToString();
                    txtPrecioVenta.Text = lib.PrecioVenta.ToString();
                    txtAnioEdicion.Text = lib.AnioEdicion.ToString();
                    chkActivo.Checked = lib.Activo;
                }

            }
        }

        protected void btnLimpiar_Click(object sender, EventArgs e)
        {
            ddlCategoria.SelectedValue = "";
            txtId.Text = "";
            txtTitulo.Text = "";
            txtAutor.Text = "";
            txtDescripcion.Text = "";
            txtEditorial.Text = "";
            txtIdioma.Text = "";
            txtImagenUrl.Text = "";
            txtISBN.Text = "";
            txtPaginas.Text = "";
            txtPorcentajeGanancia.Text = "";
            txtPrecioCompra.Text = "";
            txtPrecioVenta.Text = "";
            txtAnioEdicion.Text = "";
            chkActivo.Checked = false;
        }

    }
}