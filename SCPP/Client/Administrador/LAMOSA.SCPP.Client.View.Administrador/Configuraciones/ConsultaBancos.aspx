<%@ Page Language="C#" MasterPageFile="~/ControlPisoLamosa.Master" AutoEventWireup="true"
    CodeBehind="ConsultaBancos.aspx.cs" Inherits="LAMOSA.SCPP.Client.View.Administrador.Configuraciones.ConfiguraBancos"
    Title="Control de piso - Consulta de bancos" %>

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
        <table align="center" border="0" cellpadding="0" cellspacing="0" width="1024px">
            <tr>
                <td style="height: 10px" colspan="3">
                </td>
            </tr>
            <tr style="height: 30px;">
                <td style="width: 10px; background-color: #eee;">
                </td>
                <td colspan="3" class="lblTitulo1">
                    <asp:Label ID="lblTitulo" runat="server" Text="Consulta de configuración de bancos"></asp:Label><br />
                </td>
            </tr>
            <tr>
                <td style="height: 10px" colspan="3">
                </td>
            </tr>
            <tr>
                <td style="width: 10px;" rowspan="2">
                </td>
                <td rowspan="2" valign="top" class="leftarea"  style="width:100px">
                    <div id="navcontainer">
                        <ul id="navlist">
                            <li><a href="javascript:ListItemSelected(1,'')" id="LAddNew" runat="server">
                                <img src="../Imagenes/Nuevo.png" alt="Nuevo registro" style="border: 0px;" />
                                Nuevo registro</a></li>
                            <li><a href="javascript:ListItemSelected(2,'')" id="LExport" runat="server">
                                <img src="../Imagenes/Exportar.png" alt="Exportar tabla" style="border: 0px;" />
                                Exportar tabla</a></li>
                            <li><a href="javascript:history.back();" onclick="history.go(-1)">
                                <img src="../Imagenes/Regresar.png" alt="Regresar" style="border: 0px;" />
                                Regresar</a></li>
                        </ul>
                    </div>
                </td>
                <td>
                    &nbsp;
                </td>
                <td>
                    <igmisc:WebAsyncRefreshPanel ID="WebAsyncRefreshPanel1" runat="server">
                        <table style="height: 100px; width: 800px" border="0">
                            <tbody>
                                <tr>
                                    <td >
                                        <table width= "600px">
                                            <tr>
                                                <td class="textos_01">
                                                    Centro de trabajo:
                                                </td>
                                                <td class="textos_01">
                                                    <asp:DropDownList ID="cmbCentroTrabajo" runat="server" CssClass="textosd" AutoPostBack="true"
                                                        OnSelectedIndexChanged="cmbCentro_SelectedIndexChanged">
                                                    </asp:DropDownList>
                                                    <asp:TextBox ID="TxtPlanta1" runat="server" Enabled="False" CssClass="hidden"></asp:TextBox>
                                                </td>
                                                <td class="textos_01">
                                                    Máquina:
                                                </td>
                                                <td class="textos_01">
                                                    <asp:DropDownList ID="cmbMaquina" runat="server" Width="230px" CssClass="textosd">
                                                    </asp:DropDownList>
                                                </td>
                                            </tr>
                                             </table>
                                              <table border="0" width= "600px">
                                                    <tr>
                                                        <td class="textos_01" width= "107px">
                                                            Listar: &nbsp; &nbsp; &nbsp; &nbsp;
                                                        </td>
                                                        <td class="textos_01">
                                                            <asp:DropDownList ID="ddlActivos" runat="server"  CssClass="textosd">
                                                                <asp:ListItem Value="-1">Activos e Inactivos</asp:ListItem>
                                                                <asp:ListItem Value="1">Activos</asp:ListItem>
                                                                <asp:ListItem Value="0">Inactivos</asp:ListItem>
                                                            </asp:DropDownList>
                                                   
                                                            <asp:Button align="left" ID="btnBuscar" class="Boton_01" Text="Buscar" runat="server"
                                                                OnClick="btnBuscar_click" />
                                                                
                                                                <asp:Button align="left" ID="btnlimpiar" class="hidden" Text=" " runat="server"
                                                                OnClick="limpiar_Cbs" />
                                                        </td>
                                                     
                                                    </tr>
                                        </table>
                                    </td>
            </tr>
            <tr>
                <td>
                    <igtbl:UltraWebGrid ID="UltraWebGrid2" runat="server" CaptionAlign="Left" EnableAppStyling="False"
                        DisplayLayout-AllowAddNewDefault="Yes" DisplayLayout-AddNewRowDefault-Visible="Yes"
                        OnPageIndexChanged="cambio_pagina2" Width="800px"  >
                        <Bands>
                            <igtbl:UltraGridBand>
                                <AddNewRow Visible="NotSet" View="NotSet">
                                </AddNewRow>
                            </igtbl:UltraGridBand>
                        </Bands>
                        <DisplayLayout Name="UltraWebGrid2" AllowDeleteDefault="Yes" AllowUpdateDefault="RowTemplateOnly"
                            NoDataMessage="" RowHeightDefault="20px" SelectTypeRowDefault="Single" TableLayout="Fixed"
                            Version="3.00" CellClickActionDefault="RowSelect" LoadOnDemand="Xml" AllowAddNewDefault="Yes"
                            AllowColSizingDefault="Free" AllowSortingDefault="OnClient" CellPaddingDefault="1"
                            HeaderClickActionDefault="SortSingle" RowSelectorsDefault="No" RowSizingDefault="Free"
                            scrollbar="Auto" >
                            <RowAlternateStyleDefault BackColor="#E5E5E5">
                            </RowAlternateStyleDefault>
                            <Pager AllowPaging="False" PageSize="20">
                                <PagerStyle Font-Size="11px" Font-Names="Arial" BackColor="#666666" ForeColor="White"
                                    Height="20px" BorderStyle="Solid" BorderColor="Black" BorderWidth="1px" />
                            </Pager>
                            <EditCellStyleDefault BackColor="silver">
                            </EditCellStyleDefault>
                            <FooterStyleDefault BackColor="LightGray" BorderStyle="Solid" BorderWidth="1px">
                            </FooterStyleDefault>
                            <HeaderStyleDefault BackColor="#666666" BorderColor="Black" BorderStyle="Solid" Font-Bold="True"
                                ForeColor="White">
                            </HeaderStyleDefault>
                            <RowSelectorStyleDefault BorderStyle="Solid">
                            </RowSelectorStyleDefault>
                            <RowStyleDefault BackColor="White" BorderColor="Black" BorderStyle="Solid" BorderWidth="1px"
                                Font-Names="Verdana" Font-Size="8pt" ForeColor="Black">
                            </RowStyleDefault>
                            <SelectedRowStyleDefault BackColor="#FFFFB3" ForeColor="Black" Font-Bold="True">
                            </SelectedRowStyleDefault>
                            <AddNewBox Hidden="true">
                            </AddNewBox>
                            <ActivationObject BorderColor="Black" BorderWidth="" BorderStyle="Dotted">
                            </ActivationObject>
                            <AddNewRowDefault View="Top" Visible="No">
                            </AddNewRowDefault>
                            <FilterOptionsDefault>
                                <FilterDropDownStyle BackColor="White" BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px"
                                    CustomRules="overflow:auto;" Font-Names="Verdana,Arial,Helvetica,sans-serif"
                                    Font-Size="11px" Width="200px">
                                </FilterDropDownStyle>
                                <FilterHighlightRowStyle BackColor="#999999" ForeColor="White">
                                </FilterHighlightRowStyle>
                            </FilterOptionsDefault>
                            <ClientSideEvents BeforeRowTemplateCloseHandler="BeforeRowTemplateCloseHandler" DblClickHandler="dobleclick"
                                BeforeRowTemplateOpenHandler="BeforeOpenTemplate" />
                        </DisplayLayout>
                    </igtbl:UltraWebGrid>
                </td>
            </tr>
            </tbody> </table> </igmisc:WebAsyncRefreshPanel>

            <script type="text/javascript">
                function dobleclick(gridname, cellid) {
                    var row = igtbl_getRowById(cellid);
                    $("#<%=hddVacAcu.ClientID%>").val(row.getCell(2).getValue());
                    SwitchDiv(false);
                    $("#<%=hddBanco.ClientID%>").val(row.getCell(10).getValue());
                    $("#<%=hddCT.ClientID%>").val(row.getCell(9).getValue());
                    $("#<%=hddConfiguracionBanco.ClientID%>").val(row.getCell(13).getValue());
                    $("#<%=hddAut.ClientID%>").val(row.getCell(6).getValue());
               
                    $("#<%=btnLlenaGridWD.ClientID%>").click();

                    $find('<%=WebDialogWindow3.ClientID%>').set_windowState($IG.DialogWindowState.Normal);
                   
                }
                function ListItemSelected(idList, varList) {
                    //var nuevo = document.getElementById("<%=hddSecurityConstants.ClientID %>").getAttribute('value').indexOf('1');
                    //   var exporta = document.getElementById("<%=hddSecurityConstants.ClientID %>").getAttribute('value').indexOf('6');

                    if (idList == 1) {//Nuevo
                        Limpiarcbs();
                        $find('<%=WebDialogWindow3.ClientID%>').set_windowState($IG.DialogWindowState.Normal);
                        SwitchDiv(true);
                    } else if (idList == 2) {//Exportar
                        $find('<%=WebDialogWindow1.ClientID%>').set_windowState($IG.DialogWindowState.Normal);
                    }
                    else if (idList == 3) {//Autorizar
                    }
                }

                function BeforeRowTemplateOpen(gridName, rowId) {
                   
                }

                function SwitchDiv(nuevo) {
                    if (nuevo) {
                        Limpiarcbs()
                        ClearGrid();
                        document.getElementById("ctl00_Principal_WebDialogWindow3_tmpl_CentroTrabWD").disabled = false;
                        document.getElementById("ctl00_Principal_WebDialogWindow3_tmpl_MaquinaWD").disabled = false;
                        document.getElementById("ctl00_Principal_WebDialogWindow3_tmpl_TipoArticuloWD").disabled = false;
                        document.getElementById("ctl00_Principal_WebDialogWindow3_tmpl_btnAgregarWD").disabled = false;
                      //  document.getElementById("ctl00_Principal_WebDialogWindow3_tmpl_btnAutorizaWD").disabled = true;
                      //  document.getElementById("ctl00_Principal_WebDialogWindow3_tmpl_btnEliminarBancoWD").disabled = true;
                        
                        
                        $("#ctl00_Principal_WebDialogWindow3_tmpl_btnNuevo").click();
                     
                    } else {


                    $("#ctl00_Principal_WebDialogWindow3_tmpl_btnGrid").click();

                    document.getElementById("ctl00_Principal_WebDialogWindow3_tmpl_btnAgregarWD").disabled = false;
                   // document.getElementById("ctl00_Principal_WebDialogWindow3_tmpl_btnAutorizaWD").disabled = false;
                   // document.getElementById("ctl00_Principal_WebDialogWindow3_tmpl_btnEliminarBancoWD").disabled = false;

                    }
                }
                function ClearGrid() {
                    $("#<%=hddAut.ClientID%>").val('false')
                    $("#<%=hddBanco.ClientID%>").val('');
                    $("#<%=hddCT.ClientID%>").val('');
                    $("#<%=hddConfiguracionBanco.ClientID%>").val('');

                    var grid = igtbl_getGridById("<%=UltraWebGrid1.ClientID %>")
                    if (grid) {
                        var Rows = grid.Rows;
                        var rowCount = Rows.length - 1
                        for (var i = rowCount; i >= 0; i--) {
                            var b = Rows.remove(i);
                        }
                    }
                    $("#<%=btnBuscar.ClientID%>").click();

                }

               


                function Limpiarcbs() {

                    $("#<%=btnlimpiar.ClientID%>").click();

                }
             function WindowStateChanging(dialog, evtArgs) {     
                    if (dialog.get_windowState() == $IG.DialogWindowState.Hidden)
                        Limpiarcbs();
                }  
            </script>

            <ig:WebDialogWindow ID="WebDialogWindow1" runat="server" InitialLocation="Centered"
                Height="100px" Width="400px" Modal="true" WindowState="Hidden" Font-Size="10px">
                <ContentPane BackColor="#FAFAFA">
                    <Template>
                        <div style="padding: 5px;">
                            <table cellpadding="0" cellspacing="0" align="center" style="text-align: center;
                                width: 100%">
                                <tr>
                                    <td align="center" colspan="3">
                                        <asp:Panel ID="pnlExporta" runat="server" Width="350px">
                                            <asp:DropDownList ID="ddlSeleccion" runat="server" CssClass="AreaBox_02" Width="180px">
                                                <asp:ListItem>Documento Portable (PDF)</asp:ListItem>
                                                <asp:ListItem>MS Word (DOC)</asp:ListItem>
                                                <asp:ListItem>MS Excel (XLS)</asp:ListItem>
                                            </asp:DropDownList>
                                            <asp:Button ID="btnExporta" runat="server" CssClass="Boton_01" OnClick="btnExporta_Click"
                                                Text="Exportar" Width="115px" />
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
            </td> </tr>
            <tr>
                <td colspan="3">
                    <igtblexp:UltraWebGridExcelExporter ID="uwgConsBancos" WorksheetName="ConsBancos"
                        DownloadName="Reporte.XLS" runat='server'>
                    </igtblexp:UltraWebGridExcelExporter>
                </td>
            </tr>
        </table>
        <ig:WebDialogWindow ID="WebDialogWindow3" runat="server" InitialLocation="Centered"
            Height="550px"  Width="1015px"    ClientEvents-WindowStateChanging="WindowStateChanging"
            Modal="true" WindowState="Hidden"  Font-Size="10px">
            <ContentPane BackColor="#FAFAFA">
                <Template>
                    <div style="padding: 5px;">
                        <igmisc:WebAsyncRefreshPanel ID="WebAsyncRefreshPanel2" runat="server">
                            <table border="0" width="400px">
                                <tbody>
                                    <tr>
                                        <td style="">
                                            <table style="" border="0" width="400px">
                                                <tr>
                                                    <td colspan="4" class="lblTitulo1" valign="top">
                                                        <asp:Label ID="Label1" runat="server" Text="Configuración de bancos" Width="950"></asp:Label>
                                                    </td>
                                                 
                                                </tr>
                                                  </table>
                                                 <table style="" border="0" width="850px" >
                                                <tr>
                                                    <td class="textos_01">
                                                        Centro de trabajo:
                                                    </td>
                                                    <td class="textos_01">
                                                        <asp:DropDownList   ID="CentroTrabWD" Width="230px" runat="server" CssClass="textosd"
                                                            OnSelectedIndexChanged="CentroTrabWD_SelectedIndexChanged" AutoPostBack="true">
                                                        </asp:DropDownList>
                                                    </td>
                                                    <td class="textos_01">
                                                        Máquina:
                                                    </td>
                                                    <td class="textos_01">
                                                        <asp:DropDownList ID="MaquinaWD" Width="230px" runat="server" CssClass="textosd">
                                                        </asp:DropDownList>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="textos_01">
                                                        Tipo de artículo:
                                                    </td>
                                                    <td class="textos_01">
                                                        <asp:DropDownList ID="TipoArticuloWD" Width="230px" runat="server" CssClass="textosd"
                                                            OnSelectedIndexChanged="TipoArticuloWD_SelectedIndexChanged" AutoPostBack="true">
                                                        </asp:DropDownList>
                                                    </td>
                                                     <td class="textos_01">
                                                        Molde:
                                                    </td>
                                                    <td class="textos_01">
                                                        <asp:DropDownList ID="MoldeWD" runat="server" CssClass="textosd" AutoPostBack="true" Width="230px"
                                                            OnSelectedIndexChanged="MoldeWD_SelectedIndexChanged">
                                                        </asp:DropDownList>
                                                    </td>
                                                   
                                                </tr>
                                                <tr> <td class="textos_01">
                                                        Límite de vaciadas:
                                                    </td>
                                                    <td class="textos_01">
                                                        <asp:TextBox ID="LimVaciadasWD"  runat="server" columnkey="Limitevaciadas"  ></asp:TextBox>
                                                    </td>
                                                   
                                                    <td class="textos_01">
                                                        Cantidad de moldes:
                                                    </td>
                                                    <td class="textos_01">
                                                        <asp:TextBox ID="CantidadMoldesWD" runat="server"></asp:TextBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="textos_01">
                                                        Vaciadas al día:
                                                    </td>
                                                    <td class="textos_01">
                                                        <asp:TextBox ID="VaciadasDiaWD" runat="server"></asp:TextBox>
                                                    </td>
                                                    <td class="textos_01">
                                                        Numero de impresiones:
                                                    </td>
                                                    <td class="textos_01">
                                                        <asp:TextBox ID="txtNumImpresiones" runat="server"></asp:TextBox>
                                                    </td>
                                                </tr> 
                                                </table>
                                                 <table style="WIDTH:90%;" border="0" width="400px" >
                                                <tr align="center">
                                                    <td class="textos_01"  colspan="4" >
                                                        <asp:Button ID="btnAgregarWD" Width="150" CssClass="Boton_01" runat="server" Text="Agregar"
                                                            OnClick="btnAgregarWD_click" />
                                             
                                                        <asp:Button ID="btnAutorizaWD" Width="150" CssClass="Boton_01" runat="server" Text="Autorizar"
                                                            OnClick="btnAutorizaWD_click"     />
                                                  
                                            
                                                        <asp:Button ID="btnEliminarBancoWD" Width="150" CssClass="Boton_01" runat="server"
                                                            Text="Eliminar" OnClick="btnEliminarWD_click" />
                                              
                                                 
                                                        <asp:Button ID="btnDesactivarConfigBancoWD" Width="150" CssClass="Boton_01" runat="server"
                                                            Text="Desactivar" OnClick="btnDesactivarConfigBancoWD_click"  />
                                                            
                                                        <asp:HiddenField ID="hddConfiguracionBanco" runat="server" Value="0" />
                                                        <asp:HiddenField ID="hddCT" runat="server" Value="0" />
                                                        <asp:HiddenField ID="hddBanco" runat="server" Value="0" />
                                                        <asp:HiddenField ID="hddAut" runat="server" Value="true" />
                                                                  <asp:HiddenField ID="hddBan" runat="server" Value="0" />
                                                           <asp:HiddenField ID="hddVacAcu" runat="server" Value="true" />
                                                        <asp:CheckBox ID="AutWD" runat="server" Checked="false" Visible="false" />
                                                        <asp:Button ID="btnLlenaGridWD" CssClass="hidden" runat="server" Text="Button" OnClick="btnLlenaGridWD_click" />
                                                        <asp:Button ID="btnEnabled" CssClass="hidden" runat="server" Text="Button" OnClick="botonEnabled" />
                                                        <asp:Button ID="btnNuevo" CssClass="hidden" runat="server" Text="Button" OnClick="Nuevo" />
                                                        <asp:Button ID="btnGrid" CssClass="hidden" runat="server" Text="Button" OnClick="Grid" />
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <igtbl:UltraWebGrid ID="UltraWebGrid1" runat="server" CaptionAlign="Left" EnableAppStyling="False"
                                                DisplayLayout-AllowAddNewDefault="No" DisplayLayout-AddNewRowDefault-Visible="No"
                                                OnPageIndexChanged="cambio_pagina">
                                                <Bands>
                                                    <igtbl:UltraGridBand>
                                                        <Columns>
                                                            <igtbl:UltraGridColumn Width="124px" BaseColumnName="TipoArticulo" IsBound="true"
                                                                Key="TipoArticulo" CellMultiline="No">
                                                                <Header Caption="Tipo de articulo">
                                                                    <RowLayoutColumnInfo OriginX="1" />
                                                                </Header>
                                                                <HeaderStyle HorizontalAlign="Center" />
                                                                <Footer>
                                                                    <RowLayoutColumnInfo OriginX="1" />
                                                                </Footer>
                                                            </igtbl:UltraGridColumn>
                                                            <igtbl:UltraGridColumn Width="80px" BaseColumnName="Molde" IsBound="true" Key="Molde"
                                                                CellMultiline="No">
                                                                <Header Caption="Molde">
                                                                    <RowLayoutColumnInfo OriginX="2" />
                                                                </Header>
                                                                <HeaderStyle HorizontalAlign="Center" />
                                                                <Footer>
                                                                    <RowLayoutColumnInfo OriginX="2" />
                                                                </Footer>
                                                            </igtbl:UltraGridColumn>
                                                            <igtbl:UltraGridColumn Width="125px" BaseColumnName="Impresiones" IsBound="true"
                                                                Key="Impresiones" CellMultiline="No">
                                                                <Header Caption="Impresiones">
                                                                    <RowLayoutColumnInfo OriginX="3" />
                                                                </Header>
                                                                <HeaderStyle HorizontalAlign="Center" />
                                                                <Footer>
                                                                    <RowLayoutColumnInfo OriginX="3" />
                                                                </Footer>
                                                            </igtbl:UltraGridColumn>
                                                            <igtbl:UltraGridColumn Width="100px" BaseColumnName="CantidadMoldes" IsBound="true"
                                                                Key="CantidadMoldes" CellMultiline="No">
                                                                <Header Caption="Cantidad de moldes">
                                                                    <RowLayoutColumnInfo OriginX="4" />
                                                                </Header>
                                                                <HeaderStyle HorizontalAlign="Center" />
                                                                <Footer>
                                                                    <RowLayoutColumnInfo OriginX="4" />
                                                                </Footer>
                                                            </igtbl:UltraGridColumn>
                                                            <igtbl:UltraGridColumn Width="80px" BaseColumnName="Espacios" IsBound="True" Key="Espacios"
                                                                CellMultiline="Yes">
                                                                <Header Caption="Espacios">
                                                                    <RowLayoutColumnInfo OriginX="5" />
                                                                </Header>
                                                                <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                                                                <Footer>
                                                                    <RowLayoutColumnInfo OriginX="5" />
                                                                </Footer>
                                                            </igtbl:UltraGridColumn>
                                                            <igtbl:UltraGridColumn Width="120px" BaseColumnName="CapacidadReal" IsBound="True"
                                                                Key="CapacidadReal" CellMultiline="Yes">
                                                                <Header Caption="Capacidad real">
                                                                    <RowLayoutColumnInfo OriginX="6" />
                                                                </Header>
                                                                <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                                                                <Footer>
                                                                    <RowLayoutColumnInfo OriginX="6" />
                                                                </Footer>
                                                            </igtbl:UltraGridColumn>
                                                            <igtbl:UltraGridColumn Width="120px" BaseColumnName="LimiteVaciadas" IsBound="True"
                                                                Key="LimiteVaciadas" CellMultiline="Yes">
                                                                <Header Caption="Limite de vaciadas">
                                                                    <RowLayoutColumnInfo OriginX="7" />
                                                                </Header>
                                                                <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                                                                <Footer>
                                                                    <RowLayoutColumnInfo OriginX="7" />
                                                                </Footer>
                                                            </igtbl:UltraGridColumn>
                                                            <igtbl:UltraGridColumn Width="120px" BaseColumnName="VaciadasDiarias" IsBound="True"
                                                                Key="VaciadasDiarias" CellMultiline="Yes">
                                                                <Header Caption="Vaciadas Diarias">
                                                                    <RowLayoutColumnInfo OriginX="8" />
                                                                </Header>
                                                                <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                                                                <Footer>
                                                                    <RowLayoutColumnInfo OriginX="8" />
                                                                </Footer>
                                                            </igtbl:UltraGridColumn>
                                                            <igtbl:UltraGridColumn Width="120px" BaseColumnName="VaciadasAcumuladas" IsBound="True"
                                                                Key="VaciadasAcumuladas" CellMultiline="Yes">
                                                                <Header Caption="Vaciadas Acumuladas">
                                                                    <RowLayoutColumnInfo OriginX="9" />
                                                                </Header>
                                                                <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                                                                <Footer>
                                                                    <RowLayoutColumnInfo OriginX="9" />
                                                                </Footer>
                                                            </igtbl:UltraGridColumn>
                                                            <igtbl:UltraGridColumn Hidden="true" Width="100px" BaseColumnName="ExceptionMessage"
                                                                IsBound="True" CellMultiline="Yes">
                                                                <Header Caption="ExceptionMessage">
                                                                    <RowLayoutColumnInfo OriginX="10" />
                                                                </Header>
                                                                <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                                                                <CellStyle HorizontalAlign="Center">
                                                                </CellStyle>
                                                                <Footer>
                                                                    <RowLayoutColumnInfo OriginX="10" />
                                                                </Footer>
                                                            </igtbl:UltraGridColumn>
                                                        </Columns>
                                                        <AddNewRow Visible="NotSet" View="NotSet">
                                                        </AddNewRow>
                                                    </igtbl:UltraGridBand>
                                                </Bands>
                                                <DisplayLayout Name="UltraWebGrid1" AllowDeleteDefault="No" AllowUpdateDefault="No"
                                                    NoDataMessage="" RowHeightDefault="20px" SelectTypeRowDefault="Single" TableLayout="Fixed"
                                                    Version="3.00" CellClickActionDefault="RowSelect" LoadOnDemand="Xml" AllowAddNewDefault="Yes"
                                                    AllowColSizingDefault="Free" AllowSortingDefault="OnClient" CellPaddingDefault="1"
                                                    HeaderClickActionDefault="SortSingle" RowSelectorsDefault="No" RowSizingDefault="Free">
                                                    <RowAlternateStyleDefault BackColor="#E5E5E5">
                                                    </RowAlternateStyleDefault>
                                                    <Pager AllowPaging="False" PageSize="20">
                                                        <PagerStyle Font-Size="11px" Font-Names="Arial" BackColor="#666666" ForeColor="White"
                                                            Height="20px" BorderStyle="Solid" BorderColor="Black" BorderWidth="1px" />
                                                    </Pager>
                                                    <EditCellStyleDefault BackColor="silver">
                                                    </EditCellStyleDefault>
                                                    <FooterStyleDefault BackColor="LightGray" BorderStyle="Solid" BorderWidth="1px">
                                                    </FooterStyleDefault>
                                                    <HeaderStyleDefault BackColor="#666666" BorderColor="Black" BorderStyle="Solid" Font-Bold="True"
                                                        ForeColor="White">
                                                    </HeaderStyleDefault>
                                                    <RowSelectorStyleDefault BorderStyle="Solid">
                                                    </RowSelectorStyleDefault>
                                                    <RowStyleDefault BackColor="White" BorderColor="Black" BorderStyle="Solid" BorderWidth="1px"
                                                        Font-Names="Verdana" Font-Size="8pt" ForeColor="Black">
                                                    </RowStyleDefault>
                                                    <SelectedRowStyleDefault BackColor="#FFFFB3" ForeColor="Black" Font-Bold="True">
                                                    </SelectedRowStyleDefault>
                                                    <AddNewBox Hidden="true">
                                                    </AddNewBox>
                                                    <ActivationObject BorderColor="Black" BorderWidth="" BorderStyle="Dotted">
                                                    </ActivationObject>
                                                    <AddNewRowDefault View="Top">
                                                    </AddNewRowDefault>
                                                    <FilterOptionsDefault>
                                                        <FilterDropDownStyle BackColor="White" BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px"
                                                            CustomRules="overflow:auto;" Font-Names="Verdana,Arial,Helvetica,sans-serif"
                                                            Font-Size="11px" Width="200px">
                                                        </FilterDropDownStyle>
                                                        <FilterHighlightRowStyle BackColor="#999999" ForeColor="White">
                                                        </FilterHighlightRowStyle>
                                                    </FilterOptionsDefault>
                                                    <ClientSideEvents BeforeRowTemplateCloseHandler="BeforeRowTemplateCloseHandler" EditKeyDownHandler="KeyDown" />
                                                </DisplayLayout>
                                            </igtbl:UltraWebGrid>
                                        </td>
                                    </tr>
                                </tbody>
                            </table>

                            <script type="text/javascript" language="javascript">
                                function KeyDown(gridName, cellId, key) {
                                    if (key == 13) {

                                    }
                                }
      
                            </script>

                        </igmisc:WebAsyncRefreshPanel>
                    </div>
                </Template>
            </ContentPane>
        </ig:WebDialogWindow>
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
