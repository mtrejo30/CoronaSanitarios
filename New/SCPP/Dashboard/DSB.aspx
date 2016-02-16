<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="DSB.aspx.cs" Inherits="LAMOSA.SCPP.Client.View.Administrador.Dashboard.DSB" %>

<%@ Register Assembly="Infragistics35.WebUI.UltraWebChart.v9.1, Version=9.1.20091.1015, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb"
    Namespace="Infragistics.WebUI.UltraWebChart" TagPrefix="igchart" %>

<%@ Register Assembly="Infragistics35.WebUI.Misc.v9.1, Version=9.1.20091.1015, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb"
    Namespace="Infragistics.WebUI.Misc" TagPrefix="igmisc" %>

<%@ Register assembly="Infragistics35.WebUI.UltraWebChart.v9.1, Version=9.1.20091.1015, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" namespace="Infragistics.UltraChart.Resources.Appearance" tagprefix="igchartprop" %>
<%@ Register assembly="Infragistics35.WebUI.UltraWebChart.v9.1, Version=9.1.20091.1015, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" namespace="Infragistics.UltraChart.Data" tagprefix="igchartdata" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >

<head runat="server">
    <link href="~/Estiloscss/pro-line-down-fly/menu3.css" media="screen" rel="stylesheet" type="text/css" />
    <link href="~/Estiloscss/Estilos.css" rel="stylesheet" type="text/css" />
    <link href="~/Estiloscss/menu.css" rel="Stylesheet" type="text/css" />
    <title>Dashboard</title>
    <script language="javascript" type="text/javascript">
        function Cerrar() {
            window.close();
        }
        
    </script>
</head>
<body class="fondoLogin">
    <form id="form1" runat="server">
    <div>
        <table align="center" width="1024px"  border="0" cellpadding="0" cellspacing="0" style="background-color:white;">
            <tr>
                <td  rowspan = "2" class= "Header1DSB">&nbsp;     
                </td>
                <td  class= "Header2DSB">
                   <label class="Header2Text" id="lblfecha" runat="server"></label> | <label class="Header2Text" id="lblHora" runat="server"></label>&nbsp;
                </td>
            </tr>
            <tr>
                <td class= "Header3DSB">
                     <table width= "100%" border= "0" cellpadding= "0" cellspacing= "0">
                        <tr>
                            <td>
                                Almac&eacute;n:
                            </td>
                            <td>
                                <label id="lblAlmacen"  class="HeaderText" runat="server"></label>
                            </td>
                            <td>
                                Planta:
                            </td>
                            <td>
                                <label id="lblPlanta" class="HeaderText" runat="server"></label>
                            </td> 
                            <td>
                                <a href= "#" onclick="Cerrar()"  >Cerrar</a>  
                            </td>
                        </tr>
                    </table> 
                </td>
            </tr>
        </table>
        <table align= "center" width="1024px"  border="0" cellpadding="0" cellspacing="0" style="background-color:white;">
        <tr>
            <td valign="top" style="width:500px;" align="left">
                <igmisc:WebPanel ID="WebPanel1" runat="server" EnableAppStyling="True" Width="500px" 
                    StyleSetName="">
                    <Header Text ="% Perdida en verde en revisado" >
                    </Header>
                    <Template>
                        <igchart:UltraChart ID="UltraChart1" runat="server" BackgroundImageFileName="" 
                            BorderColor="Black" BorderWidth="0px" ChartType="Composite" 
                            EmptyChartText="Data Not Available. Please call UltraChart.Data.DataBind() after setting valid Data.DataSource" 
                            Version="9.1" onchartdataclicked="UltraChart1_ChartDataClicked" Width="500px" 
                                    Height="206px">
                                    <Legend BackgroundColor="White" BorderColor="Black" 
                                        DataAssociation="ColumnData" Font="Arial, 8.25pt" Visible="True"></Legend>
                            <CompositeChart>
                                <Series>
                                    <igchartprop:NumericSeries Key="series1" Label="Prueba 1">
                                        <points>
                                            <igchartprop:NumericDataPoint Label="Enero" Value="26">
                                                <pe elementtype="None" />
                                            </igchartprop:NumericDataPoint>
                                            <igchartprop:NumericDataPoint Label="Febrero" Value="32">
                                                <pe elementtype="None" />
                                            </igchartprop:NumericDataPoint>
                                            <igchartprop:NumericDataPoint Label="Marzo" Value="28">
                                                <pe elementtype="None" />
                                            </igchartprop:NumericDataPoint>
                                            <igchartprop:NumericDataPoint Label="1">
                                                <pe elementtype="None" />
                                            </igchartprop:NumericDataPoint>
                                            <igchartprop:NumericDataPoint Label="2">
                                                <pe elementtype="None" />
                                            </igchartprop:NumericDataPoint>
                                            <igchartprop:NumericDataPoint Label="3">
                                                <pe elementtype="None" />
                                            </igchartprop:NumericDataPoint>
                                            <igchartprop:NumericDataPoint Label="4">
                                                <pe elementtype="None" />
                                            </igchartprop:NumericDataPoint>
                                            <igchartprop:NumericDataPoint Label="5">
                                                <pe elementtype="None" />
                                            </igchartprop:NumericDataPoint>
                                            <igchartprop:NumericDataPoint Label="6">
                                                <pe elementtype="None" />
                                            </igchartprop:NumericDataPoint>
                                            <igchartprop:NumericDataPoint Label="7">
                                                <pe elementtype="None" />
                                            </igchartprop:NumericDataPoint>
                                            <igchartprop:NumericDataPoint Label="8">
                                                <pe elementtype="None" />
                                            </igchartprop:NumericDataPoint>
                                            <igchartprop:NumericDataPoint Label="9">
                                                <pe elementtype="None" />
                                            </igchartprop:NumericDataPoint>
                                            <igchartprop:NumericDataPoint Label="10">
                                                <pe elementtype="None" />
                                            </igchartprop:NumericDataPoint>
                                            <igchartprop:NumericDataPoint Label="11">
                                                <pe elementtype="None" />
                                            </igchartprop:NumericDataPoint>
                                            <igchartprop:NumericDataPoint Label="12">
                                                <pe elementtype="None" />
                                            </igchartprop:NumericDataPoint>
                                            <igchartprop:NumericDataPoint Label="13">
                                                <pe elementtype="None" />
                                            </igchartprop:NumericDataPoint>
                                            <igchartprop:NumericDataPoint Label="14">
                                                <pe elementtype="None" />
                                            </igchartprop:NumericDataPoint>
                                            <igchartprop:NumericDataPoint Label="15">
                                                <pe elementtype="None" />
                                            </igchartprop:NumericDataPoint>
                                            <igchartprop:NumericDataPoint Label="16">
                                                <pe elementtype="None" />
                                            </igchartprop:NumericDataPoint>
                                            <igchartprop:NumericDataPoint Label="17">
                                                <pe elementtype="None" />
                                            </igchartprop:NumericDataPoint>
                                            <igchartprop:NumericDataPoint Label="18">
                                                <pe elementtype="None" />
                                            </igchartprop:NumericDataPoint>
                                            <igchartprop:NumericDataPoint Label="19">
                                                <pe elementtype="None" />
                                            </igchartprop:NumericDataPoint>
                                            <igchartprop:NumericDataPoint Label="20">
                                                <pe elementtype="None" />
                                            </igchartprop:NumericDataPoint>
                                            <igchartprop:NumericDataPoint Label="21">
                                                <pe elementtype="None" />
                                            </igchartprop:NumericDataPoint>
                                            <igchartprop:NumericDataPoint Label="22">
                                                <pe elementtype="None" />
                                            </igchartprop:NumericDataPoint>
                                            <igchartprop:NumericDataPoint Label="23">
                                                <pe elementtype="None" />
                                            </igchartprop:NumericDataPoint>
                                            <igchartprop:NumericDataPoint Label="24">
                                                <pe elementtype="None" />
                                            </igchartprop:NumericDataPoint>
                                            <igchartprop:NumericDataPoint Label="25">
                                                <pe elementtype="None" />
                                            </igchartprop:NumericDataPoint>
                                            <igchartprop:NumericDataPoint Label="26">
                                                <pe elementtype="None" />
                                            </igchartprop:NumericDataPoint>
                                            <igchartprop:NumericDataPoint Label="27">
                                                <pe elementtype="None" />
                                            </igchartprop:NumericDataPoint>
                                            <igchartprop:NumericDataPoint Label="28">
                                                <pe elementtype="None" />
                                            </igchartprop:NumericDataPoint>
                                            <igchartprop:NumericDataPoint Label="29">
                                                <pe elementtype="None" />
                                            </igchartprop:NumericDataPoint>
                                            <igchartprop:NumericDataPoint Label="30">
                                                <pe elementtype="None" />
                                            </igchartprop:NumericDataPoint>
                                            <igchartprop:NumericDataPoint Label="31">
                                                <pe elementtype="None" />
                                            </igchartprop:NumericDataPoint>
                                        </points>
                                    </igchartprop:NumericSeries>
                                    <igchartprop:NumericSeries Key="series3" Label="Prueba2">
                                        <points>
                                            <igchartprop:NumericDataPoint Label="Enero">
                                                <pe elementtype="None" />
                                            </igchartprop:NumericDataPoint>
                                            <igchartprop:NumericDataPoint Label="Febrero">
                                                <pe elementtype="None" />
                                            </igchartprop:NumericDataPoint>
                                            <igchartprop:NumericDataPoint Label="Marzo">
                                                <pe elementtype="None" />
                                            </igchartprop:NumericDataPoint>
                                            <igchartprop:NumericDataPoint Label="1" Value="25">
                                                <pe elementtype="None" />
                                            </igchartprop:NumericDataPoint>
                                            <igchartprop:NumericDataPoint Label="2" Value="32">
                                                <pe elementtype="None" />
                                            </igchartprop:NumericDataPoint>
                                            <igchartprop:NumericDataPoint Label="3" Value="20">
                                                <pe elementtype="None" />
                                            </igchartprop:NumericDataPoint>
                                            <igchartprop:NumericDataPoint Label="4" Value="12">
                                                <pe elementtype="None" />
                                            </igchartprop:NumericDataPoint>
                                            <igchartprop:NumericDataPoint Label="5" Value="24">
                                                <pe elementtype="None" />
                                            </igchartprop:NumericDataPoint>
                                            <igchartprop:NumericDataPoint Label="6" Value="20">
                                                <pe elementtype="None" />
                                            </igchartprop:NumericDataPoint>
                                            <igchartprop:NumericDataPoint Label="7" Value="21">
                                                <pe elementtype="None" />
                                            </igchartprop:NumericDataPoint>
                                            <igchartprop:NumericDataPoint Label="8" Value="25">
                                                <pe elementtype="None" />
                                            </igchartprop:NumericDataPoint>
                                            <igchartprop:NumericDataPoint Label="9" Value="30">
                                                <pe elementtype="None" />
                                            </igchartprop:NumericDataPoint>
                                            <igchartprop:NumericDataPoint Label="10" Value="29">
                                                <pe elementtype="None" />
                                            </igchartprop:NumericDataPoint>
                                            <igchartprop:NumericDataPoint Label="11" Value="24">
                                                <pe elementtype="None" />
                                            </igchartprop:NumericDataPoint>
                                            <igchartprop:NumericDataPoint Label="12" Value="18">
                                                <pe elementtype="None" />
                                            </igchartprop:NumericDataPoint>
                                            <igchartprop:NumericDataPoint Label="13" Value="16">
                                                <pe elementtype="None" />
                                            </igchartprop:NumericDataPoint>
                                            <igchartprop:NumericDataPoint Label="14" Value="27">
                                                <pe elementtype="None" />
                                            </igchartprop:NumericDataPoint>
                                            <igchartprop:NumericDataPoint Label="15" Value="23">
                                                <pe elementtype="None" />
                                            </igchartprop:NumericDataPoint>
                                            <igchartprop:NumericDataPoint Label="16" Value="28">
                                                <pe elementtype="None" />
                                            </igchartprop:NumericDataPoint>
                                            <igchartprop:NumericDataPoint Label="17" Value="22">
                                                <pe elementtype="None" />
                                            </igchartprop:NumericDataPoint>
                                            <igchartprop:NumericDataPoint Label="18" Value="31">
                                                <pe elementtype="None" />
                                            </igchartprop:NumericDataPoint>
                                            <igchartprop:NumericDataPoint Label="19" Value="23">
                                                <pe elementtype="None" />
                                            </igchartprop:NumericDataPoint>
                                            <igchartprop:NumericDataPoint Label="20" Value="24">
                                                <pe elementtype="None" />
                                            </igchartprop:NumericDataPoint>
                                            <igchartprop:NumericDataPoint Label="21" Value="25">
                                                <pe elementtype="None" />
                                            </igchartprop:NumericDataPoint>
                                            <igchartprop:NumericDataPoint Label="22" Value="30">
                                                <pe elementtype="None" />
                                            </igchartprop:NumericDataPoint>
                                            <igchartprop:NumericDataPoint Label="23" Value="14">
                                                <pe elementtype="None" />
                                            </igchartprop:NumericDataPoint>
                                            <igchartprop:NumericDataPoint Label="24" Value="36">
                                                <pe elementtype="None" />
                                            </igchartprop:NumericDataPoint>
                                            <igchartprop:NumericDataPoint Label="25" Value="30">
                                                <pe elementtype="None" />
                                            </igchartprop:NumericDataPoint>
                                            <igchartprop:NumericDataPoint Label="26" Value="24">
                                                <pe elementtype="None" />
                                            </igchartprop:NumericDataPoint>
                                            <igchartprop:NumericDataPoint Label="27" Value="26">
                                                <pe elementtype="None" />
                                            </igchartprop:NumericDataPoint>
                                            <igchartprop:NumericDataPoint Label="28" Value="20">
                                                <pe elementtype="None" />
                                            </igchartprop:NumericDataPoint>
                                            <igchartprop:NumericDataPoint Label="29" Value="30">
                                                <pe elementtype="None" />
                                            </igchartprop:NumericDataPoint>
                                            <igchartprop:NumericDataPoint Label="30" Value="21">
                                                <pe elementtype="None" />
                                            </igchartprop:NumericDataPoint>
                                            <igchartprop:NumericDataPoint Label="31" Value="22">
                                                <pe elementtype="None" />
                                            </igchartprop:NumericDataPoint>
                                        </points>
                                        <pes>
                                            <igchartprop:PaintElement Fill="Red" />
                                        </pes>
                                    </igchartprop:NumericSeries>
                                    <igchartprop:NumericSeries Key="series4" Label="Prueba3">
                                        <points>
                                            <igchartprop:NumericDataPoint Label="Enero">
                                                <pe elementtype="None" />
                                            </igchartprop:NumericDataPoint>
                                            <igchartprop:NumericDataPoint Label="Febrero">
                                                <pe elementtype="None" />
                                            </igchartprop:NumericDataPoint>
                                            <igchartprop:NumericDataPoint Label="Marzo">
                                                <pe elementtype="None" />
                                            </igchartprop:NumericDataPoint>
                                            <igchartprop:NumericDataPoint Label="1" Value="20">
                                                <pe elementtype="None" />
                                            </igchartprop:NumericDataPoint>
                                            <igchartprop:NumericDataPoint Label="2" Value="20">
                                                <pe elementtype="None" />
                                            </igchartprop:NumericDataPoint>
                                            <igchartprop:NumericDataPoint Label="3" Value="20">
                                                <pe elementtype="None" />
                                            </igchartprop:NumericDataPoint>
                                            <igchartprop:NumericDataPoint Label="4" Value="20">
                                                <pe elementtype="None" />
                                            </igchartprop:NumericDataPoint>
                                            <igchartprop:NumericDataPoint Label="5" Value="20">
                                                <pe elementtype="None" />
                                            </igchartprop:NumericDataPoint>
                                            <igchartprop:NumericDataPoint Label="6" Value="20">
                                                <pe elementtype="None" />
                                            </igchartprop:NumericDataPoint>
                                            <igchartprop:NumericDataPoint Label="7" Value="20">
                                                <pe elementtype="None" />
                                            </igchartprop:NumericDataPoint>
                                            <igchartprop:NumericDataPoint Label="8" Value="20">
                                                <pe elementtype="None" />
                                            </igchartprop:NumericDataPoint>
                                            <igchartprop:NumericDataPoint Label="9" Value="20">
                                                <pe elementtype="None" />
                                            </igchartprop:NumericDataPoint>
                                            <igchartprop:NumericDataPoint Label="10" Value="20">
                                                <pe elementtype="None" />
                                            </igchartprop:NumericDataPoint>
                                            <igchartprop:NumericDataPoint Label="11" Value="20">
                                                <pe elementtype="None" />
                                            </igchartprop:NumericDataPoint>
                                            <igchartprop:NumericDataPoint Label="12" Value="20">
                                                <pe elementtype="None" />
                                            </igchartprop:NumericDataPoint>
                                            <igchartprop:NumericDataPoint Label="13" Value="20">
                                                <pe elementtype="None" />
                                            </igchartprop:NumericDataPoint>
                                            <igchartprop:NumericDataPoint Label="14" Value="20">
                                                <pe elementtype="None" />
                                            </igchartprop:NumericDataPoint>
                                            <igchartprop:NumericDataPoint Label="15" Value="20">
                                                <pe elementtype="None" />
                                            </igchartprop:NumericDataPoint>
                                            <igchartprop:NumericDataPoint Label="16" Value="20">
                                                <pe elementtype="None" />
                                            </igchartprop:NumericDataPoint>
                                            <igchartprop:NumericDataPoint Label="17" Value="20">
                                                <pe elementtype="None" />
                                            </igchartprop:NumericDataPoint>
                                            <igchartprop:NumericDataPoint Label="18" Value="20">
                                                <pe elementtype="None" />
                                            </igchartprop:NumericDataPoint>
                                            <igchartprop:NumericDataPoint Label="19" Value="20">
                                                <pe elementtype="None" />
                                            </igchartprop:NumericDataPoint>
                                            <igchartprop:NumericDataPoint Label="20" Value="20">
                                                <pe elementtype="None" />
                                            </igchartprop:NumericDataPoint>
                                            <igchartprop:NumericDataPoint Label="21" Value="20">
                                                <pe elementtype="None" />
                                            </igchartprop:NumericDataPoint>
                                            <igchartprop:NumericDataPoint Label="22" Value="20">
                                                <pe elementtype="None" />
                                            </igchartprop:NumericDataPoint>
                                            <igchartprop:NumericDataPoint Label="23" Value="20">
                                                <pe elementtype="None" />
                                            </igchartprop:NumericDataPoint>
                                            <igchartprop:NumericDataPoint Label="24" Value="20">
                                                <pe elementtype="None" />
                                            </igchartprop:NumericDataPoint>
                                            <igchartprop:NumericDataPoint Label="25" Value="20">
                                                <pe elementtype="None" />
                                            </igchartprop:NumericDataPoint>
                                            <igchartprop:NumericDataPoint Label="26" Value="20">
                                                <pe elementtype="None" />
                                            </igchartprop:NumericDataPoint>
                                            <igchartprop:NumericDataPoint Label="27" Value="20">
                                                <pe elementtype="None" />
                                            </igchartprop:NumericDataPoint>
                                            <igchartprop:NumericDataPoint Label="28" Value="20">
                                                <pe elementtype="None" />
                                            </igchartprop:NumericDataPoint>
                                            <igchartprop:NumericDataPoint Label="29" Value="20">
                                                <pe elementtype="None" />
                                            </igchartprop:NumericDataPoint>
                                            <igchartprop:NumericDataPoint Label="30" Value="20">
                                                <pe elementtype="None" />
                                            </igchartprop:NumericDataPoint>
                                            <igchartprop:NumericDataPoint Label="31" Value="20">
                                                <pe elementtype="None" />
                                            </igchartprop:NumericDataPoint>
                                        </points>
                                        <pes>
                                            <igchartprop:PaintElement Fill="Blue" />
                                        </pes>
                                    </igchartprop:NumericSeries>
                                </Series>
                                <ChartLayers>
                                    <igchartprop:ChartLayerAppearance AxisXKey="axis1" AxisYKey="axis2" 
                                        ChartAreaKey="area1" ChartType="ColumnChart" Key="chartLayer1" 
                                        SeriesList="series1">
                                        <ChartTypeAppearances>
                                            <igchartprop:ColumnChartAppearance NullHandling="DontPlot" 
                                                ChartTextValueAlignment="PositiveAndNegative">
                                                <ChartText>
                                                    <igchartprop:ChartTextAppearance ChartTextFont="Arial, 7pt" Column="0" 
                                                        ItemFormatString="&lt;DATA_VALUE:00&gt;" Row="0" />
                                                </ChartText>
                                            </igchartprop:ColumnChartAppearance>
                                        </ChartTypeAppearances>
                                    </igchartprop:ChartLayerAppearance>
                                    <igchartprop:ChartLayerAppearance AxisXKey="axis3" AxisYKey="axis2" 
                                        ChartAreaKey="area1" ChartType="LineChart" Key="chartLayer2" 
                                        SeriesList="series3">
                                        <ChartTypeAppearances>
                                            <igchartprop:LineChartAppearance HighLightLines="True" MidPointAnchors="False" 
                                                NullHandling="DontPlot" Thickness="4">
                                                <ChartText>
                                                    <igchartprop:ChartTextAppearance ChartTextFont="Arial, 7pt" Column="0" 
                                                        ItemFormatString="&lt;DATA_VALUE:00&gt;" Row="0" />
                                                </ChartText>
                                                <LineAppearances>
                                                    <igchartprop:LineAppearance>
                                                        <IconAppearance CharacterFont="Arial, 8.25pt" Icon="Circle">
                                                            <PE ElementType="None" />
                                                        </IconAppearance>
                                                    </igchartprop:LineAppearance>
                                                </LineAppearances>
                                            </igchartprop:LineChartAppearance>
                                        </ChartTypeAppearances>
                                    </igchartprop:ChartLayerAppearance>
                                    <igchartprop:ChartLayerAppearance AxisXKey="axis3" AxisYKey="axis2" 
                                        ChartAreaKey="area1" ChartType="LineChart" Key="chartLayer3" 
                                        SeriesList="series4">
                                        <ChartTypeAppearances>
                                            <igchartprop:LineChartAppearance>
                                                <ChartText>
                                                    <igchartprop:ChartTextAppearance ChartTextFont="Arial, 7pt" Column="0" 
                                                        ItemFormatString="&lt;DATA_VALUE:00&gt;" Row="0" />
                                                </ChartText>
                                                <LineAppearances>
                                                    <igchartprop:LineAppearance>
                                                        <IconAppearance CharacterFont="Arial Narrow, 6.75pt">
                                                            <PE ElementType="None" StrokeWidth="2" />
                                                        </IconAppearance>
                                                    </igchartprop:LineAppearance>
                                                </LineAppearances>
                                            </igchartprop:LineChartAppearance>
                                        </ChartTypeAppearances>
                                    </igchartprop:ChartLayerAppearance>
                                </ChartLayers>
                                <ChartAreas>
                                    <igchartprop:ChartArea Bounds="0, 0, 100, 100" BoundsMeasureType="Percentage" 
                                        Key="area1">
                                        <Axes>
                                            <igchartprop:AxisItem DataType="String" Extent="20" Key="axis1" 
                                                OrientationType="X_Axis" SetLabelAxisType="GroupBySeries" TickmarkInterval="1">
                                                <MajorGridLines AlphaLevel="255" Color="Gainsboro" DrawStyle="Dot" 
                                                    Thickness="1" Visible="True" />
                                                <MinorGridLines AlphaLevel="255" Color="LightGray" DrawStyle="Dot" 
                                                    Thickness="1" Visible="False" />
                                                <Labels HorizontalAlign="Near" ItemFormatString="" Orientation="Horizontal" 
                                                    VerticalAlign="Center" Font="Arial, 6.75pt" Visible="False">
                                                    <SeriesLabels HorizontalAlign="Center" Orientation="Horizontal" 
                                                        VerticalAlign="Center" Visible="False">
                                                    </SeriesLabels>
                                                </Labels>
                                            </igchartprop:AxisItem>
                                            <igchartprop:AxisItem DataType="Numeric" Extent="20" Key="axis2" 
                                                OrientationType="Y_Axis" SetLabelAxisType="GroupBySeries" TickmarkInterval="0">
                                                <MajorGridLines AlphaLevel="255" Color="Gainsboro" DrawStyle="Dot" 
                                                    Thickness="1" Visible="True" />
                                                <MinorGridLines AlphaLevel="255" Color="LightGray" DrawStyle="Dot" 
                                                    Thickness="1" Visible="False" />
                                                <Labels HorizontalAlign="Near" ItemFormatString="&lt;DATA_VALUE:00&gt;" Orientation="Horizontal" 
                                                    VerticalAlign="Center" Flip="True" Font="Arial, 8.25pt">
                                                    <SeriesLabels HorizontalAlign="Center" Orientation="Horizontal" 
                                                        VerticalAlign="Center">
                                                    </SeriesLabels>
                                                </Labels>
                                            </igchartprop:AxisItem>
                                            <igchartprop:AxisItem DataType="String" Extent="60" Key="axis3" 
                                                OrientationType="X_Axis" SetLabelAxisType="ContinuousData" TickmarkInterval="0">
                                                <MajorGridLines AlphaLevel="255" Color="Gainsboro" DrawStyle="Dot" 
                                                    Thickness="1" Visible="True" />
                                                <MinorGridLines AlphaLevel="255" Color="LightGray" DrawStyle="Dot" 
                                                    Thickness="1" Visible="False" />
                                                <Labels HorizontalAlign="Near" ItemFormatString="&lt;ITEM_LABEL&gt;" Orientation="VerticalLeftFacing" 
                                                    VerticalAlign="Center" Flip="True" Font="Arial, 6.75pt">
                                                    <SeriesLabels HorizontalAlign="Center" Orientation="Horizontal" 
                                                        VerticalAlign="Center">
                                                    </SeriesLabels>
                                                </Labels>
                                            </igchartprop:AxisItem>
                                        </Axes>
                                        <PE Texture="Blocks" />
                                        <GridPE ElementType="None" />
                                    </igchartprop:ChartArea>
                                </ChartAreas>
                                <Legends>
                                    <igchartprop:CompositeLegend BoundsMeasureType="Percentage">
                                    </igchartprop:CompositeLegend>
                                </Legends>
                            </CompositeChart>
                            <Axis>
                                <PE ElementType="None" Fill="Cornsilk" />
                                <X LineThickness="1" TickmarkInterval="0" TickmarkStyle="Smart" Visible="True">
                                    <MajorGridLines AlphaLevel="255" Color="Gainsboro" DrawStyle="Dot" 
                                        Thickness="1" Visible="True" />
                                    <MinorGridLines AlphaLevel="255" Color="LightGray" DrawStyle="Dot" 
                                        Thickness="1" Visible="False" />
                                    <Labels Font="Verdana, 7pt" FontColor="DimGray" HorizontalAlign="Near" 
                                        ItemFormatString="&lt;ITEM_LABEL&gt;" Orientation="VerticalLeftFacing" 
                                        VerticalAlign="Center">
                                        <SeriesLabels Font="Verdana, 7pt" FontColor="DimGray" FormatString="" 
                                            HorizontalAlign="Near" Orientation="VerticalLeftFacing" VerticalAlign="Center">
                                            <Layout Behavior="Auto">
                                            </Layout>
                                        </SeriesLabels>
                                        <Layout Behavior="Auto">
                                        </Layout>
                                    </Labels>
                                </X>
                                <Y LineThickness="1" TickmarkInterval="0" TickmarkStyle="Smart" Visible="True">
                                    <MajorGridLines AlphaLevel="255" Color="Gainsboro" DrawStyle="Dot" 
                                        Thickness="1" Visible="True" />
                                    <MinorGridLines AlphaLevel="255" Color="LightGray" DrawStyle="Dot" 
                                        Thickness="1" Visible="False" />
                                    <Labels Font="Verdana, 7pt" FontColor="DimGray" HorizontalAlign="Far" 
                                        ItemFormatString="&lt;DATA_VALUE:00.##&gt;" Orientation="Horizontal" 
                                        VerticalAlign="Center">
                                        <SeriesLabels Font="Verdana, 7pt" FontColor="DimGray" FormatString="" 
                                            HorizontalAlign="Far" Orientation="Horizontal" VerticalAlign="Center">
                                            <Layout Behavior="Auto">
                                            </Layout>
                                        </SeriesLabels>
                                        <Layout Behavior="Auto">
                                        </Layout>
                                    </Labels>
                                </Y>
                                <Y2 LineThickness="1" TickmarkInterval="0" TickmarkStyle="Smart" 
                                    Visible="False">
                                    <MajorGridLines AlphaLevel="255" Color="Gainsboro" DrawStyle="Dot" 
                                        Thickness="1" Visible="True" />
                                    <MinorGridLines AlphaLevel="255" Color="LightGray" DrawStyle="Dot" 
                                        Thickness="1" Visible="False" />
                                    <Labels Font="Verdana, 7pt" FontColor="Gray" HorizontalAlign="Near" 
                                        ItemFormatString="&lt;DATA_VALUE:00.##&gt;" Orientation="Horizontal" 
                                        VerticalAlign="Center" Visible="False">
                                        <SeriesLabels Font="Verdana, 7pt" FontColor="Gray" FormatString="" 
                                            HorizontalAlign="Near" Orientation="Horizontal" VerticalAlign="Center">
                                            <Layout Behavior="Auto">
                                            </Layout>
                                        </SeriesLabels>
                                        <Layout Behavior="Auto">
                                        </Layout>
                                    </Labels>
                                </Y2>
                                <X2 LineThickness="1" TickmarkInterval="0" TickmarkStyle="Smart" 
                                    Visible="False">
                                    <MajorGridLines AlphaLevel="255" Color="Gainsboro" DrawStyle="Dot" 
                                        Thickness="1" Visible="True" />
                                    <MinorGridLines AlphaLevel="255" Color="LightGray" DrawStyle="Dot" 
                                        Thickness="1" Visible="False" />
                                    <Labels Font="Verdana, 7pt" FontColor="Gray" HorizontalAlign="Far" 
                                        ItemFormatString="&lt;ITEM_LABEL&gt;" Orientation="VerticalLeftFacing" 
                                        VerticalAlign="Center" Visible="False">
                                        <SeriesLabels Font="Verdana, 7pt" FontColor="Gray" FormatString="" 
                                            HorizontalAlign="Far" Orientation="VerticalLeftFacing" VerticalAlign="Center">
                                            <Layout Behavior="Auto">
                                            </Layout>
                                        </SeriesLabels>
                                        <Layout Behavior="Auto">
                                        </Layout>
                                    </Labels>
                                </X2>
                                <Z LineThickness="1" TickmarkInterval="0" TickmarkStyle="Smart" Visible="False">
                                    <MajorGridLines AlphaLevel="255" Color="Gainsboro" DrawStyle="Dot" 
                                        Thickness="1" Visible="True" />
                                    <MinorGridLines AlphaLevel="255" Color="LightGray" DrawStyle="Dot" 
                                        Thickness="1" Visible="False" />
                                    <Labels Font="Verdana, 7pt" FontColor="DimGray" HorizontalAlign="Near" 
                                        ItemFormatString="" Orientation="Horizontal" VerticalAlign="Center">
                                        <SeriesLabels Font="Verdana, 7pt" FontColor="DimGray" HorizontalAlign="Near" 
                                            Orientation="Horizontal" VerticalAlign="Center">
                                            <Layout Behavior="Auto">
                                            </Layout>
                                        </SeriesLabels>
                                        <Layout Behavior="Auto">
                                        </Layout>
                                    </Labels>
                                </Z>
                                <Z2 LineThickness="1" TickmarkInterval="0" TickmarkStyle="Smart" 
                                    Visible="False">
                                    <MajorGridLines AlphaLevel="255" Color="Gainsboro" DrawStyle="Dot" 
                                        Thickness="1" Visible="True" />
                                    <MinorGridLines AlphaLevel="255" Color="LightGray" DrawStyle="Dot" 
                                        Thickness="1" Visible="False" />
                                    <Labels Font="Verdana, 7pt" FontColor="Gray" HorizontalAlign="Near" 
                                        ItemFormatString="" Orientation="Horizontal" VerticalAlign="Center" 
                                        Visible="False">
                                        <SeriesLabels Font="Verdana, 7pt" FontColor="Gray" HorizontalAlign="Near" 
                                            Orientation="Horizontal" VerticalAlign="Center">
                                            <Layout Behavior="Auto">
                                            </Layout>
                                        </SeriesLabels>
                                        <Layout Behavior="Auto">
                                        </Layout>
                                    </Labels>
                                </Z2>
                            </Axis>
                            <Effects>
                                <Effects>
                                    <igchartprop:GradientEffect />
                                </Effects>
                            </Effects>
                                    <ColorModel AlphaLevel="150" ColorBegin="Pink" ColorEnd="DarkRed" 
                                        ModelStyle="CustomLinear">
                                    </ColorModel>
                                    <TitleBottom Extent="33" Font="Arial, 8.25pt">
                                    </TitleBottom>
                            <Border Thickness="0" />
                            <Tooltips Font-Bold="False" Font-Italic="False" Font-Overline="False" 
                                Font-Strikeout="False" Font-Underline="False" />
                        </igchart:UltraChart>
                    </Template>
                </igmisc:WebPanel>
            </td>
            <td style="width:24px;" valign="top"></td>
            <td valign="top" style="width:500px;" align="right">
                <igmisc:WebPanel ID="WebPanel2" runat="server" EnableAppStyling="True" Width="500px" 
                    StyleSetName="">
                    <Header Text ="Piezas esmaltadas" >
                    </Header>
                    <Template>
                        <igchart:UltraChart ID="UltraChart2" runat="server" BackgroundImageFileName="" 
                            BorderColor="Black" BorderWidth="0px" ChartType="Composite" 
                            EmptyChartText="Data Not Available. Please call UltraChart.Data.DataBind() after setting valid Data.DataSource" 
                            Version="9.1" onchartdataclicked="UltraChart1_ChartDataClicked" Width="500px" 
                                    Height="206px">
                                    <Legend BackgroundColor="White" BorderColor="Black" 
                                        DataAssociation="ColumnData" Font="Arial, 8.25pt" Visible="True"></Legend>
                            <CompositeChart>
                                <Series>
                                    <igchartprop:NumericSeries Key="series1" Label="Prueba 1">
                                        <points>
                                            <igchartprop:NumericDataPoint Label="Enero" Value="26">
                                                <pe elementtype="None" />
                                            </igchartprop:NumericDataPoint>
                                            <igchartprop:NumericDataPoint Label="Febrero" Value="32">
                                                <pe elementtype="None" />
                                            </igchartprop:NumericDataPoint>
                                            <igchartprop:NumericDataPoint Label="Marzo" Value="28">
                                                <pe elementtype="None" />
                                            </igchartprop:NumericDataPoint>
                                            <igchartprop:NumericDataPoint Label="1">
                                                <pe elementtype="None" />
                                            </igchartprop:NumericDataPoint>
                                            <igchartprop:NumericDataPoint Label="2">
                                                <pe elementtype="None" />
                                            </igchartprop:NumericDataPoint>
                                            <igchartprop:NumericDataPoint Label="3">
                                                <pe elementtype="None" />
                                            </igchartprop:NumericDataPoint>
                                            <igchartprop:NumericDataPoint Label="4">
                                                <pe elementtype="None" />
                                            </igchartprop:NumericDataPoint>
                                            <igchartprop:NumericDataPoint Label="5">
                                                <pe elementtype="None" />
                                            </igchartprop:NumericDataPoint>
                                            <igchartprop:NumericDataPoint Label="6">
                                                <pe elementtype="None" />
                                            </igchartprop:NumericDataPoint>
                                            <igchartprop:NumericDataPoint Label="7">
                                                <pe elementtype="None" />
                                            </igchartprop:NumericDataPoint>
                                            <igchartprop:NumericDataPoint Label="8">
                                                <pe elementtype="None" />
                                            </igchartprop:NumericDataPoint>
                                            <igchartprop:NumericDataPoint Label="9">
                                                <pe elementtype="None" />
                                            </igchartprop:NumericDataPoint>
                                            <igchartprop:NumericDataPoint Label="10">
                                                <pe elementtype="None" />
                                            </igchartprop:NumericDataPoint>
                                            <igchartprop:NumericDataPoint Label="11">
                                                <pe elementtype="None" />
                                            </igchartprop:NumericDataPoint>
                                            <igchartprop:NumericDataPoint Label="12">
                                                <pe elementtype="None" />
                                            </igchartprop:NumericDataPoint>
                                            <igchartprop:NumericDataPoint Label="13">
                                                <pe elementtype="None" />
                                            </igchartprop:NumericDataPoint>
                                            <igchartprop:NumericDataPoint Label="14">
                                                <pe elementtype="None" />
                                            </igchartprop:NumericDataPoint>
                                            <igchartprop:NumericDataPoint Label="15">
                                                <pe elementtype="None" />
                                            </igchartprop:NumericDataPoint>
                                            <igchartprop:NumericDataPoint Label="16">
                                                <pe elementtype="None" />
                                            </igchartprop:NumericDataPoint>
                                            <igchartprop:NumericDataPoint Label="17">
                                                <pe elementtype="None" />
                                            </igchartprop:NumericDataPoint>
                                            <igchartprop:NumericDataPoint Label="18">
                                                <pe elementtype="None" />
                                            </igchartprop:NumericDataPoint>
                                            <igchartprop:NumericDataPoint Label="19">
                                                <pe elementtype="None" />
                                            </igchartprop:NumericDataPoint>
                                            <igchartprop:NumericDataPoint Label="20">
                                                <pe elementtype="None" />
                                            </igchartprop:NumericDataPoint>
                                            <igchartprop:NumericDataPoint Label="21">
                                                <pe elementtype="None" />
                                            </igchartprop:NumericDataPoint>
                                            <igchartprop:NumericDataPoint Label="22">
                                                <pe elementtype="None" />
                                            </igchartprop:NumericDataPoint>
                                            <igchartprop:NumericDataPoint Label="23">
                                                <pe elementtype="None" />
                                            </igchartprop:NumericDataPoint>
                                            <igchartprop:NumericDataPoint Label="24">
                                                <pe elementtype="None" />
                                            </igchartprop:NumericDataPoint>
                                            <igchartprop:NumericDataPoint Label="25">
                                                <pe elementtype="None" />
                                            </igchartprop:NumericDataPoint>
                                            <igchartprop:NumericDataPoint Label="26">
                                                <pe elementtype="None" />
                                            </igchartprop:NumericDataPoint>
                                            <igchartprop:NumericDataPoint Label="27">
                                                <pe elementtype="None" />
                                            </igchartprop:NumericDataPoint>
                                            <igchartprop:NumericDataPoint Label="28">
                                                <pe elementtype="None" />
                                            </igchartprop:NumericDataPoint>
                                            <igchartprop:NumericDataPoint Label="29">
                                                <pe elementtype="None" />
                                            </igchartprop:NumericDataPoint>
                                            <igchartprop:NumericDataPoint Label="30">
                                                <pe elementtype="None" />
                                            </igchartprop:NumericDataPoint>
                                            <igchartprop:NumericDataPoint Label="31">
                                                <pe elementtype="None" />
                                            </igchartprop:NumericDataPoint>
                                        </points>
                                    </igchartprop:NumericSeries>
                                    <igchartprop:NumericSeries Key="series3" Label="Prueba2">
                                        <points>
                                            <igchartprop:NumericDataPoint Label="Enero">
                                                <pe elementtype="None" />
                                            </igchartprop:NumericDataPoint>
                                            <igchartprop:NumericDataPoint Label="Febrero">
                                                <pe elementtype="None" />
                                            </igchartprop:NumericDataPoint>
                                            <igchartprop:NumericDataPoint Label="Marzo">
                                                <pe elementtype="None" />
                                            </igchartprop:NumericDataPoint>
                                            <igchartprop:NumericDataPoint Label="1" Value="25">
                                                <pe elementtype="None" />
                                            </igchartprop:NumericDataPoint>
                                            <igchartprop:NumericDataPoint Label="2" Value="32">
                                                <pe elementtype="None" />
                                            </igchartprop:NumericDataPoint>
                                            <igchartprop:NumericDataPoint Label="3" Value="20">
                                                <pe elementtype="None" />
                                            </igchartprop:NumericDataPoint>
                                            <igchartprop:NumericDataPoint Label="4" Value="12">
                                                <pe elementtype="None" />
                                            </igchartprop:NumericDataPoint>
                                            <igchartprop:NumericDataPoint Label="5" Value="24">
                                                <pe elementtype="None" />
                                            </igchartprop:NumericDataPoint>
                                            <igchartprop:NumericDataPoint Label="6" Value="20">
                                                <pe elementtype="None" />
                                            </igchartprop:NumericDataPoint>
                                            <igchartprop:NumericDataPoint Label="7" Value="21">
                                                <pe elementtype="None" />
                                            </igchartprop:NumericDataPoint>
                                            <igchartprop:NumericDataPoint Label="8" Value="25">
                                                <pe elementtype="None" />
                                            </igchartprop:NumericDataPoint>
                                            <igchartprop:NumericDataPoint Label="9" Value="30">
                                                <pe elementtype="None" />
                                            </igchartprop:NumericDataPoint>
                                            <igchartprop:NumericDataPoint Label="10" Value="29">
                                                <pe elementtype="None" />
                                            </igchartprop:NumericDataPoint>
                                            <igchartprop:NumericDataPoint Label="11" Value="24">
                                                <pe elementtype="None" />
                                            </igchartprop:NumericDataPoint>
                                            <igchartprop:NumericDataPoint Label="12" Value="18">
                                                <pe elementtype="None" />
                                            </igchartprop:NumericDataPoint>
                                            <igchartprop:NumericDataPoint Label="13" Value="16">
                                                <pe elementtype="None" />
                                            </igchartprop:NumericDataPoint>
                                            <igchartprop:NumericDataPoint Label="14" Value="27">
                                                <pe elementtype="None" />
                                            </igchartprop:NumericDataPoint>
                                            <igchartprop:NumericDataPoint Label="15" Value="23">
                                                <pe elementtype="None" />
                                            </igchartprop:NumericDataPoint>
                                            <igchartprop:NumericDataPoint Label="16" Value="28">
                                                <pe elementtype="None" />
                                            </igchartprop:NumericDataPoint>
                                            <igchartprop:NumericDataPoint Label="17" Value="22">
                                                <pe elementtype="None" />
                                            </igchartprop:NumericDataPoint>
                                            <igchartprop:NumericDataPoint Label="18" Value="31">
                                                <pe elementtype="None" />
                                            </igchartprop:NumericDataPoint>
                                            <igchartprop:NumericDataPoint Label="19" Value="23">
                                                <pe elementtype="None" />
                                            </igchartprop:NumericDataPoint>
                                            <igchartprop:NumericDataPoint Label="20" Value="24">
                                                <pe elementtype="None" />
                                            </igchartprop:NumericDataPoint>
                                            <igchartprop:NumericDataPoint Label="21" Value="25">
                                                <pe elementtype="None" />
                                            </igchartprop:NumericDataPoint>
                                            <igchartprop:NumericDataPoint Label="22" Value="30">
                                                <pe elementtype="None" />
                                            </igchartprop:NumericDataPoint>
                                            <igchartprop:NumericDataPoint Label="23" Value="14">
                                                <pe elementtype="None" />
                                            </igchartprop:NumericDataPoint>
                                            <igchartprop:NumericDataPoint Label="24" Value="36">
                                                <pe elementtype="None" />
                                            </igchartprop:NumericDataPoint>
                                            <igchartprop:NumericDataPoint Label="25" Value="30">
                                                <pe elementtype="None" />
                                            </igchartprop:NumericDataPoint>
                                            <igchartprop:NumericDataPoint Label="26" Value="24">
                                                <pe elementtype="None" />
                                            </igchartprop:NumericDataPoint>
                                            <igchartprop:NumericDataPoint Label="27" Value="26">
                                                <pe elementtype="None" />
                                            </igchartprop:NumericDataPoint>
                                            <igchartprop:NumericDataPoint Label="28" Value="20">
                                                <pe elementtype="None" />
                                            </igchartprop:NumericDataPoint>
                                            <igchartprop:NumericDataPoint Label="29" Value="30">
                                                <pe elementtype="None" />
                                            </igchartprop:NumericDataPoint>
                                            <igchartprop:NumericDataPoint Label="30" Value="21">
                                                <pe elementtype="None" />
                                            </igchartprop:NumericDataPoint>
                                            <igchartprop:NumericDataPoint Label="31" Value="22">
                                                <pe elementtype="None" />
                                            </igchartprop:NumericDataPoint>
                                        </points>
                                        <pes>
                                            <igchartprop:PaintElement Fill="Red" />
                                        </pes>
                                    </igchartprop:NumericSeries>
                                    <igchartprop:NumericSeries Key="series4" Label="Prueba3">
                                        <points>
                                            <igchartprop:NumericDataPoint Label="Enero">
                                                <pe elementtype="None" />
                                            </igchartprop:NumericDataPoint>
                                            <igchartprop:NumericDataPoint Label="Febrero">
                                                <pe elementtype="None" />
                                            </igchartprop:NumericDataPoint>
                                            <igchartprop:NumericDataPoint Label="Marzo">
                                                <pe elementtype="None" />
                                            </igchartprop:NumericDataPoint>
                                            <igchartprop:NumericDataPoint Label="1" Value="20">
                                                <pe elementtype="None" />
                                            </igchartprop:NumericDataPoint>
                                            <igchartprop:NumericDataPoint Label="2" Value="20">
                                                <pe elementtype="None" />
                                            </igchartprop:NumericDataPoint>
                                            <igchartprop:NumericDataPoint Label="3" Value="20">
                                                <pe elementtype="None" />
                                            </igchartprop:NumericDataPoint>
                                            <igchartprop:NumericDataPoint Label="4" Value="20">
                                                <pe elementtype="None" />
                                            </igchartprop:NumericDataPoint>
                                            <igchartprop:NumericDataPoint Label="5" Value="20">
                                                <pe elementtype="None" />
                                            </igchartprop:NumericDataPoint>
                                            <igchartprop:NumericDataPoint Label="6" Value="20">
                                                <pe elementtype="None" />
                                            </igchartprop:NumericDataPoint>
                                            <igchartprop:NumericDataPoint Label="7" Value="20">
                                                <pe elementtype="None" />
                                            </igchartprop:NumericDataPoint>
                                            <igchartprop:NumericDataPoint Label="8" Value="20">
                                                <pe elementtype="None" />
                                            </igchartprop:NumericDataPoint>
                                            <igchartprop:NumericDataPoint Label="9" Value="20">
                                                <pe elementtype="None" />
                                            </igchartprop:NumericDataPoint>
                                            <igchartprop:NumericDataPoint Label="10" Value="20">
                                                <pe elementtype="None" />
                                            </igchartprop:NumericDataPoint>
                                            <igchartprop:NumericDataPoint Label="11" Value="20">
                                                <pe elementtype="None" />
                                            </igchartprop:NumericDataPoint>
                                            <igchartprop:NumericDataPoint Label="12" Value="20">
                                                <pe elementtype="None" />
                                            </igchartprop:NumericDataPoint>
                                            <igchartprop:NumericDataPoint Label="13" Value="20">
                                                <pe elementtype="None" />
                                            </igchartprop:NumericDataPoint>
                                            <igchartprop:NumericDataPoint Label="14" Value="20">
                                                <pe elementtype="None" />
                                            </igchartprop:NumericDataPoint>
                                            <igchartprop:NumericDataPoint Label="15" Value="20">
                                                <pe elementtype="None" />
                                            </igchartprop:NumericDataPoint>
                                            <igchartprop:NumericDataPoint Label="16" Value="20">
                                                <pe elementtype="None" />
                                            </igchartprop:NumericDataPoint>
                                            <igchartprop:NumericDataPoint Label="17" Value="20">
                                                <pe elementtype="None" />
                                            </igchartprop:NumericDataPoint>
                                            <igchartprop:NumericDataPoint Label="18" Value="20">
                                                <pe elementtype="None" />
                                            </igchartprop:NumericDataPoint>
                                            <igchartprop:NumericDataPoint Label="19" Value="20">
                                                <pe elementtype="None" />
                                            </igchartprop:NumericDataPoint>
                                            <igchartprop:NumericDataPoint Label="20" Value="20">
                                                <pe elementtype="None" />
                                            </igchartprop:NumericDataPoint>
                                            <igchartprop:NumericDataPoint Label="21" Value="20">
                                                <pe elementtype="None" />
                                            </igchartprop:NumericDataPoint>
                                            <igchartprop:NumericDataPoint Label="22" Value="20">
                                                <pe elementtype="None" />
                                            </igchartprop:NumericDataPoint>
                                            <igchartprop:NumericDataPoint Label="23" Value="20">
                                                <pe elementtype="None" />
                                            </igchartprop:NumericDataPoint>
                                            <igchartprop:NumericDataPoint Label="24" Value="20">
                                                <pe elementtype="None" />
                                            </igchartprop:NumericDataPoint>
                                            <igchartprop:NumericDataPoint Label="25" Value="20">
                                                <pe elementtype="None" />
                                            </igchartprop:NumericDataPoint>
                                            <igchartprop:NumericDataPoint Label="26" Value="20">
                                                <pe elementtype="None" />
                                            </igchartprop:NumericDataPoint>
                                            <igchartprop:NumericDataPoint Label="27" Value="20">
                                                <pe elementtype="None" />
                                            </igchartprop:NumericDataPoint>
                                            <igchartprop:NumericDataPoint Label="28" Value="20">
                                                <pe elementtype="None" />
                                            </igchartprop:NumericDataPoint>
                                            <igchartprop:NumericDataPoint Label="29" Value="20">
                                                <pe elementtype="None" />
                                            </igchartprop:NumericDataPoint>
                                            <igchartprop:NumericDataPoint Label="30" Value="20">
                                                <pe elementtype="None" />
                                            </igchartprop:NumericDataPoint>
                                            <igchartprop:NumericDataPoint Label="31" Value="20">
                                                <pe elementtype="None" />
                                            </igchartprop:NumericDataPoint>
                                        </points>
                                        <pes>
                                            <igchartprop:PaintElement Fill="Blue" />
                                        </pes>
                                    </igchartprop:NumericSeries>
                                </Series>
                                <ChartLayers>
                                    <igchartprop:ChartLayerAppearance AxisXKey="axis1" AxisYKey="axis2" 
                                        ChartAreaKey="area1" ChartType="ColumnChart" Key="chartLayer1" 
                                        SeriesList="series1">
                                        <ChartTypeAppearances>
                                            <igchartprop:ColumnChartAppearance NullHandling="DontPlot" 
                                                ChartTextValueAlignment="PositiveAndNegative">
                                                <ChartText>
                                                    <igchartprop:ChartTextAppearance ChartTextFont="Arial, 7pt" Column="0" 
                                                        ItemFormatString="&lt;DATA_VALUE:00&gt;" Row="0" />
                                                </ChartText>
                                            </igchartprop:ColumnChartAppearance>
                                        </ChartTypeAppearances>
                                    </igchartprop:ChartLayerAppearance>
                                    <igchartprop:ChartLayerAppearance AxisXKey="axis3" AxisYKey="axis2" 
                                        ChartAreaKey="area1" ChartType="LineChart" Key="chartLayer2" 
                                        SeriesList="series3">
                                        <ChartTypeAppearances>
                                            <igchartprop:LineChartAppearance HighLightLines="True" MidPointAnchors="False" 
                                                NullHandling="DontPlot" Thickness="4">
                                                <ChartText>
                                                    <igchartprop:ChartTextAppearance ChartTextFont="Arial, 7pt" Column="0" 
                                                        ItemFormatString="&lt;DATA_VALUE:00&gt;" Row="0" />
                                                </ChartText>
                                                <LineAppearances>
                                                    <igchartprop:LineAppearance>
                                                        <IconAppearance CharacterFont="Arial, 8.25pt" Icon="Circle">
                                                            <PE ElementType="None" />
                                                        </IconAppearance>
                                                    </igchartprop:LineAppearance>
                                                </LineAppearances>
                                            </igchartprop:LineChartAppearance>
                                        </ChartTypeAppearances>
                                    </igchartprop:ChartLayerAppearance>
                                    <igchartprop:ChartLayerAppearance AxisXKey="axis3" AxisYKey="axis2" 
                                        ChartAreaKey="area1" ChartType="LineChart" Key="chartLayer3" 
                                        SeriesList="series4">
                                        <ChartTypeAppearances>
                                            <igchartprop:LineChartAppearance>
                                                <ChartText>
                                                    <igchartprop:ChartTextAppearance ChartTextFont="Arial, 7pt" Column="0" 
                                                        ItemFormatString="&lt;DATA_VALUE:00&gt;" Row="0" />
                                                </ChartText>
                                                <LineAppearances>
                                                    <igchartprop:LineAppearance>
                                                        <IconAppearance CharacterFont="Arial Narrow, 6.75pt">
                                                            <PE ElementType="None" StrokeWidth="2" />
                                                        </IconAppearance>
                                                    </igchartprop:LineAppearance>
                                                </LineAppearances>
                                            </igchartprop:LineChartAppearance>
                                        </ChartTypeAppearances>
                                    </igchartprop:ChartLayerAppearance>
                                </ChartLayers>
                                <ChartAreas>
                                    <igchartprop:ChartArea Bounds="0, 0, 100, 100" BoundsMeasureType="Percentage" 
                                        Key="area1">
                                        <Axes>
                                            <igchartprop:AxisItem DataType="String" Extent="20" Key="axis1" 
                                                OrientationType="X_Axis" SetLabelAxisType="GroupBySeries" TickmarkInterval="1">
                                                <MajorGridLines AlphaLevel="255" Color="Gainsboro" DrawStyle="Dot" 
                                                    Thickness="1" Visible="True" />
                                                <MinorGridLines AlphaLevel="255" Color="LightGray" DrawStyle="Dot" 
                                                    Thickness="1" Visible="False" />
                                                <Labels HorizontalAlign="Near" ItemFormatString="" Orientation="Horizontal" 
                                                    VerticalAlign="Center" Font="Arial, 6.75pt" Visible="False">
                                                    <SeriesLabels HorizontalAlign="Center" Orientation="Horizontal" 
                                                        VerticalAlign="Center" Visible="False">
                                                    </SeriesLabels>
                                                </Labels>
                                            </igchartprop:AxisItem>
                                            <igchartprop:AxisItem DataType="Numeric" Extent="20" Key="axis2" 
                                                OrientationType="Y_Axis" SetLabelAxisType="GroupBySeries" TickmarkInterval="0">
                                                <MajorGridLines AlphaLevel="255" Color="Gainsboro" DrawStyle="Dot" 
                                                    Thickness="1" Visible="True" />
                                                <MinorGridLines AlphaLevel="255" Color="LightGray" DrawStyle="Dot" 
                                                    Thickness="1" Visible="False" />
                                                <Labels HorizontalAlign="Near" ItemFormatString="&lt;DATA_VALUE:00&gt;" Orientation="Horizontal" 
                                                    VerticalAlign="Center" Flip="True" Font="Arial, 8.25pt">
                                                    <SeriesLabels HorizontalAlign="Center" Orientation="Horizontal" 
                                                        VerticalAlign="Center">
                                                    </SeriesLabels>
                                                </Labels>
                                            </igchartprop:AxisItem>
                                            <igchartprop:AxisItem DataType="String" Extent="60" Key="axis3" 
                                                OrientationType="X_Axis" SetLabelAxisType="ContinuousData" TickmarkInterval="0">
                                                <MajorGridLines AlphaLevel="255" Color="Gainsboro" DrawStyle="Dot" 
                                                    Thickness="1" Visible="True" />
                                                <MinorGridLines AlphaLevel="255" Color="LightGray" DrawStyle="Dot" 
                                                    Thickness="1" Visible="False" />
                                                <Labels HorizontalAlign="Near" ItemFormatString="&lt;ITEM_LABEL&gt;" Orientation="VerticalLeftFacing" 
                                                    VerticalAlign="Center" Flip="True" Font="Arial, 6.75pt">
                                                    <SeriesLabels HorizontalAlign="Center" Orientation="Horizontal" 
                                                        VerticalAlign="Center">
                                                    </SeriesLabels>
                                                </Labels>
                                            </igchartprop:AxisItem>
                                        </Axes>
                                        <PE Texture="Blocks" />
                                        <GridPE ElementType="None" />
                                    </igchartprop:ChartArea>
                                </ChartAreas>
                                <Legends>
                                    <igchartprop:CompositeLegend BoundsMeasureType="Percentage">
                                    </igchartprop:CompositeLegend>
                                </Legends>
                            </CompositeChart>
                            <Axis>
                                <PE ElementType="None" Fill="Cornsilk" />
                                <X LineThickness="1" TickmarkInterval="0" TickmarkStyle="Smart" Visible="True">
                                    <MajorGridLines AlphaLevel="255" Color="Gainsboro" DrawStyle="Dot" 
                                        Thickness="1" Visible="True" />
                                    <MinorGridLines AlphaLevel="255" Color="LightGray" DrawStyle="Dot" 
                                        Thickness="1" Visible="False" />
                                    <Labels Font="Verdana, 7pt" FontColor="DimGray" HorizontalAlign="Near" 
                                        ItemFormatString="&lt;ITEM_LABEL&gt;" Orientation="VerticalLeftFacing" 
                                        VerticalAlign="Center">
                                        <SeriesLabels Font="Verdana, 7pt" FontColor="DimGray" FormatString="" 
                                            HorizontalAlign="Near" Orientation="VerticalLeftFacing" VerticalAlign="Center">
                                            <Layout Behavior="Auto">
                                            </Layout>
                                        </SeriesLabels>
                                        <Layout Behavior="Auto">
                                        </Layout>
                                    </Labels>
                                </X>
                                <Y LineThickness="1" TickmarkInterval="0" TickmarkStyle="Smart" Visible="True">
                                    <MajorGridLines AlphaLevel="255" Color="Gainsboro" DrawStyle="Dot" 
                                        Thickness="1" Visible="True" />
                                    <MinorGridLines AlphaLevel="255" Color="LightGray" DrawStyle="Dot" 
                                        Thickness="1" Visible="False" />
                                    <Labels Font="Verdana, 7pt" FontColor="DimGray" HorizontalAlign="Far" 
                                        ItemFormatString="&lt;DATA_VALUE:00.##&gt;" Orientation="Horizontal" 
                                        VerticalAlign="Center">
                                        <SeriesLabels Font="Verdana, 7pt" FontColor="DimGray" FormatString="" 
                                            HorizontalAlign="Far" Orientation="Horizontal" VerticalAlign="Center">
                                            <Layout Behavior="Auto">
                                            </Layout>
                                        </SeriesLabels>
                                        <Layout Behavior="Auto">
                                        </Layout>
                                    </Labels>
                                </Y>
                                <Y2 LineThickness="1" TickmarkInterval="0" TickmarkStyle="Smart" 
                                    Visible="False">
                                    <MajorGridLines AlphaLevel="255" Color="Gainsboro" DrawStyle="Dot" 
                                        Thickness="1" Visible="True" />
                                    <MinorGridLines AlphaLevel="255" Color="LightGray" DrawStyle="Dot" 
                                        Thickness="1" Visible="False" />
                                    <Labels Font="Verdana, 7pt" FontColor="Gray" HorizontalAlign="Near" 
                                        ItemFormatString="&lt;DATA_VALUE:00.##&gt;" Orientation="Horizontal" 
                                        VerticalAlign="Center" Visible="False">
                                        <SeriesLabels Font="Verdana, 7pt" FontColor="Gray" FormatString="" 
                                            HorizontalAlign="Near" Orientation="Horizontal" VerticalAlign="Center">
                                            <Layout Behavior="Auto">
                                            </Layout>
                                        </SeriesLabels>
                                        <Layout Behavior="Auto">
                                        </Layout>
                                    </Labels>
                                </Y2>
                                <X2 LineThickness="1" TickmarkInterval="0" TickmarkStyle="Smart" 
                                    Visible="False">
                                    <MajorGridLines AlphaLevel="255" Color="Gainsboro" DrawStyle="Dot" 
                                        Thickness="1" Visible="True" />
                                    <MinorGridLines AlphaLevel="255" Color="LightGray" DrawStyle="Dot" 
                                        Thickness="1" Visible="False" />
                                    <Labels Font="Verdana, 7pt" FontColor="Gray" HorizontalAlign="Far" 
                                        ItemFormatString="&lt;ITEM_LABEL&gt;" Orientation="VerticalLeftFacing" 
                                        VerticalAlign="Center" Visible="False">
                                        <SeriesLabels Font="Verdana, 7pt" FontColor="Gray" FormatString="" 
                                            HorizontalAlign="Far" Orientation="VerticalLeftFacing" VerticalAlign="Center">
                                            <Layout Behavior="Auto">
                                            </Layout>
                                        </SeriesLabels>
                                        <Layout Behavior="Auto">
                                        </Layout>
                                    </Labels>
                                </X2>
                                <Z LineThickness="1" TickmarkInterval="0" TickmarkStyle="Smart" Visible="False">
                                    <MajorGridLines AlphaLevel="255" Color="Gainsboro" DrawStyle="Dot" 
                                        Thickness="1" Visible="True" />
                                    <MinorGridLines AlphaLevel="255" Color="LightGray" DrawStyle="Dot" 
                                        Thickness="1" Visible="False" />
                                    <Labels Font="Verdana, 7pt" FontColor="DimGray" HorizontalAlign="Near" 
                                        ItemFormatString="" Orientation="Horizontal" VerticalAlign="Center">
                                        <SeriesLabels Font="Verdana, 7pt" FontColor="DimGray" HorizontalAlign="Near" 
                                            Orientation="Horizontal" VerticalAlign="Center">
                                            <Layout Behavior="Auto">
                                            </Layout>
                                        </SeriesLabels>
                                        <Layout Behavior="Auto">
                                        </Layout>
                                    </Labels>
                                </Z>
                                <Z2 LineThickness="1" TickmarkInterval="0" TickmarkStyle="Smart" 
                                    Visible="False">
                                    <MajorGridLines AlphaLevel="255" Color="Gainsboro" DrawStyle="Dot" 
                                        Thickness="1" Visible="True" />
                                    <MinorGridLines AlphaLevel="255" Color="LightGray" DrawStyle="Dot" 
                                        Thickness="1" Visible="False" />
                                    <Labels Font="Verdana, 7pt" FontColor="Gray" HorizontalAlign="Near" 
                                        ItemFormatString="" Orientation="Horizontal" VerticalAlign="Center" 
                                        Visible="False">
                                        <SeriesLabels Font="Verdana, 7pt" FontColor="Gray" HorizontalAlign="Near" 
                                            Orientation="Horizontal" VerticalAlign="Center">
                                            <Layout Behavior="Auto">
                                            </Layout>
                                        </SeriesLabels>
                                        <Layout Behavior="Auto">
                                        </Layout>
                                    </Labels>
                                </Z2>
                            </Axis>
                            <Effects>
                                <Effects>
                                    <igchartprop:GradientEffect />
                                </Effects>
                            </Effects>
                                    <ColorModel AlphaLevel="150" ColorBegin="Pink" ColorEnd="DarkRed" 
                                        ModelStyle="CustomLinear">
                                    </ColorModel>
                                    <TitleBottom Extent="33" Font="Arial, 8.25pt" Location="Bottom">
                                    </TitleBottom>
                            <Border Thickness="0" />
                            <Tooltips Font-Bold="False" Font-Italic="False" Font-Overline="False" 
                                Font-Strikeout="False" Font-Underline="False" />
                        </igchart:UltraChart>
                    </Template>
                </igmisc:WebPanel>
            </td>
        </tr>
        <tr>
            <td valign="top" style="width:500px;" align="left">
                <igmisc:WebPanel ID="WebPanel3" runat="server" EnableAppStyling="True" Width="500px" 
                    StyleSetName="">
                    <Header Text ="% Perdida en quemado" >
                    </Header>
                    <Template>
                        <igchart:UltraChart ID="UltraChart3" runat="server" BackgroundImageFileName="" 
                            BorderColor="Black" BorderWidth="0px" ChartType="Composite" 
                            EmptyChartText="Data Not Available. Please call UltraChart.Data.DataBind() after setting valid Data.DataSource" 
                            Version="9.1" onchartdataclicked="UltraChart1_ChartDataClicked" Width="500px" 
                                    Height="206px">
                                    <Legend BackgroundColor="White" BorderColor="Black" 
                                        DataAssociation="ColumnData" Font="Arial, 8.25pt" Visible="True"></Legend>
                            <CompositeChart>
                                <Series>
                                    <igchartprop:NumericSeries Key="series1" Label="Prueba 1">
                                        <points>
                                            <igchartprop:NumericDataPoint Label="Enero" Value="26">
                                                <pe elementtype="None" />
                                            </igchartprop:NumericDataPoint>
                                            <igchartprop:NumericDataPoint Label="Febrero" Value="32">
                                                <pe elementtype="None" />
                                            </igchartprop:NumericDataPoint>
                                            <igchartprop:NumericDataPoint Label="Marzo" Value="28">
                                                <pe elementtype="None" />
                                            </igchartprop:NumericDataPoint>
                                            <igchartprop:NumericDataPoint Label="1">
                                                <pe elementtype="None" />
                                            </igchartprop:NumericDataPoint>
                                            <igchartprop:NumericDataPoint Label="2">
                                                <pe elementtype="None" />
                                            </igchartprop:NumericDataPoint>
                                            <igchartprop:NumericDataPoint Label="3">
                                                <pe elementtype="None" />
                                            </igchartprop:NumericDataPoint>
                                            <igchartprop:NumericDataPoint Label="4">
                                                <pe elementtype="None" />
                                            </igchartprop:NumericDataPoint>
                                            <igchartprop:NumericDataPoint Label="5">
                                                <pe elementtype="None" />
                                            </igchartprop:NumericDataPoint>
                                            <igchartprop:NumericDataPoint Label="6">
                                                <pe elementtype="None" />
                                            </igchartprop:NumericDataPoint>
                                            <igchartprop:NumericDataPoint Label="7">
                                                <pe elementtype="None" />
                                            </igchartprop:NumericDataPoint>
                                            <igchartprop:NumericDataPoint Label="8">
                                                <pe elementtype="None" />
                                            </igchartprop:NumericDataPoint>
                                            <igchartprop:NumericDataPoint Label="9">
                                                <pe elementtype="None" />
                                            </igchartprop:NumericDataPoint>
                                            <igchartprop:NumericDataPoint Label="10">
                                                <pe elementtype="None" />
                                            </igchartprop:NumericDataPoint>
                                            <igchartprop:NumericDataPoint Label="11">
                                                <pe elementtype="None" />
                                            </igchartprop:NumericDataPoint>
                                            <igchartprop:NumericDataPoint Label="12">
                                                <pe elementtype="None" />
                                            </igchartprop:NumericDataPoint>
                                            <igchartprop:NumericDataPoint Label="13">
                                                <pe elementtype="None" />
                                            </igchartprop:NumericDataPoint>
                                            <igchartprop:NumericDataPoint Label="14">
                                                <pe elementtype="None" />
                                            </igchartprop:NumericDataPoint>
                                            <igchartprop:NumericDataPoint Label="15">
                                                <pe elementtype="None" />
                                            </igchartprop:NumericDataPoint>
                                            <igchartprop:NumericDataPoint Label="16">
                                                <pe elementtype="None" />
                                            </igchartprop:NumericDataPoint>
                                            <igchartprop:NumericDataPoint Label="17">
                                                <pe elementtype="None" />
                                            </igchartprop:NumericDataPoint>
                                            <igchartprop:NumericDataPoint Label="18">
                                                <pe elementtype="None" />
                                            </igchartprop:NumericDataPoint>
                                            <igchartprop:NumericDataPoint Label="19">
                                                <pe elementtype="None" />
                                            </igchartprop:NumericDataPoint>
                                            <igchartprop:NumericDataPoint Label="20">
                                                <pe elementtype="None" />
                                            </igchartprop:NumericDataPoint>
                                            <igchartprop:NumericDataPoint Label="21">
                                                <pe elementtype="None" />
                                            </igchartprop:NumericDataPoint>
                                            <igchartprop:NumericDataPoint Label="22">
                                                <pe elementtype="None" />
                                            </igchartprop:NumericDataPoint>
                                            <igchartprop:NumericDataPoint Label="23">
                                                <pe elementtype="None" />
                                            </igchartprop:NumericDataPoint>
                                            <igchartprop:NumericDataPoint Label="24">
                                                <pe elementtype="None" />
                                            </igchartprop:NumericDataPoint>
                                            <igchartprop:NumericDataPoint Label="25">
                                                <pe elementtype="None" />
                                            </igchartprop:NumericDataPoint>
                                            <igchartprop:NumericDataPoint Label="26">
                                                <pe elementtype="None" />
                                            </igchartprop:NumericDataPoint>
                                            <igchartprop:NumericDataPoint Label="27">
                                                <pe elementtype="None" />
                                            </igchartprop:NumericDataPoint>
                                            <igchartprop:NumericDataPoint Label="28">
                                                <pe elementtype="None" />
                                            </igchartprop:NumericDataPoint>
                                            <igchartprop:NumericDataPoint Label="29">
                                                <pe elementtype="None" />
                                            </igchartprop:NumericDataPoint>
                                            <igchartprop:NumericDataPoint Label="30">
                                                <pe elementtype="None" />
                                            </igchartprop:NumericDataPoint>
                                            <igchartprop:NumericDataPoint Label="31">
                                                <pe elementtype="None" />
                                            </igchartprop:NumericDataPoint>
                                        </points>
                                    </igchartprop:NumericSeries>
                                    <igchartprop:NumericSeries Key="series3" Label="Prueba2">
                                        <points>
                                            <igchartprop:NumericDataPoint Label="Enero">
                                                <pe elementtype="None" />
                                            </igchartprop:NumericDataPoint>
                                            <igchartprop:NumericDataPoint Label="Febrero">
                                                <pe elementtype="None" />
                                            </igchartprop:NumericDataPoint>
                                            <igchartprop:NumericDataPoint Label="Marzo">
                                                <pe elementtype="None" />
                                            </igchartprop:NumericDataPoint>
                                            <igchartprop:NumericDataPoint Label="1" Value="25">
                                                <pe elementtype="None" />
                                            </igchartprop:NumericDataPoint>
                                            <igchartprop:NumericDataPoint Label="2" Value="32">
                                                <pe elementtype="None" />
                                            </igchartprop:NumericDataPoint>
                                            <igchartprop:NumericDataPoint Label="3" Value="20">
                                                <pe elementtype="None" />
                                            </igchartprop:NumericDataPoint>
                                            <igchartprop:NumericDataPoint Label="4" Value="12">
                                                <pe elementtype="None" />
                                            </igchartprop:NumericDataPoint>
                                            <igchartprop:NumericDataPoint Label="5" Value="24">
                                                <pe elementtype="None" />
                                            </igchartprop:NumericDataPoint>
                                            <igchartprop:NumericDataPoint Label="6" Value="20">
                                                <pe elementtype="None" />
                                            </igchartprop:NumericDataPoint>
                                            <igchartprop:NumericDataPoint Label="7" Value="21">
                                                <pe elementtype="None" />
                                            </igchartprop:NumericDataPoint>
                                            <igchartprop:NumericDataPoint Label="8" Value="25">
                                                <pe elementtype="None" />
                                            </igchartprop:NumericDataPoint>
                                            <igchartprop:NumericDataPoint Label="9" Value="30">
                                                <pe elementtype="None" />
                                            </igchartprop:NumericDataPoint>
                                            <igchartprop:NumericDataPoint Label="10" Value="29">
                                                <pe elementtype="None" />
                                            </igchartprop:NumericDataPoint>
                                            <igchartprop:NumericDataPoint Label="11" Value="24">
                                                <pe elementtype="None" />
                                            </igchartprop:NumericDataPoint>
                                            <igchartprop:NumericDataPoint Label="12" Value="18">
                                                <pe elementtype="None" />
                                            </igchartprop:NumericDataPoint>
                                            <igchartprop:NumericDataPoint Label="13" Value="16">
                                                <pe elementtype="None" />
                                            </igchartprop:NumericDataPoint>
                                            <igchartprop:NumericDataPoint Label="14" Value="27">
                                                <pe elementtype="None" />
                                            </igchartprop:NumericDataPoint>
                                            <igchartprop:NumericDataPoint Label="15" Value="23">
                                                <pe elementtype="None" />
                                            </igchartprop:NumericDataPoint>
                                            <igchartprop:NumericDataPoint Label="16" Value="28">
                                                <pe elementtype="None" />
                                            </igchartprop:NumericDataPoint>
                                            <igchartprop:NumericDataPoint Label="17" Value="22">
                                                <pe elementtype="None" />
                                            </igchartprop:NumericDataPoint>
                                            <igchartprop:NumericDataPoint Label="18" Value="31">
                                                <pe elementtype="None" />
                                            </igchartprop:NumericDataPoint>
                                            <igchartprop:NumericDataPoint Label="19" Value="23">
                                                <pe elementtype="None" />
                                            </igchartprop:NumericDataPoint>
                                            <igchartprop:NumericDataPoint Label="20" Value="24">
                                                <pe elementtype="None" />
                                            </igchartprop:NumericDataPoint>
                                            <igchartprop:NumericDataPoint Label="21" Value="25">
                                                <pe elementtype="None" />
                                            </igchartprop:NumericDataPoint>
                                            <igchartprop:NumericDataPoint Label="22" Value="30">
                                                <pe elementtype="None" />
                                            </igchartprop:NumericDataPoint>
                                            <igchartprop:NumericDataPoint Label="23" Value="14">
                                                <pe elementtype="None" />
                                            </igchartprop:NumericDataPoint>
                                            <igchartprop:NumericDataPoint Label="24" Value="36">
                                                <pe elementtype="None" />
                                            </igchartprop:NumericDataPoint>
                                            <igchartprop:NumericDataPoint Label="25" Value="30">
                                                <pe elementtype="None" />
                                            </igchartprop:NumericDataPoint>
                                            <igchartprop:NumericDataPoint Label="26" Value="24">
                                                <pe elementtype="None" />
                                            </igchartprop:NumericDataPoint>
                                            <igchartprop:NumericDataPoint Label="27" Value="26">
                                                <pe elementtype="None" />
                                            </igchartprop:NumericDataPoint>
                                            <igchartprop:NumericDataPoint Label="28" Value="20">
                                                <pe elementtype="None" />
                                            </igchartprop:NumericDataPoint>
                                            <igchartprop:NumericDataPoint Label="29" Value="30">
                                                <pe elementtype="None" />
                                            </igchartprop:NumericDataPoint>
                                            <igchartprop:NumericDataPoint Label="30" Value="21">
                                                <pe elementtype="None" />
                                            </igchartprop:NumericDataPoint>
                                            <igchartprop:NumericDataPoint Label="31" Value="22">
                                                <pe elementtype="None" />
                                            </igchartprop:NumericDataPoint>
                                        </points>
                                        <pes>
                                            <igchartprop:PaintElement Fill="Red" />
                                        </pes>
                                    </igchartprop:NumericSeries>
                                    <igchartprop:NumericSeries Key="series4" Label="Prueba3">
                                        <points>
                                            <igchartprop:NumericDataPoint Label="Enero">
                                                <pe elementtype="None" />
                                            </igchartprop:NumericDataPoint>
                                            <igchartprop:NumericDataPoint Label="Febrero">
                                                <pe elementtype="None" />
                                            </igchartprop:NumericDataPoint>
                                            <igchartprop:NumericDataPoint Label="Marzo">
                                                <pe elementtype="None" />
                                            </igchartprop:NumericDataPoint>
                                            <igchartprop:NumericDataPoint Label="1" Value="20">
                                                <pe elementtype="None" />
                                            </igchartprop:NumericDataPoint>
                                            <igchartprop:NumericDataPoint Label="2" Value="20">
                                                <pe elementtype="None" />
                                            </igchartprop:NumericDataPoint>
                                            <igchartprop:NumericDataPoint Label="3" Value="20">
                                                <pe elementtype="None" />
                                            </igchartprop:NumericDataPoint>
                                            <igchartprop:NumericDataPoint Label="4" Value="20">
                                                <pe elementtype="None" />
                                            </igchartprop:NumericDataPoint>
                                            <igchartprop:NumericDataPoint Label="5" Value="20">
                                                <pe elementtype="None" />
                                            </igchartprop:NumericDataPoint>
                                            <igchartprop:NumericDataPoint Label="6" Value="20">
                                                <pe elementtype="None" />
                                            </igchartprop:NumericDataPoint>
                                            <igchartprop:NumericDataPoint Label="7" Value="20">
                                                <pe elementtype="None" />
                                            </igchartprop:NumericDataPoint>
                                            <igchartprop:NumericDataPoint Label="8" Value="20">
                                                <pe elementtype="None" />
                                            </igchartprop:NumericDataPoint>
                                            <igchartprop:NumericDataPoint Label="9" Value="20">
                                                <pe elementtype="None" />
                                            </igchartprop:NumericDataPoint>
                                            <igchartprop:NumericDataPoint Label="10" Value="20">
                                                <pe elementtype="None" />
                                            </igchartprop:NumericDataPoint>
                                            <igchartprop:NumericDataPoint Label="11" Value="20">
                                                <pe elementtype="None" />
                                            </igchartprop:NumericDataPoint>
                                            <igchartprop:NumericDataPoint Label="12" Value="20">
                                                <pe elementtype="None" />
                                            </igchartprop:NumericDataPoint>
                                            <igchartprop:NumericDataPoint Label="13" Value="20">
                                                <pe elementtype="None" />
                                            </igchartprop:NumericDataPoint>
                                            <igchartprop:NumericDataPoint Label="14" Value="20">
                                                <pe elementtype="None" />
                                            </igchartprop:NumericDataPoint>
                                            <igchartprop:NumericDataPoint Label="15" Value="20">
                                                <pe elementtype="None" />
                                            </igchartprop:NumericDataPoint>
                                            <igchartprop:NumericDataPoint Label="16" Value="20">
                                                <pe elementtype="None" />
                                            </igchartprop:NumericDataPoint>
                                            <igchartprop:NumericDataPoint Label="17" Value="20">
                                                <pe elementtype="None" />
                                            </igchartprop:NumericDataPoint>
                                            <igchartprop:NumericDataPoint Label="18" Value="20">
                                                <pe elementtype="None" />
                                            </igchartprop:NumericDataPoint>
                                            <igchartprop:NumericDataPoint Label="19" Value="20">
                                                <pe elementtype="None" />
                                            </igchartprop:NumericDataPoint>
                                            <igchartprop:NumericDataPoint Label="20" Value="20">
                                                <pe elementtype="None" />
                                            </igchartprop:NumericDataPoint>
                                            <igchartprop:NumericDataPoint Label="21" Value="20">
                                                <pe elementtype="None" />
                                            </igchartprop:NumericDataPoint>
                                            <igchartprop:NumericDataPoint Label="22" Value="20">
                                                <pe elementtype="None" />
                                            </igchartprop:NumericDataPoint>
                                            <igchartprop:NumericDataPoint Label="23" Value="20">
                                                <pe elementtype="None" />
                                            </igchartprop:NumericDataPoint>
                                            <igchartprop:NumericDataPoint Label="24" Value="20">
                                                <pe elementtype="None" />
                                            </igchartprop:NumericDataPoint>
                                            <igchartprop:NumericDataPoint Label="25" Value="20">
                                                <pe elementtype="None" />
                                            </igchartprop:NumericDataPoint>
                                            <igchartprop:NumericDataPoint Label="26" Value="20">
                                                <pe elementtype="None" />
                                            </igchartprop:NumericDataPoint>
                                            <igchartprop:NumericDataPoint Label="27" Value="20">
                                                <pe elementtype="None" />
                                            </igchartprop:NumericDataPoint>
                                            <igchartprop:NumericDataPoint Label="28" Value="20">
                                                <pe elementtype="None" />
                                            </igchartprop:NumericDataPoint>
                                            <igchartprop:NumericDataPoint Label="29" Value="20">
                                                <pe elementtype="None" />
                                            </igchartprop:NumericDataPoint>
                                            <igchartprop:NumericDataPoint Label="30" Value="20">
                                                <pe elementtype="None" />
                                            </igchartprop:NumericDataPoint>
                                            <igchartprop:NumericDataPoint Label="31" Value="20">
                                                <pe elementtype="None" />
                                            </igchartprop:NumericDataPoint>
                                        </points>
                                        <pes>
                                            <igchartprop:PaintElement Fill="Blue" />
                                        </pes>
                                    </igchartprop:NumericSeries>
                                </Series>
                                <ChartLayers>
                                    <igchartprop:ChartLayerAppearance AxisXKey="axis1" AxisYKey="axis2" 
                                        ChartAreaKey="area1" ChartType="ColumnChart" Key="chartLayer1" 
                                        SeriesList="series1">
                                        <ChartTypeAppearances>
                                            <igchartprop:ColumnChartAppearance NullHandling="DontPlot" 
                                                ChartTextValueAlignment="PositiveAndNegative">
                                                <ChartText>
                                                    <igchartprop:ChartTextAppearance ChartTextFont="Arial, 7pt" Column="0" 
                                                        ItemFormatString="&lt;DATA_VALUE:00&gt;" Row="0" />
                                                </ChartText>
                                            </igchartprop:ColumnChartAppearance>
                                        </ChartTypeAppearances>
                                    </igchartprop:ChartLayerAppearance>
                                    <igchartprop:ChartLayerAppearance AxisXKey="axis3" AxisYKey="axis2" 
                                        ChartAreaKey="area1" ChartType="LineChart" Key="chartLayer2" 
                                        SeriesList="series3">
                                        <ChartTypeAppearances>
                                            <igchartprop:LineChartAppearance HighLightLines="True" MidPointAnchors="False" 
                                                NullHandling="DontPlot" Thickness="4">
                                                <ChartText>
                                                    <igchartprop:ChartTextAppearance ChartTextFont="Arial, 7pt" Column="0" 
                                                        ItemFormatString="&lt;DATA_VALUE:00&gt;" Row="0" />
                                                </ChartText>
                                                <LineAppearances>
                                                    <igchartprop:LineAppearance>
                                                        <IconAppearance CharacterFont="Arial, 8.25pt" Icon="Circle">
                                                            <PE ElementType="None" />
                                                        </IconAppearance>
                                                    </igchartprop:LineAppearance>
                                                </LineAppearances>
                                            </igchartprop:LineChartAppearance>
                                        </ChartTypeAppearances>
                                    </igchartprop:ChartLayerAppearance>
                                    <igchartprop:ChartLayerAppearance AxisXKey="axis3" AxisYKey="axis2" 
                                        ChartAreaKey="area1" ChartType="LineChart" Key="chartLayer3" 
                                        SeriesList="series4">
                                        <ChartTypeAppearances>
                                            <igchartprop:LineChartAppearance>
                                                <ChartText>
                                                    <igchartprop:ChartTextAppearance ChartTextFont="Arial, 7pt" Column="0" 
                                                        ItemFormatString="&lt;DATA_VALUE:00&gt;" Row="0" />
                                                </ChartText>
                                                <LineAppearances>
                                                    <igchartprop:LineAppearance>
                                                        <IconAppearance CharacterFont="Arial Narrow, 6.75pt">
                                                            <PE ElementType="None" StrokeWidth="2" />
                                                        </IconAppearance>
                                                    </igchartprop:LineAppearance>
                                                </LineAppearances>
                                            </igchartprop:LineChartAppearance>
                                        </ChartTypeAppearances>
                                    </igchartprop:ChartLayerAppearance>
                                </ChartLayers>
                                <ChartAreas>
                                    <igchartprop:ChartArea Bounds="0, 0, 100, 100" BoundsMeasureType="Percentage" 
                                        Key="area1">
                                        <Axes>
                                            <igchartprop:AxisItem DataType="String" Extent="20" Key="axis1" 
                                                OrientationType="X_Axis" SetLabelAxisType="GroupBySeries" TickmarkInterval="1">
                                                <MajorGridLines AlphaLevel="255" Color="Gainsboro" DrawStyle="Dot" 
                                                    Thickness="1" Visible="True" />
                                                <MinorGridLines AlphaLevel="255" Color="LightGray" DrawStyle="Dot" 
                                                    Thickness="1" Visible="False" />
                                                <Labels HorizontalAlign="Near" ItemFormatString="" Orientation="Horizontal" 
                                                    VerticalAlign="Center" Font="Arial, 6.75pt" Visible="False">
                                                    <SeriesLabels HorizontalAlign="Center" Orientation="Horizontal" 
                                                        VerticalAlign="Center" Visible="False">
                                                    </SeriesLabels>
                                                </Labels>
                                            </igchartprop:AxisItem>
                                            <igchartprop:AxisItem DataType="Numeric" Extent="20" Key="axis2" 
                                                OrientationType="Y_Axis" SetLabelAxisType="GroupBySeries" TickmarkInterval="0">
                                                <MajorGridLines AlphaLevel="255" Color="Gainsboro" DrawStyle="Dot" 
                                                    Thickness="1" Visible="True" />
                                                <MinorGridLines AlphaLevel="255" Color="LightGray" DrawStyle="Dot" 
                                                    Thickness="1" Visible="False" />
                                                <Labels HorizontalAlign="Near" ItemFormatString="&lt;DATA_VALUE:00&gt;" Orientation="Horizontal" 
                                                    VerticalAlign="Center" Flip="True" Font="Arial, 8.25pt">
                                                    <SeriesLabels HorizontalAlign="Center" Orientation="Horizontal" 
                                                        VerticalAlign="Center">
                                                    </SeriesLabels>
                                                </Labels>
                                            </igchartprop:AxisItem>
                                            <igchartprop:AxisItem DataType="String" Extent="60" Key="axis3" 
                                                OrientationType="X_Axis" SetLabelAxisType="ContinuousData" TickmarkInterval="0">
                                                <MajorGridLines AlphaLevel="255" Color="Gainsboro" DrawStyle="Dot" 
                                                    Thickness="1" Visible="True" />
                                                <MinorGridLines AlphaLevel="255" Color="LightGray" DrawStyle="Dot" 
                                                    Thickness="1" Visible="False" />
                                                <Labels HorizontalAlign="Near" ItemFormatString="&lt;ITEM_LABEL&gt;" Orientation="VerticalLeftFacing" 
                                                    VerticalAlign="Center" Flip="True" Font="Arial, 6.75pt">
                                                    <SeriesLabels HorizontalAlign="Center" Orientation="Horizontal" 
                                                        VerticalAlign="Center">
                                                    </SeriesLabels>
                                                </Labels>
                                            </igchartprop:AxisItem>
                                        </Axes>
                                        <PE Texture="Blocks" />
                                        <GridPE ElementType="None" />
                                    </igchartprop:ChartArea>
                                </ChartAreas>
                                <Legends>
                                    <igchartprop:CompositeLegend BoundsMeasureType="Percentage">
                                    </igchartprop:CompositeLegend>
                                </Legends>
                            </CompositeChart>
                            <Axis>
                                <PE ElementType="None" Fill="Cornsilk" />
                                <X LineThickness="1" TickmarkInterval="0" TickmarkStyle="Smart" Visible="True">
                                    <MajorGridLines AlphaLevel="255" Color="Gainsboro" DrawStyle="Dot" 
                                        Thickness="1" Visible="True" />
                                    <MinorGridLines AlphaLevel="255" Color="LightGray" DrawStyle="Dot" 
                                        Thickness="1" Visible="False" />
                                    <Labels Font="Verdana, 7pt" FontColor="DimGray" HorizontalAlign="Near" 
                                        ItemFormatString="&lt;ITEM_LABEL&gt;" Orientation="VerticalLeftFacing" 
                                        VerticalAlign="Center">
                                        <SeriesLabels Font="Verdana, 7pt" FontColor="DimGray" FormatString="" 
                                            HorizontalAlign="Near" Orientation="VerticalLeftFacing" VerticalAlign="Center">
                                            <Layout Behavior="Auto">
                                            </Layout>
                                        </SeriesLabels>
                                        <Layout Behavior="Auto">
                                        </Layout>
                                    </Labels>
                                </X>
                                <Y LineThickness="1" TickmarkInterval="0" TickmarkStyle="Smart" Visible="True">
                                    <MajorGridLines AlphaLevel="255" Color="Gainsboro" DrawStyle="Dot" 
                                        Thickness="1" Visible="True" />
                                    <MinorGridLines AlphaLevel="255" Color="LightGray" DrawStyle="Dot" 
                                        Thickness="1" Visible="False" />
                                    <Labels Font="Verdana, 7pt" FontColor="DimGray" HorizontalAlign="Far" 
                                        ItemFormatString="&lt;DATA_VALUE:00.##&gt;" Orientation="Horizontal" 
                                        VerticalAlign="Center">
                                        <SeriesLabels Font="Verdana, 7pt" FontColor="DimGray" FormatString="" 
                                            HorizontalAlign="Far" Orientation="Horizontal" VerticalAlign="Center">
                                            <Layout Behavior="Auto">
                                            </Layout>
                                        </SeriesLabels>
                                        <Layout Behavior="Auto">
                                        </Layout>
                                    </Labels>
                                </Y>
                                <Y2 LineThickness="1" TickmarkInterval="0" TickmarkStyle="Smart" 
                                    Visible="False">
                                    <MajorGridLines AlphaLevel="255" Color="Gainsboro" DrawStyle="Dot" 
                                        Thickness="1" Visible="True" />
                                    <MinorGridLines AlphaLevel="255" Color="LightGray" DrawStyle="Dot" 
                                        Thickness="1" Visible="False" />
                                    <Labels Font="Verdana, 7pt" FontColor="Gray" HorizontalAlign="Near" 
                                        ItemFormatString="&lt;DATA_VALUE:00.##&gt;" Orientation="Horizontal" 
                                        VerticalAlign="Center" Visible="False">
                                        <SeriesLabels Font="Verdana, 7pt" FontColor="Gray" FormatString="" 
                                            HorizontalAlign="Near" Orientation="Horizontal" VerticalAlign="Center">
                                            <Layout Behavior="Auto">
                                            </Layout>
                                        </SeriesLabels>
                                        <Layout Behavior="Auto">
                                        </Layout>
                                    </Labels>
                                </Y2>
                                <X2 LineThickness="1" TickmarkInterval="0" TickmarkStyle="Smart" 
                                    Visible="False">
                                    <MajorGridLines AlphaLevel="255" Color="Gainsboro" DrawStyle="Dot" 
                                        Thickness="1" Visible="True" />
                                    <MinorGridLines AlphaLevel="255" Color="LightGray" DrawStyle="Dot" 
                                        Thickness="1" Visible="False" />
                                    <Labels Font="Verdana, 7pt" FontColor="Gray" HorizontalAlign="Far" 
                                        ItemFormatString="&lt;ITEM_LABEL&gt;" Orientation="VerticalLeftFacing" 
                                        VerticalAlign="Center" Visible="False">
                                        <SeriesLabels Font="Verdana, 7pt" FontColor="Gray" FormatString="" 
                                            HorizontalAlign="Far" Orientation="VerticalLeftFacing" VerticalAlign="Center">
                                            <Layout Behavior="Auto">
                                            </Layout>
                                        </SeriesLabels>
                                        <Layout Behavior="Auto">
                                        </Layout>
                                    </Labels>
                                </X2>
                                <Z LineThickness="1" TickmarkInterval="0" TickmarkStyle="Smart" Visible="False">
                                    <MajorGridLines AlphaLevel="255" Color="Gainsboro" DrawStyle="Dot" 
                                        Thickness="1" Visible="True" />
                                    <MinorGridLines AlphaLevel="255" Color="LightGray" DrawStyle="Dot" 
                                        Thickness="1" Visible="False" />
                                    <Labels Font="Verdana, 7pt" FontColor="DimGray" HorizontalAlign="Near" 
                                        ItemFormatString="" Orientation="Horizontal" VerticalAlign="Center">
                                        <SeriesLabels Font="Verdana, 7pt" FontColor="DimGray" HorizontalAlign="Near" 
                                            Orientation="Horizontal" VerticalAlign="Center">
                                            <Layout Behavior="Auto">
                                            </Layout>
                                        </SeriesLabels>
                                        <Layout Behavior="Auto">
                                        </Layout>
                                    </Labels>
                                </Z>
                                <Z2 LineThickness="1" TickmarkInterval="0" TickmarkStyle="Smart" 
                                    Visible="False">
                                    <MajorGridLines AlphaLevel="255" Color="Gainsboro" DrawStyle="Dot" 
                                        Thickness="1" Visible="True" />
                                    <MinorGridLines AlphaLevel="255" Color="LightGray" DrawStyle="Dot" 
                                        Thickness="1" Visible="False" />
                                    <Labels Font="Verdana, 7pt" FontColor="Gray" HorizontalAlign="Near" 
                                        ItemFormatString="" Orientation="Horizontal" VerticalAlign="Center" 
                                        Visible="False">
                                        <SeriesLabels Font="Verdana, 7pt" FontColor="Gray" HorizontalAlign="Near" 
                                            Orientation="Horizontal" VerticalAlign="Center">
                                            <Layout Behavior="Auto">
                                            </Layout>
                                        </SeriesLabels>
                                        <Layout Behavior="Auto">
                                        </Layout>
                                    </Labels>
                                </Z2>
                            </Axis>
                            <Effects>
                                <Effects>
                                    <igchartprop:GradientEffect />
                                </Effects>
                            </Effects>
                                    <ColorModel AlphaLevel="150" ColorBegin="Pink" ColorEnd="DarkRed" 
                                        ModelStyle="CustomLinear">
                                    </ColorModel>
                                    <TitleBottom Extent="33" Font="Arial, 8.25pt" Location="Bottom">
                                    </TitleBottom>
                            <Border Thickness="0" />
                            <Tooltips Font-Bold="False" Font-Italic="False" Font-Overline="False" 
                                Font-Strikeout="False" Font-Underline="False" />
                        </igchart:UltraChart>
                    </Template>
                </igmisc:WebPanel>
            </td>
            <td style="width:24px;" valign="top" ></td>
            <td valign="top" style="width:500px;" align="right">
                <igmisc:WebPanel ID="WebPanel4" runat="server" EnableAppStyling="True" Width="500px" 
                    StyleSetName="">
                    <Header Text ="% Calidad A" >
                    </Header>
                    <Template>
                        <igchart:UltraChart ID="UltraChart4" runat="server" BackgroundImageFileName="" 
                            BorderColor="Black" BorderWidth="0px" ChartType="Composite" 
                            EmptyChartText="Data Not Available. Please call UltraChart.Data.DataBind() after setting valid Data.DataSource" 
                            Version="9.1" onchartdataclicked="UltraChart1_ChartDataClicked" Width="500px" 
                                    Height="206px">
                                    <Legend BackgroundColor="White" BorderColor="Black" 
                                        DataAssociation="ColumnData" Font="Arial, 8.25pt" Visible="True"></Legend>
                            <CompositeChart>
                                <Series>
                                    <igchartprop:NumericSeries Key="series1" Label="Prueba 1">
                                        <points>
                                            <igchartprop:NumericDataPoint Label="Enero" Value="26">
                                                <pe elementtype="None" />
                                            </igchartprop:NumericDataPoint>
                                            <igchartprop:NumericDataPoint Label="Febrero" Value="32">
                                                <pe elementtype="None" />
                                            </igchartprop:NumericDataPoint>
                                            <igchartprop:NumericDataPoint Label="Marzo" Value="28">
                                                <pe elementtype="None" />
                                            </igchartprop:NumericDataPoint>
                                            <igchartprop:NumericDataPoint Label="1">
                                                <pe elementtype="None" />
                                            </igchartprop:NumericDataPoint>
                                            <igchartprop:NumericDataPoint Label="2">
                                                <pe elementtype="None" />
                                            </igchartprop:NumericDataPoint>
                                            <igchartprop:NumericDataPoint Label="3">
                                                <pe elementtype="None" />
                                            </igchartprop:NumericDataPoint>
                                            <igchartprop:NumericDataPoint Label="4">
                                                <pe elementtype="None" />
                                            </igchartprop:NumericDataPoint>
                                            <igchartprop:NumericDataPoint Label="5">
                                                <pe elementtype="None" />
                                            </igchartprop:NumericDataPoint>
                                            <igchartprop:NumericDataPoint Label="6">
                                                <pe elementtype="None" />
                                            </igchartprop:NumericDataPoint>
                                            <igchartprop:NumericDataPoint Label="7">
                                                <pe elementtype="None" />
                                            </igchartprop:NumericDataPoint>
                                            <igchartprop:NumericDataPoint Label="8">
                                                <pe elementtype="None" />
                                            </igchartprop:NumericDataPoint>
                                            <igchartprop:NumericDataPoint Label="9">
                                                <pe elementtype="None" />
                                            </igchartprop:NumericDataPoint>
                                            <igchartprop:NumericDataPoint Label="10">
                                                <pe elementtype="None" />
                                            </igchartprop:NumericDataPoint>
                                            <igchartprop:NumericDataPoint Label="11">
                                                <pe elementtype="None" />
                                            </igchartprop:NumericDataPoint>
                                            <igchartprop:NumericDataPoint Label="12">
                                                <pe elementtype="None" />
                                            </igchartprop:NumericDataPoint>
                                            <igchartprop:NumericDataPoint Label="13">
                                                <pe elementtype="None" />
                                            </igchartprop:NumericDataPoint>
                                            <igchartprop:NumericDataPoint Label="14">
                                                <pe elementtype="None" />
                                            </igchartprop:NumericDataPoint>
                                            <igchartprop:NumericDataPoint Label="15">
                                                <pe elementtype="None" />
                                            </igchartprop:NumericDataPoint>
                                            <igchartprop:NumericDataPoint Label="16">
                                                <pe elementtype="None" />
                                            </igchartprop:NumericDataPoint>
                                            <igchartprop:NumericDataPoint Label="17">
                                                <pe elementtype="None" />
                                            </igchartprop:NumericDataPoint>
                                            <igchartprop:NumericDataPoint Label="18">
                                                <pe elementtype="None" />
                                            </igchartprop:NumericDataPoint>
                                            <igchartprop:NumericDataPoint Label="19">
                                                <pe elementtype="None" />
                                            </igchartprop:NumericDataPoint>
                                            <igchartprop:NumericDataPoint Label="20">
                                                <pe elementtype="None" />
                                            </igchartprop:NumericDataPoint>
                                            <igchartprop:NumericDataPoint Label="21">
                                                <pe elementtype="None" />
                                            </igchartprop:NumericDataPoint>
                                            <igchartprop:NumericDataPoint Label="22">
                                                <pe elementtype="None" />
                                            </igchartprop:NumericDataPoint>
                                            <igchartprop:NumericDataPoint Label="23">
                                                <pe elementtype="None" />
                                            </igchartprop:NumericDataPoint>
                                            <igchartprop:NumericDataPoint Label="24">
                                                <pe elementtype="None" />
                                            </igchartprop:NumericDataPoint>
                                            <igchartprop:NumericDataPoint Label="25">
                                                <pe elementtype="None" />
                                            </igchartprop:NumericDataPoint>
                                            <igchartprop:NumericDataPoint Label="26">
                                                <pe elementtype="None" />
                                            </igchartprop:NumericDataPoint>
                                            <igchartprop:NumericDataPoint Label="27">
                                                <pe elementtype="None" />
                                            </igchartprop:NumericDataPoint>
                                            <igchartprop:NumericDataPoint Label="28">
                                                <pe elementtype="None" />
                                            </igchartprop:NumericDataPoint>
                                            <igchartprop:NumericDataPoint Label="29">
                                                <pe elementtype="None" />
                                            </igchartprop:NumericDataPoint>
                                            <igchartprop:NumericDataPoint Label="30">
                                                <pe elementtype="None" />
                                            </igchartprop:NumericDataPoint>
                                            <igchartprop:NumericDataPoint Label="31">
                                                <pe elementtype="None" />
                                            </igchartprop:NumericDataPoint>
                                        </points>
                                    </igchartprop:NumericSeries>
                                    <igchartprop:NumericSeries Key="series3" Label="Prueba2">
                                        <points>
                                            <igchartprop:NumericDataPoint Label="Enero">
                                                <pe elementtype="None" />
                                            </igchartprop:NumericDataPoint>
                                            <igchartprop:NumericDataPoint Label="Febrero">
                                                <pe elementtype="None" />
                                            </igchartprop:NumericDataPoint>
                                            <igchartprop:NumericDataPoint Label="Marzo">
                                                <pe elementtype="None" />
                                            </igchartprop:NumericDataPoint>
                                            <igchartprop:NumericDataPoint Label="1" Value="25">
                                                <pe elementtype="None" />
                                            </igchartprop:NumericDataPoint>
                                            <igchartprop:NumericDataPoint Label="2" Value="32">
                                                <pe elementtype="None" />
                                            </igchartprop:NumericDataPoint>
                                            <igchartprop:NumericDataPoint Label="3" Value="20">
                                                <pe elementtype="None" />
                                            </igchartprop:NumericDataPoint>
                                            <igchartprop:NumericDataPoint Label="4" Value="12">
                                                <pe elementtype="None" />
                                            </igchartprop:NumericDataPoint>
                                            <igchartprop:NumericDataPoint Label="5" Value="24">
                                                <pe elementtype="None" />
                                            </igchartprop:NumericDataPoint>
                                            <igchartprop:NumericDataPoint Label="6" Value="20">
                                                <pe elementtype="None" />
                                            </igchartprop:NumericDataPoint>
                                            <igchartprop:NumericDataPoint Label="7" Value="21">
                                                <pe elementtype="None" />
                                            </igchartprop:NumericDataPoint>
                                            <igchartprop:NumericDataPoint Label="8" Value="25">
                                                <pe elementtype="None" />
                                            </igchartprop:NumericDataPoint>
                                            <igchartprop:NumericDataPoint Label="9" Value="30">
                                                <pe elementtype="None" />
                                            </igchartprop:NumericDataPoint>
                                            <igchartprop:NumericDataPoint Label="10" Value="29">
                                                <pe elementtype="None" />
                                            </igchartprop:NumericDataPoint>
                                            <igchartprop:NumericDataPoint Label="11" Value="24">
                                                <pe elementtype="None" />
                                            </igchartprop:NumericDataPoint>
                                            <igchartprop:NumericDataPoint Label="12" Value="18">
                                                <pe elementtype="None" />
                                            </igchartprop:NumericDataPoint>
                                            <igchartprop:NumericDataPoint Label="13" Value="16">
                                                <pe elementtype="None" />
                                            </igchartprop:NumericDataPoint>
                                            <igchartprop:NumericDataPoint Label="14" Value="27">
                                                <pe elementtype="None" />
                                            </igchartprop:NumericDataPoint>
                                            <igchartprop:NumericDataPoint Label="15" Value="23">
                                                <pe elementtype="None" />
                                            </igchartprop:NumericDataPoint>
                                            <igchartprop:NumericDataPoint Label="16" Value="28">
                                                <pe elementtype="None" />
                                            </igchartprop:NumericDataPoint>
                                            <igchartprop:NumericDataPoint Label="17" Value="22">
                                                <pe elementtype="None" />
                                            </igchartprop:NumericDataPoint>
                                            <igchartprop:NumericDataPoint Label="18" Value="31">
                                                <pe elementtype="None" />
                                            </igchartprop:NumericDataPoint>
                                            <igchartprop:NumericDataPoint Label="19" Value="23">
                                                <pe elementtype="None" />
                                            </igchartprop:NumericDataPoint>
                                            <igchartprop:NumericDataPoint Label="20" Value="24">
                                                <pe elementtype="None" />
                                            </igchartprop:NumericDataPoint>
                                            <igchartprop:NumericDataPoint Label="21" Value="25">
                                                <pe elementtype="None" />
                                            </igchartprop:NumericDataPoint>
                                            <igchartprop:NumericDataPoint Label="22" Value="30">
                                                <pe elementtype="None" />
                                            </igchartprop:NumericDataPoint>
                                            <igchartprop:NumericDataPoint Label="23" Value="14">
                                                <pe elementtype="None" />
                                            </igchartprop:NumericDataPoint>
                                            <igchartprop:NumericDataPoint Label="24" Value="36">
                                                <pe elementtype="None" />
                                            </igchartprop:NumericDataPoint>
                                            <igchartprop:NumericDataPoint Label="25" Value="30">
                                                <pe elementtype="None" />
                                            </igchartprop:NumericDataPoint>
                                            <igchartprop:NumericDataPoint Label="26" Value="24">
                                                <pe elementtype="None" />
                                            </igchartprop:NumericDataPoint>
                                            <igchartprop:NumericDataPoint Label="27" Value="26">
                                                <pe elementtype="None" />
                                            </igchartprop:NumericDataPoint>
                                            <igchartprop:NumericDataPoint Label="28" Value="20">
                                                <pe elementtype="None" />
                                            </igchartprop:NumericDataPoint>
                                            <igchartprop:NumericDataPoint Label="29" Value="30">
                                                <pe elementtype="None" />
                                            </igchartprop:NumericDataPoint>
                                            <igchartprop:NumericDataPoint Label="30" Value="21">
                                                <pe elementtype="None" />
                                            </igchartprop:NumericDataPoint>
                                            <igchartprop:NumericDataPoint Label="31" Value="22">
                                                <pe elementtype="None" />
                                            </igchartprop:NumericDataPoint>
                                        </points>
                                        <pes>
                                            <igchartprop:PaintElement Fill="Red" />
                                        </pes>
                                    </igchartprop:NumericSeries>
                                    <igchartprop:NumericSeries Key="series4" Label="Prueba3">
                                        <points>
                                            <igchartprop:NumericDataPoint Label="Enero">
                                                <pe elementtype="None" />
                                            </igchartprop:NumericDataPoint>
                                            <igchartprop:NumericDataPoint Label="Febrero">
                                                <pe elementtype="None" />
                                            </igchartprop:NumericDataPoint>
                                            <igchartprop:NumericDataPoint Label="Marzo">
                                                <pe elementtype="None" />
                                            </igchartprop:NumericDataPoint>
                                            <igchartprop:NumericDataPoint Label="1" Value="20">
                                                <pe elementtype="None" />
                                            </igchartprop:NumericDataPoint>
                                            <igchartprop:NumericDataPoint Label="2" Value="20">
                                                <pe elementtype="None" />
                                            </igchartprop:NumericDataPoint>
                                            <igchartprop:NumericDataPoint Label="3" Value="20">
                                                <pe elementtype="None" />
                                            </igchartprop:NumericDataPoint>
                                            <igchartprop:NumericDataPoint Label="4" Value="20">
                                                <pe elementtype="None" />
                                            </igchartprop:NumericDataPoint>
                                            <igchartprop:NumericDataPoint Label="5" Value="20">
                                                <pe elementtype="None" />
                                            </igchartprop:NumericDataPoint>
                                            <igchartprop:NumericDataPoint Label="6" Value="20">
                                                <pe elementtype="None" />
                                            </igchartprop:NumericDataPoint>
                                            <igchartprop:NumericDataPoint Label="7" Value="20">
                                                <pe elementtype="None" />
                                            </igchartprop:NumericDataPoint>
                                            <igchartprop:NumericDataPoint Label="8" Value="20">
                                                <pe elementtype="None" />
                                            </igchartprop:NumericDataPoint>
                                            <igchartprop:NumericDataPoint Label="9" Value="20">
                                                <pe elementtype="None" />
                                            </igchartprop:NumericDataPoint>
                                            <igchartprop:NumericDataPoint Label="10" Value="20">
                                                <pe elementtype="None" />
                                            </igchartprop:NumericDataPoint>
                                            <igchartprop:NumericDataPoint Label="11" Value="20">
                                                <pe elementtype="None" />
                                            </igchartprop:NumericDataPoint>
                                            <igchartprop:NumericDataPoint Label="12" Value="20">
                                                <pe elementtype="None" />
                                            </igchartprop:NumericDataPoint>
                                            <igchartprop:NumericDataPoint Label="13" Value="20">
                                                <pe elementtype="None" />
                                            </igchartprop:NumericDataPoint>
                                            <igchartprop:NumericDataPoint Label="14" Value="20">
                                                <pe elementtype="None" />
                                            </igchartprop:NumericDataPoint>
                                            <igchartprop:NumericDataPoint Label="15" Value="20">
                                                <pe elementtype="None" />
                                            </igchartprop:NumericDataPoint>
                                            <igchartprop:NumericDataPoint Label="16" Value="20">
                                                <pe elementtype="None" />
                                            </igchartprop:NumericDataPoint>
                                            <igchartprop:NumericDataPoint Label="17" Value="20">
                                                <pe elementtype="None" />
                                            </igchartprop:NumericDataPoint>
                                            <igchartprop:NumericDataPoint Label="18" Value="20">
                                                <pe elementtype="None" />
                                            </igchartprop:NumericDataPoint>
                                            <igchartprop:NumericDataPoint Label="19" Value="20">
                                                <pe elementtype="None" />
                                            </igchartprop:NumericDataPoint>
                                            <igchartprop:NumericDataPoint Label="20" Value="20">
                                                <pe elementtype="None" />
                                            </igchartprop:NumericDataPoint>
                                            <igchartprop:NumericDataPoint Label="21" Value="20">
                                                <pe elementtype="None" />
                                            </igchartprop:NumericDataPoint>
                                            <igchartprop:NumericDataPoint Label="22" Value="20">
                                                <pe elementtype="None" />
                                            </igchartprop:NumericDataPoint>
                                            <igchartprop:NumericDataPoint Label="23" Value="20">
                                                <pe elementtype="None" />
                                            </igchartprop:NumericDataPoint>
                                            <igchartprop:NumericDataPoint Label="24" Value="20">
                                                <pe elementtype="None" />
                                            </igchartprop:NumericDataPoint>
                                            <igchartprop:NumericDataPoint Label="25" Value="20">
                                                <pe elementtype="None" />
                                            </igchartprop:NumericDataPoint>
                                            <igchartprop:NumericDataPoint Label="26" Value="20">
                                                <pe elementtype="None" />
                                            </igchartprop:NumericDataPoint>
                                            <igchartprop:NumericDataPoint Label="27" Value="20">
                                                <pe elementtype="None" />
                                            </igchartprop:NumericDataPoint>
                                            <igchartprop:NumericDataPoint Label="28" Value="20">
                                                <pe elementtype="None" />
                                            </igchartprop:NumericDataPoint>
                                            <igchartprop:NumericDataPoint Label="29" Value="20">
                                                <pe elementtype="None" />
                                            </igchartprop:NumericDataPoint>
                                            <igchartprop:NumericDataPoint Label="30" Value="20">
                                                <pe elementtype="None" />
                                            </igchartprop:NumericDataPoint>
                                            <igchartprop:NumericDataPoint Label="31" Value="20">
                                                <pe elementtype="None" />
                                            </igchartprop:NumericDataPoint>
                                        </points>
                                        <pes>
                                            <igchartprop:PaintElement Fill="Blue" />
                                        </pes>
                                    </igchartprop:NumericSeries>
                                </Series>
                                <ChartLayers>
                                    <igchartprop:ChartLayerAppearance AxisXKey="axis1" AxisYKey="axis2" 
                                        ChartAreaKey="area1" ChartType="ColumnChart" Key="chartLayer1" 
                                        SeriesList="series1">
                                        <ChartTypeAppearances>
                                            <igchartprop:ColumnChartAppearance NullHandling="DontPlot" 
                                                ChartTextValueAlignment="PositiveAndNegative">
                                                <ChartText>
                                                    <igchartprop:ChartTextAppearance ChartTextFont="Arial, 7pt" Column="0" 
                                                        ItemFormatString="&lt;DATA_VALUE:00&gt;" Row="0" />
                                                </ChartText>
                                            </igchartprop:ColumnChartAppearance>
                                        </ChartTypeAppearances>
                                    </igchartprop:ChartLayerAppearance>
                                    <igchartprop:ChartLayerAppearance AxisXKey="axis3" AxisYKey="axis2" 
                                        ChartAreaKey="area1" ChartType="LineChart" Key="chartLayer2" 
                                        SeriesList="series3">
                                        <ChartTypeAppearances>
                                            <igchartprop:LineChartAppearance HighLightLines="True" MidPointAnchors="False" 
                                                NullHandling="DontPlot" Thickness="4">
                                                <ChartText>
                                                    <igchartprop:ChartTextAppearance ChartTextFont="Arial, 7pt" Column="0" 
                                                        ItemFormatString="&lt;DATA_VALUE:00&gt;" Row="0" />
                                                </ChartText>
                                                <LineAppearances>
                                                    <igchartprop:LineAppearance>
                                                        <IconAppearance CharacterFont="Arial, 8.25pt" Icon="Circle">
                                                            <PE ElementType="None" />
                                                        </IconAppearance>
                                                    </igchartprop:LineAppearance>
                                                </LineAppearances>
                                            </igchartprop:LineChartAppearance>
                                        </ChartTypeAppearances>
                                    </igchartprop:ChartLayerAppearance>
                                    <igchartprop:ChartLayerAppearance AxisXKey="axis3" AxisYKey="axis2" 
                                        ChartAreaKey="area1" ChartType="LineChart" Key="chartLayer3" 
                                        SeriesList="series4">
                                        <ChartTypeAppearances>
                                            <igchartprop:LineChartAppearance>
                                                <ChartText>
                                                    <igchartprop:ChartTextAppearance ChartTextFont="Arial, 7pt" Column="0" 
                                                        ItemFormatString="&lt;DATA_VALUE:00&gt;" Row="0" />
                                                </ChartText>
                                                <LineAppearances>
                                                    <igchartprop:LineAppearance>
                                                        <IconAppearance CharacterFont="Arial Narrow, 6.75pt">
                                                            <PE ElementType="None" StrokeWidth="2" />
                                                        </IconAppearance>
                                                    </igchartprop:LineAppearance>
                                                </LineAppearances>
                                            </igchartprop:LineChartAppearance>
                                        </ChartTypeAppearances>
                                    </igchartprop:ChartLayerAppearance>
                                </ChartLayers>
                                <ChartAreas>
                                    <igchartprop:ChartArea Bounds="0, 0, 100, 100" BoundsMeasureType="Percentage" 
                                        Key="area1">
                                        <Axes>
                                            <igchartprop:AxisItem DataType="String" Extent="20" Key="axis1" 
                                                OrientationType="X_Axis" SetLabelAxisType="GroupBySeries" TickmarkInterval="1">
                                                <MajorGridLines AlphaLevel="255" Color="Gainsboro" DrawStyle="Dot" 
                                                    Thickness="1" Visible="True" />
                                                <MinorGridLines AlphaLevel="255" Color="LightGray" DrawStyle="Dot" 
                                                    Thickness="1" Visible="False" />
                                                <Labels HorizontalAlign="Near" ItemFormatString="" Orientation="Horizontal" 
                                                    VerticalAlign="Center" Font="Arial, 6.75pt" Visible="False">
                                                    <SeriesLabels HorizontalAlign="Center" Orientation="Horizontal" 
                                                        VerticalAlign="Center" Visible="False">
                                                    </SeriesLabels>
                                                </Labels>
                                            </igchartprop:AxisItem>
                                            <igchartprop:AxisItem DataType="Numeric" Extent="20" Key="axis2" 
                                                OrientationType="Y_Axis" SetLabelAxisType="GroupBySeries" TickmarkInterval="0">
                                                <MajorGridLines AlphaLevel="255" Color="Gainsboro" DrawStyle="Dot" 
                                                    Thickness="1" Visible="True" />
                                                <MinorGridLines AlphaLevel="255" Color="LightGray" DrawStyle="Dot" 
                                                    Thickness="1" Visible="False" />
                                                <Labels HorizontalAlign="Near" ItemFormatString="&lt;DATA_VALUE:00&gt;" Orientation="Horizontal" 
                                                    VerticalAlign="Center" Flip="True" Font="Arial, 8.25pt">
                                                    <SeriesLabels HorizontalAlign="Center" Orientation="Horizontal" 
                                                        VerticalAlign="Center">
                                                    </SeriesLabels>
                                                </Labels>
                                            </igchartprop:AxisItem>
                                            <igchartprop:AxisItem DataType="String" Extent="60" Key="axis3" 
                                                OrientationType="X_Axis" SetLabelAxisType="ContinuousData" TickmarkInterval="0">
                                                <MajorGridLines AlphaLevel="255" Color="Gainsboro" DrawStyle="Dot" 
                                                    Thickness="1" Visible="True" />
                                                <MinorGridLines AlphaLevel="255" Color="LightGray" DrawStyle="Dot" 
                                                    Thickness="1" Visible="False" />
                                                <Labels HorizontalAlign="Near" ItemFormatString="&lt;ITEM_LABEL&gt;" Orientation="VerticalLeftFacing" 
                                                    VerticalAlign="Center" Flip="True" Font="Arial, 6.75pt">
                                                    <SeriesLabels HorizontalAlign="Center" Orientation="Horizontal" 
                                                        VerticalAlign="Center">
                                                    </SeriesLabels>
                                                </Labels>
                                            </igchartprop:AxisItem>
                                        </Axes>
                                        <PE Texture="Blocks" />
                                        <GridPE ElementType="None" />
                                    </igchartprop:ChartArea>
                                </ChartAreas>
                                <Legends>
                                    <igchartprop:CompositeLegend BoundsMeasureType="Percentage">
                                    </igchartprop:CompositeLegend>
                                </Legends>
                            </CompositeChart>
                            <Axis>
                                <PE ElementType="None" Fill="Cornsilk" />
                                <X LineThickness="1" TickmarkInterval="0" TickmarkStyle="Smart" Visible="True">
                                    <MajorGridLines AlphaLevel="255" Color="Gainsboro" DrawStyle="Dot" 
                                        Thickness="1" Visible="True" />
                                    <MinorGridLines AlphaLevel="255" Color="LightGray" DrawStyle="Dot" 
                                        Thickness="1" Visible="False" />
                                    <Labels Font="Verdana, 7pt" FontColor="DimGray" HorizontalAlign="Near" 
                                        ItemFormatString="&lt;ITEM_LABEL&gt;" Orientation="VerticalLeftFacing" 
                                        VerticalAlign="Center">
                                        <SeriesLabels Font="Verdana, 7pt" FontColor="DimGray" FormatString="" 
                                            HorizontalAlign="Near" Orientation="VerticalLeftFacing" VerticalAlign="Center">
                                            <Layout Behavior="Auto">
                                            </Layout>
                                        </SeriesLabels>
                                        <Layout Behavior="Auto">
                                        </Layout>
                                    </Labels>
                                </X>
                                <Y LineThickness="1" TickmarkInterval="0" TickmarkStyle="Smart" Visible="True">
                                    <MajorGridLines AlphaLevel="255" Color="Gainsboro" DrawStyle="Dot" 
                                        Thickness="1" Visible="True" />
                                    <MinorGridLines AlphaLevel="255" Color="LightGray" DrawStyle="Dot" 
                                        Thickness="1" Visible="False" />
                                    <Labels Font="Verdana, 7pt" FontColor="DimGray" HorizontalAlign="Far" 
                                        ItemFormatString="&lt;DATA_VALUE:00.##&gt;" Orientation="Horizontal" 
                                        VerticalAlign="Center">
                                        <SeriesLabels Font="Verdana, 7pt" FontColor="DimGray" FormatString="" 
                                            HorizontalAlign="Far" Orientation="Horizontal" VerticalAlign="Center">
                                            <Layout Behavior="Auto">
                                            </Layout>
                                        </SeriesLabels>
                                        <Layout Behavior="Auto">
                                        </Layout>
                                    </Labels>
                                </Y>
                                <Y2 LineThickness="1" TickmarkInterval="0" TickmarkStyle="Smart" 
                                    Visible="False">
                                    <MajorGridLines AlphaLevel="255" Color="Gainsboro" DrawStyle="Dot" 
                                        Thickness="1" Visible="True" />
                                    <MinorGridLines AlphaLevel="255" Color="LightGray" DrawStyle="Dot" 
                                        Thickness="1" Visible="False" />
                                    <Labels Font="Verdana, 7pt" FontColor="Gray" HorizontalAlign="Near" 
                                        ItemFormatString="&lt;DATA_VALUE:00.##&gt;" Orientation="Horizontal" 
                                        VerticalAlign="Center" Visible="False">
                                        <SeriesLabels Font="Verdana, 7pt" FontColor="Gray" FormatString="" 
                                            HorizontalAlign="Near" Orientation="Horizontal" VerticalAlign="Center">
                                            <Layout Behavior="Auto">
                                            </Layout>
                                        </SeriesLabels>
                                        <Layout Behavior="Auto">
                                        </Layout>
                                    </Labels>
                                </Y2>
                                <X2 LineThickness="1" TickmarkInterval="0" TickmarkStyle="Smart" 
                                    Visible="False">
                                    <MajorGridLines AlphaLevel="255" Color="Gainsboro" DrawStyle="Dot" 
                                        Thickness="1" Visible="True" />
                                    <MinorGridLines AlphaLevel="255" Color="LightGray" DrawStyle="Dot" 
                                        Thickness="1" Visible="False" />
                                    <Labels Font="Verdana, 7pt" FontColor="Gray" HorizontalAlign="Far" 
                                        ItemFormatString="&lt;ITEM_LABEL&gt;" Orientation="VerticalLeftFacing" 
                                        VerticalAlign="Center" Visible="False">
                                        <SeriesLabels Font="Verdana, 7pt" FontColor="Gray" FormatString="" 
                                            HorizontalAlign="Far" Orientation="VerticalLeftFacing" VerticalAlign="Center">
                                            <Layout Behavior="Auto">
                                            </Layout>
                                        </SeriesLabels>
                                        <Layout Behavior="Auto">
                                        </Layout>
                                    </Labels>
                                </X2>
                                <Z LineThickness="1" TickmarkInterval="0" TickmarkStyle="Smart" Visible="False">
                                    <MajorGridLines AlphaLevel="255" Color="Gainsboro" DrawStyle="Dot" 
                                        Thickness="1" Visible="True" />
                                    <MinorGridLines AlphaLevel="255" Color="LightGray" DrawStyle="Dot" 
                                        Thickness="1" Visible="False" />
                                    <Labels Font="Verdana, 7pt" FontColor="DimGray" HorizontalAlign="Near" 
                                        ItemFormatString="" Orientation="Horizontal" VerticalAlign="Center">
                                        <SeriesLabels Font="Verdana, 7pt" FontColor="DimGray" HorizontalAlign="Near" 
                                            Orientation="Horizontal" VerticalAlign="Center">
                                            <Layout Behavior="Auto">
                                            </Layout>
                                        </SeriesLabels>
                                        <Layout Behavior="Auto">
                                        </Layout>
                                    </Labels>
                                </Z>
                                <Z2 LineThickness="1" TickmarkInterval="0" TickmarkStyle="Smart" 
                                    Visible="False">
                                    <MajorGridLines AlphaLevel="255" Color="Gainsboro" DrawStyle="Dot" 
                                        Thickness="1" Visible="True" />
                                    <MinorGridLines AlphaLevel="255" Color="LightGray" DrawStyle="Dot" 
                                        Thickness="1" Visible="False" />
                                    <Labels Font="Verdana, 7pt" FontColor="Gray" HorizontalAlign="Near" 
                                        ItemFormatString="" Orientation="Horizontal" VerticalAlign="Center" 
                                        Visible="False">
                                        <SeriesLabels Font="Verdana, 7pt" FontColor="Gray" HorizontalAlign="Near" 
                                            Orientation="Horizontal" VerticalAlign="Center">
                                            <Layout Behavior="Auto">
                                            </Layout>
                                        </SeriesLabels>
                                        <Layout Behavior="Auto">
                                        </Layout>
                                    </Labels>
                                </Z2>
                            </Axis>
                            <Effects>
                                <Effects>
                                    <igchartprop:GradientEffect />
                                </Effects>
                            </Effects>
                                    <ColorModel AlphaLevel="150" ColorBegin="Pink" ColorEnd="DarkRed" 
                                        ModelStyle="CustomLinear">
                                    </ColorModel>
                                    <TitleBottom Extent="33" Font="Arial, 8.25pt" Location="Bottom">
                                    </TitleBottom>
                            <Border Thickness="0" />
                            <Tooltips Font-Bold="False" Font-Italic="False" Font-Overline="False" 
                                Font-Strikeout="False" Font-Underline="False" />
                        </igchart:UltraChart>
                    </Template>
                </igmisc:WebPanel>
            </td>
        </tr>
         <tr>
            <td valign="top" style="width:500px;"  align="left">
                <igmisc:WebPanel ID="WebPanel5" runat="server" EnableAppStyling="True" Width="500px" 
                    StyleSetName="">
                    <Header Text ="% Calidad A segundo fuego" >
                    </Header>
                    <Template>
                        <igchart:UltraChart ID="UltraChart5" runat="server" BackgroundImageFileName="" 
                            BorderColor="Black" BorderWidth="0px" ChartType="Composite" 
                            EmptyChartText="Data Not Available. Please call UltraChart.Data.DataBind() after setting valid Data.DataSource" 
                            Version="9.1" onchartdataclicked="UltraChart1_ChartDataClicked" Width="500px" 
                                    Height="206px">
                                    <Legend BackgroundColor="White" BorderColor="Black" 
                                        DataAssociation="ColumnData" Font="Arial, 8.25pt" Visible="True"></Legend>
                            <CompositeChart>
                                <Series>
                                    <igchartprop:NumericSeries Key="series1" Label="Prueba 1">
                                        <points>
                                            <igchartprop:NumericDataPoint Label="Enero" Value="26">
                                                <pe elementtype="None" />
                                            </igchartprop:NumericDataPoint>
                                            <igchartprop:NumericDataPoint Label="Febrero" Value="32">
                                                <pe elementtype="None" />
                                            </igchartprop:NumericDataPoint>
                                            <igchartprop:NumericDataPoint Label="Marzo" Value="28">
                                                <pe elementtype="None" />
                                            </igchartprop:NumericDataPoint>
                                            <igchartprop:NumericDataPoint Label="1">
                                                <pe elementtype="None" />
                                            </igchartprop:NumericDataPoint>
                                            <igchartprop:NumericDataPoint Label="2">
                                                <pe elementtype="None" />
                                            </igchartprop:NumericDataPoint>
                                            <igchartprop:NumericDataPoint Label="3">
                                                <pe elementtype="None" />
                                            </igchartprop:NumericDataPoint>
                                            <igchartprop:NumericDataPoint Label="4">
                                                <pe elementtype="None" />
                                            </igchartprop:NumericDataPoint>
                                            <igchartprop:NumericDataPoint Label="5">
                                                <pe elementtype="None" />
                                            </igchartprop:NumericDataPoint>
                                            <igchartprop:NumericDataPoint Label="6">
                                                <pe elementtype="None" />
                                            </igchartprop:NumericDataPoint>
                                            <igchartprop:NumericDataPoint Label="7">
                                                <pe elementtype="None" />
                                            </igchartprop:NumericDataPoint>
                                            <igchartprop:NumericDataPoint Label="8">
                                                <pe elementtype="None" />
                                            </igchartprop:NumericDataPoint>
                                            <igchartprop:NumericDataPoint Label="9">
                                                <pe elementtype="None" />
                                            </igchartprop:NumericDataPoint>
                                            <igchartprop:NumericDataPoint Label="10">
                                                <pe elementtype="None" />
                                            </igchartprop:NumericDataPoint>
                                            <igchartprop:NumericDataPoint Label="11">
                                                <pe elementtype="None" />
                                            </igchartprop:NumericDataPoint>
                                            <igchartprop:NumericDataPoint Label="12">
                                                <pe elementtype="None" />
                                            </igchartprop:NumericDataPoint>
                                            <igchartprop:NumericDataPoint Label="13">
                                                <pe elementtype="None" />
                                            </igchartprop:NumericDataPoint>
                                            <igchartprop:NumericDataPoint Label="14">
                                                <pe elementtype="None" />
                                            </igchartprop:NumericDataPoint>
                                            <igchartprop:NumericDataPoint Label="15">
                                                <pe elementtype="None" />
                                            </igchartprop:NumericDataPoint>
                                            <igchartprop:NumericDataPoint Label="16">
                                                <pe elementtype="None" />
                                            </igchartprop:NumericDataPoint>
                                            <igchartprop:NumericDataPoint Label="17">
                                                <pe elementtype="None" />
                                            </igchartprop:NumericDataPoint>
                                            <igchartprop:NumericDataPoint Label="18">
                                                <pe elementtype="None" />
                                            </igchartprop:NumericDataPoint>
                                            <igchartprop:NumericDataPoint Label="19">
                                                <pe elementtype="None" />
                                            </igchartprop:NumericDataPoint>
                                            <igchartprop:NumericDataPoint Label="20">
                                                <pe elementtype="None" />
                                            </igchartprop:NumericDataPoint>
                                            <igchartprop:NumericDataPoint Label="21">
                                                <pe elementtype="None" />
                                            </igchartprop:NumericDataPoint>
                                            <igchartprop:NumericDataPoint Label="22">
                                                <pe elementtype="None" />
                                            </igchartprop:NumericDataPoint>
                                            <igchartprop:NumericDataPoint Label="23">
                                                <pe elementtype="None" />
                                            </igchartprop:NumericDataPoint>
                                            <igchartprop:NumericDataPoint Label="24">
                                                <pe elementtype="None" />
                                            </igchartprop:NumericDataPoint>
                                            <igchartprop:NumericDataPoint Label="25">
                                                <pe elementtype="None" />
                                            </igchartprop:NumericDataPoint>
                                            <igchartprop:NumericDataPoint Label="26">
                                                <pe elementtype="None" />
                                            </igchartprop:NumericDataPoint>
                                            <igchartprop:NumericDataPoint Label="27">
                                                <pe elementtype="None" />
                                            </igchartprop:NumericDataPoint>
                                            <igchartprop:NumericDataPoint Label="28">
                                                <pe elementtype="None" />
                                            </igchartprop:NumericDataPoint>
                                            <igchartprop:NumericDataPoint Label="29">
                                                <pe elementtype="None" />
                                            </igchartprop:NumericDataPoint>
                                            <igchartprop:NumericDataPoint Label="30">
                                                <pe elementtype="None" />
                                            </igchartprop:NumericDataPoint>
                                            <igchartprop:NumericDataPoint Label="31">
                                                <pe elementtype="None" />
                                            </igchartprop:NumericDataPoint>
                                        </points>
                                    </igchartprop:NumericSeries>
                                    <igchartprop:NumericSeries Key="series3" Label="Prueba2">
                                        <points>
                                            <igchartprop:NumericDataPoint Label="Enero">
                                                <pe elementtype="None" />
                                            </igchartprop:NumericDataPoint>
                                            <igchartprop:NumericDataPoint Label="Febrero">
                                                <pe elementtype="None" />
                                            </igchartprop:NumericDataPoint>
                                            <igchartprop:NumericDataPoint Label="Marzo">
                                                <pe elementtype="None" />
                                            </igchartprop:NumericDataPoint>
                                            <igchartprop:NumericDataPoint Label="1" Value="25">
                                                <pe elementtype="None" />
                                            </igchartprop:NumericDataPoint>
                                            <igchartprop:NumericDataPoint Label="2" Value="32">
                                                <pe elementtype="None" />
                                            </igchartprop:NumericDataPoint>
                                            <igchartprop:NumericDataPoint Label="3" Value="20">
                                                <pe elementtype="None" />
                                            </igchartprop:NumericDataPoint>
                                            <igchartprop:NumericDataPoint Label="4" Value="12">
                                                <pe elementtype="None" />
                                            </igchartprop:NumericDataPoint>
                                            <igchartprop:NumericDataPoint Label="5" Value="24">
                                                <pe elementtype="None" />
                                            </igchartprop:NumericDataPoint>
                                            <igchartprop:NumericDataPoint Label="6" Value="20">
                                                <pe elementtype="None" />
                                            </igchartprop:NumericDataPoint>
                                            <igchartprop:NumericDataPoint Label="7" Value="21">
                                                <pe elementtype="None" />
                                            </igchartprop:NumericDataPoint>
                                            <igchartprop:NumericDataPoint Label="8" Value="25">
                                                <pe elementtype="None" />
                                            </igchartprop:NumericDataPoint>
                                            <igchartprop:NumericDataPoint Label="9" Value="30">
                                                <pe elementtype="None" />
                                            </igchartprop:NumericDataPoint>
                                            <igchartprop:NumericDataPoint Label="10" Value="29">
                                                <pe elementtype="None" />
                                            </igchartprop:NumericDataPoint>
                                            <igchartprop:NumericDataPoint Label="11" Value="24">
                                                <pe elementtype="None" />
                                            </igchartprop:NumericDataPoint>
                                            <igchartprop:NumericDataPoint Label="12" Value="18">
                                                <pe elementtype="None" />
                                            </igchartprop:NumericDataPoint>
                                            <igchartprop:NumericDataPoint Label="13" Value="16">
                                                <pe elementtype="None" />
                                            </igchartprop:NumericDataPoint>
                                            <igchartprop:NumericDataPoint Label="14" Value="27">
                                                <pe elementtype="None" />
                                            </igchartprop:NumericDataPoint>
                                            <igchartprop:NumericDataPoint Label="15" Value="23">
                                                <pe elementtype="None" />
                                            </igchartprop:NumericDataPoint>
                                            <igchartprop:NumericDataPoint Label="16" Value="28">
                                                <pe elementtype="None" />
                                            </igchartprop:NumericDataPoint>
                                            <igchartprop:NumericDataPoint Label="17" Value="22">
                                                <pe elementtype="None" />
                                            </igchartprop:NumericDataPoint>
                                            <igchartprop:NumericDataPoint Label="18" Value="31">
                                                <pe elementtype="None" />
                                            </igchartprop:NumericDataPoint>
                                            <igchartprop:NumericDataPoint Label="19" Value="23">
                                                <pe elementtype="None" />
                                            </igchartprop:NumericDataPoint>
                                            <igchartprop:NumericDataPoint Label="20" Value="24">
                                                <pe elementtype="None" />
                                            </igchartprop:NumericDataPoint>
                                            <igchartprop:NumericDataPoint Label="21" Value="25">
                                                <pe elementtype="None" />
                                            </igchartprop:NumericDataPoint>
                                            <igchartprop:NumericDataPoint Label="22" Value="30">
                                                <pe elementtype="None" />
                                            </igchartprop:NumericDataPoint>
                                            <igchartprop:NumericDataPoint Label="23" Value="14">
                                                <pe elementtype="None" />
                                            </igchartprop:NumericDataPoint>
                                            <igchartprop:NumericDataPoint Label="24" Value="36">
                                                <pe elementtype="None" />
                                            </igchartprop:NumericDataPoint>
                                            <igchartprop:NumericDataPoint Label="25" Value="30">
                                                <pe elementtype="None" />
                                            </igchartprop:NumericDataPoint>
                                            <igchartprop:NumericDataPoint Label="26" Value="24">
                                                <pe elementtype="None" />
                                            </igchartprop:NumericDataPoint>
                                            <igchartprop:NumericDataPoint Label="27" Value="26">
                                                <pe elementtype="None" />
                                            </igchartprop:NumericDataPoint>
                                            <igchartprop:NumericDataPoint Label="28" Value="20">
                                                <pe elementtype="None" />
                                            </igchartprop:NumericDataPoint>
                                            <igchartprop:NumericDataPoint Label="29" Value="30">
                                                <pe elementtype="None" />
                                            </igchartprop:NumericDataPoint>
                                            <igchartprop:NumericDataPoint Label="30" Value="21">
                                                <pe elementtype="None" />
                                            </igchartprop:NumericDataPoint>
                                            <igchartprop:NumericDataPoint Label="31" Value="22">
                                                <pe elementtype="None" />
                                            </igchartprop:NumericDataPoint>
                                        </points>
                                        <pes>
                                            <igchartprop:PaintElement Fill="Red" />
                                        </pes>
                                    </igchartprop:NumericSeries>
                                    <igchartprop:NumericSeries Key="series4" Label="Prueba3">
                                        <points>
                                            <igchartprop:NumericDataPoint Label="Enero">
                                                <pe elementtype="None" />
                                            </igchartprop:NumericDataPoint>
                                            <igchartprop:NumericDataPoint Label="Febrero">
                                                <pe elementtype="None" />
                                            </igchartprop:NumericDataPoint>
                                            <igchartprop:NumericDataPoint Label="Marzo">
                                                <pe elementtype="None" />
                                            </igchartprop:NumericDataPoint>
                                            <igchartprop:NumericDataPoint Label="1" Value="20">
                                                <pe elementtype="None" />
                                            </igchartprop:NumericDataPoint>
                                            <igchartprop:NumericDataPoint Label="2" Value="20">
                                                <pe elementtype="None" />
                                            </igchartprop:NumericDataPoint>
                                            <igchartprop:NumericDataPoint Label="3" Value="20">
                                                <pe elementtype="None" />
                                            </igchartprop:NumericDataPoint>
                                            <igchartprop:NumericDataPoint Label="4" Value="20">
                                                <pe elementtype="None" />
                                            </igchartprop:NumericDataPoint>
                                            <igchartprop:NumericDataPoint Label="5" Value="20">
                                                <pe elementtype="None" />
                                            </igchartprop:NumericDataPoint>
                                            <igchartprop:NumericDataPoint Label="6" Value="20">
                                                <pe elementtype="None" />
                                            </igchartprop:NumericDataPoint>
                                            <igchartprop:NumericDataPoint Label="7" Value="20">
                                                <pe elementtype="None" />
                                            </igchartprop:NumericDataPoint>
                                            <igchartprop:NumericDataPoint Label="8" Value="20">
                                                <pe elementtype="None" />
                                            </igchartprop:NumericDataPoint>
                                            <igchartprop:NumericDataPoint Label="9" Value="20">
                                                <pe elementtype="None" />
                                            </igchartprop:NumericDataPoint>
                                            <igchartprop:NumericDataPoint Label="10" Value="20">
                                                <pe elementtype="None" />
                                            </igchartprop:NumericDataPoint>
                                            <igchartprop:NumericDataPoint Label="11" Value="20">
                                                <pe elementtype="None" />
                                            </igchartprop:NumericDataPoint>
                                            <igchartprop:NumericDataPoint Label="12" Value="20">
                                                <pe elementtype="None" />
                                            </igchartprop:NumericDataPoint>
                                            <igchartprop:NumericDataPoint Label="13" Value="20">
                                                <pe elementtype="None" />
                                            </igchartprop:NumericDataPoint>
                                            <igchartprop:NumericDataPoint Label="14" Value="20">
                                                <pe elementtype="None" />
                                            </igchartprop:NumericDataPoint>
                                            <igchartprop:NumericDataPoint Label="15" Value="20">
                                                <pe elementtype="None" />
                                            </igchartprop:NumericDataPoint>
                                            <igchartprop:NumericDataPoint Label="16" Value="20">
                                                <pe elementtype="None" />
                                            </igchartprop:NumericDataPoint>
                                            <igchartprop:NumericDataPoint Label="17" Value="20">
                                                <pe elementtype="None" />
                                            </igchartprop:NumericDataPoint>
                                            <igchartprop:NumericDataPoint Label="18" Value="20">
                                                <pe elementtype="None" />
                                            </igchartprop:NumericDataPoint>
                                            <igchartprop:NumericDataPoint Label="19" Value="20">
                                                <pe elementtype="None" />
                                            </igchartprop:NumericDataPoint>
                                            <igchartprop:NumericDataPoint Label="20" Value="20">
                                                <pe elementtype="None" />
                                            </igchartprop:NumericDataPoint>
                                            <igchartprop:NumericDataPoint Label="21" Value="20">
                                                <pe elementtype="None" />
                                            </igchartprop:NumericDataPoint>
                                            <igchartprop:NumericDataPoint Label="22" Value="20">
                                                <pe elementtype="None" />
                                            </igchartprop:NumericDataPoint>
                                            <igchartprop:NumericDataPoint Label="23" Value="20">
                                                <pe elementtype="None" />
                                            </igchartprop:NumericDataPoint>
                                            <igchartprop:NumericDataPoint Label="24" Value="20">
                                                <pe elementtype="None" />
                                            </igchartprop:NumericDataPoint>
                                            <igchartprop:NumericDataPoint Label="25" Value="20">
                                                <pe elementtype="None" />
                                            </igchartprop:NumericDataPoint>
                                            <igchartprop:NumericDataPoint Label="26" Value="20">
                                                <pe elementtype="None" />
                                            </igchartprop:NumericDataPoint>
                                            <igchartprop:NumericDataPoint Label="27" Value="20">
                                                <pe elementtype="None" />
                                            </igchartprop:NumericDataPoint>
                                            <igchartprop:NumericDataPoint Label="28" Value="20">
                                                <pe elementtype="None" />
                                            </igchartprop:NumericDataPoint>
                                            <igchartprop:NumericDataPoint Label="29" Value="20">
                                                <pe elementtype="None" />
                                            </igchartprop:NumericDataPoint>
                                            <igchartprop:NumericDataPoint Label="30" Value="20">
                                                <pe elementtype="None" />
                                            </igchartprop:NumericDataPoint>
                                            <igchartprop:NumericDataPoint Label="31" Value="20">
                                                <pe elementtype="None" />
                                            </igchartprop:NumericDataPoint>
                                        </points>
                                        <pes>
                                            <igchartprop:PaintElement Fill="Blue" />
                                        </pes>
                                    </igchartprop:NumericSeries>
                                </Series>
                                <ChartLayers>
                                    <igchartprop:ChartLayerAppearance AxisXKey="axis1" AxisYKey="axis2" 
                                        ChartAreaKey="area1" ChartType="ColumnChart" Key="chartLayer1" 
                                        SeriesList="series1">
                                        <ChartTypeAppearances>
                                            <igchartprop:ColumnChartAppearance NullHandling="DontPlot" 
                                                ChartTextValueAlignment="PositiveAndNegative">
                                                <ChartText>
                                                    <igchartprop:ChartTextAppearance ChartTextFont="Arial, 7pt" Column="0" 
                                                        ItemFormatString="&lt;DATA_VALUE:00&gt;" Row="0" />
                                                </ChartText>
                                            </igchartprop:ColumnChartAppearance>
                                        </ChartTypeAppearances>
                                    </igchartprop:ChartLayerAppearance>
                                    <igchartprop:ChartLayerAppearance AxisXKey="axis3" AxisYKey="axis2" 
                                        ChartAreaKey="area1" ChartType="LineChart" Key="chartLayer2" 
                                        SeriesList="series3">
                                        <ChartTypeAppearances>
                                            <igchartprop:LineChartAppearance HighLightLines="True" MidPointAnchors="False" 
                                                NullHandling="DontPlot" Thickness="4">
                                                <ChartText>
                                                    <igchartprop:ChartTextAppearance ChartTextFont="Arial, 7pt" Column="0" 
                                                        ItemFormatString="&lt;DATA_VALUE:00&gt;" Row="0" />
                                                </ChartText>
                                                <LineAppearances>
                                                    <igchartprop:LineAppearance>
                                                        <IconAppearance CharacterFont="Arial, 8.25pt" Icon="Circle">
                                                            <PE ElementType="None" />
                                                        </IconAppearance>
                                                    </igchartprop:LineAppearance>
                                                </LineAppearances>
                                            </igchartprop:LineChartAppearance>
                                        </ChartTypeAppearances>
                                    </igchartprop:ChartLayerAppearance>
                                    <igchartprop:ChartLayerAppearance AxisXKey="axis3" AxisYKey="axis2" 
                                        ChartAreaKey="area1" ChartType="LineChart" Key="chartLayer3" 
                                        SeriesList="series4">
                                        <ChartTypeAppearances>
                                            <igchartprop:LineChartAppearance>
                                                <ChartText>
                                                    <igchartprop:ChartTextAppearance ChartTextFont="Arial, 7pt" Column="0" 
                                                        ItemFormatString="&lt;DATA_VALUE:00&gt;" Row="0" />
                                                </ChartText>
                                                <LineAppearances>
                                                    <igchartprop:LineAppearance>
                                                        <IconAppearance CharacterFont="Arial Narrow, 6.75pt">
                                                            <PE ElementType="None" StrokeWidth="2" />
                                                        </IconAppearance>
                                                    </igchartprop:LineAppearance>
                                                </LineAppearances>
                                            </igchartprop:LineChartAppearance>
                                        </ChartTypeAppearances>
                                    </igchartprop:ChartLayerAppearance>
                                </ChartLayers>
                                <ChartAreas>
                                    <igchartprop:ChartArea Bounds="0, 0, 100, 100" BoundsMeasureType="Percentage" 
                                        Key="area1">
                                        <Axes>
                                            <igchartprop:AxisItem DataType="String" Extent="20" Key="axis1" 
                                                OrientationType="X_Axis" SetLabelAxisType="GroupBySeries" TickmarkInterval="1">
                                                <MajorGridLines AlphaLevel="255" Color="Gainsboro" DrawStyle="Dot" 
                                                    Thickness="1" Visible="True" />
                                                <MinorGridLines AlphaLevel="255" Color="LightGray" DrawStyle="Dot" 
                                                    Thickness="1" Visible="False" />
                                                <Labels HorizontalAlign="Near" ItemFormatString="" Orientation="Horizontal" 
                                                    VerticalAlign="Center" Font="Arial, 6.75pt" Visible="False">
                                                    <SeriesLabels HorizontalAlign="Center" Orientation="Horizontal" 
                                                        VerticalAlign="Center" Visible="False">
                                                    </SeriesLabels>
                                                </Labels>
                                            </igchartprop:AxisItem>
                                            <igchartprop:AxisItem DataType="Numeric" Extent="20" Key="axis2" 
                                                OrientationType="Y_Axis" SetLabelAxisType="GroupBySeries" TickmarkInterval="0">
                                                <MajorGridLines AlphaLevel="255" Color="Gainsboro" DrawStyle="Dot" 
                                                    Thickness="1" Visible="True" />
                                                <MinorGridLines AlphaLevel="255" Color="LightGray" DrawStyle="Dot" 
                                                    Thickness="1" Visible="False" />
                                                <Labels HorizontalAlign="Near" ItemFormatString="&lt;DATA_VALUE:00&gt;" Orientation="Horizontal" 
                                                    VerticalAlign="Center" Flip="True" Font="Arial, 8.25pt">
                                                    <SeriesLabels HorizontalAlign="Center" Orientation="Horizontal" 
                                                        VerticalAlign="Center">
                                                    </SeriesLabels>
                                                </Labels>
                                            </igchartprop:AxisItem>
                                            <igchartprop:AxisItem DataType="String" Extent="60" Key="axis3" 
                                                OrientationType="X_Axis" SetLabelAxisType="ContinuousData" TickmarkInterval="0">
                                                <MajorGridLines AlphaLevel="255" Color="Gainsboro" DrawStyle="Dot" 
                                                    Thickness="1" Visible="True" />
                                                <MinorGridLines AlphaLevel="255" Color="LightGray" DrawStyle="Dot" 
                                                    Thickness="1" Visible="False" />
                                                <Labels HorizontalAlign="Near" ItemFormatString="&lt;ITEM_LABEL&gt;" Orientation="VerticalLeftFacing" 
                                                    VerticalAlign="Center" Flip="True" Font="Arial, 6.75pt">
                                                    <SeriesLabels HorizontalAlign="Center" Orientation="Horizontal" 
                                                        VerticalAlign="Center">
                                                    </SeriesLabels>
                                                </Labels>
                                            </igchartprop:AxisItem>
                                        </Axes>
                                        <PE Texture="Blocks" />
                                        <GridPE ElementType="None" />
                                    </igchartprop:ChartArea>
                                </ChartAreas>
                                <Legends>
                                    <igchartprop:CompositeLegend BoundsMeasureType="Percentage">
                                    </igchartprop:CompositeLegend>
                                </Legends>
                            </CompositeChart>
                            <Axis>
                                <PE ElementType="None" Fill="Cornsilk" />
                                <X LineThickness="1" TickmarkInterval="0" TickmarkStyle="Smart" Visible="True">
                                    <MajorGridLines AlphaLevel="255" Color="Gainsboro" DrawStyle="Dot" 
                                        Thickness="1" Visible="True" />
                                    <MinorGridLines AlphaLevel="255" Color="LightGray" DrawStyle="Dot" 
                                        Thickness="1" Visible="False" />
                                    <Labels Font="Verdana, 7pt" FontColor="DimGray" HorizontalAlign="Near" 
                                        ItemFormatString="&lt;ITEM_LABEL&gt;" Orientation="VerticalLeftFacing" 
                                        VerticalAlign="Center">
                                        <SeriesLabels Font="Verdana, 7pt" FontColor="DimGray" FormatString="" 
                                            HorizontalAlign="Near" Orientation="VerticalLeftFacing" VerticalAlign="Center">
                                            <Layout Behavior="Auto">
                                            </Layout>
                                        </SeriesLabels>
                                        <Layout Behavior="Auto">
                                        </Layout>
                                    </Labels>
                                </X>
                                <Y LineThickness="1" TickmarkInterval="0" TickmarkStyle="Smart" Visible="True">
                                    <MajorGridLines AlphaLevel="255" Color="Gainsboro" DrawStyle="Dot" 
                                        Thickness="1" Visible="True" />
                                    <MinorGridLines AlphaLevel="255" Color="LightGray" DrawStyle="Dot" 
                                        Thickness="1" Visible="False" />
                                    <Labels Font="Verdana, 7pt" FontColor="DimGray" HorizontalAlign="Far" 
                                        ItemFormatString="&lt;DATA_VALUE:00.##&gt;" Orientation="Horizontal" 
                                        VerticalAlign="Center">
                                        <SeriesLabels Font="Verdana, 7pt" FontColor="DimGray" FormatString="" 
                                            HorizontalAlign="Far" Orientation="Horizontal" VerticalAlign="Center">
                                            <Layout Behavior="Auto">
                                            </Layout>
                                        </SeriesLabels>
                                        <Layout Behavior="Auto">
                                        </Layout>
                                    </Labels>
                                </Y>
                                <Y2 LineThickness="1" TickmarkInterval="0" TickmarkStyle="Smart" 
                                    Visible="False">
                                    <MajorGridLines AlphaLevel="255" Color="Gainsboro" DrawStyle="Dot" 
                                        Thickness="1" Visible="True" />
                                    <MinorGridLines AlphaLevel="255" Color="LightGray" DrawStyle="Dot" 
                                        Thickness="1" Visible="False" />
                                    <Labels Font="Verdana, 7pt" FontColor="Gray" HorizontalAlign="Near" 
                                        ItemFormatString="&lt;DATA_VALUE:00.##&gt;" Orientation="Horizontal" 
                                        VerticalAlign="Center" Visible="False">
                                        <SeriesLabels Font="Verdana, 7pt" FontColor="Gray" FormatString="" 
                                            HorizontalAlign="Near" Orientation="Horizontal" VerticalAlign="Center">
                                            <Layout Behavior="Auto">
                                            </Layout>
                                        </SeriesLabels>
                                        <Layout Behavior="Auto">
                                        </Layout>
                                    </Labels>
                                </Y2>
                                <X2 LineThickness="1" TickmarkInterval="0" TickmarkStyle="Smart" 
                                    Visible="False">
                                    <MajorGridLines AlphaLevel="255" Color="Gainsboro" DrawStyle="Dot" 
                                        Thickness="1" Visible="True" />
                                    <MinorGridLines AlphaLevel="255" Color="LightGray" DrawStyle="Dot" 
                                        Thickness="1" Visible="False" />
                                    <Labels Font="Verdana, 7pt" FontColor="Gray" HorizontalAlign="Far" 
                                        ItemFormatString="&lt;ITEM_LABEL&gt;" Orientation="VerticalLeftFacing" 
                                        VerticalAlign="Center" Visible="False">
                                        <SeriesLabels Font="Verdana, 7pt" FontColor="Gray" FormatString="" 
                                            HorizontalAlign="Far" Orientation="VerticalLeftFacing" VerticalAlign="Center">
                                            <Layout Behavior="Auto">
                                            </Layout>
                                        </SeriesLabels>
                                        <Layout Behavior="Auto">
                                        </Layout>
                                    </Labels>
                                </X2>
                                <Z LineThickness="1" TickmarkInterval="0" TickmarkStyle="Smart" Visible="False">
                                    <MajorGridLines AlphaLevel="255" Color="Gainsboro" DrawStyle="Dot" 
                                        Thickness="1" Visible="True" />
                                    <MinorGridLines AlphaLevel="255" Color="LightGray" DrawStyle="Dot" 
                                        Thickness="1" Visible="False" />
                                    <Labels Font="Verdana, 7pt" FontColor="DimGray" HorizontalAlign="Near" 
                                        ItemFormatString="" Orientation="Horizontal" VerticalAlign="Center">
                                        <SeriesLabels Font="Verdana, 7pt" FontColor="DimGray" HorizontalAlign="Near" 
                                            Orientation="Horizontal" VerticalAlign="Center">
                                            <Layout Behavior="Auto">
                                            </Layout>
                                        </SeriesLabels>
                                        <Layout Behavior="Auto">
                                        </Layout>
                                    </Labels>
                                </Z>
                                <Z2 LineThickness="1" TickmarkInterval="0" TickmarkStyle="Smart" 
                                    Visible="False">
                                    <MajorGridLines AlphaLevel="255" Color="Gainsboro" DrawStyle="Dot" 
                                        Thickness="1" Visible="True" />
                                    <MinorGridLines AlphaLevel="255" Color="LightGray" DrawStyle="Dot" 
                                        Thickness="1" Visible="False" />
                                    <Labels Font="Verdana, 7pt" FontColor="Gray" HorizontalAlign="Near" 
                                        ItemFormatString="" Orientation="Horizontal" VerticalAlign="Center" 
                                        Visible="False">
                                        <SeriesLabels Font="Verdana, 7pt" FontColor="Gray" HorizontalAlign="Near" 
                                            Orientation="Horizontal" VerticalAlign="Center">
                                            <Layout Behavior="Auto">
                                            </Layout>
                                        </SeriesLabels>
                                        <Layout Behavior="Auto">
                                        </Layout>
                                    </Labels>
                                </Z2>
                            </Axis>
                            <Effects>
                                <Effects>
                                    <igchartprop:GradientEffect />
                                </Effects>
                            </Effects>
                                    <ColorModel AlphaLevel="150" ColorBegin="Pink" ColorEnd="DarkRed" 
                                        ModelStyle="CustomLinear">
                                    </ColorModel>
                                    <TitleBottom Extent="33" Font="Arial, 8.25pt" Location="Bottom">
                                    </TitleBottom>
                            <Border Thickness="0" />
                            <Tooltips Font-Bold="False" Font-Italic="False" Font-Overline="False" 
                                Font-Strikeout="False" Font-Underline="False" />
                        </igchart:UltraChart>
                    </Template>
                </igmisc:WebPanel>
            </td>
            <td style="width:24px;" valign="top"></td>
            <td valign="top" style="width:500px;" align="right">
                <igmisc:WebPanel ID="WebPanel6" runat="server" EnableAppStyling="True" Width="500px" 
                    StyleSetName="">
                    <Header Text ="Piezas empacadas" >
                    </Header>
                    <Template>
                       <igchart:UltraChart ID="UltraChart6" runat="server" BackgroundImageFileName="" 
                            BorderColor="Black" BorderWidth="0px" ChartType="Composite" 
                            EmptyChartText="Data Not Available. Please call UltraChart.Data.DataBind() after setting valid Data.DataSource" 
                            Version="9.1" onchartdataclicked="UltraChart1_ChartDataClicked" Width="500px" 
                                    Height="206px">
                                    <Legend BackgroundColor="White" BorderColor="Black" 
                                        DataAssociation="ColumnData" Font="Arial, 8.25pt" Visible="True"></Legend>
                            <CompositeChart>
                                <Series>
                                    <igchartprop:NumericSeries Key="series1" Label="Prueba 1">
                                        <points>
                                            <igchartprop:NumericDataPoint Label="Enero" Value="26">
                                                <pe elementtype="None" />
                                            </igchartprop:NumericDataPoint>
                                            <igchartprop:NumericDataPoint Label="Febrero" Value="32">
                                                <pe elementtype="None" />
                                            </igchartprop:NumericDataPoint>
                                            <igchartprop:NumericDataPoint Label="Marzo" Value="28">
                                                <pe elementtype="None" />
                                            </igchartprop:NumericDataPoint>
                                            <igchartprop:NumericDataPoint Label="1">
                                                <pe elementtype="None" />
                                            </igchartprop:NumericDataPoint>
                                            <igchartprop:NumericDataPoint Label="2">
                                                <pe elementtype="None" />
                                            </igchartprop:NumericDataPoint>
                                            <igchartprop:NumericDataPoint Label="3">
                                                <pe elementtype="None" />
                                            </igchartprop:NumericDataPoint>
                                            <igchartprop:NumericDataPoint Label="4">
                                                <pe elementtype="None" />
                                            </igchartprop:NumericDataPoint>
                                            <igchartprop:NumericDataPoint Label="5">
                                                <pe elementtype="None" />
                                            </igchartprop:NumericDataPoint>
                                            <igchartprop:NumericDataPoint Label="6">
                                                <pe elementtype="None" />
                                            </igchartprop:NumericDataPoint>
                                            <igchartprop:NumericDataPoint Label="7">
                                                <pe elementtype="None" />
                                            </igchartprop:NumericDataPoint>
                                            <igchartprop:NumericDataPoint Label="8">
                                                <pe elementtype="None" />
                                            </igchartprop:NumericDataPoint>
                                            <igchartprop:NumericDataPoint Label="9">
                                                <pe elementtype="None" />
                                            </igchartprop:NumericDataPoint>
                                            <igchartprop:NumericDataPoint Label="10">
                                                <pe elementtype="None" />
                                            </igchartprop:NumericDataPoint>
                                            <igchartprop:NumericDataPoint Label="11">
                                                <pe elementtype="None" />
                                            </igchartprop:NumericDataPoint>
                                            <igchartprop:NumericDataPoint Label="12">
                                                <pe elementtype="None" />
                                            </igchartprop:NumericDataPoint>
                                            <igchartprop:NumericDataPoint Label="13">
                                                <pe elementtype="None" />
                                            </igchartprop:NumericDataPoint>
                                            <igchartprop:NumericDataPoint Label="14">
                                                <pe elementtype="None" />
                                            </igchartprop:NumericDataPoint>
                                            <igchartprop:NumericDataPoint Label="15">
                                                <pe elementtype="None" />
                                            </igchartprop:NumericDataPoint>
                                            <igchartprop:NumericDataPoint Label="16">
                                                <pe elementtype="None" />
                                            </igchartprop:NumericDataPoint>
                                            <igchartprop:NumericDataPoint Label="17">
                                                <pe elementtype="None" />
                                            </igchartprop:NumericDataPoint>
                                            <igchartprop:NumericDataPoint Label="18">
                                                <pe elementtype="None" />
                                            </igchartprop:NumericDataPoint>
                                            <igchartprop:NumericDataPoint Label="19">
                                                <pe elementtype="None" />
                                            </igchartprop:NumericDataPoint>
                                            <igchartprop:NumericDataPoint Label="20">
                                                <pe elementtype="None" />
                                            </igchartprop:NumericDataPoint>
                                            <igchartprop:NumericDataPoint Label="21">
                                                <pe elementtype="None" />
                                            </igchartprop:NumericDataPoint>
                                            <igchartprop:NumericDataPoint Label="22">
                                                <pe elementtype="None" />
                                            </igchartprop:NumericDataPoint>
                                            <igchartprop:NumericDataPoint Label="23">
                                                <pe elementtype="None" />
                                            </igchartprop:NumericDataPoint>
                                            <igchartprop:NumericDataPoint Label="24">
                                                <pe elementtype="None" />
                                            </igchartprop:NumericDataPoint>
                                            <igchartprop:NumericDataPoint Label="25">
                                                <pe elementtype="None" />
                                            </igchartprop:NumericDataPoint>
                                            <igchartprop:NumericDataPoint Label="26">
                                                <pe elementtype="None" />
                                            </igchartprop:NumericDataPoint>
                                            <igchartprop:NumericDataPoint Label="27">
                                                <pe elementtype="None" />
                                            </igchartprop:NumericDataPoint>
                                            <igchartprop:NumericDataPoint Label="28">
                                                <pe elementtype="None" />
                                            </igchartprop:NumericDataPoint>
                                            <igchartprop:NumericDataPoint Label="29">
                                                <pe elementtype="None" />
                                            </igchartprop:NumericDataPoint>
                                            <igchartprop:NumericDataPoint Label="30">
                                                <pe elementtype="None" />
                                            </igchartprop:NumericDataPoint>
                                            <igchartprop:NumericDataPoint Label="31">
                                                <pe elementtype="None" />
                                            </igchartprop:NumericDataPoint>
                                        </points>
                                    </igchartprop:NumericSeries>
                                    <igchartprop:NumericSeries Key="series3" Label="Prueba2">
                                        <points>
                                            <igchartprop:NumericDataPoint Label="Enero">
                                                <pe elementtype="None" />
                                            </igchartprop:NumericDataPoint>
                                            <igchartprop:NumericDataPoint Label="Febrero">
                                                <pe elementtype="None" />
                                            </igchartprop:NumericDataPoint>
                                            <igchartprop:NumericDataPoint Label="Marzo">
                                                <pe elementtype="None" />
                                            </igchartprop:NumericDataPoint>
                                            <igchartprop:NumericDataPoint Label="1" Value="25">
                                                <pe elementtype="None" />
                                            </igchartprop:NumericDataPoint>
                                            <igchartprop:NumericDataPoint Label="2" Value="32">
                                                <pe elementtype="None" />
                                            </igchartprop:NumericDataPoint>
                                            <igchartprop:NumericDataPoint Label="3" Value="20">
                                                <pe elementtype="None" />
                                            </igchartprop:NumericDataPoint>
                                            <igchartprop:NumericDataPoint Label="4" Value="12">
                                                <pe elementtype="None" />
                                            </igchartprop:NumericDataPoint>
                                            <igchartprop:NumericDataPoint Label="5" Value="24">
                                                <pe elementtype="None" />
                                            </igchartprop:NumericDataPoint>
                                            <igchartprop:NumericDataPoint Label="6" Value="20">
                                                <pe elementtype="None" />
                                            </igchartprop:NumericDataPoint>
                                            <igchartprop:NumericDataPoint Label="7" Value="21">
                                                <pe elementtype="None" />
                                            </igchartprop:NumericDataPoint>
                                            <igchartprop:NumericDataPoint Label="8" Value="25">
                                                <pe elementtype="None" />
                                            </igchartprop:NumericDataPoint>
                                            <igchartprop:NumericDataPoint Label="9" Value="30">
                                                <pe elementtype="None" />
                                            </igchartprop:NumericDataPoint>
                                            <igchartprop:NumericDataPoint Label="10" Value="29">
                                                <pe elementtype="None" />
                                            </igchartprop:NumericDataPoint>
                                            <igchartprop:NumericDataPoint Label="11" Value="24">
                                                <pe elementtype="None" />
                                            </igchartprop:NumericDataPoint>
                                            <igchartprop:NumericDataPoint Label="12" Value="18">
                                                <pe elementtype="None" />
                                            </igchartprop:NumericDataPoint>
                                            <igchartprop:NumericDataPoint Label="13" Value="16">
                                                <pe elementtype="None" />
                                            </igchartprop:NumericDataPoint>
                                            <igchartprop:NumericDataPoint Label="14" Value="27">
                                                <pe elementtype="None" />
                                            </igchartprop:NumericDataPoint>
                                            <igchartprop:NumericDataPoint Label="15" Value="23">
                                                <pe elementtype="None" />
                                            </igchartprop:NumericDataPoint>
                                            <igchartprop:NumericDataPoint Label="16" Value="28">
                                                <pe elementtype="None" />
                                            </igchartprop:NumericDataPoint>
                                            <igchartprop:NumericDataPoint Label="17" Value="22">
                                                <pe elementtype="None" />
                                            </igchartprop:NumericDataPoint>
                                            <igchartprop:NumericDataPoint Label="18" Value="31">
                                                <pe elementtype="None" />
                                            </igchartprop:NumericDataPoint>
                                            <igchartprop:NumericDataPoint Label="19" Value="23">
                                                <pe elementtype="None" />
                                            </igchartprop:NumericDataPoint>
                                            <igchartprop:NumericDataPoint Label="20" Value="24">
                                                <pe elementtype="None" />
                                            </igchartprop:NumericDataPoint>
                                            <igchartprop:NumericDataPoint Label="21" Value="25">
                                                <pe elementtype="None" />
                                            </igchartprop:NumericDataPoint>
                                            <igchartprop:NumericDataPoint Label="22" Value="30">
                                                <pe elementtype="None" />
                                            </igchartprop:NumericDataPoint>
                                            <igchartprop:NumericDataPoint Label="23" Value="14">
                                                <pe elementtype="None" />
                                            </igchartprop:NumericDataPoint>
                                            <igchartprop:NumericDataPoint Label="24" Value="36">
                                                <pe elementtype="None" />
                                            </igchartprop:NumericDataPoint>
                                            <igchartprop:NumericDataPoint Label="25" Value="30">
                                                <pe elementtype="None" />
                                            </igchartprop:NumericDataPoint>
                                            <igchartprop:NumericDataPoint Label="26" Value="24">
                                                <pe elementtype="None" />
                                            </igchartprop:NumericDataPoint>
                                            <igchartprop:NumericDataPoint Label="27" Value="26">
                                                <pe elementtype="None" />
                                            </igchartprop:NumericDataPoint>
                                            <igchartprop:NumericDataPoint Label="28" Value="20">
                                                <pe elementtype="None" />
                                            </igchartprop:NumericDataPoint>
                                            <igchartprop:NumericDataPoint Label="29" Value="30">
                                                <pe elementtype="None" />
                                            </igchartprop:NumericDataPoint>
                                            <igchartprop:NumericDataPoint Label="30" Value="21">
                                                <pe elementtype="None" />
                                            </igchartprop:NumericDataPoint>
                                            <igchartprop:NumericDataPoint Label="31" Value="22">
                                                <pe elementtype="None" />
                                            </igchartprop:NumericDataPoint>
                                        </points>
                                        <pes>
                                            <igchartprop:PaintElement Fill="Red" />
                                        </pes>
                                    </igchartprop:NumericSeries>
                                    <igchartprop:NumericSeries Key="series4" Label="Prueba3">
                                        <points>
                                            <igchartprop:NumericDataPoint Label="Enero">
                                                <pe elementtype="None" />
                                            </igchartprop:NumericDataPoint>
                                            <igchartprop:NumericDataPoint Label="Febrero">
                                                <pe elementtype="None" />
                                            </igchartprop:NumericDataPoint>
                                            <igchartprop:NumericDataPoint Label="Marzo">
                                                <pe elementtype="None" />
                                            </igchartprop:NumericDataPoint>
                                            <igchartprop:NumericDataPoint Label="1" Value="20">
                                                <pe elementtype="None" />
                                            </igchartprop:NumericDataPoint>
                                            <igchartprop:NumericDataPoint Label="2" Value="20">
                                                <pe elementtype="None" />
                                            </igchartprop:NumericDataPoint>
                                            <igchartprop:NumericDataPoint Label="3" Value="20">
                                                <pe elementtype="None" />
                                            </igchartprop:NumericDataPoint>
                                            <igchartprop:NumericDataPoint Label="4" Value="20">
                                                <pe elementtype="None" />
                                            </igchartprop:NumericDataPoint>
                                            <igchartprop:NumericDataPoint Label="5" Value="20">
                                                <pe elementtype="None" />
                                            </igchartprop:NumericDataPoint>
                                            <igchartprop:NumericDataPoint Label="6" Value="20">
                                                <pe elementtype="None" />
                                            </igchartprop:NumericDataPoint>
                                            <igchartprop:NumericDataPoint Label="7" Value="20">
                                                <pe elementtype="None" />
                                            </igchartprop:NumericDataPoint>
                                            <igchartprop:NumericDataPoint Label="8" Value="20">
                                                <pe elementtype="None" />
                                            </igchartprop:NumericDataPoint>
                                            <igchartprop:NumericDataPoint Label="9" Value="20">
                                                <pe elementtype="None" />
                                            </igchartprop:NumericDataPoint>
                                            <igchartprop:NumericDataPoint Label="10" Value="20">
                                                <pe elementtype="None" />
                                            </igchartprop:NumericDataPoint>
                                            <igchartprop:NumericDataPoint Label="11" Value="20">
                                                <pe elementtype="None" />
                                            </igchartprop:NumericDataPoint>
                                            <igchartprop:NumericDataPoint Label="12" Value="20">
                                                <pe elementtype="None" />
                                            </igchartprop:NumericDataPoint>
                                            <igchartprop:NumericDataPoint Label="13" Value="20">
                                                <pe elementtype="None" />
                                            </igchartprop:NumericDataPoint>
                                            <igchartprop:NumericDataPoint Label="14" Value="20">
                                                <pe elementtype="None" />
                                            </igchartprop:NumericDataPoint>
                                            <igchartprop:NumericDataPoint Label="15" Value="20">
                                                <pe elementtype="None" />
                                            </igchartprop:NumericDataPoint>
                                            <igchartprop:NumericDataPoint Label="16" Value="20">
                                                <pe elementtype="None" />
                                            </igchartprop:NumericDataPoint>
                                            <igchartprop:NumericDataPoint Label="17" Value="20">
                                                <pe elementtype="None" />
                                            </igchartprop:NumericDataPoint>
                                            <igchartprop:NumericDataPoint Label="18" Value="20">
                                                <pe elementtype="None" />
                                            </igchartprop:NumericDataPoint>
                                            <igchartprop:NumericDataPoint Label="19" Value="20">
                                                <pe elementtype="None" />
                                            </igchartprop:NumericDataPoint>
                                            <igchartprop:NumericDataPoint Label="20" Value="20">
                                                <pe elementtype="None" />
                                            </igchartprop:NumericDataPoint>
                                            <igchartprop:NumericDataPoint Label="21" Value="20">
                                                <pe elementtype="None" />
                                            </igchartprop:NumericDataPoint>
                                            <igchartprop:NumericDataPoint Label="22" Value="20">
                                                <pe elementtype="None" />
                                            </igchartprop:NumericDataPoint>
                                            <igchartprop:NumericDataPoint Label="23" Value="20">
                                                <pe elementtype="None" />
                                            </igchartprop:NumericDataPoint>
                                            <igchartprop:NumericDataPoint Label="24" Value="20">
                                                <pe elementtype="None" />
                                            </igchartprop:NumericDataPoint>
                                            <igchartprop:NumericDataPoint Label="25" Value="20">
                                                <pe elementtype="None" />
                                            </igchartprop:NumericDataPoint>
                                            <igchartprop:NumericDataPoint Label="26" Value="20">
                                                <pe elementtype="None" />
                                            </igchartprop:NumericDataPoint>
                                            <igchartprop:NumericDataPoint Label="27" Value="20">
                                                <pe elementtype="None" />
                                            </igchartprop:NumericDataPoint>
                                            <igchartprop:NumericDataPoint Label="28" Value="20">
                                                <pe elementtype="None" />
                                            </igchartprop:NumericDataPoint>
                                            <igchartprop:NumericDataPoint Label="29" Value="20">
                                                <pe elementtype="None" />
                                            </igchartprop:NumericDataPoint>
                                            <igchartprop:NumericDataPoint Label="30" Value="20">
                                                <pe elementtype="None" />
                                            </igchartprop:NumericDataPoint>
                                            <igchartprop:NumericDataPoint Label="31" Value="20">
                                                <pe elementtype="None" />
                                            </igchartprop:NumericDataPoint>
                                        </points>
                                        <pes>
                                            <igchartprop:PaintElement Fill="Blue" />
                                        </pes>
                                    </igchartprop:NumericSeries>
                                </Series>
                                <ChartLayers>
                                    <igchartprop:ChartLayerAppearance AxisXKey="axis1" AxisYKey="axis2" 
                                        ChartAreaKey="area1" ChartType="ColumnChart" Key="chartLayer1" 
                                        SeriesList="series1">
                                        <ChartTypeAppearances>
                                            <igchartprop:ColumnChartAppearance NullHandling="DontPlot" 
                                                ChartTextValueAlignment="PositiveAndNegative">
                                                <ChartText>
                                                    <igchartprop:ChartTextAppearance ChartTextFont="Arial, 7pt" Column="0" 
                                                        ItemFormatString="&lt;DATA_VALUE:00&gt;" Row="0" />
                                                </ChartText>
                                            </igchartprop:ColumnChartAppearance>
                                        </ChartTypeAppearances>
                                    </igchartprop:ChartLayerAppearance>
                                    <igchartprop:ChartLayerAppearance AxisXKey="axis3" AxisYKey="axis2" 
                                        ChartAreaKey="area1" ChartType="LineChart" Key="chartLayer2" 
                                        SeriesList="series3">
                                        <ChartTypeAppearances>
                                            <igchartprop:LineChartAppearance HighLightLines="True" MidPointAnchors="False" 
                                                NullHandling="DontPlot" Thickness="4">
                                                <ChartText>
                                                    <igchartprop:ChartTextAppearance ChartTextFont="Arial, 7pt" Column="0" 
                                                        ItemFormatString="&lt;DATA_VALUE:00&gt;" Row="0" />
                                                </ChartText>
                                                <LineAppearances>
                                                    <igchartprop:LineAppearance>
                                                        <IconAppearance CharacterFont="Arial, 8.25pt" Icon="Circle">
                                                            <PE ElementType="None" />
                                                        </IconAppearance>
                                                    </igchartprop:LineAppearance>
                                                </LineAppearances>
                                            </igchartprop:LineChartAppearance>
                                        </ChartTypeAppearances>
                                    </igchartprop:ChartLayerAppearance>
                                    <igchartprop:ChartLayerAppearance AxisXKey="axis3" AxisYKey="axis2" 
                                        ChartAreaKey="area1" ChartType="LineChart" Key="chartLayer3" 
                                        SeriesList="series4">
                                        <ChartTypeAppearances>
                                            <igchartprop:LineChartAppearance>
                                                <ChartText>
                                                    <igchartprop:ChartTextAppearance ChartTextFont="Arial, 7pt" Column="0" 
                                                        ItemFormatString="&lt;DATA_VALUE:00&gt;" Row="0" />
                                                </ChartText>
                                                <LineAppearances>
                                                    <igchartprop:LineAppearance>
                                                        <IconAppearance CharacterFont="Arial Narrow, 6.75pt">
                                                            <PE ElementType="None" StrokeWidth="2" />
                                                        </IconAppearance>
                                                    </igchartprop:LineAppearance>
                                                </LineAppearances>
                                            </igchartprop:LineChartAppearance>
                                        </ChartTypeAppearances>
                                    </igchartprop:ChartLayerAppearance>
                                </ChartLayers>
                                <ChartAreas>
                                    <igchartprop:ChartArea Bounds="0, 0, 100, 100" BoundsMeasureType="Percentage" 
                                        Key="area1">
                                        <Axes>
                                            <igchartprop:AxisItem DataType="String" Extent="20" Key="axis1" 
                                                OrientationType="X_Axis" SetLabelAxisType="GroupBySeries" TickmarkInterval="1">
                                                <MajorGridLines AlphaLevel="255" Color="Gainsboro" DrawStyle="Dot" 
                                                    Thickness="1" Visible="True" />
                                                <MinorGridLines AlphaLevel="255" Color="LightGray" DrawStyle="Dot" 
                                                    Thickness="1" Visible="False" />
                                                <Labels HorizontalAlign="Near" ItemFormatString="" Orientation="Horizontal" 
                                                    VerticalAlign="Center" Font="Arial, 6.75pt" Visible="False">
                                                    <SeriesLabels HorizontalAlign="Center" Orientation="Horizontal" 
                                                        VerticalAlign="Center" Visible="False">
                                                    </SeriesLabels>
                                                </Labels>
                                            </igchartprop:AxisItem>
                                            <igchartprop:AxisItem DataType="Numeric" Extent="20" Key="axis2" 
                                                OrientationType="Y_Axis" SetLabelAxisType="GroupBySeries" TickmarkInterval="0">
                                                <MajorGridLines AlphaLevel="255" Color="Gainsboro" DrawStyle="Dot" 
                                                    Thickness="1" Visible="True" />
                                                <MinorGridLines AlphaLevel="255" Color="LightGray" DrawStyle="Dot" 
                                                    Thickness="1" Visible="False" />
                                                <Labels HorizontalAlign="Near" ItemFormatString="&lt;DATA_VALUE:00&gt;" Orientation="Horizontal" 
                                                    VerticalAlign="Center" Flip="True" Font="Arial, 8.25pt">
                                                    <SeriesLabels HorizontalAlign="Center" Orientation="Horizontal" 
                                                        VerticalAlign="Center">
                                                    </SeriesLabels>
                                                </Labels>
                                            </igchartprop:AxisItem>
                                            <igchartprop:AxisItem DataType="String" Extent="60" Key="axis3" 
                                                OrientationType="X_Axis" SetLabelAxisType="ContinuousData" TickmarkInterval="0">
                                                <MajorGridLines AlphaLevel="255" Color="Gainsboro" DrawStyle="Dot" 
                                                    Thickness="1" Visible="True" />
                                                <MinorGridLines AlphaLevel="255" Color="LightGray" DrawStyle="Dot" 
                                                    Thickness="1" Visible="False" />
                                                <Labels HorizontalAlign="Near" ItemFormatString="&lt;ITEM_LABEL&gt;" Orientation="VerticalLeftFacing" 
                                                    VerticalAlign="Center" Flip="True" Font="Arial, 6.75pt">
                                                    <SeriesLabels HorizontalAlign="Center" Orientation="Horizontal" 
                                                        VerticalAlign="Center">
                                                    </SeriesLabels>
                                                </Labels>
                                            </igchartprop:AxisItem>
                                        </Axes>
                                        <PE Texture="Blocks" />
                                        <GridPE ElementType="None" />
                                    </igchartprop:ChartArea>
                                </ChartAreas>
                                <Legends>
                                    <igchartprop:CompositeLegend BoundsMeasureType="Percentage">
                                    </igchartprop:CompositeLegend>
                                </Legends>
                            </CompositeChart>
                            <Axis>
                                <PE ElementType="None" Fill="Cornsilk" />
                                <X LineThickness="1" TickmarkInterval="0" TickmarkStyle="Smart" Visible="True">
                                    <MajorGridLines AlphaLevel="255" Color="Gainsboro" DrawStyle="Dot" 
                                        Thickness="1" Visible="True" />
                                    <MinorGridLines AlphaLevel="255" Color="LightGray" DrawStyle="Dot" 
                                        Thickness="1" Visible="False" />
                                    <Labels Font="Verdana, 7pt" FontColor="DimGray" HorizontalAlign="Near" 
                                        ItemFormatString="&lt;ITEM_LABEL&gt;" Orientation="VerticalLeftFacing" 
                                        VerticalAlign="Center">
                                        <SeriesLabels Font="Verdana, 7pt" FontColor="DimGray" FormatString="" 
                                            HorizontalAlign="Near" Orientation="VerticalLeftFacing" VerticalAlign="Center">
                                            <Layout Behavior="Auto">
                                            </Layout>
                                        </SeriesLabels>
                                        <Layout Behavior="Auto">
                                        </Layout>
                                    </Labels>
                                </X>
                                <Y LineThickness="1" TickmarkInterval="0" TickmarkStyle="Smart" Visible="True">
                                    <MajorGridLines AlphaLevel="255" Color="Gainsboro" DrawStyle="Dot" 
                                        Thickness="1" Visible="True" />
                                    <MinorGridLines AlphaLevel="255" Color="LightGray" DrawStyle="Dot" 
                                        Thickness="1" Visible="False" />
                                    <Labels Font="Verdana, 7pt" FontColor="DimGray" HorizontalAlign="Far" 
                                        ItemFormatString="&lt;DATA_VALUE:00.##&gt;" Orientation="Horizontal" 
                                        VerticalAlign="Center">
                                        <SeriesLabels Font="Verdana, 7pt" FontColor="DimGray" FormatString="" 
                                            HorizontalAlign="Far" Orientation="Horizontal" VerticalAlign="Center">
                                            <Layout Behavior="Auto">
                                            </Layout>
                                        </SeriesLabels>
                                        <Layout Behavior="Auto">
                                        </Layout>
                                    </Labels>
                                </Y>
                                <Y2 LineThickness="1" TickmarkInterval="0" TickmarkStyle="Smart" 
                                    Visible="False">
                                    <MajorGridLines AlphaLevel="255" Color="Gainsboro" DrawStyle="Dot" 
                                        Thickness="1" Visible="True" />
                                    <MinorGridLines AlphaLevel="255" Color="LightGray" DrawStyle="Dot" 
                                        Thickness="1" Visible="False" />
                                    <Labels Font="Verdana, 7pt" FontColor="Gray" HorizontalAlign="Near" 
                                        ItemFormatString="&lt;DATA_VALUE:00.##&gt;" Orientation="Horizontal" 
                                        VerticalAlign="Center" Visible="False">
                                        <SeriesLabels Font="Verdana, 7pt" FontColor="Gray" FormatString="" 
                                            HorizontalAlign="Near" Orientation="Horizontal" VerticalAlign="Center">
                                            <Layout Behavior="Auto">
                                            </Layout>
                                        </SeriesLabels>
                                        <Layout Behavior="Auto">
                                        </Layout>
                                    </Labels>
                                </Y2>
                                <X2 LineThickness="1" TickmarkInterval="0" TickmarkStyle="Smart" 
                                    Visible="False">
                                    <MajorGridLines AlphaLevel="255" Color="Gainsboro" DrawStyle="Dot" 
                                        Thickness="1" Visible="True" />
                                    <MinorGridLines AlphaLevel="255" Color="LightGray" DrawStyle="Dot" 
                                        Thickness="1" Visible="False" />
                                    <Labels Font="Verdana, 7pt" FontColor="Gray" HorizontalAlign="Far" 
                                        ItemFormatString="&lt;ITEM_LABEL&gt;" Orientation="VerticalLeftFacing" 
                                        VerticalAlign="Center" Visible="False">
                                        <SeriesLabels Font="Verdana, 7pt" FontColor="Gray" FormatString="" 
                                            HorizontalAlign="Far" Orientation="VerticalLeftFacing" VerticalAlign="Center">
                                            <Layout Behavior="Auto">
                                            </Layout>
                                        </SeriesLabels>
                                        <Layout Behavior="Auto">
                                        </Layout>
                                    </Labels>
                                </X2>
                                <Z LineThickness="1" TickmarkInterval="0" TickmarkStyle="Smart" Visible="False">
                                    <MajorGridLines AlphaLevel="255" Color="Gainsboro" DrawStyle="Dot" 
                                        Thickness="1" Visible="True" />
                                    <MinorGridLines AlphaLevel="255" Color="LightGray" DrawStyle="Dot" 
                                        Thickness="1" Visible="False" />
                                    <Labels Font="Verdana, 7pt" FontColor="DimGray" HorizontalAlign="Near" 
                                        ItemFormatString="" Orientation="Horizontal" VerticalAlign="Center">
                                        <SeriesLabels Font="Verdana, 7pt" FontColor="DimGray" HorizontalAlign="Near" 
                                            Orientation="Horizontal" VerticalAlign="Center">
                                            <Layout Behavior="Auto">
                                            </Layout>
                                        </SeriesLabels>
                                        <Layout Behavior="Auto">
                                        </Layout>
                                    </Labels>
                                </Z>
                                <Z2 LineThickness="1" TickmarkInterval="0" TickmarkStyle="Smart" 
                                    Visible="False">
                                    <MajorGridLines AlphaLevel="255" Color="Gainsboro" DrawStyle="Dot" 
                                        Thickness="1" Visible="True" />
                                    <MinorGridLines AlphaLevel="255" Color="LightGray" DrawStyle="Dot" 
                                        Thickness="1" Visible="False" />
                                    <Labels Font="Verdana, 7pt" FontColor="Gray" HorizontalAlign="Near" 
                                        ItemFormatString="" Orientation="Horizontal" VerticalAlign="Center" 
                                        Visible="False">
                                        <SeriesLabels Font="Verdana, 7pt" FontColor="Gray" HorizontalAlign="Near" 
                                            Orientation="Horizontal" VerticalAlign="Center">
                                            <Layout Behavior="Auto">
                                            </Layout>
                                        </SeriesLabels>
                                        <Layout Behavior="Auto">
                                        </Layout>
                                    </Labels>
                                </Z2>
                            </Axis>
                            <Effects>
                                <Effects>
                                    <igchartprop:GradientEffect />
                                </Effects>
                            </Effects>
                                    <ColorModel AlphaLevel="150" ColorBegin="Pink" ColorEnd="DarkRed" 
                                        ModelStyle="CustomLinear">
                                    </ColorModel>
                                    <TitleBottom Extent="33" Font="Arial, 8.25pt" Location="Bottom">
                                    </TitleBottom>
                            <Border Thickness="0" />
                            <Tooltips Font-Bold="False" Font-Italic="False" Font-Overline="False" 
                                Font-Strikeout="False" Font-Underline="False" />
                        </igchart:UltraChart>
                    </Template>
                </igmisc:WebPanel>
            </td>
        </tr>
    </table>
    </div>
    </form>
</body>
</html>
