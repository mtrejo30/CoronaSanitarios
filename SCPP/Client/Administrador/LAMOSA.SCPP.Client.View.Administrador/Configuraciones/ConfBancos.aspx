<%@ Page Language="C#" MasterPageFile="~/ControlPisoLamosa.Master" AutoEventWireup="true" 
    CodeBehind="ConfBancos.aspx.cs" Inherits="LAMOSA.SCPP.Client.View.Administrador.Configuraciones.ConfBancos" Title="Control de piso - Configuracion de bancos" %>
    
<%@ Register Assembly="Infragistics35.WebUI.Misc.v9.1, Version=9.1.20091.1015, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb"
    Namespace="Infragistics.WebUI.Misc" TagPrefix="igmisc" %>

<%@ Register Assembly="Infragistics35.WebUI.WebDateChooser.v9.1, Version=9.1.20091.1015, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb"
    Namespace="Infragistics.WebUI.WebSchedule" TagPrefix="igsch" %>

<%@ Register Assembly="Infragistics35.Web.v9.1, Version=9.1.20091.1015, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb"
    Namespace="Infragistics.Web.UI.LayoutControls" TagPrefix="ig" %>
    
<%@ Register Assembly="Infragistics35.WebUI.WebDataInput.v9.1, Version=9.1.20091.1015, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb"
    Namespace="Infragistics.WebUI.WebDataInput" TagPrefix="igtxt" %>

<%@ Register Assembly="CrystalDecisions.Web, Version=10.5.3700.0, Culture=neutral, PublicKeyToken=692fbea5521e1304"
    Namespace="CrystalDecisions.Web" TagPrefix="CR" %>
    
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
            <tr><td style="height:10px" colspan= "3"></td></tr>
            <tr style="height:30px;">
                <td style="width:10px; background-color:#eee;"></td>
                <td  colspan= "3"  class ="lblTitulo1">
                    <asp:Label ID="lblTitulo" runat="server"  Text="Configuracion de bancos" ></asp:Label><br/>
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
                </td><td style="width:20px;" rowspan="2">&nbsp;</td>
                <td>           
                    <igmisc:WebAsyncRefreshPanel ID="WebAsyncRefreshPanel1" runat="server">
                         <table style="height: 200px; width: 750px" >
                           <tbody>
                                <tr>
                                        <td style=" height:40px" class="textos_01">
                                         <table style="width: 650px">
                                            <tr>
                                               <td  class="textos_01">
                                                Planta:
                                               </td>
                                               <td  class="textos_01">
                                                <asp:TextBox ID="TxtPlanta" runat="server" Enabled="False" CssClass="textost" ></asp:TextBox>
                                               </td>
                                                    
                                               <td  class="textos_01">
                                               </td>
                                                <td  class="textos_01">
                                               </td>
                                            </tr>        
                                            <tr>
                                               <td  class="textos_01">
                                               Centro de trabajo:
                                               </td>
                                               <td  class="textos_01">
                                                        <asp:DropDownList ID="cmbCentro" runat="server" CssClass="textosd" OnSelectedIndexChanged="cmbCentro_SelectedIndexChanged">
                                                         </asp:DropDownList>
                                               </td>
                                                    
                                               <td  class="textos_01">
                                               Vaciadas por dia:
                                               </td>
                                                <td  class="textos_01">
                                                <asp:TextBox ID="VaciadasDia" runat="server" CssClass="textost" ></asp:TextBox>
                                               </td>
                                            </tr> 
                                            <tr>
                                               <td  class="textos_01">
                                               Maquina:
                                               </td>
                                               <td  class="textos_01">
                                                        <asp:DropDownList ID="cmbMaquina" runat="server" CssClass="textosd">
                                                        <asp:ListItem Value="1" Text="Banco 1"></asp:ListItem>  
                                                        <asp:ListItem Value="2" Text="Banco 2"></asp:ListItem>  
                                                        </asp:DropDownList>
                                               </td>
                                                    
                                               <td  class="textos_01">
                                                Limite de vaciadas:
                                               </td>
                                                <td  class="textos_01">
                                                <asp:TextBox ID="LimVaciadas" runat="server" ></asp:TextBox>
                                               </td>
                                            </tr> 
                                            <tr>
                                               <td  class="textos_01">
                                               Autorizado:
                                               </td>
                                               <td  class="textos_01">
                                                      <asp:CheckBox ID="Autorizado" runat="server" />
                                               </td>
                                                    
                                               <td  class="textos_01">
                                               Vaciadas acumuladas:
                                               </td>
                                                <td  class="textos_01">
                                                 <asp:TextBox ID="VacAcumuladas" runat="server" ></asp:TextBox>
                                               </td>
                                            </tr> 
                                            <tr>
                                               <td  class="textos_01">
                                               Fecha activación:
                                               </td>
                                               <td  class="textos_01">
                                                     <igsch:WebDateChooser ID="FechaAct" runat="server"  Width="156" >
                                                 </igsch:WebDateChooser>
                                               </td>
                                                    
                                               <td  class="textos_01">
                                              
                                               </td>
                                                <td  class="textos_01">
                                                 
                                               </td>
                                            </tr> 
                                            
                                            </table>
                                           </td>
                                 </tr>   
                                            
                                      <tr>
                                        <td align= "center" style=" height:26px" class="textos">
                                            <input id="igtbl_reBuscaBtn" type="button" class="Boton_01" onclick="javascript:ListItemSelected(2,'');" value="Buscar" style="width: 75px" />
                                        </td>
                                    </tr>
                                        
                                        
                                 <tr>
                                    <td>
                                           
                                   
                                        <igtbl:ultrawebgrid ID="UltraWebGrid1" runat="server" CaptionAlign="Left" EnableAppStyling="False">
                                            <Bands>
                                                <igtbl:UltraGridBand>
                                                    
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
                                        </igtbl:ultrawebgrid>
                                        
                                      </td>
                                        </tr>
                           </tbody>  
                         </table>
                    
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
              
                    </script>
                    
                    

                </td>
            </tr>
            <tr>
                <td colspan ="3">
                    <igtblexp:UltraWebGridExcelExporter ID="uwgConsBancos" WorksheetName="ConsBancos" DownloadName="Reporte.XLS" runat='server'>
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
