<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="GestionDetalleVenta.aspx.cs" Inherits="E_Commerce_Bookstore.GestionDetalleVenta" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container my-5">

        <div class="card shadow border-0">
            <div class="card-header bg-primary text-white">
                <h3 class="mb-0 text-center">Detalle del Pedido</h3>
            </div>
            <div class="card-header bg-primary text-white">
                <asp:Label ID="lbMensaje" runat="server" CssClass="fw-bold mt-3 text-center d-block"></asp:Label>  
            </div>

            <div class="card-body">

                <!-- Datos del pedido -->
                <div class="row g-3 mb-4">

                    <div class="col-md-3">
                        <label class="form-label">ID Pedido</label>
                        <asp:TextBox ID="txtId" runat="server" CssClass="form-control" ReadOnly="true" />
                    </div>

                    <div class="col-md-4">
                        <label class="form-label">Cliente</label>
                        <asp:TextBox ID="txtCliente" runat="server" CssClass="form-control" ReadOnly="true" />
                    </div>

                    <div class="col-md-3">
                        <label class="form-label">Fecha</label>
                        <asp:TextBox ID="txtFecha" runat="server" CssClass="form-control" ReadOnly="true" />
                    </div>

                    <div class="col-md-2">
                        <label class="form-label">Estado</label>
                        <asp:TextBox ID="txtEstado" runat="server" CssClass="form-control" ReadOnly="true" />
                    </div>

                    <div class="col-md-4">
                        <label class="form-label">Dirección de envío</label>
                        <asp:TextBox ID="txtDireccion" runat="server" CssClass="form-control" ReadOnly="true" />
                    </div>

                    <div class="col-md-4">
                        <label class="form-label">Subtotal</label>
                        <asp:TextBox ID="txtSubtotal" runat="server" CssClass="form-control" ReadOnly="true" />
                    </div>

                    <div class="col-md-4">
                        <label class="form-label">Total</label>
                        <asp:TextBox ID="txtTotal" runat="server" CssClass="form-control" ReadOnly="true" />
                    </div>
                </div>

                <!-- Detalle del pedido -->
                <h5 class="fw-bold mb-3">Artículos del pedido</h5>

                <asp:GridView ID="dgvDetalle" runat="server"
                    CssClass="table table-bordered table-hover text-center align-middle"
                    AutoGenerateColumns="False">
                    <Columns>
                        <asp:BoundField HeaderText="Libro" DataField="Titulo" />
                        <asp:BoundField HeaderText="Cantidad" DataField="Cantidad" />
                        <asp:BoundField HeaderText="Precio" DataField="PrecioUnitario" DataFormatString="{0:C}" />
                        <asp:BoundField HeaderText="Subtotal" DataField="Subtotal" DataFormatString="{0:C}" />
                    </Columns>
                </asp:GridView>

                <div class="mt-4 text-end">
                    <asp:Button ID="btnVolver" runat="server" CssClass="btn btn-secondary" Text="Volver" OnClick="btnVolver_Click" />
                </div>

            </div>
        </div>

    </div>
</asp:Content>
