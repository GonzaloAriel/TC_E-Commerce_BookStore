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
    public partial class GestionUsuario : System.Web.UI.Page
    {
        UsuarioNegocio negocio = new UsuarioNegocio();
        TipoUsuarioNegocio negocioTipo = new TipoUsuarioNegocio();
        ValidacionGestion validar = new ValidacionGestion();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CargarGrilla();
                CargarTipos();
            }
        }

        private void CargarGrilla()
        {
            var lista = negocio.ListarConTipo();
            dgvUsuarios.DataSource = lista;
            dgvUsuarios.DataBind();
        }
        private void CargarTipos()
        {
            ddlTipoUsuario.DataSource = negocioTipo.Listar();
            ddlTipoUsuario.DataTextField = "Rol";
            ddlTipoUsuario.DataValueField = "Id";
            ddlTipoUsuario.DataBind();

            ddlTipoUsuario.Items.Insert(0, new System.Web.UI.WebControls.ListItem("Seleccione...", "0"));
        }

        protected void btnLimpiar_Click(object sender, EventArgs e)
        {
            txtId.Text = "";
            txtNombreUsuario.Text = "";
            txtContrasena.Text = "";
            txtEmail.Text = "";
            ddlTipoUsuario.SelectedIndex = 0;
            chkActivo.Checked = false;
            lblMensaje.Text = "";
        }

        protected void btnEliminar_Click(object sender, EventArgs e)
        {
            int id = int.Parse(txtId.Text);
            negocio.Eliminar(id);
            lblMensaje.Text = "Usuario eliminado.";
            CargarGrilla();
        }

        protected void btnModificar_Click(object sender, EventArgs e)
        {
            try
            {
                Usuario u = ObtenerDesdeFormulario();
                u.Id = int.Parse(txtId.Text);

                if (validar.EmailExiste(u.Email, u.Id))
                {
                    lblMensaje.Text = "❌ Este email ya pertenece a otro usuario.";
                    return;
                }

                negocio.Modificar(u);
                lblMensaje.Text = "<div class='alert alert-success'> Usuario modificado correctamente. </div>";
                CargarGrilla();
            }
            catch (Exception ex)
            {
                lblMensaje.Text = "<div class='alert alert-danger'>Error: " + ex.Message + "</div>";
            }

        }

        protected void btnAgregar_Click(object sender, EventArgs e)
        {
            try
            {
                Usuario u = ObtenerDesdeFormulario();

                if (validar.EmailExiste(u.Email))
                {
                    lblMensaje.Text = "<div class='alert alert-danger'>Error: ❌ El email ya está registrado. </div>";
                    return;
                }



                negocio.Agregar(u);
                lblMensaje.Text = "<div class='alert alert-success'> Usuario agregado correctamente. </div>";
                CargarGrilla();

            }
            catch (Exception ex)
            {
                lblMensaje.Text = "<div class='alert alert-danger'>Error: " + ex.Message + "</div>";
            }
        }

        private Usuario ObtenerDesdeFormulario()
        {
            return new Usuario
            {
                NombreUsuario = txtNombreUsuario.Text,
                Contrasena = txtContrasena.Text,
                Email = txtEmail.Text,
                IdTipoUsuario = new TipoUsuario { Id = int.Parse(ddlTipoUsuario.SelectedValue) },
                Activo = chkActivo.Checked
            };
        }

        protected void dgvUsuarios_SelectedIndexChanged(object sender, EventArgs e)
        {
            var fila = dgvUsuarios.SelectedRow;

            txtId.Text = fila.Cells[0].Text;
            txtNombreUsuario.Text = fila.Cells[1].Text;
            txtEmail.Text = fila.Cells[2].Text;

            ddlTipoUsuario.SelectedValue = negocio.ObtenerIdTipo(int.Parse(txtId.Text)).ToString();

            chkActivo.Checked = ((CheckBox)fila.Cells[4].Controls[0]).Checked;
        }

        protected void btnVolver_Click(object sender, EventArgs e)
        {
            Session["EmailCliente"] = txtEmail.Text;
            Session["IdUsuarioCliente"] = txtId.Text;

            Response.Redirect("GestionClientes.aspx");
        }
    }
}