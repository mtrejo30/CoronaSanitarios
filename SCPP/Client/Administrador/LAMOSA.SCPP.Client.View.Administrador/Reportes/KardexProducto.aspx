<%@ Page Language="C#" MasterPageFile="~/ControlPisoLamosa.Master" AutoEventWireup="true"
    CodeBehind="KardexProducto.aspx.cs" Inherits="LAMOSA.SCPP.Client.View.Administrador.Reportes.KardexProducto"
    Title="Control de piso - Kardex del producto" %>

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
                <td style="height: 10px" colspan="3">
                </td>
            </tr>
            <tr style="height: 30px;">
                <td style="width: 10px; background-color: #eee;">
                </td>
                <td colspan="6" class="lblTitulo1">
                    <asp:Label ID="lblTitulo" runat="server" Text="Kardex del producto"></asp:Label><br />
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
                    <igmisc:WebAsyncRefreshPanel ID="WebAsyncRefreshPanel1" runat="server">
                        <table style="height: 192px; width: 550px">
                            <tbody>
                                <tr>
                                    <td style="height: 26px" class="textos_01">
                                        <table style="width: 550px">
                                            <tr>
                                                <td class="textos_01">
                                                    Código:
                                                </td>
                                                <td class="textos_01">
                                                    <asp:TextBox ID="TxtCode" runat="server" Enabled="true" CssClass="textost"></asp:TextBox>
                                                </td>
                                                <td class="textos_01">
                                                    <asp:Button ID="igtbl_reBuscaBtn0" runat="server" CssClass="Boton_01" OnClick="BuscarCodigo"
                                                        Text="Buscar" Style="width: 75px" />
                                                </td>
                                                <td class="textos_01">
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="textos_01">
                                                    Planta:
                                                </td>
                                                <td class="textos_01">
                                                    <asp:TextBox ID="TxtPlanta1" runat="server" Enabled="False" CssClass="textost" columnkey="DesPlanta"></asp:TextBox>
                                                </td>
                                                <td class="textos_01">
                                                    Color:
                                                </td>
                                                <td class="textos_01">
                                                    <asp:TextBox ID="TxtColor" runat="server" Enabled="False" CssClass="textost" columnkey="Color"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="textos_01">
                                                    Tipo de artículo:
                                                </td>
                                                <td class="textos_01">
                                                    <asp:TextBox ID="TxtTipoArt" runat="server" Enabled="False" CssClass="textost" columnkey="DesTipoArticulo"></asp:TextBox>
                                                </td>
                                                <td class="textos_01">
                                                    Calidad:
                                                </td>
                                                <td class="textos_01">
                                                    <asp:TextBox ID="TxtCalidad" runat="server" Enabled="False" CssClass="textost" columnkey="Calidad"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="textos_01">
                                                    Modelo:
                                                </td>
                                                <td class="textos_01">
                                                    <asp:TextBox ID="TxtModelo" runat="server" Enabled="False" CssClass="textost" columnkey="DesArticulo"></asp:TextBox>
                                                </td>
                                                <td class="textos_01">
                                                    <asp:TextBox ID="TextCodPieza" runat="server" Visible="false" Enabled="False" CssClass="textost"
                                                        columnkey="DesArticulo"></asp:TextBox>
                                                   
                                                </td>
                                                <td class="textos_01">
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="textos_01">
                                        <igtbl:UltraWebGrid ID="UltraWebGrid1" runat="server" CaptionAlign="Left" EnableAppStyling="False">
                                            <Bands>
                                                <igtbl:UltraGridBand>
                                                    <Columns>
                                                        <igtbl:UltraGridColumn Width="124px" BaseColumnName="Fecha" IsBound="true" Key="Fecha"
                                                            CellMultiline="No">
                                                            <Header Caption="Fecha">
                                                                <RowLayoutColumnInfo OriginX="0" />
                                                            </Header>
                                                            <HeaderStyle HorizontalAlign="Center" />
                                                            <Footer>
                                                                <RowLayoutColumnInfo OriginX="0" />
                                                            </Footer>
                                                        </igtbl:UltraGridColumn>
                                                        <igtbl:UltraGridColumn Hidden="true" Width="124px" BaseColumnName="CodProceso" IsBound="true"
                                                            Key="CodProceso" CellMultiline="No">
                                                            <Header Caption="CodProceso">
                                                                <RowLayoutColumnInfo OriginX="1" />
                                                            </Header>
                                                            <HeaderStyle HorizontalAlign="Center" />
                                                            <Footer>
                                                                <RowLayoutColumnInfo OriginX="1" />
                                                            </Footer>
                                                        </igtbl:UltraGridColumn>
                                                        <igtbl:UltraGridColumn Width="124px" BaseColumnName="DesProceso" IsBound="true" Key="DesProceso"
                                                            CellMultiline="No">
                                                            <Header Caption="Proceso">
                                                                <RowLayoutColumnInfo OriginX="2" />
                                                            </Header>
                                                            <HeaderStyle HorizontalAlign="Center" />
                                                            <Footer>
                                                                <RowLayoutColumnInfo OriginX="2" />
                                                            </Footer>
                                                        </igtbl:UltraGridColumn>
                                                        <igtbl:UltraGridColumn Hidden="true" Width="124px" BaseColumnName="CodCentroTrabajo"
                                                            IsBound="true" Key="CodCentroTrabajo" CellMultiline="No">
                                                            <Header Caption="CodCentroTrabajo">
                                                                <RowLayoutColumnInfo OriginX="3" />
                                                            </Header>
                                                            <HeaderStyle HorizontalAlign="Center" />
                                                            <Footer>
                                                                <RowLayoutColumnInfo OriginX="3" />
                                                            </Footer>
                                                        </igtbl:UltraGridColumn>
                                                        <igtbl:UltraGridColumn Width="124px" BaseColumnName="DesCentroTrabajo" IsBound="true"
                                                            Key="DesCentroTrabajo" CellMultiline="No">
                                                            <Header Caption="Centro de trabajo">
                                                                <RowLayoutColumnInfo OriginX="4" />
                                                            </Header>
                                                            <HeaderStyle HorizontalAlign="Center" />
                                                            <Footer>
                                                                <RowLayoutColumnInfo OriginX="4" />
                                                            </Footer>
                                                        </igtbl:UltraGridColumn>
                                                        <igtbl:UltraGridColumn Hidden="true" Width="124px" BaseColumnName="CodMaquina" IsBound="true"
                                                            Key="CodMaquina" CellMultiline="No">
                                                            <Header Caption="CodMaquina">
                                                                <RowLayoutColumnInfo OriginX="5" />
                                                            </Header>
                                                            <HeaderStyle HorizontalAlign="Center" />
                                                            <Footer>
                                                                <RowLayoutColumnInfo OriginX="5" />
                                                            </Footer>
                                                        </igtbl:UltraGridColumn>
                                                        <igtbl:UltraGridColumn Width="75px" BaseColumnName="DesMaquina" IsBound="true" Key="DesMaquina"
                                                            CellMultiline="No">
                                                            <Header Caption="Maquina">
                                                                <RowLayoutColumnInfo OriginX="6" />
                                                            </Header>
                                                            <HeaderStyle HorizontalAlign="Center" />
                                                            <Footer>
                                                                <RowLayoutColumnInfo OriginX="6" />
                                                            </Footer>
                                                        </igtbl:UltraGridColumn>
                                                        
                                                        <igtbl:UltraGridColumn Hidden="true" Width="124px" BaseColumnName="CodOperador" IsBound="true"
                                                            Key="CodOperador" CellMultiline="No">
                                                            <Header Caption="CodOperador">
                                                                <RowLayoutColumnInfo OriginX="8" />
                                                            </Header>
                                                            <HeaderStyle HorizontalAlign="Center" />
                                                            <Footer>
                                                                <RowLayoutColumnInfo OriginX="8" />
                                                            </Footer>
                                                        </igtbl:UltraGridColumn>
                                                        <igtbl:UltraGridColumn Width="110px" BaseColumnName="Operador" IsBound="true" Key="Operador"
                                                            CellMultiline="No">
                                                            <Header Caption="Operador">
                                                                <RowLayoutColumnInfo OriginX="9" />
                                                            </Header>
                                                            <HeaderStyle HorizontalAlign="Center" />
                                                            <Footer>
                                                                <RowLayoutColumnInfo OriginX="9" />
                                                            </Footer>
                                                        </igtbl:UltraGridColumn>
                                                        <igtbl:UltraGridColumn Type="Button" Hidden="true" Width="124px" BaseColumnName="CodPiezaTransaccion"
                                                            IsBound="true" Key="CodPiezaTransaccion" CellMultiline="No">
                                                            <Header Caption="CodPiezaTransaccion">
                                                                <RowLayoutColumnInfo OriginX="10" />
                                                            </Header>
                                                            <HeaderStyle HorizontalAlign="Center" />
                                                            <Footer>
                                                                <RowLayoutColumnInfo OriginX="10" />
                                                            </Footer>
                                                        </igtbl:UltraGridColumn>
                                                        <igtbl:UltraGridColumn Hidden="true" Width="120px" BaseColumnName="ExceptionMessage"
                                                            IsBound="True" Key="ExceptionMessage" CellMultiline="No">
                                                            <Header Caption="ExceptionMessage">
                                                                <RowLayoutColumnInfo OriginX="11" />
                                                            </Header>
                                                            <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                                                            <Footer>
                                                                <RowLayoutColumnInfo OriginX="11" />
                                                            </Footer>
                                                        </igtbl:UltraGridColumn>
                                                    </Columns>
                                                    <AddNewRow Visible="NotSet" View="NotSet">
                                                    </AddNewRow>
                                                </igtbl:UltraGridBand>
                                            </Bands>
                                            <DisplayLayout Name="UltraWebGrid1" AllowDeleteDefault="Yes" AllowUpdateDefault="RowTemplateOnly"
                                                NoDataMessage="" RowHeightDefault="20px" SelectTypeRowDefault="Single" TableLayout="Fixed"
                                                Version="3.00"  CellClickActionDefault="RowSelect" LoadOnDemand="Xml" AllowAddNewDefault="Yes"
                                                AllowColSizingDefault="Free" AllowSortingDefault="OnClient" CellPaddingDefault="1"
                                                HeaderClickActionDefault="SortSingle" RowSelectorsDefault="No" RowSizingDefault="Free"
                                                StationaryMargins="Header">
                                                <FrameStyle Height="400px" Width="800px">
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
                                                    ForeColor="White">
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
                                                <ClientSideEvents BeforeRowTemplateCloseHandler="BeforeRowTemplateCloseHandler" ClickCellButtonHandler="CellButtonClick"  />
                                            </DisplayLayout>
                                        </igtbl:UltraWebGrid>
                                    </td>
                                </tr>
                            </tbody>
                        </table>
                    </igmisc:WebAsyncRefreshPanel>

                    <script type="text/javascript">
                        function CellButtonClick(gridName, CellID) {
                      
                               $find('<%=WebDialogWindow4.ClientID%>').set_windowState($IG.DialogWindowState.Normal);
                        var dialog = $find('<%=WebDialogWindow4.ClientID%>');
                            var resultVar = dialog.get_windowState();
                            if (resultVar == 0)
                            $("#ctl00_Principal_WebDialogWindow4_tmpl_btnLlenarDefectos").click(); 
                    
                             

                             /* function WindowStateChanging(dialog, evtArgs) {
                            var dialog = $find('<%=WebDialogWindow4.ClientID%>');
                            var resultVar = dialog.get_windowState();
                                
                            if (resultVar == 0)
                            $("#ctl00_Principal_WebDialogWindow4_tmpl_btnLlenarDefectos").click(); 
                            }  */
                        }
                    
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
                    </script>

                    <ig:WebDialogWindow ID="WebDialogWindow1" runat="server" InitialLocation="Centered"
                        Height="110px" Width="380px" Modal="true" WindowState="Hidden">
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
                                                <input id="nombre" value="Kardex de producto" style="width: 200px;" type="text" runat="server" />
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
                <td style="width: 10px;">
                </td>
                <td colspan="3">
                    <igtblexp:UltraWebGridExcelExporter ID="uwgkardex" WorksheetName="kardexproducto"
                        DownloadName="Reporte.XLS" runat='server'>
                    </igtblexp:UltraWebGridExcelExporter>
                </td>
            </tr>
        </table>
        <ig:WebDialogWindow ID="WebDialogWindow4" runat="server" InitialLocation="Centered"
            Height="430px"      Width="600px" Modal="true" WindowState="Hidden" Font-Size="10px">
            <ContentPane BackColor="#FAFAFA">
                <Template>
                    <table align="center">
                        <tr>
                            <td class="lblTitulo1">
                                Defectos
                                 <asp:Button ID="btnLlenarDefectos" runat="server" Text="Button" CssClass="hidden"
                                                        OnClick="LlenarDefectos" />
                            </td>
                        </tr>
                    </table>
                    <igtbl:UltraWebGrid ID="UltraWebGrid4" runat="server" CaptionAlign="Left" EnableAppStyling="False"
                        EnableViewState="true" DisplayLayout-AllowAddNewDefault="No" DisplayLayout-AddNewRowDefault-Visible="No">
                        <Bands>
                            <igtbl:UltraGridBand>
                                <Columns>
                                    <igtbl:UltraGridColumn Width="150px" BaseColumnName="Zona" IsBound="True" Key="Zona"
                                        CellMultiline="Yes">
                                        <Header Caption="Zona">
                                            <RowLayoutColumnInfo OriginX="1" />
                                        </Header>
                                        <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                                        <Footer>
                                            <RowLayoutColumnInfo OriginX="1" />
                                        </Footer>
                                    </igtbl:UltraGridColumn>
                                    <igtbl:UltraGridColumn Width="90px" BaseColumnName="Defecto" IsBound="True" Key="Defecto"
                                        CellMultiline="No">
                                        <Header Caption="Defecto">
                                            <RowLayoutColumnInfo OriginX="2" />
                                        </Header>
                                        <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                                        <Footer>
                                            <RowLayoutColumnInfo OriginX="2" />
                                        </Footer>
                                    </igtbl:UltraGridColumn>
                                    <igtbl:UltraGridColumn Width="150px" BaseColumnName="AccionDefecto" IsBound="True"
                                        Key="AccionDefecto" CellMultiline="Yes">
                                        <Header Caption="Acción Defecto">
                                            <RowLayoutColumnInfo OriginX="3" />
                                        </Header>
                                        <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                                        <Footer>
                                            <RowLayoutColumnInfo OriginX="3" />
                                        </Footer>
                                    </igtbl:UltraGridColumn>
                                    <igtbl:UltraGridColumn Hidden="true" Width="100px" BaseColumnName="ExceptionMessage"
                                        IsBound="True" CellMultiline="Yes">
                                        <Header Caption="ExceptionMessage">
                                            <RowLayoutColumnInfo OriginX="4" />
                                        </Header>
                                        <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                                        <CellStyle HorizontalAlign="Center">
                                        </CellStyle>
                                        <Footer>
                                            <RowLayoutColumnInfo OriginX="4" />
                                        </Footer>
                                    </igtbl:UltraGridColumn>
                                </Columns>
                                <AddNewRow View="NotSet" Visible="NotSet">
                                </AddNewRow>
                            </igtbl:UltraGridBand>
                        </Bands>
                        <DisplayLayout Name="UltraWebGrid4" AllowDeleteDefault="No" AllowUpdateDefault="No"
                            NoDataMessage="" RowHeightDefault="20px" SelectTypeRowDefault="Single" TableLayout="Fixed"
                            Version="3.00" CellClickActionDefault="RowSelect" LoadOnDemand="Xml" AllowAddNewDefault="Yes"
                            AllowColSizingDefault="Free" AllowSortingDefault="OnClient" CellPaddingDefault="1"
                            HeaderClickActionDefault="SortSingle" RowSelectorsDefault="No" RowSizingDefault="Free">
                            <RowAlternateStyleDefault BackColor="#E5E5E5">
                            </RowAlternateStyleDefault>
                            <Pager AllowPaging="True" PageSize="20">
                                <PagerStyle Font-Size="11px" Font-Names="Arial" BackColor="#666666" ForeColor="White"
                                    Height="20px" BorderStyle="Solid" BorderColor="Black" BorderWidth="1px" />
                            </Pager>
                            <EditCellStyleDefault BackColor="silver">
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
                </Template>
            </ContentPane>
        </ig:WebDialogWindow>
        <asp:HiddenField ID="CodPiezaTransaccion" runat="server" />
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
