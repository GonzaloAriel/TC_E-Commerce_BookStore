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
    public partial class Site : System.Web.UI.MasterPage
    {
        private CategoriaNegocio categoriaNegocio = new CategoriaNegocio();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CargarCategoriasNavbar();

                ActualizarCarritoVisual();

                if (Session["Cliente"] is Cliente cliente)
                {
                    hlBadgePerfil.NavigateUrl = "~/MiPerfil.aspx";
                    lblNombreUsuario.Text = cliente.Nombre;
                }
                else
                {
                    hlBadgePerfil.NavigateUrl = "~/MiCuenta.aspx?ReturnUrl=MiPerfil.aspx&origen=badge";
                    lblNombreUsuario.Text = "Ingresar";
                }
            }
        }
        private void CargarCategoriasNavbar()
        {
            try
            {
                List<Dominio.Categoria> listaCategorias = categoriaNegocio.Listar();
                repCategoriasNavbar.DataSource = listaCategorias;
                repCategoriasNavbar.DataBind();
            }
            catch
            {
                
            }
        }
        public void ActualizarCarritoVisual()
        {
            string cookieId = CookieHelper.ObtenerCookieId(Request, Response);
            int? idCliente = Session["IdCliente"] as int?;

            CarritoNegocio negocio = new CarritoNegocio();
            CarritoCompra carrito = negocio.ObtenerCarritoActivo(cookieId, idCliente);

            Session["Carrito"] = carrito;

            Label lblCantidad = (Label)FindControl("lblCantidadCarrito");
            UpdatePanel upd = (UpdatePanel)FindControl("updCarrito");

            if (lblCantidad != null)
            {
                int cantidadTotal = carrito?.Items?.Sum(i => i.Cantidad) ?? 0;
                lblCantidad.Text = cantidadTotal.ToString();
                lblCantidad.Visible = cantidadTotal > 0;
            }

            if (upd != null)
            {
                upd.Update();
            }
        }
    }
}