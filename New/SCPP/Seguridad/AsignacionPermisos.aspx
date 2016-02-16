<%@ Page Language="C#" MasterPageFile="~/ControlPisoLamosa.Master" AutoEventWireup="true"
    CodeBehind="AsignacionPermisos.aspx.cs" Inherits="LAMOSA.SCPP.Client.View.Administrador.Seguridad.AsignacionPermisos"
    Title="Control de piso - Asignación de Permisos" %>

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
        <asp:ScriptManager ID="sm" runat="server"></asp:ScriptManager>
        <table align="center" border="0" cellpadding="0" cellspacing="0" width="1024px">
            <tr>
                <td style="height: 10px" colspan="3">
                </td>
            </tr>
            <tr style="height: 30px;">
                <td style="width: 10px; background-color: #eee;">
                </td>
                <td colspan="3" class="lblTitulo1">
                    <asp:Label ID="lblTitulo" runat="server" Text="Asignación de Permisos"></asp:Label><br />
                </td>
            </tr>
            <tr>
                <td style="height: 10px" colspan="3">
                </td>
            </tr>
            <tr>
                <td style="width: 10px;" rowspan="2">
                </td>
                <td rowspan="2" valign="top" class="leftarea">
                    <div id="navcontainer">
                        <ul id="navlist">
                            <li><a href="javascript:history.back();" onclick="history.go(-1)">
                                <img src="../Imagenes/Regresar.png" alt="Regresar" style="border: 0px;" />
                                Regresar</a></li>
                        </ul>
                    </div>
                </td>
                <td style="width: 20px;" rowspan="2">
                    &nbsp;
                </td>
                <td>
                    <igmisc:WebAsyncRefreshPanel ID="WebAsyncRefreshPanel1" runat="server">
                        <table style="height: 170px; width: 600px">
                            <tbody>
                                <tr>
                                    <td style="height: 40px" class="textos_01">
                                        <table style="width: 550px">
                                            <tr>
                                                <td class="textos_01">
                                                    Rol de usuario:
                                                </td>
                                                <td class="textos_01">
                                                    <asp:DropDownList ID="cmbRol" runat="server" CssClass="textosd" 
                                                        onselectedindexchanged="Button2_Click">
                                                    </asp:DropDownList>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="textos_01">
                                                    Módulo:
                                                </td>
                                                <td class="textos_01">
                                                    <asp:DropDownList ID="cmbModulo" runat="server" CssClass="textosd" 
                                                        onselectedindexchanged="Button2_Click">
                                                        <asp:ListItem Value="0" Text="Administración"> </asp:ListItem>
                                                    </asp:DropDownList>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="textos_01">
                                                </td>
                                                <td class="textos_01">
                                                    <asp:Button ID="Button1" CssClass="Boton_01" runat="server" Text="Guardar" OnClientClick="javascript:$('#ctl00_Principal_hAux').val(1)" />
                                                    <asp:Button ID="Button2" OnClientClick="javascript:$('#ctl00_Principal_hAux').val('')"
                                                        Style="padding-left: 15px" CssClass="Boton_01" runat="server" Text="Buscar" OnClick="Button2_Click" />
                                                    <asp:HiddenField ID="hAux" runat="server" />
                                                    <asp:HiddenField ID="hRol" runat="server" />
                                                    <asp:HiddenField ID="hModulo" runat="server" />
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <div style="overflow: auto; width: 800px;">
                                            <igtbl:UltraWebGrid ID="UltraWebGrid1" runat="server" CaptionAlign="Left" 
                                                EnableAppStyling="False"  
                                                oninitializerow="UltraWebGrid1_InitializeRow" >
                                                <Bands>
                                                    <igtbl:UltraGridBand>
                                                        <AddNewRow View="NotSet" Visible="NotSet">
                                                        </AddNewRow>
                                                    </igtbl:UltraGridBand>
                                                </Bands>
                                                <DisplayLayout Name="UltraWebGrid1" AllowDeleteDefault="NotSet" NoDataMessage=""
                                                    RowHeightDefault="20px" SelectTypeRowDefault="Single" TableLayout="Fixed" Version="3.00"
                                                    CellClickActionDefault="RowSelect" LoadOnDemand="Xml" AllowAddNewDefault="NotSet"
                                                    AllowColSizingDefault="Free" AllowSortingDefault="OnClient" CellPaddingDefault="1"
                                                    HeaderClickActionDefault="SortSingle" RowSelectorsDefault="No" RowSizingDefault="Free"
                                                    BorderCollapseDefault="Separate" AllowUpdateDefault="Yes"  StationaryMargins="Header">
                                                    <FrameStyle Height="400px" Width="800px">
                                                    </FrameStyle>
                                                    <RowAlternateStyleDefault BackColor="#E5E5E5">
                                                    </RowAlternateStyleDefault>
                                                    
                                                    <ClientSideEvents BeforeRowTemplateCloseHandler="BeforeRowTemplateCloseHandler" />
                                                    <EditCellStyleDefault BackColor="Silver">
                                                    </EditCellStyleDefault>
                                                    <FooterStyleDefault BackColor="LightGray" BorderStyle="Solid" BorderWidth="1px">
                                                    </FooterStyleDefault>
                                                    <HeaderStyleDefault BackColor="#666666" BorderColor="Black" BorderStyle="Solid" 
                                                        Font-Bold="True" ForeColor="White" >
                                                    </HeaderStyleDefault>
                                                    <RowSelectorStyleDefault BorderStyle="Solid">
                                                    </RowSelectorStyleDefault>
                                                    <RowStyleDefault BackColor="White" BorderColor="Black" BorderStyle="Solid" 
                                                        BorderWidth="1px" Font-Names="Verdana" Font-Size="8pt" ForeColor="Black">
                                                    </RowStyleDefault>
                                                    <SelectedRowStyleDefault BackColor="#FFFFB3" Font-Bold="True" ForeColor="Black">
                                                    </SelectedRowStyleDefault>
                                                    <ActivationObject BorderColor="Black" BorderStyle="Dotted" BorderWidth="">
                                                    </ActivationObject>
                                                    <AddNewRowDefault View="Top">
                                                    </AddNewRowDefault>
                                                    <FilterOptionsDefault>
                                                        <FilterDropDownStyle BackColor="White" BorderColor="Silver" BorderStyle="Solid" 
                                                            BorderWidth="1px" CustomRules="overflow:auto;" 
                                                            Font-Names="Verdana,Arial,Helvetica,sans-serif" Font-Size="11px" Width="200px">
                                                        </FilterDropDownStyle>
                                                        <FilterHighlightRowStyle BackColor="#999999" ForeColor="White">
                                                        </FilterHighlightRowStyle>
                                                    </FilterOptionsDefault>
                                                </DisplayLayout>
                                            </igtbl:UltraWebGrid>
                                        </div>
                                    </td>
                                </tr>
                            </tbody>
                        </table>
                    </igmisc:WebAsyncRefreshPanel>
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td colspan="3">
                    <igtblexp:UltraWebGridExcelExporter ID="uwgUsuarios" WorksheetName="Usuarios" DownloadName="Reporte.XLS"
                        runat='server'>
                    </igtblexp:UltraWebGridExcelExporter>
                </td>
            </tr>
        </table>
        <asp:HiddenField ID="UserInSession" runat="server" />
        <asp:HiddenField ID="HddUser" runat="server" />
        <asp:HiddenField ID="Sucursalhdd" runat="server" />
        <asp:UpdatePanel runat="server" ID="UpdatePanel2" ChildrenAsTriggers="True" UpdateMode="Always"
            RenderMode="Inline">
            <ContentTemplate>
                <asp:HiddenField ID="HiddenField1" runat="server" />
                <asp:HiddenField ID="hddSecurityConstants" runat="server" />
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
</asp:Content>
