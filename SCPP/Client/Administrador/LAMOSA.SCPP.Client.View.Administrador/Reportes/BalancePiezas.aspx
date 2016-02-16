<%@ Page Language="C#" MasterPageFile="~/ControlPisoLamosa.Master" AutoEventWireup="true"
    CodeBehind="BalancePiezas.aspx.cs" Inherits="LAMOSA.SCPP.Client.View.Administrador.Reportes.BalancePiezas"
    Title="Control de piso - Balance de piezas" %>

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
                <td style="height: 10px" colspan="4">
                </td>
            </tr>
            <tr style="height: 30px;">
                <td style="width: 10px; background-color: #eee;">
                </td>
                <td colspan="6" class="lblTitulo1">
                    <asp:Label ID="lblTitulo" runat="server" Text="Balance de piezas"></asp:Label><br />
                </td>
            </tr>
            <tr>
                <td style="height: 10px" colspan="4">
                </td>
            </tr>
            <tr>
                <td style="width: 10px;" rowspan="2">
                </td>
                <td rowspan="2" valign="top" class="leftarea" style="width: 100px">
                    <div id="navcontainer">
                        <ul id="navlist">
                            <li><a href="javascript:ListItemSelected(2,'')" id="LExport" runat="server">
                                <img src="../Imagenes/Exportar.png" alt="Exportar tabla" style="border: 0px;" />
                                Exportar tabla</a></li>
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
                        <table style="height: 200px; width: 550px">
                            <tbody>
                                <tr>
                                    <td style="height: 40px" class="textos_01">
                                        <table style="width: 550px">
                                            <tr>
                                                <td class="textos_01">
                                                    Almacén:
                                                </td>
                                                <td class="textos_01">
                                                    <asp:DropDownList ID="cmbalmacen" runat="server" CssClass="textosd">
                                                        <asp:ListItem Text="Todos" Value="0">
                                                        </asp:ListItem>
                                                        <asp:ListItem Text="Monterrey" Value="1">
                                                        </asp:ListItem>
                                                        <asp:ListItem Text="Benito Juarez" Value="2">
                                                        </asp:ListItem>
                                                    </asp:DropDownList>
                                                </td>
                                                <td class="textos_01">
                                                    Proceso:
                                                </td>
                                                <td class="textos_01">
                                                    <asp:DropDownList ID="cmbMaquina" runat="server" CssClass="textosd">
                                                        <asp:ListItem Value="1" Text="Banco 1"></asp:ListItem>
                                                        <asp:ListItem Value="2" Text="Banco 2"></asp:ListItem>
                                                    </asp:DropDownList>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="textos_01">
                                                    Planta:
                                                </td>
                                                <td class="textos_01">
                                                    <asp:DropDownList ID="cmbplanta" runat="server" CssClass="textosd">
                                                        <asp:ListItem Text="Todos" Value="0">
                                                        </asp:ListItem>
                                                        <asp:ListItem Text="Planta 1" Value="1">
                                                        </asp:ListItem>
                                                    </asp:DropDownList>
                                                </td>
                                                <td class="textos_01">
                                                    Modelo:
                                                </td>
                                                <td class="textos_01">
                                                    <asp:DropDownList ID="cmbModelo" runat="server" CssClass="textosd">
                                                        <asp:ListItem Text="Todos" Value="0"></asp:ListItem>
                                                        <asp:ListItem Text="Venecia" Value="1"></asp:ListItem>
                                                        <asp:ListItem Text="Regency" Value="2"></asp:ListItem>
                                                        <asp:ListItem Text="One Piece" Value="3"></asp:ListItem>
                                                    </asp:DropDownList>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="textos_01">
                                                    Tipo de artículo:
                                                </td>
                                                <td class="textos_01">
                                                    <asp:DropDownList ID="cmbTipoArt" runat="server" CssClass="textosd">
                                                        <asp:ListItem Text="Todos" Value="0"></asp:ListItem>
                                                        <asp:ListItem Text="Taza" Value="1"></asp:ListItem>
                                                        <asp:ListItem Text="Lavabo" Value="2"></asp:ListItem>
                                                        <asp:ListItem Text="Pedestal" Value="3"></asp:ListItem>
                                                        <asp:ListItem Text="Mingitorio" Value="3"></asp:ListItem>
                                                    </asp:DropDownList>
                                                </td>
                                                <td class="textos_01">
                                                </td>
                                                <td class="textos_01">
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="center" style="height: 26px" class="textos_01">
                                        <input id="igtbl_reBuscaBtn" type="button" class="Boton_01" 
                                            value="Buscar" style="width: 75px" runat="server" />
                                    </td>
                                </tr>
                                <tr>
                                    <td>

                                        <script src="../FuncionesJS/jquery-1.4.2.js" type="text/javascript"></script>

                                        <div style="overflow: scroll; width: 850px;">
                                            <igtbl:UltraWebGrid ID="UltraWebGrid1" runat="server" CaptionAlign="Left" EnableAppStyling="False">
                                                <Bands>
                                                    <igtbl:UltraGridBand>
                                                        <AddNewRow>
                                                            <RowStyle VerticalAlign="Top" />
                                                        </AddNewRow>
                                                        <Columns>
                                                            <igtbl:UltraGridColumn Width="75px" BaseColumnName="CT" DataType="System.Int32" IsBound="True"
                                                                Key="CT" Hidden="False">
                                                                <Header Caption="CT">
                                                                </Header>
                                                                <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                                                            </igtbl:UltraGridColumn>
                                                            <igtbl:UltraGridColumn Width="75px" BaseColumnName="Inv Inicial" IsBound="True" Key="Inv Inicial">
                                                                <Header Caption="Inv Inicial">
                                                                    <RowLayoutColumnInfo OriginX="1" />
                                                                </Header>
                                                                <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                                                                <Footer>
                                                                    <RowLayoutColumnInfo OriginX="1" />
                                                                </Footer>
                                                            </igtbl:UltraGridColumn>
                                                            <igtbl:UltraGridColumn Width="80px" BaseColumnName="Entradas" IsBound="True" Key="Entradas">
                                                                <Header Caption="Entradas">
                                                                    <RowLayoutColumnInfo OriginX="2" />
                                                                </Header>
                                                                <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                                                                <Footer>
                                                                    <RowLayoutColumnInfo OriginX="2" />
                                                                </Footer>
                                                            </igtbl:UltraGridColumn>
                                                            <igtbl:UltraGridColumn Width="90px" BaseColumnName="Salidas" IsBound="True" Key="Salidas">
                                                                <Header Caption="Salidas">
                                                                    <RowLayoutColumnInfo OriginX="3" />
                                                                </Header>
                                                                <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                                                                <Footer>
                                                                    <RowLayoutColumnInfo OriginX="3" />
                                                                </Footer>
                                                            </igtbl:UltraGridColumn>
                                                            <igtbl:UltraGridColumn Width="100px" BaseColumnName="Desperdicio" IsBound="True"
                                                                Key="Desperdicio">
                                                                <Header Caption="Desperdicio">
                                                                    <RowLayoutColumnInfo OriginX="4" />
                                                                </Header>
                                                                <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                                                                <Footer>
                                                                    <RowLayoutColumnInfo OriginX="4" />
                                                                </Footer>
                                                            </igtbl:UltraGridColumn>
                                                            <igtbl:UltraGridColumn Width="100px" BaseColumnName="Inv Fisico" IsBound="True" Key="Inv Fisico">
                                                                <Header Caption="Inv Fisico">
                                                                    <RowLayoutColumnInfo OriginX="5" />
                                                                </Header>
                                                                <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                                                                <Footer>
                                                                    <RowLayoutColumnInfo OriginX="5" />
                                                                </Footer>
                                                            </igtbl:UltraGridColumn>
                                                            <igtbl:UltraGridColumn Width="100px" BaseColumnName="Ajuste" IsBound="True" Key="Ajuste">
                                                                <Header Caption="Ajuste">
                                                                    <RowLayoutColumnInfo OriginX="6" />
                                                                </Header>
                                                                <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                                                                <Footer>
                                                                    <RowLayoutColumnInfo OriginX="6" />
                                                                </Footer>
                                                            </igtbl:UltraGridColumn>
                                                        </Columns>
                                                        <RowTemplateStyle BackColor="White" BorderColor="White" BorderStyle="Ridge">
                                                            <BorderDetails WidthBottom="3px" WidthLeft="3px" WidthRight="3px" WidthTop="3px" />
                                                        </RowTemplateStyle>
                                                        <AddNewRow View="NotSet" Visible="NotSet">
                                                        </AddNewRow>
                                                    </igtbl:UltraGridBand>
                                                </Bands>
                                                <DisplayLayout Name="UltraWebGrid1" AllowDeleteDefault="Yes" NoDataMessage="" RowHeightDefault="20px"
                                                    SelectTypeRowDefault="Single" TableLayout="Fixed" Version="3.00" CellClickActionDefault="CellSelect"
                                                    LoadOnDemand="Xml" AllowAddNewDefault="Yes" AllowColSizingDefault="Free" AllowSortingDefault="No"
                                                    CellPaddingDefault="1" HeaderClickActionDefault="SortSingle" RowSelectorsDefault="No"
                                                    RowSizingDefault="Free" StationaryMargins="Header">
                                                    <FrameStyle Height="400px" Width="800px">
                                                    </FrameStyle>
                                                    <RowAlternateStyleDefault BackColor="#E5E5E5">
                                                    </RowAlternateStyleDefault>
                                                    <Pager AllowPaging="False">
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
                                                    <ClientSideEvents CellClickHandler="modal" />
                                                </DisplayLayout>
                                            </igtbl:UltraWebGrid>
                                        </div>
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
                              function ListItemSelected(idList, varList) {
                                  //var nuevo = document.getElementById("<%=hddSecurityConstants.ClientID %>").getAttribute('value').indexOf('1');
                                  //   var exporta = document.getElementById("<%=hddSecurityConstants.ClientID %>").getAttribute('value').indexOf('6');

                                  if (idList == 2) {//Exportar
                                       $find('<%=WebDialogWindow1.ClientID%>').set_windowState($IG.DialogWindowState.Normal);
                                      
                                  }
                              }


                              function modal(event) 
                              {
                                  $find('<%=WebDialogWindow2.ClientID%>').set_windowState($IG.DialogWindowState.Normal);
                                  $("#<%=BRefresh.ClientID%>").click();
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
                    <ig:WebDialogWindow ID="WebDialogWindow2" runat="server" InitialLocation="Centered"
                        Height="280px" Width="1265px" Modal="true" WindowState="Hidden" Font-Size="10px">
                        <ContentPane BackColor="#FAFAFA">
                            <Template>
                                <div style="padding: 5px;">
                                    <table>
                                        <tr>
                                            <td>
                                                <asp:Label ID="Label1" runat="server" Text="CT: 603S"></asp:Label><br />
                                            </td>
                                            <br />
                                        </tr>
                                    </table>
                                    <igtbl:UltraWebGrid ID="UltraWebGrid2" runat="server" CaptionAlign="Left" EnableAppStyling="False">
                                        <Bands>
                                            <igtbl:UltraGridBand>
                                                <Columns>
                                                    <igtbl:UltraGridColumn Width="75px" BaseColumnName="num" DataType="System.Int32"
                                                        IsBound="True" Key="num" Hidden="False">
                                                        <Header Caption="#">
                                                        </Header>
                                                        <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                                                    </igtbl:UltraGridColumn>
                                                    <igtbl:UltraGridColumn Width="75px" BaseColumnName="Linea" IsBound="True" Key="Linea">
                                                        <Header Caption="Linea">
                                                            <RowLayoutColumnInfo OriginX="1" />
                                                        </Header>
                                                        <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                                                        <Footer>
                                                            <RowLayoutColumnInfo OriginX="1" />
                                                        </Footer>
                                                    </igtbl:UltraGridColumn>
                                                    <igtbl:UltraGridColumn Width="80px" BaseColumnName="Modelo" IsBound="True" Key="Modelo">
                                                        <Header Caption="Modelo">
                                                            <RowLayoutColumnInfo OriginX="2" />
                                                        </Header>
                                                        <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                                                        <Footer>
                                                            <RowLayoutColumnInfo OriginX="2" />
                                                        </Footer>
                                                    </igtbl:UltraGridColumn>
                                                    <igtbl:UltraGridColumn Width="90px" BaseColumnName="Modelo2" IsBound="True" Key="Modelo2">
                                                        <Header Caption="Modelo">
                                                            <RowLayoutColumnInfo OriginX="3" />
                                                        </Header>
                                                        <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                                                        <Footer>
                                                            <RowLayoutColumnInfo OriginX="3" />
                                                        </Footer>
                                                    </igtbl:UltraGridColumn>
                                                    <igtbl:UltraGridColumn Width="90px" BaseColumnName="Descripcion" IsBound="True" Key="Descripcion">
                                                        <Header Caption="Descripción">
                                                            <RowLayoutColumnInfo OriginX="3" />
                                                        </Header>
                                                        <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                                                        <Footer>
                                                            <RowLayoutColumnInfo OriginX="3" />
                                                        </Footer>
                                                    </igtbl:UltraGridColumn>
                                                    <igtbl:UltraGridColumn Width="90px" BaseColumnName="M/C" IsBound="True" Key="M/C">
                                                        <Header Caption="M/C">
                                                            <RowLayoutColumnInfo OriginX="3" />
                                                        </Header>
                                                        <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                                                        <Footer>
                                                            <RowLayoutColumnInfo OriginX="3" />
                                                        </Footer>
                                                    </igtbl:UltraGridColumn>
                                                    <igtbl:UltraGridColumn Width="90px" BaseColumnName="Pta" IsBound="True" Key="Pta">
                                                        <Header Caption="Pta">
                                                            <RowLayoutColumnInfo OriginX="3" />
                                                        </Header>
                                                        <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                                                        <Footer>
                                                            <RowLayoutColumnInfo OriginX="3" />
                                                        </Footer>
                                                    </igtbl:UltraGridColumn>
                                                    <igtbl:UltraGridColumn Width="90px" BaseColumnName="Pta2" IsBound="True" Key="Pta2">
                                                        <Header Caption="Pta">
                                                            <RowLayoutColumnInfo OriginX="3" />
                                                        </Header>
                                                        <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                                                        <Footer>
                                                            <RowLayoutColumnInfo OriginX="3" />
                                                        </Footer>
                                                    </igtbl:UltraGridColumn>
                                                    <igtbl:UltraGridColumn Width="90px" BaseColumnName="Inv Inicial" IsBound="True" Key="Inv Inicial">
                                                        <Header Caption="Inv Inicial">
                                                            <RowLayoutColumnInfo OriginX="3" />
                                                        </Header>
                                                        <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                                                        <Footer>
                                                            <RowLayoutColumnInfo OriginX="3" />
                                                        </Footer>
                                                    </igtbl:UltraGridColumn>
                                                    <igtbl:UltraGridColumn Width="90px" BaseColumnName="Entradas" IsBound="True" Key="Entradas">
                                                        <Header Caption="Entradas">
                                                            <RowLayoutColumnInfo OriginX="3" />
                                                        </Header>
                                                        <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                                                        <Footer>
                                                            <RowLayoutColumnInfo OriginX="3" />
                                                        </Footer>
                                                    </igtbl:UltraGridColumn>
                                                    <igtbl:UltraGridColumn Width="90px" BaseColumnName="Salidas" IsBound="True" Key="Salidas">
                                                        <Header Caption="Salidas">
                                                            <RowLayoutColumnInfo OriginX="3" />
                                                        </Header>
                                                        <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                                                        <Footer>
                                                            <RowLayoutColumnInfo OriginX="3" />
                                                        </Footer>
                                                    </igtbl:UltraGridColumn>
                                                    <igtbl:UltraGridColumn Width="90px" BaseColumnName="Desperdicio" IsBound="True" Key="Desperdicio">
                                                        <Header Caption="Desperdicio">
                                                            <RowLayoutColumnInfo OriginX="3" />
                                                        </Header>
                                                        <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                                                        <Footer>
                                                            <RowLayoutColumnInfo OriginX="3" />
                                                        </Footer>
                                                    </igtbl:UltraGridColumn>
                                                    <igtbl:UltraGridColumn Width="90px" BaseColumnName="Inv Fisico" IsBound="True" Key="Inv Fisico">
                                                        <Header Caption="Inv Fisico">
                                                            <RowLayoutColumnInfo OriginX="3" />
                                                        </Header>
                                                        <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                                                        <Footer>
                                                            <RowLayoutColumnInfo OriginX="3" />
                                                        </Footer>
                                                    </igtbl:UltraGridColumn>
                                                    <igtbl:UltraGridColumn Width="90px" BaseColumnName="Ajuste" IsBound="True" Key="Ajuste">
                                                        <Header Caption="Ajuste">
                                                            <RowLayoutColumnInfo OriginX="3" />
                                                        </Header>
                                                        <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                                                        <Footer>
                                                            <RowLayoutColumnInfo OriginX="3" />
                                                        </Footer>
                                                    </igtbl:UltraGridColumn>
                                                </Columns>
                                                <AddNewRow View="NotSet" Visible="NotSet">
                                                </AddNewRow>
                                            </igtbl:UltraGridBand>
                                        </Bands>
                                        <DisplayLayout Name="UltraWebGrid2" AllowDeleteDefault="Yes" NoDataMessage="" RowHeightDefault="20px"
                                            SelectTypeRowDefault="Single" TableLayout="Fixed" Version="3.00" CellClickActionDefault="RowSelect"
                                            LoadOnDemand="Xml" AllowAddNewDefault="Yes" AllowColSizingDefault="Free" AllowSortingDefault="No"
                                            CellPaddingDefault="1" HeaderClickActionDefault="SortSingle" RowSelectorsDefault="No"
                                            RowSizingDefault="Free">
                                            <RowAlternateStyleDefault BackColor="#E5E5E5">
                                            </RowAlternateStyleDefault>
                                            <Pager AllowPaging="False">
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
                                            <ClientSideEvents />
                                        </DisplayLayout>
                                    </igtbl:UltraWebGrid>
                                </div>
                            </Template>
                        </ContentPane>
                    </ig:WebDialogWindow>
                </td>
            </tr>
            <tr>
                <td colspan="3">
                    <igtblexp:UltraWebGridExcelExporter ID="uwgBalanceP" WorksheetName="BalancePiezas"
                        DownloadName="Reporte.XLS" runat='server'>
                    </igtblexp:UltraWebGridExcelExporter>
                </td>
            </tr>
        </table>
        <asp:Button OnClick="LlenaModal" ID="BRefresh" runat="server" Text="Reload" CssClass="hidden" />
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
