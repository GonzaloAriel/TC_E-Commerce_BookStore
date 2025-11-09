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
                CargarPedido();
            }
        }

        private void CargarPedido()
        {
            string num = Request.QueryString["num"];
            if (string.IsNullOrWhiteSpace(num))
            {
                MostrarError();
                return;
            }

            Pedido p = negocio.ObtenerPedidoPorNumero(num);
            if (p == null)
            {
                MostrarError();
                return;
            }

            // 👇 se crea acá la cultura para formatear en pesos argentinos
            CultureInfo esAR = new CultureInfo("es-AR");

            lblNro.Text = p.NumeroPedido;
            lblFecha.Text = p.Fecha.ToString("dd/MM/yyyy HH:mm");
            lblSubtotal.Text = p.Subtotal.ToString("C", esAR);
            lblEnvio.Text = p.TotalEnvio.ToString("C", esAR);
            lblTotal.Text = p.Total.ToString("C", esAR);

            // Configurar color de badge según estado
            string estado = (p.Estado ?? "").ToLower();
            string css = "badge rounded-pill ";
            if (estado.Contains("prep")) css += "text-bg-info";
            else if (estado.Contains("conf")) css += "text-bg-primary";
            else if (estado.Contains("entreg")) css += "text-bg-success";
            else if (estado.Contains("cancel")) css += "text-bg-danger";
            else css += "text-bg-secondary";

            badgeEstado.InnerText = p.Estado ?? "—";
            badgeEstado.Attributes["class"] = css;
        }

        private void MostrarError()
        {
            pnlError.Visible = true;
        }
    }
}