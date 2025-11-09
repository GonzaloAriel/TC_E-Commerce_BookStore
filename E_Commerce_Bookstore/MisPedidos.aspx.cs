using Dominio;
using Negocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace E_Commerce_Bookstore
{
    public partial class MisPedidos : System.Web.UI.Page
    {
        PedidoNegocio negocio = new PedidoNegocio();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CargarPedidos();
            }
        }
        private void CargarPedidos()
        {
            try
            {
                int idCliente = ObtenerIdClienteSesion();
                List<Pedido> lista = negocio.ListarPedidosPorCliente(idCliente);

                repPedidos.DataSource = lista;
                repPedidos.DataBind();

                pnlVacio.Visible = (lista == null || lista.Count == 0);
            }
            catch
            {
                pnlVacio.Visible = true;
            }
        }

        private int ObtenerIdClienteSesion()
        {
            if (Session["IdCliente"] != null)
                return Convert.ToInt32(Session["IdCliente"]);

            if (Session["UsuarioEmail"] != null)
            {
                int id = negocio.ObtenerIdClientePorEmail(Session["UsuarioEmail"].ToString());
                if (id > 0) return id;
            }

            return negocio.ObtenerIdClientePorEmail("nicolas.strozzi@gmail.com");
        }
    }
}