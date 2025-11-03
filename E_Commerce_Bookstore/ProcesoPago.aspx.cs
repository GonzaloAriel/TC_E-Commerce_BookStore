using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace E_Commerce_Bookstore
{
    public partial class ProcesoPago : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // Aseguramos que los paneles existan y arranquen ocultos
                if (pnlTarjeta != null) pnlTarjeta.Visible = false;
                if (pnlTransferencia != null) pnlTransferencia.Visible = false;
                if (pnlEfectivo != null) pnlEfectivo.Visible = false;

                // Valor de ejemplo (puede venir del carrito)
                if (lblMontoTransferencia != null)
                    lblMontoTransferencia.Text = "$12.500";
            }
        }
        protected void rblMetodo_SelectedIndexChanged(object sender, EventArgs e)
        {
            string metodo = rblMetodo.SelectedValue ?? string.Empty;

            // Ocultar todo
            pnlTarjeta.Visible = false;
            pnlTransferencia.Visible = false;
            pnlEfectivo.Visible = false;

            // Mostrar según selección
            if (metodo == "CREDITO" || metodo == "DEBITO")
                pnlTarjeta.Visible = true;
            else if (metodo == "TRANSFERENCIA")
                pnlTransferencia.Visible = true;
            else if (metodo == "EFECTIVO")
                pnlEfectivo.Visible = true;
        }
        protected void btnConfirmarPago_Click(object sender, EventArgs e) 
        {
            Response.Redirect("ConfirmacionCompra.aspx");
        }
    }
}