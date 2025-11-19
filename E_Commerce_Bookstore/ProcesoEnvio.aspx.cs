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
    public partial class ProcesoEnvio : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //Si no hay cliente en sesión, mandamos a MiCuenta con ReturnUrl
            if (Session["IdCliente"] == null)
            {
                Response.Redirect("MiCuenta.aspx?ReturnUrl=ProcesoEnvio.aspx", false);
                return;
            }
            if (!IsPostBack)
            {
                CargarDatosCliente();

                //Leer lo que se eligio en el carrito
                bool envioADomicilio = false;
                if (Session["EnvioADomicilio"] != null)
                {
                    bool.TryParse(Session["EnvioADomicilio"].ToString(), out envioADomicilio);
                }
                pnlEntrega.Visible=envioADomicilio;

                if (envioADomicilio)
                {
                    chkFacturacion.Visible=true;
                    pnlFacturacion.Visible = chkFacturacion.Checked;
                    ToggleValidators(pnlEntrega, true);
                    ToggleValidators(pnlFacturacion, pnlFacturacion.Visible);
                }
                else
                {
                    //Sin envio a domicilio
                    chkFacturacion.Visible = false;
                    pnlFacturacion.Visible = true;
                    ToggleValidators(pnlEntrega, false);
                    ToggleValidators(pnlFacturacion, true);
                }                
            }
        }

        private void CargarDatosCliente()
        {
            int idCliente = (int)Session["IdCliente"];

            ClienteNegocio cliNeg = new ClienteNegocio();
            Cliente cliente = cliNeg.ObtenerClientePorId(idCliente);

            if (cliente == null)
                return;

            // TextBox de email de contacto:
            txtEmail.Text = cliente.Email;

            // Para panel de ENTREGA:
            txtNombre.Text = cliente.Nombre;
            txtApellido.Text = cliente.Apellido;
            txtCalle.Text = cliente.Direccion ?? "";
            txtCP.Text = cliente.CP ?? "";
            

            // Para panel de FACTURACION
            txtFacNombre.Text = cliente.Nombre;
            txtFacApellido.Text = cliente.Apellido;
            txtFacCalle.Text = cliente.Direccion ?? "";
            txtFacCP.Text = cliente.CP ?? "";
            
        }
        // habilitar/deshabilitar validadores dentro de un contenedor
        private void ToggleValidators(Control root, bool enabled)
        {
            if (root == null) return;

            foreach (Control c in root.Controls)
            {
                var val = c as BaseValidator;
                if (val != null) val.Enabled = enabled;

                if (c.HasControls())
                    ToggleValidators(c, enabled);
            }
        }
        protected void chkFacturacion_CheckedChanged(object sender, EventArgs e)
        {
            pnlFacturacion.Visible=chkFacturacion.Checked;

            ToggleValidators(pnlFacturacion, pnlFacturacion.Visible);
        }
                
        protected void btnContinuar_Click(object sender, EventArgs e)
        {
            Response.Redirect("ProcesoPago.aspx");            
        }     
    }
}