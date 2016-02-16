<%@ Page    Title="Control de piso - Reporte de Asignación de Etiquetas" 
            Language="C#" 
            MasterPageFile="~/ControlPisoLamosa.Master" 
            AutoEventWireup="true" 
            CodeBehind="frmReporteAsignacionEtiquetas.aspx.cs" 
            Inherits="LAMOSA.SCPP.Client.View.Administrador.Reportes.frmReporteAsignacionEtiquetas" %>
<%@ Register assembly="Infragistics35.WebUI.WebDateChooser.v9.1, Version=9.1.20091.1015, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" namespace="Infragistics.WebUI.WebSchedule" tagprefix="igsch" %>
<asp:Content ID="Content1" ContentPlaceHolderID="Principal" runat="server">
<table border="0" cellpadding ="0" cellspacing="0" style="text-align:center; vertical-align:middle;width:1024px;padding:0px;">
<tr style="height:30px">
<td  class="lblTitulo1" style="text-align:left"> <asp:Label ID="lblTituloPagina" runat="server" Text="Reporte de Asignación de Etiqueta" ToolTip="Reporte de Asignación de Etiqueta"></asp:Label></td>
</tr>
<tr style="height:50px"><td/></tr>
<tr>
<td>
<table style="width:100%">
<tr>
<td style="text-align:left; width:20%"></td>
<td style="text-align:right; width:10%"><asp:Label CssClass="label" ID="lblPlanta" runat="server" Text="Planta:"></asp:Label></td>
<td style="text-align:left; width:15%"><asp:DropDownList CssClass="textos_01" 
        ID="cmbPlanta" Width="140px" runat="server"></asp:DropDownList></td>
<td style="text-align:left; width:5%"></td>
<td style="text-align:right; width:15%"><asp:Label CssClass="label" ID="lblCentroTrabajo" runat="server" Text="Centro Trabajo:"></asp:Label></td>
<td style="text-align:left;width:15%"><asp:DropDownList CssClass="textos_01" 
        ID="cmbCentroTrabajo" Width="140px" runat="server"></asp:DropDownList></td>
<td style="text-align:left; width:20%"></td>
</tr>
<tr>
<td style="text-align:left; width:20%"></td>
<td style="text-align:right; width:10%"><asp:Label CssClass="label" ID="lblEmpleado" runat="server" Text="Empleado:"></asp:Label></td>
<td style="text-align:left; width:15%"> <asp:TextBox CssClass="textos_01" 
        ID="tbEmpleado" Width="140px" runat="server"></asp:TextBox></td>
<td style="text-align:left; width:5%"></td>
<td style="text-align:right; width:15%"><asp:Label CssClass="label" ID="lblMaquina" runat="server" Text="Maquina:"></asp:Label></td>
<td style="text-align:left;width:15%"><asp:DropDownList CssClass="textos_01" 
        ID="cmbMaquina" Width="140px" runat="server"></asp:DropDownList></td>
<td style="text-align:left; width:20%"></td>
</tr>
<tr>
<td style="text-align:left; width:20%"></td>
<td style="text-align:right; width:10%"><asp:Label CssClass="label" ID="lblFecha" runat="server" Text="Fecha:"></asp:Label></td>
<td style="text-align:left; width:15%"><igsch:WebDateChooser CssClass="textos" ID="wdateFechaIni" runat="server"></igsch:WebDateChooser></td>
<td style="text-align:center; width:5%"><asp:Label CssClass="label" ID="lblRango" runat="server" Text="-"></asp:Label></td>
<td style="text-align:right; width:15%"><igsch:WebDateChooser CssClass="textos" ID="wdateFechaFin" runat="server"></igsch:WebDateChooser></td>
<td style="text-align:left;width:15%"><asp:Button CssClass="Boton_01" ID="btnBuscarAsignacionEtiqueta" runat="server" Text="Buscar" /></td>
<td style="text-align:left; width:20%"></td>
</tr>
<tr>
<td colspan="7" style="width:100%; text-align:center">
<table width="100%">
<tr>
<td colspan="5" style="text-align:center; width:100%">
    <asp:GridView ID="gridViewAsignacionEtiquetas" runat="server" 
        AutoGenerateColumns="False" Caption="Resumen de Asignación de Etiquetas" 
        CaptionAlign="Bottom" CssClass="label" AllowPaging="True" PageSize="20">
        <RowStyle CssClass="textos_01" />
        <Columns>
            <asp:BoundField DataField="FechaEntrega" DataFormatString="dd/MM/yyyy" 
                HeaderText="Fecha" ReadOnly="True">
            <HeaderStyle HorizontalAlign="Center" CssClass="label" />
            <ItemStyle HorizontalAlign="Center" />
            </asp:BoundField>
            <asp:BoundField DataField="DescCentroTrabajo" HeaderText="Centro Trabajo" 
                ReadOnly="True">
            <HeaderStyle HorizontalAlign="Center" CssClass="label" />
            <ItemStyle HorizontalAlign="Left" />
            </asp:BoundField>
            <asp:BoundField DataField="DescMaquina" HeaderText="Maquina" ReadOnly="True">
            <HeaderStyle HorizontalAlign="Center" CssClass="label" />
            <ItemStyle HorizontalAlign="Left" />
            </asp:BoundField>
            <asp:BoundField DataField="ClaveEmpleado" HeaderText="Clave Empleado" 
                ReadOnly="True">
            <HeaderStyle HorizontalAlign="Center" CssClass="label" />
            <ItemStyle HorizontalAlign="Center" />
            </asp:BoundField>
            <asp:BoundField DataField="NombreEmpleado" HeaderText="Empleado" 
                ReadOnly="True">
            <HeaderStyle HorizontalAlign="Center" CssClass="label" />
            <ItemStyle HorizontalAlign="Left" />
            </asp:BoundField>
            <asp:BoundField DataField="RangoCodigo" HeaderText="Rango Código" 
                ReadOnly="True">
            <HeaderStyle HorizontalAlign="Center" CssClass="label" />
            <ItemStyle HorizontalAlign="Center" />
            </asp:BoundField>
            <asp:BoundField DataField="Entregados" HeaderText="Entregados" ReadOnly="True">
            <HeaderStyle HorizontalAlign="Center" CssClass="label" />
            <ItemStyle HorizontalAlign="Center" />
            </asp:BoundField>
            <asp:BoundField DataField="Usados" HeaderText="Usados" ReadOnly="True">
            <HeaderStyle HorizontalAlign="Center" CssClass="label" />
            <ItemStyle HorizontalAlign="Center" />
            </asp:BoundField>
            <asp:HyperLinkField DataTextField="SinUso" HeaderText="Sin Uso">
            <HeaderStyle HorizontalAlign="Center" CssClass="label" />
            <ItemStyle HorizontalAlign="Center" />
            </asp:HyperLinkField>
            <asp:BoundField DataField="PorcUso" HeaderText="% Uso" ReadOnly="True">
            <HeaderStyle HorizontalAlign="Center" CssClass="label" />
            <ItemStyle HorizontalAlign="Center" />
            </asp:BoundField>
            <asp:BoundField DataField="CodigoSinUso" HeaderText="CodigoSinUso" 
                ShowHeader="False" Visible="False" />
        </Columns>
        <SelectedRowStyle CssClass="label" />
        <HeaderStyle CssClass="label" />
        <AlternatingRowStyle BackColor="#E5E5E5" />
    </asp:GridView>
</td>
</tr></table>
</td>
</tr>
</table>
</td>
</tr>
</table>
</asp:Content>
