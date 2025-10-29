<%@ Page Title="Catálogo" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Catalogo.aspx.cs" Inherits="E_Commerce_Bookstore.Catalogo" %>
<asp:Content ID="Head1" ContentPlaceHolderID="HeadContent" runat="server"></asp:Content>

<asp:Content ID="Main1" ContentPlaceHolderID="MainContent" runat="server">
    <h2 class="mb-3">Catálogo</h2>

    <asp:TextBox ID="txtBuscar" runat="server" CssClass="form-control mb-2" Placeholder="Buscar..."></asp:TextBox>
    <asp:Button ID="btnBuscar" runat="server" Text="Buscar" CssClass="btn btn-secondary mb-3" OnClick="btnBuscar_Click" />

    <asp:Repeater ID="repLibros" runat="server">
        <ItemTemplate>
            <div class="card mb-3">
                <div class="row g-0">
                    <div class="col-md-2 p-2">
                        <img src='<%# Eval("ImagenUrl") %>' class="img-fluid" alt="img" />
                    </div>
                    <div class="col-md-10">
                        <div class="card-body">
                            <h5 class="card-title"><%# Eval("Titulo") %></h5>
                            <p class="card-text mb-1"><strong>Autor:</strong> <%# Eval("Autor") %></p>
                            <p class="card-text mb-1"><strong>Editorial:</strong> <%# Eval("Editorial") %></p>
                            <p class="card-text"><strong>Precio:</strong> $ <%# Eval("PrecioVenta", "{0:N2}") %></p>
                        </div>
                    </div>
                </div>
            </div>
        </ItemTemplate>
    </asp:Repeater>
</asp:Content>
