using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace E_Commerce_Bookstore
{
    public partial class Catalogo : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack) { }
        }
        protected void btnBuscar_Click(object sender, EventArgs e)
        {
            // por ahora vacío (o llamá a tu carga/búsqueda)
        }

    }
}