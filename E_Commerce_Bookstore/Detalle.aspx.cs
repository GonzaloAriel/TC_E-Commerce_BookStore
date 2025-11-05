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
            int idProducto = int.Parse(Request.QueryString["id"]);
            int cantidadSolicitada;

            if (!int.TryParse(txtCantidad.Text, out cantidadSolicitada) || cantidadSolicitada <= 0)
            {
                lblMensajeAgregado.Text = "⚠️ Ingresá una cantidad válida.";
                lblMensajeAgregado.CssClass = "text-warning mt-2 d-block";
                return;
            }

            DetalleNegocio negocio = new DetalleNegocio();
            var producto = negocio.ObtenerPorId(idProducto);
            if (producto == null || !producto.Activo)
            {
                lblMensajeAgregado.Text = "❌ El libro no está disponible.";
                lblMensajeAgregado.CssClass = "text-danger mt-2 d-block";
                return;
            }

            if (producto.Stock == 0)
            {
                lblMensajeAgregado.Text = "❌ No hay stock disponible.";
                lblMensajeAgregado.CssClass = "text-danger mt-2 d-block";
                return;
            }

            // Verificar si ya hay unidades del producto en el carrito
            var carrito = Session["Carrito"] as CarritoCompra ?? new CarritoCompra();
            var itemExistente = carrito.Items.FirstOrDefault(i => i.IdProducto == idProducto);
            int cantidadEnCarrito = itemExistente?.Cantidad ?? 0;

            int cantidadTotal = cantidadEnCarrito + cantidadSolicitada;

            if (cantidadTotal > producto.Stock)
            {
                int disponibleParaAgregar = producto.Stock - cantidadEnCarrito;

                if (disponibleParaAgregar <= 0)
                {
                    lblMensajeAgregado.Text = $"⚠️ Ya seleccionaste el máximo disponible ({producto.Stock}).";
                    lblMensajeAgregado.CssClass = "text-warning mt-2 d-block";
                    return;
                }

                lblMensajeAgregado.Text = $"⚠️ Solo podés agregar {disponibleParaAgregar} unidad(es) más.";
                lblMensajeAgregado.CssClass = "text-warning mt-2 d-block";
                cantidadSolicitada = disponibleParaAgregar;
            }
            else if (cantidadTotal == producto.Stock)
            {
                lblMensajeAgregado.Text = $"⚠️ Ya seleccionaste el máximo disponible ({producto.Stock}).";
                lblMensajeAgregado.CssClass = "text-warning mt-2 d-block";
            }

            AgregarAlCarrito(idProducto, cantidadSolicitada);

            // Actualizar badge del carrito en el master page
            var carritoActualizado = Session["Carrito"] as CarritoCompra;
            Label lblCantidad = (Label)Master.FindControl("lblCantidadCarrito");
            UpdatePanel upd = (UpdatePanel)Master.FindControl("updCarrito");

            if (lblCantidad != null && carritoActualizado != null)
            {
                int cantidadFinal = carritoActualizado.Items.Sum(i => i.Cantidad);
                lblCantidad.Text = cantidadFinal.ToString();
                lblCantidad.Visible = cantidadFinal > 0;
            }

            if (upd != null)
            {
                upd.Update();
            }

            lblMensajeAgregado.Text = $"✅ Se agregaron {cantidadSolicitada} unidad(es) de '{producto.Titulo}' al carrito.";
            lblMensajeAgregado.CssClass = "text-success mt-2 d-block";
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
        private void AgregarAlCarrito(int idProducto, int cantidad)
        {
            CarritoCompra carrito = Session["Carrito"] as CarritoCompra;
            if (carrito == null)
                carrito = new CarritoCompra();

            if (carrito.Items == null)
                carrito.Items = new List<ItemCarrito>();

            DetalleNegocio negocio = new DetalleNegocio();
            var producto = negocio.ObtenerPorId(idProducto);
            if (producto == null || !producto.Activo)
                return;

            if (producto.Stock < cantidad)
            {
                cantidad = producto.Stock;
                if (cantidad == 0)
                    return;
            }

            var existente = carrito.Items.FirstOrDefault(i => i.IdProducto == idProducto);
            if (existente != null)
            {
                int nuevaCantidad = existente.Cantidad + cantidad;
                existente.Cantidad = (nuevaCantidad > producto.Stock) ? producto.Stock : nuevaCantidad;
            }
            else
            {
                carrito.Items.Add(new ItemCarrito
                {
                    IdProducto = producto.Id,
                    Libro = producto,
                    Cantidad = cantidad,
                    PrecioUnitario = producto.PrecioVenta
                });
            }

            Session["Carrito"] = carrito;
        }


    }
}