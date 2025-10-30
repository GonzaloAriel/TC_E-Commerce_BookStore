using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
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

        protected void btnLimpiar_Click(object sender, EventArgs e)
        {

        }

        protected void dgvArticulo_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}