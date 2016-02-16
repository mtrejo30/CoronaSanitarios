<%@ Page Language="C#" MasterPageFile="~/ControlPisoLamosa.Master" AutoEventWireup="true"
    CodeBehind="AdmonPlanta.aspx.cs" Inherits="LAMOSA.SCPP.Client.View.Administrador.Reportes.AdmonPlanta"
    Title="Contol de piso - Administracion de planta" %>

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
    <div>
        <asp:ScriptManager ID="sm" runat="server">
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
                    <asp:Label ID="lblTitulo" runat="server" Text="Administración de plantas"></asp:Label><br />
                </td>
            </tr>
            <tr>
                <td style="height: 10px" colspan="4">
                </td>
            </tr>
            <tr>
                <td style="width: 10px;" rowspan="2">
                </td>
                <td rowspan="2" valign="top" class="leftarea" style="width: 100px">
                    <div id="navcontainer">
                        <ul id="navlist">
                            <li><a href="javascript:ListItemSelected(2,'')" id="LExport" runat="server">
                                <img src="../Imagenes/Exportar.png" alt="Exportar tabla" style="border: 0px;" />
                                Exportar tabla</a></li>
                            <li><a href="javascript:history.back();" onclick="history.go(-1)">
                                <img src="../Imagenes/Regresar.png" alt="Regresar" style="border: 0px;" />
                                Regresar</a></li>
                        </ul>
                    </div>
                </td>
                <td style="width: 20px;" rowspan="2">
                    &nbsp;
                </td>
                <td>
                    &nbsp;<script type="text/javascript">
                              function ListItemSelected(idList, varList) {
                                  //var nuevo = document.getElementById("<%=hddSecurityConstants.ClientID %>").getAttribute('value').indexOf('1');
                                  //   var exporta = document.getElementById("<%=hddSecurityConstants.ClientID %>").getAttribute('value').indexOf('6');
                                  if (idList == 2) {//Exportar
                                      $find('<%=WebDialogWindow1.ClientID%>').set_windowState($IG.DialogWindowState.Normal);

                                  }
                              }
                              function WebAsyncRefreshPanel1_InitializePanel(oPanel) {
                                  ig_shared.getCBManager()._timeLimit = 40000;
                              }
                    </script>

                    <igmisc:WebAsyncRefreshPanel ID="WebAsyncRefreshPanel1" InitializePanel="WebAsyncRefreshPanel1_InitializePanel"
                        runat="server">
                        <table style="height: 100px; width: 550px">
                            <tbody>
                                <tr>
                                    <td style="height: 40px" class="textos_01">
                                        <table style="width: 550px">
                                            <tr>
                                                <td class="textos_01" style="text-align: right">
                                                    Planta:
                                                </td>
                                                <td class="textos_01">
                                                    <asp:DropDownList ID="ddlPlanta" runat="server" CssClass="textosd" OnSelectedIndexChanged="ddlPlanta_SelectedIndexChanged"
                                                        >
                                                    </asp:DropDownList>
                                                </td>
                                                <td class="textos_01" style="text-align: right">
                                                    Tipo de artículo:
                                                </td>
                                                <td class="textos_01">
                                                    <asp:DropDownList ID="ddlTipoArticulo" runat="server" AutoPostBack="true" CssClass="textosd"
                                                        OnSelectedIndexChanged="ddlTipoArticulo_SelectedIndexChanged">
                                                    </asp:DropDownList>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="textos_01" style="text-align: right">
                                                    Modelos:
                                                </td>
                                                <td class="textos_01">
                                                    <asp:DropDownList ID="ddlModelo" runat="server" CssClass="textosd">
                                                        <asp:ListItem Text="Todos" Value="0"></asp:ListItem>
                                                        <asp:ListItem Text="Venecia" Value="1"></asp:ListItem>
                                                        <asp:ListItem Text="Regency" Value="2"></asp:ListItem>
                                                        <asp:ListItem Text="One Piece" Value="3"></asp:ListItem>
                                                    </asp:DropDownList>
                                                </td>
                                                <td class="textos_01" style="text-align: right; display: none;">
                                                    &nbsp;Centro de trabajo</td>
                                                <td class="textos_01">
                                                    <asp:DropDownList ID="ddlCentroTrabajo" runat="server" Height="22px" 
                                                        Visible="False" Width="156px">
                                                    </asp:DropDownList>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="textos_01" style="text-align: right">
                                                    Fecha Inicial
                                                </td>
                                                <td class="textos_01">
                                                    <igsch:WebDateChooser ID="wdcFechaIni" runat="server" Width="156">
                                                    </igsch:WebDateChooser>
                                                </td>
                                                <td class="textos_01" style="text-align: right">
                                                    &nbsp; Fecha Final
                                                </td>
                                                <td class="textos_01">
                                                    <igsch:WebDateChooser ID="wdcFechaFin" runat="server" Width="156">
                                                    </igsch:WebDateChooser>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="center" style="height: 26px" class="textos">
                                        <asp:Button ID="bBuscar" runat="server" CssClass="Boton_01" Text="Buscar" OnClick="bBuscar_Click" />
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <igtbl:UltraWebGrid ID="UltraWebGrid1" runat="server" CaptionAlign="Left" EnableAppStyling="False"
                                            OnInitializeLayout="UltraWebGrid1_InitializeLayout">
                                            <Bands>
                                            </Bands>
                                            <DisplayLayout Name="UltraWebGrid1" AllowDeleteDefault="Yes" NoDataMessage="" RowHeightDefault="20px"
                                                SelectTypeRowDefault="Single" Version="3.00" CellClickActionDefault="RowSelect"
                                                LoadOnDemand="Xml" AllowAddNewDefault="Yes" AllowColSizingDefault="Free" AllowSortingDefault="No"
                                                CellPaddingDefault="1" HeaderClickActionDefault="SortSingle" RowSelectorsDefault="No"
                                                RowSizingDefault="Free" StationaryMargins="Header">
                                                <FrameStyle Width="800px">
                                                </FrameStyle>
                                                <RowAlternateStyleDefault BackColor="#E5E5E5">
                                                </RowAlternateStyleDefault>
                                                <Pager AllowPaging="False">
                                                    <PagerStyle Font-Size="11px" Font-Names="Arial" BackColor="#666666" ForeColor="White"
                                                        Height="20px" BorderStyle="Solid" BorderColor="Black" BorderWidth="1px" />
                                                </Pager>
                                                <EditCellStyleDefault BackColor="silver">
                                                </EditCellStyleDefault>
                                                <FooterStyleDefault BackColor="LightGray" BorderStyle="Solid" BorderWidth="1px">
                                                </FooterStyleDefault>
                                                <HeaderStyleDefault BackColor="#666666" BorderColor="Black" BorderStyle="Solid" Font-Bold="True"
                                                    ForeColor="White" Wrap="true">
                                                </HeaderStyleDefault>
                                                <RowSelectorStyleDefault BorderStyle="Solid">
                                                </RowSelectorStyleDefault>
                                                <RowStyleDefault BackColor="White" BorderColor="Black" BorderStyle="Solid" BorderWidth="1px"
                                                    Font-Names="Verdana" Font-Size="8pt" ForeColor="Black">
                                                </RowStyleDefault>
                                                <SelectedRowStyleDefault BackColor="#FFFFB3" ForeColor="Black" Font-Bold="True">
                                                </SelectedRowStyleDefault>
                                                <AddNewBox Hidden="true">
                                                </AddNewBox>
                                                <ActivationObject BorderColor="Black" BorderWidth="" BorderStyle="Dotted">
                                                </ActivationObject>
                                                <AddNewRowDefault View="Top" Visible="No">
                                                </AddNewRowDefault>
                                                <FilterOptionsDefault>
                                                    <FilterDropDownStyle BackColor="White" BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px"
                                                        CustomRules="overflow:auto;" Font-Names="Verdana,Arial,Helvetica,sans-serif"
                                                        Font-Size="11px" Width="200px">
                                                    </FilterDropDownStyle>
                                                    <FilterHighlightRowStyle BackColor="#999999" ForeColor="White">
                                                    </FilterHighlightRowStyle>
                                                </FilterOptionsDefault>
                                            </DisplayLayout>
                                        </igtbl:UltraWebGrid>
                                    </td>
                                </tr>
                            </tbody>
                        </table>
                    </igmisc:WebAsyncRefreshPanel>
                    <ig:WebDialogWindow ID="WebDialogWindow1" runat="server" InitialLocation="Centered"
                        Height="100px" Width="400px" Modal="true" WindowState="Hidden" Font-Size="10px">
                        <ContentPane BackColor="#FAFAFA">
                            <Template>
                                <div style="padding: 5px;">
                                    <table cellpadding="0" cellspacing="0" align="center" style="text-align: center;
                                        width: 100%">
                                        <tr>
                                            <td class="textos_01">
                                                Nombre del archivo:
                                            </td>
                                            <td class="textos_01">
                                                <input id="nombre" value="Reporte de Planta" style="width: 200px;" type="text" runat="server" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="width: 200px" align="center" colspan="2">
                                                <asp:Button ID="btnExporta" runat="server" CssClass="Boton_01" OnClick="btnExporta_Click"
                                                    Text="Exportar" Width="115px" />
                                            </td>
                                        </tr>
                                    </table>
                                </div>
                            </Template>
                        </ContentPane>
                    </ig:WebDialogWindow>
                </td>
            </tr>
            <tr>
                <td colspan="3">
                    <igtblexp:UltraWebGridExcelExporter ID="uwgAdmonP" WorksheetName="AdmonPlantas" DownloadName="Reporte.XLS"
                        runat='server'>
                    </igtblexp:UltraWebGridExcelExporter>
                </td>
            </tr>
        </table>
        <asp:HiddenField ID="UserInSession" runat="server" />
        <asp:HiddenField ID="HddUser" runat="server" />
        <asp:HiddenField ID="Sucursalhdd" runat="server" />
        <asp:UpdatePanel runat="server" ID="UpdatePanel2" ChildrenAsTriggers="True" UpdateMode="Always"
            RenderMode="Inline">
            <ContentTemplate>
                <asp:HiddenField ID="HiddenField1" runat="server" />
                <asp:HiddenField ID="hddSecurityConstants" runat="server" />
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
</asp:Content>
