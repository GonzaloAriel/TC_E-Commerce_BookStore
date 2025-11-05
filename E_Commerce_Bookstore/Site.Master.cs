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