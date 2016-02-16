<%@ Page Language="C#" MasterPageFile="~/ControlPisoLamosa.Master" AutoEventWireup="true"
    CodeBehind="Defectos.aspx.cs" Inherits="LAMOSA.SCPP.Client.View.Administrador.Reportes.Defectos"
    Title="Defectos" %>

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
        <asp:ScriptManager ID="sm" runat="server" AsyncPostBackTimeout="3600">
            <Scripts>
                <asp:ScriptReference Path="../CallWebServiceMethods.js" />
            </Scripts>
            <Services>
                <asp:ServiceReference Path="../WebServiceSe.asmx" />
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
                    <asp:Label ID="lblTitulo" runat="server" Text="Defectos"></asp:Label><br />
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
                        <table style="height: 200px; width: 700px">
                            <tbody>
                                <tr>
                                    <td style="height: 40px" class="textos_01">
                                        <table style="width: 700px">
                                            <tr>
                                                <td class="textos_01">
                                                    Planta:
                                                </td>
                                                <td class="textos_01">
                                                    <asp:DropDownList ID="ddlPlanta" runat="server" CssClass="textosd" OnSelectedIndexChanged="CargaCentroTrabajo_SelectedIndexChanged">
                                                    </asp:DropDownList>
                                                </td>
                                                <td class="textos_01">
                                                    Proceso:
                                                </td>
                                                <td class="textos_01">
                                                    <asp:DropDownList ID="ddlProceso" runat="server" CssClass="textosd"
                                                        OnSelectedIndexChanged="CargaCentroTrabajo_SelectedIndexChanged">
                                                    </asp:DropDownList>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="textos_01">
                                                    &nbsp;Vaciador(Clave):
                                                </td>
                                                <td class="textos_01">
                                                    <asp:TextBox ID="txtEmpleado" runat="server"></asp:TextBox>
                                                </td>
                                                <td><input ID="SeleccionaEmp" class="Boton_01" onclick="openModalWin(1)" 
                                                        style="width: 100px" type="button" value="Seleccionar" /></td>
                                                <td style="display:none">
                                                    Centro trabajo</td>
                                                <td>
                                                    &nbsp;<asp:DropDownList ID="ddlCentroTrabajo" runat="server" 
                                                        AutoPostBack="true" CssClass="textosd" Visible=false>
                                                    </asp:DropDownList>
                                                </td>
                                                <td>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="textos_01">
                                                    Tipo articulo:
                                                </td>
                                                <td class="textos_01">
                                                    <asp:DropDownList ID="ddlTipoArticulo" AutoPostBack="true" runat="server" CssClass="textosd"
                                                        OnSelectedIndexChanged="ddlTipoArticulo_SelectedIndexChanged">
                                                    </asp:DropDownList>
                                                </td>
                                                <td class="textos_01">
                                                    Modelo:
                                                </td>
                                                <td class="textos_01">
                                                    <asp:DropDownList ID="ddlModelo" runat="server" CssClass="textosd" Width="250px">
                                                    </asp:DropDownList>
                                                </td>
                                                <td class="textos_01">
                                                </td>
                                                <td class="textos_01">
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="textos_01">
                                                    Color:
                                                </td>
                                                <td class="textos_01">
                                                    <asp:DropDownList ID="ddlColor" runat="server" CssClass="textosd">
                                                    </asp:DropDownList>
                                                </td>
                                                <td class="textos_01">
                                                    Estado de defecto:
                                                </td>
                                                <td class="textos_01">
                                                    <asp:DropDownList ID="ddlEdoDefecto" runat="server" CssClass="textosd">
                                                    </asp:DropDownList>
                                                </td>
                                                <td class="textos_01">
                                                </td>
                                                <td class="textos_01">
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="textos_01">
                                                    Fecha:
                                                </td>
                                                <td class="textos_01">
                                                    <igsch:WebDateChooser ID="FechaIni" runat="server" Width="156">
                                                    </igsch:WebDateChooser>
                                                </td>
                                                <td class="textos_01">
                                                    al
                                                </td>
                                                <td class="textos_01">
                                                    <igsch:WebDateChooser ID="FechaFin" runat="server" Width="156">
                                                    </igsch:WebDateChooser>
                                                </td>
                                                <td class="textos_01">
                                                </td>
                                                <td class="textos_01">
                                                    <asp:Button OnClick="LlenaModal" ID="BRefresh2" runat="server" Text="Reload" CssClass="hidden" />
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="left" style="height: 20px; width: 850px;">
                                        <asp:Button ID="bBuscar" runat="server" CssClass="Boton_01" Text="Buscar" OnClick="bBuscar_Click" />
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <div style="overflow: auto; width: 800px;">
                                            <igtbl:UltraWebGrid ID="UltraWebGrid1" runat="server" CaptionAlign="Left" EnableAppStyling="False"
                                                OnInitializeLayout="UltraWebGrid1_InitializeLayout">
                                                <Bands>
                                                    <igtbl:UltraGridBand>
                                                        <AddNewRow View="NotSet" Visible="NotSet">
                                                        </AddNewRow>
                                                    </igtbl:UltraGridBand>
                                                </Bands>
                                                <DisplayLayout Name="UltraWebGrid1" AllowUpdateDefault="No" NoDataMessage="" RowHeightDefault="20px"
                                                    SelectTypeRowDefault="Single" CellClickActionDefault="CellSelect" TableLayout="Fixed"
                                                    Version="3.00" LoadOnDemand="Xml" AllowColSizingDefault="Free" AllowSortingDefault="No"
                                                    CellPaddingDefault="1" HeaderClickActionDefault="SortSingle" RowSelectorsDefault="Yes"
                                                    RowSizingDefault="Free" SelectTypeCellDefault="Single" StationaryMargins="Header">
                                                    <FrameStyle Height="400px" Width="800px">
                                                    </FrameStyle>
                                                    <RowAlternateStyleDefault BackColor="#E5E5E5">
                                                    </RowAlternateStyleDefault>
                                                    <ClientSideEvents DblClickHandler="DblClick" />
                                                    <Pager AllowPaging="false">
                                                        <PagerStyle BackColor="#666666" BorderColor="Black" BorderStyle="Solid" BorderWidth="1px"
                                                            Font-Names="Arial" Font-Size="11px" ForeColor="White" Height="20px" />
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
                                                    <SelectedHeaderStyleDefault BackColor="#FFEE33">
                                                    </SelectedHeaderStyleDefault>
                                                    <RowStyleDefault BackColor="White" BorderColor="Black" BorderStyle="Solid" BorderWidth="1px"
                                                        Font-Names="Verdana" Font-Size="8pt" ForeColor="Black">
                                                    </RowStyleDefault>
                                                    <SelectedRowStyleDefault BackColor="#FFEE33" Font-Bold="True" ForeColor="Black">
                                                    </SelectedRowStyleDefault>
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
                                        </div>
                                    </td>
                                </tr>
                            </tbody>
                        </table>
                    </igmisc:WebAsyncRefreshPanel>
                    <ig:WebDialogWindow ID="WebDialogWindow2" runat="server" InitialLocation="Centered"
                        Height="300px" Width="800px" Modal="true" WindowState="Hidden" Font-Size="10px">
                        <ContentPane BackColor="#FAFAFA">
                            <Template>
                                <div style="padding: 5px;">
                                    <table align="center">
                                        <tr>
                                            <td class="textos_01">
                                                <strong id="title_modal"></strong>
                                            </td>
                                            <br />
                                        </tr>
                                    </table>
                                    <igmisc:WebAsyncRefreshPanel ID="WebAsyncRefreshPanel2" runat="server">
                                        <input type="hidden" id="hIdDefecto" runat="server" />
                                        <input type="hidden" id="hIdZona" runat="server" />
                                        <input type="hidden" id="hIdProceso" runat="server" />
                                        <asp:Button OnClick="LlenaModal" ID="BRefresh" runat="server" Text="Reload" CssClass="hidden" />
                                        <igtbl:UltraWebGrid ID="UltraWebGrid2" runat="server" CaptionAlign="Left" EnableAppStyling="False">
                                            <Bands>
                                                <igtbl:UltraGridBand>
                                                </igtbl:UltraGridBand>
                                            </Bands>
                                            <DisplayLayout Name="UltraWebGrid1" AllowUpdateDefault="No" CellClickActionDefault="RowSelect"
                                                NoDataMessage="" RowHeightDefault="20px" SelectTypeRowDefault="Single" TableLayout="Fixed"
                                                Version="3.00" LoadOnDemand="Xml" AllowColSizingDefault="Free" AllowSortingDefault="No"
                                                CellPaddingDefault="1" HeaderClickActionDefault="SortSingle" RowSelectorsDefault="No"
                                                RowSizingDefault="Free">
                                                <RowAlternateStyleDefault BackColor="#E5E5E5">
                                                </RowAlternateStyleDefault>
                                                <Pager AllowPaging="False">
                                                    <PagerStyle Font-Size="11px" Font-Names="Arial" BackColor="#666666" ForeColor="White"
                                                        Height="20px" BorderStyle="Solid" BorderColor="Black" BorderWidth="1px" />
                                                </Pager>
                                                <EditCellStyleDefault BackColor="yellow">
                                                </EditCellStyleDefault>
                                                <SelectedRowStyleDefault BackColor="#FFEE33" Font-Bold="True" ForeColor="Black">
                                                </SelectedRowStyleDefault>
                                                <SelectedHeaderStyleDefault BackColor="#FFEE33">
                                                </SelectedHeaderStyleDefault>
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
                                                <ActivationObject BorderColor="Black" BorderWidth="2" BorderStyle="Dotted">
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
                                    </igmisc:WebAsyncRefreshPanel>
                                </div>
                            </Template>
                        </ContentPane>
                    </ig:WebDialogWindow>
                    <input type="hidden" id="changeValue" value="" />

                    <script type="text/javascript">
                        ig_shared.getCBManager()._timeLimit = 600000; // Modifica el TimeOut de todos los WARP's 
                        var beforeClose = true;
                        var clickcancel = true;
                        targetModal = 1; //1=empleado, 2=supervisor
                        var controlSeleccionaName = "";
                        function selecciona(codempleado, nombre) {
                            if (targetModal == 1)
                                $("#<%=txtEmpleado.ClientID %>").val(codempleado);
                            else {
                                $("#txtEmpleado").val(codempleado);
                                $("#nomEmp").val(nombre);
                                $("#changeValue").val(true);
                            }
                            $find('<%=WebDialogWindow3.ClientID%>').set_windowState($IG.DialogWindowState.Hidden);
                            //$("#NombreT").val(nombre);
                        }
                        function openModalWin(idTarget) {
                            targetModal = idTarget;
                            $find('<%=WebDialogWindow3.ClientID%>').set_windowState($IG.DialogWindowState.Normal);
                        }


                        function ListItemSelected(idList, varList) {
                            //var nuevo = document.getElementById("<%=hddSecurityConstants.ClientID %>").getAttribute('value').indexOf('1');
                            //   var exporta = document.getElementById("<%=hddSecurityConstants.ClientID %>").getAttribute('value').indexOf('6');

                            if (idList == 2) {//Exportar
                                $find('<%=WebDialogWindow1.ClientID%>').set_windowState($IG.DialogWindowState.Normal);
                            }
                        }
                        function DblClick(gridName, rowId) {
                            var gs = igtbl_getGridById(gridName);
                            var row = igtbl_getRowById(rowId);
                            var cell = gs.getActiveCell();
                            var colNo = cell.Column.Id.substring(cell.Column.Id.lastIndexOf('_') + 1);
                            if (colNo > 3 && (row.getIndex() + 1) < gs.Rows.length) {

                                $("#<%=hIdDefecto.ClientID %>").val(row.getCell(0).getValue());
                                $("#<%=hIdZona.ClientID %>").val(row.getCell(2).getValue());
                                $("#<%=hIdProceso.ClientID %>").val(row.getCell((colNo - 1)).getValue());
                                $("#title_modal").html(cell.Column.Key + "/" + row.getCell(1).getValue() + " " + row.getCell(3).getValue());
                                $find('<%=WebDialogWindow2.ClientID%>').set_windowState($IG.DialogWindowState.Normal);
                                //  $("#<%=BRefresh.ClientID%>").click();
                                var ctrl = document.getElementById("ctl00_Principal_WebDialogWindow2_tmpl_BRefresh");
                                ctrl.click();
                            }
                        }

                        function BeforeRowTemplateCloseHandler(event) {
                            if ($find('<%=WebDialogWindow3.ClientID%>').get_windowState() == $IG.DialogWindowState.Normal)
                                return true;
                            //                                                if (clickcancel) {
                            //                                                    $("#btnCancelar").click();
                            //                                                }
                            //                                                if (clickok) {
                            //                                                    $("#btnGuardar").click();
                            //                                                }
                            //                                                return beforeClose;
                            //                                                if (clickeliminar) {
                            //                                                    $("#btnEliminar").click();
                            //                                                }
                            return beforeClose;
                        }
                        $(function() {
                            $("#tableTemplate INPUT, #tableTemplate SELECT").change(function() {
                                $("#changeValue").val(true);
                            });
                        });

                    </script>

                    <ig:WebDialogWindow ID="WebDialogWindow1" runat="server" InitialLocation="Centered"
                        Height="150px" Width="400px" Modal="true" WindowState="Hidden" Font-Size="10px">
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
                                                <input id="nombre" value="Reporte de Defectos" style="width: 200px;" type="text"
                                                    runat="server" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="center" colspan="3">
                                                <asp:Button ID="btnExporta" runat="server" CssClass="Boton_01" OnClick="btnExporta_Click"
                                                    Text="Exportar" Width="115px" />
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
                    <ig:WebDialogWindow ID="WebDialogWindow3" runat="server" InitialLocation="Centered"
                        Height="300px" Width="500px" Modal="true" WindowState="Hidden" Font-Size="10px">
                        <ContentPane BackColor="#FAFAFA">
                            <Template>
                                <igmisc:WebAsyncRefreshPanel runat="server" ID="WebAsyncRefreshPane11">
                                    <div>
                                        <table border="0">
                                            <tbody>
                                                <tr>
                                                    <td class="textos_01">
                                                        Número empleado:
                                                    </td>
                                                    <td class="textos_01">
                                                        <asp:TextBox ID="NumEmpleadoWD" runat="server" CssClass="textost" Width="250px"></asp:TextBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="textos_01">
                                                        Nombre vaciador:
                                                    </td>
                                                    <td class="textos_01">
                                                        <asp:TextBox ID="NomEmpleadoWD" runat="server" CssClass="textost" Width="250px"></asp:TextBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        &nbsp;
                                                    </td>
                                                    <td class="textos_01">
                                                        <table width="100%">
                                                            <tr>
                                                                <td align="center">
                                                                    <asp:Button ID="BuscarEmp" runat="server" CssClass="Boton_01" OnClick="btnLlenaGridEmp_Click"
                                                                        Text="Buscar" Width="100px" />
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                </tr>
                                            </tbody>
                                        </table>
                                        <table border="0" align="center">
                                            <tbody>
                                                <tr>
                                                    <td>
                                                        <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" BackColor="White"
                                                            BorderColor="#CC9966" BorderStyle="None" BorderWidth="1px" CellPadding="4">
                                                            <Columns>
                                                                <asp:BoundField DataField="CodEmpleadoMFG" HeaderText="Clave" />
                                                                <asp:TemplateField HeaderText="Nombre Completo">
                                                                    <ItemTemplate>
                                                                        <asp:Literal ID="literal1" runat="server" Text='<%# "<a href=\"#\" onclick=\"selecciona("+ comilla + Eval("CodEmpleadoMFG")+ comilla +","+ comilla + Eval("NombreCompleto")+ comilla +");\" >" + Eval("NombreCompleto") + "</a>" %>'></asp:Literal>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:BoundField DataField="DescPuesto" HeaderText="Puesto" />
                                                            </Columns>
                                                            <RowStyle BackColor="White" ForeColor="#330099" />
                                                            <FooterStyle BackColor="#FFFFCC" ForeColor="#330099" />
                                                            <PagerStyle BackColor="#FFFFCC" ForeColor="#330099" HorizontalAlign="Center" />
                                                            <SelectedRowStyle BackColor="#FFCC66" Font-Bold="True" ForeColor="#663399" />
                                                            <HeaderStyle BackColor="#990000" Font-Bold="True" ForeColor="#FFFFCC" />
                                                        </asp:GridView>
                                                    </td>
                                                </tr>
                                            </tbody>
                                        </table>
                                        <table border="0" align="center">
                                            <tbody>
                                                <tr>
                                                    <td style="width: 130px">
                                                        <asp:Button ID="btnAceptar" runat="server" Text="Aceptar" CssClass="Boton_01" Width="140px"
                                                            ValidationGroup="grpUs" ToolTip="Guarda la información." />
                                                    </td>
                                                    <td style="width: 160px">
                                                        <asp:Button ID="btnCancelar" runat="server" Text="Cancelar" CssClass="Boton_01" OnClick="btnCancelar_Click"
                                                            Width="140px" CausesValidation="False" ToolTip="Limpia la información de los campos." />
                                                    </td>
                                                </tr>
                                            </tbody>
                                        </table>
                                    </div>
                                </igmisc:WebAsyncRefreshPanel>
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
