<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ProcesoPago.aspx.cs" Inherits="E_Commerce_Bookstore.ProcesoPago" UnobtrusiveValidationMode="None"%>
<asp:Content ID="c1" ContentPlaceHolderID="MainContent" runat="server">
  <div class="container py-4" style="max-width:800px">

    <h2 class="mb-4">Proceso de Pago</h2>

    <!-- ==================== MÉTODO DE PAGO ==================== -->
   <!-- MÉTODO DE PAGO -->
<div class="mb-3">
  <label class="form-label d-block">Método de pago</label>
  <asp:RadioButtonList ID="rblMetodo" runat="server" CssClass="form-check"
      RepeatDirection="Vertical" AutoPostBack="true"
      OnSelectedIndexChanged="rblMetodo_SelectedIndexChanged">
    <asp:ListItem Text="Tarjeta de crédito" Value="CREDITO" />
    <asp:ListItem Text="Tarjeta de débito" Value="DEBITO" />
    <asp:ListItem Text="Transferencia bancaria" Value="TRANSFERENCIA" />
    <asp:ListItem Text="Efectivo" Value="EFECTIVO" />
  </asp:RadioButtonList>
  <asp:RequiredFieldValidator runat="server" ControlToValidate="rblMetodo"
      ErrorMessage="Seleccione un método de pago"
      CssClass="text-danger" Display="Dynamic" />
</div>


    <!-- ==================== TARJETA (crédito/débito) ==================== -->
    <asp:Panel ID="pnlTarjeta" runat="server" CssClass="border rounded p-3 mb-3" Visible="false">
      <h5 class="mb-3">Datos de la tarjeta</h5>
      <div class="row g-3">
        <div class="col-md-6">
          <label class="form-label">Número de tarjeta</label>
          <asp:TextBox ID="txtNumeroTarjeta" runat="server" CssClass="form-control" MaxLength="16" />
          <asp:RequiredFieldValidator runat="server" ControlToValidate="txtNumeroTarjeta"
            ErrorMessage="Número requerido" CssClass="text-danger" Display="Dynamic" />
        </div>
        <div class="col-md-3">
          <label class="form-label">Vencimiento (MM/AA)</label>
          <asp:TextBox ID="txtVencimiento" runat="server" CssClass="form-control" MaxLength="5" />
          <asp:RequiredFieldValidator runat="server" ControlToValidate="txtVencimiento"
            ErrorMessage="Vencimiento requerido" CssClass="text-danger" Display="Dynamic" />
        </div>
        <div class="col-md-3">
          <label class="form-label">CVV</label>
          <asp:TextBox ID="txtCVV" runat="server" CssClass="form-control" MaxLength="4" />
          <asp:RequiredFieldValidator runat="server" ControlToValidate="txtCVV"
            ErrorMessage="CVV requerido" CssClass="text-danger" Display="Dynamic" />
        </div>
        <div class="col-md-12">
          <label class="form-label">Titular</label>
          <asp:TextBox ID="txtTitular" runat="server" CssClass="form-control" />
          <asp:RequiredFieldValidator runat="server" ControlToValidate="txtTitular"
            ErrorMessage="Nombre del titular requerido" CssClass="text-danger" Display="Dynamic" />
        </div>
      </div>
    </asp:Panel>

    <!-- ==================== TRANSFERENCIA BANCARIA ==================== -->
    <asp:Panel ID="pnlTransferencia" runat="server" CssClass="border rounded p-3 mb-3" Visible="false">
      <h5 class="mb-3">Datos para transferencia bancaria</h5>
      <p>Realizá la transferencia con los siguientes datos:</p>
      <ul>
        <li><strong>Banco:</strong> Banco Nación Argentina</li>
        <li><strong>CBU:</strong> 0110599500095958476523</li>
        <li><strong>Alias:</strong> EBOOKSTORE.PAGOS</li>
        <li><strong>Titular:</strong> E-Bookstore SRL</li>
        <li><strong>Monto total:</strong> <asp:Label ID="lblMontoTransferencia" runat="server" Text="$0.00" /></li>
      </ul>
      <small class="text-muted">Por favor, enviá el comprobante a pagos@ebookstore.com.ar para confirmar tu pedido.</small>
    </asp:Panel>

    <!-- ==================== EFECTIVO ==================== -->
    <asp:Panel ID="pnlEfectivo" runat="server" CssClass="border rounded p-3 mb-3" Visible="false">
      <h5 class="mb-3">Pago en efectivo</h5>
      <p>Podés abonar en efectivo al momento de recibir el pedido o al retirarlo en el local.</p>
      <p class="text-muted">Recordá tener el monto justo, no garantizamos cambio disponible.</p>
    </asp:Panel>

    <!-- ==================== VALIDACIONES Y BOTONES ==================== -->
    <asp:ValidationSummary ID="vsPago" runat="server" CssClass="text-danger mt-3" />

    <div class="mt-4 d-flex gap-2">
      <a href="ProcesoEnvio.aspx" class="btn btn-outline-secondary">Volver</a>
      <asp:Button ID="btnConfirmarPago" runat="server"
        Text="Confirmar pago"
        CssClass="btn btn-success"
        OnClick="btnConfirmarPago_Click" />
    </div>

  </div>
</asp:Content>