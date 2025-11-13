using System;
using System.Collections.Generic;
using E_Commerce_Bookstore.Helpers;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Negocio;

namespace E_Commerce_Bookstore
{
    public partial class MiCuenta : System.Web.UI.Page
    {
        
        protected void Page_Load(object sender, EventArgs e)
        {            
        }
        protected void btnLogin_Click(object sender, EventArgs e)
        {
            if (!Page.IsValid) return;

            var email = txtEmailLogin.Text.Trim();
            var pass = txtPassLogin.Text;

            var cliNeg = new ClienteNegocio();
            int idCliente = cliNeg.ValidarLogin(email, pass);

            if (idCliente > 0)
            {
                //new CarritoNegocio().AsegurarCarritoActivo(idCliente);
                Session["IdCliente"] = idCliente;
                //De Aca lo nuevo para vincular carrito con cliente..

                string cookieId = CookieHelper.ObtenerCookieId(Request, Response);
                CarritoNegocio negocio = new CarritoNegocio();
                negocio.AsignarClienteAlCarrito(cookieId, idCliente);

                //Hasta  aca..
                Session["UsuarioEmail"] = email;
                string returnTo = Request.QueryString["ReturnUrl"] ?? "ProcesoEnvio.aspx";
                Response.Redirect(returnTo, false);
            }
            else
            {
                lblMsgLogin.Text = "Email o contraseña inválidos.";
            }
        }

        protected void btnRegistrar_Click(object sender, EventArgs e)
        {
            if (!Page.IsValid) return;
            if (!int.TryParse(txtDNI.Text, out int dni)) { lblMsgReg.Text = "DNI inválido."; return; }

            var cliNeg = new ClienteNegocio();
            int idCliente = cliNeg.RegistrarCliente(
              nombre: txtNombre.Text.Trim(),
              apellido: txtApellido.Text.Trim(),
              dni: dni,
              email: txtEmailReg.Text.Trim(),
              telefono: txtTel.Text.Trim(),
              direccion: txtDireccion.Text.Trim(),
              cp: txtCP.Text.Trim(),
              password: txtPassReg.Text
            );

            if (idCliente > 0)
            {
                //new CarritoNegocio().AsegurarCarritoActivo(idCliente);
                
                //De Aca lo nuevo para vincular carrito con cliente..
                Session["IdCliente"] = idCliente;
                string cookieId = CookieHelper.ObtenerCookieId(Request, Response);
                CarritoNegocio negocio = new CarritoNegocio();
                negocio.AsignarClienteAlCarrito(cookieId, idCliente);
                //Hasta  aca..

                Session["UsuarioEmail"] = txtEmailReg.Text.Trim();
                Response.Redirect("ProcesoEnvio.aspx", false);
            }
            else if (idCliente == -1) lblMsgReg.Text = "Ese email ya está registrado.";
            else if (idCliente == -2) lblMsgReg.Text = "Ese DNI ya está registrado.";
            else lblMsgReg.Text = "No se pudo registrar. Probá más tarde.";
        }
    }
}