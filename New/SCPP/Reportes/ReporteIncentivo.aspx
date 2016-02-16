<%@ Page Title="" Language="C#" MasterPageFile="~/ControlPisoLamosa.Master" AutoEventWireup="true"
    CodeBehind="ReporteIncentivo.aspx.cs" Inherits="LAMOSA.SCPP.Client.View.Administrador.Reportes.ReporteIncentivo" %>

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
        <table>
            <tr>
                <td>
                    <igmisc:WebAsyncRefreshPanel ID="warpReporteIncentivo" runat="server">
                        <table id="tableFilterID" style="width: 100%">
                            <tr>
                                <td colspan="6" style="text-align: left">
                                    <asp:Label ID="lblReporteIncentivo" runat="server" Text="Reporte de Incentivos"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="6">
                                </td>
                            </tr>
                            <tr>
                                <td style="text-align: right">
                                    <asp:Label ID="lblPlanta" runat="server" Text="Planta:"></asp:Label>
                                </td>
                                <td style="text-align: left; width: 20%">
                                    <asp:DropDownList ID="cbPlanta" runat="server" Width="150px">
                                    </asp:DropDownList>
                                </td>
                                <td style="text-align: right">
                                    <asp:Label ID="lblTipoArticulo" runat="server" Text="Tipo Articulo:"></asp:Label>
                                </td>
                                <td style="text-align: left; width: 20%">
                                    <asp:DropDownList ID="cbTipoArticulo" runat="server" Width="200px" AutoPostBack="True"
                                        Enabled="False" OnSelectedIndexChanged="cbTipoArticulo_SelectedIndexChanged">
                                    </asp:DropDownList>
                                </td>
                                <td style="text-align: right">
                                    <asp:Label ID="Label1" runat="server" Text="Modelo:"></asp:Label>
                                </td>
                                <td style="text-align: left; width: 25%">
                                    <asp:DropDownList ID="cbModelo" runat="server" Width="250px">
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td style="text-align: right">
                                    <asp:Label ID="lblFechaInicial" runat="server" Text="Fecha Desde:"></asp:Label>
                                </td>
                                <td style="text-align: left">
                                    <igsch:WebDateChooser ID="wdcFechaInicial" runat="server" Width="150" Value="">
                                    </igsch:WebDateChooser>
                                </td>
                                <td style="text-align: right">
                                    <asp:Label ID="lblFechaFinal" runat="server" Text="Fecha Hasta:"></asp:Label>
                                </td>
                                <td style="text-align: left">
                                    <igsch:WebDateChooser ID="wdcFechaFinal" runat="server" Width="150">
                                    </igsch:WebDateChooser>
                                </td>
                                <td style="text-align: left">
                                    <asp:Button ID="btnBuscar" runat="server" Text="Buscar" OnClick="btnBuscar_Click" />
                                </td>
                                <td style="text-align: left">
                                </td>
                            </tr>
                            <tr>
                                <td colspan="6">
                                    <br />
                                </td>
                            </tr>
                            <tr>
                                <td colspan="6">
                                    <br />
                                </td>
                            </tr>
                            <tr>
                                <td colspan="6">
                                    <asp:GridView ID="gvReporteIncentivo" runat="server" AutoGenerateColumns="False"
                                        Font-Names="Arial" Font-Size="9pt" AlternatingRowStyle-BackColor="#E5E5E5" HeaderStyle-BackColor="green">
                                        <Columns>
                                            <asp:BoundField DataField="Item" HeaderText="Item" ReadOnly="True">
                                                <ItemStyle Width="45px" HorizontalAlign="Center" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="NoNomina" HeaderText="NoNomina" ReadOnly="True">
                                                <ItemStyle Width="80px" HorizontalAlign="Center" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="NombreOperador" HeaderText="Operador" ReadOnly="True">
                                                <ItemStyle Width="330px" HorizontalAlign="Left" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="PiezasProcesadas" HeaderText="Pzas. Proc." ReadOnly="True">
                                                <ItemStyle Width="50px" HorizontalAlign="Center" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="MalasHastaRevisado" HeaderText="Malas -&gt; Revisado"
                                                ReadOnly="True">
                                                <ItemStyle Width="90px" HorizontalAlign="Center" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="PorcentajePV" HeaderText="% PV" ReadOnly="True">
                                                <ItemStyle Width="50px" HorizontalAlign="Center" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="NoCalidadClasificacion" HeaderText="Malas -&gt; Clasificado"
                                                ReadOnly="True">
                                                <ItemStyle Width="90px" HorizontalAlign="Center" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="PorcentajePQ" HeaderText="% PQ" ReadOnly="True">
                                                <ItemStyle Width="50px" HorizontalAlign="Center" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="PiezasBase" HeaderText="Pzas. Base" ReadOnly="True">
                                                <ItemStyle Width="50px" HorizontalAlign="Center" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="PiezasPagar" HeaderText="Pzas. a Pagar" ReadOnly="True">
                                                <ItemStyle Width="50px" HorizontalAlign="Center" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="PremioPieza" HeaderText="Prem. Pieza" ReadOnly="True">
                                                <ItemStyle Width="50px" HorizontalAlign="Center" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="Total" HeaderText="Total" ReadOnly="True">
                                                <ItemStyle Width="60px" HorizontalAlign="Right" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="DiasVaciados" HeaderText="Dias Vac." ReadOnly="True">
                                                <ItemStyle Width="50px" HorizontalAlign="Center" />
                                            </asp:BoundField>
                                        </Columns>
                                        <HeaderStyle BackColor="#990000" />
                                        <AlternatingRowStyle BackColor="#E5E5E5" />
                                    </asp:GridView>
                                </td>
                            </tr>
                        </table>
                    </igmisc:WebAsyncRefreshPanel>
                </td>
            </tr>
        </table>
    </div>
    <div>
        <asp:Button ID="btnExportar" runat="server" OnClick="btnExportar_Click" Text="Exportar" />
    </div>
</asp:Content>
