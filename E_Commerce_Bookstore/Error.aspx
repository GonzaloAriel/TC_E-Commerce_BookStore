<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Error.aspx.cs" Inherits="E_Commerce_Bookstore.Error" %>
<asp:Content ID="c1" ContentPlaceHolderID="MainContent" runat="server">
  <div class="container py-5">
    <div class="alert alert-danger">
      Ocurrió un error procesando tu operación. Intentá nuevamente.
    </div>
    <a href="Catalogo.aspx" class="btn btn-outline-secondary">Volver al catálogo</a>
  </div>
</asp:Content>
