<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="E_Commerce_Bookstore.Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
     <!-- Aquí puedes agregar <style> o <script> específicos de esta página -->
</asp:Content>


<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="text-center">
        <h1 class="mb-4">Bienvenido a Book&Comic Store</h1>
        <p class="lead">Explora los mejores libros y cómics del momento 📚🦸‍♂️</p>
        <a href="Categorias.aspx" class="btn btn-primary btn-lg mt-3">Ver Categorías</a>
    </div>
</asp:Content>



