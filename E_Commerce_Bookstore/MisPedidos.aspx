<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="MisPedidos.aspx.cs" Inherits="E_Commerce_Bookstore.MisPedidos" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

  <asp:Panel ID="pnlVacio" runat="server" Visible="false" CssClass="container my-5">
    <div class="alert alert-info">Todavía no tenés pedidos. Andá al <a href="Catalogo.aspx" class="alert-link">Catálogo</a>.</div>
  </asp:Panel>

  <div class="container my-4">
    <h3 class="mb-3">Mis pedidos</h3>

    <asp:Repeater ID="repPedidos" runat="server">
      <HeaderTemplate>
        <table class="table table-striped table-hover">
          <thead>
            <tr>
              <th>N°</th>
              <th>Fecha</th>
              <th>Estado</th>
              <th class="text-end">Total</th>
              <th class="text-end">Acciones</th>
            </tr>
          </thead>
          <tbody>
      </HeaderTemplate>

      <ItemTemplate>
        <tr>
          <td><%# Eval("NumeroPedido") %></td>
          <td><%# Eval("Fecha", "{0:dd/MM/yyyy HH:mm}") %></td>
          <td><%# Eval("Estado") %></td>
          <td class="text-end"><%# Eval("Total", "{0:C}") %></td>
          <td class="text-end">
  <a class="btn btn-sm btn-outline-primary"
     href='<%# "ConfirmacionCompra.aspx?num=" + Eval("NumeroPedido") %>'>
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
  </div>
</asp:Content>