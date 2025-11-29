<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="GestionUsuario.aspx.cs" Inherits="E_Commerce_Bookstore.GestionUsuario" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container mt-5">
        <h2 class="text-center mb-4">Gestión de Usuarios</h2>

        <asp:Label ID="lblMensaje" runat="server" CssClass="fw-bold d-block mb-3"></asp:Label>

        <!-- 💠 GRILLA -->
        <asp:GridView ID="dgvUsuarios" runat="server" AutoGenerateColumns="False"
            CssClass="table table-bordered table-hover text-center"
            DataKeyNames="Id"
            OnSelectedIndexChanged="dgvUsuarios_SelectedIndexChanged">

            <Columns>
                <asp:BoundField HeaderText="ID" DataField="Id" />
                <asp:BoundField HeaderText="Usuario" DataField="NombreUsuario" />
                <asp:BoundField HeaderText="Email" DataField="Email" />
                <asp:BoundField HeaderText="Tipo" DataField="Rol" />
                <asp:CheckBoxField HeaderText="Activo" DataField="Activo" />
                <asp:CommandField ShowSelectButton="true" SelectText="Seleccionar" />
            </Columns>

        </asp:GridView>

        <hr />

        <!-- 📝 FORMULARIO -->
        <div class="row g-3">
            <div class="col-md-3">
                <label>ID</label>
                <asp:TextBox ID="txtId" runat="server" CssClass="form-control" ReadOnly="true"></asp:TextBox>
            </div>

            <div class="col-md-4">
                <label>Nombre Usuario</label>
                <asp:TextBox ID="txtNombreUsuario" runat="server" CssClass="form-control"></asp:TextBox>
            </div>

            <div class="col-md-4">
                <label>Contraseña</label>
                <asp:TextBox ID="txtContrasena" runat="server" CssClass="form-control" TextMode="Password"></asp:TextBox>
            </div>

            <div class="col-md-4">
                <label>Email</label>
                <asp:TextBox ID="txtEmail" runat="server" CssClass="form-control"></asp:TextBox>
            </div>

            <div class="col-md-4">
                <label>Tipo de Usuario</label>
                <asp:DropDownList ID="ddlTipoUsuario" runat="server" CssClass="form-select"></asp:DropDownList>
            </div>

            <div class="col-md-2 d-flex align-items-end">
                <asp:CheckBox ID="chkActivo" runat="server" CssClass="form-check-input me-2" />
                <label class="form-check-label">Activo</label>
            </div>
        </div>

        <!-- BOTONES -->
        <div class="mt-4">
            <asp:Button ID="btnAgregar" runat="server" Text="Agregar" CssClass="btn btn-success me-2" OnClick="btnAgregar_Click" />
            <asp:Button ID="btnModificar" runat="server" Text="Modificar" CssClass="btn btn-warning me-2" OnClick="btnModificar_Click" />
            <asp:Button ID="btnEliminar" runat="server" Text="Eliminar" CssClass="btn btn-danger me-2" OnClick="btnEliminar_Click" />
            <asp:Button ID="btnLimpiar" runat="server" Text="Limpiar" CssClass="btn btn-secondary" OnClick="btnLimpiar_Click" />
        </div>

        <div>
            <asp:Button ID="btnVolver" runat="server"
                CssClass="btn btn-info mt-2"
                Text="Volver a Clientes"
                OnClick="btnVolver_Click" />
        </div>
    </div>
</asp:Content>
