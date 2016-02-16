<%@ Page Language="C#" MasterPageFile="~/ControlPisoLamosa.Master" Title="Lamosa - Configuración de Impresión de Etiquetas"
    AutoEventWireup="true" CodeBehind="ConfiguracionImpresionEtiqueta.aspx.cs" Inherits="LAMOSA.SCPP.Client.View.Administrador.Configuraciones.ConfiguracionImpresionEtiqueta" %>

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
    <div>
        <asp:ScriptManager ID="sm" runat="server">
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
                    <asp:Label ID="lblTitulo" runat="server" Text="Configuración de Impresión de Etiquetas"></asp:Label><br />
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
                        <table>
                            <tbody>
                                <tr>
                                    <td style="height: 40px" class="textos_01">
                                        <table>
                                            <tr>
                                                <td class="textos_01" style="text-align: left" width="170">
                                                    *Etiqueta (Modelo - Calidad):
                                                </td>
                                                <td class="textos_01" width="140">
                                                    <asp:DropDownList ID="ddlEtiqueta" Width="120px" OnSelectedIndexChanged="ddlEtiqueta_SelectIndexChanged"
                                                        runat="server" CssClass="textosd" AutoPostBack="true">
                                                    </asp:DropDownList>
                                                </td>
                                                <td class="textos_01" style="text-align: left" width="100">
                                                    *Impresiones:
                                                </td>
                                                <td class="textos_01" width="70">
                                                    <igtxt:WebNumericEdit ID="webNumImpresiones" runat="server" DataMode="Int" MaxLength="1"
                                                        MaxValue="9" MinValue="0" Width="50px" NullText="0">
                                                        <ClientSideEvents ValueChange="valueChangeInEdit" />
                                                    </igtxt:WebNumericEdit>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="textos_01" style="text-align: center; width: 240px;" valign="middle" 
                                                    colspan="2">
                                                    <asp:Button ID="btnBuscar" runat="server" class="Boton_01" Text="Buscar" OnClick="btnBuscar_Click"
                                                        OnClientClick="validarCapturaRequerida()" Width="80px" />
                                                </td>
                                                <td class="textos_01" style="text-align: left; width: 240px;" valign="middle" 
                                                    colspan="2">
                                                    <asp:Button ID="btnActualizar" runat="server" CssClass="Boton_01" OnClick="btnActualizar_Click"
                                                        Text="Actualizar" Width="80px" OnClientClick="validarActualizacion()"  />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="text-align: center; width: 480px" colspan="4">
                                                <igmisc:WebAsyncRefreshPanel ID="WebAsyncRefreshPanel1" runat="server" ClientError="WebAsyncRefreshPanel1_Error">
                                                    <igtbl:UltraWebGrid ID="uwgConfiguracionImpresionEtiquetas" runat="server" EnableAppStyling="False"
                                                        Height="250px" Width="300px" OnInitializeRow="uwgConfiguracionImpresionEtiquetas_InitializeRow">
                                                        <Bands>
                                                            <igtbl:UltraGridBand>
                                                                <AddNewRow Visible="NotSet" View="NotSet">
                                                                </AddNewRow>
                                                                <Columns>
                                                                    <igtbl:UltraGridColumn AllowUpdate="No" BaseColumnName="Modelo" Key="Modelo" Width="80px">
                                                                        <HeaderStyle HorizontalAlign="Center" />
                                                                        <CellStyle HorizontalAlign="Center" Width="80px">
                                                                        </CellStyle>
                                                                        <Header Caption="Modelo">
                                                                        </Header>
                                                                    </igtbl:UltraGridColumn>
                                                                    <igtbl:UltraGridColumn AllowUpdate="No" BaseColumnName="Calidad" Key="Calidad" Width="80px">
                                                                        <HeaderStyle HorizontalAlign="Center" />
                                                                        <CellStyle HorizontalAlign="Center" Width="80px">
                                                                        </CellStyle>
                                                                        <Header Caption="Calidad">
                                                                            <RowLayoutColumnInfo OriginX="1" />
                                                                        </Header>
                                                                        <Footer>
                                                                            <RowLayoutColumnInfo OriginX="1" />
                                                                        </Footer>
                                                                    </igtbl:UltraGridColumn>
                                                                    <igtbl:UltraGridColumn AllowUpdate="No" BaseColumnName="Impresiones" Key="Impresiones"
                                                                        Width="100px">
                                                                        <HeaderStyle HorizontalAlign="Center" />
                                                                        <CellStyle HorizontalAlign="Right" Width="100px">
                                                                        </CellStyle>
                                                                        <Header Caption="Impresiones">
                                                                            <RowLayoutColumnInfo OriginX="2" />
                                                                        </Header>
                                                                        <Footer>
                                                                            <RowLayoutColumnInfo OriginX="2" />
                                                                        </Footer>
                                                                    </igtbl:UltraGridColumn>
                                                                </Columns>
                                                            </igtbl:UltraGridBand>
                                                        </Bands>
                                                        <DisplayLayout Name="uwgConfiguracionImpresionEtiquetas" NoDataMessage="" RowHeightDefault="20px"
                                                            SelectTypeRowDefault="Single" TableLayout="Fixed" Version="3.00" CellClickActionDefault="RowSelect"
                                                            LoadOnDemand="Xml" CellPaddingDefault="1" HeaderClickActionDefault="SortSingle"
                                                            RowSelectorsDefault="No">
                                                            <RowAlternateStyleDefault BackColor="#E5E5E5">
                                                            </RowAlternateStyleDefault>
                                                            <FrameStyle Height="250px" Width="320px">
                                                            </FrameStyle>
                                                            <ClientSideEvents CellClickHandler="CellClick" />
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
                                                        </DisplayLayout>
                                                    </igtbl:UltraWebGrid>
                                                    </igmisc:WebAsyncRefreshPanel>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                            </tbody>
                        </table>

                    <script type="text/javascript">
                        function AfterRowActivate(gn, rowID) {
                            var cmb = $get('<%= ddlEtiqueta.ClientID %>');
                            var tb = igedit_getById('<%= webNumImpresiones.ClientID %>');
                            var grid = igtbl_getGridById('<%= uwgConfiguracionImpresionEtiquetas.ClientID %>');
                            var indexRow = rowID.toString().split('_')[2];
                            var sClave = grid.Rows.getRow(indexRow).getCell(0).getValue() + '-' + grid.Rows.getRow(indexRow).getCell(1).getValue();
                            var iImpresiones = parseInt(grid.Rows.getRow(indexRow).getCell(2).getValue());
                            tb.setValue(iImpresiones);
                            cmb.value = sClave;
                        }
                        function WebAsyncRefreshPanel1_Error(oPanel, oEvent, flags) {
                            var serverError = ig_shared.getCBManager().serverError;
                            alert(serverError);
                        }
                        function validarActualizacion() {
                            try {
                                var cmb = $get('<%= ddlEtiqueta.ClientID %>');
                                if (cmb.value == null | cmb.value == '' | cmb.value == "0") {
                                    return false;
                                }
                            }
                            catch (e) {
                                alert(e.message);
                                return false;
                            }
                            finally {
                                cmb = null;
                            }
                            return true;
                        }
                        function validarCapturaRequerida() {
                            try {
                                var cmb = $get('<%= ddlEtiqueta.ClientID %>');
                                var tb = $get('<%= webNumImpresiones.ClientID %>');
                                if (cmb.value == null | cmb.value == '') {
                                    alert('Es necesario capturar el campo Etiqueta.');
                                    return false;
                                }
                                if ((tb.value == null | tb.value == '') & cmb.value != "0") {
                                    alert('Es necesario capturar el campo Impresiones.');
                                    return false;
                                }
                            }
                            catch (e) {
                                alert(e.message);
                                return false;
                            }
                            finally {
                                cmb = null;
                                tb = null;
                            }
                            return true;
                        }
                        function valueChangeInEdit(oEdit, oldValue, oEvent) {
                            //alert(oldValue);
                            //alert(oEdit.getValue());
                            //alert(oEdit.getText());
                            var btn = $get('<%= btnActualizar.ClientID %>');
                            if (oEdit.getValue() == null) {
                                // restore old value
                                oEvent.cancel = true;
                                return;
                            }
                            if (oldValue == oEdit.getValue()) {
                                // restore old value
                                oEvent.cancel = true;
                                return;
                            }
                            btn.enable = false;
                            // trigger postback of page
                            oEvent.needPostBack = true;
                            //igtbl_cancelPostBack(igtbl_getGridById('<%= uwgConfiguracionImpresionEtiquetas.ClientID %>'));
                            /*if (oEdit.getText() == "post me to server") {
                                oEvent.needPostBack = true;
                            }*/
                        }
                        function textChangedInEdit(oEdit, newText, oEvent) {
                            //status = oEdit.ID + "  text:" + newText;
                            //alert(status);
                            // trigger postback of page
                            //if (newText == "ok")
                            oEvent.needPostBack = true;
                        }
                        function RowSelectorClick(gridName, rowId, button) {
                            alert(gridName);
                            alert(rowId);
                            alert(button);
                        }
                        function CellClick(gridName, cellId, button) {
                            try {
                                var cmb = $get('<%= ddlEtiqueta.ClientID %>');
                                var tb = igedit_getById('<%= webNumImpresiones.ClientID %>');
                                var grid = igtbl_getGridById('<%= uwgConfiguracionImpresionEtiquetas.ClientID %>');
                                var indexRow = cellId.toString().split('_')[2];
                                var sClave = grid.Rows.getRow(indexRow).getCell(0).getValue() + '-' + grid.Rows.getRow(indexRow).getCell(1).getValue();
                                var iImpresiones = parseInt(grid.Rows.getRow(indexRow).getCell(2).getValue());
                                tb.setValue(iImpresiones);
                                cmb.value = sClave;
                            }
                            catch (e) {
                                alert(e.message);
                            }
                        }
                    </script>

                </td>
            </tr>
            <tr>
                <td colspan="3" />
            </tr>
        </table>
    </div>
</asp:Content>
