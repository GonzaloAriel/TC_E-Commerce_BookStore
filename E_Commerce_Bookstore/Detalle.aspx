<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Detalle.aspx.cs" Inherits="E_Commerce_Bookstore.Detalle" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container mt-5">
    <div class="card shadow-lg p-4" style="background-color: #c6c7c8a6;">
        <div class="row">
            <!-- Imagen del libro -->
            <div class="col-md-4">
                <asp:Image ID="imgLibro" runat="server" CssClass="img-fluid rounded" ImageUrl="https://covers.openlibrary.org/b/id/14925450-L.jpg" />
            </div>

            <!-- Detalles del libro -->
            <div class="col-md-8">
                <h2>
                    <asp:Label ID="lblTitulo" runat="server" Text="Harry Potter y la piedra filosofal"></asp:Label></h2>
                <p class="text-muted">
                    Autor:
                <asp:Label ID="lblAutor" runat="server" Text="J. K. Rowling"></asp:Label>
                </p>
                <p class="text-muted">
                    Editorial:
                <asp:Label ID="lblEditorial" runat="server" Text="Emecé Editores"></asp:Label>
                </p>

                <p>
                    <asp:Label ID="lblDescripcion" runat="server" Text="Harry Potter jamás había oído hablar de Hogwarts cuando
                        las cartas empezaron a llegar al número cuatro de Privet Drive. Escritas con tinta verde sobre pergamino
                        amarillento y con un sello púrpura, fueron confiscadas rápidamente por sus tíos, unos hombres de aspecto
                        siniestro. Entonces, el día de su undécimo cumpleaños, un hombre enorme, de ojos saltones, llamado
                        Rubeus Hagrid, irrumpió con una noticia asombrosa: Harry Potter era un mago y tenía una plaza
                        en el Colegio Hogwarts de Magia y Hechicería. ¡Una increíble aventura estaba a punto de comenzar!"></asp:Label>
                </p>

                <h4 class="text-success">Precio: $<asp:Label ID="lblPrecio" runat="server" Text="49999.99"></asp:Label></h4>
                <p class="text-success">
                    Stock disponible:
                <asp:Label ID="lblStock" runat="server" Text="12"></asp:Label>
                </p>

                <!-- Incrementar cantidad + agregar + volver-->
                <div class="d-flex align-items-center gap-2 mb-3 flex-wrap">
                    <!-- Selector de cantidad -->
                    <div class="input-group" style="max-width: 150px;">
                        <button type="button" class="btn btn-outline-light border-2" style="background-color: gray;" onclick="decrementCantidad()">−</button>
                        <asp:TextBox ID="txtCantidad" runat="server" CssClass="form-control text-center" Text="1" />
                        <button type="button" class="btn btn-outline-light" style="background-color: gray;" onclick="incrementCantidad()">+</button>
                    </div>

                    <!-- Botón agregar al carrito -->
                    <asp:Button ID="btnAgregarCarrito" runat="server" CssClass="btn btn-primary border-2" Text="Agregar al carrito" />

                    <!-- Botón volver -->
                    <asp:Button ID="btnVolver" runat="server" CssClass="btn btn-success border-2" Text="Volver" />
                </div>

            </div>
        </div>
    </div>

</div>
<!-- Sección de libros sugeridos -->
<div class="container text-center mt-5">
    <h3 class="text-dark mb-4">Libros sugeridos</h3>
    <div class="row justify-content-center g-3">

        <!-- Libro 1 -->
        <div class="col-6 col-md-3 text-center">
            <asp:HyperLink ID="hlLibro1" runat="server" NavigateUrl="LibroDetalle.aspx?id=1">
                <asp:Image ID="imgLibro1" runat="server" CssClass="img-fluid rounded w-75" ImageUrl="https://covers.openlibrary.org/b/id/8231856-L.jpg" AlternateText="La música del silencio" />
            </asp:HyperLink>
            <asp:Label ID="lblLibro1" runat="server" CssClass="text-dark mt-2 d-block" Text="La música del silencio" />
        </div>

        <!-- Libro 2 -->
        <div class="col-6 col-md-3 text-center">
            <asp:HyperLink ID="hlLibro2" runat="server" NavigateUrl="LibroDetalle.aspx?id=2">
                <asp:Image ID="imgLibro2" runat="server" CssClass="img-fluid rounded w-75" ImageUrl="https://covers.openlibrary.org/b/id/8379256-L.jpg" AlternateText="Las mentiras de Locke Lamora" />
            </asp:HyperLink>
            <asp:Label ID="lblLibro2" runat="server" CssClass="text-dark mt-2 d-block" Text="Las mentiras de Locke Lamora" />
        </div>

        <!-- Libro 3 -->
        <div class="col-6 col-md-3 text-center">
            <asp:HyperLink ID="hlLibro3" runat="server" NavigateUrl="LibroDetalle.aspx?id=3">
                <asp:Image ID="imgLibro3" runat="server" CssClass="img-fluid rounded w-75" ImageUrl="https://covers.openlibrary.org/b/id/8231857-L.jpg" AlternateText="Elantris" />
            </asp:HyperLink>
            <asp:Label ID="lblLibro3" runat="server" CssClass="text-dark mt-2 d-block" Text="Elantris" />
        </div>

        <!-- Libro 4 -->
        <div class="col-6 col-md-3 text-center">
            <asp:HyperLink ID="hlLibro4" runat="server" NavigateUrl="LibroDetalle.aspx?id=4">
                <asp:Image ID="imgLibro4" runat="server" CssClass="img-fluid rounded w-75" ImageUrl="https://covers.openlibrary.org/b/id/8231858-L.jpg" AlternateText="El mago" />
            </asp:HyperLink>
            <asp:Label ID="lblLibro4" runat="server" CssClass="text-dark mt-2 d-block" Text="El mago" />
        </div>

    </div>
</div>

<!-- Para incrementar la cantidad de libros para mandar al carrito -->
<script>
    function incrementCantidad() {
        var txt = document.getElementById('<%= txtCantidad.ClientID %>');
        var valor = parseInt(txt.value) || 0;
        txt.value = valor + 1;
    }

    function decrementCantidad() {
        var txt = document.getElementById('<%= txtCantidad.ClientID %>');
        var valor = parseInt(txt.value) || 0;
        if (valor > 1) {
            txt.value = valor - 1;
        }
    }
</script>
</asp:Content>
