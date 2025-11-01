using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace E_Commerce_Bookstore
{
    public partial class ProcesoEnvio : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //Master.ShowChrome(false);
                pnlFacturacion.Visible=false;
                /*string modo = Request.QueryString["modo"];

                if (modo == "local")
                {
                    // Ocultamos todo lo de entrega, dejamos sólo facturación
                    pnlFacturacion.Visible = true;
                    contenedorEntrega.Visible = false; // ver más abajo
                    chkFacturacion.Visible = false;    // ya no hace falta el check
                }
                else
                {
                    // flujo normal: envío a domicilio
                    pnlFacturacion.Visible = false;
                    contenedorEntrega.Visible = true;
                    chkFacturacion.Visible = true;
                }*/
            }
        }
        protected void chkFacturacion_CheckedChanged(object sender, EventArgs e)
        {
            pnlFacturacion.Visible=chkFacturacion.Checked;
        }
                
        protected void btnContinuar_Click(object sender, EventArgs e)
        {
            Response.Redirect("ProcesoPago.aspx");
        }     
    }
}