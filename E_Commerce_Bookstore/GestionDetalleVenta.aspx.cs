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
    public partial class GestionDetalleVenta : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["IdPedidoSeleccionado"] != null)
            {
                int idPedido = (int)Session["IdPedidoSeleccionado"];
                CargarPedido(idPedido);
            }
            else
            {
                // Por si entran sin selección previa
                Response.Redirect("GestionPedidos.aspx");
            }
        }

        private void CargarPedido(int id)
        {
            try
            {
                PedidoNegocio negocio = new PedidoNegocio();
                Pedido pedido = negocio.ObtenerPedido(id);
                List<PedidoDetalle> detalle = negocio.ListarDetalle(id);

                if (pedido == null)
                {
                    lbMensaje.Text = "Pedido no encontrado";
                    return;
                }

                // Datos del pedido
                txtId.Text = pedido.Id.ToString();
                txtCliente.Text = pedido.Cliente.Nombre;
                txtFecha.Text = pedido.Fecha.ToShortDateString();
                txtEstado.Text = pedido.Estado;
                txtDireccion.Text = pedido.DireccionEnvio;
                txtSubtotal.Text = pedido.Subtotal.ToString("C");
                txtTotal.Text = pedido.Total.ToString("C");

                // Grilla de detalle
                dgvDetalle.DataSource = detalle;
                dgvDetalle.DataBind();
            }
            catch (Exception ex)
            {
                lbMensaje.Text = "❌ Error al cargar Detalle: " + ex.Message;
                lbMensaje.ForeColor = System.Drawing.Color.Red;
            }

        }

        protected void btnVolver_Click(object sender, EventArgs e)
        {
            Response.Redirect("GestionVentas.aspx");
        }
    }
}