<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ProcesoPago.aspx.cs" Inherits="E_Commerce_Bookstore.ProcesoPago" UnobtrusiveValidationMode="None"%>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <div class="container my-4">

        <h2 class="mb-3">Método de pago</h2>

        <!-- LISTA DE MÉTODOS -->
        <asp:RadioButtonList ID="rblMetodo" runat="server" AutoPostBack="true"
            OnSelectedIndexChanged="rblMetodo_SelectedIndexChanged">
            <asp:ListItem Value="TRANSFERENCIA">Transferencia bancaria</asp:ListItem>
            <asp:ListItem Value="EFECTIVO">Pago en efectivo</asp:ListItem>
            <asp:ListItem Value="DEBITO">Tarjeta de débito</asp:ListItem>
            <asp:ListItem Value="CREDITO">Tarjeta de crédito</asp:ListItem>
        </asp:RadioButtonList>

        <hr />

        <!-- TRANSFERENCIA -->
        <asp:Panel ID="pnlTransferencia" runat="server" CssClass="mt-3" Visible="false">
            <h4>Transferencia bancaria</h4>
            <p>Alias: <strong>LIBRO.TIENDA.PAGO</strong></p>
            <p>CBU: <strong>00012345000987654321</strong></p>

            <p class="mt-3">
                Total a pagar:
                <strong><asp:Label ID="lblMontoTransferencia" runat="server"></asp:Label></strong>
            </p>
        </asp:Panel>

        <!-- EFECTIVO -->
        <asp:Panel ID="pnlEfectivo" runat="server" CssClass="mt-3" Visible="false">
            <h4>Pago en efectivo</h4>
            <p>Podés abonar en el local al retirar el pedido.</p>
        </asp:Panel>

        <!-- TARJETA (DÉBITO O CRÉDITO, REUTILIZADO) -->
        <asp:Panel ID="pnlTarjeta" runat="server" CssClass="mt-3" Visible="false">
            <h4>Datos de la tarjeta</h4>

            <div class="mb-2">
                <label>Número de tarjeta</label>
                <asp:TextBox ID="txtTarjetaNumero" runat="server" CssClass="form-control" />
                <asp:RequiredFieldValidator runat="server" ControlToValidate="txtTarjetaNumero"
                    ErrorMessage="Ingresá el número de tarjeta"
                    CssClass="text-danger" Display="Dynamic" />
            </div>

            <div class="mb-2">
                <label>Nombre del titular</label>
                <asp:TextBox ID="txtTarjetaNombre" runat="server" CssClass="form-control" />
                <asp:RequiredFieldValidator runat="server" ControlToValidate="txtTarjetaNombre"
                    ErrorMessage="Ingresá el nombre del titular"
                    CssClass="text-danger" Display="Dynamic" />
            </div>

            <div class="row">
                <div class="col-md-6 mb-2">
                    <label>Vencimiento (MM/AA)</label>
                    <asp:TextBox ID="txtTarjetaVencimiento" runat="server" CssClass="form-control" />
                    <asp:RequiredFieldValidator runat="server" ControlToValidate="txtTarjetaVencimiento"
                        ErrorMessage="Ingresá el vencimiento"
                        CssClass="text-danger" Display="Dynamic" />
                </div>

                <div class="col-md-6 mb-2">
                    <label>CVV</label>
                    <asp:TextBox ID="txtTarjetaCVV" runat="server" CssClass="form-control" TextMode="Password" />
                    <asp:RequiredFieldValidator runat="server" ControlToValidate="txtTarjetaCVV"
                        ErrorMessage="Ingresá el CVV"
                        CssClass="text-danger" Display="Dynamic" />
                </div>
            </div>
        </asp:Panel>

        <asp:Button ID="btnConfirmarPago" runat="server" CssClass="btn btn-primary mt-4"
            Text="Confirmar pago" OnClick="btnConfirmarPago_Click" />

    </div>

</asp:Content>