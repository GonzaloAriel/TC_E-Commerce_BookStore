<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ProcesoEnvio.aspx.cs" Inherits="E_Commerce_Bookstore.ProcesoEnvio" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h2>Proceso de Envío</h2>

    <asp:Panel ID="pnlDireccion" runat="server" CssClass="card p-3 mb-3">
        <h4>Dirección de entrega</h4>
        <asp:TextBox ID="txtDireccion" runat="server" CssClass="form-control" Placeholder="Ej: Av. Siempre Viva 742"></asp:TextBox>
        <asp:TextBox ID="txtCiudad" runat="server" CssClass="form-control mt-2" Placeholder="Ciudad"></asp:TextBox>
        <asp:TextBox ID="txtProvincia" runat="server" CssClass="form-control mt-2" Placeholder="Provincia"></asp:TextBox>
        <asp:TextBox ID="txtCodigoPostal" runat="server" CssClass="form-control mt-2" Placeholder="Código Postal"></asp:TextBox>
    </asp:Panel>

    <asp:Panel ID="pnlMetodo" runat="server" CssClass="card p-3 mb-3">
        <h4>Método de envío</h4>
        <asp:RadioButtonList ID="rblMetodos" runat="server" CssClass="form-check">
            <asp:ListItem Text="Envío estándar (3-5 días) - $1500" Value="Estandar" Selected="True"></asp:ListItem>
            <asp:ListItem Text="Envío exprés (24 hs) - $2500" Value="Express"></asp:ListItem>
            <asp:ListItem Text="Retiro en tienda (sin costo)" Value="Tienda"></asp:ListItem>
        </asp:RadioButtonList>
    </asp:Panel>

    <asp:Button ID="btnContinuar" runat="server" Text="Continuar al pago" CssClass="btn btn-primary" OnClick="btnContinuar_Click" />
</asp:Content>
