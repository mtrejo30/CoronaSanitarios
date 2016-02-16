

<%@ Page Language="C#" MasterPageFile="~/ControlPisoLamosa.Master" AutoEventWireup="true" 
    CodeBehind="CambioContraseña.aspx.cs"  Inherits="LAMOSA.SCPP.Client.View.Administrador.CambioContraseña"   Title="Control de piso - Cambio de contraseña"  %>


    
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
        
        <script   src="../FuncionesJS/jquery-1.4.2.js" type="text/javascript"></script>
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
                    <asp:Label ID="lblTitulo" runat="server"  Text="Cambiar contraseña" ></asp:Label><br/>
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
                </td>
                <td style="width:20px;" rowspan="2">&nbsp;</td>
                <td>           
                   <igmisc:WebAsyncRefreshPanel ID="WebAsyncRefreshPanel1" runat="server">
                       <table style="height: 170px; width: 600px" >
                          <tbody>
                            <tr>
                                <td style=" height:40px" class="textos_01">
                                      <table style="width: 550px">
                                      
                                        <tr>
                                            <td class="textos_01">
                                              Usuario:
                                            </td>
                                            <td class="textos_01">
                                            <asp:TextBox ID="NomUsuario" runat="server" Width="150px" MaxLength="10"  value="" CssClass="textost" ></asp:TextBox>
                                         
                                            </td>
                                         </tr>   
                                        <tr>
                                            <td class="textos_01">
                                               Contraseña anterior:
                                            </td>
                                            <td class="textos_01">
                                            <asp:TextBox ID="ContrasenaAnt" runat="server" Width="150px" MaxLength="10"  TextMode="Password"  value="" ></asp:TextBox>
                                            </td>
                                         </tr>   
                                        <tr>
                                                <td class="textos_01">
                                               Contraseña nueva:
                                                </td>
                                                <td class="textos_01">
                                                  <asp:TextBox ID="ContrasenaNueva"  runat="server" Width="150px" MaxLength="10"  TextMode="Password" ValidationGroup="grpUs" value="" ></asp:TextBox>
                                                </td>
                                           </tr>
                                        <tr>
                                                <td class="textos_01">
                                                 Confirma contraseña:
                                                </td>
                                                <td class="textos_01">
                                                   <asp:TextBox ID="ConfirmarContra" runat="server" MaxLength="10"  Width=" 150px" value="" 
                                                                         TextMode="Password" ValidationGroup="grpUs"></asp:TextBox>
                                                                                                
                                                 <asp:CompareValidator ID="cvValidarContrasenias" runat="server" ControlToCompare="ConfirmarContra"
                                                        ControlToValidate="ContrasenaNueva" ErrorMessage="Las contraseñas no coinciden" Width="210px" ValidationGroup="grpUs"></asp:CompareValidator>
                                                </td>
                                          </tr>
                                        <tr>
                                                  <td class="textos_01"  colspan="2"> 
                                                    <input id="GuardarI" onclick="ok(event)" class="Boton_01" style="width: 75px;" type="button" 
                                                                            value="Guardar"   runat="server" />     
                                                    <asp:Button ID="Guardar" runat="server"  CssClass="hidden"  Text="Guardar" OnClick="btnCambiaContrasena_Click" />
                                                   </td>
                                           </tr>
                                      </table>
                                </td>
                            </tr>    
                            <tr>
                                    <td>
                 
                                        <script type="text/javascript"> 
                                       


                                            function ok(event) {
                                                var Usuario = document.getElementById("ctl00_Principal_NomUsuario").value;
                                                var ContrasenaAnt = document.getElementById("ctl00_Principal_ContrasenaAnt").value;
                                                var ContrasenaNueva = document.getElementById("ctl00_Principal_ContrasenaNueva").value
                                                var ConfirmarContra = document.getElementById("ctl00_Principal_ConfirmarContra").value

                                                if (Usuario != "" && ContrasenaAnt != "" && ContrasenaNueva != "" && ConfirmarContra != "") {
                                                    if (confirm('¿Desea guardar cambios?')) {

                                                        $("#<%=Guardar.ClientID%>").click();
                                                      
                                                    }
                                                }
                                                else alert('Información incompleta para guardar la contraseña!');
                                            }
                                           
                                            
                                         
                                         
                                        </script>
                       
                                    </td>
                            </tr>
                         </tbody>
                       </table>
                       
                       
                        <asp:HiddenField ID="hddNombreUsuario" runat="server" />
                       <asp:HiddenField ID="hddCodUsuario" runat="server" />
                        
                       
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
          
        </table>
        
        <asp:HiddenField ID="Planta" runat="server" />
        
        <asp:HiddenField ID="UserInSession" runat="server" />
        <asp:HiddenField ID="HddUser" runat="server" />
        <asp:HiddenField ID="Sucursalhdd" runat="server" />
 
         
  </div>

</asp:Content>
