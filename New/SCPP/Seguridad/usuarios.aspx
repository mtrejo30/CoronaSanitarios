<%@ Page Language="C#" MasterPageFile="~/ControlPisoLamosa.Master" AutoEventWireup="true"
    CodeBehind="usuarios.aspx.cs" Inherits="LAMOSA.SCPP.Client.View.Administrador.Seguridad.usuarios"
    Title="Control de piso - Usuarios" %>

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
                    <asp:Label ID="lblTitulo" runat="server" Text="Usuarios"></asp:Label><br />
                </td>
            </tr>
            <tr>
                <td style="height: 10px" colspan="3"></td>
            </tr>
            <tr>
                <td style="width: 10px;" rowspan="2"> </td>
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
                        <table style="height: 170px; width: 600px">
                            <tbody>
                                <tr>
                                    <td style="height: 40px" class="textos_01">
                                        <table style="width: 550px">
                                            <tr>
                                                <td class="textos_01">
                                                    Planta:
                                                </td>
                                                <td class="textos_01">
                                                    <asp:DropDownList ID="cmbPlanta2" runat="server" CssClass="textosd" AutoPostBack="true"
                                                        OnSelectedIndexChanged="cmbPlanta_SelectedIndexChanged"  >
                                                    </asp:DropDownList>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="textos_01">
                                                    Rol de usuario:
                                                </td>
                                                <td class="textos_01">
                                                    <asp:DropDownList ID="cmbRol" runat="server" CssClass="textosd" AutoPostBack="true"
                                                        OnSelectedIndexChanged="cmbRol_SelectedIndexChanged">
                                                    </asp:DropDownList>
                                                    <asp:DropDownList  CssClass="hidden" ID="cmbRol2" runat="server">
                                                    </asp:DropDownList>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="textos_01">
                                                    Usuario:
                                                </td>
                                                <td class="textos_01">
                                                    <asp:TextBox ID="txtUsuario" runat="server" CssClass="textost"></asp:TextBox>
                                                </td>
                                                <td class="textos_01" colspan="2">
                                                    <asp:Button ID="Button4" runat="server" CssClass="Boton_01" OnClick="btnLlenaGrid_Click"
                                                        Text="Buscar" />
                                                        <asp:Button ID="BotonEliminar" runat="server" Text="Button" CssClass="hidden" OnClick="BotonEliminar_click" />
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <tr>
                                    <td>

                                        <script src="../FuncionesJS/jquery-1.4.2.js" type="text/javascript"></script>

                                        <script type="text/javascript">
                                            var beforeClose = true;
                                            var clickcancel = true;
                                            targetModal = 1; //1=empleado, 2=supervisor
                                            var controlSeleccionaName = "";
                                            function selecciona(codemp, codempleado, nombre) {
                                                if (targetModal == 1) {
                                                    $("#CodEmpleadoN").val(codemp);
                                                    $("#CodEmpleadoT").val(codempleado);
                                                    $("#NombreT").val(nombre);
                                                }
                                                else if (targetModal == 2) {
                                                    $("#SupervisorN").val(codemp);
                                                    $("#SupervisorT").val(codempleado);
                                                }
                                                    $find('<%=WebDialogWindow3.ClientID%>').set_windowState($IG.DialogWindowState.Hidden);
                                                    $find('<%=WebDialogWindow4.ClientID%>').set_windowState($IG.DialogWindowState.Hidden);
                                              
                                            }
                                            function ok(event) {
                                                var NombreUsuario = $("#NombreUsuarioT").val();
                                                var Contrasena = $("#ContrasenaT").val();
                                                var Confirmar = $("#ConfirmarT").val();
                                                var CodEmpleado = $("#CodEmpleadoN").val();
                                                var CodRol = $("#ddlRol").val();
                                                var CodSupervisor = $("#SupervisorN").val();
                                                var Activo = "'" + $("#ActivoT").attr('checked') + "'";
                                                var Bloqueado = "'" + $("#BloqueadoT").attr('checked') + "'";
                                                var Email = $("#CorreoT").val()
                                                var CodUsuario = $("#codUsuarioH").val();
                                                //Contrasena != "" && Confirmar != "" && 
                                                if (NombreUsuario != "" && CodEmpleado != "" && CodRol >= 1 && CodSupervisor != "" && Email != "") {
                                                    if (confirm('¿Desea guardar cambios?')) {
                                                        //asignar valores a los hidden
                                                        $("#<%=hddNombreUsuario.ClientID%>").val(NombreUsuario);
                                                        //$("#<%=hddContrasena.ClientID%>").val(Contrasena);
                                                        $("#<%=hddCodEmpleado.ClientID%>").val(CodEmpleado);
                                                        $("#<%=hddCodRol.ClientID%>").val(CodRol);
                                                        $("#<%=hddSupervisor.ClientID%>").val(CodSupervisor);
                                                        $("#<%=hddBloqueado.ClientID%>").val(Bloqueado);
                                                        $("#<%=hddCorreo.ClientID%>").val(Email);
                                                        $("#<%=hddCodUsuario.ClientID%>").val(CodUsuario);
                                                        //enviar al server
                                                        $("#<%=BotonGuardar.ClientID%>").click();
                                                        clickcancel = false;
                                                        igtbl_gRowEditButtonClick(event);
                                                        clickcancel = true;
                                                        beforeClose = true;
                                                    }
                                                }
                                                else alert('Información incompleta para poder guardar el registro!');
                                            }

                                            function cancel(event) {
                                                var NombreUsuario = $("#NombreUsuarioT").val();
                                                var Contrasena = $("#ContrasenaT").val();
                                                var CodEmpleado = $("#CodEmpleadoN").val();
                                                var CodRol = $("#ddlRol").val();
                                                var CodSupervisor = $("#SupervisorN").val();
                                                var Bloqueado = "'" + $("#BloqueadoT").attr('checked') + "'";
                                                var CodUsuario = $("#codUsuarioH").val();
                                                var Email = $("#CorreoT").val();

                                                var oNombreUsuario = $("#NombreUsuarioH").val();
                                                var oCodRol = $("#DesRolH").val();
                                                var oCodSupervisor = $("#SupervisorH").val();
                                                var oBloqueado = $("#BloqueadoH").val();
                                                var oEmail = $("#CorreoH").val();


                                                var edit = true;
                                                beforeClose = false;
                                                if (NombreUsuario != oNombreUsuario || CodRol != oCodRol || CodSupervisor != oCodSupervisor || Bloqueado != oBloqueado || Email != oEmail) {
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

                                                var ClaveTurno = $("#codUsuarioH").val();
                                            
                                                if (confirm('¿Desea eliminar registro?')) {
                                                    $("#<%=hddCodUsuario.ClientID%>").val(ClaveTurno);

                                                    $("#<%=BotonEliminar.ClientID%>").click();
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
                                            function openModalWin(idTarget) {
                                                targetModal = idTarget;
                                                if (idTarget == 1)
                                                    $find('<%=WebDialogWindow3.ClientID%>').set_windowState($IG.DialogWindowState.Normal);
                                                else
                                                    $find('<%=WebDialogWindow4.ClientID%>').set_windowState($IG.DialogWindowState.Normal);
                                            }


                                            function BeforeRowTemplateOpen(gridName, rowId) {
                                                SwitchDiv(false);
                                                
                                                $("#ddlRol").html($("#<%=cmbRol2.ClientID %>").html());

                                            }

                                            function AfterRowTemplateOpen(gridName, rowId) {
                                                var grid = igtbl_getGridById(gridName);
                                                var indexRow = String(rowId);
                                                indexRow = indexRow.substring(indexRow.lastIndexOf('_') + 1, indexRow.length);
                                                var CodigoUsuario = grid.Rows.getRow(indexRow).getCell(0).getValue();
                                                var CuentaUsuario = grid.Rows.getRow(indexRow).getCell(8).getValue();
                                                $("#<%=hddCodUsuario.ClientID%>").val(CodigoUsuario);
                                                $("#<%=hideCuentaUsuario.ClientID%>").val(CuentaUsuario);
                                                var r = $("#DesRolH").val() ? $("#DesRolH").val() : $("#ddlRol").val();
                                            }

                                            function fnResetearPassword(ctrl) 
                                            {
                                                $("#<%=hideResetearContrasena.ClientID%>").val(1);
                                                ctrl.click();
                                            }
                                            function fnDesbloquearUsuario(ctrl) 
                                            {
                                                $("#<%=hideDesbloquearUsuario.ClientID%>").val(1);
                                                ctrl.click();
                                            }

                                            function correo(event) {
                                                var Email = $("#CorreoT").val();

                                                if (/^\w+([\.-]?\w+)*@\w+([\.-]?\w+)*(\.\w{2,3,4})+$/.test(Email)) {

                                                    alert("La dirección de email " + Email + " es correcta.");

                                                } else {

                                                    alert("La dirección de email es incorrecta.");

                                                }

                                            }
                                        </script>

                                        <igtbl:UltraWebGrid ID="UltraWebGrid1" runat="server" CaptionAlign="Left" EnableAppStyling="False"
                                            OnPageIndexChanged="cambio_pagina" >
                                            <Bands>
                                                <igtbl:UltraGridBand>
                                                    <Columns>
                                                        <igtbl:UltraGridColumn Width="110px" BaseColumnName="codUsuario" IsBound="true" Key="codUsuario"
                                                            CellMultiline="No" Hidden="true">
                                                            <Header Caption="Codigo Usuario">
                                                                <RowLayoutColumnInfo OriginX="1" />
                                                            </Header>
                                                            <HeaderStyle HorizontalAlign="Center" />
                                                            <Footer>
                                                                <RowLayoutColumnInfo OriginX="1" />
                                                            </Footer>
                                                        </igtbl:UltraGridColumn>
                                                        <igtbl:UltraGridColumn Width="110px" BaseColumnName="CodEmpleado" IsBound="true"
                                                            Key="CodEmpleado" CellMultiline="No">
                                                            <Header Caption="Clave empleado">
                                                                <RowLayoutColumnInfo OriginX="2" />
                                                            </Header>
                                                            <HeaderStyle HorizontalAlign="Center" />
                                                            <Footer>
                                                                <RowLayoutColumnInfo OriginX="2" />
                                                            </Footer>
                                                        </igtbl:UltraGridColumn>
                                                        <igtbl:UltraGridColumn Width="210px" BaseColumnName="Nombre" IsBound="true" Key="Nombre"
                                                            CellMultiline="No">
                                                            <Header Caption="Nombre">
                                                                <RowLayoutColumnInfo OriginX="3" />
                                                            </Header>
                                                            <HeaderStyle HorizontalAlign="Center" />
                                                            <Footer>
                                                                <RowLayoutColumnInfo OriginX="3" />
                                                            </Footer>
                                                        </igtbl:UltraGridColumn>
                                                        <igtbl:UltraGridColumn Width="120px" BaseColumnName="DesRol" IsBound="True" Key="DesRol"
                                                            CellMultiline="Yes">
                                                            <Header Caption="Rol de usuario">
                                                                <RowLayoutColumnInfo OriginX="6" />
                                                            </Header>
                                                            <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                                                            <Footer>
                                                                <RowLayoutColumnInfo OriginX="6" />
                                                            </Footer>
                                                        </igtbl:UltraGridColumn>
                                                        <igtbl:UltraGridColumn Width="90px" BaseColumnName="NombreUsuario" IsBound="True"
                                                            Key="NombreUsuario" CellMultiline="Yes">
                                                            <Header Caption="Usuario">
                                                                <RowLayoutColumnInfo OriginX="7" />
                                                            </Header>
                                                            <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                                                            <Footer>
                                                                <RowLayoutColumnInfo OriginX="7" />
                                                            </Footer>
                                                        </igtbl:UltraGridColumn>
                                                        <igtbl:UltraGridColumn Width="90px" BaseColumnName="Activo" IsBound="True" Key="Activo"
                                                            Type="CheckBox" CellMultiline="no">
                                                            <Header Caption="Activo">
                                                                <RowLayoutColumnInfo OriginX="9" />
                                                            </Header>
                                                            <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                                                            <CellStyle HorizontalAlign="Center">
                                                            </CellStyle>
                                                            <Footer>
                                                                <RowLayoutColumnInfo OriginX="9" />
                                                            </Footer>
                                                        </igtbl:UltraGridColumn>
                                                        <igtbl:UltraGridColumn Width="90px" BaseColumnName="Bloqueado" IsBound="True" Key="Bloqueado"
                                                            Type="CheckBox" CellMultiline="no">
                                                            <Header Caption="Bloqueado">
                                                                <RowLayoutColumnInfo OriginX="10" />
                                                            </Header>
                                                            <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                                                            <CellStyle HorizontalAlign="Center">
                                                            </CellStyle>
                                                            <Footer>
                                                                <RowLayoutColumnInfo OriginX="10" />
                                                            </Footer>
                                                        </igtbl:UltraGridColumn>
                                                        <igtbl:UltraGridColumn Hidden="true" Width="70px" BaseColumnName="FechaRegistro"
                                                            IsBound="True" Key="FechaRegistro" DataType="System.DateTime" CellMultiline="No">
                                                            <Header Caption="Fecha">
                                                                <RowLayoutColumnInfo OriginX="11" />
                                                            </Header>
                                                            <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                                                            <CellStyle HorizontalAlign="Center">
                                                            </CellStyle>
                                                            <Footer>
                                                                <RowLayoutColumnInfo OriginX="7" />
                                                            </Footer>
                                                        </igtbl:UltraGridColumn>
                                                        <igtbl:UltraGridColumn Hidden="true" Width="70px" BaseColumnName="Email" IsBound="True"
                                                            Key="Email" CellMultiline="No">
                                                            <Header Caption="Email">
                                                                <RowLayoutColumnInfo OriginX="11" />
                                                            </Header>
                                                            <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                                                            <CellStyle HorizontalAlign="Center">
                                                            </CellStyle>
                                                            <Footer>
                                                                <RowLayoutColumnInfo OriginX="8" />
                                                            </Footer>
                                                        </igtbl:UltraGridColumn>
                                                        <igtbl:UltraGridColumn Hidden="true" Width="70px" BaseColumnName="Contrasena" IsBound="True"
                                                            Key="Contrasena" CellMultiline="No">
                                                            <Header Caption="Contraseña">
                                                                <RowLayoutColumnInfo OriginX="11" />
                                                            </Header>
                                                            <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                                                            <CellStyle HorizontalAlign="Center">
                                                            </CellStyle>
                                                            <Footer>
                                                                <RowLayoutColumnInfo OriginX="9" />
                                                            </Footer>
                                                        </igtbl:UltraGridColumn>
                                                        <igtbl:UltraGridColumn Width="110px" Hidden="true" BaseColumnName="CodSupervisor"
                                                            IsBound="true" Key="CodSupervisor" CellMultiline="No">
                                                            <Header Caption="CodSupervisor">
                                                                <RowLayoutColumnInfo OriginX="2" />
                                                            </Header>
                                                            <HeaderStyle HorizontalAlign="Center" />
                                                            <Footer>
                                                                <RowLayoutColumnInfo OriginX="2" />
                                                            </Footer>
                                                        </igtbl:UltraGridColumn>
                                                    </Columns>
                                                    <RowEditTemplate>
                                                       <table style="font-family: Arial">
                                                            <tr>
                                                                <td class="textos_01" style=" text-align:left">Número de empleado:</td>
                                                                <td align="left"><input id="CodEmpleadoT" type="text" readonly="readonly" disabled="disabled" class="mayus" columnkey="CodEmpleado"/></td><!-- -->
                                                                <td class="textos_01"><input id="SeleccionaEmp" type="button" class="Boton_01" onclick="openModalWin(1)" value="Seleccionar"/></td>
                                                                <td align="left"><input id="CodEmpleadoN" type="text" readonly="readonly" disabled="disabled" class="hidden" columnkey="CodEmpleado"/></td><!---->
                                                                <td><input type="hidden" id="codUsuarioH" columnkey="codUsuario"/></td><!---->
                                                                <td colspan ="2"><input type="hidden" id="CodEmpleadoH" columnkey="CodEmpleado"/></td><!---->
                                                            </tr>
                                                            <tr>
                                                                <td class="textos_01">Nombre:</td>
                                                                <td><input id="NombreT" type="text" readonly="readonly" class="mayus" columnkey="Nombre" /></td><!--columnkey="Nombre"-->
                                                                <td><input type="hidden" id="NombreH" columnkey="Nombre"/></td><!--columnkey="Nombre" -->
                                                                <td colspan="4"></td>
                                                            </tr>
                                                            <tr>
                                                                <td class="textos_01">Correo:</td>
                                                                <td class="textos_01"><input id="CorreoT" type="text" columnkey="Email" /></td><!--columnkey="Email"-->
                                                                <td class="textos_01" style=" text-align:right">Bloqueado:</td>
                                                                <td><input id="BloqueadoT" type="checkbox" columnkey="Bloqueado"></td><!-- -->
                                                                <td><input type="hidden" id="BloqueadoH" columnkey="Bloqueado"/></td><!-- -->
                                                                <td><input type="hidden" id="CorreoH" columnkey="Email"/></td><!--columnkey="Email"-->
                                                                <td>
                                                                <table>
                                                                <tr>
                                                                     <td class="textos_01"><input id="ActivoT" class="hidden" style="text-align: left;" type="checkbox" checked="checked" columnkey="Activo"/></td><!-- -->
                                                                     <td><input type="hidden" id="ActivoH" columnkey="Activo"/></td><!-- -->
                                                                 </tr>
                                                                </table>
                                                                </td>
                                                            </tr>
                                                           
                                                           <tr>
                                                                <td class="textos_01">Supervisor:</td>
                                                                <td><input id="SupervisorT"  type="text" readonly="readonly" class="mayus" disabled="disabled" columnkey="CodSupervisor"/></td><!-- -->
                                                                <td class="textos_01" ><input id="SeleccionaSup" type="button" class="Boton_01" onclick="openModalWin(2)" value="Seleccionar" /></td>
                                                                <td><input id="SupervisorN" type="text" readonly="readonly" class="hidden" disabled="disabled" columnkey="CodSupervisor"/></td><!---->
                                                                <td colspan="3"><input type="hidden" id="SupervisorH" columnkey="CodSupervisor"/></td><!-- -->
                                                            </tr>
                                                            <tr>
                                                                <td class="textos_01">Rol de usuario:</td>
                                                                <td class="textos_01" colspan ="5"><select  id="ddlRol" columnkey="CodRol"></select></td><!---->
                                                            </tr>
                                                            <tr>
                                                                <td class="textos_01">Usuario:</td>
                                                                <td class="textos_01"><input id="NombreUsuarioT" type="text" maxlength="10" columnkey="NombreUsuario"></td><!-- -->
                                                                <td><input id="btnDesbloquearUsuario" class="Boton_01" type="submit" value="Desbloquear" onclick="fnDesbloquearUsuario(this);" /></td>
                                                                <td>
                                                                <table>
                                                                <tr>
                                                                <td><asp:Label ID="lblContrasena" runat="server" Text="Contraseña"></asp:Label></td>
                                                                <td><input id="btnResetearContrasena" class="Boton_01" type="submit" value="Reiniciar" onclick = "fnResetearPassword(this);" /></td>
                                                                </tr>
                                                                </table>
                                                                </td>
                                                                <td><input type="hidden" id="DesRolH" columnkey="CodRol"/></td><!---->
                                                                <td><input type="hidden" id="NombreUsuarioH" columnkey="NombreUsuario"/></td><!---->
                                                                <td><input type="hidden" id="ContrasenaHH" columnkey="Contrasena"/></td><!-- -->
                                                            </tr>
                                                            <tr id="prueba" class="hidden">
                                                                <td class="textos_01"><!--Contraseña:--></td>
                                                                <td><asp:TextBox ID="ContrasenaT"  runat="server" MaxLength="10" TextMode="Password" ValidationGroup="grpUs" value="Lamosa06" Visible="false"></asp:TextBox></td><!--columnkey="Contrasena"-->
                                                                <td class="textos_01"><!--Confirmar:--></td>
                                                                <td><asp:TextBox ID="ConfirmarT" runat="server" MaxLength="10" TextMode="Password" ValidationGroup="grpUs" Visible ="False"></asp:TextBox></td><!--columnkey="Contrasena"-->
                                                                <td><asp:CompareValidator ID="cvValidarContrasenias" runat="server" ControlToCompare="ConfirmarT" ControlToValidate="" ValidationGroup="grpUs" Visible="False"></asp:CompareValidator><!--ContrasenaT--></td>
                                                                <td><input type="hidden" id="ConfirmarH" value ="Lamosa06" /></td><!-- columnkey="Contrasena"-->
                                                                <td><input type="hidden" id="ContrasenaH" value ="Lamosa06" /></td><!--columnkey="Contrasena"-->
                                                            </tr>
                                                            <tr>
                                                           <td class="textos_01" colspan ="7">
                                                           <table>
                                                           <tr>
                                                           <td><asp:Label ID="lblUsuario" runat="server" Text="Usuario" Visible="false"></asp:Label></td>
                                                           </tr>
                                                           </table>
                                                           </td>
                                                           </tr>
                                                            <tr>
                                                                <td align="center"><p align="center"><input id="Button1" onclick="ok(event)" class="Boton_01" style="width: 75px;" type="button" value="Guardar"></input></p></td>
                                                                <td align="center"><p align="center"><input id="Button2" onclick="cancel(event)" class="Boton_01" style="width: 75px;" type="button" value="Cancelar"></input></p></td>
                                                                <td align="center"><p align="center"><input id="Button3" onclick="eliminar(event)" class="Boton_01" style="width: 75px;" type="button" value="Eliminar"></input></p></td>
                                                                <td colspan ="4"></td>
                                                            </tr>
                                                        </table>
                                                    </RowEditTemplate>
                                                    <RowTemplateStyle Height="300" Width="650" BackColor="White" BorderColor="White" BorderStyle="Ridge">
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
                        <asp:HiddenField ID="hddNombreUsuario" runat="server" />
                        <asp:HiddenField ID="hddContrasena" runat="server" />
                        <asp:HiddenField ID="hddCodEmpleado" runat="server" />
                        <asp:HiddenField ID="hddCodRol" runat="server" />
                        <asp:HiddenField ID="hddSupervisor" runat="server" />
                        <asp:HiddenField ID="hddBloqueado" runat="server" />
                        <asp:HiddenField ID="hddCorreo" runat="server" />
                        <asp:HiddenField ID="hddCodUsuario" runat="server" />
                        <asp:HiddenField ID="hideCuentaUsuario" runat="server" />
                        <asp:HiddenField ID="hideResetearContrasena" runat="server" Value="0" />
                        <asp:HiddenField ID="hideDesbloquearUsuario" runat="server" Value="0" />
                        <asp:Button ID="BotonGuardar" runat="server" Text="Button" 
                            OnClick="BotonGuardar_click" BackColor="White" BorderColor="White" Height="0px" 
                            Width="0px" />
                    </igmisc:WebAsyncRefreshPanel>

                    <script type="text/javascript">

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





                        function SwitchDiv(nuevo) {


                            if (nuevo) {


                                document.getElementById("prueba").className = ""
                                document.getElementById("Button3").className = "hidden";

                            } else {

                                document.getElementById("prueba").className = "hidden"
                                document.getElementById("Button3").className = "Boton_01";
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
                                                <input id="nombre" value="Usuarios" style="width: 200px;" type="text"
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
                    <ig:WebDialogWindow ID="WebDialogWindow2" runat="server" InitialLocation="Centered"
                        Height="200px" Width="500px" Modal="true" WindowState="Hidden" Font-Size="10px">
                        <ContentPane BackColor="#FAFAFA">
                            <Template>
                                <div style="padding: 5px;">
                                    <igtbl:UltraWebGrid ID="UltraWebGrid2" runat="server" CaptionAlign="Left" EnableAppStyling="false">
                                        <Bands>
                                            <igtbl:UltraGridBand>
                                                <Columns>
                                                    <igtbl:UltraGridColumn Width="150px" BaseColumnName="descripcion" IsBound="True"
                                                        Key="descripcion" CellMultiline="Yes">
                                                        <Header Caption="Descripción">
                                                            <RowLayoutColumnInfo OriginX="1" />
                                                        </Header>
                                                        <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                                                        <Footer>
                                                            <RowLayoutColumnInfo OriginX="1" />
                                                        </Footer>
                                                    </igtbl:UltraGridColumn>
                                                    <igtbl:UltraGridColumn Width="90px" BaseColumnName="Acceso" IsBound="True" Key="Id_acceso"
                                                        CellMultiline="No">
                                                        <Header Caption="Acceso">
                                                            <RowLayoutColumnInfo OriginX="2" />
                                                        </Header>
                                                        <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                                                        <Footer>
                                                            <RowLayoutColumnInfo OriginX="2" />
                                                        </Footer>
                                                    </igtbl:UltraGridColumn>
                                                </Columns>
                                                <RowEditTemplate>
                                                    <table style="font-family: Arial; text-align: left">
                                                        <tr>
                                                            <td class="textos_01">
                                                                Planta 1
                                                            </td>
                                                            <td class="textos_01">
                                                                <asp:CheckBox ID="CheckBox3" runat="server" />
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td class="textos_01">
                                                                Planta 2
                                                            </td>
                                                            <td class="textos_01">
                                                                <asp:CheckBox ID="CheckBox4" runat="server" />
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td class="textos_01">
                                                                Planta 3
                                                            </td>
                                                            <td class="textos_01">
                                                                <asp:CheckBox ID="CheckBox5" runat="server" />
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td class="textos_01">
                                                                Bentito Juarez
                                                            </td>
                                                            <td class="textos_01">
                                                                <asp:CheckBox ID="CheckBox6" runat="server" />
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </RowEditTemplate>
                                                <RowTemplateStyle Height="200px" BackColor="White" BorderColor="White" BorderStyle="Ridge">
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
                                            <ClientSideEvents BeforeRowTemplateCloseHandler="BeforeRowTemplateCloseHandler" />
                                        </DisplayLayout>
                                    </igtbl:UltraWebGrid>
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
                                                                        <asp:Literal ID="literal1" runat="server" Text='<%# "<a href=\"#\" onclick=\"selecciona("+ comilla + Eval("CodEmpleado")+ comilla +","+ comilla + Eval("CodEmpleadoMFG")+ comilla +","+ comilla + Eval("NombreCompleto")+ comilla +");\" >" + Eval("NombreCompleto") + "</a>" %>'></asp:Literal>
                                                                   
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
                    <ig:WebDialogWindow ID="WebDialogWindow4" runat="server" InitialLocation="Centered"
                        Height="300px" Width="500px" Modal="true" WindowState="Hidden" Font-Size="10px">
                        <ContentPane BackColor="#FAFAFA">
                            <Template>
                                <igmisc:WebAsyncRefreshPanel runat="server" ID="WebAsyncRefreshPanel2">
                                    <div>
                                        <table border="0">
                                            <tbody>
                                                <tr>
                                                    <td class="textos_01">
                                                        Número empleado:
                                                    </td>
                                                    <td class="textos_01">
                                                        <asp:TextBox ID="NumSup" runat="server" CssClass="textost" Width="250px"></asp:TextBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="textos_01">
                                                        Nombre Supervisor:
                                                    </td>
                                                    <td class="textos_01">
                                                        <asp:TextBox ID="NomSup" runat="server" CssClass="textost" Width="250px"></asp:TextBox>
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
                                                                    <asp:Button ID="BuscarSup" runat="server" CssClass="Boton_01" OnClick="btnLlenaGridSup_Click"
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
                                                        <asp:GridView ID="GridView2" runat="server" AutoGenerateColumns="False" BackColor="White"
                                                            BorderColor="#CC9966" BorderStyle="None" BorderWidth="1px" CellPadding="4">
                                                            <Columns>
                                                                <asp:BoundField DataField="CodEmpleadoMFG" HeaderText="Clave" />
                                                                <asp:TemplateField HeaderText="Nombre Completo">
                                                                    <ItemTemplate>
                                                                      
                                                                     <asp:Literal ID="literal1" runat="server" Text='<%# "<a href=\"#\" onclick=\"selecciona("+ comilla + Eval("CodEmpleado")+ comilla +","+ comilla + Eval("CodEmpleadoMFG")+ comilla +","+ comilla + Eval("NombreCompleto")+ comilla +");\" >" + Eval("NombreCompleto") + "</a>" %>'></asp:Literal>
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
                                                        <asp:Button ID="btnAceptar2" runat="server" Text="Aceptar" CssClass="Boton_01" Width="140px"
                                                            ValidationGroup="grpUs" ToolTip="Guarda la información." />
                                                    </td>
                                                    <td style="width: 160px">
                                                        <asp:Button ID="btnCancelar2" runat="server" Text="Cancelar" CssClass="Boton_01"
                                                            OnClick="btnCancelar_Click" Width="140px" CausesValidation="False" ToolTip="Limpia la información de los campos." />
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
