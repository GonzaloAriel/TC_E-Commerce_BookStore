using Dominio;
using Negocio;
using System;
using System.Globalization;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace E_Commerce_Bookstore
{
    public partial class ConfirmacionCompra : System.Web.UI.Page
    {
        private PedidoNegocio negocio = new PedidoNegocio();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                MostrarMensajes();
            }
        }

        private void MostrarMensajes()
        {
            string metodo = "";

            if (Session["MetodoPago"] != null)
                metodo = Session["MetodoPago"].ToString();

            // Título principal
            lblTitulo.Text = "¡Compra realizada!";

            // Mostrar método en la badge del header
            if (metodo == "TRANSFERENCIA")
                lblMetodo.Text = "Transferencia bancaria";

            if (metodo == "EFECTIVO")
                lblMetodo.Text = "Pago en efectivo";

            // Mensaje principal + color del cuadro
            if (metodo == "TRANSFERENCIA")
            {
                lblMensaje.Text = "Enviá el comprobante de transferencia para preparar tu pedido.";
                boxMensaje.Attributes["class"] = "alert alert-info mb-4";   // azul
            }
            else if (metodo == "EFECTIVO")
            {
                lblMensaje.Text = "Tu pedido está listo para retirar en el local.";
                boxMensaje.Attributes["class"] = "alert alert-success mb-4"; // verde
            }
            else
            {
                lblMensaje.Text = "Gracias por su compra.";
                boxMensaje.Attributes["class"] = "alert alert-primary mb-4"; // por defecto
            }
        }
    }
}