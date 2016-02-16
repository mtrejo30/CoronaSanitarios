<%@ Page Language="C#" MasterPageFile="~/ControlPisoLamosa.Master" Title="Control de piso - Configuración de esmalte"
    AutoEventWireup="true" CodeBehind="ConfEsmalte.aspx.cs" Inherits="LAMOSA.SCPP.Client.View.Administrador.Configuraciones.ConfEsmalte" %>

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
                <td colspan="3" class="lblTitulo1">
                    <asp:Label ID="lblTitulo" runat="server" Text="Configuración de esmalte"></asp:Label><br />
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
                        <table style="height: 100px; width: 450px">
                            <tbody>
                                <tr>
                                    <td>
                                        <table>
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
                                                <td align="center" style="height: 26px" class="textos">
                                                    <asp:Button ID="igtbl_reBuscaBtn" runat="server" CssClass="Boton_01" OnClick="btnLlenaGrid_Click"
                                                        Text="Buscar" />
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <tr>
                                    <td>

                                        <script src="../FuncionesJS/jquery-1.4.2.js" type="text/javascript"></script>

                                        <script src="../FuncionesJS/Validaciones.js" type="text/javascript"></script>

                                        <script type="text/javascript">
                                            var beforeClose = true;
                                            var clickcancel = true;
                                            var isValuesChange = false; //Variable que controla si se ha cambiado algun valor del RowEditTemplate al mostrarse
                                            function ok(event) {
                                                var CodCondicionEsmalte = $("#CodCondicionEsmalteH").val();
                                                var Fecha = $("#FechaT").val();
                                                var TiempoEspejo = $("#TiempoEspejoT").val();
                                                var Viscosidad = $("#ViscosidadT").val();
                                                var Densidad = $("#DensidadT").val();
                                                var Espesor = $("#EspesorT").val();
                                                var Autorizacion = "'" + $("#AutorizacionT").attr('checked') + "'";
                                                var Activo = "'" + $("#ActivoT").attr('checked') + "'";
                                                var Turno = $("#cbxTurno").val();
                                                var Color = $("#cbxColor").val();
                                                var lstMaquinas = $("#cbxMaquina").val();

                                                var numeroLote = $("#txtNumeroLote").val();
                                                var tamanoLote = $("#txtTamanoLote").val();
                                                var cantidadGoma = $("#txtCantidadGoma").val();
                                                var molino = $("#txtMolino").val();
                                                var granulometria = $("#txtGranulometria").val();

                                                var isAllCaptured = true; ;
                                                $('#templatePasta input:text, #templatePasta SELECT').each(function() {
                                                    if (!$(this).val() || $(this).val() < 1) {isAllCaptured = false; return isAllCaptured; }
                                                })
												
                                                if (isValuesChange && isAllCaptured) {
                                                    if (confirm('¿Desea guardar cambios?')) {
                                                        //asignar valores a los hidden
                                                        $("#<%=hddCodCondicionEsmalte.ClientID%>").val(CodCondicionEsmalte);
                                                        $("#<%=hddTiempoEspejo.ClientID%>").val(TiempoEspejo);
                                                        $("#<%=hddViscosidad.ClientID%>").val(Viscosidad);
                                                        $("#<%=hddDensidad.ClientID%>").val(Densidad);
                                                        $("#<%=hddEspesor.ClientID%>").val(Espesor);
                                                        $("#<%=hddAutorizacion.ClientID%>").val(Autorizacion);
                                                        $("#<%=hddActivo.ClientID%>").val(Activo);
                                                        $("#<%=hddTurno.ClientID%>").val(Turno);
                                                        $("#<%=hddColor.ClientID%>").val(Color);
                                                        $("#<%=hddMaquinas.ClientID%>").val(lstMaquinas);

                                                        $("#<%=hddNumeroLote.ClientID%>").val(numeroLote);
                                                        $("#<%=hddTamanoLote.ClientID%>").val(tamanoLote);
                                                        $("#<%=hddCantidadGoma.ClientID%>").val(cantidadGoma);
                                                        $("#<%=hddMolino.ClientID%>").val(molino);
                                                        $("#<%=hddGranulometria.ClientID%>").val(granulometria);

                                                        $("#<%=BotonGuardar.ClientID%>").click();

                                                        clickcancel = false;
                                                        igtbl_gRowEditButtonClick(event);
                                                        clickcancel = true;
                                                        beforeClose = true;
                                                    }
                                                }
                                                else alert('Informacion incompleta para poder guardar el registro!');
                                            }

                                            function autorizar(event) {
                                                var CodCondicionEsmalte = $("#CodCondicionEsmalteH").val();
                                                var Autorizacion = "'" + $("#AutorizacionT").attr('checked') + "'";
                                                if (confirm('¿Realmente desea autorizar?')) {
                                                    //asignar valores a los hidden
                                                    $("#<%=hddCodCondicionEsmalte.ClientID%>").val(CodCondicionEsmalte);
                                                    $("#<%=hddAutorizacion.ClientID%>").val(Autorizacion);
                                                    $("#<%=BotonAutorizar.ClientID%>").click();
                                                    clickcancel = false;
                                                    igtbl_gRowEditButtonClick(event);
                                                    clickcancel = true;
                                                    beforeClose = true;
                                                }
                                            }

                                            function cancel(event) {
                                                var Planta_id = $("#IdPlanta").val();
                                                var NombrePlanta = $("#NombrePlanta").val();
                                                var ClaveTurno = $("#ClaveTurno").val();
                                                var Descripcion = $("#Descripcion").val();
                                                var HoraInicio = $("#HoraInicio").val();
                                                var HoraFin = $("HoraFin").val();
                                                var Activo = "'" + $("#Activo").attr('checked') + "S'";
                                                var oPlanta = $("#oPlanta").val();
                                                var oNombrePlanta = $("#oNombrePlanta").val();
                                                var oClaveTurno = $("#oClaveTurno").val();
                                                var oDescripcion = $("#oDescripcion").val();
                                                var oHoraInicio = $("#oHoraInicio").val();
                                                var oHoraFin = $("oHoraFin").val();
                                                var oActivo = "'" + $("#oActivo").val() + "S'";
                                                var edit = true;
                                                beforeClose = false;
                                                if (Descripcion != oDescripcion || HoraInicio != oHoraInicio || HoraFin != oHoraFin || Activo != oActivo) {
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
                                                var Planta_id = $("#IdPlanta").val();
                                                var ClaveTurno = $("#ClaveTurno").val();
                                                var Descripcion = $("#Descripcion").val();
                                                var HoraInicio = $("#HoraInicio").val();
                                                var HoraFin = $("HoraFin").val();
                                                var Activo = "'" + $("#Activo").attr('checked') + "S'";
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
                                            function BeforeRowTemplateOpen(gridName, rowId) {
                                                isValuesChange = false;
                                                $("#cbxTurno").html($("#<%=ddlTurno.ClientID %>").html());
                                                $("#cbxColor").html($("#<%=ddlColor.ClientID %>").html());
                                                $("#cbxMaquina").html($("#<%=ddlMaquina.ClientID %>").html());
                                                $("#cbxTurno option[value=" + $("#hTurno").val() + "]").attr("selected", true);
                                                $("#cbxColor option[value=" + $("#hColor").val() + "]").attr("selected", true);
                                                $("#cbxMaquina option").attr("selected", "");
                                                var maquina = $("#hListaMaquinas").val();
                                                if (maquina) {
                                                    var lstMaquina = maquina.split(',')
                                                    for (var i = 0; i < lstMaquina.length; i++)
                                                        $("#cbxMaquina option[value=" + lstMaquina[i] + "]").attr("selected", true);
                                                }
                                                SwitchDiv(false);
                                            }
                                            function SwitchDiv(nuevo) {
                                                $('#templatePasta INPUT, #templatePasta SELECT').each(function() {
                                                    $(this).attr('disabled', (nuevo ? '' : 'disabled'))
                                                })
                                                $('#FechaT').attr('disabled', 'disabled');
                                                $('#CancelT').attr('disabled', '');
                                                $('#AutorizacionT').attr('disabled', '');
                                                $('#cbxMaquina').attr('disabled', '');
                                                $('#AutorizarT').attr('disabled', '');
                                                $('#AutorizarT').removeClass('hidden').addClass('hidden');
                                                $('#AutorizacionT').removeClass('hidden').addClass('hidden');
                                                if (!nuevo) {
                                                    var Autorizacion = $("#AutorizacionT").attr('checked');
                                                    document.getElementById("AutorizarT").className = Autorizacion ? "hidden" : "Boton_01";
                                                }
                                            }
                                            $(function() {
                                                $('#ctl00_cmbPlanta').change(function() {
                                                    $('#<%= btnPlantaChange.ClientID %>').click();
                                                });
                                                $('#templatePasta input:text, #templatePasta SELECT').live(
                                                'change', function() {
                                                    isValuesChange = true;
                                                });
                                            });
                                        </script>

                                        <asp:DropDownList ID="ddlTurno" CssClass="hidden" runat="server">
                                        </asp:DropDownList>
                                        <asp:DropDownList ID="ddlColor" CssClass="hidden" runat="server">
                                        </asp:DropDownList>
                                        <asp:DropDownList ID="ddlMaquina" CssClass="hidden" runat="server">
                                        </asp:DropDownList>
                                        <asp:Button ID="btnPlantaChange" CssClass="hidden" runat="server" Text="Button" OnClick="Planta_SelectedIndexChange" />
                                        <igtbl:UltraWebGrid ID="UltraWebGrid1" runat="server" CaptionAlign="Left" EnableAppStyling="False"
                                            DisplayLayout-AllowUpdateDefault="NotSet" OnPageIndexChanged="cambio_pagina">
                                            <Bands>
                                                <igtbl:UltraGridBand>
                                                    <Columns>
                                                        <igtbl:UltraGridColumn Width="90px" BaseColumnName="CodCondicionEsmalte" IsBound="True"
                                                            Key="CodCondicionEsmalte" CellMultiline="Yes" Hidden="true">
                                                            <Header Caption="CodCondicionEsmalte">
                                                                <RowLayoutColumnInfo OriginX="0" />
                                                            </Header>
                                                            <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                                                            <Footer>
                                                                <RowLayoutColumnInfo OriginX="0" />
                                                            </Footer>
                                                        </igtbl:UltraGridColumn>
                                                        <igtbl:UltraGridColumn Width="90px" BaseColumnName="CodPlanta" IsBound="True" Key="CodPlanta"
                                                            CellMultiline="Yes" Hidden="true">
                                                            <Header Caption="CodPlanta">
                                                                <RowLayoutColumnInfo OriginX="1" />
                                                            </Header>
                                                            <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                                                            <Footer>
                                                                <RowLayoutColumnInfo OriginX="1" />
                                                            </Footer>
                                                        </igtbl:UltraGridColumn>
                                                        <igtbl:UltraGridColumn Width="90px" BaseColumnName="Fecha" IsBound="True" Key="Fecha"
                                                            CellMultiline="Yes">
                                                            <Header Caption="Fecha">
                                                                <RowLayoutColumnInfo OriginX="2" />
                                                            </Header>
                                                            <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                                                            <Footer>
                                                                <RowLayoutColumnInfo OriginX="2" />
                                                            </Footer>
                                                        </igtbl:UltraGridColumn>
                                                        <igtbl:UltraGridColumn Width="120px" BaseColumnName="TiempoEspejo" IsBound="True"
                                                            Key="TiempoEspejo" CellMultiline="Yes">
                                                            <Header Caption="Tiempo espejo">
                                                                <RowLayoutColumnInfo OriginX="3" />
                                                            </Header>
                                                            <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                                                            <Footer>
                                                                <RowLayoutColumnInfo OriginX="3" />
                                                            </Footer>
                                                        </igtbl:UltraGridColumn>
                                                        <igtbl:UltraGridColumn Width="100px" BaseColumnName="Viscosidad" IsBound="True" Key="Viscosidad"
                                                            CellMultiline="No">
                                                            <Header Caption="Viscosidad">
                                                                <RowLayoutColumnInfo OriginX="4" />
                                                            </Header>
                                                            <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                                                            <Footer>
                                                                <RowLayoutColumnInfo OriginX="4" />
                                                            </Footer>
                                                        </igtbl:UltraGridColumn>
                                                        <igtbl:UltraGridColumn Width="100px" BaseColumnName="Densidad" IsBound="True" Key="Densidad"
                                                            CellMultiline="No">
                                                            <Header Caption="Densidad">
                                                                <RowLayoutColumnInfo OriginX="5" />
                                                            </Header>
                                                            <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                                                            <Footer>
                                                                <RowLayoutColumnInfo OriginX="5" />
                                                            </Footer>
                                                        </igtbl:UltraGridColumn>
                                                        <igtbl:UltraGridColumn Width="100px" BaseColumnName="Espesor" IsBound="True" Key="Espesor"
                                                            CellMultiline="No">
                                                            <Header Caption="Espesor">
                                                                <RowLayoutColumnInfo OriginX="6" />
                                                            </Header>
                                                            <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                                                            <Footer>
                                                                <RowLayoutColumnInfo OriginX="6" />
                                                            </Footer>
                                                        </igtbl:UltraGridColumn>
                                                        <igtbl:UltraGridColumn Width="100px" BaseColumnName="UsuarioAutoriza" IsBound="True"
                                                            Key="UsuarioAutoriza" CellMultiline="No" Hidden="true">
                                                            <Header Caption="UsuarioAutoriza">
                                                                <RowLayoutColumnInfo OriginX="7" />
                                                            </Header>
                                                            <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                                                            <Footer>
                                                                <RowLayoutColumnInfo OriginX="7" />
                                                            </Footer>
                                                        </igtbl:UltraGridColumn>
                                                        <igtbl:UltraGridColumn Width="100px" BaseColumnName="FechaAutorizacion" IsBound="True"
                                                            Key="FechaAutorizacion" CellMultiline="No" Hidden="true">
                                                            <Header Caption="FechaAutorizacion">
                                                                <RowLayoutColumnInfo OriginX="8" />
                                                            </Header>
                                                            <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                                                            <Footer>
                                                                <RowLayoutColumnInfo OriginX="8" />
                                                            </Footer>
                                                        </igtbl:UltraGridColumn>
                                                        <igtbl:UltraGridColumn Width="120px" BaseColumnName="Autorizacion" IsBound="True"
                                                            Key="Autorizacion" Type="CheckBox" CellMultiline="no">
                                                            <Header Caption="Autorización">
                                                                <RowLayoutColumnInfo OriginX="9" />
                                                            </Header>
                                                            <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                                                            <CellStyle HorizontalAlign="Center">
                                                            </CellStyle>
                                                            <Footer>
                                                                <RowLayoutColumnInfo OriginX="9" />
                                                            </Footer>
                                                        </igtbl:UltraGridColumn>
                                                        <igtbl:UltraGridColumn Width="70px" BaseColumnName="Activo" IsBound="True" Key="Activo"
                                                            Type="CheckBox" CellMultiline="no">
                                                            <Header Caption="Activo">
                                                                <RowLayoutColumnInfo OriginX="10" />
                                                            </Header>
                                                            <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                                                            <CellStyle HorizontalAlign="Center">
                                                            </CellStyle>
                                                            <Footer>
                                                                <RowLayoutColumnInfo OriginX="10" />
                                                            </Footer>
                                                        </igtbl:UltraGridColumn>
                                                        <igtbl:UltraGridColumn Width="120px" BaseColumnName="ExceptionMessage" IsBound="True"
                                                            Key="ExceptionMessage" CellMultiline="No" Hidden="true">
                                                            <Header Caption="ExceptionMessage">
                                                                <RowLayoutColumnInfo OriginX="11" />
                                                            </Header>
                                                            <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                                                            <Footer>
                                                                <RowLayoutColumnInfo OriginX="11" />
                                                            </Footer>
                                                        </igtbl:UltraGridColumn>
                                                    </Columns>
                                                    <RowEditTemplate>
                                                        <table id="templatePasta" style="font-family: Arial; text-align: center">
                                                            <tr>
                                                                <td class="textos_01" style="text-align: right">
                                                                    <input type="hidden" id="CodCondicionEsmalteH" columnkey="CodCondicionEsmalte" />
                                                                    Fecha:
                                                                </td>
                                                                <td class="textos_01" style="width: 14px">
                                                                    <input type="text" id="FechaT" value="<%#(System.DateTime.Today.ToShortDateString())%>"
                                                                        disabled="disabled" />
                                                                </td>
                                                                <td class="textos_01" style="text-align: right">
                                                                    Numero de Lote:
                                                                </td>
                                                                <td class="textos_01">
                                                                    <input id="txtNumeroLote" columnkey="NumeroLote" maxlength="20" type="text" />
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td class="textos_01" style="text-align: right">
                                                                    Tiempo de espejo:
                                                                </td>
                                                                <td class="textos_01" style="width: 14px">
                                                                    <input type="hidden" id="TiempoEspejoH" columnkey="TiempoEspejo" />
                                                                    <input id="TiempoEspejoT" columnkey="TiempoEspejo" type="text" disabled="disabled"
                                                                        class="Decimal" style="width: 150px">
                                                                </td>
                                                                <td class="textos_01" style="text-align: right">
                                                                    Tamaño de Lote
                                                                </td>
                                                                <td class="textos_01">
                                                                    <input id="txtTamanoLote" columnkey="TamanoLote" class="Decimal" type="text" onclick="return txtTamanoLote_onclick()" />
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td class="textos_01" style="text-align: right">
                                                                    Viscosidad:
                                                                </td>
                                                                <td class="textos_01" style="width: 14px">
                                                                    <input type="hidden" id="ViscosidadH" columnkey="Viscosidad" />
                                                                    <input id="ViscosidadT" columnkey="Viscosidad" style="width: 150px;" type="text"
                                                                        disabled="disabled" class="Decimal">
                                                                </td>
                                                                <td class="textos_01" style="text-align: right">
                                                                    Cantidad de Goma:
                                                                </td>
                                                                <td class="textos_01">
                                                                    <input id="txtCantidadGoma" columnkey="CantidadGoma" class="Decimal" type="text" />
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td class="textos_01" style="text-align: right">
                                                                    Densidad:
                                                                </td>
                                                                <td class="textos_01" style="width: 14px">
                                                                    <input type="hidden" id="DensidadH" columnkey="Densidad" />
                                                                    <input id="DensidadT" columnkey="Densidad" style="width: 150px;" type="text" disabled="disabled"
                                                                        class="Decimal">
                                                                </td>
                                                                <td class="textos_01" style="text-align: right">
                                                                    Molino:
                                                                </td>
                                                                <td class="textos_01">
                                                                    <input id="txtMolino" columnkey="Molino" class="Entero" type="text" />
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td class="textos_01" style="text-align: right">
                                                                    Variable 1:
                                                                </td>
                                                                <td class="textos_01" style="width: 14px">
                                                                    <input type="hidden" id="EspesorH" columnkey="Espesor" />
                                                                    <input id="EspesorT" columnkey="Espesor" style="width: 150px;" type="text" disabled="disabled"
                                                                        onclick="return EspesorT_onclick()" class="Decimal">
                                                                </td>
                                                                <td class="textos_01" style="text-align: right">
                                                                    Granulometria:
                                                                </td>
                                                                <td class="textos_01">
                                                                    <input id="txtGranulometria" columnkey="Granulometria" class="Decimal" type="text" />
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td class="textos_01" style="text-align: right">
                                                                    Turno:
                                                                </td>
                                                                <td class="textos_01" style="width: 14px">
                                                                    <input type="hidden" id="hTurno" columnkey="CodigoTurno" />
                                                                    <select id="cbxTurno" columnkey="CodigoTurno" style="width: 150px;">
                                                                    </select>
                                                                </td>
                                                                <td class="textos_01" style="text-align: right">
                                                                    &nbsp;
                                                                </td>
                                                                <td class="textos_01">
                                                                    &nbsp;
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td class="textos_01" style="text-align: right">
                                                                    Color:
                                                                </td>
                                                                <td class="textos_01" style="width: 14px">
                                                                    <input type="hidden" id="hColor" columnkey="CodigoColor" />
                                                                    <select id="cbxColor" columnkey="CodigoColor" style="width: 150px;">
                                                                    </select>
                                                                </td>
                                                                <td class="textos_01" style="text-align: right">
                                                                    &nbsp;
                                                                </td>
                                                                <td class="textos_01">
                                                                    &nbsp;
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td class="textos_01" style="text-align: right">
                                                                    Maquinas:
                                                                </td>
                                                                <td class="textos_01" style="width: 14px">
                                                                    <input type="hidden" id="hListaMaquinas" columnkey="ListaMaquinas" />
                                                                    <select size="4" multiple="multiple" id="cbxMaquina" style="height: 100px; width: 150px;">
                                                                    </select>
                                                                    <a class="tooltip" style="position: absolute;">
                                                                        <img alt="Orientaci&oacute;n" src="../Imagenes/help_icon.gif"/>
                                                                        <span>Para seleccionar varias opciones deje presionada la tecla Ctrl y de click sobre las opciones deseadas.
                                                                        </span></a>
                                                                </td>
                                                                <td class="textos_01" style="text-align: right">
                                                                    &nbsp;
                                                                </td>
                                                                <td class="textos_01">
                                                                    &nbsp;
                                                                </td>
                                                            </tr>
                                                            <input type="hidden" id="AutorizacionH" columnkey="Autorizacion" />
                                                            <caption>
                                                                <input id="AutorizacionT" columnkey="Autorizacion" style="width: 15px; text-align: left;"
                                                                    type="checkbox">
                                                                    <input id="ActivoH" columnkey="Activo" type="hidden" />
                                                                    <input id="ActivoT" columnkey="Activo" disabled="disabled" style="width: 15px; text-align: left;"
                                                                        type="checkbox" class="hidden">
                                                                        <tr>
                                                                            <td align="center" colspan="2">
                                                                                <p align="center">
                                                                                    <input id="GuardarT" class="Boton_01" disabled="disabled" onclick="ok(event)" style="width: 75px;"
                                                                                        type="button" value="Guardar"> </input>
                                                                                    <input id="CancelT" class="Boton_01" onclick="cancel(event)" style="width: 75px;"
                                                                                        type="button" value="Cancelar"> </input>
                                                                                    <input id="AutorizarT" class="Boton_01" onclick="autorizar(event)" style="width: 75px;"
                                                                                        type="button" value="Autorizar"> </input>
                                                                                </p>
                                                                            </td>
                                                                        </tr>
                                                                    </input>
                                                                </input>
                                                                </input>
                                                            </caption>
                                                        </table>
                                                    </RowEditTemplate>
                                                    <RowTemplateStyle Height="380px" Width="550" BackColor="White" BorderColor="White"
                                                        BorderStyle="Ridge">
                                                    </RowTemplateStyle>
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
                                                <FrameStyle Width="800px">
                                                </FrameStyle>
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
                                                <ClientSideEvents BeforeRowTemplateCloseHandler="BeforeRowTemplateCloseHandler" AfterRowTemplateOpenHandler="BeforeRowTemplateOpen"
                                                    DblClickHandler="BeforeRowTemplateOpen" />
                                            </DisplayLayout>
                                        </igtbl:UltraWebGrid>
                                    </td>
                                </tr>
                            </tbody>
                        </table>
                        <input type="hidden" id="hddPlanta" runat="server" />
                        <input type="hidden" id="hddCodCondicionEsmalte" runat="server" />
                        <input type="hidden" id="hddTiempoEspejo" runat="server" />
                        <input type="hidden" id="hddViscosidad" runat="server" />
                        <input type="hidden" id="hddDensidad" runat="server" />
                        <input type="hidden" id="hddEspesor" runat="server" />
                        <input type="hidden" id="hddAutorizacion" runat="server" />
                        <input type="hidden" id="hddActivo" runat="server" />
                        <input type="hidden" id="hddTurno" runat="server" />
                        <input type="hidden" id="hddColor" runat="server" />
                        <input type="hidden" id="hddMaquinas" runat="server" />
                        <input type="hidden" id="hddNumeroLote" runat="server" />
                        <input type="hidden" id="hddTamanoLote" runat="server" />
                        <input type="hidden" id="hddCantidadGoma" runat="server" />
                        <input type="hidden" id="hddMolino" runat="server" />
                        <input type="hidden" id="hddGranulometria" runat="server" />
                        <asp:Button ID="BotonGuardar" runat="server" Text="Button" CssClass="hidden" OnClick="BotonGuardar_click" />
                        <asp:Button ID="BotonAutorizar" runat="server" Text="Button" CssClass="hidden" OnClick="BotonAutorizar_click" />
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
                                  if (idList == 1) {//Nuevo
                                      igtbl_addNew("<%=UltraWebGrid1.ClientID%>", 0).editRow();
                                      SwitchDiv(true);

                                  } else if (idList == 2) {//Exportar
                                      $find('<%=WebDialogWindow1.ClientID%>').set_windowState($IG.DialogWindowState.Normal);
                                  }
                              }



                              function txtTamanoLote_onclick() {

                              }

                              function EspesorT_onclick() {

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
                                                <input id="nombre" value="Configuracion de esmalte" style="width: 200px;" type="text"
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
                </td>
            </tr>
            <tr>
                <td>
                    <igtblexp:UltraWebGridExcelExporter ID="uwgTurnos" WorksheetName="Turnos" DownloadName="Reporte.XLS"
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
