<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/ControlPisoLamosa.Master"
    Title="Control de piso - Excedente de códigos de barras" CodeBehind="ExcedenteCodBarras.aspx.cs"
    Inherits="LAMOSA.SCPP.Client.View.Administrador.Administracion.ExcedenteCodBarras" %>

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
            <Scripts>
                <asp:ScriptReference Path="../CallWebServiceMethods.js" />
            </Scripts>
            <Services>
                <asp:ServiceReference Path="../WebServiceSeg.asmx" />
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
                <td colspan="3" class="lblTitulo1">
                    <asp:Label ID="lblTitulo" runat="server" Text="Excedentes de códigos de Barra"></asp:Label><br />
                </td>
            </tr>
            <tr>
                <td style="height: 10px" colspan="3">
                </td>
            </tr>
            <tr>
                <td style="width: 10px;" rowspan="2">
                </td>
                <td rowspan="2" valign="top" class="leftarea">
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
                <td style="width: 20px;" rowspan="2">
                    &nbsp;
                </td>
                <td>
                    <igmisc:WebAsyncRefreshPanel ID="WebAsyncRefreshPanel1" runat="server">
                        <table style="height: 170px; width: 700px">
                            <tbody>
                                <tr>
                                    <td class="textos_01">
                                        <table style="width: 442px" border="0" >
                                              <tr>
                                                <td class="textos_01">
                                                    Planta:
                                                </td>
                                                <td class="textos_01">
                                                    <asp:TextBox ID="txtPlanta" Width="225" runat="server" CssClass="hidden"></asp:TextBox>
                                                    <asp:DropDownList ID="dllPlanta" Width="230" OnSelectedIndexChanged="dllPlanta_SelectedIndexChanged"
                                                        runat="server" CssClass="textosd" AutoPostBack="true">
                                                    </asp:DropDownList>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="textos_01">
                                                    Centro de Trabajo:
                                                </td>
                                                <td class="textos_01">
                                                    <asp:DropDownList ID="cmbCentroTrabajo" runat="server" CssClass="textosd">
                                                    </asp:DropDownList>
                                                     <asp:DropDownList ID="cmbCentroTrabajo2" runat="server" CssClass="hidden">
                                                    </asp:DropDownList>
                                                    
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="textos_01">
                                                    Empleado(Clave):
                                                </td>
                                                <td class="textos_01">
                                                    <asp:TextBox ID="txtEmpleado" runat="server"></asp:TextBox>
                                                    <input id="SeleccionaEmp" type="button" class="Boton_01" onclick="openModalWin(1)"
                                                        value="Seleccionar" style="width: 100px" />
                                                </td>
                                            </tr>
                                                  </table>
                                         <table  border="0" >
                                            <tr>
                                                <td class="textos_01">
                                                    Fecha:  &nbsp;  &nbsp;  &nbsp;  &nbsp;  &nbsp;  &nbsp;   &nbsp;  &nbsp;  &nbsp;  &nbsp;  &nbsp;  &nbsp;  &nbsp;  
                                                </td>
                                                <td class="textos_01" >
                                                    <igsch:WebDateChooser ID="wdcFechaIni" runat="server">
                                                    </igsch:WebDateChooser>   
                                                       </td> 
                                            <td  class="textos_01">
                                               &nbsp; al  &nbsp;
                                               </td> 
                                                 <td  class="textos_01">
                                                         <igsch:WebDateChooser ID="wdcFechaFin" runat="server">
                                                    </igsch:WebDateChooser>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="4" style="text-align: left" class="textos_01">
                                                    <asp:Button ID="Search" runat="server"  CssClass="Boton_01" OnClick="btnLlenaGrid_Click" Text="Buscar" />
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <input type="hidden" id="changeValue" value="" />

                                        <script type="text/javascript">
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
                                            }
                                            function ok(event) {
                                                var id = $("#hID").val() ? $("#hID").val() : -1;
                                                var codCT = $("#ddlCT").val();
                                                var cod_empleado = $("#txtEmpleado").val();
                                                var cod_desde = $("#cod_desde").val();
                                                var cod_hasta = $("#cod_hasta").val();
                                                if ((cod_hasta - cod_desde) < 0) {
                                                    alert('El codigo desde debe ser mayor o igual al codigo hasta');
                                                }
                                                else if (codCT != "" && cod_empleado != "" && cod_desde != "" && cod_hasta != "") {
                                                    if (confirm('¿Desea guardar cambios?')) {
                                                        //asignar valores a los hidden
                                                        $("#<%=hID.ClientID%>").val(id);
                                                        $("#<%=hCodCT.ClientID%>").val(codCT);
                                                        $("#<%=hCodEmpleado.ClientID%>").val(cod_empleado);
                                                        $("#<%=hCodDesde.ClientID%>").val(cod_desde);
                                                        $("#<%=hCodHasta.ClientID%>").val(cod_hasta);
                                                        //enviar al server
                                                        $("#<%=BotonGuardar.ClientID%>").click();
                                                        clickcancel = false;
                                                        igtbl_gRowEditButtonClick(event);
                                                        clickcancel = true;
                                                        beforeClose = true;
                                                    }
                                                }
                                                else alert('Informacion incompleta para poder guardar el registro!');
                                            }

                                            function cancel(event) {
                                                var edit = true;
                                                beforeClose = false;
                                                if ($("#changeValue").val()) {
                                                    if (!confirm('¿Está seguro de cerrar pantalla sin hacer cambios?')) {
                                                        edit = false;
                                                        beforeClose = true;
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
                                            function ListItemSelected(idList, varList) {
                                                if (idList == 1) {//Nuevo
                                                    igtbl_addNew("<%=uwgCodigBarras.ClientID%>", 0).editRow();
                                                } else if (idList == 2) {//Exportar
                                                    $find('<%=WebDialogWindow1.ClientID%>').set_windowState($IG.DialogWindowState.Normal);
                                                }
                                            }
                                            function BeforeRowTemplateOpen(gridName, rowId) {
                                                $("#ddlCT").html($("#<%=cmbCentroTrabajo2.ClientID %>").html())
                                            }
                                            function AfterRowTemplateOpen(gridName, rowId) {
                                                $("#changeValue").val("");
                                            }
                                            function openModalWin(idTarget) {
                                                targetModal = idTarget;
                                                $find('<%=WebDialogWindow3.ClientID%>').set_windowState($IG.DialogWindowState.Normal);
                                            }
                                            function BeforeRowTemplateCloseHandler(event) {
                                                if ($find('<%=WebDialogWindow3.ClientID%>').get_windowState() == $IG.DialogWindowState.Normal)
                                                    return true;
                                                return beforeClose;
                                            }
                                            $(function() {
                                                $("#tableTemplate INPUT, #tableTemplate SELECT").change(function() {
                                                    $("#changeValue").val(true);
                                                });
                                                $('.codeC').keyup(function() {
                                                    var cd = parseInt($('#cod_desde').val());
                                                    var ch = parseInt($('#cod_hasta').val());
                                                    var cant = ch - cd + 1;
                                                    $('#cantidad').val(cant);
                                                });
                                            });
                                            function cantidad() {
                                                alert(6)

                                            }
                                        </script>

                                        <igtbl:UltraWebGrid ID="uwgCodigBarras" runat="server" CaptionAlign="Left" EnableAppStyling="False"
                                            OnInitializeRow="uwgCodigBarras_InitializeRow" OnDeleteRow="uwgCodigBarras_DeleteRow">
                                            <Bands>
                                                <igtbl:UltraGridBand>
                                                    <Columns>
                                                    </Columns>
                                                    <RowEditTemplate>
                                                        <p align="center">
                                                            Asignacion de codigos de barra</p>
                                                        <br>
                                                            <table style="padding-left: 80px;" id="tableTemplate">
                                                                <tbody>
                                                                    <tr>
                                                                        <td>
                                                                            Centro de Trabajo:
                                                                        </td>
                                                                        <td>
                                                                            <input type="hidden" id="hCodCT" columnkey="ClaveCentroTrabajo" value="" />
                                                                            <select id="ddlCT" width="200px" columnkey="ClaveCentroTrabajo">
                                                                            </select>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td class="textos_01">
                                                                            Empleado(Clave):
                                                                        </td>
                                                                        <td class="textos_01">
                                                                            <input type="text" columnkey="ClaveEmpleado" id="txtEmpleado" />
                                                                            <input id="SeleccionaEmp" type="button" class="Boton_01" onclick="openModalWin(2)"
                                                                                value="Seleccionar" style="width: 100px" />
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td class="textos_01">
                                                                            Nombre Empleado:
                                                                        </td>
                                                                        <td class="textos_01">
                                                                            <input type="text" columnkey="EMpleado" style="width: 150px;" readonly="readonly"
                                                                                disabled="disabled" id="nomEmp" class="mayus" />
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td class="textos_01">
                                                                            Codigo desde:
                                                                        </td>
                                                                        <td class="textos_01">
                                                                            <input type="text" columnkey="Codigo Desde" style="width: 150px;" name="cod_desde"
                                                                                id="cod_desde" class="mayus codeC" />
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td class="textos_01">
                                                                            Codigo hasta:
                                                                        </td>
                                                                        <td class="textos_01">
                                                                            <input type="text" columnkey="Codigo Hasta" style="width: 150px;" name="cod_hasta"
                                                                                id="cod_hasta" class="mayus codeC" />
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td class="textos_01">
                                                                            Cantidad:
                                                                        </td>
                                                                        <td class="textos_01">
                                                                            <input type="text" columnkey="EMpleado" style="width: 150px;" readonly="readonly"
                                                                                disabled="disabled" id="cantidad" name="cantidad" class="mayus" />
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td colspan="2">
                                                                            <p align="center">
                                                                                <input id="igtbl_reOkBtn" onclick="ok(event)" style="width: 81px;" type="button"
                                                                                    value="OK" />
                                                                                &nbsp;
                                                                                <input id="igtbl_reCancelBtn" onclick="cancel(event)" style="width: 103px;" type="button"
                                                                                    value="Cancel"> </input>
                                                                                </input>
                                                                            </p>
                                                                        </td>
                                                                    </tr>
                                                                </tbody>
                                                            </table>
                                                            <br></br>
                                                        </br>
                                                    </RowEditTemplate>
                                                    <RowTemplateStyle BackColor="White" BorderColor="White" BorderStyle="Ridge" Width="625">
                                                        <BorderDetails WidthBottom="3px" WidthLeft="3px" WidthRight="3px" WidthTop="3px" />
                                                    </RowTemplateStyle>
                                                    <AddNewRow Visible="NotSet" View="NotSet">
                                                    </AddNewRow>
                                                </igtbl:UltraGridBand>
                                            </Bands>
                                            <DisplayLayout Name="uwgCodigBarras" AllowDeleteDefault="Yes" AllowUpdateDefault="RowTemplateOnly"
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
                                                <ClientSideEvents BeforeRowTemplateCloseHandler="BeforeRowTemplateCloseHandler" BeforeRowTemplateOpenHandler="BeforeRowTemplateOpen"
                                                    AfterRowTemplateOpenHandler="AfterRowTemplateOpen" />
                                            </DisplayLayout>
                                        </igtbl:UltraWebGrid>
                                    </td>
                                </tr>
                            </tbody>
                        </table>
                        <asp:HiddenField ID="hID" runat="server" />
                        <asp:HiddenField ID="hCodCT" runat="server" />
                        <asp:HiddenField ID="hCodEmpleado" runat="server" />
                        <asp:HiddenField ID="hCodDesde" runat="server" />
                        <asp:HiddenField ID="hCodHasta" runat="server" />
                        <asp:Button ID="BotonGuardar" runat="server" Text="Button" CssClass="hidden" OnClick="BotonGuardar_click" />
                    </igmisc:WebAsyncRefreshPanel>
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
                                                <input id="nombre" value="Reporte de Usuarios" style="width: 200px;" type="text"
                                                    runat="server" />
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
                                                        Nombre empleado:
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
                    <igtblexp:UltraWebGridExcelExporter ID="uwgUsuarios" WorksheetName="Usuarios" DownloadName="Reporte.XLS"
                        runat='server'>
                    </igtblexp:UltraWebGridExcelExporter>
                </td>
            </tr>
        </table>
        <asp:HiddenField ID="Planta" runat="server" />
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
