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

            try
            {
                
                Libro nuevo = new Libro
                {
                    Titulo = txtTitulo.Text.Trim(),
                    Descripcion = txtDescripcion.Text.Trim(),
                    ISBN = txtISBN.Text.Trim(),
                    Idioma = txtIdioma.Text.Trim(),
                    AnioEdicion = int.Parse(txtAnioEdicion.Text),
                    Paginas = int.Parse(txtPaginas.Text),
                    Stock = int.Parse(txtStock.Text),
                    Activo = chkActivo.Checked, 
                    PrecioCompra = decimal.Parse(txtPrecioCompra.Text),
                    PrecioVenta = decimal.Parse(txtPrecioVenta.Text),
                    PorcentajeGanancia = decimal.Parse(txtPorcentajeGanancia.Text),
                    ImagenUrl = txtImagenUrl.Text.Trim(),
                    Editorial = txtEditorial.Text.Trim(),
                    Autor = txtAutor.Text.Trim(),
                    Categoria = new Dominio.Categoria     
                    {
                        Id = int.Parse(ddlCategoria.SelectedValue)
                    }
                };

                LibroNegocio negocio = new LibroNegocio();
                negocio.AgregarLibro(nuevo);

                lbMensaje.Text = "✅ Libro agregado correctamente.";
                lbMensaje.ForeColor = System.Drawing.Color.Green;

                CargarGrilla();
                LimpiarCampos();
            }
            catch (FormatException)
            {
                lbMensaje.Text = "⚠️ Verifica los valores numéricos (Año, Páginas, Precios, etc.).";
                lbMensaje.ForeColor = System.Drawing.Color.OrangeRed;
            }
            catch (Exception ex)
            {
                lbMensaje.Text = "❌ Error al guardar el libro: " + ex.Message;
                lbMensaje.ForeColor = System.Drawing.Color.Red;
            }
        }

        protected void dgvArticulo_SelectedIndexChanged(object sender, EventArgs e)
        {
            lbMensaje.Text = "";

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

        protected void btnEliminar_Click(object sender, EventArgs e)
        {
            LibroNegocio negocio = new LibroNegocio();

            try
            {
                int id = int.Parse(txtId.Text);
                negocio.Eliminar(id);

                lbMensaje.CssClass = "text-success fw-bold text-center";
                lbMensaje.Text = "👍 Libro eliminado correctamente";

                CargarGrilla();
                LimpiarCampos();
            }
            catch (Exception ex)
            {
                lbMensaje.Text = "❌ Error al eliminar el libro: " + ex.Message;
                lbMensaje.ForeColor = System.Drawing.Color.Red;
            }
        }

        protected void btnLimpiar_Click(object sender, EventArgs e)
        {
            LimpiarCampos();
        }

        private void CargarGrilla()
        {
            LibroNegocio negocio = new LibroNegocio();
            dgvArticulo.DataSource = negocio.listarGrilla();
            dgvArticulo.DataBind();
        }

        private void LimpiarCampos()
        {
            txtId.Text = "";
            txtTitulo.Text = "";
            txtDescripcion.Text = "";
            txtISBN.Text = "";
            txtIdioma.Text = "";
            txtAnioEdicion.Text = "";
            txtPaginas.Text = "";
            txtStock.Text = "";
            chkActivo.Checked = true;
            txtPrecioCompra.Text = "";
            txtPrecioVenta.Text = "";
            txtPorcentajeGanancia.Text = "";
            txtImagenUrl.Text = "";
            txtEditorial.Text = "";
            txtAutor.Text = "";
            ddlCategoria.ClearSelection();
        }

        protected void btnModificar_Click(object sender, EventArgs e)
        {
            LibroNegocio negocio = new LibroNegocio();
            try
            {
                Libro libro = new Libro
                {
                    Id = int.Parse(txtId.Text),
                    Titulo = txtTitulo.Text.Trim(),
                    Descripcion = txtDescripcion.Text.Trim(),
                    ISBN = txtISBN.Text.Trim(),
                    Idioma = txtIdioma.Text.Trim(),
                    AnioEdicion = int.Parse(txtAnioEdicion.Text),
                    Paginas = int.Parse(txtPaginas.Text),
                    Stock = int.Parse(txtStock.Text),
                    Activo = chkActivo.Checked,
                    PrecioCompra = decimal.Parse(txtPrecioCompra.Text),
                    PrecioVenta = decimal.Parse(txtPrecioVenta.Text),
                    PorcentajeGanancia = decimal.Parse(txtPorcentajeGanancia.Text),
                    ImagenUrl = txtImagenUrl.Text.Trim(),
                    Editorial = txtEditorial.Text.Trim(),
                    Autor = txtAutor.Text.Trim(),
                    Categoria = new Dominio.Categoria
                    {
                        Id = int.Parse(ddlCategoria.SelectedValue)
                    }
                };

                negocio.ModificarLibro(libro);

                lbMensaje.Text = "✅ Libro modificado correctamente.";
                lbMensaje.ForeColor = System.Drawing.Color.Green;

                CargarGrilla();
                LimpiarCampos();
            }
            catch (FormatException)
            {
                lbMensaje.Text = "⚠️ Verifica los valores numéricos (Año, Páginas, Precios, etc.).";
                lbMensaje.ForeColor = System.Drawing.Color.OrangeRed;
            }
            catch (Exception ex)
            {
                lbMensaje.Text = "❌ Error al modificar el libro: " + ex.Message;
                lbMensaje.ForeColor = System.Drawing.Color.Red;
            }
        }
    }
}