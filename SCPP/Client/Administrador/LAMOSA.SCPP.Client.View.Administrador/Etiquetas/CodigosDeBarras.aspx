<%@ Page Language="C#" MasterPageFile="~/ControlPisoLamosa.Master" Title="Lamosa - Codigo de Barras"
    AutoEventWireup="true" CodeBehind="CodigosDeBarras.aspx.cs" Inherits="LAMOSA.SCPP.Client.View.Administrador.Etiquetas.CodigosDeBarras" %>

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
                    <asp:Label ID="lblTitulo" runat="server" Text="Asignación de códigos de barra"></asp:Label><br />
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
                            <!--<li><a href="javascript:ListItemSelected(2,'')" id="LExport" runat="server">
                                <img src="../Imagenes/Exportar.png" alt="Exportar tabla" style="border: 0px;" />
                                Exportar tabla</a></li>-->
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
                            //$("#NombreT").val(nombre);
                        }
                        function ok(event) {
                            var id = $("#hID").val() ? $("#hID").val() : -1;
                            var codCT = $("#ddlCT").val();
                            var cod_maquina = $("#cmbMaquina").val();
                            var cod_empleado = $("#txtEmpleado").val();
                            var cod_desde = $("#cod_desde").val();
                            var cod_hasta = $("#cod_hasta").val();
                            if ((cod_hasta - cod_desde) < 0) {
                                alert('El codigo desde debe ser mayor o igual al codigo hasta');
                            }
                            else if (codCT != "" && cod_maquina != "" && cod_empleado != "" && cod_desde != "" && cod_hasta != "") {
                                if (confirm('¿Desea guardar cambios?')) {
                                    //asignar valores a los hidden
                                    $("#<%=hID.ClientID%>").val(id);
                                    $("#<%=hCodCT.ClientID%>").val(codCT);
                                    $("#<%=hCodMaquina.ClientID%>").val(cod_maquina);
                                    $("#<%=hCodEmpleado.ClientID%>").val(cod_empleado);
                                    $("#<%=hCodDesde.ClientID%>").val(cod_desde);
                                    $("#<%=hCodHasta.ClientID%>").val(cod_hasta);
                                    //alert("Planta: " + id.toString());
                                    //alert("Centro Trabajo: " + codCT.toString());
                                    //alert("Maquina: " + cod_maquina.toString());
                                    //alert("Empleado: " + cod_empleado.toString());
                                    //alert("Desde: " + cod_desde.toString());
                                    //alert("Hasta: " + cod_hasta.toString());
                                    //enviar al server
                                    //alert("#<%=BotonGuardar.ClientID%>");
                                    $("#<%=BotonGuardar.ClientID%>").click();
                                    //clickcancel = false;
                                    //igtbl_gRowEditButtonClick(event);
                                    //clickcancel = true;
                                    //beforeClose = true;
                                }
                            }
                            else alert('Informacion incompleta para poder guardar el registro!');
                        }
                        function ValidarEliminarAsignacionCodigo()
                        {
                            var btnEliminar = $get('<%=btnEliminar.ClientID%>'); //$("#<%=btnEliminar.ClientID%>").click();
                            var grid = igtbl_getGridById('<%=uwgCodigBarras.ClientID%>');
                            if (grid.ActiveRow == '')
                                alert("Favor de seleccionar el registro que desea eliminar.");
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
                                /*  SwitchDiv(true);*/

                            } else if (idList == 2) {//Exportar
                                $find('<%=WebDialogWindow1.ClientID%>').set_windowState($IG.DialogWindowState.Normal);
                            }
                        }
                        function BeforeRowTemplateOpen(gridName, rowId) {
                            $("#ddlCT").html($("#<%=cmbCentroTrabajo2.ClientID %>").html())
                        }
                        function AfterRowTemplateOpen(gridName, rowId) {
                            var ct = $("#hCodCT").val() ? $("#hCodCT").val() : $("#ddlCT").val();
                            if (ct) 
                                change(ct, $("#hCodMachine").val());
                            $("#changeValue").val("");
                            $("#tPlanta").val($("#<%=txtPlanta.ClientID %>").val());
                        }
                        function fillCmbMachine(options) {
                            $("#cmbMaquina").html(options);
                        }
                        function openModalWin(idTarget) {
                            targetModal = idTarget;
                            $find('<%=WebDialogWindow3.ClientID%>').set_windowState($IG.DialogWindowState.Normal);
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
                        function change(codigoCentroTrabajo, codigoMaquina) {
                            //LoadcmbMachine(cod_CT, cod_machine);
                            var cmbPlanta = $get('<%= dllPlanta.ClientID %>');
                            var codigoPlanta = cmbPlanta.value
                            if (codigoMaquina == null || String(codigoMaquina).length == 0) codigoMaquina = -1;
                            ObtenerMaquinas(-1, codigoCentroTrabajo, codigoPlanta, -1, codigoMaquina);
                        }
                        $(function() {
                            $("#tableTemplate INPUT, #tableTemplate SELECT").change(function() {
                                $("#changeValue").val(true);
                            });
                        });
                    </script>

                    <igmisc:WebAsyncRefreshPanel ID="WebAsyncRefreshPanel1" runat="server">
                        <table style="height: 170px; width: 600px">
                            <tbody>
                                <tr>
                                    <td style="height: 40px" class="textos_01">
                                        <table style="width: 550px">
                                            <tr>
                                                <td class="textos_01">Planta:</td>
                                                <td class="textos_01">
                                                    <asp:TextBox ID="txtPlanta" Width="225" runat="server" CssClass="hidden"></asp:TextBox>
                                                    <asp:DropDownList ID="dllPlanta" Width="230" OnSelectedIndexChanged="dllPlanta_SelectedIndexChanged"
                                                        runat="server" CssClass="textosd" AutoPostBack="true">
                                                    </asp:DropDownList>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="textos_01">Proceso:</td>
                                                <td class="textos_01">
                                                    <asp:TextBox ID="txtProceso" Width="225" runat="server" CssClass="hidden"></asp:TextBox>
                                                    <asp:DropDownList ID="dllProceso" Width="230" OnSelectedIndexChanged="dllProceso_SelectedIndexChanged"
                                                        runat="server" CssClass="textosd" AutoPostBack="true">
                                                    </asp:DropDownList>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="textos_01">Centro de Trabajo:</td>
                                                <td class="textos_01">
                                                    <asp:DropDownList ID="cmbCentroTrabajo" Width="230" OnSelectedIndexChanged="cmbCentroTrabajo_SelectedIndexChanged"
                                                        runat="server" CssClass="textosd" AutoPostBack="true">
                                                    </asp:DropDownList>
                                                    <asp:DropDownList ID="cmbCentroTrabajo2" Width="230" OnSelectedIndexChanged="cmbCentroTrabajo_SelectedIndexChanged"
                                                        runat="server" CssClass="hidden" AutoPostBack="true">
                                                    </asp:DropDownList>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="textos_01">Banco:</td>
                                                <td class="textos_01">
                                                    <asp:DropDownList ID="cmbBanco" runat="server" Width="230" CssClass="textosd">
                                                    </asp:DropDownList>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="textos_01">Empleado(Clave):</td>
                                                <td class="textos_01">
                                                <table>
                                                <tr>
                                                <td><asp:TextBox ID="txtEmpleado" runat="server"></asp:TextBox></td>
                                                <td><input id="SeleccionaEmp" type="button" class="Boton_01" onclick="openModalWin(1)" value="Seleccionar"/></td>
                                                <td><asp:Button ID="Search" runat="server" OnClick="btnLlenaGrid_Click" Text="Buscar" class="Boton_01" style="text-align:center"/></td>
                                                </tr>
                                                </table>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <input type="hidden" id="changeValue" value="" />
                                        <igtbl:UltraWebGrid ID="uwgCodigBarras" runat="server" CaptionAlign="Left" EnableAppStyling="False"
                                            OnInitializeRow="uwgCodigBarras_InitializeRow" 
                                            OnDeleteRow="uwgCodigBarras_DeleteRow" 
                                            onpageindexchanged="uwgCodigBarras_PageIndexChanged">
                                            <Bands>
                                                <igtbl:UltraGridBand>
                                                    <RowEditTemplate>
                                                        <p align="center">
                                                            Asignacion de codigos de barra</p>
                                                        <br>
                                                            <table style="padding-left: 80px;" id="tableTemplate">
                                                                <tbody>
                                                                    <tr>
                                                                        <td>Planta:</td>
                                                                        <td>
                                                                            <input type="hidden" id="hID" value="" columnkey="ClaveCodigoBarra" />
                                                                            <input type="text" columnkey="Planta" id="tPlanta" style="width: 150px;" readonly="readonly" disabled="disabled" class="mayus" />
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td>Centro de Trabajo:</td>
                                                                        <td>
                                                                            <input type="hidden" id="hCodCT" columnkey="ClaveCentroTrabajo" value="" />
                                                                            <select onchange="change(this.value);" id="ddlCT" width="200px" columnkey="ClaveCentroTrabajo">
                                                                            </select>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td>Maquina:</td>
                                                                        <td>
                                                                            <input type="hidden" id="hCodMachine" columnkey="ClaveMaquina" value="" />
                                                                            <select id="cmbMaquina" width="200px">
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
                                                                            <input type="text" columnkey="Codigo Desde" style="width: 150px;" id="cod_desde"
                                                                                class="mayus" />
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td class="textos_01">
                                                                            Codigo hasta:
                                                                        </td>
                                                                        <td class="textos_01">
                                                                            <input type="text" columnkey="Codigo Hasta" style="width: 150px;" id="cod_hasta"
                                                                                class="mayus" />
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
                                                <ClientSideEvents AfterRowTemplateOpenHandler="AfterRowTemplateOpen" 
                                                    BeforeRowTemplateCloseHandler="BeforeRowTemplateCloseHandler" 
                                                    BeforeRowTemplateOpenHandler="BeforeRowTemplateOpen" />
                                                <Pager AllowPaging="True" NextText="Siguiente" PageSize="20" 
                                                    PrevText="Anterior" StyleMode="PrevNext">
                                                    <PagerStyle BackColor="#666666" BorderColor="Black" BorderStyle="Solid" 
                                                        BorderWidth="1px" Font-Names="Arial" Font-Size="11px" ForeColor="White" 
                                                        Height="20px" />
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
                                <tr>
                                <td align="right"><asp:Button ID="btnEliminar" runat="server" Text="Eliminar" ToolTip="Eliminar asignación de codigo" onclick="btnEliminar_Click" onclientclick="ValidarEliminarAsignacionCodigo()" CssClass="Boton_01" style ="text-align:center"/></td>
                                </tr>
                            </tbody>
                        </table>
                        <asp:HiddenField ID="hID" runat="server" />
                        <asp:HiddenField ID="hCodCT" runat="server" />
                        <asp:HiddenField ID="hCodMaquina" runat="server" />
                        <asp:HiddenField ID="hCodEmpleado" runat="server" />
                        <asp:HiddenField ID="hCodDesde" runat="server" />
                        <asp:HiddenField ID="hCodHasta" runat="server" />
                        <asp:Button ID="BotonGuardar" runat="server" Text="Button" CssClass="" 
                            OnClick="BotonGuardar_Click" Height="0px" Width="0px" BackColor="White" BorderColor="White" BorderStyle="None" />
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
