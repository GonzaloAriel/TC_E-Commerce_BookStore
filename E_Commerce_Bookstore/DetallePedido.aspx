<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="DetallePedido.aspx.cs" Inherits="E_Commerce_Bookstore.DetallePedido" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
  <div class="container py-4">
    <h2 class="text-primary fw-bold mb-3">🧾 Detalle del pedido</h2>

    <div class="card shadow-sm mb-3">
      <div class="card-body">
        <div class="row g-3">
          <div class="col-sm-6">
            <strong>N° Pedido:</strong> <asp:Label ID="lblNumero" runat="server" />
          </div>
          <div class="col-sm-6">
            <strong>Fecha:</strong> <asp:Label ID="lblFecha" runat="server" />
          </div>
          <div class="col-sm-6">
            <strong>Estado:</strong> <asp:Label ID="lblEstado" runat="server" />
          </div>
          <div class="col-sm-6">
            <strong>Método de pago:</strong> <asp:Label ID="lblPago" runat="server" />
          </div>
          <div class="col-sm-6">
            <strong>Total:</strong> $ <asp:Label ID="lblTotal" runat="server" />
          </div>
        </div>
      </div>
    </div>

    <div class="table-responsive shadow-sm">
      <asp:GridView ID="gvItems" runat="server" AutoGenerateColumns="False"
        CssClass="table table-striped table-hover align-middle text-center mb-0"
        HeaderStyle-CssClass="table-primary">
        <Columns>
          <asp:BoundField DataField="Titulo" HeaderText="Libro" />
          <asp:BoundField DataField="Cantidad" HeaderText="Cantidad" />
          <asp:BoundField DataField="PrecioUnitario" HeaderText="Precio unit." DataFormatString="{0:N2}" />
          <asp:BoundField DataField="Subtotal" HeaderText="Subtotal" DataFormatString="{0:N2}" />
        </Columns>
      </asp:GridView>
    </div>

    <div class="mt-3 d-flex gap-2">
      <a href="MisPedidos.aspx" class="btn btn-outline-secondary">← Volver a Mis Pedidos</a>
      <a href="Catalogo.aspx" class="btn btn-primary">Volver a comprar</a>
    </div>
  </div>
</asp:Content>
