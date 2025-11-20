using Dominio;
using Negocio;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection.Emit;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace E_Commerce_Bookstore
{
    public partial class MisPedidos : System.Web.UI.Page
    {
        private PedidoNegocio negocio = new PedidoNegocio();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CargarPedidos();
            }
        }

        private void CargarPedidos()
        {
            int idCliente = ObtenerIdClienteSesion();

            // SOLO PARA PROBAR:
            if (idCliente == 0)
                idCliente = 1; // poné un IdCliente que exista en tu tabla PEDIDOS

            List<Pedido> lista = negocio.ListarPedidosPorCliente(idCliente);

            if (lista != null && lista.Count > 0)
            {
                repPedidos.DataSource = lista;
                repPedidos.DataBind();
                repPedidos.Visible = true;
                lblSinPedidos.Visible = false;
            }
            else
            {
                repPedidos.Visible = false;
                lblSinPedidos.Visible = true;
                lblSinPedidos.Text = "No tenés pedidos registrados.";
            }
        }


        private int ObtenerIdClienteSesion()
        {
            if (Session["IdCliente"] != null)
            {
                int id;
                if (int.TryParse(Session["IdCliente"].ToString(), out id))
                    return id;
            }

            return 0; // 0 = no hay cliente
        }

    }
}