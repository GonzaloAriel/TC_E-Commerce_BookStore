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
    public partial class Site : System.Web.UI.MasterPage
    {
        private CategoriaNegocio categoriaNegocio = new CategoriaNegocio();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CargarCategoriasNavbar();

                var carrito = Session["Carrito"] as CarritoCompra;
                int cantidad = carrito?.Items?.Sum(i => i.Cantidad) ?? 0;

                lblCantidadCarrito.Text = cantidad.ToString();
                lblCantidadCarrito.Visible = cantidad > 0;
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
    }
}