<%@ Page Language="C#" MasterPageFile="~/ControlPisoLamosa.Master"AutoEventWireup="true" 
CodeBehind="Configuracion.aspx.cs" Inherits="LAMOSA.SCPP.Client.View.Administrador.Seguridad.Configuracion" Title="Control de piso - Configuración"%>

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
                <td style="height:10px" colspan= "3"></td></tr>
            <tr style="height:30px;">
                <td style="width:10px; background-color:#eee;"></td>
                <td  colspan= "3"  class ="lblTitulo1">
                    <asp:Label ID="lblTitulo" runat="server"  Text="Configuración" ></asp:Label><br/>
                </td> 
            </tr>
            <tr><td style="height:10px" colspan= "3"></td></tr>
            <tr>
                <td style="width:10px;" rowspan="2"></td>
                <td rowspan="2" valign="top" class="leftarea">
                    <div id="navcontainer">
                        <ul id="navlist">
                            <li><a href="javascript:history.back();" onclick="history.go(-1)"><img src="../Imagenes/Regresar.png" alt="Regresar" style="border:0px;" /> Regresar</a></li>
                        </ul>
                    </div>
                </td><td style="width:20px;" rowspan="2">&nbsp;</td>
                <td>           
                    <igmisc:WebAsyncRefreshPanel ID="WebAsyncRefreshPanel1" runat="server">
                      <table style="height: 170px; width: 600px" 
                        <tbody>
                            <tr>
                                <td style=" height:40px" class="textos_01">
                                      <table style="width: 350px">
                                          <tr>
                                               <td class="textos_01">  
                                                  Número de intentos de acceso:
                                               </td>
                                               <td  class="textos_01">  
                                                      <asp:TextBox ID="TxtIntentos" runat="server"   CssClass=textost ></asp:TextBox>
                                               </td>
                                         </tr>
                                         <tr>
                                               <td class="textos_01">  
                                                  Número de días para cambiar contraseña:
                                               </td>
                                               <td  class="textos_01">  
                                                      <asp:TextBox ID="TxtDiasPass" runat="server"   CssClass=textost ></asp:TextBox>
                                               </td>
                                        </tr>
                                         <tr>
                                            <td align="left">
                                                <asp:Button ID="btnGuardar" runat="server"  CssClass="Boton_01" OnClick="BotonGuardar_click"
                                                            Text="Guardar" Width="115px" />
                                            </td>
				                        </tr>
                                      </table>
                                  </td>
                            </tr>   
                            </tbody>
                    </table>
                  </igmisc:WebAsyncRefreshPanel>
                    &nbsp;
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
