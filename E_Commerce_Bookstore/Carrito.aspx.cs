using Dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace E_Commerce_Bookstore
{
    public partial class Carrito : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CarritoCompra carrito = Session["Carrito"] as CarritoCompra ?? new CarritoCompra();
                rptCarrito.DataSource = carrito.Items;
                rptCarrito.DataBind();
                lblTotal.Text = carrito.Total.ToString("N2");
            }

        }

        protected void btnVolver_Click(object sender, EventArgs e)
        {
            Response.Redirect("Catalogo.aspx", false);
        }

        protected void btnComprar_Click(object sender, EventArgs e)
        {
            Response.Redirect("ProcesoEnvio.aspx", false);
        }

        protected void rptCarrito_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            CarritoCompra carrito = Session["Carrito"] as CarritoCompra;
            if (carrito == null || carrito.Items == null) return;

            int id = int.Parse(e.CommandArgument.ToString());
            var item = carrito.Items.FirstOrDefault(i => i.IdProducto == id);

            if (item != null)
            {
                switch (e.CommandName)
                {
                    case "Incrementar":
                        if (item.Cantidad < item.Libro.Stock)
                            item.Cantidad++;
                        break;

                    case "Decrementar":
                        if (item.Cantidad > 1)
                            item.Cantidad--;
                        break;

                    case "Eliminar":
                        carrito.Items.Remove(item);
                        break;
                }
            }

            // Actualizar sesión
            Session["Carrito"] = carrito;

            // Actualizar controles sin redireccionar
            rptCarrito.DataSource = carrito.Items;
            rptCarrito.DataBind();
            lblTotal.Text = carrito.Items.Sum(i => i.Subtotal).ToString("N2");

            // 🔄 Actualizar badge del carrito en el master page
            Label lblCantidad = (Label)Master.FindControl("lblCantidadCarrito");
            UpdatePanel upd = (UpdatePanel)Master.FindControl("updCarrito");

            if (lblCantidad != null)
            {
                int cantidadTotal = carrito.Items.Sum(i => i.Cantidad);
                lblCantidad.Text = cantidadTotal.ToString();
                lblCantidad.Visible = cantidadTotal > 0;
            }

            if (upd != null)
            {
                upd.Update(); // ✅ Actualiza el panel sin recargar toda la página
            }

        }
    }
}