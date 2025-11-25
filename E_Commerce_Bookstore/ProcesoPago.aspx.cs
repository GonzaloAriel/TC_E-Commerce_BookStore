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
                pnlTarjeta.Visible = false;

                var master = this.Master as Site;
                if (master != null)
                {
                    master.OcultarNavbar();
                }
            }
        }
        protected void rblMetodo_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Ocultar todos los paneles
            
            pnlTransferencia.Visible = false;
            pnlEfectivo.Visible = false;
            pnlTarjeta.Visible = false;

            // Mostrar el seleccionado
            string metodo = rblMetodo.SelectedValue;
            if (metodo == "TRANSFERENCIA")
            {
                pnlTransferencia.Visible = true;

                // Traer total real del carrito
                string cookieId = CookieHelper.ObtenerCookieId(Request, Response);
               
                if (Session["IdCliente"] == null)
                    return;

                int idCliente = (int)Session["IdCliente"];

                CarritoNegocio negocio = new CarritoNegocio();
                CarritoCompra carrito = negocio.ObtenerCarritoActivo(cookieId, idCliente);

                if (carrito != null)
                    lblMontoTransferencia.Text = " $" + carrito.Total.ToString("N2");
            }
            else if (metodo == "EFECTIVO")
            {
                pnlEfectivo.Visible = true;
            }
            else if (metodo == "DEBITO" || metodo == "CREDITO")
            {
                pnlTarjeta.Visible = true;
            }
        }
        protected void btnConfirmarPago_Click(object sender, EventArgs e)
        {
            // Validaciones de los TextBox (tarjeta) se manejan en el .aspx
            if (!Page.IsValid)
                return;

            // Debe haber un método elegido
            if (string.IsNullOrEmpty(rblMetodo.SelectedValue))
            {
                // si querés, podrías mostrar un label de error acá
                return;
            }

            // Guardamos el método de pago en sesión (para ConfirmacionCompra)
            string metodoPago = rblMetodo.SelectedValue;
            Session["MetodoPago"] = metodoPago;

            // Verificamos cliente logueado
            
            if (Session["IdCliente"] == null)
            {
                Response.Redirect("MiCuenta.aspx?ReturnUrl=ProcesoPago.aspx", false);
                return;
            }
            int idCliente = (int)Session["IdCliente"];

            // Obtenemos el carrito actual
            string cookieId = CookieHelper.ObtenerCookieId(Request, Response);
            CarritoNegocio carritoNegocio = new CarritoNegocio();
            CarritoCompra carrito = carritoNegocio.ObtenerCarritoActivo(cookieId, idCliente);

            if (carrito == null)
            {
                // Si no hay carrito, volvemos al carrito
                Response.Redirect("Carrito.aspx", false);
                return;
            }

            // ==========================
            // Crear el PEDIDO
            // ==========================
            PedidoNegocio pedidoNegocio = new PedidoNegocio();

            Pedido pedido = new Pedido();
            pedido.Cliente = new Cliente { Id = idCliente };
            pedido.NumeroPedido = pedidoNegocio.GenerarNumeroPedido();
            pedido.Fecha = DateTime.Now;
            pedido.Estado = "Pendiente";
            pedido.Subtotal = carrito.Total;   // si tenés Subtotal separado, podés cambiarlo
            pedido.Total = carrito.Total;            
            // === FACTURACIÓN ===
            pedido.NombreFacturacion = Session["NombreFacturacion"]?.ToString();
            pedido.ApellidoFacturacion = Session["ApellidoFacturacion"]?.ToString();
            pedido.DireccionFacturacion = Session["DireccionFacturacion"]?.ToString();
            pedido.BarrioFacturacion = Session["BarrioFacturacion"]?.ToString();
            pedido.CiudadFacturacion = Session["CiudadFacturacion"]?.ToString();
            pedido.DeptoFacturacion = Session["DeptoFacturacion"]?.ToString();
            pedido.CPFacturacion = Session["CPFacturacion"]?.ToString();


            int idPedido = pedidoNegocio.CrearPedido(pedido);

            // ==========================
            // Crear DETALLES del pedido
            // ==========================
            if (carrito.Items != null)
            {
                foreach (var item in carrito.Items)
                {
                    pedidoNegocio.CrearDetallePedido(idPedido, item);
                }
            }

            // ==========================
            // Registrar PAGO
            // ==========================
            decimal montoPago = carrito.Total;
            pedidoNegocio.RegistrarPago(idPedido, montoPago, metodoPago);

            // ==========================
            // Registrar ENVÍO 
            // ==========================
            bool envioADomicilio = false;
            if (Session["EnvioADomicilio"] != null)
            {
                bool.TryParse(Session["EnvioADomicilio"].ToString(), out envioADomicilio);
            }

            if (envioADomicilio)
            {
                string barrio = Session["BarrioEnvio"] != null ? Session["BarrioEnvio"].ToString() : "";
                string ciudad = Session["CiudadEnvio"] != null ? Session["CiudadEnvio"].ToString() : "";
                string depto = Session["DeptoEnvio"] != null ? Session["DeptoEnvio"].ToString() : "";
                string nombre = Session["NombreEnvio"]?.ToString();
                string apellido = Session["ApellidoEnvio"]?.ToString();
                string direccion = Session["DireccionEnvio"]?.ToString();
                string cp = Session["CPEnvio"]?.ToString();
                pedidoNegocio.RegistrarEnvio(idPedido, "Envío a domicilio", 0m,barrio,ciudad,depto,nombre,apellido,cp,direccion);
            }
           
            // ==========================
            //  Descontar stock y cerrar carrito
            // ==========================
            carritoNegocio.DescontarStockPorCarrito(carrito.Id);
            carritoNegocio.DesactivarCarrito(carrito.Id);

            Session["Carrito"] = null;

            var master = this.Master as Site;
            if (master != null)
            {
                master.ActualizarCarritoVisual();
            }

            // Guardamos el último pedido en sesión (para usar en ConfirmacionCompra si queremos)
            Session["UltimoPedidoId"] = idPedido;
            Session["UltimoNumeroPedido"] = pedido.NumeroPedido;

            // ==========================
            // Ir a Confirmación
            // ==========================
            Response.Redirect("ConfirmacionCompra.aspx", false);
        }
    }
}
