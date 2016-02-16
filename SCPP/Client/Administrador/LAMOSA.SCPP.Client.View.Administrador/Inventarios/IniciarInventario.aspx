<%@ Page Language="C#" MasterPageFile="~/ControlPisoLamosa.Master" AutoEventWireup="true"
    CodeBehind="IniciarInventario.aspx.cs" Inherits="LAMOSA.SCPP.Client.View.Administrador.Inventarios.IniciarInventario"
    Title="Control de piso - Iniciar Inventario" %>

<%@ Register Assembly="Infragistics35.Web.v9.1, Version=9.1.20091.1015, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb"
    Namespace="Infragistics.Web.UI.LayoutControls" TagPrefix="ig" %>
<%@ Register Assembly="Infragistics35.WebUI.Misc.v9.1, Version=9.1.20091.1015, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb"
    Namespace="Infragistics.WebUI.Misc" TagPrefix="igmisc" %>
<%@ Register Assembly="Infragistics35.WebUI.UltraWebGrid.ExcelExport.v9.1, Version=9.1.20091.1015, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb"
    Namespace="Infragistics.WebUI.UltraWebGrid.ExcelExport" TagPrefix="igtblexp" %>
<%@ Register Assembly="Infragistics35.WebUI.UltraWebGrid.v9.1, Version=9.1.20091.1015, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb"
    Namespace="Infragistics.WebUI.UltraWebGrid" TagPrefix="igtbl" %>
<asp:Content ID="Content1" ContentPlaceHolderID="Principal" runat="server">

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
                    <asp:Label ID="lblTitulo" runat="server" Text="Iniciar inventario"></asp:Label><br />
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
                            <li><a href="javascript:ListItemSelected(1,'')" id="LAddNew" runat="server">
                                <img src="../Imagenes/Nuevo.png" alt="Nuevo registro" style="border: 0px;" />
                                Nuevo registro</a></li>
                            <li><a href="javascript:ListItemSelected(2,'')" id="LExport" runat="server">
                                <img src="../Imagenes/Exportar.png" alt="Exportar tabla" style="border: 0px;" />
                                Exportar tabla</a></li>
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
                        <table>
                            <tbody>
                                <tr>
                                    <td>
                                        <table>
                                            <tbody>
                                                <tr>
                                                    <td>
                                                        <asp:Button ID="bBuscar" runat="server" CssClass="Boton_01" Style="float: left" Text="Refrescar"
                                                            OnClick="bBuscar_Click" />
                                                    </td>
                                                    <td>
                                                        <input type="button" class="Boton_01" style="width: 150px" value="Terminar ultimo Inv."
                                                            onclick="javascript:if(confirm('¿Desea terminar este ultimo proceso?'))$('#<%=btnTerminaInv.ClientID %>').click();" />
                                                        <asp:Button ID="btnTerminaInv" runat="server" Style="float: left; padding-left: 10px;
                                                            display: none" Text="Terminar ultimo Inv." Width="150px" OnClick="btnTerminaInventario_Click" />
                                                    </td>
                                                    <td>
                                                        <input type="button" class="Boton_01" value="Eliminar" onclick="javascript:DeleteRow(this)" />
                                                    </td>
                                                    <td>
                                                        <input type="button" class="Boton_01" style="width: 150px" value="Generar Inventario"
                                                            onclick="javascript:if(confirm('¿Desea generar un nuevo inventario en proceso?'))$('#<%=btnGenerarInventario.ClientID %>').click();" />
                                                        <asp:Button ID="btnGenerarInventario" runat="server" Style="float: right; display: none"
                                                            CssClass="Boton_01" Text="Generar Inventario" OnClick="btnGenerarInventario_Click" />
                                                    </td>
                                                </tr>
                                            </tbody>
                                        </table>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <igtbl:UltraWebGrid ID="UltraWebGrid1" runat="server" CaptionAlign="Left" EnableAppStyling="False"
                                            OnDeleteRow="UltraWebGrid1_DeleteRow">
                                            <Bands>
                                                <igtbl:UltraGridBand>
                                                    <AddNewRow Visible="NotSet" View="NotSet">
                                                    </AddNewRow>
                                                </igtbl:UltraGridBand>
                                            </Bands>
                                            <DisplayLayout Name="UltraWebGrid1" AllowDeleteDefault="Yes" AllowUpdateDefault="RowTemplateOnly"
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
                                                <ClientSideEvents DblClickHandler="DblClick" BeforeRowDeletedHandler="BeforeRowDeleted" />
                                            </DisplayLayout>
                                        </igtbl:UltraWebGrid>
                                    </td>
                                </tr>
                            </tbody>
                        </table>
                    </igmisc:WebAsyncRefreshPanel>

                    <script type="text/javascript">
                        var ajusteAutomatico = false;
                    
                        function ListItemSelected(idList, varList) {
                            if (idList == 1) {//Nuevo
                                igtbl_addNew("<%=UltraWebGrid1.ClientID%>", 0).editRow();

                            } else if (idList == 2) {//Exportar
                                $find('<%=WebDialogWindow1.ClientID%>').set_windowState($IG.DialogWindowState.Normal);
                            }
                        }
                        function BeforeRowDeleted(gridname, cellid) {
                            return !true;
                        }

                        function DblClick(gridname, cellid) {
                            var row = igtbl_getRowById(cellid);
                            $("#<%=hIdInventario.ClientID%>").val(row.getCell(0).getValue());
                            $("#<%=btnBuscaDetalle.ClientID%>").click();
                            $find('<%=WebDialogWindow3.ClientID%>').set_windowState($IG.DialogWindowState.Normal);
                        }

                        function DeleteRow(btnControl) {
                            var grid = igtbl_getGridById("<%=UltraWebGrid1.ClientID %>")
                            if (grid && grid.getActiveRow()) {
                                if (confirm('Desea realmente eliminar este inventario')) {
                                    var row = grid.getActiveRow();
                                    if (row.getCell(4).getValue()) {
                                        var c = row.deleteRow();
                                        $('#<%=bBuscar.ClientID %>').click();
                                    } else
                                        alert('Solo se puede eliminar el Inventario en Proceso Activo.')
                                }
                            } else {
                                alert('Debe seleccionar una fila primero')
                            }
                        }
                        function ClearGrid() {
                            var grid = igtbl_getGridById("<%=UltraWebGrid2.ClientID %>")
                            if (grid) {
                                var Rows = grid.Rows;
                                var rowCount = Rows.length - 1
                                for (var i = rowCount; i >= 0; i--) {
                                    Rows.remove(i);
                                }
                            }
                            if (ajusteAutomatico) {
                                debugger;
                                ajusteAutomatico = false;
                                $('#<%=bBuscar.ClientID %>').click();
                            }
                        }
                        function WindowStateChanging(dialog, evtArgs) {
                            if (dialog.get_windowState() == $IG.DialogWindowState.Normal)
                                ClearGrid();
                        }
                        function ExportToExcelDetalle() {
                            $('#<%=btnExportarDetalle.ClientID %>').click();
                        }  
                    </script>

                    <ig:WebDialogWindow ID="WebDialogWindow3" runat="server" InitialLocation="Centered"
                        Height="450px" Width="950px" Modal="true" ClientEvents-WindowStateChanging="WindowStateChanging" WindowState="Hidden" Font-Size="10px">
                        <ContentPane BackColor="#FAFAFA">
                            <Template>
                                <div style="padding: 5px;">
                                    <igmisc:WebAsyncRefreshPanel Height="100%" ID="WebAsyncRefreshPanel2" runat="server">
                                        <asp:HiddenField ID="hIdInventario" runat="server" />
                                        <table border="0">
                                            <tbody>
                                                <tr>
                                                    <td style="">
                                                        <table>
                                                            <tr>
                                                                <td>
                                                                    <asp:Button ID="btnBuscaDetalle" runat="server" CssClass="Boton_01" Text="Actualizar"
                                                                        OnClick="btnBuscaDetalle_Click" />
                                                                </td>
                                                                <td>
                                                                    <asp:Button ID="btnAjusteAutomatico" Width="150" runat="server" CssClass="Boton_01"
                                                                        Text="Ajuste automatico" OnClientClick="javascript:ajusteAutomatico=true;" OnClick="btnAjusteAutomatico_Click"/>
                                                                    <a class="tooltip">
                                                                        <img alt="Orientaci&oacute;n" src="../Imagenes/help_icon.gif" />
                                                                        <span>Ajuste automatico, Esta opci&oacute;n confirmara todas las piezas aqui mostradas
                                                                        </span></a>
                                                                </td>
                                                                <td>
                                                                <input type="button" style="width:150px" class="Boton_01" value="Exportar a excel" onclick="javascript:ExportToExcelDetalle()" />
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <igtbl:UltraWebGrid ID="UltraWebGrid2" runat="server" CaptionAlign="Left" EnableAppStyling="False"
                                                            DisplayLayout-AllowAddNewDefault="No" DisplayLayout-AddNewRowDefault-Visible="No">
                                                            <Bands>
                                                                <igtbl:UltraGridBand>
                                                                    <AddNewRow Visible="NotSet" View="NotSet">
                                                                    </AddNewRow>
                                                                </igtbl:UltraGridBand>
                                                            </Bands>
                                                            <DisplayLayout Name="UltraWebGrid1" AllowDeleteDefault="No" AllowUpdateDefault="No"
                                                                NoDataMessage="" RowHeightDefault="20px" SelectTypeRowDefault="Single" TableLayout="Fixed"
                                                                Version="3.00" CellClickActionDefault="RowSelect" LoadOnDemand="Xml" AllowAddNewDefault="Yes"
                                                                AllowColSizingDefault="Free" AllowSortingDefault="OnClient" CellPaddingDefault="1"
                                                                HeaderClickActionDefault="SortSingle" RowSelectorsDefault="No" RowSizingDefault="Free">
                                                                <RowAlternateStyleDefault BackColor="#E5E5E5">
                                                                </RowAlternateStyleDefault>
                                                                <Pager AllowPaging="False" PageSize="20">
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
                                                                <ClientSideEvents />
                                                            </DisplayLayout>
                                                        </igtbl:UltraWebGrid>
                                                    </td>
                                                </tr>
                                            </tbody>
                                        </table>
                                    </igmisc:WebAsyncRefreshPanel>
                                </div>
                            </Template>
                        </ContentPane>
                    </ig:WebDialogWindow>
                    <asp:Button ID="btnExportarDetalle" Width="150" style="display:none" runat="server" CssClass="Boton_01" Text="Exportar a Excel"
                                                                        OnClick="btnExportaDetalle_Click" />
                    <ig:WebDialogWindow ID="WebDialogWindow1" runat="server" InitialLocation="Centered"
                        Height="100px" Width="400px" Modal="true" WindowState="Hidden" Font-Size="10px">
                        <ContentPane BackColor="#FAFAFA">
                            <Template>
                                <div style="padding: 5px;">
                                    <table cellpadding="0" cellspacing="0" align="center" style="text-align: center;
                                        width: 100%">
                                        <tr>
                                            <td align="center" colspan="3">
                                                <asp:Panel ID="pnlExporta" runat="server" Width="300px">
                                                    <asp:DropDownList ID="ddlSeleccion" runat="server" CssClass="AreaBox_02" Width="180px">
                                                        <asp:ListItem>Documento Portable (PDF)</asp:ListItem>
                                                        <asp:ListItem>MS Word (DOC)</asp:ListItem>
                                                        <asp:ListItem>MS Excel (XLS)</asp:ListItem>
                                                    </asp:DropDownList>
                                                    <asp:Button ID="btnExporta" runat="server" CssClass="Boton_01" OnClick="btnExporta_Click"
                                                        Text="Exportar" Width="115px" />
                                                </asp:Panel>
                                                &nbsp; &nbsp;
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="3">
                                                &nbsp;
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
                <td>
                    <igtblexp:UltraWebGridExcelExporter ID="uwgIniciarInv" WorksheetName="Plantas" DownloadName="Reporte.XLS"
                        runat='server'>
                    </igtblexp:UltraWebGridExcelExporter>
                </td>
            </tr>
        </table>
    </div>
</asp:Content>
