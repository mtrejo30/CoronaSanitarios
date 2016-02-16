<%@ Page Language="C#" MasterPageFile="~/ControlPisoLamosa.Master" AutoEventWireup="true"
    Title="Control de piso - Reemplazar código de barras" CodeBehind="ReemplazarCodBarras.aspx.cs"
    Inherits="LAMOSA.SCPP.Client.View.Administrador.Planta.ReemplazarCodBarras" %>

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

    <script src="../FuncionesJS/sliding_effect.js" type="text/javascript"></script>

    <div>
        <asp:ScriptManager ID="sm" runat="server">
        </asp:ScriptManager>
        <table align="center" border="0" cellpadding="0" cellspacing="0" width="1024px">
            <tr>
                <td style="height: 10px" colspan="3">
                </td>
            </tr>
            <tr style="height: 30px;">
                <td style="width: 10px; background-color: #eee;">
                </td>
                <td colspan="3" class="lblTitulo1">
                    <asp:Label ID="lblTitulo" runat="server" Text="Reemplazar código de barras"></asp:Label><br />
                </td>
            </tr>
            <tr>
                <td style="height: 10px" colspan="3">
                </td>
            </tr>
            <tr>
                <td style="width: 10px;" rowspan="2">
                </td>
                <td rowspan="2" valign="top" class="leftarea" style="width: 100px">
                    <div id="navcontainer">
                        <ul id="navlist">
                            <li><a href="javascript:history.back();" onclick="history.go(-1)">
                                <img src="../Imagenes/Regresar.png" alt="Regresar" style="border: 0px;" />
                                Regresar</a></li>
                        </ul>
                    </div>
                </td>
                <td>
                    &nbsp;
                </td>
                <td>
                    <igmisc:WebAsyncRefreshPanel ID="WebAsyncRefreshPanel1" runat="server">
                        <table style="height: 100px; width: 450px">
                            <tbody>
                                <tr>
                                    <td colspan="2">
                                        <table style="width: 550px">
                                            <tr>
                                                <td class="textos_01">
                                                    
                                                    Proceso:
                                                    
                                                </td>
                                                <td class="textos_01">
                                                    
                                                    <asp:DropDownList ID="ddlProceso" runat="server" CssClass="textosd">
                                                    </asp:DropDownList>
                                                    
                                                </td>
                                                <td class="textos_01">
                                                    Tipo de artículo:
                                                </td>
                                                <td class="textos_01">
                                                    <asp:DropDownList ID="ddlTipoArticulo" runat="server" AutoPostBack="true" CssClass="textosd"
                                                        OnSelectedIndexChanged="ddlTipoArticulo_SelectedIndexChanged">
                                                    </asp:DropDownList>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="textos_01">
                                                    &nbsp;</td>
                                                <td class="textos_01">
                                                    &nbsp;</td>
                                                <td class="textos_01">
                                                    Modelo:
                                                </td>
                                                <td class="textos_01">
                                                    <asp:DropDownList ID="ddlModelo" runat="server" CssClass="textosd">
                                                        <asp:ListItem Text="Todos" Value="0"></asp:ListItem>
                                                        <asp:ListItem Text="Venecia" Value="1"></asp:ListItem>
                                                        <asp:ListItem Text="Regency" Value="2"></asp:ListItem>
                                                        <asp:ListItem Text="One Piece" Value="3"></asp:ListItem>
                                                    </asp:DropDownList>
                                                </td>
                                                <td>
                                                    <asp:Button ID="btnBuscar" runat="server" CssClass="Boton_01" OnClick="Button1_Click"
                                                        Text="B&uacute;scar" />
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right">
                                    </td>
                                    <td align="left">
                                        
                                    </td>
                                </tr>
                                <tr>
                                    <td class="textos_01">
                                        Códigos de barras nuevos
                                    </td>
                                    <td class="textos_01">
                                        Códigos de barras detenidos en proceso anterior
                                    </td>
                                </tr>
                                <tr>
                                    <td class="textos_01"></td>
                                    <td class="textos_01">
                                        <input id="Aceptar" type="button" class="Boton_01" onclick="javascript:validateSelection()"
                                            style="width: 152px;" value="Relacionar Codigos" />
                                    </td>
                                </tr>
                                <tr>
                                    <td valign="top">
                                        <div id="navigation-block" style="width: 400px">
                                            <ul id="CodsReemplazo" runat="server" class="sliding">
                                            </ul>
                                        </div>
                                    </td>
                                    <td valign="top">
                                        <div id="navigation-block2" style="width: 400px">
                                            <ul id="CodsDetenidos" runat="server" class="sliding">
                                            </ul>
                                        </div>
                                    </td>
                                </tr>
                            </tbody>
                        </table>

                        <script type="text/javascript">
                            $(function() {
                                slide(".sliding", 25, 15, 150, .8);
                                $('.sliding li a').click(function() {
                                    $(this).parents(".sliding").find('a').each(function() {
                                        var $table = $(this).find('table');
                                        var $tr = $(this).find('TR');
                                        var tdVal = $tr.find('TD').eq(1).html()
                                        if (tdVal) {
                                            var type = $tr.find('TD').eq(3).html();
                                            var model = $tr.find('TD').eq(4).html();
                                            var color = $tr.find('TD').eq(7).html();
                                            var calidad = $tr.find('TD').eq(8).html();
                                            findSuggest(type, model, color, calidad);
                                        }
                                        $(this).removeClass('selected');
                                    });
                                    $(this).addClass('selected');
                                });
                            });
                            function findSuggest(type, model, color, calidad) {
                                var count = 0;
                                var countAux = 0;
                                var $link;
                                $('#<%=CodsDetenidos.ClientID %> li a table tr').each(function() {
                                    $(this).parents("a").removeClass('selected');
                                    var tdType = $(this).find('TD').eq(3).html();
                                    var tdModel = $(this).find('TD').eq(4).html();
                                    var tdColor = $(this).find('TD').eq(7).html();
                                    var tdCalidad = $(this).find('TD').eq(8).html();
                                    if (type == tdType)
                                        countAux++;
                                    if (model == tdModel)
                                        countAux++;
                                    if (color == tdColor)
                                        countAux++;
                                    if (calidad == tdCalidad)
                                        countAux++;
                                    if (countAux > count) {
                                        $link = $(this).parents("a");
                                        count = countAux;
                                    }
                                    countAux = 0;
                                });
                                $link.addClass('selected');
                            }
                            function validateSelection() {
                                var codDetenido = ($('#<%=CodsDetenidos.ClientID %> li a.selected table tr').find('td').eq(0).html());
                                var codReemplazo = ($('#<%=CodsReemplazo.ClientID %> li a.selected table tr').find('td').eq(0).html());
                                var msg = !codReemplazo ? ' Reemplazo' : '';
                                msg += !codDetenido ? (msg ? ' y' : '') + ' Detenidos' : ''
                                if (!codReemplazo || !codDetenido)
                                    alert('Debe seleccionar un codigo :' + msg)
                                else if (confirm('¿Esta seguro que desea realizar este Reemplazo?')) {
                                    $('#<%=HCodReemplazo.ClientID %>').val(codReemplazo)
                                    $('#<%=HCodDetenido.ClientID %>').val(codDetenido);
                                    $('#<%=btnSave.ClientID %>').click();
                                    //alert(codDetenido + '------' + codReemplazo)
                                }
                            }
		                </script>

                        <asp:Button ID="btnSave" runat="server" Text="Button" 
                            onclick="btnSave_Click1" CssClass="hidden" />
                        <asp:HiddenField ID="HCodReemplazo" runat="server" />
                        <asp:HiddenField ID="HCodDetenido" runat="server" />
                    </igmisc:WebAsyncRefreshPanel>
                </td>
            </tr>
            <tr>
                <td colspan="3">
                    <igtblexp:UltraWebGridExcelExporter ID="uwgCentrosTra" WorksheetName="Centros trabajo"
                        DownloadName="Reporte.XLS" runat='server'>
                    </igtblexp:UltraWebGridExcelExporter>
                </td>
            </tr>
        </table>
        <asp:HiddenField ID="UserInSession" runat="server" />
        <asp:HiddenField ID="HddUser" runat="server" />
        <asp:HiddenField ID="Sucursalhdd" runat="server" />
    </div>
</asp:Content>
