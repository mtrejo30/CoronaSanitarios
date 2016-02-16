

<%@ Page Language="C#" MasterPageFile="~/ControlPisoLamosa.Master" Title="Control de piso - Configuración de operación"
AutoEventWireup="true" CodeBehind="CondicionOperacion.aspx.cs" Inherits="LAMOSA.SCPP.Client.View.Administrador.Configuraciones.CondicionOperacion" %>




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
                    <asp:Label ID="lblTitulo" runat="server"  Text="Configuración de operación" ></asp:Label><br/>
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
                    <table style="height: 100px; width: 800px" >
                        <tbody>
                            <tr>
                              <td>
                                 <table>
                                    <tr>
                                             <td  class="textos_01">
                                             Proceso:
                                             </td>
                                             <td  class="textos_01">
                                               <asp:DropDownList ID="cmbProceso"  Width="230px" runat="server" CssClass="textosd" AutoPostBack="true" OnSelectedIndexChanged="cmbProceso_SelectedIndexChanged" >  </asp:DropDownList>
                                             </td>
                                             
                                             <td  class="textos_01">
                                                 Centro de trabajo:
                                             </td>
                                            <td  class="textos_01">
                                                 <asp:DropDownList ID="cmbCentroT" Width="230px" runat="server" CssClass="textosd" AutoPostBack="true" OnSelectedIndexChanged="cmbCentroT_SelectedIndexChanged" >  </asp:DropDownList>
                                            </td>
                                   </tr> 
                                   
                                         <tr>
                                             <td  class="textos_01">
                                             Área:
                                             </td>
                                             <td  class="textos_01">
                                                <asp:DropDownList ID="cmbArea"  Width="230px" runat="server" CssClass="textosd"  >  </asp:DropDownList>
                                                
                                                <asp:DropDownList ID="cmbAreaH" runat="server" CssClass="hidden"  >  </asp:DropDownList>
                                             </td>
                                             
                                             <td  class="textos_01">
                                            
                                             </td>
                                            <td  class="textos_01">
                                                
                                            </td>
                                          
                                           
                                   </tr> 
                                  
                                   <tr>
                                               <td  class="textos_01">
                                               Fecha:
                                               </td>
                                               <td  class="textos_01">
                                                     <igsch:WebDateChooser ID="FechaIni" runat="server"  Width="156" >
                                                 </igsch:WebDateChooser>
                                               </td>
                                                    
                                               <td  class="textos_01"    >
                                        
                                              al
                                               </td>
                                                <td  class="textos_01">
                                                 <igsch:WebDateChooser ID="FechaFin" runat="server"  Width="156" >
                                                 </igsch:WebDateChooser>
                                               </td>
                                               
                                                <td align= "center" style=" height:26px" class="textos">
                                                    <asp:Button ID="igtbl_reBuscaBtn" runat="server"  CssClass="Boton_01" OnClick="btnLlenaGrid_Click"
                                                            Text="Buscar" />
                                                </td>
                                            </tr> 
                                            
                                </table>
                              </td>
                            </tr>
                            
                             <tr>
                                        
                                    </tr>
                            
                            <tr>
                                    <td>
                                        <script src="../FuncionesJS/jquery-1.4.2.js" type="text/javascript"></script>
                                        <script type="text/javascript">
                                            var beforeClose = true;
                                            var clickcancel = true;
                                            function ok(event) {
                                                var CodCondicionOperacion = $("#CodCondicionOperacionH").val();
                                                var CodArea = $("#CodAreaT").val();
                                                var Fecha = $("#FechaT").val();
                                                var Temperatura = $("#TemperaturaT").val();
                                                var Humedad = $("#HumedadT").val();
                                            
                                                var Autorizacion = "'" + $("#AutorizacionT").attr('checked') + "'";
                                                var Activo = "'" + $("#ActivoT").attr('checked') + "'";
                                                if (Temperatura != "" && Humedad != "") {
                                                    if (confirm('¿Desea guardar cambios?')) {
                                                       
                                                        //asignar valores a los hidden
                                                        $("#<%=hddCodCondicionOperacion.ClientID%>").val(CodCondicionOperacion);
                                                        $("#<%=hddTemperatura.ClientID%>").val(Temperatura); 
                                                        $("#<%=hddHumedad.ClientID%>").val(Humedad);
                                                        $("#<%=hddCodArea.ClientID%>").val(CodArea);
                                                        $("#<%=hddAutorizacion.ClientID%>").val(Autorizacion);
                                                        $("#<%=hddActivo.ClientID%>").val(Activo);
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
                                                var CodCondicionOperacion = $("#CodCondicionOperacionH").val();
                                       
                                                var Autorizacion = "'" + $("#AutorizacionT").attr('checked') + "'";

                                    
                                                if (confirm('¿Realmente desea autorizar?')) {

                                                        //asignar valores a los hidden
                                                    $("#<%=hddCodCondicionOperacion.ClientID%>").val(CodCondicionOperacion);
                                                       
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
                                        </script>
                                        <igtbl:UltraWebGrid ID="UltraWebGrid1" runat="server" CaptionAlign="Left" EnableAppStyling="False"   DisplayLayout-AllowUpdateDefault="NotSet"  OnPageIndexChanged = "cambio_pagina"  > 
                                            <Bands>
                                                <igtbl:UltraGridBand>
                                                    <Columns>
                                                        <igtbl:UltraGridColumn Width="90px" BaseColumnName="CodCondicionEsmalte" IsBound="True" Key="CodCondicionEsmalte"  CellMultiline="Yes" Hidden="true">
                                                            <Header Caption="CodCondicionEsmalte">
                                                              <RowLayoutColumnInfo OriginX="0" />
                                                            </Header>
                                                            <HeaderStyle  HorizontalAlign = "Center"></HeaderStyle>
                                                            <Footer><RowLayoutColumnInfo OriginX="0" /></Footer>
                                                        </igtbl:UltraGridColumn>
                                                        
                                                        <igtbl:UltraGridColumn Width="90px" BaseColumnName="CodPlanta" IsBound="True" Key="CodPlanta"  CellMultiline="Yes" Hidden="true">
                                                            <Header Caption="CodPlanta">
                                                              <RowLayoutColumnInfo OriginX="1" />
                                                            </Header>
                                                            <HeaderStyle  HorizontalAlign = "Center"></HeaderStyle>
                                                            <Footer><RowLayoutColumnInfo OriginX="1" /></Footer>
                                                        </igtbl:UltraGridColumn>
                                                        
                                                        <igtbl:UltraGridColumn Width="90px" BaseColumnName="Fecha" IsBound="True" Key="Fecha"  CellMultiline="Yes">
                                                            <Header Caption="Fecha">
                                                              <RowLayoutColumnInfo OriginX="2" />
                                                            </Header>
                                                            <HeaderStyle  HorizontalAlign = "Center"></HeaderStyle>
                                                            <Footer><RowLayoutColumnInfo OriginX="2" /></Footer>
                                                        </igtbl:UltraGridColumn>
                                                        
                                                        <igtbl:UltraGridColumn Width="120px" BaseColumnName="TiempoEspejo" IsBound="True" Key="TiempoEspejo" CellMultiline="Yes">
                                                            <Header Caption="Tiempo espejo">
                                                                <RowLayoutColumnInfo OriginX="3" />
                                                            </Header>
                                                            <HeaderStyle  HorizontalAlign = "Center"></HeaderStyle>
                                                            <Footer><RowLayoutColumnInfo OriginX="3" /></Footer>
                                                        </igtbl:UltraGridColumn>
                                                        
                                                        <igtbl:UltraGridColumn Width="100px" BaseColumnName="Viscosidad" IsBound="True" Key="Viscosidad" CellMultiline="No">
                                                            <Header Caption="Viscosidad">
                                                                <RowLayoutColumnInfo OriginX="4" />
                                                            </Header>
                                                            <HeaderStyle  HorizontalAlign = "Center"></HeaderStyle>
                                                            <Footer><RowLayoutColumnInfo OriginX="4" /></Footer>
                                                        </igtbl:UltraGridColumn>
                                                        
                                                         <igtbl:UltraGridColumn Width="100px" BaseColumnName="Densidad" IsBound="True" Key="Densidad" CellMultiline="No">
                                                            <Header Caption="Densidad">
                                                                <RowLayoutColumnInfo OriginX="5" />
                                                            </Header>
                                                            <HeaderStyle  HorizontalAlign = "Center"></HeaderStyle>
                                                            <Footer><RowLayoutColumnInfo OriginX="5" /></Footer>
                                                        </igtbl:UltraGridColumn>
                                                        
                                                        <igtbl:UltraGridColumn Width="100px" BaseColumnName="Espesor" IsBound="True" Key="Espesor" CellMultiline="No">
                                                            <Header Caption="Espesor">
                                                                <RowLayoutColumnInfo OriginX="6" />
                                                            </Header>
                                                            <HeaderStyle  HorizontalAlign = "Center"></HeaderStyle>
                                                            <Footer><RowLayoutColumnInfo OriginX="6" /></Footer>
                                                        </igtbl:UltraGridColumn>
                                                        
                                                         <igtbl:UltraGridColumn Width="100px" BaseColumnName="UsuarioAutoriza" IsBound="True" Key="UsuarioAutoriza" CellMultiline="No" Hidden ="true">
                                                            <Header Caption="UsuarioAutoriza">
                                                                <RowLayoutColumnInfo OriginX="7" />
                                                            </Header>
                                                            <HeaderStyle  HorizontalAlign = "Center"></HeaderStyle>
                                                            <Footer><RowLayoutColumnInfo OriginX="7" /></Footer>
                                                        </igtbl:UltraGridColumn>
                                                        
                                                        <igtbl:UltraGridColumn Width="100px" BaseColumnName="FechaAutorizacion" IsBound="True" Key="FechaAutorizacion" CellMultiline="No" Hidden ="true">
                                                            <Header Caption="FechaAutorizacion">
                                                                <RowLayoutColumnInfo OriginX="8" />
                                                            </Header>
                                                            <HeaderStyle  HorizontalAlign = "Center"></HeaderStyle>
                                                            <Footer><RowLayoutColumnInfo OriginX="8" /></Footer>
                                                        </igtbl:UltraGridColumn>
                                                        
                                                        <igtbl:UltraGridColumn Width="120px" BaseColumnName="Autorizacion" IsBound="True" Key="Autorizacion" Type="CheckBox" CellMultiline="no">
                                                            <Header Caption="Autorización">
                                                                <RowLayoutColumnInfo OriginX="9" />
                                                            </Header>
                                                            <HeaderStyle HorizontalAlign = "Center"></HeaderStyle>
                                                            <CellStyle HorizontalAlign="Center"></CellStyle>
                                                            <Footer><RowLayoutColumnInfo OriginX="9" /></Footer>
                                                        </igtbl:UltraGridColumn>
                                                        
                                                         <igtbl:UltraGridColumn Width="70px" BaseColumnName="Activo" IsBound="True" Key="Activo" Type="CheckBox" CellMultiline="no">
                                                            <Header Caption="Activo" >
                                                                <RowLayoutColumnInfo OriginX="10" />
                                                            </Header>
                                                            <HeaderStyle HorizontalAlign = "Center"></HeaderStyle>
                                                            <CellStyle HorizontalAlign="Center"></CellStyle>
                                                            <Footer><RowLayoutColumnInfo OriginX="10" /></Footer>
                                                         </igtbl:UltraGridColumn>
                                                         
                                                        <igtbl:UltraGridColumn Width="120px" BaseColumnName="ExceptionMessage" IsBound="True" Key="ExceptionMessage" CellMultiline="No" Hidden ="true">
                                                            <Header Caption="ExceptionMessage">
                                                                <RowLayoutColumnInfo OriginX="11" />
                                                            </Header>
                                                            <HeaderStyle  HorizontalAlign = "Center"></HeaderStyle>
                                                            <Footer><RowLayoutColumnInfo OriginX="11" /></Footer>
                                                        </igtbl:UltraGridColumn>
                                                        
                                                    </Columns>
                                                    <RowEditTemplate >
                                                        <table style="font-family: Arial; text-align: center">
                                                             <tr>
                                                              <!--  <td class ="textos_01">
                                                                    Proceso:
                                                                </td>
                                                                <td class ="textos_01">
                                                                   
                                                                    <input id="ProcesoT" columnkey="DesProceso" style="width: 150px; type="text"   disabled="disabled">
                                                                </td>-->
                                                            </tr>
                                                              <tr>
                                                                <td class ="textos_01">
                                                                    Area:
                                                                </td>
                                                                <td class ="textos_01">
                                                                    <input type="hidden" id="CodAreaH" columnkey="CodArea" />
                                                                   <select  id="CodAreaT" width="200px" columnkey="CodArea">
                                                                    </select>
                                                                    
                                                                   
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td class ="textos_01">
                                                                <input type="hidden" id="CodCondicionOperacionH" columnkey="CodCondicionOperacion" />
                                                           
                                                                    Fecha:
                                                                   
                                                                </td>
                                                                <td class ="textos_01">
                                                                
                                                                <input type="text"   id="FechaT" value="<%#(System.DateTime.Today.ToShortDateString())%>"   disabled="disabled"/>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td class ="textos_01">
                                                                    Temperatura:
                                                                </td>
                                                                <td class ="textos_01">
                                                                    <input type="hidden" id="TemperaturaH" columnkey="Temperatura" />
                                                                
                                                                    <input id="TemperaturaT" columnkey="Temperatura"  type="text"  disabled="disabled" >
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td class ="textos_01">
                                                                    Humedad:
                                                                </td>
                                                                <td class ="textos_01">
                                                                    <input type="hidden" id="HumedadH" columnkey="Humedad" />
                                                                    <input id="HumedadT" columnkey="Humedad" style="width: 150px; type="text"   disabled="disabled">
                                                                </td>
                                                            </tr>
                                                            
                                                                    <input type="hidden" id="AutorizacionH" columnkey="Autorizacion" />
                                                                    <input id="AutorizacionT"  columnkey="Autorizacion" style="width: 15px; text-align: left;" type="checkbox" >
                                                              
                                                                    <input type="hidden" id="ActivoH" columnkey="Activo" />
                                                                    <input id="ActivoT" type ="hidden" columnkey="Activo" style="width: 15px; text-align: left;" type="checkbox" disabled="disabled">
                                                                
                                                            <tr>
                                                                
                                                                <td colspan="2" align="center">
                                                                    <p align="center">
                                                                        <input id="GuardarT" onclick="ok(event)" class="Boton_01" style="width: 75px;" type="button" 
                                                                            value="Guardar"  disabled="disabled" > </input>
                                                                        <input id="CancelT" onclick="cancel(event)" class="Boton_01" style="width: 75px;" type= "button"
                                                                            value="Cancelar"  > </input >
                                                                       <input id="AutorizarT" onclick="autorizar(event)" class="Boton_01" style="width: 75px;" type= "button"
                                                                            value="Autorizar"> </input>
                                                                       
                                                                    </p>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </RowEditTemplate>
                                                    <RowTemplateStyle Height="210px"  Width="330" BackColor="White" BorderColor="White" BorderStyle="Ridge"></RowTemplateStyle>
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
                                                 AfterRowTemplateOpenHandler="AfterRowTemplateOpen"  DblClickHandler="AfterRowTemplateOpen"
                                                 BeforeRowTemplateOpenHandler="BeforeRowTemplateOpen" />
                                            </DisplayLayout>
                                        </igtbl:UltraWebGrid>
                                    </td>
                           </tr>
                       </tbody>
                   </table>
                   
                    <input type="hidden" id="hddPlanta" runat="server" />
                    <input type="hidden" id="hddCodCondicionOperacion" runat="server" />
                    <input type="hidden" id="hddCodArea" runat="server" />
                    <input type="hidden" id="hddTemperatura" runat="server" />
                    <input type="hidden" id="hddHumedad" runat="server" />
 
                    <input type="hidden" id="hddAutorizacion" runat="server" />
                    <input type="hidden" id="hddActivo" runat="server" />
                    
                    <asp:Button ID="BotonGuardar" runat="server" Text="Button" CssClass="hidden" OnClick="BotonGuardar_click" />
                     <asp:Button ID="BotonAutorizar" runat="server" Text="Button" CssClass="hidden"  OnClick="BotonAutorizar_click"/>

                    
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


                            


                              function BeforeRowTemplateOpen(gridName, rowId) {

                                  SwitchDiv(false);
                                  $("#CodAreaT").html($("#<%=cmbAreaH.ClientID %>").html());

                              }

                              function AfterRowTemplateOpen(gridName, rowId) {
                                  SwitchDiv(false);
                                  var r = $("#CodAreaH").val() ? $("#CodAreaH").val() : $("#CodAreaT").val();

                              }

                                       

                              function SwitchDiv(nuevo) {
                                  if (nuevo) {
                                    
                                      document.getElementById("FechaT").disabled = "";
                                      document.getElementById("TemperaturaT").disabled = "";
                                      document.getElementById("HumedadT").disabled = "";
             
                                      document.getElementById("AutorizacionT").disabled = "";
                                      document.getElementById("ActivoT").disabled = "";
                                      document.getElementById("GuardarT").disabled = "";
                                      document.getElementById("AutorizarT").className = "hidden";
                                 
                                  } else {
                                  document.getElementById("FechaT").disabled = "disabled";
                                  document.getElementById("TemperaturaT").disabled = "disabled";
                                  document.getElementById("HumedadT").disabled = "disabled";
                         
                 
                                  document.getElementById("ActivoT").disabled = "disabled";
                                  document.getElementById("GuardarT").disabled = "disabled";

                                  var Autorizacion = $("#AutorizacionT").attr('checked');
                                  if (Autorizacion == true) {
                                      document.getElementById("AutorizacionT").className = "hidden";
                                      document.getElementById("AutorizarT").className = "hidden";
                                  }
                                  else {
                                      document.getElementById("AutorizacionT").className = "hidden";
                                      document.getElementById("AutorizarT").className = "Boton_01";
                                  }
                      
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
                                                  <input id="nombre" value="Condicion de operacion" style="width: 200px;" type="text" runat="server"  />  
                                                    
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
</asp:Content>
