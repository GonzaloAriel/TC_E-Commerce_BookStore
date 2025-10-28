<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="GestionProducto.aspx.cs" Inherits="E_Commerce_Bookstore.GestionProducto" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container my-5">
        <div class="card shadow p-4">
            <h2 class="text-center mb-4">Gestión de Articulos</h2>

            <div class="row g-3">
                <!-- ID (solo lectura) -->
                <div class="col-md-2">
                    <label for="txtId" class="form-label">ID</label>
                    <asp:TextBox ID="txtId" runat="server" CssClass="form-control" ReadOnly="true"></asp:TextBox>
                </div>

                <div class="col-md-5">
                    <label for="txtTitulo" class="form-label">Título</label>
                    <asp:TextBox ID="txtTitulo" runat="server" CssClass="form-control" placeholder="Ej: Harry Potter y la Piedra Filosofal"></asp:TextBox>
                </div>

                <div class="col-md-5">
                    <label for="txtAutor" class="form-label">Autor</label>
                    <asp:TextBox ID="txtAutor" runat="server" CssClass="form-control" placeholder="Ej: J.K. Rowling"></asp:TextBox>
                </div>

                <!-- Descripción -->
                <div class="col-md-12">
                    <label for="txtDescripcion" class="form-label">Descripción</label>
                    <asp:TextBox ID="txtDescripcion" runat="server" CssClass="form-control" TextMode="MultiLine" Rows="3" placeholder="Descripción del producto..."></asp:TextBox>
                </div>

                <!-- ISBN e Idioma -->
                <div class="col-md-4">
                    <label for="txtISBN" class="form-label">ISBN</label>
                    <asp:TextBox ID="txtISBN" runat="server" CssClass="form-control"></asp:TextBox>
                </div>

                <div class="col-md-4">
                    <label for="txtIdioma" class="form-label">Idioma</label>
                    <asp:TextBox ID="txtIdioma" runat="server" CssClass="form-control"></asp:TextBox>
                </div>

                <div class="col-md-4">
                    <label for="txtAnioEdicion" class="form-label">Año de Edición</label>
                    <asp:TextBox ID="txtAnioEdicion" runat="server" CssClass="form-control" TextMode="Number"></asp:TextBox>
                </div>

                <!-- Páginas, Stock, Activo -->
                <div class="col-md-3">
                    <label for="txtPaginas" class="form-label">Páginas</label>
                    <asp:TextBox ID="txtPaginas" runat="server" CssClass="form-control" TextMode="Number"></asp:TextBox>
                </div>

                <div class="col-md-3">
                    <label for="txtStock" class="form-label">Stock</label>
                    <asp:TextBox ID="txtStock" runat="server" CssClass="form-control" TextMode="Number"></asp:TextBox>
                </div>

                <div class="col-md-3">
                    <label for="chkActivo" class="form-label">Activo</label><br />
                    <asp:CheckBox ID="chkActivo" runat="server" CssClass="form-check-input ms-2" Checked="true" />
                </div>

                <div class="col-md-3">
                    <label for="ddlCategoria" class="form-label">Categoría</label>
                    <asp:DropDownList ID="ddlCategoria" runat="server" CssClass="form-select">
                        <asp:ListItem Text="Seleccione..." Value=""></asp:ListItem>
                        <asp:ListItem Text="Libro" Value="1"></asp:ListItem>
                        <asp:ListItem Text="Cómic" Value="2"></asp:ListItem>
                        <asp:ListItem Text="Manga" Value="3"></asp:ListItem>
                    </asp:DropDownList>
                </div>

                <!-- Editorial -->
                <div class="col-md-6">
                    <label for="txtEditorial" class="form-label">Editorial</label>
                    <asp:TextBox ID="txtEditorial" runat="server" CssClass="form-control"></asp:TextBox>
                </div>

                <!-- Imagen -->
                <div class="col-md-6">
                    <label for="txtImagenUrl" class="form-label">URL de Imagen</label>
                    <asp:TextBox ID="txtImagenUrl" runat="server" CssClass="form-control" placeholder="https://..."></asp:TextBox>
                </div>

                <!-- Precio Compra y Venta -->
                <div class="col-md-4">
                    <label for="txtPrecioCompra" class="form-label">Precio Compra</label>
                    <asp:TextBox ID="txtPrecioCompra" runat="server" CssClass="form-control" TextMode="Number" step="0.01"></asp:TextBox>
                </div>

                <div class="col-md-4">
                    <label for="txtPrecioVenta" class="form-label">Precio Venta</label>
                    <asp:TextBox ID="txtPrecioVenta" runat="server" CssClass="form-control" TextMode="Number" step="0.01"></asp:TextBox>
                </div>

                <div class="col-md-4">
                    <label for="txtPorcentajeGanancia" class="form-label">% Ganancia</label>
                    <asp:TextBox ID="txtPorcentajeGanancia" runat="server" CssClass="form-control" ReadOnly="true"></asp:TextBox>
                </div>

                <!-- Botones -->
                <div class="col-12 text-center mt-4">
                    <asp:Button ID="btnGuardar" runat="server" Text="Guardar" CssClass="btn btn-success me-2" OnClick="btnGuardar_Click" />
                    <asp:Button ID="btnLimpiar" runat="server" Text="Limpiar" CssClass="btn btn-secondary" OnClick="btnLimpiar_Click" />
                </div>
            </div>
        </div>
    </div>
</asp:Content>
