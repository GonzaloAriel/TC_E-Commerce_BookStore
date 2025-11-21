using Dominio;
using E_Commerce_Bookstore.Helpers;
using Negocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace E_Commerce_Bookstore
{
    public partial class Populares : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CargarPopulares();
            }
        }

        private void CargarPopulares()
        {
            LibroNegocio negocio = new LibroNegocio();
            repPopulares.DataSource = negocio.ListarPopulares();
            repPopulares.DataBind();
        }

        protected void btnAccionCommand(object sender, CommandEventArgs e)
        {
            int idLibro = Convert.ToInt32(e.CommandArgument);

            if (e.CommandName == "Detalle")
            {
                Response.Redirect("Detalle.aspx?id=" + idLibro.ToString());
                return;
            }

            if (e.CommandName == "Comprar")
            {
                CarritoNegocio carritoNegocio = new CarritoNegocio();

                string cookieId = CookieHelper.ObtenerCookieId(Request, Response);
                int idCliente = 0;

                if (Session["IdCliente"] != null)
                {
                    idCliente = Convert.ToInt32(Session["IdCliente"]);
                }

                CarritoCompra carrito = carritoNegocio.ObtenerOCrearCarritoActivo(cookieId, idCliente);

                LibroNegocio libNeg = new LibroNegocio();
                Libro libro = libNeg.ObtenerPorId(idLibro);

                if (libro == null) return;
                if (!libro.Activo) return;
                if (libro.Stock <= 0) return;

                carritoNegocio.AgregarItem(carrito.Id, idLibro, 1, libro.PrecioVenta);

                Session["Carrito"] = carritoNegocio.ObtenerCarritoActivo(cookieId, idCliente);

                CargarPopulares();
            }
        }

        protected void repPopulares_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType != ListItemType.Item &&
                e.Item.ItemType != ListItemType.AlternatingItem)
            {
                return;
            }

            Libro libro = (Libro)e.Item.DataItem;
            Label lblLocalMensaje = (Label)e.Item.FindControl("lblLocalMensaje");
            Literal litPopular = (Literal)e.Item.FindControl("litPopular");
            Literal litBestSeller = (Literal)e.Item.FindControl("litBestSeller");

            lblLocalMensaje.Text = "";
            lblLocalMensaje.Visible = false;

            if (libro.Stock == 0)
            {
                lblLocalMensaje.Text = "Sin stock.";
                lblLocalMensaje.Visible = true;
            }

            litPopular.Text = "<span class='badge bg-primary position-absolute top-0 start-0 m-2'>Popular</span>";

            if (libro.BestSeller)
            {
                litBestSeller.Text = "<span class='badge bg-warning text-dark position-absolute top-0 end-0 m-2'>Best Seller</span>";
            }
        }
    }
}