<%@ Page Language="C#" MasterPageFile="~/ControlPisoLamosa.Master" AutoEventWireup="true"
    CodeBehind="PantallaInicial.aspx.cs" Inherits="LAMOSA.SCPP.Client.View.Administrador.Clasificacion.PantallaInicial"
    Title="Control de piso - Proceso de clasificación" %>

<%@ Register Assembly="Infragistics35.WebUI.Misc.v9.1, Version=9.1.20091.1015, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb"
    Namespace="Infragistics.WebUI.Misc" TagPrefix="igmisc" %>
<%@ Register Assembly="Infragistics35.WebUI.WebDateChooser.v9.1, Version=9.1.20091.1015, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb"
    Namespace="Infragistics.WebUI.WebSchedule" TagPrefix="igsch" %>
<%@ Register Assembly="Infragistics35.Web.v9.1, Version=9.1.20091.1015, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb"
    Namespace="Infragistics.Web.UI.LayoutControls" TagPrefix="ig" %>
<%@ Register Assembly="Infragistics35.WebUI.WebDataInput.v9.1, Version=9.1.20091.1015, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb"
    Namespace="Infragistics.WebUI.WebDataInput" TagPrefix="igtxt" %>
<%@ Register Assembly="Infragistics35.WebUI.UltraWebGrid.ExcelExport.v9.1, Version=9.1.20091.1015, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb"
    Namespace="Infragistics.WebUI.UltraWebGrid.ExcelExport" TagPrefix="igtblexp" %>
<%@ Register Assembly="Infragistics35.WebUI.UltraWebGrid.v9.1, Version=9.1.20091.1015, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb"
    Namespace="Infragistics.WebUI.UltraWebGrid" TagPrefix="igtbl" %>
<%@ Register Assembly="Infragistics35.WebUI.Shared.v9.1, Version=9.1.20091.1015, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb"
    Namespace="Infragistics.WebUI.Shared" TagPrefix="ish" %>
<asp:Content ID="Content1" ContentPlaceHolderID="Principal" runat="server">

    <script src="../FuncionesJS/jquery-1.4.2.js" type="text/javascript"></script>

    <script src="../FuncionesJS/htmlTabControl.js" type="text/javascript"></script>

    <div>
        <asp:ScriptManager ID="sm" runat="server">
            <Scripts>
                <asp:ScriptReference Path="CallWebServiceMethods.js" />
            </Scripts>
            <Services>
                <asp:ServiceReference Path="WebService.asmx" />
            </Services>
        </asp:ScriptManager>
        <table align="center" border="0" cellpadding="0" cellspacing="0" width="1024px">
            <tr>
                <td style="height: 10px" colspan="4">
                </td>
            </tr>
            <tr style="height: 30px;">
                <td style="width: 10px; background-color: #eee;">
                </td>
                <td colspan="6" class="lblTitulo1">
                    <asp:Label ID="lblTitulo" runat="server" Text="Proceso de clasificación"></asp:Label><br />
                </td>
            </tr>
            <tr>
                <td style="height: 10px" colspan="4">
                </td>
            </tr>
            <tr>
                <td style="width: 10px;">
                </td>
                <td valign="top" class="leftarea" style="width: 100px">
                    <div id="navcontainer">
                        <ul id="navlist">
                            <li><a href="javascript:history.back();" onclick="history.go(-1)">
                                <img src="../Imagenes/Regresar.png" alt="Regresar" style="border: 0px;" />
                                Regresar</a></li>
                        </ul>
                    </div>
                </td>
                <td style="width: 20px;">
                    &nbsp;
                </td>
                <td>
                    <igmisc:WebAsyncRefreshPanel ID="WebAsyncRefreshPanel1" runat="server">
                        <script type="text/javascript" >
                            $(function() {
                                var i = 0;
                                var cont = '';
                                while (i <= 10) {
                                    cont += (i++) + ',';
                                }
                                $('#aw').html(cont);
                            })
                        </script>
                        <table style="height: 100px; width: 300px">
                            <tbody>
                                <tr>
                                    <asp:Label ID="Label1" runat="server" Text="Label"></asp:Label>
                                    <td style="height: 40px" class="textos_01">
                                        <table style="width: 300px">
                                            <tr>
                                                <td class="textos_01" id="aw">
                                                    SP a consultar:
                                                </td>
                                                <td class="textos_01">
                                                    <asp:DropDownList ID="DropDownList1" runat="server" DataSourceID="SqlDataSourceSucursal"
                                                        DataTextField="name" DataValueField="name">
                                                    </asp:DropDownList>
                                                    <asp:SqlDataSource ID="SqlDataSourceSucursal" runat="server" ConnectionString="<%$ ConnectionStrings:lamosaConnectionString %>"
                                                        SelectCommand="select REPLACE(REPLACE(name,'SEL',''),'HHSYNC','')name from sys.all_objects where UPPER(name) like 'HHSYNC%' and UPPER(name) like '%SEL'">
                                                    </asp:SqlDataSource>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="center" style="height: 26px" class="textos_01">
                                        <asp:Button ID="Button1" CssClass="Boton_01" runat="server" Text="Button" />
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:GridView ID="UltraWebGrid1" runat="server">
                                        </asp:GridView>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                    <asp:GridView ID="UltraWebGrid2" runat="server">
                                        </asp:GridView>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                    <asp:GridView ID="UltraWebGrid3" runat="server">
                                        </asp:GridView>
                                    </td>
                                </tr>
                            </tbody>
                        </table>
                    </igmisc:WebAsyncRefreshPanel>
                </td>
            </tr>
        </table>
    </div>
</asp:Content>
