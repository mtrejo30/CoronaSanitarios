<%@ Page Language="C#" MasterPageFile="~/ControlPisoLamosa.Master" AutoEventWireup="true" 
    CodeBehind="CapturaClasificacion.aspx.cs" Inherits="LAMOSA.SCPP.Client.View.Administrador.Clasificacion.CapturaClasificacion" Title="Control de piso - Proceso de clasificación" %>

<%@ Register Assembly="Infragistics35.WebUI.UltraWebGrid.v9.1, Version=9.1.20091.1015, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb"
    Namespace="Infragistics.WebUI.UltraWebGrid" TagPrefix="igtbl" %>




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
                <td style="height:10px" colspan= "4"></td>
            </tr>
            <tr style="height:30px;">
                <td style="width:10px; background-color:#eee;"></td>
                <td  colspan= "6"  class ="lblTitulo1">
                    <asp:Label ID="lblTitulo" runat="server"  Text="Proceso de clasificación" ></asp:Label><br/>
                </td> 
            </tr>
            <tr><td style="height:10px" colspan= "4"></td></tr>
            <tr>
                <td style="width:10px;"></td>
                <td valign="top" class="leftarea" style="width:100px">
                    <div id="navcontainer">
                        <ul id="navlist">
  
                            <li><a href="javascript:history.back();" onclick="history.go(-1)"><img src="../Imagenes/Regresar.png" alt="Regresar" style="border:0px;" /> Regresar</a></li>
                        </ul>
                    </div>
                </td><td style="width:20px;">&nbsp;</td>
                <td>           
                    <igmisc:WebAsyncRefreshPanel ID="WebAsyncRefreshPanel1" runat="server">
                         <table style="height: 600px; width: 300px" border="1">
                        <tbody>
                                <tr style="height:300px">
                                    <td  style=" height:40px" class="textos_01" valign="top">
                                         <table style="width: 300px" border="0">
                                               <tr>
                                                    <td  rowspan="4" >           
                                                         <img src="../Imagenes/Defectos.png" alt="" style="border:0px;" /> 
                                                    </td>

                                                    <td>           
                                                         <img src="../Imagenes/L1.png" alt="" style="border:0px;" /> 
                                                    </td>
                                              </tr>     
                                              <tr>
                                                    <td>           
                                                         <img src="../Imagenes/L2.png" alt="" style="border:0px;" /> 
                                                    </td>
                                             </tr>      
                                             <tr>
                                                    <td>           
                                                          <img src="../Imagenes/L3.png" alt="" style="border:0px;" />
                                                    </td>
                                            </tr>       
                                            <tr>
                                                     <td>           
                                                         <img src="../Imagenes/L4.png" alt="" style="border:0px;" /> 
                                                    </td>  
                                            </tr>
                                                    

                                           </table>
                                        </td>
                                        <td  style=" height:40px" class="textos_01" valign="top">  
                                           <table style="width: 300px" border="0">
                                               <tr>
                                                    <td  class="textos_01">           
                                                       Tarima:  
                                                    </td>

                                                    <td>  
                                                        <table>   
                                                             <tr>
                                                                 <td>
                                                                     <asp:TextBox ID="Tarima" runat="server" Enabled="False"  CssClass="textost" ></asp:TextBox> 
                                                                </td>
                                                            </tr>   
                                                            <tr>
                                                                 <td>
                                                                     <input id="Cargar" type="button" class="Boton_01" onclick="" value="Cargar.." style="width: 75px" />
                                                                </td>
                                                            </tr>    
                                                        </table> 
                                                    </td>
                                              </tr>     
                                              <tr>
                                                    <td>           
                                                        Piezas:
                                                    </td>
                                                     <td>           
                                                         <asp:TextBox ID="Piezas" runat="server" Enabled="False"  CssClass="textost" ></asp:TextBox> 
                                                    </td>
                                             </tr>      
                                             <tr>
                                                    <td>           
                                                         Auditadas:
                                                    </td>
                                                     <td>           
                                                          <asp:TextBox ID="Auditadas" runat="server" Enabled="False"  CssClass="textost" ></asp:TextBox>
                                                    </td>
                                            </tr>   
                                             <tr>
                                                     <td>           
                                                        <br />
                                                             <br />
                                                                 <br />
                                                    </td>  
                                          
                                            </tr>    
                                            <tr>
                                                     <td>           
                                                         Etiqueta:
                                                    </td>  
                                                     <td>           
                                                         <asp:TextBox ID="Etiqueta" runat="server" Enabled="False"  CssClass="textost" ></asp:TextBox>
                                                    </td>
                                            </tr>
                                            
                                                  <tr>
                                                     <td>           
                                                         Tipo:
                                                    </td>  
                                                     <td>           
                                                         <asp:TextBox ID="Tipo" runat="server" Enabled="False"  CssClass="textost" ></asp:TextBox>
                                                    </td>
                                            </tr>
                                                  <tr>
                                                     <td>           
                                                         Modelo:
                                                    </td>  
                                                     <td>           
                                                         <asp:TextBox ID="Modelo" runat="server" Enabled="False"  CssClass="textost" ></asp:TextBox>
                                                    </td>
                                            </tr>
                                                  <tr>
                                                     <td>           
                                                         Color:
                                                    </td>  
                                                     <td>           
                                                         <asp:TextBox ID="Color" runat="server" Enabled="False"  CssClass="textost" ></asp:TextBox>
                                                    </td>
                                            </tr> 

                                             </table>
                                         </td>
                                   </tr>  
                                   <tr style="height:300px">
                                   
                                   
                                       <td align= "center" style=" height:26px" class="textos_01">
                                          
                                           
                                        
                                       
                                           <input id="continuar" type="button" class="Boton_01" onclick="" value="Continuar" style="width: 75px" />
                                       </td>
                                  </tr>
                                  
                                
                            </tbody>
                    </table>
          
                  </igmisc:WebAsyncRefreshPanel>

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
