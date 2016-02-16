<%@ Page Language="C#" MasterPageFile="~/ControlPisoLamosa.Master" Title="Control de piso - Turnos" AutoEventWireup="true" 
CodeBehind="RolesUsuarios.aspx.cs" Inherits="LAMOSA.SCPP.Client.View.Administrador.Seguridad.RolesUsuarios" %>

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
                    <asp:Label ID="lblTitulo" runat="server"  Text="Roles de Usuarios" ></asp:Label><br/>
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
                                <td> <asp:DropDownList  CssClass="hidden" ID="cmbPlanta" runat="server">
                                                    </asp:DropDownList>
                                        <script src="../FuncionesJS/jquery-1.4.2.js" type="text/javascript"></script>
                                        <script type="text/javascript">
                                            var beforeClose = true;
                                            var clickcancel = true;
                                            function ok(event) {

                                                var ClaveRol = $("#ClaveRolT").val();
                                                var Descripcion = $("#DescripcionRolT").val();
                                                var CodPlanta = $("#ddlPlanta").val();
                                                var Activo = "'" + $("#ActivoT").attr('checked') + "'";
                                                if (Descripcion != "" ) {
                                                    if (confirm('¿Desea guardar cambios?')) {
                                                        //asignar valores a los hidden 
                                                        $("#<%=hddClaveRol.ClientID%>").val(ClaveRol);
                                                        $("#<%=hddDescripcionRol.ClientID%>").val(Descripcion);
                                                        $("#<%=hddCodPlanta.ClientID%>").val(CodPlanta);
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

                                            function cancel(event) {

                                                var ClaveRol = $("#ClaveRolT").val();
                                                var Descripcion = $("#DescripcionRolT").val();
                                                var Activo = "'" + $("#ActivoT").attr('checked') + "'";

                                                var oClaveRol = $("#ClaveRolH").val();
                                                var oDescripcion = $("#DescripcionRolH").val();
                                                var oActivo = "'" + $("#ActivoH").attr('checked') + "'";
                                                
                                                var edit = true;
                                                beforeClose = false;
                                                if (Descripcion != oDescripcion || Activo != oActivo) {
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

                                                var ClaveRol = $("#ClaveRolT").val();
                                                var Descripcion = $("#DescripcionRolT").val();
                                                var Activo = "'" + $("#ActivoT").attr('checked') + "'";
                                                if (confirm('¿Desea eliminar registro?')) {
                                                    $("#<%=hddClaveRol.ClientID%>").val(ClaveRol);

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

                                            function BeforeRowTemplateOpen(gridName, rowId) {

                                                SwitchDiv(false);
                                                $("#ddlPlanta").html($("#<%=cmbPlanta.ClientID %>").html());

                                            }
                                        </script>
                                        <igtbl:UltraWebGrid ID="UltraWebGrid1" runat="server" CaptionAlign="Left" EnableAppStyling="False" > 
                                            <Bands>
                                                <igtbl:UltraGridBand>
                                                    <Columns>
                                                        <igtbl:UltraGridColumn Width="100px" BaseColumnName="ClaveRol" DataType="System.Int32" IsBound="True" Key="ClaveRol" Hidden="False">
                                                            <Header Caption="Clave rol">
                                                              <RowLayoutColumnInfo OriginX="0" />
                                                            </Header>
                                                            <HeaderStyle  HorizontalAlign = "Center"></HeaderStyle>
                                                            <Footer><RowLayoutColumnInfo OriginX="0" /></Footer>
                                                        </igtbl:UltraGridColumn>
                                                        
                                                        <igtbl:UltraGridColumn Width="200px" BaseColumnName="DescripcionRol" IsBound="True" Key="DescripcionRol" CellMultiline="Yes">
                                                            <Header Caption="Descripción">
                                                                <RowLayoutColumnInfo OriginX="1" />
                                                            </Header>
                                                            <HeaderStyle  HorizontalAlign = "Center"></HeaderStyle>
                                                            <Footer><RowLayoutColumnInfo OriginX="1" /></Footer>
                                                        </igtbl:UltraGridColumn>
                                                      <igtbl:UltraGridColumn Width="70px" BaseColumnName="Activo" IsBound="True" Key="Activo" Type="CheckBox" CellMultiline="no">
                                                            <Header Caption="Activo">
                                                                <RowLayoutColumnInfo OriginX="2" />
                                                            </Header>
                                                            <HeaderStyle HorizontalAlign = "Center"></HeaderStyle>
                                                            <CellStyle HorizontalAlign="Center"></CellStyle>
                                                            <Footer><RowLayoutColumnInfo OriginX="2" /></Footer>
                                                        </igtbl:UltraGridColumn>
                                                         <igtbl:UltraGridColumn Width="150px" BaseColumnName="ExceptionMessage" IsBound="True" Key="ExceptionMessage" CellMultiline="Yes" Hidden = "true">
                                                            <Header Caption="ExceptionMessage">
                                                                <RowLayoutColumnInfo OriginX="3" />
                                                            </Header>
                                                            <HeaderStyle  HorizontalAlign = "Center"></HeaderStyle>
                                                            <Footer><RowLayoutColumnInfo OriginX="3" /></Footer>
                                                        </igtbl:UltraGridColumn>
                                               
                                                    </Columns>
                                                    <RowEditTemplate>
                                                        <table style="font-family: Arial; text-align: center">
                                                         <tr id="planta">
                                                                <td class ="textos_01">
                                                                   Planta:
                                                                </td >
                                                                <td class ="textos_01">
                                                                    
                                                                    <input type="hidden" id="CodPlantaH" name="CodPlantaH" />
                                                                     <select id="ddlPlanta" width="200px" columnkey="CodPlanta">
                                                                    </select>
                                                                   

                                                                </td>
                                                            </tr>
                                                            <tr id="clave">
                                                                <td class ="textos_01">
                                                                Clave rol:
                                                                </td>
                                                                <td class ="textos_01">
                                                                    <input type="hidden" id="ClaveRolH" columnkey="ClaveRol" />
                                                                    <input  id="ClaveRolT" columnkey="ClaveRol" style="width: 150px;" type="text"   disabled="disabled"  >
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td class ="textos_01">
                                                                    Descripci&oacute;n:
                                                                </td>
                                                                <td class ="textos_01">
                                                                    <input type="hidden" id="DescripcionRolH" columnkey="DescripcionRol" />
                                                                    <input id="DescripcionRolT" columnkey="DescripcionRol" style="width: 150px; height: 50px" type="text" >
                                                                </td>
                                                            </tr>
                                                          
                                                        
                                                            <tr id="activo">
                                                                <td class ="textos_01">
                                                                      Activo:
                                                                </td>
                                                                <td align="left">
                                                                    <input type="hidden" id="ActivoH" columnkey="Activo" />
                                                                    <input id="ActivoT" columnkey="Activo" style="width: 15px; text-align: left;" type="checkbox" >
                                                                </td>
                                                            </tr>
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
                                                    <RowTemplateStyle Height="200px"  Width="300" BackColor="White" BorderColor="White" BorderStyle="Ridge"></RowTemplateStyle>
                                                    <AddNewRow Visible="NotSet" View="NotSet"></AddNewRow>
                                                </igtbl:UltraGridBand>
                                            </Bands>
                                            <DisplayLayout Name="UltraWebGrid2" AllowDeleteDefault="Yes" AllowUpdateDefault="RowTemplateOnly"
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
                                                <ClientSideEvents  BeforeRowTemplateCloseHandler ="BeforeRowTemplateCloseHandler" BeforeRowTemplateOpenHandler="BeforeRowTemplateOpen" />
                                            </DisplayLayout>
                                        </igtbl:UltraWebGrid>
                                    </td>
                           </tr>
                       </tbody>
                   </table>
                     <input type="hidden" id="hddCodPlanta" runat="server" />
                       <input type="hidden" id="hddClaveRol" runat="server" />
                        <input type="hidden" id="hddDescripcionRol" runat="server" />
                     
                        <input type="hidden" id="hddActivo" runat="server" />
                        <asp:Button ID="BotonGuardar" runat="server" Text="Button" CssClass="hidden" OnClick="BotonGuardar_click" />
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

                              function obtenerRol() {
                              }


                              function BeforeRowTemplateOpen(gridName, rowId) {
                                  SwitchDiv(false);
                                  $("#ddlPlanta").html($("#<%=cmbPlanta.ClientID %>").html());
                              }

                              function SwitchDiv(nuevo) {
                                  if (nuevo) {

                                      document.getElementById("Button3").className = "hidden";
                                      document.getElementById("clave").className = "hidden";
                                      document.getElementById("activo").className = "hidden";
                                      document.getElementById("planta").className = " ";

                                  } else {
                                      

                                  document.getElementById("Button3").className = "Boton_01";
                                  document.getElementById("clave").className = " ";
                                  document.getElementById("activo").className = " ";
                                  document.getElementById("planta").className = "hidden";

                                  }

                              }

                              function AfterRowTemplateOpen(gridName, rowId) {
                                  SwitchDiv(false);
                                  var r = $("#CodPlantaH").val() ? $("#CodPlantaH").val() : $("#ddlPlanta").val();

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
                                                  <input id="nombre" value="ReporteRol" style="width: 200px;" type="text" runat="server"  />  
                                                    
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
