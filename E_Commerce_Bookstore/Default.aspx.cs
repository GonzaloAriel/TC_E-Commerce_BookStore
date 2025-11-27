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
    public partial class Default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            CargarBannerDestacados();
        }

        private void CargarBannerDestacados()
        {
            try
            {
                LibroNegocio negocio = new LibroNegocio();

                // Imagen por defecto si un libro no tiene ImagenUrl
                string imagenPorDefecto = "https://placehold.co/400x600?text=Libro";

                // BEST SELLER
                List<Libro> listaBest = negocio.ListarBestSellers();
                if (listaBest != null && listaBest.Count > 0)
                {
                    Libro l = listaBest[0];

                    lblBestSellerTitulo.Text = l.Titulo;
                    lblBestSellerPrecio.Text = l.PrecioVenta.ToString("N2");

                    if (!string.IsNullOrEmpty(l.ImagenUrl))
                        imgBestSeller.ImageUrl = l.ImagenUrl;
                    else
                        imgBestSeller.ImageUrl = imagenPorDefecto;
                }

                // POPULARES
                List<Libro> listaPop = negocio.ListarPopulares();
                if (listaPop != null && listaPop.Count > 0)
                {
                    Libro l = listaPop[0];

                    lblPopularTitulo.Text = l.Titulo;
                    lblPopularPrecio.Text = l.PrecioVenta.ToString("N2");

                    if (!string.IsNullOrEmpty(l.ImagenUrl))
                        imgPopular.ImageUrl = l.ImagenUrl;
                    else
                        imgPopular.ImageUrl = imagenPorDefecto;
                }

                // OFERTAS
                List<Libro> listaOfer = negocio.ListarOfertas();
                if (listaOfer != null && listaOfer.Count > 0)
                {
                    Libro l = listaOfer[0];

                    lblOfertaTitulo.Text = l.Titulo;
                    lblOfertaPrecio.Text = l.PrecioVenta.ToString("N2");

                    if (!string.IsNullOrEmpty(l.ImagenUrl))
                        imgOferta.ImageUrl = l.ImagenUrl;
                    else
                        imgOferta.ImageUrl = imagenPorDefecto;
                }
            }
            catch (Exception ex)
            {
                Session["error"] = ex;
                Response.Redirect("Error.aspx", false);
                Context.ApplicationInstance.CompleteRequest();
            }
            
        }
    }
}