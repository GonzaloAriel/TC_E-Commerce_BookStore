<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="MiCuenta.aspx.cs" Inherits="E_Commerce_Bookstore.MiCuenta" UnobtrusiveValidationMode="None" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
  <div class="container mt-4">
    <h2 class="mb-4">Mi cuenta</h2>

    <div class="row g-4">
      <!-- LOGIN -->
      <div class="col-md-5">
        <div class="card p-3 h-100">
          <h4 class="mb-3">Acceder</h4>

          <label>Email *</label>
          <asp:TextBox ID="txtEmailLogin" runat="server" CssClass="form-control" TextMode="Email" />
          <asp:RequiredFieldValidator runat="server" ControlToValidate="txtEmailLogin"
              ErrorMessage="Email requerido" CssClass="text-danger" Display="Dynamic" ValidationGroup="Login" />

          <label class="mt-2">Contraseña *</label>
          <asp:TextBox ID="txtPassLogin" runat="server" TextMode="Password" CssClass="form-control" />
          <asp:RequiredFieldValidator runat="server" ControlToValidate="txtPassLogin"
              ErrorMessage="Contraseña requerida" CssClass="text-danger" Display="Dynamic" ValidationGroup="Login" />

          <div class="mt-3">
            <asp:Button ID="btnLogin" runat="server" Text="Acceder" CssClass="btn btn-primary"
                        OnClick="btnLogin_Click" ValidationGroup="Login" />
            <asp:LinkButton ID="lnkRecuperar" runat="server" CausesValidation="false"
                            CssClass="btn btn-link" PostBackUrl="RecuperarClave.aspx">
              ¿Olvidaste tu contraseña?
            </asp:LinkButton>
          </div>

          <asp:ValidationSummary runat="server" ValidationGroup="Login" CssClass="text-danger mt-2" />
          <asp:Label ID="lblMsgLogin" runat="server" CssClass="text-danger mt-2" />
        </div>
      </div>

      <!-- REGISTRO -->
      <div class="col-md-7">
        <div class="card p-3 h-100">
          <h4 class="mb-3">Crear cuenta</h4>

          <div class="row">
            <div class="col-md-6">
              <label>Nombre *</label>
              <asp:TextBox ID="txtNombre" runat="server" CssClass="form-control" />
              <asp:RequiredFieldValidator runat="server" ControlToValidate="txtNombre"
                  ErrorMessage="Campo obligatorio" CssClass="text-danger" Display="Dynamic" ValidationGroup="Registro" />
            </div>

            <div class="col-md-6">
              <label>Apellido *</label>
              <asp:TextBox ID="txtApellido" runat="server" CssClass="form-control" />
              <asp:RequiredFieldValidator runat="server" ControlToValidate="txtApellido"
                  ErrorMessage="Campo obligatorio" CssClass="text-danger" Display="Dynamic" ValidationGroup="Registro" />
            </div>

            <div class="col-md-6">
              <label>DNI *</label>
              <asp:TextBox ID="txtDNI" runat="server" CssClass="form-control" />
              <asp:RequiredFieldValidator runat="server" ControlToValidate="txtDNI"
                  ErrorMessage="Campo obligatorio" CssClass="text-danger" Display="Dynamic" ValidationGroup="Registro" />
              <asp:RegularExpressionValidator runat="server" ControlToValidate="txtDNI"
                  ValidationExpression="^\d{7,8}$" ErrorMessage="DNI inválido"
                  CssClass="text-danger" Display="Dynamic" ValidationGroup="Registro" />
            </div>

            <div class="col-md-6">
              <label>Email *</label>
              <asp:TextBox ID="txtEmailReg" runat="server" CssClass="form-control" TextMode="Email" />
              <asp:RequiredFieldValidator runat="server" ControlToValidate="txtEmailReg"
                  ErrorMessage="Campo obligatorio" CssClass="text-danger" Display="Dynamic" ValidationGroup="Registro" />
              <asp:RegularExpressionValidator runat="server" ControlToValidate="txtEmailReg"
                  ValidationExpression="^[^@\s]+@[^@\s]+\.[^@\s]+$" ErrorMessage="Email inválido"
                  CssClass="text-danger" Display="Dynamic" ValidationGroup="Registro" />
            </div>

            <div class="col-md-6">
              <label>Teléfono</label>
              <asp:TextBox ID="txtTel" runat="server" CssClass="form-control" />
              <asp:RegularExpressionValidator runat="server" ControlToValidate="txtTel"
                  ValidationExpression="^\d{6,15}$" ErrorMessage="Teléfono inválido"
                  CssClass="text-danger" Display="Dynamic" ValidationGroup="Registro" />
            </div>

            <div class="col-md-3">
              <label>Clave *</label>
              <asp:TextBox ID="txtPassReg" runat="server" TextMode="Password" CssClass="form-control" />
              <asp:RequiredFieldValidator runat="server" ControlToValidate="txtPassReg"
                  ErrorMessage="Campo obligatorio" CssClass="text-danger" Display="Dynamic" ValidationGroup="Registro" />
              <asp:RegularExpressionValidator runat="server" ControlToValidate="txtPassReg"
                  ValidationExpression="^.{6,}$" ErrorMessage="Mínimo 6 caracteres"
                  CssClass="text-danger" Display="Dynamic" ValidationGroup="Registro" />
            </div>

            <div class="col-md-3">
              <label>Repetí la clave *</label>
              <asp:TextBox ID="txtPassReg2" runat="server" TextMode="Password" CssClass="form-control" />
              <asp:CompareValidator runat="server" ControlToCompare="txtPassReg" ControlToValidate="txtPassReg2"
                  ErrorMessage="Las contraseñas no coinciden" CssClass="text-danger"
                  Display="Dynamic" ValidationGroup="Registro" />
            </div>
          </div>

          <div class="row mt-3">
            <div class="col-md-8">
              <label>Dirección *</label>
              <asp:TextBox ID="txtDireccion" runat="server" CssClass="form-control" />
              <asp:RequiredFieldValidator runat="server" ControlToValidate="txtDireccion"
                  ErrorMessage="Campo obligatorio" CssClass="text-danger" Display="Dynamic" ValidationGroup="Registro" />
            </div>

            <div class="col-md-4">
              <label>Código Postal *</label>
              <asp:TextBox ID="txtCP" runat="server" CssClass="form-control" />
              <asp:RequiredFieldValidator runat="server" ControlToValidate="txtCP"
                  ErrorMessage="Campo obligatorio" CssClass="text-danger" Display="Dynamic" ValidationGroup="Registro" />
            </div>
          </div>

          <div class="mt-3">
            <asp:Button ID="btnRegistrar" runat="server" Text="Registrarme" CssClass="btn btn-success"
                        OnClick="btnRegistrar_Click" ValidationGroup="Registro" />
            <asp:ValidationSummary runat="server" ValidationGroup="Registro" CssClass="text-danger mt-2" />
            <asp:Label ID="lblMsgReg" runat="server" CssClass="text-danger ms-3" />
          </div>
        </div>
      </div>
    </div>
  </div>
</asp:Content>