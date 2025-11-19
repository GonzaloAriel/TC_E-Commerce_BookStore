using Negocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace E_Commerce_Bookstore
{
    public partial class RecuperarPassword : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnRecuperar_Click(object sender, EventArgs e)
        {
            if (!Page.IsValid)
                return;

            lblMensaje.Text = "";

            string email = txtEmail.Text.Trim();
            string nuevaPassword = txtNuevaPassword.Text.Trim();

            ClienteNegocio clienteNegocio = new ClienteNegocio();
            bool ok = clienteNegocio.CambiarPasswordPorEmail(email, nuevaPassword);

            if (ok)
            {
                lblMensaje.CssClass = "text-success";
                lblMensaje.Text = "Contraseña actualizada. Ya podés iniciar sesión.";
            }
            else
            {
                lblMensaje.CssClass = "text-danger";
                lblMensaje.Text = "No se pudo actualizar la contraseña. Revisá el email.";
            }
        }
    }
}