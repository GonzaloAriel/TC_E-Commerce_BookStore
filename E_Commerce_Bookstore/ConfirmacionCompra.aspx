<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ConfirmacionCompra.aspx.cs" Inherits="E_Commerce_Bookstore.ConfirmacionCompra" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

  <div class="container my-4 max-w-3xl">
    <div class="card shadow-sm">
      <div class="card-body">
        <div class="d-flex align-items-center justify-content-between mb-2">
          <div class="d-flex align-items-center gap-3">
            <span class="btn btn-success btn-sm disabled px-3">¡Compra confirmada!</span>
            <span class="badge" id="badgeEstado" runat="server">Estado</span>
          </div>
          <small class="text-muted">Fecha: <asp:Label ID="lblFecha" runat="server" /></small>
        </div>

        <h5 class="mb-1">Pedido <span class="text-primary">#<asp:Label ID="lblNro" runat="server" /></span></h5>
        <hr />

        <div class="row g-3">
          <div class="col-md-8">
            <div class="alert alert-info mb-0">
              Te enviamos un email con el detalle. Podés seguir el estado desde <a href="MisPedidos.aspx" class="alert-link">Mis pedidos</a>.
            </div>
          </div>
          <div class="col-md-4">
            <ul class="list-group">
              <li class="list-group-item d-flex justify-content-between">
                <span>Subtotal</span>
                <strong><asp:Label ID="lblSubtotal" runat="server" /></strong>
              </li>
              <li class="list-group-item d-flex justify-content-between">
                <span>Envío</span>
                <strong><asp:Label ID="lblEnvio" runat="server" /></strong>
              </li>
              <li class="list-group-item d-flex justify-content-between">
                <span>Total</span>
                <strong class="fs-5"><asp:Label ID="lblTotal" runat="server" /></strong>
              </li>
            </ul>
          </div>
        </div>

        <div class="d-flex gap-2 mt-3">
          <a href="MisPedidos.aspx" class="btn btn-primary">Ir a Mis pedidos</a>
          <a href="Catalogo.aspx" class="btn btn-outline-secondary">Seguir comprando</a>
        </div>
      </div>
    </div>
  </div>

  <!-- Error -->
  <asp:Panel ID="pnlError" runat="server" Visible="false" CssClass="container my-5">
    <div class="alert alert-warning">No se encontró el pedido. Volvé a <a href="MisPedidos.aspx" class="alert-link">Mis pedidos</a>.</div>
  </asp:Panel>

</asp:Content>


