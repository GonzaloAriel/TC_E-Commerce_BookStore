using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using Dominio;
using Negocio;

namespace E_Commerce_Bookstore
{
    public partial class GestionVentas : System.Web.UI.Page
    {
        PedidoNegocio negocio = new PedidoNegocio();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
                cargarGrilla();
        }

        protected void btnEliminar_Click(object sender, EventArgs e)
        {
            try
            {
                int id = int.Parse(txtId.Text);
                negocio.Eliminar(id);

                lbMensaje.Text = "✅ Pedido eliminado correctamente.";
                lbMensaje.ForeColor = System.Drawing.Color.Red;

                cargarGrilla();
            }
            catch (Exception ex)
            {
                lbMensaje.Text = "❌ Error al eliminar el pedido: " + ex.Message;
                lbMensaje.ForeColor = System.Drawing.Color.Red;
            }

        }

        protected void btnModificar_Click(object sender, EventArgs e)
        {
            try
            {
                Pedido pedido = obtenerPedidoDesdeFormulario();
                pedido.Id = int.Parse(txtId.Text);

                negocio.Modificar(pedido);

                lbMensaje.Text = "✅ Pedido modificado correctamente.";
                lbMensaje.ForeColor = System.Drawing.Color.Green;

                cargarGrilla();
            }
            catch (Exception ex)
            {
                lbMensaje.Text = "❌ Error al modificar el pedido: " + ex.Message;
                lbMensaje.ForeColor = System.Drawing.Color.Red;
            }

        }

        protected void btnAgregar_Click(object sender, EventArgs e)
        {
            try
            {
                Pedido pedido = obtenerPedidoDesdeFormulario();
                negocio.Agregar(pedido);

                lbMensaje.Text = "✅ Pedido agregado correctamente.";
                lbMensaje.ForeColor = System.Drawing.Color.Green;

                cargarGrilla();
            }
            catch (Exception ex)
            {
                lbMensaje.Text = "❌ Error al guardar el libro: " + ex.Message;
                lbMensaje.ForeColor = System.Drawing.Color.Red;
            }

        }

        protected void dgvPedidos_SelectedIndexChanged(object sender, EventArgs e)
        {
            int id = Convert.ToInt32(dgvPedidos.SelectedDataKey.Value);

            var pedido = negocio.Listar().FirstOrDefault(p => p.Id == id);

            if (pedido == null) return;

            txtId.Text = pedido.Id.ToString();
            txtIdCliente.Text = pedido.Cliente.Id.ToString();
            txtClienteNombre.Text = pedido.Cliente.Nombre;
            txtNumeroPedido.Text = pedido.NumeroPedido;
            txtFecha.Text = pedido.Fecha.ToString("yyyy-MM-dd");
            ddlEstado.SelectedValue = pedido.Estado;
            txtSubtotal.Text = pedido.Subtotal.ToString();
            txtTotal.Text = pedido.Total.ToString();
            txtDireccion.Text = pedido.DireccionEnvio;
        }

        protected void btnLimpiar_Click(object sender, EventArgs e)
        {
            LimpiarCampos();
            cargarGrilla();
        }

        protected void LimpiarCampos()
        {
            txtId.Text = "";
            txtIdCliente.Text = "";
            txtClienteNombre.Text = "";
            txtNumeroPedido.Text = "";
            txtFecha.Text = "";
            ddlEstado.ClearSelection();
            txtSubtotal.Text = "";
            txtTotal.Text = "";
            txtDireccion.Text = "";
            cargarGrilla();
            lbMensaje.Text = "";
        }

        protected void btnBuscar_Click(object sender, EventArgs e)
        {
            cargarGrilla(txtFiltro.Text);
        }

        private void cargarGrilla(string filtro = "")
        {
            var lista = negocio.Listar();

            if (!string.IsNullOrEmpty(filtro))
            {
                filtro = filtro.ToLower();
                lista = lista.Where(p =>
                    p.Cliente.Nombre.ToLower().Contains(filtro) ||
                    p.NumeroPedido.ToLower().Contains(filtro)
                ).ToList();
            }

            // PROYECTO PARA LA GRILLA
            var vista = lista.Select(p => new
            {
                p.Id,
                ClienteNombre = p.Cliente.Nombre,
                p.NumeroPedido,
                p.Fecha,
                p.Estado,
                p.Total
            }).ToList();

            dgvPedidos.DataSource = vista;
            dgvPedidos.DataBind();

        }

        private Pedido obtenerPedidoDesdeFormulario()
        {
            return new Pedido
            {
                Cliente = new Cliente
                {
                    Id = int.Parse(txtIdCliente.Text),
                    Nombre = txtClienteNombre.Text
                },
                NumeroPedido = txtNumeroPedido.Text,
                Fecha = DateTime.Parse(txtFecha.Text),
                Estado = ddlEstado.SelectedValue,
                Subtotal = decimal.Parse(txtSubtotal.Text),
                Total = decimal.Parse(txtTotal.Text),
                DireccionEnvio = txtDireccion.Text
            };
        }
    }
}
