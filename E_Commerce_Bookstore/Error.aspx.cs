using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace E_Commerce_Bookstore
{
    public partial class Error : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            // Ocultar el navbar desde el code-behind
            var master = this.Master as Site;
            if (master != null)
            {
                master.OcultarNavbar();
            }
        }
    }
}