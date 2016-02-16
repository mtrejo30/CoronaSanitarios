<%@ Page Language="C#" MasterPageFile="~/ControlPisoLamosa.Master" Title="Lamosa - Piezas con Residencia"
    AutoEventWireup="true" CodeBehind="PiezasConResidencia.aspx.cs" Inherits="LAMOSA.SCPP.Client.View.Administrador.Etiquetas.PiezasConResidencia" %>

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
                <td style="height: 10px" colspan="3">
                </td>
            </tr>
            <tr style="height: 30px;">
                <td style="width: 10px; background-color: #eee;">
                </td>
                <td colspan="3" class="lblTitulo1">
                    <asp:Label ID="lblTitulo" runat="server" Text="Baja de Piezas con Residencia"></asp:Label><br />
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
                    <igmisc:WebAsyncRefreshPanel ID="WebAsyncRefreshPanel1" runat="server" ClientError="WebAsyncRefreshPanel1_Error" RefreshComplete="WebAsyncRefreshPanel1_RefreshComplete">
                        <table style="height: 170px; width: 600px">
                            <tbody>
                                <tr>
                                    <td style="height: 40px" class="textos_01">
                                        <table style="width: 750px">
                                            <tr>
                                                <td class="textos_01" style="text-align: right">
                                                    Alerta Residencia
                                                </td>
                                                <td class="textos_01" colspan="3" style="width: 65px">
                                                    <asp:DropDownList ID="ddlAlertaResidencia" runat="server" AutoPostBack="true" CssClass="textosd"
                                                        Width="230" OnSelectedIndexChanged="ddlAlertaResidencia_SelectedIndexChanged">
                                                    </asp:DropDownList>
                                                </td>
                                                <td class="textos_01" style="width: 93px; text-align: right">
                                                    &nbsp;
                                                </td>
                                                <td class="textos_01">
                                                    &nbsp;
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="textos_01" style="text-align: right">
                                                    *Planta:
                                                </td>
                                                <td class="textos_01" style="width: 65px">
                                                    <asp:DropDownList ID="ddlPlanta" Width="230" OnSelectedIndexChanged="LlenarMaquina_SelectedIndexChanged"
                                                        runat="server" CssClass="textosd" AutoPostBack="true">
                                                    </asp:DropDownList>
                                                </td>
                                                <td class="textos_01" style="width: 93px; text-align: right">
                                                    &nbsp;*Proceso:
                                                </td>
                                                <td class="textos_01">
                                                    <asp:DropDownList ID="ddlProceso" runat="server" CssClass="textosd" Width="230" AutoPostBack="True"
                                                        OnSelectedIndexChanged="LlenarMaquina_SelectedIndexChanged">
                                                    </asp:DropDownList>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="textos_01" style="text-align: right">
                                                    *Tipo Articulo:
                                                </td>
                                                <td class="textos_01" style="width: 65px">
                                                    <asp:DropDownList ID="ddlTipoArticulo" runat="server" Width="230" CssClass="textosd"
                                                        AutoPostBack="true" OnSelectedIndexChanged="ddlTipoArticulo_SelectedIndexChanged">
                                                    </asp:DropDownList>
                                                </td>
                                                <td class="textos_01" style="width: 93px; text-align: right">
                                                    *Articulo:
                                                </td>
                                                <td class="textos_01">
                                                    <asp:DropDownList ID="ddlArticulo" runat="server" CssClass="textosd" Width="230">
                                                    </asp:DropDownList>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="textos_01" style="text-align: right">
                                                    Maquina:
                                                </td>
                                                <td class="textos_01" style="width: 65px">
                                                    <asp:DropDownList ID="ddlMaquina" runat="server" CssClass="textosd" Width="230">
                                                    </asp:DropDownList>
                                                </td>
                                                <td class="textos_01" style="width: 93px; text-align: right">
                                                    Color:
                                                </td>
                                                <td class="textos_01">
                                                    <asp:DropDownList ID="ddlColor" runat="server" CssClass="textosd" Width="230">
                                                    </asp:DropDownList>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="text-align: right" class="textos_01">
                                                    Empleado(Clave):
                                                </td>
                                                <td class="textos_01" style="width: 65px">
                                                    <asp:TextBox ID="txtEmpleado" runat="server"></asp:TextBox>
                                                    <input id="SeleccionaEmp" class="Boton_01" onclick="openModalWin()" style="width: 89px;
                                                        position: absolute;" type="button" value="Seleccionar" />
                                                </td>
                                                <td class="textos_01" style="width: 93px; text-align: right">
                                                    Turno:
                                                </td>
                                                <td class="textos_01">
                                                    <asp:DropDownList ID="ddlTurno" runat="server" CssClass="textosd" Width="230">
                                                    </asp:DropDownList>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="textos_01" style="text-align: right">
                                                    D&iacute;as de residencia
                                                </td>
                                                <td class="textos_01" style="width: 65px">
                                                    <asp:TextBox ID="txtDiasResidencia" runat="server" Width="230"></asp:TextBox>
                                                </td>
                                                <td class="textos_01" style="width: 93px; text-align: right">
                                                    &nbsp; Tipo Busqueda:</td>
                                                <td class="textos_01">
                                                    <asp:DropDownList ID="ddlTipoBusqueda" runat="server" CssClass="textosd" Width="230">
                                                        <asp:ListItem Value="0" Text="Piezas de alerta" ></asp:ListItem>
                                                        <asp:ListItem Value="1" Text="Piezas Dadas de Baja por residencia" ></asp:ListItem>
                                                    </asp:DropDownList>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="textos_01" style="text-align: left">
                                                    <asp:Button ID="btnBuscar" runat="server" class="Boton_01" Text="Buscar" OnClick="btnBuscar_Click"
                                                        OnClientClick="javascript:return validarCapturaRequerida();" />
                                                </td>
                                                <td class="textos_01">
                                                    &nbsp;
                                                </td>
                                                <td class="textos_01">
                                                    <asp:Button ID="btnBaja" runat="server" CssClass="Boton_01" Text="Baja" 
                                                        onclick="btnBaja_Click" />
                                                </td>
                                                <td class="textos_01">
                                                    <asp:Button ID="btnReestablecer" runat="server" CssClass="Boton_01" 
                                                        Text="Reestablecer" onclick="btnReestablecer_Click" />
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <igtbl:UltraWebGrid ID="uwgPiezasConResidencia" runat="server" CaptionAlign="Left"
                                            EnableAppStyling="False">
                                            <Bands>
                                                <igtbl:UltraGridBand>
                                                    <AddNewRow Visible="NotSet" View="NotSet">
                                                    </AddNewRow>
                                                </igtbl:UltraGridBand>
                                            </Bands>
                                            <DisplayLayout Name="uwgPiezasConResidencia" NoDataMessage="" RowHeightDefault="20px"
                                                SelectTypeRowDefault="Single" TableLayout="Fixed" Version="3.00" CellClickActionDefault="RowSelect"
                                                LoadOnDemand="Xml" AllowAddNewDefault="Yes" AllowColSizingDefault="Free" CellPaddingDefault="1"
                                                HeaderClickActionDefault="SortSingle" RowSelectorsDefault="No" RowSizingDefault="Free">
                                                <RowAlternateStyleDefault BackColor="#E5E5E5">
                                                </RowAlternateStyleDefault>
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
                                                <ClientSideEvents InitializeLayoutHandler="InitializeLayout" />
                                            </DisplayLayout>
                                        </igtbl:UltraWebGrid>
                                    </td>
                                </tr>
                            </tbody>
                        </table>
                    </igmisc:WebAsyncRefreshPanel>

                    <script type="text/javascript">
                        ig_shared.getCBManager()._timeLimit = 300000; // Modifica el TimeOut de todos los WARP's 
                        WebAsyncRefreshPanel1_RefreshComplete(null);
                        function selecciona(codempleado, nombre) {
                            $("#<%=txtEmpleado.ClientID %>").val(codempleado);
                            $find('<%=WebDialogWindow3.ClientID%>').set_windowState($IG.DialogWindowState.Hidden);
                        }
                        function WebAsyncRefreshPanel1_Error(oPanel, oEvent, flags) {
                            var serverError = ig_shared.getCBManager().serverError;
                            alert(serverError);
                        }
                        function openModalWin() {
                            $find('<%=WebDialogWindow3.ClientID%>').set_windowState($IG.DialogWindowState.Normal);
                        }
                        function WebAsyncRefreshPanel1_RefreshComplete(oPanel) {
                            if ($('#<%=txtEmpleado.ClientID %>').attr('disabled'))
                                $('#SeleccionaEmp').attr('disabled', 'disabled')
                        }
                        function InitializeLayout(grid) {
                            var headerCheckBox = $('.checkBoxEditar');
                            headerCheckBox.html(headerCheckBox.html() + '<input type="checkbox" id="chkSelect"  />')
                        }
                        function validarCapturaRequerida() {
                            var strMensaje = '';
                            if (!$('#<%=ddlPlanta.ClientID %>').attr('disabled') && $('#<%=ddlPlanta.ClientID %>').val() < 1) strMensaje = 'Planta, '
                            if (!$('#<%=ddlProceso.ClientID %>').attr('disabled') && $('#<%=ddlProceso.ClientID %>').val() < 1) strMensaje = strMensaje + 'Proceso, '
                            if (!$('#<%=ddlTipoArticulo.ClientID %>').attr('disabled') && $('#<%=ddlTipoArticulo.ClientID %>').val() < 1) strMensaje = strMensaje + 'Tipo Articulo, '
                            /*if (!$('#<%=ddlArticulo.ClientID %>').attr('disabled') && $('#<%=ddlArticulo.ClientID %>').val() < 1) strMensaje = strMensaje + 'Articulo, '*/

                            if (strMensaje != '') {
                                strMensaje = 'Los siguientes Campos son Requeridos: ' + strMensaje;
                                strMensaje = strMensaje.substring(0, strMensaje.length - 2)
                                alert(strMensaje);
                                return false;
                            }
                            return true;
                        }
                        $(function() {
                            $('#chkSelect').live('click', function() {
                                //if ($(this).attr('checked')) {
                                var grid = igtbl_getGridById('<%=uwgPiezasConResidencia.ClientID %>');
                                var numColumns = grid.Bands[0].Columns.length
                                var rows = grid.Rows;
                                for (i = 0; i < rows.length; i++) {
                                    rows.getRow(i).getCell(numColumns - 1).setValue($(this).attr('checked'));
                                }
                                //}
                            });
                        })
                    </script>

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
                                    </div>
                                </igmisc:WebAsyncRefreshPanel>
                            </Template>
                        </ContentPane>
                    </ig:WebDialogWindow>
                </td>
            </tr>
            <tr>
                <td colspan="3">
                    &nbsp;
                </td>
            </tr>
        </table>
    </div>
</asp:Content>
