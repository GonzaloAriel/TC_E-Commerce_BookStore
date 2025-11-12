<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="MisPedidos.aspx.cs" Inherits="E_Commerce_Bookstore.MisPedidos" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

  <asp:Repeater ID="repPedidos" runat="server" Visible="false">
    <HeaderTemplate>
      <table class="table table-striped table-hover shadow-sm align-middle text-center">
        <thead class="table-primary">
          <tr>
            <th>N° Pedido</th>
            <th>Fecha</th>
            <th>Método de Pago</th>
            <th>Total ($)</th>
            <th>Estado</th>
            <th>Acción</th>
          </tr>
        </thead>
        <tbody>
    </HeaderTemplate>

    <ItemTemplate>
      <tr>
        <td><%# Eval("NumeroPedido") %></td>
        <td><%# Eval("Fecha", "{0:dd/MM/yyyy}") %></td>
        <td><%# Eval("MetodoPago") %></td>
        <td><%# Eval("Total", "{0:N2}") %></td>
        <td><%# Eval("Estado") %></td>
        <td>
          <a href='DetallePedido.aspx?numero=<%# Eval("NumeroPedido") %>' class="btn btn-sm btn-primary">
            Ver detalle
          </a>
        </td>
      </tr>
    </ItemTemplate>

    <FooterTemplate>
        </tbody>
      </table>
    </FooterTemplate>
  </asp:Repeater>

  <asp:Label ID="lblMensaje" runat="server" CssClass="text-muted" Visible="false"></asp:Label>

  <div class="mt-3 text-end">
    <a href="Catalogo.aspx" class="btn btn-outline-secondary">Volver a comprar</a>
  </div>

</asp:Content>

