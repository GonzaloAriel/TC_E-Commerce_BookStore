<%@ Page Title="Catálogo" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Catalogo.aspx.cs" Inherits="E_Commerce_Bookstore.Catalogo" %>

<asp:Content ID="Content" ContentPlaceHolderID="MainContent" runat="server">

  <h2 class="mb-4">Catálogo</h2>

  <!-- BUSCADOR (Autor / ISBN / Categoría) -->
  <div class="mb-4">
    <div class="input-group">
      <asp:TextBox ID="txtBuscar" runat="server" CssClass="form-control"
                   placeholder="Buscar por autor, ISBN o categoría..." />
      <asp:Button ID="btnBuscar" runat="server" Text="Buscar"
                  CssClass="btn btn-primary" OnClick="btnBuscar_Click" />
    </div>
    <small class="text-muted">Ej.: “Gaiman”, “978…”, “Fantasía”</small>
  </div>

  <div class="row">
    <!-- Sidebar: Categorías -->
    <aside class="col-md-3 mb-4">
      <h5>Categorías</h5>
      <asp:Repeater ID="repCategorias" runat="server" OnItemCommand="repCategorias_ItemCommand">
        <ItemTemplate>
          <div>
            <asp:LinkButton runat="server" CssClass="btn btn-link text-start p-0"
              CommandName="FiltrarCategoria" CommandArgument='<%# Eval("Id") %>'>
              <%# Eval("Nombre") %>
            </asp:LinkButton>
          </div>
        </ItemTemplate>
      </asp:Repeater>
      <hr />
      <asp:LinkButton ID="lnkVerTodo" runat="server" CssClass="btn btn-outline-secondary btn-sm"
                      OnClick="lnkVerTodo_Click">
        Ver todos los libros
      </asp:LinkButton>
    </aside>

    <!-- Cards -->
    <section class="col-md-9">
      <div class="row row-cols-1 row-cols-md-2 row-cols-lg-3 g-4">
        <asp:Repeater ID="repLibros" runat="server" OnItemCommand="repLibros_ItemCommand">
          <ItemTemplate>
            <div class="col">
              <div class="card h-100 shadow-sm">
                <img src='<%# Eval("ImagenUrl") %>' class="card-img-top"
                     style="object-fit:cover;height:280px;" alt='<%# Eval("Titulo") %>' />
                <div class="card-body d-flex flex-column">
                  <h6 class="card-title mb-2 text-truncate" title='<%# Eval("Titulo") %>'>
                    <%# Eval("Titulo") %>
                  </h6>
                  <p class="fw-bold text-success mb-3">
                    $ <%# Eval("PrecioVenta","{0:N2}") %>
                  </p>
                  <div class="mt-auto d-flex justify-content-between">
                    <asp:Button runat="server" Text="Ver Detalle" CssClass="btn btn-outline-secondary btn-sm"
                                CommandName="Detalle" CommandArgument='<%# Eval("Id") %>' OnCommand="btnAccionCommand"/> 
                    <asp:Button runat="server" Text="Comprar" CssClass="btn btn-primary btn-sm"
                                CommandName="Comprar" CommandArgument='<%# Eval("Id") %>' OnCommand="btnAccionCommand"/>
                  </div>
                </div>
              </div>
            </div>
          </ItemTemplate>
        </asp:Repeater>
      </div>

      <asp:Label ID="lblMensaje" runat="server" CssClass="text-success d-block mt-3"></asp:Label>
    </section>
  </div>

</asp:Content>