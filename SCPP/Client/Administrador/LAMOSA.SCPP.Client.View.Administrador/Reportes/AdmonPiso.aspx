<%@ Page Language="C#" MasterPageFile="~/ControlPisoLamosa.Master" AutoEventWireup="true"
    CodeBehind="AdmonPiso.aspx.cs" Inherits="LAMOSA.SCPP.Client.View.Administrador.Reportes.AdmonPiso"
    Title="Control de piso - Administracion de piso" %>

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
                    <asp:Label ID="lblTitulo" runat="server" Text="Administración de piso"></asp:Label><br />
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
                    <igmisc:WebAsyncRefreshPanel ID="WebAsyncRefreshPanel1" InitializePanel="WebAsyncRefreshPanel1_InitializePanel"
                        runat="server">
                        <table style="height: 200px; width: 680px">
                            <tbody>
                                <tr>
                                    <td style="height: 40px" class="textos_01">
                                        <table style="width: 795px">
                                            <tr>
                                                <td class="textos_01" style="text-align: right; width: 165px;">
                                                    Planta:
                                                </td>
                                                <td class="textos_01">
                                                    <asp:DropDownList ID="ddlPlanta" runat="server" CssClass="textosd" Width="230" OnSelectedIndexChanged="ddlPlanta_SelectedIndexChanged"
                                                        AutoPostBack="True">
                                                    </asp:DropDownList>
                                                </td>
                                                <td class="textos_01" style="text-align: right">
                                                    Tipo de artículo:
                                                </td>
                                                <td class="textos_01">
                                                    <asp:DropDownList ID="ddlTipoArticulo" runat="server" AutoPostBack="true" CssClass="textosd"
                                                        OnSelectedIndexChanged="ddlTipoArticulo_SelectedIndexChanged" Width="230">
                                                    </asp:DropDownList>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="textos_01" style="text-align: right; width: 165px;">
                                                    Turno:
                                                </td>
                                                <td class="textos_01">
                                                    <asp:DropDownList ID="ddlTurno" runat="server" CssClass="textosd" Width="230">
                                                    </asp:DropDownList>
                                                </td>
                                                <td class="textos_01" style="text-align: right">
                                                    Modelo:
                                                </td>
                                                <td class="textos_01">
                                                    <asp:DropDownList ID="ddlModelo" runat="server" CssClass="textosd" Width="230">
                                                        <asp:ListItem Text="Todos" Value="0"></asp:ListItem>
                                                        <asp:ListItem Text="Venecia" Value="1"></asp:ListItem>
                                                        <asp:ListItem Text="Regency" Value="2"></asp:ListItem>
                                                        <asp:ListItem Text="One Piece" Value="3"></asp:ListItem>
                                                    </asp:DropDownList>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="textos_01" style="text-align: right; width: 165px;">
                                                    Proceso Origen:
                                                </td>
                                                <td class="textos_01">
                                                    <asp:DropDownList ID="ddlProcesoOrigen" runat="server" AutoPostBack="True" 
                                                        CssClass="textosd" onselectedindexchanged="ddlPlanta_SelectedIndexChanged" 
                                                        Width="230">
                                                    </asp:DropDownList>
                                                </td>
                                                <td class="textos_01" style="text-align: right">
                                                    Proceso Destino</td>
                                                <td class="textos_01">
                                                    <asp:DropDownList ID="ddlProcesoDestino" runat="server" 
                                                        CssClass="textosd" Width="230">
                                                    </asp:DropDownList>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="textos_01" style="text-align: right; width: 165px;">
                                                    Centro de trabajo:</td>
                                                <td class="textos_01">
                                                    <asp:DropDownList ID="ddlCentroTrabajo" runat="server" CssClass="textosd" 
                                                        Width="230">
                                                    </asp:DropDownList>
                                                </td>
                                                <td class="textos_01" style="text-align: right">
                                                    &nbsp;</td>
                                                <td class="textos_01">
                                                    &nbsp;</td>
                                            </tr>
                                            <tr>
                                                <td class="textos_01" style="text-align: right; width: 165px;">
                                                    Fecha Inicial:
                                                </td>
                                                <td class="textos_01">
                                                    <igsch:WebDateChooser ID="wdcFechaInicial" runat="server" Value="" Width="230">
                                                        <ClientSideEvents CalendarValueChanged="wdcFechaInicial_CalendarValueChanged" 
                                                            InitializeDateChooser="wdcFechaInicial_InitializeDateChooser" />
                                                    </igsch:WebDateChooser>
                                                </td>
                                                <td class="textos_01" style="text-align: right">
                                                    Fecha Final:
                                                </td>
                                                <td class="textos_01">
                                                    <igsch:WebDateChooser ID="wdcFechaFinal" runat="server" Value="" Width="230px">
                                                        <ClientSideEvents CalendarValueChanged="wdcFechaFinal_CalendarValueChanged"
                                                            InitializeDateChooser="wdcFechaFinal_InitializeDateChooser" />
                                                    </igsch:WebDateChooser>
                                                </td>
                                            </tr>
                                            <tr style="display: none">
                                                <td class="textos_01" style="width: 165px">
                                                </td>
                                                <td class="textos_01">
                                                    Operadores:
                                                </td>
                                                <td style="display: none" class="textos_01">
                                                    <asp:Button ID="igtbl_reSelecciona" CssClass="Boton_01" runat="server" Text="Seleccionar" />
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="center" style="height: 26px" class="textos_01">
                                        <asp:Button ID="igtbl_reBuscaBtn" CssClass="Boton_01" runat="server" Text="Buscar"
                                            OnClick="bBuscar_Click" />
                                    </td>
                                </tr>
                                <tr>
                                    <td>

                                        <script src="../FuncionesJS/jquery-1.4.2.js" type="text/javascript"></script>

                                        <script type="text/javascript">
                                            var beforeClose = true;
                                            var clickcancel = true;
                                            function ok(event) {
                                                var proceso = $("#id_proceso").val();
                                                var Claveunica = $("#ClaveTurno").val();
                                                var Descripcion = $("#Descripcion").val();
                                                var Clavedefecto = $("#clavetipo").val();
                                                var Responsable = $("resp").val();
                                                if (Descripcion != "" && proceso != "" && Clavedefecto != "" && Responsable != "") {
                                                    if (confirm('¿Desea guardar cambios?')) {

                                                    }
                                                }
                                                else alert('Informacion incompleta para poder guardar el registro!');
                                            }

                                            function cancel(event) {
                                                var proceso = $("#id_proceso").val();
                                                var Claveunica = $("#ClaveTurno").val();
                                                var Descripcion = $("#Descripcion").val();
                                                var Clavedefecto = $("#clavetipo").val();
                                                var Responsable = $("resp").val();

                                                var oProceso = $("#oProceso").val();
                                                var oclaveunica = $("#oclaveunica").val();
                                                var oClavedefecto = $("#oClavedefecto").val();
                                                var oDescripcion = $("#oDescripcion").val();
                                                var oresponsable = $("#oresponsable").val();

                                                var edit = true;
                                                beforeClose = false;
                                                if (Claveunica != oclaveunica || Descripcion != oDescripcion || proceso != oProceso || Clavedefecto != oClavedefecto || Responsable != oresponsable) {
                                                    if (!confirm('¿Está seguro de cerrar pantalla sin hacer cambios?')) {
                                                    }
                                                }
                                                if (edit) {
                                                    clickcancel = false;
                                                    igtbl_gRowEditButtonClick(event);
                                                    clickcancel = true;
                                                    beforeClose = true;
                                                }
                                            }
                                            function eliminar(event) {
                                                var proceso = $("#id_proceso").val();
                                                var Claveunica = $("#ClaveTurno").val();
                                                var Descripcion = $("#Descripcion").val();
                                                var Clavedefecto = $("#clavetipo").val();
                                                var Responsable = $("resp").val();
                                                if (confirm('¿Desea eliminar registro?')) {
                                                    clickcancel = false;
                                                    igtbl_gRowEditButtonClick(event);
                                                    clickcancel = false;
                                                    beforeClose = false;
                                                }
                                                else {
                                                    clickcancel = false;
                                                    igtbl_gRowEditButtonClick(event);
                                                    clickcancel = true;
                                                    beforeClose = false;
                                                }
                                            }

                                            function BeforeRowTemplateCloseHandler(event) {
                                                if (clickcancel) {
                                                    $("#btnCancelar").click();
                                                }
                                                return beforeClose;
                                                if (clickok) {
                                                    $("#btnGuardar").click();
                                                }
                                                return beforeClose;
                                                if (clickeliminar) {
                                                    $("#btnEliminar").click();
                                                }
                                                return beforeClose;
                                            }

                                            var odate1 = null;
                                            var odate2 = null;

                                            function wdcFechaInicial_CalendarValueChanged(oCalendar, oDate, oEvent) {

                                                var d1 = oDate;
                                                var d2 = odate2.getValue();
                                                if (d2 != null) {
                                                    if (d1 > d2) {
                                                        oEvent.cancel = true;
                                                        alert("Fecha incorrecta, debe seleccionar una fecha menor o igual a la fecha final");
                                                    }
                                                }
                                            }

                                            function wdcFechaFinal_CalendarValueChanged(oCalendar, oDate, oEvent) {
                                                var d1 = odate1.getValue();
                                                var d2 = oDate;
                                                if (d2 < d1) {
                                                    oEvent.cancel = false;
                                                    alert("Fecha incorrecta, debe seleccionar una fecha mayor o igual a la fecha inicial");
                                                }
                                            }

                                            function wdcFechaInicial_InitializeDateChooser(oDateChooser) {
                                                odate1 = oDateChooser;
                                            }

                                            function wdcFechaFinal_InitializeDateChooser(oDateChooser) {
                                                odate2 = oDateChooser;
                                            }
                                        </script>

                                        <igtbl:UltraWebGrid ID="UltraWebGrid1" runat="server" CaptionAlign="Left" EnableAppStyling="False"
                                            OnInitializeLayout="UltraWebGrid1_InitializeLayout" Height="400px" Width="800px">
                                            <Bands>
                                                <igtbl:UltraGridBand>
                                                    <AddNewRow View="NotSet" Visible="NotSet">
                                                    </AddNewRow>
                                                    <Columns>
                                                        <igtbl:UltraGridColumn>
                                                            <Header Caption="ClaveModelo">
                                                            </Header>
                                                        </igtbl:UltraGridColumn>
                                                        <igtbl:UltraGridColumn>
                                                            <Header Caption="Clas de Art">
                                                                <RowLayoutColumnInfo OriginX="1" />
                                                            </Header>
                                                            <Footer>
                                                                <RowLayoutColumnInfo OriginX="1" />
                                                            </Footer>
                                                        </igtbl:UltraGridColumn>
                                                        <igtbl:UltraGridColumn>
                                                            <Header Caption="# Operador">
                                                                <RowLayoutColumnInfo OriginX="2" />
                                                            </Header>
                                                            <Footer>
                                                                <RowLayoutColumnInfo OriginX="2" />
                                                            </Footer>
                                                        </igtbl:UltraGridColumn>
                                                        <igtbl:UltraGridColumn>
                                                            <Header Caption="Objetivo">
                                                                <RowLayoutColumnInfo OriginX="3" />
                                                            </Header>
                                                            <Footer>
                                                                <RowLayoutColumnInfo OriginX="3" />
                                                            </Footer>
                                                        </igtbl:UltraGridColumn>
                                                        <igtbl:UltraGridColumn>
                                                            <Header Caption="# Pzas. Procesadas">
                                                                <RowLayoutColumnInfo OriginX="4" />
                                                            </Header>
                                                            <Footer>
                                                                <RowLayoutColumnInfo OriginX="4" />
                                                            </Footer>
                                                        </igtbl:UltraGridColumn>
                                                        <igtbl:UltraGridColumn>
                                                            <Header Caption="Malas del día">
                                                                <RowLayoutColumnInfo OriginX="5" />
                                                            </Header>
                                                            <Footer>
                                                                <RowLayoutColumnInfo OriginX="5" />
                                                            </Footer>
                                                        </igtbl:UltraGridColumn>
                                                        <igtbl:UltraGridColumn>
                                                            <Header Caption="% Malas Acum">
                                                                <RowLayoutColumnInfo OriginX="6" />
                                                            </Header>
                                                            <Footer>
                                                                <RowLayoutColumnInfo OriginX="6" />
                                                            </Footer>
                                                        </igtbl:UltraGridColumn>
                                                        <igtbl:UltraGridColumn>
                                                            <Header Caption="Malas Proceso 2">
                                                                <RowLayoutColumnInfo OriginX="7" />
                                                            </Header>
                                                            <Footer>
                                                                <RowLayoutColumnInfo OriginX="7" />
                                                            </Footer>
                                                        </igtbl:UltraGridColumn>
                                                        <igtbl:UltraGridColumn>
                                                            <Header Caption="% Malas Proceso 2">
                                                                <RowLayoutColumnInfo OriginX="8" />
                                                            </Header>
                                                            <Footer>
                                                                <RowLayoutColumnInfo OriginX="8" />
                                                            </Footer>
                                                        </igtbl:UltraGridColumn>
                                                        <igtbl:UltraGridColumn>
                                                            <Header Caption="% Malas Acum. Proc 2">
                                                                <RowLayoutColumnInfo OriginX="9" />
                                                            </Header>
                                                            <Footer>
                                                                <RowLayoutColumnInfo OriginX="9" />
                                                            </Footer>
                                                        </igtbl:UltraGridColumn>
                                                        <igtbl:UltraGridColumn>
                                                            <Header Caption="% Malas Totales">
                                                                <RowLayoutColumnInfo OriginX="10" />
                                                            </Header>
                                                            <Footer>
                                                                <RowLayoutColumnInfo OriginX="10" />
                                                            </Footer>
                                                        </igtbl:UltraGridColumn>
                                                        <igtbl:UltraGridColumn>
                                                            <Header Caption="Inventario">
                                                                <RowLayoutColumnInfo OriginX="11" />
                                                            </Header>
                                                            <Footer>
                                                                <RowLayoutColumnInfo OriginX="11" />
                                                            </Footer>
                                                        </igtbl:UltraGridColumn>
                                                        <igtbl:UltraGridColumn>
                                                            <Header Caption="Entrega al Sig Proc.">
                                                                <RowLayoutColumnInfo OriginX="12" />
                                                            </Header>
                                                            <Footer>
                                                                <RowLayoutColumnInfo OriginX="12" />
                                                            </Footer>
                                                        </igtbl:UltraGridColumn>
                                                    </Columns>
                                                    <RowTemplateStyle BackColor="White" BorderColor="White" BorderStyle="Ridge">
                                                        <BorderDetails WidthBottom="3px" WidthLeft="3px" WidthRight="3px" WidthTop="3px" />
                                                    </RowTemplateStyle>
                                                </igtbl:UltraGridBand>
                                            </Bands>
                                            <DisplayLayout Name="UltraWebGrid1" AllowDeleteDefault="Yes" NoDataMessage="" RowHeightDefault="20px"
                                                SelectTypeRowDefault="Single" Version="3.00" CellClickActionDefault="RowSelect"
                                                LoadOnDemand="Xml" AllowAddNewDefault="Yes" AllowColSizingDefault="Free" CellPaddingDefault="1"
                                                HeaderClickActionDefault="SortSingle" RowSelectorsDefault="No" RowSizingDefault="Free"
                                                StationaryMargins="Header" HeaderStyleDefault-Height="30px">
                                                <RowAlternateStyleDefault BackColor="#E5E5E5">
                                                </RowAlternateStyleDefault>
                                                <FrameStyle Width="800px">
                                                </FrameStyle>
                                                <ClientSideEvents BeforeRowTemplateCloseHandler="BeforeRowTemplateCloseHandler" />
                                                <Pager AllowPaging="False">
                                                    <PagerStyle BackColor="#666666" BorderColor="Black" BorderStyle="Solid" BorderWidth="1px"
                                                        Font-Names="Arial" Font-Size="11px" ForeColor="White" Height="20px" />
                                                </Pager>
                                                <EditCellStyleDefault BackColor="Silver">
                                                </EditCellStyleDefault>
                                                <FooterStyleDefault BackColor="LightGray" BorderStyle="Solid" BorderWidth="1px">
                                                </FooterStyleDefault>
                                                <HeaderStyleDefault BackColor="#666666" BorderColor="Black" BorderStyle="Solid" Font-Bold="True"
                                                    ForeColor="White">
                                                </HeaderStyleDefault>
                                                <RowSelectorStyleDefault BorderStyle="Solid">
                                                </RowSelectorStyleDefault>
                                                <RowStyleDefault BackColor="White" BorderColor="Black" BorderStyle="Solid" BorderWidth="1px"
                                                    Font-Names="Verdana" Font-Size="8pt" ForeColor="Black">
                                                </RowStyleDefault>
                                                <SelectedRowStyleDefault BackColor="#FFFFB3" Font-Bold="True" ForeColor="Black">
                                                </SelectedRowStyleDefault>
                                                <ActivationObject BorderColor="Black" BorderStyle="Dotted" BorderWidth="">
                                                </ActivationObject>
                                                <AddNewRowDefault View="Top">
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
                    &nbsp;<script type="text/javascript">
                              function onMouseOut(listbar, object, e) {
                              }
                              function onMouseOver(listbar, object, e) {
                                  if (object == listbar.Groups[0].Items[0])
                                      ig.getElementById(listbar.Id).style.filter = "Shadow(Color=#999999,Direction=45);";
                                  else if (object == listbar.Groups[0].Items[1])
                                      ig.getElementById(listbar.Id).style.filter = "Shadow(Color=#3366cc,Direction=45);";
                                  else if (object == listbar.Groups[0].Items[2])
                                      ig.getElementById(listbar.Id).style.filter = "Shadow(Color=#99cc99,Direction=45);";
                                  else if (object == listbar.Groups[0].Items[3])
                                      ig.getElementById(listbar.Id).style.filter = "Shadow(Color=#ccff66,Direction=45);";
                                  else if (object == listbar.Groups[1].Items[0])
                                      ig.getElementById(listbar.Id).style.filter = "Shadow(Color=#ff6666,Direction=45);";
                                  else if (object == listbar.Groups[1].Items[1])
                                      ig.getElementById(listbar.Id).style.filter = "Shadow(Color=#cc9966,Direction=45);";

                              }
                              function ListItemSelected(idList, varList) {
                                  //var nuevo = document.getElementById("<%=hddSecurityConstants.ClientID %>").getAttribute('value').indexOf('1');
                                  //   var exporta = document.getElementById("<%=hddSecurityConstants.ClientID %>").getAttribute('value').indexOf('6');

                                  if (idList == 2) {//Exportar
                                      $find('<%=WebDialogWindow1.ClientID%>').set_windowState($IG.DialogWindowState.Normal);

                                  }
                              }
                              function WebAsyncRefreshPanel1_InitializePanel(oPanel) {
                                  ig_shared.getCBManager()._timeLimit = 300000; // Modifica el TimeOut de todos los WARP's 
                              }
                    </script>

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
                                                <input id="nombre" value="Reporte de Piso" style="width: 200px;" type="text" runat="server" />
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
                    <igtblexp:UltraWebGridExcelExporter ID="uwgAdmonPiso" WorksheetName="AdministracionPiso"
                        DownloadName="Reporte.XLS" runat='server'>
                    </igtblexp:UltraWebGridExcelExporter>
                </td>
            </tr>
        </table>
        <ig:WebDialogWindow ID="WebDialogWindow2" runat="server" InitialLocation="Centered"
            Height="300px" Width="300px" Modal="true" WindowState="Hidden" Font-Size="10px">
            <ContentPane BackColor="#FAFAFA">
                <Template>
                    <div>
                        <table border="0" align="center">
                            <tbody>
                                <tr>
                                    <td colspan="2" valign="top" class="textos_01">
                                        Selección de operadores:
                                    </td>
                                </tr>
                                <tr>
                                    <td class="textos_01">
                                        Puesto:
                                    </td>
                                    <td class="textos_01">
                                        <asp:DropDownList ID="cmbPuesto" runat="server" CssClass="" Height="22px" Width="150px">
                                            <asp:ListItem Value="1" Text="Vaciador"></asp:ListItem>
                                            <asp:ListItem Value="2" Text="Esmaltador"></asp:ListItem>
                                            <asp:ListItem Value="3" Text="Clasificador"></asp:ListItem>
                                            <asp:ListItem Value="4" Text="Revisador"></asp:ListItem>
                                            <asp:ListItem Value="5" Text="Cargador horno"></asp:ListItem>
                                            <asp:ListItem Value="6" Text="Auditor"></asp:ListItem>
                                            <asp:ListItem Value="7" Text="Encargado de secado"></asp:ListItem>
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="textos_01">
                                        Operadores:
                                    </td>
                                    <td class="textos_01">
                                        <asp:ListBox ID="id_operador" runat="server" Width="150px">
                                            <asp:ListItem Value="1" Text="Juan Díaz"></asp:ListItem>
                                            <asp:ListItem Value="2" Text="Roberto Perez"></asp:ListItem>
                                            <asp:ListItem Value="3" Text="Alejandro Dimas"></asp:ListItem>
                                        </asp:ListBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2" align="center">
                                        <asp:Button ID="Button2" runat="server" CssClass="Boton_01" Text="Aceptar" Width="115" />
                                    </td>
                                </tr>
                            </tbody>
                        </table>
                    </div>
                </Template>
            </ContentPane>
        </ig:WebDialogWindow>
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
