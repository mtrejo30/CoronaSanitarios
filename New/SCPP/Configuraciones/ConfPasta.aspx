<%@ Page Language="C#" MasterPageFile="~/ControlPisoLamosa.Master" Title="Control de piso - Configuración de pasta"
    AutoEventWireup="true" CodeBehind="ConfPasta.aspx.cs" Inherits="LAMOSA.SCPP.Client.View.Administrador.Configuraciones.ConfPasta" %>

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
                    <asp:Label ID="lblTitulo" runat="server" Text="Configuración de pasta"></asp:Label><br />
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

                    <script src="../FuncionesJS/jquery-1.4.2.js" type="text/javascript"></script>

                    <script src="../FuncionesJS/Validaciones.js" type="text/javascript"></script>

                    <script type="text/javascript">
                        $(function() {
                            var valorAnterior = '';
                            $("#txtPerdidaBrillo").live('keyup', function() {
                                var separador = ':'
                                var patron = new Array(2, 2);
                                val = $(this).val();
                                if (valorAnterior != val) {
                                    largo = val.length
                                    val = val.split(separador)
                                    val2 = ''
                                    for (r = 0; r < val.length; r++) {
                                        val2 += val[r]
                                    }
                                    for (z = 0; z < val2.length; z++) {
                                        if (isNaN(val2.charAt(z))) {
                                            letra = new RegExp(val2.charAt(z), "g")
                                            val2 = val2.replace(letra, "")
                                        }
                                    }
                                    val = ''
                                    val3 = new Array()
                                    for (s = 0; s < patron.length; s++) {
                                        val3[s] = val2.substring(0, patron[s])
                                        val2 = val2.substr(patron[s])
                                    }
                                    for (q = 0; q < val3.length; q++) {
                                        if (q == 0) {
                                            val = val3[q]
                                        }
                                        else {
                                            if (val3[q] != "") {
                                                val += separador + val3[q]
                                            }
                                        }
                                    }
                                    $(this).val(val)
                                    valorAnterior = val
                                }
                            });
							
                            $("#txtPerdidaBrillo").live('blur', function() {
                                if (!/^(0[1-9]|1\d|2[0-3]):([0-5]\d)$/.test($(this).val()))
                                    $(this).val('');
                            });
                        });
                    </script>

                    <igmisc:WebAsyncRefreshPanel ID="WebAsyncRefreshPanel1" runat="server" RefreshComplete="warp_RefreshComplete"
                        InitializePanel="warp_RefreshComplete">
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

                                        <script type="text/javascript">
                                            var beforeClose = true;
                                            var clickcancel = true;
                                            var isValuesChange = false; //Variable que controla si se ha cambiado algun valor del RowEditTemplate al mostrarse

                                            function ok(event) {
                                                var CodCondicionPasta = $("#CodCondicionPastaH").val();
                                                var Fecha = $("#FechaT").val();
                                                var Densidad = $("#DensidadT").val();
                                                var BU = $("#BuT").val();
                                                var Autorizacion = "'" + $("#AutorizacionT").attr('checked') + "'";
                                                var Activo = "'" + $("#ActivoT").attr('checked') + "'";
                                                var baroi = $("#txtBaroi").val();
                                                var turno = $("#cbxTurno").val();
                                                var area = $("#cbxArea").val();

                                                var deposito = $("#txtDeposito").val();
                                                var perdidaBrillo = $("#txtPerdidaBrillo").val();
                                                var viscosidad = $("#txtViscosidad").val();
                                                var codigoProveedor = $("#cbxProveedor").val();

                                                var isAllCaptured = true; ;
                                                $('#templatePasta input:text, #templatePasta SELECT').each(function() {
                                                    if (!$(this).val() || $(this).val() < 1) { isAllCaptured = false; return isAllCaptured; }
                                                })
                                                if (isValuesChange && isAllCaptured) {
                                                    if (confirm('¿Desea guardar cambios?')) {
                                                        //asignar valores a los hidden
                                                        $("#<%=hddCodCondicionPasta.ClientID%>").val(CodCondicionPasta);
                                                        $("#<%=hddDensidad.ClientID%>").val(Densidad);
                                                        $("#<%=hddBu.ClientID%>").val(BU);
                                                        $("#<%=hddAutorizacion.ClientID%>").val(Autorizacion);
                                                        $("#<%=hddActivo.ClientID%>").val(Activo);
                                                        $("#<%=hddTurno.ClientID%>").val(turno);
                                                        $("#<%=hddBaroi.ClientID%>").val(baroi);
                                                        $("#<%=hddArea.ClientID%>").val(area);

                                                        $("#<%=hddDeposito.ClientID%>").val(deposito);
                                                        $("#<%=hddPerdidaBrillo.ClientID%>").val(perdidaBrillo);
                                                        $("#<%=hddViscosidad.ClientID%>").val(viscosidad);
                                                        $("#<%=hddCodigoProveedor.ClientID%>").val(codigoProveedor);

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
                                                var CodCondicionPasta = $("#CodCondicionPastaH").val();
                                                var Autorizacion = "'" + $("#AutorizacionT").attr('checked') + "'";
                                                if (confirm('¿Realmente desea autorizar?')) {
                                                    //asignar valores a los hidden
                                                    $("#<%=hddCodCondicionPasta.ClientID%>").val(CodCondicionPasta);
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
                                                $("#cbxArea").html($("#<%=ddlArea.ClientID %>").html());
                                                $("#cbxProveedor").html($("#<%=ddlProveedor.ClientID %>").html());
                                                $("#cbxTurno option[value=" + $("#hTurno").val() + "]").attr("selected", true);
                                                $("#cbxProveedor option[value=" + $("#hProveedor").val() + "]").attr("selected", true);
                                                var areas = $("#hListaAreas").val();
                                                var lstAreas = areas.split(',')
                                                $("#cbxArea option").attr("selected", "");
                                                for (var i = 0; i < lstAreas.length; i++)
                                                    $("#cbxArea option[value=" + lstAreas[i] + "]").attr("selected", true);
                                                SwitchDiv(false);
                                            }

                                            function SwitchDiv(nuevo) {
                                                $('#templatePasta INPUT, #templatePasta SELECT').each(function() {
                                                    $(this).attr('disabled', (nuevo ? '' : 'disabled'))
                                                })
                                                $('#FechaT').attr('disabled', 'disabled');
                                                $('#CancelT').attr('disabled', '');
                                                $('#AutorizacionT').attr('disabled', '');
                                                $('#cbxArea').attr('disabled', '');
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
                                                //                                                $('#DensidadT, #BuT, #AutorizacionT, #ActivoT, #cbxTurno, #cbxArea, #txtBaroi, #txtDeposito, #txtPerdidaBrillo, txtViscosidad , #cbxProveedor').change(function() {
                                                $('#templatePasta input:text, #templatePasta SELECT').live(
                                                'change', function() {
                                                    isValuesChange = true;
                                                });
                                            });
                                        </script>

                                        <asp:DropDownList ID="ddlTurno" CssClass="hidden" runat="server">
                                        </asp:DropDownList>
                                        <asp:DropDownList ID="ddlArea" CssClass="hidden" runat="server">
                                        </asp:DropDownList>
                                        <asp:DropDownList ID="ddlProveedor" CssClass="hidden" runat="server">
                                        </asp:DropDownList>
                                        <asp:Button ID="btnPlantaChange" CssClass="hidden" runat="server" Text="Button" OnClick="Planta_SelectedIndexChange" />
                                        <igtbl:UltraWebGrid ID="UltraWebGrid1" runat="server" CaptionAlign="Left" EnableAppStyling="False"
                                            OnPageIndexChanged="cambio_pagina">
                                            <Bands>
                                                <igtbl:UltraGridBand>
                                                    <Columns>
                                                        <igtbl:UltraGridColumn Width="90px" BaseColumnName="CodCondicionPasta" IsBound="True"
                                                            Key="CodCondicionPasta" CellMultiline="Yes" Hidden="true">
                                                            <Header Caption="CodCondicionPasta">
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
                                                        <igtbl:UltraGridColumn Width="150px" BaseColumnName="Densidad" IsBound="True" Key="Densidad"
                                                            CellMultiline="Yes">
                                                            <Header Caption="Densidad">
                                                                <RowLayoutColumnInfo OriginX="3" />
                                                            </Header>
                                                            <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                                                            <Footer>
                                                                <RowLayoutColumnInfo OriginX="3" />
                                                            </Footer>
                                                        </igtbl:UltraGridColumn>
                                                        <igtbl:UltraGridColumn Width="90px" BaseColumnName="Bu" IsBound="True" Key="Bu" CellMultiline="No">
                                                            <Header Caption="">
                                                                <RowLayoutColumnInfo OriginX="4" />
                                                            </Header>
                                                            <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                                                            <Footer>
                                                                <RowLayoutColumnInfo OriginX="4" />
                                                            </Footer>
                                                        </igtbl:UltraGridColumn>
                                                        <igtbl:UltraGridColumn Width="90px" BaseColumnName="UsuarioAutoriza" IsBound="True"
                                                            Key="UsuarioAutoriza" CellMultiline="Yes" Hidden="true">
                                                            <Header Caption="UsuarioAutoriza">
                                                                <RowLayoutColumnInfo OriginX="5" />
                                                            </Header>
                                                            <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                                                            <Footer>
                                                                <RowLayoutColumnInfo OriginX="5" />
                                                            </Footer>
                                                        </igtbl:UltraGridColumn>
                                                        <igtbl:UltraGridColumn Width="90px" BaseColumnName="FechaAutorizacion" IsBound="True"
                                                            Key="FechaAutorizacion" CellMultiline="Yes" Hidden="true">
                                                            <Header Caption="FechaAutorizacion">
                                                                <RowLayoutColumnInfo OriginX="6" />
                                                            </Header>
                                                            <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                                                            <Footer>
                                                                <RowLayoutColumnInfo OriginX="6" />
                                                            </Footer>
                                                        </igtbl:UltraGridColumn>
                                                        <igtbl:UltraGridColumn Width="100px" BaseColumnName="Autorizacion" IsBound="True"
                                                            Key="Autorizacion" Type="CheckBox" CellMultiline="no">
                                                            <Header Caption="Autorización">
                                                                <RowLayoutColumnInfo OriginX="7" />
                                                            </Header>
                                                            <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                                                            <CellStyle HorizontalAlign="Center">
                                                            </CellStyle>
                                                            <Footer>
                                                                <RowLayoutColumnInfo OriginX="7" />
                                                            </Footer>
                                                        </igtbl:UltraGridColumn>
                                                        <igtbl:UltraGridColumn Width="100px" BaseColumnName="Activo" IsBound="True" Key="Activo"
                                                            Type="CheckBox" CellMultiline="no">
                                                            <Header Caption="Activo">
                                                                <RowLayoutColumnInfo OriginX="8" />
                                                            </Header>
                                                            <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                                                            <CellStyle HorizontalAlign="Center">
                                                            </CellStyle>
                                                            <Footer>
                                                                <RowLayoutColumnInfo OriginX="8" />
                                                            </Footer>
                                                        </igtbl:UltraGridColumn>
                                                        <igtbl:UltraGridColumn Width="120px" BaseColumnName="ExceptionMessage" IsBound="True"
                                                            Key="ExceptionMessage" CellMultiline="No" Hidden="true">
                                                            <Header Caption="ExceptionMessage">
                                                                <RowLayoutColumnInfo OriginX="9" />
                                                            </Header>
                                                            <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                                                            <Footer>
                                                                <RowLayoutColumnInfo OriginX="9" />
                                                            </Footer>
                                                        </igtbl:UltraGridColumn>
                                                    </Columns>
                                                    <RowEditTemplate>
                                                        <table id="templatePasta" style="font-family: Arial; text-align: center">
                                                            <tr>
                                                                <td class="textos_01" style="text-align: right">
                                                                    <input type="hidden" id="CodCondicionPastaH" columnkey="CodCondicionPasta" />
                                                                    Fecha:
                                                                </td>
                                                                <td class="textos_01">
                                                                    <input type="text" id="FechaT" value="<%#(System.DateTime.Today.ToShortDateString())%>"
                                                                        disabled="disabled" />
                                                                </td>
                                                                <td class="textos_01" style="text-align: right">
                                                                    Deposito:
                                                                </td>
                                                                <td class="textos_01">
                                                                    <input id="txtDeposito" columnkey="Deposito" type="text" class="Entero" />
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td class="textos_01" style="text-align: right">
                                                                    Densidad:
                                                                </td>
                                                                <td class="textos_01">
                                                                    <input type="hidden" id="DensidadH" columnkey="densidad" />
                                                                    <input id="DensidadT" columnkey="densidad" style="width: 150px;" type="text" disabled="disabled"
                                                                        class="Decimal">
                                                                </td>
                                                                <td class="textos_01" style="text-align: right">
                                                                    Perdida de Brillo (HH:mm):
                                                                </td>
                                                                <td class="textos_01">
                                                                    <input id="txtPerdidaBrillo" columnkey="PerdidaBrillo" type="text" />
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td class="textos_01" style="text-align: right">
                                                                    BU:
                                                                </td>
                                                                <td class="textos_01">
                                                                    <input type="hidden" id="BuH" columnkey="Bu" />
                                                                    <input id="BuT" columnkey="Bu" style="width: 150px;" type="text" disabled="disabled"
                                                                        class="Decimal" />
                                                                </td>
                                                                <td class="textos_01" style="text-align: right">
                                                                    Viscosidad:
                                                                </td>
                                                                <td class="textos_01">
                                                                    <input id="txtViscosidad" columnkey="Viscosidad" type="text" class="Entero" />
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td class="textos_01" style="text-align: right">
                                                                    Baroi:
                                                                </td>
                                                                <td class="textos_01">
                                                                    <input id="txtBaroi" columnkey="CodigoBaroi" class="Entero" style="width: 150px;"
                                                                        type="text" disabled="disabled" />
                                                                </td>
                                                                <td class="textos_01" style="text-align: right">
                                                                    Clave de Proveedor:
                                                                </td>
                                                                <td class="textos_01">
                                                                    <input type="hidden" id="hProveedor" columnkey="CodigoProveedor" />
                                                                    <select id="cbxProveedor" columnkey="CodigoProveedor">
                                                                    </select>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td class="textos_01" style="text-align: right">
                                                                    Turno:
                                                                </td>
                                                                <td class="textos_01">
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
                                                                    Area:
                                                                </td>
                                                                <td class="textos_01">
                                                                    <input type="hidden" id="hListaAreas" columnkey="ListaAreas" />
                                                                    <select size="4" multiple="multiple" id="cbxArea" style="height: 100px; width: 150px;">
                                                                    </select>
                                                                    <a class="tooltip" style="position: absolute;">
                                                                        <img alt="Orientaci&oacute;n" src="../Imagenes/help_icon.gif" />
                                                                        <span>Para seleccionar varias opciones deje presionada la tecla Ctrl y de click sobre
                                                                            las opciones deseadas. </span></a>
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
                                                                        type="checkbox" class="hidden" />
                                                                    <tr>
                                                                        <td align="center" colspan="4">
                                                                            <input id="GuardarT" class="Boton_01" onclick="ok(event)" style="width: 75px;" type="button"
                                                                                value="Guardar"> </input>
                                                                            <input id="CancelT" class="Boton_01" onclick="cancel(event)" style="width: 75px;"
                                                                                type="button" value="Cancelar"> </input>
                                                                            <input id="AutorizarT" class="Boton_01" onclick="autorizar(event)" style="width: 75px;"
                                                                                type="button" value="Autorizar"> </input>
                                                                        </td>
                                                                        <td align="center">
                                                                            &nbsp;
                                                                        </td>
                                                                        <td align="center">
                                                                            &nbsp;
                                                                        </td>
                                                                    </tr>
                                                                </input>
                                                                </input> </input>
                                                            </caption>
                                                        </table>
                                                    </RowEditTemplate>
                                                    <RowTemplateStyle Height="310px" Width="500" BackColor="White" BorderColor="White"
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
                        <input type="hidden" id="hddCodCondicionPasta" runat="server" />
                        <input type="hidden" id="hddDensidad" runat="server" />
                        <input type="hidden" id="hddBu" runat="server" />
                        <input type="hidden" id="hddAutorizacion" runat="server" />
                        <input type="hidden" id="hddActivo" runat="server" />
                        <input type="hidden" id="hddTurno" runat="server" />
                        <input type="hidden" id="hddBaroi" runat="server" />
                        <input type="hidden" id="hddArea" runat="server" />
                        <input type="hidden" id="hddDeposito" runat="server" />
                        <input type="hidden" id="hddPerdidaBrillo" runat="server" />
                        <input type="hidden" id="hddViscosidad" runat="server" />
                        <input type="hidden" id="hddCodigoProveedor" runat="server" />
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
                                                <input id="nombre" value="Configuracion de pasta" style="width: 200px;" type="text"
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
