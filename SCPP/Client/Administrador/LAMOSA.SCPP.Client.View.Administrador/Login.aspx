<%@ Page Language="C#" 
    AutoEventWireup="true"  
    Inherits="LAMOSA.SCPP.Client.View.Administrador.Login" 
    CodeBehind="Login.aspx.cs"%>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">

<head runat="server">
    <title>Lamosa - Login</title>   
    <link href="~/Estiloscss/pro-line-down-fly/menu3.css" media="screen" rel="stylesheet" type="text/css" />
    <link href="~/Estiloscss/Estilos.css" rel="stylesheet" type="text/css" />
    <link href="Estiloscss/menu.css" rel="Stylesheet" type="text/css" />
</head>

<body class="fondoLogin">
    <form id="form1" runat="server">
    <div>
        <table align="center" width="1024px"  border="0" cellpadding="0" cellspacing="0" style="background-color:white;">
            <tr>
                <td  rowspan = "2" class= "Header1">&nbsp;     
                </td>
                <td  class= "Header2">
                    <label class="Header2Text" id="lblfecha" runat="server"></label> | <label class="Header2Text" id="lblHora" runat="server"></label>&nbsp;
                </td>
            </tr>
            <tr>
                <td class= "Header3">&nbsp;     
                </td>
            </tr>
            <tr>
                <td colspan="3" style="height: 20px">
                    <div id="pro_linedrop"></div>
                </td>
            </tr>
            <tr>
                <td style="height:10px" colspan= "2"></td>
            </tr>
        </table>
        <table align="center" width="1024px"  border="0" cellpadding="0" cellspacing="0" style="background-color:white; height:400px;" >
            <tr style=" height:30px;">
                <td style=" width:10px; background-color:#eee;"></td>
                <td colspan= "2" style=" width:1014px;"  class ="lblTitulo1">
                    <asp:Label ID="lblTitulo" runat="server" Text="Login" ></asp:Label><br/>
                </td> 
            </tr>
            <tr>
                <td colspan = "3">
                     <table align="center" width="300px"  border="0" cellpadding="0" cellspacing="0" >
                        <tr>
                            <td  class="textos_Login" style="height:30px;">
                                <asp:Label ID="lblCuentaUsuario" runat="server" Text="Cuenta Usuario:"></asp:Label>
                            </td>
                            <td style="height:30px;">
                                <asp:TextBox  ID="txtUsuario" CssClass="TxtLogin" runat="server" MaxLength="10" 
                                    Width="100px" ></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="textos_Login" style="height:30px;">
                                <asp:Label ID="lblContrasenaUsuario" runat="server" Text="Contraseña:"></asp:Label>
                            </td>
                            <td style="height:30px;">
                                <asp:TextBox ID="txtPassword" CssClass="TxtLogin" runat="server" 
                                    TextMode="Password" MaxLength="10" Width="100px" ></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                        <td style="height:30px;" class="textos_Login">
                            <asp:Label ID="lblContrasenaUsuarioNueva" runat="server" 
                                Text="Contraseña Nueva:" Enabled="False" Visible="False"></asp:Label>
                            </td>
                        <td style="height:30px;">
                            <asp:TextBox ID="tbContrasenaUsuarioNueva" runat="server" class="TxtLogin" 
                                Enabled="False" MaxLength="10" TextMode="Password" Visible="False" 
                                Width="100px"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                        <td style="height:30px;" class="textos_Login">
                            <asp:Label ID="lblConfirmarContrasenaUsuario" runat="server" 
                                Text="Confirmar Contraseña:" Enabled="False" Visible="False"></asp:Label>
                            </td>
                        <td style="height:30px;">
                            <asp:TextBox ID="tbConfirmarContrasenaUsuario" runat="server" class="TxtLogin" 
                                Enabled="False" MaxLength="10" TextMode="Password" Visible="False" 
                                Width="100px"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="textos_Login" style="height:50px;" colspan="2"> 
                                <asp:HiddenField ID="hideEstadoPantalla" runat="server" Value="1" />
                            </td>
                        </tr>
                        <tr>
                            <td align="center" colspan="2" style="height:30px;font-size:10px; font-family:Arial;"> 
                              <!--  <a href ="#">¿Ha olvidado la contraseña?</a>-->
                            </td>
                        </tr>
                        <tr>
                            <td align="center" colspan="2">
                                <asp:Button ID="btnLogin"  CssClass="Boton_01" CommandName="Login" Text="Iniciar sesión" runat="server" OnClick="LoginButton_Click" />
                             <asp:Button ID="btnReset" runat="server" OnClick="btnReset_Click" Width="150"  CssClass="Boton_01" Visible="false" Text="Cambiar contraseña"  />
                             <!--  <asp:Button ID="Button1" runat="server" OnClick="btnReset_Click"  CssClass="Boton_01" Visible="false" Text="Cambiar contraseña" OnClientClick="if(!confirm('¿Quiere cambiar su contraseña?')){return false;};" />-->
                            </td>
                        </tr>
                    </table>      
                </td>
            </tr>
        </table>    
    </div>
    </form>
</body>    
</html>
