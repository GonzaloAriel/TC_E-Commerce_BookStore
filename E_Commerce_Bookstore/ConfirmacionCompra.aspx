<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ConfirmacionCompra.aspx.cs" Inherits="E_Commerce_Bookstore.ConfirmacionCompra" %>
<asp:Content ID="c1" ContentPlaceHolderID="MainContent" runat="server">
  <div class="container py-5 text-center">
    <h2 class="mb-3">¡Gracias por tu compra!</h2>
    <p>Tu número de pedido es:</p>
    <h3><asp:Label ID="lblPedido" runat="server" Text="(sin número)" /></h3>

    <div class="my-4">
      <p class="text-muted">Te enviamos un correo con el detalle del pedido.</p>
      <a href="MisPedidos.aspx" class="btn btn-primary">Ver mis pedidos</a>
      <a href="Catalogo.aspx" class="btn btn-outline-secondary ms-2">Seguir comprando</a>
    </div>
  </div>
</asp:Content>
