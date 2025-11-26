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
            // Si el usuario no esta logueado, lo mandamos al login
            if (Session["IdCliente"] == null)
            {
                Response.Redirect("MiCuenta.aspx?ReturnUrl=ProcesoEnvio.aspx", false);
                return;
            }

            try
            {
                if (!IsPostBack)
                {
                    bool envioADomicilio = false;
                    if (Session["EnvioADomicilio"] != null)
                    {
                        bool.TryParse(Session["EnvioADomicilio"].ToString(), out envioADomicilio);
                    }

                    // Mostrar/ocultar paneles segun el metodo
                    pnlEntrega.Visible = envioADomicilio;

                    if (envioADomicilio)
                    {
                        chkFacturacion.Visible = true;
                        pnlFacturacion.Visible = chkFacturacion.Checked;
                        ToggleValidators(pnlEntrega, true);
                        ToggleValidators(pnlFacturacion, pnlFacturacion.Visible);
                    }
                    else
                    {
                        chkFacturacion.Visible = false;
                        pnlFacturacion.Visible = true;
                        ToggleValidators(pnlEntrega, false);
                        ToggleValidators(pnlFacturacion, true);
                    }
                }

                // Ocultar navbar
                var master = this.Master as Site;
                if (master != null)
                {
                    master.OcultarNavbar();
                }
            }
            catch
            {
                Response.Redirect("Error.aspx", false);
            }
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
            if (!Page.IsValid)
                return;

            try
            {
                // Dato de contacto
                Session["EmailContacto"] = txtEmail.Text.Trim();

                // Envio
                Session["NombreEnvio"] = txtNombre.Text.Trim();
                Session["ApellidoEnvio"] = txtApellido.Text.Trim();
                Session["DireccionEnvio"] = txtCalle.Text.Trim();
                Session["BarrioEnvio"] = txtBarrio.Text.Trim();
                Session["CiudadEnvio"] = txtCiudad.Text.Trim();
                Session["DeptoEnvio"] = txtDepto.Text.Trim();
                Session["CPEnvio"] = txtCP.Text.Trim();

                // Facturacion
                Session["NombreFacturacion"] = txtFacNombre.Text.Trim();
                Session["ApellidoFacturacion"] = txtFacApellido.Text.Trim();
                Session["DireccionFacturacion"] = txtFacCalle.Text.Trim();
                Session["BarrioFacturacion"] = txtFacBarrio.Text.Trim();
                Session["CiudadFacturacion"] = txtFacCiudad.Text.Trim();
                Session["DeptoFacturacion"] = txtFacDepto.Text.Trim();
                Session["CPFacturacion"] = txtFacCP.Text.Trim();

                Response.Redirect("ProcesoPago.aspx", false);
            }
            catch
            {
                Response.Redirect("Error.aspx", false);
            }
        }

    }
}