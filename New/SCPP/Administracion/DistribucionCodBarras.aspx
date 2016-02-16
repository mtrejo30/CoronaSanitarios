<%@ Page Language="C#" MasterPageFile="~/ControlPisoLamosa.Master" Title="Control de piso - Distribución código de barras" AutoEventWireup="true" 
CodeBehind="DistribucionCodBarras.aspx.cs" Inherits="LAMOSA.SCPP.Client.View.Administrador.Administracion.DistribucionCodBarras" %>




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
                    <asp:Label ID="lblTitulo" runat="server"  Text="Asignación código de barras" ></asp:Label><br/>
                </td> 
            </tr>
            <tr><td style="height:10px" colspan= "3"></td></tr>
            <tr>
                <td style="width:10px;" rowspan="2"></td>
                <td rowspan="2" valign="top" class="leftarea" style="width:100px">
                    <div id="navcontainer">
                        <ul id="navlist">
                            
                            <li><a href="javascript:history.back();" onclick="history.go(-1)"><img src="../Imagenes/Regresar.png" alt="Regresar" style="border:0px;" /> Regresar</a></li>
                        </ul>
                    </div>
                </td><td>&nbsp;</td>
                <td>           
                    <igmisc:WebAsyncRefreshPanel ID="WebAsyncRefreshPanel1" runat="server">
                    <table style="height: 100px; width: 300px">
                        <tbody>
                             <tr>
                                   <td style=" height:40px" class="textos_01">
                                          <table style="width: 300px">
                                          
                                            <tr>
                                                <td class="textos_01">
                                                    Planta:
                                                </td>
                                                <td class="textos_01">
                                                      <asp:DropDownList ID="cmbPlanta" runat="server" CssClass = "textosd">
                                                      <asp:ListItem Value= "1" Text="Planta 1"> </asp:ListItem>
                                                      <asp:ListItem Value= "2" Text="Planta 2"> </asp:ListItem>
                                                      <asp:ListItem Value= "3" Text="Planta 3"> </asp:ListItem>
                                                      <asp:ListItem Value= "4" Text="Benito Ju&aacute;rez"> </asp:ListItem>
                                                      </asp:DropDownList>
                                                </td>
                                              </tr>
                                            <tr>
                                                <td class="textos_01">
                                                    Centro de trabajo:
                                                </td>
                                                  <td  class="textos_01">
                                                    <asp:DropDownList ID="cmbCentro" runat="server"   CssClass="textosd" >
                                                      <asp:ListItem Value= "1" Text="Vaciado de taza"> </asp:ListItem>
                                                      <asp:ListItem Value= "2" Text="Vaciado de pedestal"> </asp:ListItem>
                                                     </asp:DropDownList>
                                                   </td>
                                              </tr>
                                            <tr>
                                                <td class="textos_01">
                                                    Usuario:
                                                </td>
                                                <td class="textos_01">
                                                   <asp:DropDownList ID="cmbRol" runat="server" CssClass="textosd" >
                                                      <asp:ListItem Value= "0" Text="Todos"> </asp:ListItem>
                                                      <asp:ListItem Value= "1" Text="Administrador"> </asp:ListItem>
                                                      <asp:ListItem Value= "2" Text="Sistemas"> </asp:ListItem>
                                                      <asp:ListItem Value= "3" Text="Gerencia"> </asp:ListItem>
                                                    </asp:DropDownList>
                                                </td>
                                              </tr>
                                            <tr>
                                                       <td class="textos_01">  
                                                          Código Inicial:
                                                       </td>
                                                       <td  class="textos_01">  
                                                              <asp:TextBox ID="CodIni" runat="server"   CssClass=textost ></asp:TextBox>
                                                       </td>
                                                </tr>
                                            <tr>
                                                       <td class="textos_01">  
                                                          Cantidad:
                                                       </td>
                                                       <td  class="textos_01">  
                                                              <asp:TextBox ID="Cantidad" runat="server"   CssClass=textost ></asp:TextBox>
                                                       </td>
                                                </tr>
                                            <tr>
                                                       <td class="textos_01">  
                                                          Código Final:
                                                       </td>
                                                       <td  class="textos_01">  
                                                              <asp:TextBox ID="CodFin" runat="server"   CssClass=textost ></asp:TextBox>
                                                       </td>
                                                </tr>
                                            <tr>
                                                       <td class="textos_01">  
                                                          Total:
                                                       </td>
                                                       <td  class="textos_01">  
                                                              
                                                             <asp:TextBox ID="Total" runat="server" Enabled="False" CssClass="textost" ></asp:TextBox>
                                                       </td>
                                                </tr>
                                            <tr>
                                             <td align="left" class="textos_01">
                                                <input id="Agregar" type="button" class="Boton_01" onclick="" value="Agregar" />
                                          </td>
                                           <td align="left" class="textos_01">
                                                <input id="Excedente" type="button" class="Boton_01" onclick="javascript:excedentes()"  style=" width : 120px;" value="Ir a excedente" />
                                          </td>
				                         </tr>
				                            <tr>
                                             <td class="textos_01">
                                                Excedentes recientes
                                            </td>
				                         </tr>
                                                   
                                           </table>
                                     </td>
                              </tr>  
                              
                               
                              <tr>
                                    <td>         
                                        <igtbl:UltraWebGrid ID="UltraWebGrid1" runat="server" CaptionAlign="Left" EnableAppStyling="False" > 
                                            <Bands>
                                                <igtbl:UltraGridBand>
                                                    <Columns>
                                                        <igtbl:UltraGridColumn Width="90px" BaseColumnName="Inicio" DataType="System.Int32" IsBound="True" Key="Inicio" Hidden="False">
                                                            <Header Caption="Inicio"></Header>
                                                            <HeaderStyle  HorizontalAlign = "Center"></HeaderStyle>
                                                        </igtbl:UltraGridColumn>
                                                        
                                                       <igtbl:UltraGridColumn Width="90px" BaseColumnName="Fin" DataType="System.Int32" IsBound="True" Key="Fin" Hidden="False">
                                                            <Header Caption="Fin"></Header>
                                                            <HeaderStyle  HorizontalAlign = "Center"></HeaderStyle>
                                                        </igtbl:UltraGridColumn>
                                                        
                                                        <igtbl:UltraGridColumn Width="90px" BaseColumnName="Cantidad" IsBound="True" Key="Cantidad" CellMultiline="Yes">
                                                            <Header Caption="Cantidad">
                                                                <RowLayoutColumnInfo OriginX="3" />
                                                            </Header>
                                                            <HeaderStyle  HorizontalAlign = "Center"></HeaderStyle>
                                                            <Footer><RowLayoutColumnInfo OriginX="3" /></Footer>
                                                        </igtbl:UltraGridColumn>
                                                        
                                                    </Columns>
                                                    
                                                    <RowTemplateStyle Height="150px"  Width="240" BackColor="White" BorderColor="White" BorderStyle="Ridge"></RowTemplateStyle>
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
                                                <ClientSideEvents  BeforeRowTemplateCloseHandler ="BeforeRowTemplateCloseHandler" />
                                            </DisplayLayout>
                                        </igtbl:UltraWebGrid>   
                                    </td>
                            </tr>
                            <tr>
                                   <td class="textos_01">
                                   Total
                                   </td>
				           </tr>
				                         
				           <tr>
                                   <td class="textos_01">
                                   Capacidad
                                   </td>
				           </tr>
				           <tr>
                                   <td class="textos_01">
                                   Resta
                                   </td>
				            </tr>
				             <tr>
                                  <td style=" height:40px" class="textos_01">
                                      <table style="width: 300px">
                                        <tr>
                                            <td align="left" class="textos_01">
                                              <input id="Cancelar" type="button" class="Boton_01" onclick="" value="Cancelar" />
                                           </td>
                                           <td align="left" class="textos_01">
                                              <input id="Aceptar" type="button" class="Boton_01" onclick=""  value="Aceptar" />
                                           </td>
                                        </tr>
                                     </table>
                                  </td>
				            </tr>
                           
                       </tbody>
                   </table>
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

                              function excedentes() {
                
                                      $find('<%=WebDialogWindow1.ClientID%>').set_windowState($IG.DialogWindowState.Normal);
                                 
                              }
                             
                    </script>
                    <ig:WebDialogWindow ID="WebDialogWindow1" runat="server" InitialLocation="Centered"
                        Height="400px" Width="400px" Modal="true" WindowState="Hidden" Font-Size="10px">
                        
                        <ContentPane BackColor="#FAFAFA">
                        
                            <Template>
                                <div style="text-align:center">
                                    <table  align="center"  width="100%">
                                       <tr>
                                        
                                      <td class="textos_01">
                                       Excedentes recientes
                                       </td>
                                       </tr>
                                       
                                        <tr>
                                            <td align="center" >
                                        <igtbl:UltraWebGrid ID="UltraWebGrid2" runat="server" CaptionAlign="Left" EnableAppStyling="False" > 
                                            <Bands>
                                                <igtbl:UltraGridBand>
                                                    <Columns>
                                                        <igtbl:UltraGridColumn Width="90px" BaseColumnName="Usuario" DataType="System.Int32" IsBound="True" Key="Usuario" Hidden="False">
                                                            <Header Caption="Usuario"></Header>
                                                            <HeaderStyle  HorizontalAlign = "Center"></HeaderStyle>
                                                        </igtbl:UltraGridColumn>
                                                        
                                                       <igtbl:UltraGridColumn Width="90px" BaseColumnName="Rango" DataType="System.Int32" IsBound="True" Key="Rango" Hidden="False">
                                                            <Header Caption="Rango"></Header>
                                                            <HeaderStyle  HorizontalAlign = "Center"></HeaderStyle>
                                                        </igtbl:UltraGridColumn>
                                                        
                                                        <igtbl:UltraGridColumn Width="90px" BaseColumnName="Cantidad" IsBound="True" Key="Cantidad" CellMultiline="Yes">
                                                            <Header Caption="Cantidad">
                                                                <RowLayoutColumnInfo OriginX="3" />
                                                            </Header>
                                                            <HeaderStyle  HorizontalAlign = "Center"></HeaderStyle>
                                                            <Footer><RowLayoutColumnInfo OriginX="3" /></Footer>
                                                        </igtbl:UltraGridColumn>
                                                        
                                                         <igtbl:UltraGridColumn Width="70px" BaseColumnName="Activo2" IsBound="True" Key="Activo" Type="CheckBox" CellMultiline="no">
                                                            <Header  >
                                                                <RowLayoutColumnInfo OriginX="4" />
                                                            </Header>
                                                            <HeaderStyle HorizontalAlign = "Center"></HeaderStyle>
                                                            <CellStyle HorizontalAlign="Center"></CellStyle>
                                                        
                                                        </igtbl:UltraGridColumn>
                                                        
                                                    </Columns>
                                                    
                                                    <RowTemplateStyle Height="150px"  Width="240" BackColor="White" BorderColor="White" BorderStyle="Ridge"></RowTemplateStyle>
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
                                                <ClientSideEvents  BeforeRowTemplateCloseHandler ="BeforeRowTemplateCloseHandler" />
                                            </DisplayLayout>
                                        </igtbl:UltraWebGrid>   
                                                
                                            </td>
                                        </tr>
                                         <tr>
                                          
                                        </tr>
                                        <tr>
                                           <td  align="center">
                                              <input    id="Asignar" type="button" class="Boton_01" onclick=""  value="Asignar" />
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
