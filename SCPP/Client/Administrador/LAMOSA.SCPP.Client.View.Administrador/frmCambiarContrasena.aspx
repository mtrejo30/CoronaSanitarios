<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="frmCambiarContrasena.aspx.cs" Inherits="LAMOSA.SCPP.Client.View.Administrador.frmCambiarContrasena" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="headCambiarContrasena" runat="server">
    <title>Lamosa - Cambiar Contraseña</title>   
    <link href= "~/Estiloscss/pro-line-down-fly/menu3.css"  media="screen" rel="stylesheet" type="text/css" />
    <link href="~/Estiloscss/Estilos.css" rel="stylesheet" type="text/css" />
    <link href="Estiloscss/menu.css" rel="Stylesheet" type="text/css" />
</head>
<body class = "fondoLogin">
    <form id="frmCambiarContrasena" runat="server">
    <div>
    <table id="tblmainCambiarContrasena" align ="center" width ="1024" >
    <tr>
    <td class = "Header1" style="width:694px" />
    <td class ="Header2" style="width:320px">
    <table>
    <tr>
    <td><label class ="Header2Text" id ="lblfecha" runat="server"/><% DateTime.Now.ToString("dd/MM/yyyy"); %></td>
    <td><label class ="Header2Text" id ="lblhora" runat="server"/><% DateTime.Now.ToString("hh:mm:ss");%></td>
    </tr>
    </table>
    </td>
    </tr>
    <tr>
    <td class= "Header3"/>
    </tr>
    <tr>
    <td colspan="3" style="height:20px"><div id="pro_linedrop"/></td>
    </tr>
    <tr>
    <td style="height:10px" colspan= "2"></td>
    </tr>
    </table>
    </div>
    </form>
</body>
</html>
