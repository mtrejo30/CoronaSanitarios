<%@ Page Language="C#" MasterPageFile="~/ControlPisoLamosa.Master" Title="Control de piso - Configuración de Alertas"
    AutoEventWireup="true" CodeBehind="ConfiguracionAlertas.aspx.cs" Inherits="LAMOSA.SCPP.Client.View.Administrador.Alertas.ConfiguracionAlertas" %>

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

    <script type="text/javascript">

        function LimpiarForma() {
            document.getElementById("ctl00_Principal_txtAsunto").value = '';
            //document.getElementById("ctl00_Principal_txtClave").value = '';
            document.getElementById("ctl00_Principal_txtMensaje").value = '';
            document.getElementById("ctl00_Principal_txtDestinatario").value = '';
            document.getElementById("ctl00_Principal_txtOperador").value = '';
            document.getElementById("ctl00_Principal_txtLimiteMinimo").value = '';
            document.getElementById("ctl00_Principal_txtLimiteMaximo").value = '';
            document.getElementById("ctl00_Principal_hdnId").value = '0';

            document.getElementById("ctl00_Principal_ddlPlanta").value = '0';
            document.getElementById("ctl00_Principal_ddlColor").value = '0';
            document.getElementById("ctl00_Principal_ddlProceso").value = '0';
            document.getElementById("ctl00_Principal_ddlModelo").value = '0';
            document.getElementById("ctl00_Principal_ddlMaquina").value = '0';
            document.getElementById("ctl00_Principal_ddlTipoDefecto").value = '0';
            document.getElementById("ctl00_Principal_ddlCalidad").value = '0';
            document.getElementById("ctl00_Principal_ddlTurno").value = '0';
            document.getElementById("ctl00_Principal_ddlTipoArticulo").value = '0';
            //document.getElementById("ctl00_Principal_dllFrecuencia").value = '0';

        }

        function validaLlenado() {
            /*
            if (document.getElementById("ctl00_Principal_txtClave").value == ''){
            alert('La clave es un campo requerido');
            return false;
            }*/

            if (document.getElementById("ctl00_Principal_txtAsunto").value == '') {
                alert('El asunto es un campo requerido');
                return false;
            }

            var str = document.getElementById("ctl00_Principal_txtAsunto").value
            str = str.replace(/^(\s|\&nbsp;)*|(\s|\&nbsp;)*$/g, "");

            if (str == '') {
                alert('El asunto es un campo requerido');
                return false;
            }

            if (document.getElementById("ctl00_Principal_txtMensaje").value == '') {
                alert('El mensaje es un campo requerido');
                return false;
            }

            str = document.getElementById("ctl00_Principal_txtMensaje").value
            str = str.replace(/^(\s|\&nbsp;)*|(\s|\&nbsp;)*$/g, "");

            if (str == '') {
                alert('El mensaje es un campo requerido');
                return false;
            }
            /*

                                                if (document.getElementById("ctl00_Principal_txtDestinatario").value == '') {
            alert('Los destinatarios son un campo requerido');
            return false;
            }

                                                if (document.getElementById("ctl00_Principal_txtOperador").value == '') {
            alert('La clave del operador es un campo requerido');
            return false;
            }
                                                
            if (document.getElementById("ctl00_Principal_txtLimiteMinimo").value == '') {
            alert('El Limite minimo es un campo requerido');
            return false;
            }

                                                if (document.getElementById("ctl00_Principal_txtLimiteMinimo").value == '') {
            alert('El Limite maximo es un campo requerido');
            return false;
            }*/

            var re = new RegExp('^[0-9]{1,10}$');
            if (!document.getElementById("ctl00_Principal_txtLimiteMinimo").value.match(re)) {
                alert("Los limites deben ser numeros");
                return false;
            }

            if (!document.getElementById("ctl00_Principal_txtLimiteMaximo").value.match(re)) {
                alert("Los limites deben ser numeros");
                return false;
            }

            re = new RegExp('^[0-9]{0,10}$');
            if (!document.getElementById("ctl00_Principal_txtOperador").value.match(re)) {
                alert("Codigo del operador deben ser un numero");
                return false;
            }

            /*
            if (document.getElementById("ctl00_Principal_ddlPlanta").value = '0') {
            alert('Seleccione una planta válida');
            return false;
            }

                                                if (document.getElementById("ctl00_Principal_ddlColor").value = '0') {
            alert('Seleccione un color válido');
            return false;
            }
            //
            if (document.getElementById("ctl00_Principal_ddlProceso").value = 0) {
            alert('Seleccione un proceso válido');
            return false;
            }
            //
            if (document.getElementById("ctl00_Principal_ddlCT").value = 0) {
            alert('Seleccione un Centro de trabajo válido');
            return false;
            }
            //
                                                
            if (document.getElementById("ctl00_Principal_dllTipoAlerta").value = '0') {
            alert('Seleccione un tipo de alerta válido');
            return false;
            }
            //
            /*
            if (document.getElementById("ctl00_Principal_ddlArea").value = 0) {
            alert('Seleccione un área válida');
            return false;
            }
            //
                                                
            if (document.getElementById("ctl00_Principal_dllFrecuencia").value = '0') {
            alert('Seleccione una frecuencia válida');
            return false;
            }
            //
            /*
            if (document.getElementById("ctl00_Principal_ddlMaquina").value = 0) {
            alert('Seleccione una maquina válida');
            return false;
            }
            //
            if (document.getElementById("ctl00_Principal_ddlCalidad").value = 0) {
            alert('Seleccione una calidad válida');
            return false;
            }
            //
            if (document.getElementById("ctl00_Principal_ddlTurno").value = 0) {
            alert('Seleccione un turno válida');
            return false;
            }
            //
            if (document.getElementById("ctl00_Principal_ddlTipoArticulo").value = 0) {
            alert('Seleccione un tipo de artículo válido');
            return false;
            }
            */
        }
                                            
                                          
    </script>

    <script type="text/javascript">

        function Action(gridID, cellIDb, key) {
            if (key == 46) {
                return true;
            }

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


            if (idList == 1) {//Nuevo


            } else if (idList == 2) {//Exportar

            }
        }

        function obtenerplanta() {
        }

        function abreTemplete() {
            SwitchDiv(false);
        }

        function SwitchDiv(nuevo) {
            if (nuevo) {

                document.getElementById("Button3").className = "hidden";
                document.getElementById("activo").className = "hidden";
                document.getElementById("clave").className = "hidden";

            } else {

                document.getElementById("Button3").className = "Boton_01";
                document.getElementById("activo").className = "";
                document.getElementById("clave").className = "";


            }

        } 
                              
                              
                              
    </script>

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
                    <asp:Label ID="lblTitulo" runat="server" Text="Alertas"></asp:Label><br />
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
                            <%--   <li><a href="javascript:ListItemSelected(1,'')" ID="LAddNew" runat="server"><img src="../Imagenes/Nuevo.png" alt="Nuevo registro" style="border:0px;" /> Nuevo registro</a></li>--%>
                            <li><a href="javascript:LimpiarForma()" id="LNuevo" runat="server">
                                <img src="../Imagenes/Nuevo.png" alt="Exportar tabla" style="border: 0px;" />
                                Nuevo Registro</a></li>
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
                                    <td style="height: 26px" class="textos_01">
                                        <table style="width: 550px">
                                            <tr>
                                                <td class="textos_01">
                                                    Asunto :
                                                </td>
                                                <td class="textos_01" colspan="3">
                                                    <asp:TextBox ID="txtAsunto" runat="server" Width="92%"></asp:TextBox>
                                                </td>
                                                
                                            </tr>
                                            <tr>
                                                <td class="textos_01">
                                                    Mensaje:
                                                </td>
                                                <td class="textos_01" colspan="3">
                                                    <asp:TextBox ID="txtMensaje" TextMode="MultiLine" Width="92%" runat="server"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="textos_01">
                                                    Destinatarios:</td>
                                                <td class="textos_01" colspan="3">
                                                    <asp:TextBox ID="txtDestinatario" runat="server" TextMode="MultiLine" Width="92%" ></asp:TextBox>
                                                    <br />* separados por  '<b> ; </b>'
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="textos_01">
                                                    Planta:
                                                </td>
                                                <td class="textos_01">
                                                    <asp:DropDownList ID="ddlPlanta" runat="server" AutoPostBack="true"
                                                        CssClass="textosd" OnSelectedIndexChanged="ddlPlanta_SelectedIndexChanged">
                                                    </asp:DropDownList>
                                                </td>
                                                <td class="textos_01">
                                                    Color:
                                                </td>
                                                <td class="textos_01">
                                                    <asp:DropDownList ID="ddlColor" runat="server" CssClass="textosd">
                                                    </asp:DropDownList>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="textos_01">
                                                    Proceso:
                                                </td>
                                                <td class="textos_01">
                                                    <asp:DropDownList ID="ddlProceso" runat="server" CssClass="textosd"
                                                        AutoPostBack="true" OnSelectedIndexChanged="ddlProceso_SelectedIndexChanged">
                                                    </asp:DropDownList>
                                                </td>
                                                <td class="textos_01">
                                                    Operador :
                                                </td>
                                                <td class="textos_01">
                                                    <asp:TextBox ID="txtOperador" runat="server"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="textos_01">
                                                    Maquina:
                                                </td>
                                                <td class="textos_01">
                                                    <asp:DropDownList ID="ddlMaquina" runat="server" CssClass="textosd">
                                                    </asp:DropDownList>
                                                </td>
                                                <td class="textos_01">
                                                    Tipo Alerta :
                                                </td>
                                                <td class="textos_01">
                                                    <asp:DropDownList ID="dllTipoAlerta" runat="server" CssClass="textosd">
                                                    </asp:DropDownList>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="textos_01">
                                                    Turno :
                                                </td>
                                                <td class="textos_01">
                                                    <asp:DropDownList ID="ddlTurno" runat="server" CssClass="textosd">
                                                    </asp:DropDownList>
                                                </td>
                                                <td class="textos_01">
                                                    Frecuencia :
                                                </td>
                                                <td class="textos_01">
                                                    <asp:DropDownList ID="dllFrecuencia" runat="server" CssClass="textosd">
                                                    </asp:DropDownList>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="textos_01">
                                                    Tipo Articulo:
                                                </td>
                                                <td class="textos_01">
                                                    <asp:DropDownList ID="ddlTipoArticulo" runat="server" AutoPostBack="true" CssClass="textosd"
                                                        OnSelectedIndexChanged="ddlTipoArticulo_SelectedIndexChanged">
                                                    </asp:DropDownList>
                                                </td>
                                                <td class="textos_01">
                                                    Calidad :
                                                </td>
                                                <td class="textos_01">
                                                    <asp:DropDownList ID="ddlCalidad" runat="server" CssClass="textosd">
                                                    </asp:DropDownList>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="textos_01">
                                                    Modelo :
                                                </td>
                                                <td class="textos_01">
                                                    <asp:DropDownList ID="ddlModelo" runat="server" CssClass="textosd">
                                                    </asp:DropDownList>
                                                </td>
                                                <td class="textos_01">
                                                    Limite Minimo:
                                                </td>
                                                <td class="textos_01">
                                                    <asp:TextBox ID="txtLimiteMinimo" runat="server"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="textos_01">
                                                    Desperdicio :
                                                </td>
                                                <td class="textos_01">
                                                    <asp:DropDownList ID="ddlDesperdicio" runat="server" CssClass="textosd" 
                                                        onselectedindexchanged="ddlDesperdicio_SelectedIndexChanged" AutoPostBack="true">
                                                    </asp:DropDownList>
                                                </td>
                                                <td class="textos_01">
                                                    Limite Maximo:
                                                </td>
                                                <td class="textos_01">
                                                    <asp:TextBox ID="txtLimiteMaximo" runat="server"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="textos_01">
                                                    Tipo Defecto :</td>
                                                <td class="textos_01">
                                                    <asp:DropDownList ID="ddlDefecto" runat="server" CssClass="textosd">
                                                    </asp:DropDownList>
                                                </td>
                                                <td class="textos_01">
                                                    Zona Defecto:</td>
                                                <td class="textos_01">
                                                    <asp:DropDownList ID="ddlZonaDefecto" runat="server" CssClass="textosd">
                                                    </asp:DropDownList>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="textos_01">
                                                </td>
                                                <td class="textos_01">
                                                    <asp:Button ID="btnGuardar" CssClass="Boton_01" runat="server" Text="Guardar" OnClientClick="return validaLlenado();"
                                                        OnClick="btnGuardar_Click" />
                                                </td>
                                                <td class="textos_01">
                                                </td>
                                                <td class="textos_01">
                                                    <asp:HiddenField ID="hdnId" runat="server" />
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <tr>
                                    <td>

                                        <script src="../FuncionesJS/jquery-1.4.2.js" type="text/javascript"></script>

                                    </td>
                                </tr>
                            </tbody>
                        </table>
                        <input type="hidden" id="hddCodTurno" runat="server" />
                        <input type="hidden" id="hddDesTurno" runat="server" />
                        <input type="hidden" id="hddHoraInicio" runat="server" />
                        <input type="hidden" id="hddHoraFin" runat="server" />
                        <input type="hidden" id="hddActivo" runat="server" />
                        <asp:Button ID="BotonGuardar" runat="server" Text="Button" CssClass="hidden" />
                        <asp:Button ID="BotonEliminar" runat="server" Text="Button" CssClass="hidden" />
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
                                                <input id="nombre" value="Turnos" style="width: 200px;" type="text" runat="server" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="width: 200px" align="center" colspan="2">
                                                <asp:Button ID="btnExporta" runat="server" CssClass="Boton_01" Text="Exportar" Width="115px" />
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
                    <igmisc:WebAsyncRefreshPanel ID="WebAsyncRefreshPanel2" runat="server">
                    </igmisc:WebAsyncRefreshPanel>
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
