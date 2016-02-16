<%@ Page Title="" Language="C#" MasterPageFile="~/ControlPisoLamosa.Master" AutoEventWireup="true" CodeBehind="DSBFiltros.aspx.cs" Inherits="LAMOSA.SCPP.Client.View.Administrador.Dashboard.DSBFiltros" %>
<asp:Content ID="Content1" ContentPlaceHolderID="Principal" runat="server">
    <script language="javascript" type="text/javascript">
        function StartDSB() {
            window.open("DSB.aspx","Dashboard", "fullscreen");
        }
        
    </script>
    <table align="center" width="1024px"  border="0" cellpadding="0" cellspacing="0" style="background-color:white; height:400px;" >
            <tr><td style="height:10px" colspan = "3"></td></tr>
            <tr style=" height:30px;">
                <td style=" width:10px; background-color:#eee;"></td>
                <td colspan= "2" style=" width:1014px;"  class ="lblTitulo1">
                    <asp:Label ID="lblTitulo" runat="server" Text="Dashboard" ></asp:Label><br/>
                </td> 
            </tr>
             <tr><td style="height:10px" colspan = "3"></td></tr>
            <tr>
                <td colspan = "3">
                     <table align="center" width="300px"  border="0" cellpadding="0" cellspacing="0" >
                        <tr>
                            <td class="textos_Login" style="height:30px;"> Almac&eacute;n:</td>
                            <td style="height:30px;">
                                <asp:DropDownList ID="cmbAlmacen" CssClass="CmbLogin" runat="server">
                                    <asp:ListItem Value= "0" Text="Todos"> </asp:ListItem>
                                    <asp:ListItem Value= "3000" Text="Monterrey"> </asp:ListItem>
                                    <asp:ListItem Value= "3200" Text="Benito Ju&aacute;rez"> </asp:ListItem>
                                    </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td class="textos_Login" style="height:30px;"> Planta:</td>
                            <td style="height:30px;">
                                <asp:DropDownList ID="cmbPlanta" CssClass="CmbLogin" runat="server">
                                    <asp:ListItem Value= "0" Text="Todos"> </asp:ListItem>
                                    <asp:ListItem Value= "1" Text="Planta 1"> </asp:ListItem>
                                    <asp:ListItem Value= "2" Text="Planta 2"> </asp:ListItem>
                                    <asp:ListItem Value= "3" Text="Planta 3"> </asp:ListItem>
                                    <asp:ListItem Value= "4" Text="Benito Ju&aacute;rez"> </asp:ListItem>
                                    </asp:DropDownList>
                            </td>
                        </tr>
                       
                        <tr>
                            <td align="center" colspan="2">
                                <input type="button"  id="GeneraButton"  class="Boton_01" onclick="StartDSB()" value="Generar" />
                            </td>
                        </tr>
                    </table>      
                </td>
            </tr>
    </table>
</asp:Content>
