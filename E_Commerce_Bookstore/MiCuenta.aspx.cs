using Dominio;
using E_Commerce_Bookstore.Helpers;
using Negocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace E_Commerce_Bookstore
{
    public partial class MiCuenta : System.Web.UI.Page
    {

        protected void Page_Load(object sender, EventArgs e)
        {            
        }

        protected void btnLogin_Click(object sender, EventArgs e)
        {
            lblMensajeLogin.Text = "";

            string email = txtEmailLogin.Text.Trim();
            string password = txtPasswordLogin.Text.Trim();

            if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password))
            {
                lblMensajeLogin.Text = "Ingresá email y contraseña.";
                return;
            }

            ClienteNegocio clienteNegocio = new ClienteNegocio();
            int idCliente = clienteNegocio.ValidarLogin(email, password);

            if (idCliente <= 0)
            {
                lblMensajeLogin.Text = "Email o contraseña incorrectos.";
                return;
            }

            // Guardamos el cliente en sesión
            Session["IdCliente"] = idCliente;

            // Obtener el objeto completo del cliente
            Cliente cliente = clienteNegocio.ObtenerClientePorId(idCliente);
            Session["Cliente"] = cliente;

            // Fusionar carrito anónimo con el del cliente
            string cookieId = ObtenerOCrearCookieId();
            CarritoNegocio carritoNegocio = new CarritoNegocio();
            carritoNegocio.FusionarCarritos(cookieId, idCliente);

            // Redirección
            string returnTo = Request.QueryString["ReturnUrl"];
            if (string.IsNullOrEmpty(returnTo))
                returnTo = "MiPerfil.aspx";

            Response.Redirect(returnTo, false);
        }

        private string ObtenerOCrearCookieId()
        {
            HttpCookie cookie = Request.Cookies["CarritoId"];

            if (cookie == null || string.IsNullOrEmpty(cookie.Value))
            {
                string nuevoId = Guid.NewGuid().ToString();
                cookie = new HttpCookie("CarritoId", nuevoId);
                cookie.Expires = DateTime.Now.AddDays(30);
                Response.Cookies.Add(cookie);
                return nuevoId;
            }

            return cookie.Value;
        }
    }
}