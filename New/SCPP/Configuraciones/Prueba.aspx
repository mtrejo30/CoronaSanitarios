<%@ Page Language="C#" MasterPageFile="~/ControlPisoLamosa.Master" Title="Control de piso - Pruebas" AutoEventWireup="true" 
CodeBehind="Prueba.aspx.cs" Inherits="LAMOSA.SCPP.Client.View.Administrador.Configuraciones.Prueba" %>

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
                    <asp:Label ID="lblTitulo" runat="server"  Text="Pruebas" ></asp:Label><br/>
                </td> 
            </tr>
            <tr><td style="height:10px" colspan= "3"></td></tr>
            <tr>
                <td style="width:10px;" rowspan="2"></td>
                <td rowspan="2" valign="top" class="leftarea" >
                    <div id="navcontainer">
                        <ul id="navlist">
                            <li><a href="javascript:ListItemSelected(1,'')" ID="LAddNew" runat="server"><img src="../Imagenes/Nuevo.png" alt="Nuevo registro" style="border:0px;" /> Nuevo registro</a></li>
                            <li><a href="javascript:ListItemSelected(2,'')" ID="LExport" runat="server"><img src="../Imagenes/Exportar.png" alt="Exportar tabla" style="border:0px;" /> Exportar tabla</a></li>
                            <li><a href="javascript:history.back();" onclick="history.go(-1)"><img src="../Imagenes/Regresar.png" alt="Regresar" style="border:0px;" /> Regresar</a></li>
                        </ul>
                    </div>
                </td>
                <td>&nbsp;</td>
                <td>           
                    <igmisc:WebAsyncRefreshPanel ID="WebAsyncRefreshPanel1" runat="server">
                    <table style="height: 100px; width: 800px" >
                        <tbody>
                            <tr>
                              <td>
                                  <table>
                                        
                                    <tr>
                                     <td class="textos_01">
                                        Proceso:
                                      </td>
                                      <td  class="textos_01">
                                      <asp:DropDownList ID="cmbProceso" runat="server" CssClass="textosd" AutoPostBack="true" OnSelectedIndexChanged="cmbProceso_SelectedIndexChanged">
                                       
                                        </asp:DropDownList>
                                        <asp:DropDownList ID="cmbProcesoH" runat="server" CssClass="hidden" >
                                       
                                        </asp:DropDownList>
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
                                            function ok(event) {
                                                var ClavePrueba = $("#ClavePruebaT").val();
                                                var DesPrueba = $("#DesPruebaT").val();
                                                var Proceso = $("#ddlProceso").val();
                                                var ProcesoFin = $("#ddlProcesoFin").val();
                                                var ResidenciaMax = $("#ResidenciaMaxT").val();

                                                if (DesPrueba != "" && Proceso != "" && ProcesoFin != "" && ResidenciaMax != "") {
                                                    if (confirm('¿Desea guardar cambios?')) {
                                                        //asignar valores a los hidden
                                                        $("#<%=hddClavePrueba.ClientID%>").val(ClavePrueba);
                                                        $("#<%=hddDesPrueba.ClientID%>").val(DesPrueba);
                                                        $("#<%=hddProceso.ClientID%>").val(Proceso);
                                                        $("#<%=hddProcesoFin.ClientID%>").val(ProcesoFin);
                                                        $("#<%=hddResidenciaMax.ClientID%>").val(ResidenciaMax);
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
                                                var ClavePrueba = $("#ClavePruebaT").val();
                                                var DesPrueba = $("#DesPruebaT").val();
                                                var Proceso = $("#ddlProceso").val();
                                                var ProcesoFin = $("#ddlProcesoFin").val();
                                                var ResidenciaMax = $("#ResidenciaMaxT").val();

                                                var oDesPrueba = $("#DesPruebaH").val();
                                                var oCodProceso = $("#CodProcesoH").val();
                                                var oCodProceso = $("#CodProcesoH").val();
                                                var oCodProcesoFin = $("#CodProcesoFinH").val();
                                                var oResidenciaMax = $("#ResidenciaMaxH").val();
                                              
                                                var edit = true;
                                                beforeClose = false;
                                                if (DesPrueba != oDesPrueba || Proceso != oCodProceso || ProcesoFin != oCodProcesoFin || ResidenciaMax != oResidenciaMax) {
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
                                                var ClavePrueba = $("#ClavePruebaT").val();
                                                var DesPrueba = $("#DesPruebaT").val();
                                                var Proceso = $("#ddlProceso").val();
                                                var ProcesoFin = $("#ddlProcesoFin").val();
                                                var ResidenciaMax = $("ResidenciaMaxT").val();

                                                if (confirm('¿Desea eliminar registro?')) {
                                                    $("#<%=hddClavePrueba.ClientID%>").val(ClavePrueba);

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

                                            
                                            function ListItemSelected(idList, varList) {
                                                if (idList == 1) {//Nuevo   
                                                    igtbl_addNew("<%=UltraWebGrid1.ClientID%>", 0).editRow();
                                                }
                                            }
                                            
                                         
                                        </script>
                                        <igtbl:UltraWebGrid   ID="UltraWebGrid1" runat="server" CaptionAlign="Left" EnableAppStyling="False" OnPageIndexChanged = "cambio_pagina" > 
                                            <Bands>
                                                <igtbl:UltraGridBand>
                                                    <Columns>
                                            
                                                       <igtbl:UltraGridColumn Width="120px" BaseColumnName="ClavePrueba"  IsBound="True" Key="ClavePrueba" >
                                                            <Header Caption="Clave prueba"></Header>
                                                            <HeaderStyle  HorizontalAlign = "Center"></HeaderStyle>
                                                        </igtbl:UltraGridColumn>
                                                        
                                                        <igtbl:UltraGridColumn Width="120px" BaseColumnName="DesPrueba" IsBound="True" Key="DesPrueba" CellMultiline="Yes">
                                                            <Header Caption="Descripción">
                                                                <RowLayoutColumnInfo OriginX="1" />
                                                            </Header>
                                                            <HeaderStyle  HorizontalAlign = "Center"></HeaderStyle>
                                                            <Footer><RowLayoutColumnInfo OriginX="1" /></Footer>
                                                        </igtbl:UltraGridColumn>
                                                        <igtbl:UltraGridColumn Width="150px" BaseColumnName="DesMolde" IsBound="True" Key="DesMolde" CellMultiline="Yes">
                                                            <Header Caption="Descripción">
                                                                <RowLayoutColumnInfo OriginX="2" />
                                                            </Header>
                                                            <HeaderStyle  HorizontalAlign = "Center"></HeaderStyle>
                                                            <Footer><RowLayoutColumnInfo OriginX="2" /></Footer>
                                                        </igtbl:UltraGridColumn>
                                                        <igtbl:UltraGridColumn Width="125px" BaseColumnName="NumImpresiones" IsBound="True" Key="NumImpresiones" CellMultiline="Yes">
                                                            <Header Caption="Núm. Impresiones">
                                                                <RowLayoutColumnInfo OriginX="3" />
                                                            </Header>
                                                            <HeaderStyle  HorizontalAlign = "Center"></HeaderStyle>
                                                            <Footer><RowLayoutColumnInfo OriginX="3" /></Footer>
                                                        </igtbl:UltraGridColumn>
                                                        
                                                         <igtbl:UltraGridColumn Width="120px" BaseColumnName="FechaRegistro" IsBound="True" Key="FechaRegistro" CellMultiline="Yes">
                                                            <Header Caption="Fecha">
                                                                <RowLayoutColumnInfo OriginX="4" />
                                                            </Header>
                                                            <HeaderStyle  HorizontalAlign = "Center"></HeaderStyle>
                                                            <Footer><RowLayoutColumnInfo OriginX="4" /></Footer>
                                                        </igtbl:UltraGridColumn>
                                                        
                                                        <igtbl:UltraGridColumn Hidden="true" Width="120px" BaseColumnName="FechaBaja" IsBound="True" Key="FechaBaja" CellMultiline="Yes">
                                                            <Header Caption="FechaBaja">
                                                                <RowLayoutColumnInfo OriginX="5" />
                                                            </Header>
                                                            <HeaderStyle  HorizontalAlign = "Center"></HeaderStyle>
                                                            <Footer><RowLayoutColumnInfo OriginX="5" /></Footer>
                                                        </igtbl:UltraGridColumn>
                                                        
                                                           <igtbl:UltraGridColumn Width="70px" BaseColumnName="Activo" IsBound="True" Key="Activo"  Type="CheckBox" CellMultiline="no" AllowUpdate="No">
                                                            <Header Caption="  Activo  ">
                                                                <RowLayoutColumnInfo OriginX="6" />
                                                            </Header>
                                                            <HeaderStyle  HorizontalAlign = "Center"></HeaderStyle>
                                                            <Footer><RowLayoutColumnInfo OriginX="6" /></Footer>
                                                        </igtbl:UltraGridColumn>
                                                        
                                                            <igtbl:UltraGridColumn Hidden="true" Width="100px" BaseColumnName="ExceptionMessage" IsBound="True"   CellMultiline="Yes">
                                                                    <Header Caption="ExceptionMessage">
                                                                        <RowLayoutColumnInfo OriginX="7" />
                                                                    </Header>
                                                                    <HeaderStyle HorizontalAlign = "Center"></HeaderStyle>
                                                                    <CellStyle HorizontalAlign="Center"></CellStyle>
                                                                    <Footer><RowLayoutColumnInfo OriginX="7" /></Footer>
                                                                </igtbl:UltraGridColumn>
                                      
                                                      
                                                    </Columns>
                                                    <RowEditTemplate>
                                                        <table style="font-family: Arial; text-align: center">
                                                          
                                                            <tr>
                                                                <td class ="textos_01">
                                                                    Clave prueba:
                                                                </td>
                                                                <td class ="textos_01">
                                                                    <input type="hidden" id="ClavePruebaH" columnkey="ClavePrueba" />
                                                                    <input id="ClavePruebaT" columnkey="ClavePrueba" style="width: 150px;" type="text"  disabled="disabled" >
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td class ="textos_01">
                                                                    Descripci&oacute;n:
                                                                </td>
                                                                <td class ="textos_01">
                                                                    <input type="hidden" id="DesPruebaH" columnkey="DesPrueba" />
                                                                    <input id="DesPruebaT" columnkey="DesPrueba" style="width: 150px; height: 50px" type="text" >
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td class ="textos_01">
                                                                    Proceso:
                                                                </td>
                                                                <td class ="textos_01">
                                                                    <input type="hidden" id="CodProcesoH" columnkey="CodProceso" />
                                                                    
                                                                     <select  id="ddlProceso" width="200px" columnkey="CodProceso">
                                                                    </select>
                                                                </td>
                                                            </tr>
                                                         
                                                            <tr>
                                                            
                                                             <tr>
                                                                <td class ="textos_01">
                                                                    Proceso Final:
                                                                </td>
                                                                <td class ="textos_01">
                                                                    <input type="hidden" id="CodProcesoFinH" columnkey="CodProcesoFin" />
                                                                   <select  id="ddlProcesoFin" width="200px" columnkey="CodProcesoFin">
                                                                    </select>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                            
                                                             <tr>
                                                                <td class ="textos_01">
                                                                    Residencia Max:
                                                                </td>
                                                                <td class ="textos_01">
                                                                    <input type="hidden" id="ResidenciaMaxH" columnkey="ResidenciaMax" />
                                                                    <input id="ResidenciaMaxT" maxlength="12" columnkey="ResidenciaMax" style="width: 150px;" type="text" class ="mayus" />
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                            
                                                             
                                                           
                                                            <tr>
                                                                
                                                                <td colspan="2" align="center">
                                                                    <p align="center">
                                                                        <input id="Button1" onclick="ok(event)" class="Boton_01" style="width: 75px;" type="button" 
                                                                            value="Guardar"> </input>
                                                                        <input id="Button2" onclick="cancel(event)" class="Boton_01" style="width: 75px;" type= "button"
                                                                            value="Cancelar"> </input>
                                                                        <input id="Button3" onclick="eliminar(event)" class="Boton_01" style="width: 75px;" type="button"
                                                                            value="Eliminar"> </input>
                                                                    </p>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </RowEditTemplate>
                                                    <RowTemplateStyle Height="250px"  Width="310" BackColor="White" BorderColor="White" BorderStyle="Ridge"></RowTemplateStyle>
                                                    <AddNewRow Visible="NotSet" View="NotSet"></AddNewRow>
                                                </igtbl:UltraGridBand>
                                            </Bands>
                                            <DisplayLayout Name="UltraWebGrid1" AllowDeleteDefault="Yes" AllowUpdateDefault="RowTemplateOnly"
                                                NoDataMessage="" RowHeightDefault="20px" SelectTypeRowDefault="Single" TableLayout="Fixed" Version="3.00"
                                                CellClickActionDefault="RowSelect" LoadOnDemand="Xml" AllowAddNewDefault="Yes"
                                                AllowColSizingDefault="Free" AllowSortingDefault="OnClient" CellPaddingDefault="1"
                                                HeaderClickActionDefault="SortSingle" RowSelectorsDefault="No" RowSizingDefault="Free" >
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
                                                BeforeRowTemplateOpenHandler="BeforeRowTemplateOpen"     AfterRowTemplateOpenHandler="AfterRowTemplateOpen"   
                                                  />
                                            </DisplayLayout>
                                        </igtbl:UltraWebGrid>
                     
                                    </td>
                           </tr>
                       </tbody>
                   </table>
                   
                        <input type="hidden" id="hddClavePrueba" runat="server" />
                        <input type="hidden" id="hddDesPrueba" runat="server" />
                        <input type="hidden" id="hddProceso" runat="server" />
                        <input type="hidden" id="hddProcesoFin" runat="server" />
                        <input type="hidden" id="hddResidenciaMax" runat="server" />
                        
                        <asp:Button ID="BotonGuardar" runat="server" Text="Button" CssClass="hidden" OnClick="BotonGuardar_click"  />
           <asp:Button ID="BotonEliminar" runat="server" Text="Button" CssClass="hidden" OnClick="BotonEliminar_click" />
                        
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

                                  $("#ddlProceso").html($("#<%=cmbProcesoH.ClientID %>").html());
                                  $("#ddlProcesoFin").html($("#<%=cmbProcesoH.ClientID %>").html());

                              }

                              function AfterRowTemplateOpen(gridName, rowId) {

                                  var r = $("#CodProcesoH").val() ? $("#CodProcesoH").val() : $("#ddlProceso").val();
                                  var r = $("#CodProcesoFinH").val() ? $("#CodProcesoFinH").val() : $("#ddlProcesoFin").val();
                              }

                              function SwitchDiv(nuevo) {


                                  if (nuevo) {


                                     
                                      document.getElementById("Button3").className = "hidden";

                                  } else {

                                    
                                      document.getElementById("Button3").className = "Boton_01";
                                  }

                              }
                    </script>
                    <ig:WebDialogWindow ID="WebDialogWindow1" runat="server" InitialLocation="Centered"
                        Height="100px" Width="400px" Modal="true" WindowState="Hidden" Font-Size="10px">
                        <ContentPane BackColor="#FAFAFA">
                            <Template>
                                <div style="padding: 5px;">
                                    <table cellpadding="0" cellspacing="0" align="center" style="text-align: center; width: 100%">
                                        <tr>
                                            <td align="center" colspan="3">
                                                <asp:Panel ID="pnlExporta" runat="server" Width="300px">
                                                    <asp:DropDownList ID="ddlSeleccion" runat="server" CssClass="AreaBox_02" Width="180px">                                                       
                                                        <asp:ListItem>MS Excel (XLS)</asp:ListItem>
                                                    </asp:DropDownList>
                                                    <asp:Button ID="btnExporta" runat="server" CssClass="Boton_01" onclick= "btnExporta_Click" Text="Exportar" Width="115px" />
                                                </asp:Panel>
                                                &nbsp; &nbsp;
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
                </td>
            </tr>
            <tr>
              
                <td>
                    <igtblexp:UltraWebGridExcelExporter ID="ExpPruebas" WorksheetName="Pruebas" DownloadName="ReportePruebas.XLS" runat='server'>
                    </igtblexp:UltraWebGridExcelExporter>
                </td>
            </tr>
        </table>
        
           <asp:HiddenField ID="Planta" runat="server" />
        
        
        
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
