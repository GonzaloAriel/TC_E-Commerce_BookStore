<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="MisPedidos.aspx.cs" Inherits="E_Commerce_Bookstore.MisPedidos" %>
<asp:Content ID="c1" ContentPlaceHolderID="MainContent" runat="server">
  <div class="container py-4">
    <h2 class="mb-3">Mis pedidos</h2>

    <!-- Cuando no hay pedidos -->
    <asp:Panel ID="pnlVacio" runat="server" Visible="false" CssClass="alert alert-info">
      Aún no tenés pedidos registrados.
    </asp:Panel>

    <!-- Lista de pedidos -->
    <asp:Repeater ID="repPedidos" runat="server">
      <HeaderTemplate>
        <div class="table-responsive">
          <table class="table table-striped align-middle">
            <thead>
              <tr>
                <th>N.º Pedido</th>
                <th>Fecha</th>
                <th>Estado</th>
                <th>Método de pago</th>
                <th>Método de envío</th>
                <th class="text-end">Subtotal</th>
                <th class="text-end">Envío</th>
                <th class="text-end">Total</th>
                <th>Dirección de envío</th>
              </tr>
            </thead>
            <tbody>
      </HeaderTemplate>

      <ItemTemplate>
        <tr>
          <td><%# Eval("NumeroPedido") %></td>
          <td><%# Eval("Fecha", "{0:dd/MM/yyyy HH:mm}") %></td>
          <td><span class="badge bg-secondary"><%# Eval("Estado") %></span></td>
          <td><%# Eval("MetodoPago") %></td>
          <td><%# Eval("MetodoEnvio") %></td>
          <td class="text-end">$ <%# Eval("Subtotal","{0:N2}") %></td>
          <td class="text-end">$ <%# Eval("TotalEnvio","{0:N2}") %></td>
          <td class="text-end fw-semibold">$ <%# Eval("Total","{0:N2}") %></td>
          <td><%# Eval("DireccionDeEnvio") %></td>
        </tr>
      </ItemTemplate>

      <FooterTemplate>
            </tbody>
          </table>
        </div>
      </FooterTemplate>
    </asp:Repeater>

  </div>
</asp:Content>
