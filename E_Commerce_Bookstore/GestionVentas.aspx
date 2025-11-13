<%@ Page Title="Gestión de Ventas" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="GestionVentas.aspx.cs" Inherits="E_Commerce_Bookstore.GestionVentas" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <style>
        .alerta {
            padding: 12px;
            border-radius: 8px;
            font-weight: 500;
            margin-bottom: 20px;
        }

        .alert-success { background-color: #d1e7dd; color: #0f5132; border-left: 5px solid #198754; }
        .alert-warning { background-color: #fff3cd; color: #664d03; border-left: 5px solid #ffc107; }
        .alert-danger  { background-color: #f8d7da; color: #842029; border-left: 5px solid #dc3545; }

        .badge-pendiente { background-color: #ffc107; color: black; }
        .badge-enviado   { background-color: #0d6efd; }
        .badge-entregado { background-color: #198754; }
        .badge-cancelado { background-color: #dc3545; }

        .card {
            border-radius: 1rem;
        }
    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container my-5">
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>

                <!-- 🟩 Alerta visual -->
                <asp:Label ID="lblMensaje" runat="server" CssClass="d-block mb-3 fw-bold text-center"></asp:Label>

                <!-- 🧾 Gestión de Ventas -->
                <div class="card shadow border-0 mb-4">
                    <div class="card-header bg-primary text-white text-center py-3 rounded-top">
                        <h3 class="mb-0">Gestión de Ventas</h3>
                    </div>

                    <div class="card-body px-4 py-4">

                        <!-- Filtro -->
                        <div class="row g-3 align-items-end mb-4">
                            <div class="col-md-3">
                                <label for="ddlCampo" class="form-label">Campo</label>
                                <asp:DropDownList ID="ddlCampo" runat="server" CssClass="form-select" AutoPostBack="true">
                                    <asp:ListItem Text="Seleccione..." Value=""></asp:ListItem>
                                    <asp:ListItem Text="Cliente" Value="Cliente"></asp:ListItem>
                                    <asp:ListItem Text="Fecha" Value="Fecha"></asp:ListItem>
                                    <asp:ListItem Text="Estado" Value="Estado"></asp:ListItem>
                                </asp:DropDownList>
                            </div>

                            <div class="col-md-3">
                                <label for="txtFiltro" class="form-label">Filtro</label>
                                <asp:TextBox ID="txtFiltro" runat="server" CssClass="form-control" placeholder="Ingrese valor..."></asp:TextBox>
                            </div>

                            <div class="col-md-2">
                                <asp:Button ID="btnFiltrar" runat="server" Text="Filtrar" CssClass="btn btn-primary w-100" OnClick="btnFiltrar_Click" />
                            </div>
                            <div class="col-md-2">
                                <asp:Button ID="btnLimpiar" runat="server" Text="Limpiar" CssClass="btn btn-secondary w-100" OnClick="btnLimpiar_Click" />
                            </div>
                        </div>

                        <!-- Tabla principal -->
                        <div class="table-responsive mb-4">
                            <asp:GridView ID="dgvVentas" runat="server"
                                CssClass="table table-hover table-bordered text-center align-middle"
                                AutoGenerateColumns="False" DataKeyNames="Id"
                                 OnSelectedIndexChanged ="dgvVentas_SelectedIndexChanged">
                                <Columns>
                                    <asp:BoundField HeaderText="ID" DataField="Id" />
                                    <asp:BoundField HeaderText="Cliente" DataField="Cliente" />
                                    <asp:BoundField HeaderText="Fecha" DataField="Fecha" DataFormatString="{0:dd/MM/yyyy}" />
                                    <asp:BoundField HeaderText="Total" DataField="Total" DataFormatString="{0:C}" />
                                    <asp:TemplateField HeaderText="Estado">
                                        <ItemTemplate>
                                            <span class='<%# 
                                                Eval("Estado").ToString() == "Pendiente" ? "badge badge-pendiente" :
                                                Eval("Estado").ToString() == "Enviado" ? "badge badge-enviado" :
                                                Eval("Estado").ToString() == "Entregado" ? "badge badge-entregado" :
                                                "badge badge-cancelado" %>'>
                                                <%# Eval("Estado") %>
                                            </span>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:CommandField ShowSelectButton="true" SelectText="Seleccionar" HeaderText="Acción" />
                                </Columns>
                            </asp:GridView>
                        </div>

                        <!-- Detalle -->
                        <div class="card border-0 shadow-sm p-3 mb-4">
                            <h5 class="fw-bold mb-3">Detalle de Venta</h5>
                            <div class="row g-3">
                                <div class="col-md-3">
                                    <label class="form-label">ID Venta</label>
                                    <asp:TextBox ID="txtIdVenta" runat="server" CssClass="form-control" ReadOnly="true"></asp:TextBox>
                                </div>

                                <div class="col-md-3">
                                    <label class="form-label">Cliente</label>
                                    <asp:TextBox ID="txtCliente" runat="server" CssClass="form-control" ReadOnly="true"></asp:TextBox>
                                </div>

                                <div class="col-md-3">
                                    <label class="form-label">Fecha</label>
                                    <asp:TextBox ID="txtFecha" runat="server" CssClass="form-control" ReadOnly="true"></asp:TextBox>
                                </div>

                                <div class="col-md-3">
                                    <label class="form-label">Total</label>
                                    <asp:TextBox ID="txtTotal" runat="server" CssClass="form-control" ReadOnly="true"></asp:TextBox>
                                </div>

                                <div class="col-md-4">
                                    <label class="form-label">Estado</label>
                                    <asp:DropDownList ID="ddlEstadoVenta" runat="server" CssClass="form-select">
                                        <asp:ListItem>Seleccione...</asp:ListItem>
                                        <asp:ListItem>Pendiente</asp:ListItem>
                                        <asp:ListItem>Enviado</asp:ListItem>
                                        <asp:ListItem>Entregado</asp:ListItem>
                                        <asp:ListItem>Cancelado</asp:ListItem>
                                    </asp:DropDownList>
                                </div>

                                <div class="col-md-2 d-flex align-items-end">
                                    <asp:Button ID="btnActualizarEstado" runat="server" Text="Actualizar Estado" CssClass="btn btn-success w-100" OnClick="btnActualizarEstado_Click" />
                                </div>
                            </div>
                        </div>

                        <!-- Detalle de artículos -->
                        <asp:GridView ID="dgvDetalle" runat="server"
                            CssClass="table table-sm table-bordered text-center align-middle"
                            AutoGenerateColumns="False">
                            <Columns>
                                <asp:BoundField HeaderText="Título" DataField="Titulo" />
                                <asp:BoundField HeaderText="Cantidad" DataField="Cantidad" />
                                <asp:BoundField HeaderText="Precio Unitario" DataField="PrecioUnitario" DataFormatString="{0:C}" />
                                <asp:BoundField HeaderText="Subtotal" DataField="Subtotal" DataFormatString="{0:C}" />
                            </Columns>
                        </asp:GridView>

                    </div>
                </div>

            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
</asp:Content>
