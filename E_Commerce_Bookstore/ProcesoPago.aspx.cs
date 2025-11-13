using Dominio;
using E_Commerce_Bookstore.Helpers;
using Negocio;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Script.Serialization;
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
                pnlTransferencia.Visible = false;
                pnlEfectivo.Visible = false;               
            }
        }
        protected void rblMetodo_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Ocultar todos los paneles
            
            pnlTransferencia.Visible = false;
            pnlEfectivo.Visible = false;

            // Mostrar el seleccionado
            string metodo = rblMetodo.SelectedValue;            
            if (metodo == "TRANSFERENCIA") pnlTransferencia.Visible = true;
            else if (metodo == "EFECTIVO") pnlEfectivo.Visible = true;
        }        
        protected void btnConfirmarPago_Click(object sender, EventArgs e) 
        {
            //desde aca
            // Esto es para desactivar el carrito al finalizar la compra
            string cookieId = CookieHelper.ObtenerCookieId(Request, Response);
            int? idCliente = Session["IdCliente"] as int?;

            CarritoNegocio negocio = new CarritoNegocio();
            CarritoCompra carrito = negocio.ObtenerCarritoActivo(cookieId, idCliente);

            if (carrito != null)
            {
                negocio.DesactivarCarrito(carrito.Id);
                negocio.DescontarStockPorCarrito(carrito.Id);
                Session["Carrito"] = null;
                ((Site)Master).ActualizarCarritoVisual();
            }
            //Hasta aca

            Session["MetodoPago"] = rblMetodo.SelectedValue;
            Response.Redirect("ConfirmacionCompra.aspx");
        }
    }
}