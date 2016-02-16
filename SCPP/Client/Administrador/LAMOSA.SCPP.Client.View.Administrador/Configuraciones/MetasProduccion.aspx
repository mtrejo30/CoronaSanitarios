<%@ Page Language="C#" MasterPageFile="~/ControlPisoLamosa.Master" Title="Control de piso - Metas de Producción" AutoEventWireup="true" CodeBehind="MetasProduccion.aspx.cs" 
Inherits="LAMOSA.SCPP.Client.View.Administrador.Configuraciones.MetasProduccion" %>


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
        <table align="center" border="0" cellpadding="0" cellspacing="0" width="1024px" >
            <tr>
                <td style="height:10px" colspan= "3"></td>
            </tr>
            <tr style="height:30px;">
                <td style="width:10px; background-color:#eee;"></td>
                <td  colspan= "3"  class ="lblTitulo1">
                    <asp:Label ID="lblTitulo" runat="server"  Text="Metas de producción" ></asp:Label><br/>
                </td> 
            </tr>
            <tr><td style="height:10px" colspan= "3"></td></tr>
            <tr>
                <td style="width:10px;" rowspan="2"></td>
                <td rowspan="2" valign="top" class="leftarea" style="width:100px">
                    <div id="navcontainer">
                        <ul id="navlist">
                            <li><a href="javascript:ListItemSelected(1,'')" ID="LAddNew" runat="server"><img src="../Imagenes/Nuevo.png" alt="Nuevo registro" style="border:0px;" /> Nuevo registro</a></li>
                            <li><a href="javascript:ListItemSelected(2,'')" ID="LExport" runat="server"><img src="../Imagenes/Exportar.png" alt="Exportar tabla" style="border:0px;" /> Exportar tabla</a></li>
                            <li><a href="javascript:history.back();" onclick="history.go(-1)"><img src="../Imagenes/Regresar.png" alt="Regresar" style="border:0px;" /> Regresar</a></li>
                        </ul>
                    </div>
                </td><td>&nbsp;</td>
                <td>           
                    <igmisc:WebAsyncRefreshPanel ID="WebAsyncRefreshPanel1" runat="server">
                    <table style="height: 100px; width: 450px">
                        <tbody>
                            <tr>
                              <td>
                                 <table>
                                    
                                  
                                    <tr>
                                               <td  class="textos_01">
                                               Fecha:
                                               </td>
                                               <td  class="textos_01">
                                                     <igsch:WebDateChooser ID="FechaIni" runat="server"  Width="156" >
                                                 </igsch:WebDateChooser>
                                               </td>
                                                    
                                               <td  class="textos_01">
                                              al
                                               </td>
                                               <td  class="textos_01">
                                                 <igsch:WebDateChooser ID="FechaFin" runat="server"  Width="156" >
                                                 </igsch:WebDateChooser>
                                               </td>
                                               <td align= "center" style=" height:26px" class="textos">
                                                  <asp:Button  ID="igtbl_reBuscaBtn" runat="server"  CssClass="Boton_01" OnClick="btnLlenaGrid_Click"
                                                            Text="Buscar" />
                                             </td>
                                             
                                            
                                               
                                                   <asp:DropDownList ID="cmbCalidad" Visible="true" runat="server" CssClass="hidden" >
                                                  
                                                    </asp:DropDownList> 
                                            
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
                                            function ok(event) {
                                                var TipoProc = $("#ctl00_Principal_UltraWebGrid1_ctl00_TipoProcT").val();
                                                var CantProc = $("#CantProcT").val();

                                                var TipoInv = $("#ctl00_Principal_UltraWebGrid1_ctl00_TipoInvT").val();
                                                var CantInv = $("#CantInvT").val();

                                                var TipoDesp = $("#ctl00_Principal_UltraWebGrid1_ctl00_TipoDespT").val();
                                                var CantDesp = $("#CantDespT").val();

                                                var TipoVerde = $("#ctl00_Principal_UltraWebGrid1_ctl00_TipoVerdeT").val();
                                                var CantVerde = $("#CantVerdeT").val();

                                                var TipoQuemado = $("#ctl00_Principal_UltraWebGrid1_ctl00_TipoQuemadoT").val();
                                                var CantQuemado = $("#CantQuemadoT").val();

                                                var CboCalidad1 = $("#ddlCalidad1").val();
                                                var TipoCal1 = $("#ctl00_Principal_UltraWebGrid1_ctl00_TipoCal1T").val();
                                                var PorcCal1 = $("#PorcCal1T").val();

                                                var CboCalidad2 = $("#ddlCalidad2").val();
                                                var TipoCal2 = $("#ctl00_Principal_UltraWebGrid1_ctl00_TipoCal2T").val();
                                                var PorcCal2 = $("#PorcCal2T").val();

                                                var CboCalidad3 = $("#ddlCalidad3").val();
                                                var TipoCal3 = $("#ctl00_Principal_UltraWebGrid1_ctl00_TipoCal3T").val();
                                                var PorcCal3 = $("#PorcCal3T").val();

                                                var CboCalidad4 = $("#ddlCalidad4").val();
                                                var TipoCal4 = $("#ctl00_Principal_UltraWebGrid1_ctl00_TipoCal4T").val();
                                                var PorcCal4 = $("#PorcCal4T").val();
                                              /*/  alert(TipoProc + "-" + CantProc + "-" + TipoInv + "-" + CantInv + "-" + TipoDesp + "-" + CantDesp + "-" + TipoVerde + "-" + CantVerde + "-" + TipoQuemado + "-" + CantQuemado + "-" + CboCalidad1 + "-" + TipoCal1 + "-" + PorcCal1 + "-" + CboCalidad2 + "-" + TipoCal2 + "-" + PorcCal2 + "-" + CboCalidad3 + "-" + TipoCal3 + "-" + PorcCal3 + "-" + CboCalidad4 + "-" + TipoCal4 + "-" + PorcCal4);*/
                                                if (CantProc != "" && CantInv != "" && CantDesp != "" && CantVerde != "" && CantQuemado != "" && PorcCal1 != "" && PorcCal2 != "" && PorcCal3 != "" && PorcCal4 != "") {
                                                    if (confirm('¿Desea guardar cambios?')) {
                                                        //asignar valores a los hidden
                                                        $("#<%=hddcant_procesadas.ClientID%>").val(CantProc);
                                                        $("#<%=hddcant_inventario.ClientID%>").val(CantInv);
                                                        $("#<%=hddcant_desperdicio.ClientID%>").val(CantDesp);
                                                        $("#<%=hddcant_desp_verde.ClientID%>").val(CantVerde);
                                                        $("#<%=hddcant_desp_quemado.ClientID%>").val(CantQuemado);
                                                        $("#<%=hddcalidad1.ClientID%>").val(CboCalidad1);
                                                        $("#<%=hddporcentaje_cal1.ClientID%>").val(PorcCal1);
                                                        $("#<%=hddcalidad2.ClientID%>").val(CboCalidad2);
                                                        $("#<%=hddporcentaje_cal2.ClientID%>").val(PorcCal2);
                                                        $("#<%=hddcalidad3.ClientID%>").val(CboCalidad3);
                                                        $("#<%=hddporcentaje_cal3.ClientID%>").val(PorcCal3);
                                                        $("#<%=hddcalidad4.ClientID%>").val(CboCalidad4);
                                                        $("#<%=hddporcentaje_cal4.ClientID%>").val(PorcCal4);
                                                        $("#<%=hddtipo_procesadas.ClientID%>").val(TipoProc);
                                                        $("#<%=hddtipo_inventario.ClientID%>").val(TipoInv);
                                                        $("#<%=hddtipo_desperdicio.ClientID%>").val(TipoDesp);
                                                        $("#<%=hddtipo_desp_verde.ClientID%>").val(TipoVerde);
                                                        $("#<%=hddtipo_desp_quemado.ClientID%>").val(TipoQuemado);
                                                        $("#<%=hddtipo_porcent_cal1.ClientID%>").val(TipoCal1);
                                                        $("#<%=hddtipo_porcent_cal2.ClientID%>").val(TipoCal2);
                                                        $("#<%=hddtipo_porcent_cal3.ClientID%>").val(TipoCal3);
                                                        $("#<%=hddtipo_porcent_cal4.ClientID%>").val(TipoCal4);
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



                                           


                                        </script>
                                        <div style="overflow: scroll; width:800px; " >
                                        <igtbl:UltraWebGrid ID="UltraWebGrid1" runat="server" CaptionAlign="Left" EnableAppStyling="False" OnPageIndexChanged = "cambio_pagina" > 
                                            <Bands>
                                                <igtbl:UltraGridBand>
                                                    <Columns>
                                                       <igtbl:UltraGridColumn Width="125px" BaseColumnName="PiezasProcesadas" IsBound="True" Key="PiezasProcesadas" CellMultiline="Yes">
                                                            <Header Caption="Piezas procesadas">
                                                             </Header>
                                                            <HeaderStyle  HorizontalAlign = "Center"></HeaderStyle>
                                                        </igtbl:UltraGridColumn>
                                                        
                                                        <igtbl:UltraGridColumn Width="140px" BaseColumnName="Inventarios" IsBound="True" Key="Inventarios" CellMultiline="Yes">
                                                            <Header Caption="Cantidad inventarios">
                                                                <RowLayoutColumnInfo OriginX="1" />
                                                            </Header>
                                                            <HeaderStyle  HorizontalAlign = "Center"></HeaderStyle>
                                                            <Footer><RowLayoutColumnInfo OriginX="1" /></Footer>
                                                        </igtbl:UltraGridColumn>
                                                        
                                                        <igtbl:UltraGridColumn Width="100px" BaseColumnName="PiezasMalas" IsBound="True" Key="PiezasMalas" CellMultiline="No">
                                                            <Header Caption="Piezas malas">
                                                                <RowLayoutColumnInfo OriginX="2" />
                                                            </Header>
                                                            <HeaderStyle  HorizontalAlign = "Center"></HeaderStyle>
                                                            <Footer><RowLayoutColumnInfo OriginX="2" /></Footer>
                                                        </igtbl:UltraGridColumn>
                                                        
                                                        <igtbl:UltraGridColumn Width="140px" BaseColumnName="PiezasMalasVerde" IsBound="True" Key="PiezasMalasVerde" CellMultiline="No">
                                                            <Header Caption="Piezas malas verdes">
                                                                <RowLayoutColumnInfo OriginX="3" />
                                                            </Header>
                                                            <HeaderStyle  HorizontalAlign = "Center"></HeaderStyle>
                                                            <Footer><RowLayoutColumnInfo OriginX="3" /></Footer>
                                                        </igtbl:UltraGridColumn>
                                                        
                                                            <igtbl:UltraGridColumn Width="155px" BaseColumnName="PiezasMalasQuemado" IsBound="True" Key="PiezasMalasQuemado" CellMultiline="No">
                                                            <Header Caption="Piezas malas quemado">
                                                                <RowLayoutColumnInfo OriginX="4" />
                                                            </Header>
                                                            <HeaderStyle  HorizontalAlign = "Center"></HeaderStyle>
                                                            <Footer><RowLayoutColumnInfo OriginX="4" /></Footer>
                                                        </igtbl:UltraGridColumn>
                                                        
                                                         <igtbl:UltraGridColumn Width="105px" BaseColumnName="Calidad1" IsBound="True" Key="Calidad1" CellMultiline="No">
                                                            <Header Caption="% calidad de 1">
                                                                <RowLayoutColumnInfo OriginX="5" />
                                                            </Header>
                                                            <HeaderStyle  HorizontalAlign = "Center"></HeaderStyle>
                                                            <Footer><RowLayoutColumnInfo OriginX="5" /></Footer>
                                                        </igtbl:UltraGridColumn>
                                                        
                                                               <igtbl:UltraGridColumn Width="105px" BaseColumnName="Calidad2" IsBound="True" Key="Calidad2" CellMultiline="No">
                                                            <Header Caption="% calidad de 2">
                                                                <RowLayoutColumnInfo OriginX="6" />
                                                            </Header>
                                                            <HeaderStyle  HorizontalAlign = "Center"></HeaderStyle>
                                                            <Footer><RowLayoutColumnInfo OriginX="6" /></Footer>
                                                        </igtbl:UltraGridColumn>
                                                        
                                                               <igtbl:UltraGridColumn Width="105px" BaseColumnName="Calidad3" IsBound="True" Key="Calidad3" CellMultiline="No">
                                                            <Header Caption="% calidad de 3">
                                                                <RowLayoutColumnInfo OriginX="7" />
                                                            </Header>
                                                            <HeaderStyle  HorizontalAlign = "Center"></HeaderStyle>
                                                            <Footer><RowLayoutColumnInfo OriginX="7" /></Footer>
                                                        </igtbl:UltraGridColumn>
                                                        
                                                               <igtbl:UltraGridColumn Width="105px" BaseColumnName="Calidad4" IsBound="True" Key="Calidad4" CellMultiline="No">
                                                            <Header Caption="% calidad de 4">
                                                                <RowLayoutColumnInfo OriginX="8" />
                                                            </Header>
                                                            <HeaderStyle  HorizontalAlign = "Center"></HeaderStyle>
                                                            <Footer><RowLayoutColumnInfo OriginX="8" /></Footer>
                                                        </igtbl:UltraGridColumn>
                                                        
                      
                                                        
                                                           <igtbl:UltraGridColumn Width="100px" BaseColumnName="Activo" IsBound="True" Key="Activo" Type="CheckBox" CellMultiline="no">
                                                            <Header Caption="Activo">
                                                                <RowLayoutColumnInfo OriginX="9" />
                                                            </Header>
                                                            <HeaderStyle HorizontalAlign = "Center"></HeaderStyle>
                                                            <CellStyle HorizontalAlign="Center"></CellStyle>
                                                            <Footer><RowLayoutColumnInfo OriginX="9" /></Footer>
                                                        </igtbl:UltraGridColumn>
                                                        
                                                          <igtbl:UltraGridColumn  Hidden="true" Width="105px" BaseColumnName="CodMetas" IsBound="True" Key="CodMetas" CellMultiline="No">
                                                            <Header Caption="CodMetas">
                                                                <RowLayoutColumnInfo OriginX="10" />
                                                            </Header>
                                                            <HeaderStyle  HorizontalAlign = "Center"></HeaderStyle>
                                                            <Footer><RowLayoutColumnInfo OriginX="10" /></Footer>
                                                        </igtbl:UltraGridColumn>
                                                        
                                                          <igtbl:UltraGridColumn Hidden="true"  Width="105px" BaseColumnName="Planta" IsBound="True" Key="Planta" CellMultiline="No">
                                                            <Header Caption="Planta">
                                                                <RowLayoutColumnInfo OriginX="11" />
                                                            </Header>
                                                            <HeaderStyle  HorizontalAlign = "Center"></HeaderStyle>
                                                            <Footer><RowLayoutColumnInfo OriginX="11" /></Footer>
                                                        </igtbl:UltraGridColumn>
                                                        
                                                          <igtbl:UltraGridColumn Hidden="true" Width="105px" BaseColumnName="CantProc" IsBound="True" Key="CantProc" CellMultiline="No">
                                                            <Header Caption="CantProc">
                                                                <RowLayoutColumnInfo OriginX="12" />
                                                            </Header>
                                                            <HeaderStyle  HorizontalAlign = "Center"></HeaderStyle>
                                                            <Footer><RowLayoutColumnInfo OriginX="12" /></Footer>
                                                        </igtbl:UltraGridColumn>
                                                        
                                                          <igtbl:UltraGridColumn Hidden="true"  Width="105px" BaseColumnName="TipoProc" IsBound="True" Key="TipoProc" CellMultiline="No">
                                                            <Header Caption="TipoProc">
                                                                <RowLayoutColumnInfo OriginX="13" />
                                                            </Header>
                                                            <HeaderStyle  HorizontalAlign = "Center"></HeaderStyle>
                                                            <Footer><RowLayoutColumnInfo OriginX="13" /></Footer>
                                                        </igtbl:UltraGridColumn>
                                                        
                                                          <igtbl:UltraGridColumn Hidden="true" Width="105px" BaseColumnName="CantInv" IsBound="True" Key="CantInv" CellMultiline="No">
                                                            <Header Caption="CantInv">
                                                                <RowLayoutColumnInfo OriginX="14" />
                                                            </Header>
                                                            <HeaderStyle  HorizontalAlign = "Center"></HeaderStyle>
                                                            <Footer><RowLayoutColumnInfo OriginX="14" /></Footer>
                                                        </igtbl:UltraGridColumn>
                                                        
                                                          <igtbl:UltraGridColumn Hidden="true" Width="105px" BaseColumnName="TipoInv" IsBound="True" Key="TipoInv" CellMultiline="No">
                                                            <Header Caption="TipoInv">
                                                                <RowLayoutColumnInfo OriginX="15" />
                                                            </Header>
                                                            <HeaderStyle  HorizontalAlign = "Center"></HeaderStyle>
                                                            <Footer><RowLayoutColumnInfo OriginX="15" /></Footer>
                                                        </igtbl:UltraGridColumn>
                                                        
                                                          <igtbl:UltraGridColumn Hidden="true" Width="105px" BaseColumnName="CantDesp" IsBound="True" Key="CantDesp" CellMultiline="No">
                                                            <Header Caption="CantDesp">
                                                                <RowLayoutColumnInfo OriginX="16" />
                                                            </Header>
                                                            <HeaderStyle  HorizontalAlign = "Center"></HeaderStyle>
                                                            <Footer><RowLayoutColumnInfo OriginX="16" /></Footer>
                                                        </igtbl:UltraGridColumn>
                                                        
                                                          <igtbl:UltraGridColumn Hidden="true" Width="105px" BaseColumnName="TipoDesp" IsBound="True" Key="TipoDesp" CellMultiline="No">
                                                            <Header Caption="TipoDesp">
                                                                <RowLayoutColumnInfo OriginX="17" />
                                                            </Header>
                                                            <HeaderStyle  HorizontalAlign = "Center"></HeaderStyle>
                                                            <Footer><RowLayoutColumnInfo OriginX="17" /></Footer>
                                                        </igtbl:UltraGridColumn>
                                                        
                                                          <igtbl:UltraGridColumn Hidden="true" Width="105px" BaseColumnName="CantVerde" IsBound="True" Key="CantVerde" CellMultiline="No">
                                                            <Header Caption="CantVerde">
                                                                <RowLayoutColumnInfo OriginX="18" />
                                                            </Header>
                                                            <HeaderStyle  HorizontalAlign = "Center"></HeaderStyle>
                                                            <Footer><RowLayoutColumnInfo OriginX="18" /></Footer>
                                                        </igtbl:UltraGridColumn>
                                                        
                                                          <igtbl:UltraGridColumn Hidden="true" Width="105px" BaseColumnName="TipoVerde" IsBound="True" Key="TipoVerde" CellMultiline="No">
                                                            <Header Caption="TipoVerde">
                                                                <RowLayoutColumnInfo OriginX="19" />
                                                            </Header>
                                                            <HeaderStyle  HorizontalAlign = "Center"></HeaderStyle>
                                                            <Footer><RowLayoutColumnInfo OriginX="19" /></Footer>
                                                        </igtbl:UltraGridColumn>
                                                        
                                                         <igtbl:UltraGridColumn Hidden="true" Width="105px" BaseColumnName="CantQuemado" IsBound="True" Key="CantQuemado" CellMultiline="No">
                                                            <Header Caption="CantQuemado">
                                                                <RowLayoutColumnInfo OriginX="20" />
                                                            </Header>
                                                            <HeaderStyle  HorizontalAlign = "Center"></HeaderStyle>
                                                            <Footer><RowLayoutColumnInfo OriginX="20" /></Footer>
                                                        </igtbl:UltraGridColumn>
                                                        
                                                         <igtbl:UltraGridColumn Hidden="true" Width="105px" BaseColumnName="TipoQuemado" IsBound="True" Key="TipoQuemado" CellMultiline="No">
                                                            <Header Caption="TipoQuemado">
                                                                <RowLayoutColumnInfo OriginX="21" />
                                                            </Header>
                                                            <HeaderStyle  HorizontalAlign = "Center"></HeaderStyle>
                                                            <Footer><RowLayoutColumnInfo OriginX="21" /></Footer>
                                                        </igtbl:UltraGridColumn>
                                                        
                                                         <igtbl:UltraGridColumn Hidden="true" Width="105px" BaseColumnName="ICalidad1" IsBound="True" Key="ICalidad1" CellMultiline="No">
                                                            <Header Caption="ICalidad1">
                                                                <RowLayoutColumnInfo OriginX="22" />
                                                            </Header>
                                                            <HeaderStyle  HorizontalAlign = "Center"></HeaderStyle>
                                                            <Footer><RowLayoutColumnInfo OriginX="22" /></Footer>
                                                        </igtbl:UltraGridColumn>
                                                        
                                                         <igtbl:UltraGridColumn Hidden="true" Width="105px" BaseColumnName="PorcCal1" IsBound="True" Key="PorcCal1" CellMultiline="No">
                                                            <Header Caption="PorcCal1">
                                                                <RowLayoutColumnInfo OriginX="23" />
                                                            </Header>
                                                            <HeaderStyle  HorizontalAlign = "Center"></HeaderStyle>
                                                            <Footer><RowLayoutColumnInfo OriginX="23" /></Footer>
                                                        </igtbl:UltraGridColumn>
                                                        
                                                         <igtbl:UltraGridColumn Hidden="true" Width="105px" BaseColumnName="TipoCal1" IsBound="True" Key="TipoCal1" CellMultiline="No">
                                                            <Header Caption="TipoCal1">
                                                                <RowLayoutColumnInfo OriginX="24" />
                                                            </Header>
                                                            <HeaderStyle  HorizontalAlign = "Center"></HeaderStyle>
                                                            <Footer><RowLayoutColumnInfo OriginX="24" /></Footer>
                                                        </igtbl:UltraGridColumn>
                                                        
                                                          <igtbl:UltraGridColumn Hidden="true" Width="105px" BaseColumnName="ICalidad2" IsBound="True" Key="ICalidad1" CellMultiline="No">
                                                            <Header Caption="ICalidad2">
                                                                <RowLayoutColumnInfo OriginX="25" />
                                                            </Header>
                                                            <HeaderStyle  HorizontalAlign = "Center"></HeaderStyle>
                                                            <Footer><RowLayoutColumnInfo OriginX="25" /></Footer>
                                                        </igtbl:UltraGridColumn>
                                                        
                                                         <igtbl:UltraGridColumn Hidden="true" Width="105px" BaseColumnName="PorcCal2" IsBound="True" Key="PorcCal1" CellMultiline="No">
                                                            <Header Caption="PorcCal2">
                                                                <RowLayoutColumnInfo OriginX="26" />
                                                            </Header>
                                                            <HeaderStyle  HorizontalAlign = "Center"></HeaderStyle>
                                                            <Footer><RowLayoutColumnInfo OriginX="26" /></Footer>
                                                        </igtbl:UltraGridColumn>
                                                        
                                                         <igtbl:UltraGridColumn Hidden="true" Width="105px" BaseColumnName="TipoCal2" IsBound="True" Key="TipoCal1" CellMultiline="No">
                                                            <Header Caption="TipoCal2">
                                                                <RowLayoutColumnInfo OriginX="27" />
                                                            </Header>
                                                            <HeaderStyle  HorizontalAlign = "Center"></HeaderStyle>
                                                            <Footer><RowLayoutColumnInfo OriginX="27" /></Footer>
                                                        </igtbl:UltraGridColumn>
                                                        
                                                          <igtbl:UltraGridColumn Hidden="true" Width="105px" BaseColumnName="ICalidad3" IsBound="True" Key="ICalidad1" CellMultiline="No">
                                                            <Header Caption="ICalidad3">
                                                                <RowLayoutColumnInfo OriginX="28" />
                                                            </Header>
                                                            <HeaderStyle  HorizontalAlign = "Center"></HeaderStyle>
                                                            <Footer><RowLayoutColumnInfo OriginX="28" /></Footer>
                                                        </igtbl:UltraGridColumn>
                                                        
                                                         <igtbl:UltraGridColumn Hidden="true" Width="105px" BaseColumnName="PorcCal3" IsBound="True" Key="PorcCal1" CellMultiline="No">
                                                            <Header Caption="PorcCal3">
                                                                <RowLayoutColumnInfo OriginX="29" />
                                                            </Header>
                                                            <HeaderStyle  HorizontalAlign = "Center"></HeaderStyle>
                                                            <Footer><RowLayoutColumnInfo OriginX="29" /></Footer>
                                                        </igtbl:UltraGridColumn>
                                                        
                                                         <igtbl:UltraGridColumn Hidden="true" Width="105px" BaseColumnName="TipoCal3" IsBound="True" Key="TipoCal1" CellMultiline="No">
                                                            <Header Caption="TipoCal3">
                                                                <RowLayoutColumnInfo OriginX="30" />
                                                            </Header>
                                                            <HeaderStyle  HorizontalAlign = "Center"></HeaderStyle>
                                                            <Footer><RowLayoutColumnInfo OriginX="30" /></Footer>
                                                        </igtbl:UltraGridColumn>
                                                        
                                                          <igtbl:UltraGridColumn Hidden="true" Width="105px" BaseColumnName="ICalidad4" IsBound="True" Key="ICalidad1" CellMultiline="No">
                                                            <Header Caption="ICalidad4">
                                                                <RowLayoutColumnInfo OriginX="31" />
                                                            </Header>
                                                            <HeaderStyle  HorizontalAlign = "Center"></HeaderStyle>
                                                            <Footer><RowLayoutColumnInfo OriginX="31" /></Footer>
                                                        </igtbl:UltraGridColumn>
                                                        
                                                         <igtbl:UltraGridColumn Hidden="true" Width="105px" BaseColumnName="PorcCal4" IsBound="True" Key="PorcCal1" CellMultiline="No">
                                                            <Header Caption="PorcCal4">
                                                                <RowLayoutColumnInfo OriginX="32" />
                                                            </Header>
                                                            <HeaderStyle  HorizontalAlign = "Center"></HeaderStyle>
                                                            <Footer><RowLayoutColumnInfo OriginX="32" /></Footer>
                                                        </igtbl:UltraGridColumn>
                                                        
                                                         <igtbl:UltraGridColumn Hidden="true" Width="105px" BaseColumnName="TipoCal4" IsBound="True" Key="TipoCal1" CellMultiline="No">
                                                            <Header Caption="TipoCal4">
                                                                <RowLayoutColumnInfo OriginX="33" />
                                                            </Header>
                                                            <HeaderStyle  HorizontalAlign = "Center"></HeaderStyle>
                                                            <Footer><RowLayoutColumnInfo OriginX="33" /></Footer>
                                                        </igtbl:UltraGridColumn>
                                                        
                                                     <igtbl:UltraGridColumn Hidden="true" Width="100px" BaseColumnName="ExceptionMessage" IsBound="True"   CellMultiline="Yes">
                                                                    <Header Caption="ExceptionMessage">
                                                                        <RowLayoutColumnInfo OriginX="34" />
                                                                    </Header>
                                                                    <HeaderStyle HorizontalAlign = "Center"></HeaderStyle>
                                                                    <CellStyle HorizontalAlign="Center"></CellStyle>
                                                                    <Footer><RowLayoutColumnInfo OriginX="34" /></Footer>
                                                                </igtbl:UltraGridColumn>
                                                        
                                                       
                                                
                                                        
                                                    </Columns>
                                                    <RowEditTemplate>
                                                        <table style="font-family: Arial; text-align: center">
                                                         <br />
                                                           
                                                            
                                                            
                                                             <tr>
                                                                    <td class ="textos_01">
                                                                         Piezas procesadas:
                                                                    </td>
                                                                    <td class ="textos_01">
                                                                         <input type="hidden"  id="TipoProcH" columnkey="TipoProc" />
                                                                         <asp:DropDownList     ID="TipoProcT" Width="80px" runat="server" CssClass="textosd" columnkey="TipoProc" >
                                                                               <asp:ListItem Value=1 Text="Min" ></asp:ListItem>
                                                                               <asp:ListItem Value=2 Text="Max"></asp:ListItem>
                                                                          </asp:DropDownList>
                                                                    </td>
                                                                    <td class ="textos_01">
                                                                            <input type="hidden" id="CantProcH" columnkey="CantProc" />
                                                                            <input id="CantProcT"  columnkey="CantProc" style="width: 70px; type="text"  >
                                                                    </td>
                                                                   
                                                                    <td class ="textos_01">
                                                                  &nbsp;&nbsp;Porcentaje calidad 1:
                                                                </td >
                                                                    <td class ="textos_01">
                                                                <input type="hidden" id="ICalidad1H" columnkey="ICalidad1" />
                                                                       <select  id="ddlCalidad1" width="200px" columnkey="ICalidad1" >
                                                                    </select>
                                                                </td>
                                                                    <td class="textos_01">
                                                                   <input type="hidden" id="TipoCal1H" columnkey="TipoCal1" />
                                                                   <asp:DropDownList ID="TipoCal1T" Width="80px" runat="server" CssClass="textosd"  columnkey="TipoCal1">
                                                                       <asp:ListItem Value=1 Text="Min"  ></asp:ListItem>
                                                                       <asp:ListItem Value=2 Text="Max"></asp:ListItem>
                                                                  </asp:DropDownList>
                                                                </td>
                                                                    <td class="textos_01">
                                                                   <input type="hidden" id="PorcCal1H" columnkey="PorcCal1" />
                                                                    <input id="PorcCal1T" columnkey="PorcCal1" style="width: 70px; type="text" >
                                                                </td>
                                                                
                                                             </tr>
                                                             <tr>
                                                                    <td class ="textos_01">
                                                                         Cantidad Inventarios:
                                                                    </td>
                                                                    <td class ="textos_01">
                                                                         <input type="hidden" id="TipoInvH" columnkey="TipoInv" />
                                                                         <asp:DropDownList ID="TipoInvT" Width="80px" runat="server" CssClass="textosd" columnkey="TipoInv">
                                                                               <asp:ListItem Value=1 Text="Min"></asp:ListItem>
                                                                               <asp:ListItem Value=2 Text="Max"></asp:ListItem>
                                                                          </asp:DropDownList>
                                                                    </td>
                                                                    <td class ="textos_01">
                                                                            <input type="hidden" id="CantInvH" columnkey="CantInv" />
                                                                            <input id="CantInvT" columnkey="CantInv" style="width: 70px; type="text" >
                                                                    </td>
                                                          
                                                                    <td class ="textos_01">
                                                                    &nbsp;&nbsp;     Porcentaje calidad 2:
                                                                    </td >
                                                                    <td class ="textos_01">
                                                                    <input type="hidden" id="ICalidad2H" columnkey="ICalidad2" />
                                                                         <select  id="ddlCalidad2" width="200px" columnkey="ICalidad2">
                                                                    </select>
                                                                    </td>
                                                                    <td class="textos_01">
                                                                       <input type="hidden" id="TipoCal2H" columnkey="TipoCal2" />
                                                                       <asp:DropDownList ID="TipoCal2T" Width="80px" runat="server" CssClass="textosd"  columnkey="TipoCal2">
                                                                           <asp:ListItem Value=1 Text="Min"></asp:ListItem>
                                                                           <asp:ListItem Value=2 Text="Max"></asp:ListItem>
                                                                      </asp:DropDownList>
                                                                    </td>
                                                                    <td class="textos_01">
                                                                       <input type="hidden" id="PorcCal2H" columnkey="PorcCal2" />
                                                                        <input id="PorcCal2T" columnkey="PorcCal2" style="width: 70px; type="text" >
                                                                    </td>
                                                                
                                                            </tr>
                                                             <tr>
                                                                    <td class ="textos_01">
                                                                         Piezas malas:
                                                                    </td>
                                                                    <td class ="textos_01">
                                                                         <input type="hidden" id="TipoDespH" columnkey="TipoDesp" />
                                                                         <asp:DropDownList ID="TipoDespT" Width="80px" runat="server" CssClass="textosd"  columnkey="TipoDesp">
                                                                               <asp:ListItem Value=1 Text="Min"></asp:ListItem>
                                                                               <asp:ListItem Value=2 Text="Max"></asp:ListItem>
                                                                          </asp:DropDownList>
                                                                    </td>
                                                                    <td class ="textos_01">
                                                                            <input type="hidden" id="CantDespH" columnkey="CantDesp" />
                                                                            <input id="CantDespT" columnkey="CantDesp" style="width: 70px; type="text" >
                                                                    </td>
                                                          
                                                                    <td class ="textos_01">
                                                                 &nbsp;&nbsp;    Porcentaje calidad 3:
                                                                </td >
                                                                    <td class ="textos_01">
                                                                <input type="hidden" id="ICalidad3H" columnkey="ICalidad3" />
                                                                      <select  id="ddlCalidad3" width="200px" columnkey="ICalidad3">
                                                                    </select>
                                                                </td>
                                                                    <td class="textos_01">
                                                                   <input type="hidden" id="TipoCal3H" columnkey="TipoCal3" />
                                                                   <asp:DropDownList ID="TipoCal3T" Width="80px" runat="server" CssClass="textosd" columnkey="TipoCal3">
                                                                       <asp:ListItem Value=1 Text="Min"></asp:ListItem>
                                                                       <asp:ListItem Value=2 Text="Max"></asp:ListItem>
                                                                  </asp:DropDownList>
                                                                </td>
                                                                    <td class="textos_01">
                                                                   <input type="hidden" id="PorcCal3H" columnkey="PorcCal3" />
                                                                    <input id="PorcCal3T" columnkey="PorcCal3" style="width: 70px; type="text" >
                                                                </td>
                                                                
                                                            </tr> 
                                                             <tr>
                                                                    <td class ="textos_01">
                                                                       Piezas malas verdes:
                                                                    </td>
                                                                    <td class ="textos_01">
                                                                         <input type="hidden" id="TipoVerdeH" columnkey="TipoVerde" />
                                                                         <asp:DropDownList ID="TipoVerdeT" Width="80px" runat="server" CssClass="textosd"  columnkey="TipoVerde">
                                                                               <asp:ListItem Value=1 Text="Min"></asp:ListItem>
                                                                               <asp:ListItem Value=2 Text="Max"></asp:ListItem>
                                                                          </asp:DropDownList>
                                                                    </td>
                                                                    <td class ="textos_01">
                                                                            <input type="hidden" id="CantVerdeH" columnkey="CantVerde" />
                                                                            <input id="CantVerdeT" columnkey="CantVerde" style="width: 70px; type="text" >
                                                                    </td>
                                                          
                                                          
                                                                    <td class ="textos_01">
                                                                  &nbsp;&nbsp;       Porcentaje calidad 4:
                                                                    </td >
                                                                    <td class ="textos_01">
                                                                    <input type="hidden" id="ICalidad4H" columnkey="ICalidad4" />
                                                                          <select  id="ddlCalidad4" width="200px" columnkey="ICalidad4">
                                                                    </select>
                                                                    </td>
                                                                    <td class="textos_01">
                                                                       <input type="hidden" id="TipoCal4H" columnkey="TipoCal4" />
                                                                       <asp:DropDownList ID="TipoCal4T" Width="80px" runat="server" CssClass="textosd" columnkey="TipoCal4">
                                                                           <asp:ListItem Value=1 Text="Min"></asp:ListItem>
                                                                           <asp:ListItem Value=2 Text="Max"></asp:ListItem>
                                                                      </asp:DropDownList>
                                                                    </td>
                                                                    <td class="textos_01">
                                                                   <input type="hidden" id="PorcCal4H" columnkey="PorcCal4" />
                                                                    <input id="PorcCal4T" columnkey="PorcCal4" style="width: 70px; type="text" >
                                                                </td>
                                                                
                                                            </tr> 
                                                             <tr>
                                                            <td class ="textos_01">
                                                               Piezas malas quemado:
                                                            </td>
                                                            <td class ="textos_01">
                                                                 <input type="hidden" id="TipoQuemadoH" columnkey="TipoQuemado" />
                                                                 <asp:DropDownList ID="TipoQuemadoT" Width="80px" runat="server" CssClass="textosd"  columnkey="TipoQuemado">
                                                                       <asp:ListItem Value=1 Text="Min"></asp:ListItem>
                                                                       <asp:ListItem Value=2 Text="Max"></asp:ListItem>
                                                                                                                                      </asp:DropDownList>
                                                            </td>
                                                            <td class ="textos_01">
                                                                <input type="hidden" id="CantQuemadoH" columnkey="CantQuemado" />
                                                                <input id="CantQuemadoT" columnkey="CantQuemado" style="width: 70px; type="text" >
                                                        </td>
                                                  
                                                               <td class ="textos_01"></td >
                                                               <td class ="textos_01"></td>
                                                               <td class ="textos_01"></td>
                                                               <td class ="textos_01"></td>
                                                                
                                                            </tr> 

                                                             <tr>
                                                                
                                                                <td colspan="7" align="center">
                                                                    <p align="center">
                                                                        <input id="Button1" onclick="ok(event)" class="Boton_01" style="width: 75px;" type="button" 
                                                                            value="Guardar"> </input>
                                                                        <input id="Button2" onclick="cancel(event)" class="Boton_01" style="width: 75px;" type= "button"
                                                                            value="Cancelar"> </input>
                                                                      
                                                                    </p>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </RowEditTemplate>
                                                    <RowTemplateStyle Height="215px"  Width="750" BackColor="White" BorderColor="White" BorderStyle="Ridge"></RowTemplateStyle>
                                                    <AddNewRow Visible="NotSet" View="NotSet"></AddNewRow>
                                                </igtbl:UltraGridBand>
                                            </Bands>
                                            <DisplayLayout Name="UltraWebGrid1" AllowDeleteDefault="Yes" AllowUpdateDefault="RowTemplateOnly" 
                                                NoDataMessage="" RowHeightDefault="20px" SelectTypeRowDefault="Single" TableLayout="Fixed" Version="3.00"
                                                CellClickActionDefault="RowSelect" LoadOnDemand="Xml" AllowAddNewDefault="Yes"
                                                AllowColSizingDefault="Free" AllowSortingDefault="OnClient" CellPaddingDefault="1"
                                                HeaderClickActionDefault="SortSingle" RowSelectorsDefault="No" RowSizingDefault="Free">
                                                <RowAlternateStyleDefault BackColor= "#E5E5E5"></RowAlternateStyleDefault>
                                                <Pager AllowPaging="True" PageSize="20">
                                                    <PagerStyle Font-Size= "11px"  Font-Names="Arial" BackColor= "#666666" ForeColor="White" Height="20px" BorderStyle= "Solid" BorderColor="Black" BorderWidth= "1px"/>
                                                </Pager>
                                                <EditCellStyleDefault BackColor= "silver" ></EditCellStyleDefault>
                                                <FooterStyleDefault BackColor="LightGray" BorderStyle="Solid" BorderWidth="1px"></FooterStyleDefault>
                                                <HeaderStyleDefault BackColor="#666666"   BorderColor="Black" BorderStyle="Solid" Font-Bold="True" ForeColor="White"> </HeaderStyleDefault>
                                                <RowSelectorStyleDefault BorderStyle="Solid"></RowSelectorStyleDefault>
                                                <RowStyleDefault BackColor="White" BorderColor="Black" BorderStyle="Solid" BorderWidth="1px"
                                                                 Font-Names="Verdana" Font-Size="8pt" ForeColor="Black"></RowStyleDefault>
                                                <SelectedRowStyleDefault BackColor="#FFFFB3" ForeColor="Black" Font-Bold="True"></SelectedRowStyleDefault>
                                                <AddNewBox Hidden="true"></AddNewBox>
                                                <ActivationObject BorderColor="Black" BorderWidth="" BorderStyle="Dotted"></ActivationObject>
                                                <AddNewRowDefault View="Top" Visible="No"></AddNewRowDefault>
                                                <FilterOptionsDefault>
                                                    <FilterDropDownStyle BackColor="White" BorderColor="Silver" BorderStyle="Solid" 
                                                                         BorderWidth="1px" CustomRules="overflow:auto;" 
                                                                         Font-Names="Verdana,Arial,Helvetica,sans-serif" Font-Size="11px" Width="200px">
                                                    </FilterDropDownStyle>
                                                    <FilterHighlightRowStyle BackColor="#999999" ForeColor="White"></FilterHighlightRowStyle>
                                                </FilterOptionsDefault>
                                                <ClientSideEvents  BeforeRowTemplateCloseHandler ="BeforeRowTemplateCloseHandler" 
                                                 BeforeRowTemplateOpenHandler="BeforeRowTemplateOpen" AfterRowTemplateOpenHandler="AfterRowTemplateOpen" />
                                            </DisplayLayout>
                                        </igtbl:UltraWebGrid>  
                                      </div>
                                    </td>
                           </tr>
                       </tbody>
                   </table>
                   
                     <input type="hidden" id="hddPlanta" runat="server" />
                     <input type="hidden" id="hddcant_procesadas" runat="server" />
                     <input type="hidden" id="hddcant_inventario" runat="server" />
                     <input type="hidden" id="hddcant_desperdicio" runat="server" />
                     <input type="hidden" id="hddcant_desp_verde" runat="server" />
                     <input type="hidden" id="hddcant_desp_quemado" runat="server" />
                     <input type="hidden" id="hddcalidad1" runat="server" />
                     <input type="hidden" id="hddporcentaje_cal1" runat="server" />
                     <input type="hidden" id="hddcalidad2" runat="server" />
                     <input type="hidden" id="hddporcentaje_cal2" runat="server" />
                     <input type="hidden" id="hddcalidad3" runat="server" />
                     <input type="hidden" id="hddporcentaje_cal3" runat="server" />
                     <input type="hidden" id="hddcalidad4" runat="server" />
                     <input type="hidden" id="hddporcentaje_cal4" runat="server" />
                     <input type="hidden" id="hddtipo_procesadas" runat="server" />
                     <input type="hidden" id="hddtipo_inventario" runat="server" />
                     <input type="hidden" id="hddtipo_desperdicio" runat="server" />
                     <input type="hidden" id="hddtipo_desp_verde" runat="server" />
                     <input type="hidden" id="hddtipo_desp_quemado" runat="server" />
                     <input type="hidden" id="hddtipo_porcent_cal1" runat="server" />
                     <input type="hidden" id="hddtipo_porcent_cal2" runat="server" />
                     <input type="hidden" id="hddtipo_porcent_cal3" runat="server" />
                     <input type="hidden" id="hddtipo_porcent_cal4" runat="server" />
                     
                      <asp:Button ID="BotonGuardar" runat="server" Text="Button" CssClass="hidden" OnClick="BotonGuardar_click" />
                      

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

                              function BeforeRowTemplateOpen(gridName, rowId) {
                                  SwitchDiv(false);

                                  $("#ddlCalidad1").html($("#<%=cmbCalidad.ClientID %>").html());
                                  $("#ddlCalidad2").html($("#<%=cmbCalidad.ClientID %>").html());
                                  $("#ddlCalidad3").html($("#<%=cmbCalidad.ClientID %>").html());
                                  $("#ddlCalidad4").html($("#<%=cmbCalidad.ClientID %>").html());

                              }

                              function AfterRowTemplateOpen(gridName, rowId) {

                                  var r = $("#ICalidad1H").val() ? $("#ICalidad1H").val() : $("#ddlCalidad1").val();
                                  var r = $("#ICalidad2H").val() ? $("#ICalidad2H").val() : $("#ddlCalidad2").val();
                                  var r = $("#ICalidad3H").val() ? $("#ICalidad3H").val() : $("#ddlCalidad3").val();
                                  var r = $("#ICalidad4H").val() ? $("#ICalidad4H").val() : $("#ddlCalidad4").val();

                              }

                              function SwitchDiv(nuevo) {
                       
                                  if (nuevo) {

                                      document.getElementById("ctl00_Principal_UltraWebGrid1_ctl00_TipoProcT").disabled = false;
                                      document.getElementById("CantProcT").disabled = false;
                                      document.getElementById("ddlCalidad1").disabled = false;
                                      document.getElementById("ctl00_Principal_UltraWebGrid1_ctl00_TipoCal1T").disabled = false;
                                      document.getElementById("PorcCal1T").disabled = false;
                                      document.getElementById("ctl00_Principal_UltraWebGrid1_ctl00_TipoInvT").disabled = false;
                                      document.getElementById("CantInvT").disabled = false;
                                      document.getElementById("ddlCalidad2").disabled = false;
                                      document.getElementById("ctl00_Principal_UltraWebGrid1_ctl00_TipoCal2T").disabled = false;
                                      document.getElementById("PorcCal2T").disabled = false;
                                      document.getElementById("ctl00_Principal_UltraWebGrid1_ctl00_TipoDespT").disabled = false;
                                      document.getElementById("CantDespT").disabled = false;
                                      document.getElementById("ddlCalidad3").disabled = false;
                                      document.getElementById("ctl00_Principal_UltraWebGrid1_ctl00_TipoCal3T").disabled = false;
                                      document.getElementById("PorcCal3T").disabled = false;
                                      document.getElementById("ctl00_Principal_UltraWebGrid1_ctl00_TipoVerdeT").disabled = false;
                                      document.getElementById("CantVerdeT").disabled = false;
                                      document.getElementById("ddlCalidad4").disabled = false;
                                      document.getElementById("ctl00_Principal_UltraWebGrid1_ctl00_TipoCal4T").disabled = false;
                                      document.getElementById("PorcCal4T").disabled = false;
                                      document.getElementById("ctl00_Principal_UltraWebGrid1_ctl00_TipoQuemadoT").disabled = false;
                                      document.getElementById("CantQuemadoT").disabled = false;
                                      document.getElementById("Button1").disabled = false; 
                             

                                  } else {
                                      

                                  document.getElementById("ctl00_Principal_UltraWebGrid1_ctl00_TipoProcT").disabled = true;
                                  document.getElementById("CantProcT").disabled = true;
                                  document.getElementById("ddlCalidad1").disabled = true;
                                  document.getElementById("ctl00_Principal_UltraWebGrid1_ctl00_TipoCal1T").disabled = true;
                                  document.getElementById("PorcCal1T").disabled = true;
                                  document.getElementById("ctl00_Principal_UltraWebGrid1_ctl00_TipoInvT").disabled = true;
                                  document.getElementById("CantInvT").disabled = true;
                                  document.getElementById("ddlCalidad2").disabled = true;
                                  document.getElementById("ctl00_Principal_UltraWebGrid1_ctl00_TipoCal2T").disabled = true;
                                  document.getElementById("PorcCal2T").disabled = true;
                                  document.getElementById("ctl00_Principal_UltraWebGrid1_ctl00_TipoDespT").disabled = true;
                                  document.getElementById("CantDespT").disabled = true;
                                  document.getElementById("ddlCalidad3").disabled = true;
                                  document.getElementById("ctl00_Principal_UltraWebGrid1_ctl00_TipoCal3T").disabled = true;
                                  document.getElementById("PorcCal3T").disabled = true;
                                  document.getElementById("ctl00_Principal_UltraWebGrid1_ctl00_TipoVerdeT").disabled = true;
                                  document.getElementById("CantVerdeT").disabled = true;
                                  document.getElementById("ddlCalidad4").disabled = true;
                                  document.getElementById("ctl00_Principal_UltraWebGrid1_ctl00_TipoCal4T").disabled = true;
                                  document.getElementById("PorcCal4T").disabled = true;
                                  document.getElementById("ctl00_Principal_UltraWebGrid1_ctl00_TipoQuemadoT").disabled = true;
                                  document.getElementById("CantQuemadoT").disabled = true;
                                  document.getElementById("Button1").disabled = true;
                              
    
    
    


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
                                                  
                                                  <td  class ="textos_01">
                                                       Nombre del archivo:
                                                  </td> 
                                                  <td  class ="textos_01">  
                                                  <input id="nombre" value="Metas de produccion" style="width: 200px;" type="text" runat="server"  />  
                                                    
                                                  </td>
                                                
                                            </tr>
                                        
                                            <tr >          
                                                    <td style="width:200px" align="center"   colspan="2">      
                                                    <asp:Button ID="btnExporta" runat="server"  CssClass="Boton_01" OnClick="btnExporta_Click"
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
                    <igtblexp:UltraWebGridExcelExporter ID="uwgTurnos" WorksheetName="Turnos" DownloadName="Reporte.XLS" runat='server'>
                    </igtblexp:UltraWebGridExcelExporter>
                </td>
            </tr>
        </table>
        <asp:HiddenField ID="UserInSession" runat="server" />
        <asp:HiddenField ID="HddUser" runat="server" />
        <asp:HiddenField ID="Sucursalhdd" runat="server" />
        <asp:UpdatePanel runat="server" ID="UpdatePanel2" ChildrenAsTriggers="True" UpdateMode="Always" RenderMode="Inline">
            <ContentTemplate>
                <asp:HiddenField ID="HiddenField1" runat="server" />
                <asp:HiddenField ID="hddSecurityConstants" runat="server" />
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
    
    <asp:Literal ID="Literal1"  Visible="false" runat="server"></asp:Literal>
</asp:Content>
