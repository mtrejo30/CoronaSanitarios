<%@ Page Language="C#" MasterPageFile="~/ControlPisoLamosa.Master" AutoEventWireup="true" 
    CodeBehind="ZonaDefectos.aspx.cs" Inherits="LAMOSA.SCPP.Client.View.Administrador.Administracion.ZonaDefectos" Title="Control de piso - Zona de defectos" %>
    
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
                    <asp:Label ID="lblTitulo" runat="server"  Text="Zonas de defectos" ></asp:Label><br/>
                </td> 
            </tr>
            <tr><td style="height:10px" colspan= "3"></td></tr>
            <tr>
                <td style="width:10px;" rowspan="2"></td>
                <td rowspan="2" valign="top" class="leftarea" style="width:100px">
                    <div id="navcontainer">
                        <ul id="navlist">
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
                                    <td class="textos_01">  
                                        Tipo de artículo:
                                    </td>
                                    <td  class="textos_01">
                                        <asp:DropDownList ID="cmbTipoAr" runat="server" CssClass="textosd" AutoPostBack="true" OnSelectedIndexChanged="cmbProceso_SelectedIndexChanged" >
                          
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                 </table>
                                   </td>
                              </tr> 
                                <tr>
                                    <td>
                                        <script src="../FuncionesJS/jquery-1.4.2.js" type="text/javascript"></script>
                                        
                                        <igtbl:UltraWebGrid ID="UltraWebGrid1" runat="server" CaptionAlign="Left" EnableAppStyling="False"
                                        OnPageIndexChanged = "cambio_pagina"  > 
                                            <Bands>
                                                <igtbl:UltraGridBand>
                                                    <Columns>
                                                        <igtbl:UltraGridColumn Width="100px" BaseColumnName="ClaveUnica" DataType="System.Int32" IsBound="True" Key="ClaveUnica" Hidden="False">
                                                            <Header Caption="Clave única"></Header>
                                                            <HeaderStyle  HorizontalAlign = "Center"></HeaderStyle>
                                                        </igtbl:UltraGridColumn>
                                                        
                                                         <igtbl:UltraGridColumn Width="150px" BaseColumnName="ClaveZonaDefecto" IsBound="True" Key="ClaveZonaDefecto" CellMultiline="Yes">
                                                            <Header Caption="Clave zona defecto">
                                                                <RowLayoutColumnInfo OriginX="1" />
                                                            </Header>
                                                            <HeaderStyle  HorizontalAlign = "Center"></HeaderStyle>
                                                            <Footer><RowLayoutColumnInfo OriginX="1" /></Footer>
                                                        </igtbl:UltraGridColumn>
                                                        
                                                        <igtbl:UltraGridColumn Width="150px" BaseColumnName="Descripcion" IsBound="True" Key="Descripcion" CellMultiline="Yes">
                                                            <Header Caption="Descripción">
                                                                <RowLayoutColumnInfo OriginX="2" />
                                                            </Header>
                                                            <HeaderStyle  HorizontalAlign = "Center"></HeaderStyle>
                                                            <Footer><RowLayoutColumnInfo OriginX="2" /></Footer>
                                                        </igtbl:UltraGridColumn>
                                                        
                                                         <igtbl:UltraGridColumn Width="120px" BaseColumnName="TipoArticulo" IsBound="True" Key="TipoArticulo" CellMultiline="Yes">
                                                            <Header Caption="Tipo artículo">
                                                                <RowLayoutColumnInfo OriginX="3" />
                                                            </Header>
                                                            <HeaderStyle  HorizontalAlign = "Center"></HeaderStyle>
                                                            <Footer><RowLayoutColumnInfo OriginX="3" /></Footer>
                                                        </igtbl:UltraGridColumn>
                                                        
                                                        <igtbl:UltraGridColumn  Hidden="true" Width="120px" BaseColumnName="ClaveTipoArticulo" IsBound="True" Key="ClaveTipoArticulo" CellMultiline="Yes">
                                                            <Header Caption="ClaveTipoArticulo">
                                                                <RowLayoutColumnInfo OriginX="4" />
                                                            </Header>
                                                            <HeaderStyle  HorizontalAlign = "Center"></HeaderStyle>
                                                            <Footer><RowLayoutColumnInfo OriginX="4" /></Footer>
                                                        </igtbl:UltraGridColumn>
                                                        
                                                        <igtbl:UltraGridColumn Hidden="true" Width="100px" BaseColumnName="ExceptionMessage" IsBound="True"   CellMultiline="Yes">
                                                                    <Header Caption="ExceptionMessage">
                                                                        <RowLayoutColumnInfo OriginX="5" />
                                                                    </Header>
                                                                    <HeaderStyle HorizontalAlign = "Center"></HeaderStyle>
                                                                    <CellStyle HorizontalAlign="Center"></CellStyle>
                                                                    <Footer><RowLayoutColumnInfo OriginX="5" /></Footer>
                                                                </igtbl:UltraGridColumn>
                                                                
                                                    </Columns>
                                                    
                                                    
                                                    <AddNewRow Visible="NotSet" View="NotSet"></AddNewRow>
                                                </igtbl:UltraGridBand>
                                            </Bands>
                                            <DisplayLayout Name="UltraWebGrid1" AllowDeleteDefault="Yes"  
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
                                                <ClientSideEvents  BeforeRowTemplateCloseHandler ="BeforeRowTemplateCloseHandler"   KeyDownHandler="Action" />
                                            </DisplayLayout>
                                        </igtbl:UltraWebGrid>
                                    </td>
                                </tr>
                            </tbody>
                    </table>
                  </igmisc:WebAsyncRefreshPanel>
                    &nbsp;<script type="text/javascript">
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
                                  //var nuevo = document.getElementById("<%=hddSecurityConstants.ClientID %>").getAttribute('value').indexOf('1');
                                  //   var exporta = document.getElementById("<%=hddSecurityConstants.ClientID %>").getAttribute('value').indexOf('6');

                                   if (idList == 2) {//Exportar
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
                                                  
                                                  <td  class ="textos_01">
                                                     
                                                       Nombre del archivo:
                                                  </td> 
                                                  <td  class ="textos_01">  
                                                  <input id="nombre" value="Zona de defectos" style="width: 200px;" type="text" runat="server"  />  
                                                    
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
                    <igtblexp:UltraWebGridExcelExporter ID="uwgTipoDefe" WorksheetName="Tipo defectos" DownloadName="Reporte.XLS" runat='server'>
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
