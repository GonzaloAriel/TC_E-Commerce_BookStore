using Negocio;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace E_Commerce_Bookstore
{
    public partial class DetallePedido : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack) return;

            string numero = Request.QueryString["numero"];
            if (string.IsNullOrWhiteSpace(numero))
            {
                Response.Redirect("MisPedidos.aspx", false);
                return;
            }

            CargarCabecera(numero);
            CargarItems(numero);
        }

        private void CargarCabecera(string numero)
        {
            var datos = new AccesoDatos();
            try
            {
                datos.setearConsulta(@"
                    SELECT TOP 1 p.NumeroPedido, p.Fecha, p.Estado, p.MetodoPago, p.Total, p.IdCliente
                    FROM PEDIDOS p
                    WHERE p.NumeroPedido = @Numero");
                datos.setearParametro("@Numero", numero);
                datos.ejecutarLectura();

                if (!datos.Lector.Read())
                {
                    Response.Redirect("MisPedidos.aspx", false);
                    return;
                }

                // Validación opcional por sesión (seguridad)
                if (Session["IdCliente"] != null && datos.Lector["IdCliente"] != DBNull.Value)
                {
                    int idCli = Convert.ToInt32(Session["IdCliente"]);
                    if (idCli != Convert.ToInt32(datos.Lector["IdCliente"]))
                    {
                        Response.Redirect("MisPedidos.aspx", false);
                        return;
                    }
                }

                lblNumero.Text = datos.Lector["NumeroPedido"].ToString();
                lblFecha.Text = ((DateTime)datos.Lector["Fecha"]).ToString("dd/MM/yyyy");
                lblEstado.Text = datos.Lector["Estado"].ToString();
                lblPago.Text = datos.Lector["MetodoPago"].ToString();
                lblTotal.Text = Convert.ToDecimal(datos.Lector["Total"]).ToString("N2", CultureInfo.InvariantCulture);
            }
            finally
            {
                datos.cerrarConexion();
            }
        }

        private void CargarItems(string numero)
        {
            var datos = new AccesoDatos();
            try
            {
                datos.setearConsulta(@"
                    SELECT l.Titulo,
                           d.Cantidad,
                           d.PrecioUnitario,
                           (d.Cantidad * d.PrecioUnitario) AS Subtotal
                    FROM PEDIDOS_DETALLE d
                    INNER JOIN PEDIDOS p ON p.Id = d.IdPedido
                    INNER JOIN LIBROS  l ON l.Id = d.IdLibro
                    WHERE p.NumeroPedido = @Numero
                    ORDER BY l.Titulo");
                datos.setearParametro("@Numero", numero);
                datos.ejecutarLectura();

                var filas = new List<ItemFila>();
                while (datos.Lector.Read())
                {
                    filas.Add(new ItemFila
                    {
                        Titulo = datos.Lector["Titulo"].ToString(),
                        Cantidad = Convert.ToInt32(datos.Lector["Cantidad"]),
                        PrecioUnitario = Convert.ToDecimal(datos.Lector["PrecioUnitario"]),
                        Subtotal = Convert.ToDecimal(datos.Lector["Subtotal"])
                    });
                }

                gvItems.DataSource = filas;
                gvItems.DataBind();
            }
            finally
            {
                datos.cerrarConexion();
            }
        }

        private class ItemFila
        {
            public string Titulo { get; set; }
            public int Cantidad { get; set; }
            public decimal PrecioUnitario { get; set; }
            public decimal Subtotal { get; set; }
        }
    }
}