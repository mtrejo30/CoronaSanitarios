<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Dashboard.aspx.cs" Inherits="LAMOSA.SCPP.Client.View.Administrador.Dashboard.Dashboard" %>

<%@ Register Assembly="Infragistics35.WebUI.UltraWebChart.v9.1, Version=9.1.20091.1015, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb"
    Namespace="Infragistics.WebUI.UltraWebChart" TagPrefix="igchart" %>
<%@ Register Assembly="Infragistics35.WebUI.Misc.v9.1, Version=9.1.20091.1015, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb"
    Namespace="Infragistics.WebUI.Misc" TagPrefix="igmisc" %>
<%@ Register Assembly="Infragistics35.WebUI.UltraWebChart.v9.1, Version=9.1.20091.1015, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb"
    Namespace="Infragistics.UltraChart.Resources.Appearance" TagPrefix="igchartprop" %>
<%@ Register Assembly="Infragistics35.WebUI.UltraWebChart.v9.1, Version=9.1.20091.1015, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb"
    Namespace="Infragistics.UltraChart.Data" TagPrefix="igchartdata" %>
<%@ Register Assembly="Infragistics35.WebUI.UltraWebGrid.v9.1, Version=9.1.20091.1015, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb"
    Namespace="Infragistics.WebUI.UltraWebGrid" TagPrefix="igtbl" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <link href="../Estiloscss/pro-line-down-fly/menu3.css" media="screen" rel="stylesheet"
        type="text/css" />
    <link href="../Estiloscss/Estilos.css" rel="stylesheet" type="text/css" />
    <link href="../Estiloscss/menu.css" rel="Stylesheet" type="text/css" />
    <title>Dashboard</title>

    <script language="javascript" type="text/javascript">
        function Cerrar() {
            close();
        }
        
    </script>

    <style type="text/css">
        .style1
        {
            width: 317px;
        }
        ul
        {
            list-style: none;
            clear: both;
            line-height: 1.5;
            border: 0;
            outline: 0;
            font-size: 100%;
            vertical-align: baseline;
            background: transparent;
        }
        ul li
        {
            margin: 0;
            padding: 0;
            border: 0;
            outline: 0;
            font-size: 100%;
            vertical-align: baseline;
            background: transparent;
            list-style: none !important;
            padding-bottom: 10px;
            overflow: hidden;
        }
        ul li span
        {
            color: #C00;
            font-style: italic bold;
            margin-right: 5px;
            float: left;
            display: inline;
            margin: 0;
            padding: 0;
            border: 0;
            outline: 0;
            font-size: 85%;
            vertical-align: baseline;
            background: transparent;
            line-height: 1.5;
            padding-right: 5px;
        }
        ul li p
        {
            color: #3A546D;
            margin: 0;
            padding: 0;
            border: 0;
            outline: 0;
            font-size: 85%;
            vertical-align: baseline;
            background: transparent;
        }
        .style2
        {
            width: 370px;
        }
        .style3
        {
            width: 317px;
            height: 165px;
        }
        .style4
        {
            width: 370px;
            height: 165px;
        }
        .style5
        {
            height: 165px;
        }
    </style>
</head>
<body class="fondoLogin" style="width: 80%; margin-left: 10%; margin-right: 10%">
    <form id="form1" runat="server">
    <div>
        <table align="center" width="100%" border="0" cellpadding="0" cellspacing="0" style="background-color: white;">
            <tr>
                <td rowspan="2" class="Header1DSB">
                    &nbsp;
                </td>
                <td class="Header2DSB">
                    <label class="Header2Text" id="lblfecha" runat="server">Planta: 
                        <asp:DropDownList ID="ddlPlanta" AutoPostBack="true" runat="server">
                        </asp:DropDownList>
                    </label>
                    |
                </td>
                <td>
                    <label id="lblAlmacen" class="HeaderText" runat="server">
                    </label>
                </td>
                <td>
                </td>
                <td>
                    <label id="lblPlanta" class="HeaderText" runat="server">
                    </label>
                </td>
                <td>
                    <a href="#" onclick="Cerrar()">Cerrar</a>
                </td>
            </tr>
            <tr>
				<td style="font-size: 5px;">.</td>
			</tr>
        </table>
        <table align="left" width="100%" border="0" cellpadding="0" cellspacing="0" style="background-color: white;">
            <tr>
                <td valign="top" align="left" class="style1">
                    <igchart:UltraChart ID="ucEsmaltado" runat="server" Version="9.1" BorderColor="#868686"
                        BorderWidth="0px" Height="170px" BackColor="#FFFFFF" ChartType="ScatterChart"
                        ImageUrl="" Transform3D-Scale="94" BackgroundImageStyle="Centered" TextRenderingHint="SingleBitPerPixelGridFit"
                        Width="317px">
                        <Legend BackgroundColor="Transparent" BorderThickness="0" Font="Calibri, 9pt" 
                            Visible="True"></Legend>
                        <ColorModel AlphaLevel="247" ColorBegin="107, 150, 224" 
                            ColorEnd="107, 150, 224" ModelStyle="CustomSkin" Scaling="Increasing">
                            <Skin>
                                <PEs>
                                    <igchartprop:PaintElement ElementType="Gradient" Fill="47, 123, 214" FillGradientStyle="Vertical"
                                        FillStopColor="29, 82, 145" Stroke="29, 82, 145" />
                                    <igchartprop:PaintElement Fill="Purple" Stroke="ActiveCaption" />
                                    <igchartprop:PaintElement ElementType="Gradient" Fill="Brown" />
                                </PEs>
                            </Skin>
                        </ColorModel>
                        <ScatterChart ConnectWithLines="True" IconSize="Small" LineAppearance-Thickness="2" Icon="Diamond">
                        </ScatterChart>
                        <Axis>
                            <PE Fill="White" StrokeWidth="0" />
                            <X LineThickness="1" TickmarkInterval="10" TickmarkStyle="Smart" Visible="True" Extent="20"
                                LineColor="134, 134, 134">
                                <MajorGridLines AlphaLevel="255" Color="Gainsboro" DrawStyle="Dot" Thickness="1"
                                    Visible="False" />
                                <MinorGridLines AlphaLevel="255" Color="LightGray" DrawStyle="Dot" Thickness="1"
                                    Visible="False" />
                                <Labels Font="Arial, 7pt" HorizontalAlign="Near"
                                    ItemFormatString="&lt;DATA_VALUE:00.##&gt;" Orientation="VerticalLeftFacing"
                                    VerticalAlign="Center">
                                    <SeriesLabels Font="Arial, 7pt" HorizontalAlign="Near"
                                        Orientation="Horizontal" VerticalAlign="Center">
                                        <Layout Behavior="Auto">
                                        </Layout>
                                    </SeriesLabels>
                                    <Layout Behavior="Auto">
                                    </Layout>
                                </Labels>
                            </X>
                            <Y LineThickness="1" TickmarkInterval="40" TickmarkStyle="Smart" Visible="True"
                                Extent="20" LineColor="134, 134, 134">
                                <MajorGridLines AlphaLevel="100" Color="134, 134, 134" DrawStyle="Dot" Thickness="1"
                                    Visible="True" />
                                <MinorGridLines AlphaLevel="255" Color="LightGray" DrawStyle="Dot" Thickness="1"
                                    Visible="False" />
                                <Labels Font="Arial, 7pt" HorizontalAlign="Far"
                                    ItemFormatString="&lt;DATA_VALUE:00.##&gt;" Orientation="Horizontal" 
                                    VerticalAlign="Center">
                                    <SeriesLabels Font="Arial, 7pt" FontColor="DimGray" HorizontalAlign="Far" Orientation="Horizontal"
                                        VerticalAlign="Center">
                                        <Layout Behavior="Auto">
                                        </Layout>
                                    </SeriesLabels>
                                    <Layout Behavior="Auto">
                                    </Layout>
                                </Labels>
                            </Y>
                            <Y2 LineThickness="1" TickmarkInterval="40" TickmarkStyle="Smart" 
                                Visible="False">
                                <MajorGridLines AlphaLevel="255" Color="Gainsboro" DrawStyle="Dot" Thickness="1"
                                    Visible="True" />
                                <MinorGridLines AlphaLevel="255" Color="LightGray" DrawStyle="Dot" Thickness="1"
                                    Visible="False" />
                                <Labels Font="Verdana, 7pt" FontColor="Gray" HorizontalAlign="Near" ItemFormatString="&lt;DATA_VALUE:00.##&gt;"
                                    Orientation="Horizontal" VerticalAlign="Center">
                                    <SeriesLabels Font="Verdana, 7pt" FontColor="Gray" HorizontalAlign="Near" Orientation="Horizontal"
                                        VerticalAlign="Center" FormatString="">
                                        <Layout Behavior="Auto">
                                        </Layout>
                                    </SeriesLabels>
                                    <Layout Behavior="Auto">
                                    </Layout>
                                </Labels>
                            </Y2>
                            <X2 LineThickness="1" TickmarkInterval="10" TickmarkStyle="Smart" Visible="False">
                                <MajorGridLines AlphaLevel="255" Color="Gainsboro" DrawStyle="Dot" Thickness="1"
                                    Visible="True" />
                                <MinorGridLines AlphaLevel="255" Color="LightGray" DrawStyle="Dot" Thickness="1"
                                    Visible="False" />
                                <Labels Font="Verdana, 7pt" FontColor="Gray" HorizontalAlign="Near" ItemFormatString="&lt;DATA_VALUE:00.##&gt;"
                                    Orientation="VerticalLeftFacing" VerticalAlign="Center">
                                    <SeriesLabels Font="Verdana, 7pt" FontColor="Gray" HorizontalAlign="Near" Orientation="VerticalLeftFacing"
                                        VerticalAlign="Center">
                                        <Layout Behavior="Auto">
                                        </Layout>
                                    </SeriesLabels>
                                    <Layout Behavior="Auto">
                                    </Layout>
                                </Labels>
                            </X2>
                            <Z LineThickness="1" TickmarkInterval="0" TickmarkStyle="Smart" Visible="False">
                                <MajorGridLines AlphaLevel="255" Color="Gainsboro" DrawStyle="Dot" Thickness="1"
                                    Visible="True" />
                                <MinorGridLines AlphaLevel="255" Color="LightGray" DrawStyle="Dot" Thickness="1"
                                    Visible="False" />
                                <Labels Font="Verdana, 7pt" FontColor="DimGray" HorizontalAlign="Near" ItemFormatString=""
                                    Orientation="Horizontal" VerticalAlign="Center">
                                    <SeriesLabels Font="Verdana, 7pt" FontColor="DimGray" HorizontalAlign="Near" Orientation="Horizontal"
                                        VerticalAlign="Center">
                                        <Layout Behavior="Auto">
                                        </Layout>
                                    </SeriesLabels>
                                    <Layout Behavior="Auto">
                                    </Layout>
                                </Labels>
                            </Z>
                            <Z2 LineThickness="1" TickmarkInterval="0" TickmarkStyle="Smart" Visible="False">
                                <MajorGridLines AlphaLevel="255" Color="Gainsboro" DrawStyle="Dot" Thickness="1"
                                    Visible="True" />
                                <MinorGridLines AlphaLevel="255" Color="LightGray" DrawStyle="Dot" Thickness="1"
                                    Visible="False" />
                                <Labels Font="Verdana, 7pt" FontColor="Gray" HorizontalAlign="Near" ItemFormatString=""
                                    Orientation="Horizontal" VerticalAlign="Center">
                                    <SeriesLabels Font="Verdana, 7pt" FontColor="Gray" HorizontalAlign="Near" Orientation="Horizontal"
                                        VerticalAlign="Center">
                                        <Layout Behavior="Auto">
                                        </Layout>
                                    </SeriesLabels>
                                    <Layout Behavior="Auto">
                                    </Layout>
                                </Labels>
                            </Z2>
                        </Axis>
                        <TitleLeft FontColor="0, 0, 192" Extent="33" Location="Left" Visible="True">
                        </TitleLeft>
                        <Effects>
                            <Effects>
                                <igchartprop:GradientEffect Style="backwarddiagonal" />
                                <igchartprop:StrokeEffect StrokeOpacity="255" StrokeWidth="0" />
                            </Effects>
                        </Effects>
                        <TitleRight FontColor="0, 0, 192" Extent="33" Location="Right" Visible="True">
                            <Margins Bottom="0" Left="0" Right="0" Top="0" />
                        </TitleRight>
                        <TitleTop Font="Arial, 12pt, style=Bold" FontColor="204, 102, 0" Text="Piezas Esmaltadas"
                            Extent="30" Flip="True" HorizontalAlign="Center">
                            <Margins Bottom="0" Left="0" Right="0" Top="0" />
                        </TitleTop>
                        <TitleBottom Font="Lucida Console, 10.2pt" FontColor="0, 0, 192" Text="Copyright Infragistics Inc."
                            Visible="False" Extent="33" Location="Bottom">
                        </TitleBottom>
                        
                        <Border Color="134, 134, 134" Thickness="0" />
                        
                        <Tooltips BackColor="192, 192, 255" Font-Bold="False" Font-Italic="False" Font-Overline="False"
                            Font-Strikeout="False" Font-Underline="False" FontColor="Navy" EnableFadingEffect="True" />
                    </igchart:UltraChart>
                </td>
                <td valign="top" align="left" class="style2">
                   <igchart:UltraChart ID="ucEmpaque" runat="server" Version="9.1" BorderColor="#868686"
                        BorderWidth="0px" Height="170px" BackColor="#FFFFFF" ChartType="ScatterChart"
                        ImageUrl="" Transform3D-Scale="94" BackgroundImageStyle="Centered" TextRenderingHint="SingleBitPerPixelGridFit"
                        Width="317px">
                        <Legend BackgroundColor="Transparent" BorderThickness="0" Font="Calibri, 9pt" 
                            Visible="True"></Legend>
                        <ColorModel AlphaLevel="247" ColorBegin="107, 150, 224" 
                            ColorEnd="107, 150, 224" ModelStyle="CustomSkin" Scaling="Increasing">
                            <Skin>
                                <PEs>
                                    <igchartprop:PaintElement ElementType="Gradient" Fill="47, 123, 214" FillGradientStyle="Vertical"
                                        FillStopColor="29, 82, 145" Stroke="29, 82, 145" />
                                    <igchartprop:PaintElement Fill="Purple" Stroke="ActiveCaption" />
                                    <igchartprop:PaintElement ElementType="Gradient" Fill="Brown" />
                                </PEs>
                            </Skin>
                        </ColorModel>
                        <ScatterChart ConnectWithLines="True" IconSize="Small" LineAppearance-Thickness="2" Icon="Diamond">
                        </ScatterChart>
                        <Axis>
                            <PE Fill="White" StrokeWidth="0" />
                            <X LineThickness="1" TickmarkInterval="5" TickmarkStyle="Smart" Visible="True" Extent="20"
                                LineColor="134, 134, 134">
                                <MajorGridLines AlphaLevel="255" Color="Gainsboro" DrawStyle="Dot" Thickness="1"
                                    Visible="False" />
                                <MinorGridLines AlphaLevel="255" Color="LightGray" DrawStyle="Dot" Thickness="1"
                                    Visible="False" />
                                <Labels Font="Arial, 7pt" HorizontalAlign="Near"
                                    ItemFormatString="&lt;DATA_VALUE:00.##&gt;" Orientation="VerticalLeftFacing"
                                    VerticalAlign="Center">
                                    <SeriesLabels Font="Arial, 7pt" HorizontalAlign="Near"
                                        Orientation="Horizontal" VerticalAlign="Center">
                                        <Layout Behavior="Auto">
                                        </Layout>
                                    </SeriesLabels>
                                    <Layout Behavior="Auto">
                                    </Layout>
                                </Labels>
                            </X>
                            <Y LineThickness="1" TickmarkInterval="40" TickmarkStyle="Smart" Visible="True"
                                Extent="20" LineColor="134, 134, 134">
                                <MajorGridLines AlphaLevel="100" Color="134, 134, 134" DrawStyle="Dot" Thickness="1"
                                    Visible="True" />
                                <MinorGridLines AlphaLevel="255" Color="LightGray" DrawStyle="Dot" Thickness="1"
                                    Visible="False" />
                                <Labels Font="Arial, 7pt" HorizontalAlign="Far"
                                    ItemFormatString="&lt;DATA_VALUE:00.##&gt;" Orientation="Horizontal" 
                                    VerticalAlign="Center">
                                    <SeriesLabels Font="Arial, 7pt" FontColor="DimGray" HorizontalAlign="Far" Orientation="Horizontal"
                                        VerticalAlign="Center">
                                        <Layout Behavior="Auto">
                                        </Layout>
                                    </SeriesLabels>
                                    <Layout Behavior="Auto">
                                    </Layout>
                                </Labels>
                            </Y>
                            <Y2 LineThickness="1" TickmarkInterval="40" TickmarkStyle="Smart" 
                                Visible="False">
                                <MajorGridLines AlphaLevel="255" Color="Gainsboro" DrawStyle="Dot" Thickness="1"
                                    Visible="True" />
                                <MinorGridLines AlphaLevel="255" Color="LightGray" DrawStyle="Dot" Thickness="1"
                                    Visible="False" />
                                <Labels Font="Verdana, 7pt" FontColor="Gray" HorizontalAlign="Near" ItemFormatString="&lt;DATA_VALUE:00.##&gt;"
                                    Orientation="Horizontal" VerticalAlign="Center">
                                    <SeriesLabels Font="Verdana, 7pt" FontColor="Gray" HorizontalAlign="Near" Orientation="Horizontal"
                                        VerticalAlign="Center" FormatString="">
                                        <Layout Behavior="Auto">
                                        </Layout>
                                    </SeriesLabels>
                                    <Layout Behavior="Auto">
                                    </Layout>
                                </Labels>
                            </Y2>
                            <X2 LineThickness="1" TickmarkInterval="5" TickmarkStyle="Smart" 
                                Visible="False">
                                <MajorGridLines AlphaLevel="255" Color="Gainsboro" DrawStyle="Dot" Thickness="1"
                                    Visible="True" />
                                <MinorGridLines AlphaLevel="255" Color="LightGray" DrawStyle="Dot" Thickness="1"
                                    Visible="False" />
                                <Labels Font="Verdana, 7pt" FontColor="Gray" HorizontalAlign="Near" ItemFormatString="&lt;DATA_VALUE:00.##&gt;"
                                    Orientation="VerticalLeftFacing" VerticalAlign="Center">
                                    <SeriesLabels Font="Verdana, 7pt" FontColor="Gray" HorizontalAlign="Near" Orientation="VerticalLeftFacing"
                                        VerticalAlign="Center">
                                        <Layout Behavior="Auto">
                                        </Layout>
                                    </SeriesLabels>
                                    <Layout Behavior="Auto">
                                    </Layout>
                                </Labels>
                            </X2>
                            <Z LineThickness="1" TickmarkInterval="0" TickmarkStyle="Smart" Visible="False">
                                <MajorGridLines AlphaLevel="255" Color="Gainsboro" DrawStyle="Dot" Thickness="1"
                                    Visible="True" />
                                <MinorGridLines AlphaLevel="255" Color="LightGray" DrawStyle="Dot" Thickness="1"
                                    Visible="False" />
                                <Labels Font="Verdana, 7pt" FontColor="DimGray" HorizontalAlign="Near" ItemFormatString=""
                                    Orientation="Horizontal" VerticalAlign="Center">
                                    <SeriesLabels Font="Verdana, 7pt" FontColor="DimGray" HorizontalAlign="Near" Orientation="Horizontal"
                                        VerticalAlign="Center">
                                        <Layout Behavior="Auto">
                                        </Layout>
                                    </SeriesLabels>
                                    <Layout Behavior="Auto">
                                    </Layout>
                                </Labels>
                            </Z>
                            <Z2 LineThickness="1" TickmarkInterval="0" TickmarkStyle="Smart" Visible="False">
                                <MajorGridLines AlphaLevel="255" Color="Gainsboro" DrawStyle="Dot" Thickness="1"
                                    Visible="True" />
                                <MinorGridLines AlphaLevel="255" Color="LightGray" DrawStyle="Dot" Thickness="1"
                                    Visible="False" />
                                <Labels Font="Verdana, 7pt" FontColor="Gray" HorizontalAlign="Near" ItemFormatString=""
                                    Orientation="Horizontal" VerticalAlign="Center">
                                    <SeriesLabels Font="Verdana, 7pt" FontColor="Gray" HorizontalAlign="Near" Orientation="Horizontal"
                                        VerticalAlign="Center">
                                        <Layout Behavior="Auto">
                                        </Layout>
                                    </SeriesLabels>
                                    <Layout Behavior="Auto">
                                    </Layout>
                                </Labels>
                            </Z2>
                        </Axis>
                        <TitleLeft FontColor="0, 0, 192" Extent="33" Location="Left" Visible="True">
                        </TitleLeft>
                        <Effects>
                            <Effects>
                                <igchartprop:GradientEffect Style="backwarddiagonal" />
                                <igchartprop:StrokeEffect StrokeOpacity="255" StrokeWidth="0" />
                            </Effects>
                        </Effects>
                        <TitleRight FontColor="0, 0, 192" Extent="33" Location="Right" Visible="True">
                            <Margins Bottom="0" Left="0" Right="0" Top="0" />
                        </TitleRight>
                        <TitleTop Font="Arial, 12pt, style=Bold" FontColor="204, 102, 0" Text="Piezas Empacadas"
                            Extent="30" Flip="True" HorizontalAlign="Center">
                            <Margins Bottom="0" Left="0" Right="0" Top="0" />
                        </TitleTop>
                        <TitleBottom Font="Lucida Console, 10.2pt" FontColor="0, 0, 192" Text="Copyright Infragistics Inc."
                            Visible="False" Extent="33" Location="Bottom">
                        </TitleBottom>
                        
                        <Border Color="134, 134, 134" Thickness="0" />
                        
                        <Tooltips BackColor="192, 192, 255" Font-Bold="False" Font-Italic="False" Font-Overline="False"
                            Font-Strikeout="False" Font-Underline="False" FontColor="Navy" EnableFadingEffect="True" />
                    </igchart:UltraChart>
                </td>
                <td rowspan="2" valign="top">
                    <div style="float: left; position: relative; overflow: auto; height: 330px; width: 65%; margin-left:10%;
                        border: 1px solid #EDF1CE; -moz-border-radius: 10px; border-radius: 10px; background: #EDF1CE;
                        top: 0px; left: 0px;" class="hidden">
                        <div class="eventos" style="border-bottom: 1px solid #F5DA81; margin-left: 10px;
                            width: 90%; margin-top: 10px; font:italic bold 14px Georgia, Serif; color:#506599">
                            <p>
                                Eventos</p>
                        </div>
                        <div class="listEventos">
                            <ul id="uEventos" runat="server">
                                <li><span>Planta 1: </span>
                                    <p>
                                        Mañana inicia trabajo</p>
                                </li>
                            </ul>
                        </div>
                    </div>
                </td>
            </tr>
            <tr>
                <td valign="top" align="left" class="style3">
                    <igchart:UltraChart ID="ucFuego1" runat="server" Version="9.1" BorderColor="#868686"
                        BorderWidth="0px" Height="170px" BackColor="#FFFFFF" ChartType="ScatterChart"
                        ImageUrl="" Transform3D-Scale="94" BackgroundImageStyle="Centered" TextRenderingHint="SingleBitPerPixelGridFit"
                        Width="317px">
                        <Legend BackgroundColor="Transparent" BorderThickness="0" Font="Calibri, 9pt" 
                            Visible="True"></Legend>
                        <ColorModel AlphaLevel="247" ColorBegin="107, 150, 224" 
                            ColorEnd="107, 150, 224" ModelStyle="CustomSkin" Scaling="Increasing">
                            <Skin>
                                <PEs>
                                    <igchartprop:PaintElement ElementType="Gradient" Fill="47, 123, 214" FillGradientStyle="Vertical"
                                        FillStopColor="29, 82, 145" Stroke="29, 82, 145" />
                                    <igchartprop:PaintElement Fill="Purple" Stroke="ActiveCaption" />
                                    <igchartprop:PaintElement ElementType="Gradient" Fill="Brown" />
                                </PEs>
                            </Skin>
                        </ColorModel>
                        <ScatterChart ConnectWithLines="True" IconSize="Small" LineAppearance-Thickness="2" Icon="Diamond">
                        </ScatterChart>
                        <Axis>
                            <PE Fill="White" StrokeWidth="0" />
                            <X LineThickness="1" TickmarkInterval="10" TickmarkStyle="Smart" Visible="True" Extent="20"
                                LineColor="134, 134, 134">
                                <MajorGridLines AlphaLevel="255" Color="Gainsboro" DrawStyle="Dot" Thickness="1"
                                    Visible="False" />
                                <MinorGridLines AlphaLevel="255" Color="LightGray" DrawStyle="Dot" Thickness="1"
                                    Visible="False" />
                                <Labels Font="Arial, 7pt" HorizontalAlign="Near"
                                    ItemFormatString="&lt;DATA_VALUE:00.##&gt;" Orientation="VerticalLeftFacing"
                                    VerticalAlign="Center">
                                    <SeriesLabels Font="Arial, 7pt" HorizontalAlign="Near"
                                        Orientation="Horizontal" VerticalAlign="Center">
                                        <Layout Behavior="Auto">
                                        </Layout>
                                    </SeriesLabels>
                                    <Layout Behavior="Auto">
                                    </Layout>
                                </Labels>
                            </X>
                            <Y LineThickness="1" TickmarkInterval="40" TickmarkStyle="Smart" Visible="True"
                                Extent="20" LineColor="134, 134, 134">
                                <MajorGridLines AlphaLevel="100" Color="134, 134, 134" DrawStyle="Dot" Thickness="1"
                                    Visible="True" />
                                <MinorGridLines AlphaLevel="255" Color="LightGray" DrawStyle="Dot" Thickness="1"
                                    Visible="False" />
                                <Labels Font="Arial, 7pt" HorizontalAlign="Far"
                                    ItemFormatString="&lt;DATA_VALUE:00.##&gt;" Orientation="Horizontal" 
                                    VerticalAlign="Center">
                                    <SeriesLabels Font="Arial, 7pt" FontColor="DimGray" HorizontalAlign="Far" Orientation="Horizontal"
                                        VerticalAlign="Center">
                                        <Layout Behavior="Auto">
                                        </Layout>
                                    </SeriesLabels>
                                    <Layout Behavior="Auto">
                                    </Layout>
                                </Labels>
                            </Y>
                            <Y2 LineThickness="1" TickmarkInterval="40" TickmarkStyle="Smart" 
                                Visible="False">
                                <MajorGridLines AlphaLevel="255" Color="Gainsboro" DrawStyle="Dot" Thickness="1"
                                    Visible="True" />
                                <MinorGridLines AlphaLevel="255" Color="LightGray" DrawStyle="Dot" Thickness="1"
                                    Visible="False" />
                                <Labels Font="Verdana, 7pt" FontColor="Gray" HorizontalAlign="Near" ItemFormatString="&lt;DATA_VALUE:00.##&gt;"
                                    Orientation="Horizontal" VerticalAlign="Center">
                                    <SeriesLabels Font="Verdana, 7pt" FontColor="Gray" HorizontalAlign="Near" Orientation="Horizontal"
                                        VerticalAlign="Center" FormatString="">
                                        <Layout Behavior="Auto">
                                        </Layout>
                                    </SeriesLabels>
                                    <Layout Behavior="Auto">
                                    </Layout>
                                </Labels>
                            </Y2>
                            <X2 LineThickness="1" TickmarkInterval="10" TickmarkStyle="Smart" Visible="False">
                                <MajorGridLines AlphaLevel="255" Color="Gainsboro" DrawStyle="Dot" Thickness="1"
                                    Visible="True" />
                                <MinorGridLines AlphaLevel="255" Color="LightGray" DrawStyle="Dot" Thickness="1"
                                    Visible="False" />
                                <Labels Font="Verdana, 7pt" FontColor="Gray" HorizontalAlign="Near" ItemFormatString="&lt;DATA_VALUE:00.##&gt;"
                                    Orientation="VerticalLeftFacing" VerticalAlign="Center">
                                    <SeriesLabels Font="Verdana, 7pt" FontColor="Gray" HorizontalAlign="Near" Orientation="VerticalLeftFacing"
                                        VerticalAlign="Center">
                                        <Layout Behavior="Auto">
                                        </Layout>
                                    </SeriesLabels>
                                    <Layout Behavior="Auto">
                                    </Layout>
                                </Labels>
                            </X2>
                            <Z LineThickness="1" TickmarkInterval="0" TickmarkStyle="Smart" Visible="False">
                                <MajorGridLines AlphaLevel="255" Color="Gainsboro" DrawStyle="Dot" Thickness="1"
                                    Visible="True" />
                                <MinorGridLines AlphaLevel="255" Color="LightGray" DrawStyle="Dot" Thickness="1"
                                    Visible="False" />
                                <Labels Font="Verdana, 7pt" FontColor="DimGray" HorizontalAlign="Near" ItemFormatString=""
                                    Orientation="Horizontal" VerticalAlign="Center">
                                    <SeriesLabels Font="Verdana, 7pt" FontColor="DimGray" HorizontalAlign="Near" Orientation="Horizontal"
                                        VerticalAlign="Center">
                                        <Layout Behavior="Auto">
                                        </Layout>
                                    </SeriesLabels>
                                    <Layout Behavior="Auto">
                                    </Layout>
                                </Labels>
                            </Z>
                            <Z2 LineThickness="1" TickmarkInterval="0" TickmarkStyle="Smart" Visible="False">
                                <MajorGridLines AlphaLevel="255" Color="Gainsboro" DrawStyle="Dot" Thickness="1"
                                    Visible="True" />
                                <MinorGridLines AlphaLevel="255" Color="LightGray" DrawStyle="Dot" Thickness="1"
                                    Visible="False" />
                                <Labels Font="Verdana, 7pt" FontColor="Gray" HorizontalAlign="Near" ItemFormatString=""
                                    Orientation="Horizontal" VerticalAlign="Center">
                                    <SeriesLabels Font="Verdana, 7pt" FontColor="Gray" HorizontalAlign="Near" Orientation="Horizontal"
                                        VerticalAlign="Center">
                                        <Layout Behavior="Auto">
                                        </Layout>
                                    </SeriesLabels>
                                    <Layout Behavior="Auto">
                                    </Layout>
                                </Labels>
                            </Z2>
                        </Axis>
                        <TitleLeft FontColor="0, 0, 192" Extent="33" Location="Left" Visible="True">
                        </TitleLeft>
                        <Effects>
                            <Effects>
                                <igchartprop:GradientEffect Style="backwarddiagonal" />
                                <igchartprop:StrokeEffect StrokeOpacity="255" StrokeWidth="0" />
                            </Effects>
                        </Effects>
                        <TitleRight FontColor="0, 0, 192" Extent="33" Location="Right" Visible="True">
                            <Margins Bottom="0" Left="0" Right="0" Top="0" />
                        </TitleRight>
                        <TitleTop Font="Arial, 12pt, style=Bold" FontColor="204, 102, 0" Text="Piezas Fuego 1"
                            Extent="30" Flip="True" HorizontalAlign="Center">
                            <Margins Bottom="0" Left="0" Right="0" Top="0" />
                        </TitleTop>
                        <TitleBottom Font="Lucida Console, 10.2pt" FontColor="0, 0, 192" Text="Copyright Infragistics Inc."
                            Visible="False" Extent="33" Location="Bottom">
                        </TitleBottom>
                        
                        <Border Color="134, 134, 134" Thickness="0" />
                        
                        <Tooltips BackColor="192, 192, 255" Font-Bold="False" Font-Italic="False" Font-Overline="False"
                            Font-Strikeout="False" Font-Underline="False" FontColor="Navy" EnableFadingEffect="True" />
                    </igchart:UltraChart>
                </td>
                <td valign="top" align="left" class="style4">
                <igchart:UltraChart ID="ucFuego2" runat="server" Version="9.1" BorderColor="#868686"
                        BorderWidth="0px" Height="170px" BackColor="#FFFFFF" ChartType="ScatterChart"
                        ImageUrl="" Transform3D-Scale="94" BackgroundImageStyle="Centered" TextRenderingHint="SingleBitPerPixelGridFit"
                        Width="317px">
                        <Legend BackgroundColor="Transparent" BorderThickness="0" Font="Calibri, 9pt" 
                            Visible="True"></Legend>
                        <ColorModel AlphaLevel="247" ColorBegin="107, 150, 224" 
                            ColorEnd="107, 150, 224" ModelStyle="CustomSkin" Scaling="Increasing">
                            <Skin>
                                <PEs>
                                    <igchartprop:PaintElement ElementType="Gradient" Fill="47, 123, 214" FillGradientStyle="Vertical"
                                        FillStopColor="29, 82, 145" Stroke="29, 82, 145" />
                                    <igchartprop:PaintElement Fill="Purple" Stroke="ActiveCaption" />
                                    <igchartprop:PaintElement ElementType="Gradient" Fill="Brown" />
                                </PEs>
                            </Skin>
                        </ColorModel>
                        <ScatterChart ConnectWithLines="True" IconSize="Small" LineAppearance-Thickness="2" Icon="Diamond">
                        </ScatterChart>
                        <Axis>
                            <PE Fill="White" StrokeWidth="0" />
                            <X LineThickness="1" TickmarkInterval="10" TickmarkStyle="Smart" Visible="True" Extent="20"
                                LineColor="134, 134, 134">
                                <MajorGridLines AlphaLevel="255" Color="Gainsboro" DrawStyle="Dot" Thickness="1"
                                    Visible="False" />
                                <MinorGridLines AlphaLevel="255" Color="LightGray" DrawStyle="Dot" Thickness="1"
                                    Visible="False" />
                                <Labels Font="Arial, 7pt" HorizontalAlign="Near"
                                    ItemFormatString="&lt;DATA_VALUE:00.##&gt;" Orientation="VerticalLeftFacing"
                                    VerticalAlign="Center">
                                    <SeriesLabels Font="Arial, 7pt" HorizontalAlign="Near"
                                        Orientation="Horizontal" VerticalAlign="Center">
                                        <Layout Behavior="Auto">
                                        </Layout>
                                    </SeriesLabels>
                                    <Layout Behavior="Auto">
                                    </Layout>
                                </Labels>
                            </X>
                            <Y LineThickness="1" TickmarkInterval="40" TickmarkStyle="Smart" Visible="True"
                                Extent="20" LineColor="134, 134, 134">
                                <MajorGridLines AlphaLevel="100" Color="134, 134, 134" DrawStyle="Dot" Thickness="1"
                                    Visible="True" />
                                <MinorGridLines AlphaLevel="255" Color="LightGray" DrawStyle="Dot" Thickness="1"
                                    Visible="False" />
                                <Labels Font="Arial, 7pt" HorizontalAlign="Far"
                                    ItemFormatString="&lt;DATA_VALUE:00.##&gt;" Orientation="Horizontal" 
                                    VerticalAlign="Center">
                                    <SeriesLabels Font="Arial, 7pt" FontColor="DimGray" HorizontalAlign="Far" Orientation="Horizontal"
                                        VerticalAlign="Center">
                                        <Layout Behavior="Auto">
                                        </Layout>
                                    </SeriesLabels>
                                    <Layout Behavior="Auto">
                                    </Layout>
                                </Labels>
                            </Y>
                            <Y2 LineThickness="1" TickmarkInterval="40" TickmarkStyle="Smart" 
                                Visible="False">
                                <MajorGridLines AlphaLevel="255" Color="Gainsboro" DrawStyle="Dot" Thickness="1"
                                    Visible="True" />
                                <MinorGridLines AlphaLevel="255" Color="LightGray" DrawStyle="Dot" Thickness="1"
                                    Visible="False" />
                                <Labels Font="Verdana, 7pt" FontColor="Gray" HorizontalAlign="Near" ItemFormatString="&lt;DATA_VALUE:00.##&gt;"
                                    Orientation="Horizontal" VerticalAlign="Center">
                                    <SeriesLabels Font="Verdana, 7pt" FontColor="Gray" HorizontalAlign="Near" Orientation="Horizontal"
                                        VerticalAlign="Center" FormatString="">
                                        <Layout Behavior="Auto">
                                        </Layout>
                                    </SeriesLabels>
                                    <Layout Behavior="Auto">
                                    </Layout>
                                </Labels>
                            </Y2>
                            <X2 LineThickness="1" TickmarkInterval="10" TickmarkStyle="Smart" Visible="False">
                                <MajorGridLines AlphaLevel="255" Color="Gainsboro" DrawStyle="Dot" Thickness="1"
                                    Visible="True" />
                                <MinorGridLines AlphaLevel="255" Color="LightGray" DrawStyle="Dot" Thickness="1"
                                    Visible="False" />
                                <Labels Font="Verdana, 7pt" FontColor="Gray" HorizontalAlign="Near" ItemFormatString="&lt;DATA_VALUE:00.##&gt;"
                                    Orientation="VerticalLeftFacing" VerticalAlign="Center">
                                    <SeriesLabels Font="Verdana, 7pt" FontColor="Gray" HorizontalAlign="Near" Orientation="VerticalLeftFacing"
                                        VerticalAlign="Center">
                                        <Layout Behavior="Auto">
                                        </Layout>
                                    </SeriesLabels>
                                    <Layout Behavior="Auto">
                                    </Layout>
                                </Labels>
                            </X2>
                            <Z LineThickness="1" TickmarkInterval="0" TickmarkStyle="Smart" Visible="False">
                                <MajorGridLines AlphaLevel="255" Color="Gainsboro" DrawStyle="Dot" Thickness="1"
                                    Visible="True" />
                                <MinorGridLines AlphaLevel="255" Color="LightGray" DrawStyle="Dot" Thickness="1"
                                    Visible="False" />
                                <Labels Font="Verdana, 7pt" FontColor="DimGray" HorizontalAlign="Near" ItemFormatString=""
                                    Orientation="Horizontal" VerticalAlign="Center">
                                    <SeriesLabels Font="Verdana, 7pt" FontColor="DimGray" HorizontalAlign="Near" Orientation="Horizontal"
                                        VerticalAlign="Center">
                                        <Layout Behavior="Auto">
                                        </Layout>
                                    </SeriesLabels>
                                    <Layout Behavior="Auto">
                                    </Layout>
                                </Labels>
                            </Z>
                            <Z2 LineThickness="1" TickmarkInterval="0" TickmarkStyle="Smart" Visible="False">
                                <MajorGridLines AlphaLevel="255" Color="Gainsboro" DrawStyle="Dot" Thickness="1"
                                    Visible="True" />
                                <MinorGridLines AlphaLevel="255" Color="LightGray" DrawStyle="Dot" Thickness="1"
                                    Visible="False" />
                                <Labels Font="Verdana, 7pt" FontColor="Gray" HorizontalAlign="Near" ItemFormatString=""
                                    Orientation="Horizontal" VerticalAlign="Center">
                                    <SeriesLabels Font="Verdana, 7pt" FontColor="Gray" HorizontalAlign="Near" Orientation="Horizontal"
                                        VerticalAlign="Center">
                                        <Layout Behavior="Auto">
                                        </Layout>
                                    </SeriesLabels>
                                    <Layout Behavior="Auto">
                                    </Layout>
                                </Labels>
                            </Z2>
                        </Axis>
                        <TitleLeft FontColor="0, 0, 192" Extent="33" Location="Left" Visible="True">
                        </TitleLeft>
                        <Effects>
                            <Effects>
                                <igchartprop:GradientEffect Style="backwarddiagonal" />
                                <igchartprop:StrokeEffect StrokeOpacity="255" StrokeWidth="0" />
                            </Effects>
                        </Effects>
                        <TitleRight FontColor="0, 0, 192" Extent="33" Location="Right" Visible="True">
                            <Margins Bottom="0" Left="0" Right="0" Top="0" />
                        </TitleRight>
                        <TitleTop Font="Arial, 12pt, style=Bold" FontColor="204, 102, 0" Text="Piezas Fuego 2"
                            Extent="30" Flip="True" HorizontalAlign="Center">
                            <Margins Bottom="0" Left="0" Right="0" Top="0" />
                        </TitleTop>
                        <TitleBottom Font="Lucida Console, 10.2pt" FontColor="0, 0, 192" Text="Copyright Infragistics Inc."
                            Visible="False" Extent="33" Location="Bottom">
                        </TitleBottom>
                        
                        <Border Color="134, 134, 134" Thickness="0" />
                        
                        <Tooltips BackColor="192, 192, 255" Font-Bold="False" Font-Italic="False" Font-Overline="False"
                            Font-Strikeout="False" Font-Underline="False" FontColor="Navy" EnableFadingEffect="True" />
                    </igchart:UltraChart>
                </td>
                <td align="left" class="style5">
                </td>
            </tr>
            <tr>
                <td style="margin-top: 30px">
                    <igtbl:UltraWebGrid ID="uwgSituacionActual" runat="server" Height="154px" Width="325px">
                        <Bands>
                            <igtbl:UltraGridBand>
                                <AddNewRow View="NotSet" Visible="NotSet">
                                </AddNewRow>
                            </igtbl:UltraGridBand>
                        </Bands>
                        <DisplayLayout AllowColSizingDefault="Free" ColWidthDefault="77px" AllowSortingDefault="OnClient" BorderCollapseDefault="Separate"
                            HeaderClickActionDefault="SortMulti" Name="UltraWebGrid1" RowHeightDefault="20px"
                            RowSelectorsDefault="No" SelectTypeRowDefault="Extended" StationaryMargins="Header"
                            TableLayout="Fixed" Version="4.00">
                            <FrameStyle BackColor="Window" BorderColor="InactiveCaption" Font-Names="Microsoft Sans Serif"
                                Font-Size="8.25pt" Height="154px" Width="325px">
                            </FrameStyle>
                            <RowAlternateStyleDefault BackColor="#CCCCCC" BorderStyle="Solid" BorderWidth="0px" BorderColor="Silver">
                            </RowAlternateStyleDefault>
                            <Pager MinimumPagesForDisplay="2">
                                <PagerStyle BackColor="LightGray">
                                    <BorderDetails ColorLeft="White" ColorTop="White" WidthLeft="1px" WidthTop="1px" />
                                </PagerStyle>
                            </Pager>
                            <EditCellStyleDefault BorderStyle="None" BorderWidth="0px">
                            </EditCellStyleDefault>
                            <FooterStyleDefault BackColor="LightGray" BorderStyle="Solid" BorderWidth="1px">
                                <BorderDetails ColorLeft="White" ColorTop="White" WidthLeft="1px" WidthTop="1px" />
                            </FooterStyleDefault>
                            <HeaderStyleDefault BackColor="#CC0000" BorderStyle="Solid" HorizontalAlign="Left"
                                Font-Bold="True" ForeColor="White">
                                <BorderDetails ColorLeft="White" ColorTop="White" WidthLeft="1px" WidthTop="1px" />
                            </HeaderStyleDefault>
                            <RowStyleDefault BackColor="Window" BorderColor="Silver" BorderStyle="Solid" BorderWidth="0px"
                                Font-Names="Microsoft Sans Serif" Font-Size="8.25pt">
                                <Padding Left="3px" />
                                <BorderDetails ColorLeft="Window" ColorTop="Window" />
                            </RowStyleDefault>
                            <GroupByRowStyleDefault BackColor="Control" BorderColor="Window">
                            </GroupByRowStyleDefault>
                            <AddNewBox Hidden="False">
                                <BoxStyle BackColor="Window" BorderColor="InactiveCaption" BorderStyle="Solid" BorderWidth="1px">
                                    <BorderDetails ColorLeft="White" ColorTop="White" WidthLeft="1px" WidthTop="1px" />
                                </BoxStyle>
                            </AddNewBox>
                            <ActivationObject BorderColor="" BorderWidth="">
                            </ActivationObject>
                            <FilterOptionsDefault>
                                <FilterDropDownStyle BackColor="White" CustomRules="overflow:auto;" Font-Names="Verdana,Arial,Helvetica,sans-serif"
                                    Font-Size="11px" Height="300px" Width="200px">
                                    <Padding Left="2px" />
                                </FilterDropDownStyle>
                                <FilterHighlightRowStyle BackColor="#151C55" ForeColor="White">
                                </FilterHighlightRowStyle>
                                <FilterOperandDropDownStyle BackColor="White" BorderColor="Silver" BorderStyle="Solid"
                                    BorderWidth="1px" CustomRules="overflow:auto;" Font-Names="Verdana,Arial,Helvetica,sans-serif"
                                    Font-Size="11px">
                                    <Padding Left="2px" />
                                </FilterOperandDropDownStyle>
                            </FilterOptionsDefault>
                        </DisplayLayout>
                    </igtbl:UltraWebGrid>
                </td>
                <td valign="top" colspan="2" class="style2">
                    <igchart:UltraChart ID="ucDefectos" runat="server" Version="9.1" BorderColor="#868686"
                        BorderWidth="0px" Height="144px" BackColor="#FFFFFF" ChartType="ScatterChart"
                        ImageUrl="" Transform3D-Scale="94" BackgroundImageStyle="Centered" TextRenderingHint="SingleBitPerPixelGridFit"
                        Width="679px" BackgroundImageFileName="" EmptyChartText="Data Not Available. Please call UltraChart.Data.DataBind() after setting valid Data.DataSource"
                        Transform3D-EdgeSize="3" Transform3D-Outline="True" Transform3D-ZRotation="1">
                        <Legend BackgroundColor="Transparent" Font="Arial, 9pt" BorderThickness="0"
                            Visible="True"></Legend>
                        <ColorModel AlphaLevel="247" ColorBegin="107, 150, 224"  
                            ColorEnd="254, 189, 0" ModelStyle="CustomSkin"
                            Scaling="Increasing">
                            <Skin>
                                <PEs>
                                    <igchartprop:PaintElement ElementType="Gradient" Fill="47, 123, 214" FillGradientStyle="Vertical"
                                        FillStopColor="29, 82, 145" Stroke="29, 82, 145" />
                                    <igchartprop:PaintElement Fill="254, 189, 0" FillGradientStyle="Vertical"
                                    FillStopColor="252, 152, 18" Stroke="252, 152, 18" />
                                    <igchartprop:PaintElement Fill="Purple" Stroke="ActiveCaption" />
                                    <igchartprop:PaintElement ElementType="Gradient" Fill="YellowGreen" 
                                        FillGradientStyle="Vertical" />
                                </PEs>
                            </Skin>
                        </ColorModel>
                        <ScatterChart ConnectWithLines="True" IconSize="Small" LineAppearance-Thickness="2" Icon="Diamond" UseGroupByColumn="True">
                        </ScatterChart>
                        <Axis>
                            <PE Fill="White" StrokeWidth="0" />
                            <X LineThickness="1" TickmarkInterval="10" TickmarkStyle="Smart" Visible="True" Extent="20"
                                LineColor="134, 134, 134">
                                <MajorGridLines AlphaLevel="255" Color="Gainsboro" DrawStyle="Dot" Thickness="1"
                                    Visible="False" />
                                <MinorGridLines AlphaLevel="255" Color="LightGray" DrawStyle="Dot" Thickness="1"
                                    Visible="False" />
                                <Labels Font="Arial, 7pt" HorizontalAlign="Near"
                                    ItemFormatString="&lt;DATA_VALUE:00.##&gt;" Orientation="VerticalLeftFacing"
                                    VerticalAlign="Center">
                                    <SeriesLabels Font="Arial, 7pt" HorizontalAlign="Near"
                                        Orientation="Horizontal" VerticalAlign="Center">
                                        <Layout Behavior="Auto">
                                        </Layout>
                                    </SeriesLabels>
                                    <Layout Behavior="Auto">
                                    </Layout>
                                </Labels>
                            </X>
                            <Y LineThickness="1" TickmarkInterval="50" TickmarkStyle="Smart" Visible="True"
                                Extent="20" LineColor="134, 134, 134">
                                <MajorGridLines AlphaLevel="100" Color="134, 134, 134" DrawStyle="Dot" Thickness="1"
                                    Visible="True" />
                                <MinorGridLines AlphaLevel="255" Color="LightGray" DrawStyle="Dot" Thickness="1"
                                    Visible="False" />
                                <Labels Font="Arial, 7pt" HorizontalAlign="Far"
                                    ItemFormatString="&lt;DATA_VALUE:00.##&gt;" Orientation="Horizontal" 
                                    VerticalAlign="Center">
                                    <SeriesLabels Font="Arial, 7pt" FontColor="DimGray" HorizontalAlign="Far" Orientation="Horizontal"
                                        VerticalAlign="Center">
                                        <Layout Behavior="Auto">
                                        </Layout>
                                    </SeriesLabels>
                                    <Layout Behavior="Auto">
                                    </Layout>
                                </Labels>
                            </Y>
                            <Y2 LineThickness="1" TickmarkInterval="50" TickmarkStyle="Smart" Visible="False">
                                <MajorGridLines AlphaLevel="255" Color="Gainsboro" DrawStyle="Dot" Thickness="1"
                                    Visible="True" />
                                <MinorGridLines AlphaLevel="255" Color="LightGray" DrawStyle="Dot" Thickness="1"
                                    Visible="False" />
                                <Labels Font="Verdana, 7pt" FontColor="Gray" HorizontalAlign="Near" ItemFormatString="&lt;DATA_VALUE:00.##&gt;"
                                    Orientation="Horizontal" VerticalAlign="Center">
                                    <SeriesLabels Font="Verdana, 7pt" FontColor="Gray" HorizontalAlign="Near" Orientation="Horizontal"
                                        VerticalAlign="Center" FormatString="">
                                        <Layout Behavior="Auto">
                                        </Layout>
                                    </SeriesLabels>
                                    <Layout Behavior="Auto">
                                    </Layout>
                                </Labels>
                            </Y2>
                            <X2 LineThickness="1" TickmarkInterval="10" TickmarkStyle="Smart" 
                                Visible="False">
                                <MajorGridLines AlphaLevel="255" Color="Gainsboro" DrawStyle="Dot" Thickness="1"
                                    Visible="True" />
                                <MinorGridLines AlphaLevel="255" Color="LightGray" DrawStyle="Dot" Thickness="1"
                                    Visible="False" />
                                <Labels Font="Verdana, 7pt" FontColor="Gray" HorizontalAlign="Near" ItemFormatString="&lt;DATA_VALUE:00.##&gt;"
                                    Orientation="VerticalLeftFacing" VerticalAlign="Center">
                                    <SeriesLabels Font="Verdana, 7pt" FontColor="Gray" HorizontalAlign="Near" Orientation="VerticalLeftFacing"
                                        VerticalAlign="Center">
                                        <Layout Behavior="Auto">
                                        </Layout>
                                    </SeriesLabels>
                                    <Layout Behavior="Auto">
                                    </Layout>
                                </Labels>
                            </X2>
                            <Z LineThickness="1" TickmarkInterval="0" TickmarkStyle="Smart" Visible="False">
                                <MajorGridLines AlphaLevel="255" Color="Gainsboro" DrawStyle="Dot" Thickness="1"
                                    Visible="True" />
                                <MinorGridLines AlphaLevel="255" Color="LightGray" DrawStyle="Dot" Thickness="1"
                                    Visible="False" />
                                <Labels Font="Verdana, 7pt" FontColor="DimGray" HorizontalAlign="Near" ItemFormatString=""
                                    Orientation="Horizontal" VerticalAlign="Center">
                                    <SeriesLabels Font="Verdana, 7pt" FontColor="DimGray" HorizontalAlign="Near" Orientation="Horizontal"
                                        VerticalAlign="Center">
                                        <Layout Behavior="Auto">
                                        </Layout>
                                    </SeriesLabels>
                                    <Layout Behavior="Auto">
                                    </Layout>
                                </Labels>
                            </Z>
                            <Z2 LineThickness="1" TickmarkInterval="0" TickmarkStyle="Smart" Visible="False">
                                <MajorGridLines AlphaLevel="255" Color="Gainsboro" DrawStyle="Dot" Thickness="1"
                                    Visible="True" />
                                <MinorGridLines AlphaLevel="255" Color="LightGray" DrawStyle="Dot" Thickness="1"
                                    Visible="False" />
                                <Labels Font="Verdana, 7pt" FontColor="Gray" HorizontalAlign="Near" ItemFormatString=""
                                    Orientation="Horizontal" VerticalAlign="Center">
                                    <SeriesLabels Font="Verdana, 7pt" FontColor="Gray" HorizontalAlign="Near" Orientation="Horizontal"
                                        VerticalAlign="Center">
                                        <Layout Behavior="Auto">
                                        </Layout>
                                    </SeriesLabels>
                                    <Layout Behavior="Auto">
                                    </Layout>
                                </Labels>
                            </Z2>
                        </Axis>
                        <TitleLeft FontColor="0, 0, 192" Extent="33" Location="Left" Visible="True">
                        </TitleLeft>
                        <Effects>
                            <Effects>
                                <igchartprop:GradientEffect Style="backwarddiagonal" />
                                <igchartprop:StrokeEffect StrokeOpacity="255" StrokeWidth="0" />
                            </Effects>
                        </Effects>
                        <TitleRight FontColor="0, 0, 192" Extent="33" Location="Right" Visible="True">
                            <Margins Bottom="0" Left="0" Right="0" Top="0" />
                        </TitleRight>
                        <Data>
                            <EmptyStyle EnableLineStyle="True">
                                <LineStyle MidPointAnchors="True" />
                            </EmptyStyle>
                        </Data>
                        <TitleTop Font="Arial, 12pt, style=Bold" FontColor="204, 102, 0" Text="Tendencia de Defectos"
                            Extent="30" Flip="True" HorizontalAlign="Center">
                            <Margins Bottom="0" Left="0" Right="0" Top="0" />
                        </TitleTop>
                        <TitleBottom Font="Lucida Console, 10.2pt" FontColor="0, 0, 192" Text="Copyright Infragistics Inc."
                            Visible="False" Extent="33" Location="Bottom">
                        </TitleBottom>
                        
                        <Border Color="134, 134, 134" Thickness="0" />
                        
                        <Tooltips BackColor="192, 192, 255" Font-Bold="False" Font-Italic="False" Font-Overline="False"
                            Font-Strikeout="False" Font-Underline="False" FontColor="Navy" />
                    </igchart:UltraChart>
                </td>
            </tr>
            <tr>
                <td valign="top" colspan="3">
                    <igtbl:UltraWebGrid ID="uwgCapProdAvanceAcum" runat="server" Height="154px" Width="798px">
                        <Bands>
                            <igtbl:UltraGridBand>
                                <AddNewRow View="NotSet" Visible="NotSet">
                                </AddNewRow>
                            </igtbl:UltraGridBand>
                        </Bands>
                        <DisplayLayout AllowColSizingDefault="Free" AllowSortingDefault="OnClient" BorderCollapseDefault="Separate"
                            HeaderClickActionDefault="SortMulti" Name="UltraWebGrid1" RowHeightDefault="20px"
                            RowSelectorsDefault="No" SelectTypeRowDefault="Extended" StationaryMargins="Header"
                            TableLayout="Fixed" Version="4.00" >
                            <FrameStyle BackColor="Window" BorderColor="InactiveCaption" Font-Names="Microsoft Sans Serif"
                                Font-Size="7.25pt" Height="154px" Width="798px">
                            </FrameStyle>
                            <RowAlternateStyleDefault BackColor="#CCCCCC" BorderWidth="0px" BorderStyle="Solid" BorderColor="Silver">
                            </RowAlternateStyleDefault>
                            <Pager MinimumPagesForDisplay="2">
                                <PagerStyle BackColor="LightGray">
                                    <BorderDetails ColorLeft="White" ColorTop="White" WidthLeft="1px" WidthTop="1px" />
                                </PagerStyle>
                            </Pager>
                            <EditCellStyleDefault BorderStyle="None" BorderWidth="0px">
                            </EditCellStyleDefault>
                            <FooterStyleDefault BackColor="LightGray" BorderStyle="Solid" BorderWidth="1px">
                                <BorderDetails ColorLeft="White" ColorTop="White" WidthLeft="1px" WidthTop="1px" />
                            </FooterStyleDefault>
                            <HeaderStyleDefault BackColor="#CC0000" BorderStyle="Solid" HorizontalAlign="Left"
                                Font-Bold="True" Font-Size="10px" ForeColor="White">
                                <BorderDetails ColorLeft="White" ColorTop="White" WidthLeft="1px" WidthTop="1px" />
                            </HeaderStyleDefault>
                            <RowStyleDefault BackColor="Window" BorderColor="Silver" BorderStyle="Solid" BorderWidth="0px"
                                Font-Names="Microsoft Sans Serif" Font-Size="8.25pt">
                                <Padding Left="3px" />
                                <BorderDetails ColorLeft="Window" ColorTop="Window" />
                            </RowStyleDefault>
                            <GroupByRowStyleDefault BackColor="Control" BorderColor="Window">
                            </GroupByRowStyleDefault>
                            <AddNewBox Hidden="False">
                                <BoxStyle BackColor="Window" BorderColor="InactiveCaption" BorderStyle="Solid" BorderWidth="1px">
                                    <BorderDetails ColorLeft="White" ColorTop="White" WidthLeft="1px" WidthTop="1px" />
                                </BoxStyle>
                            </AddNewBox>
                            <ActivationObject BorderColor="" BorderWidth="">
                            </ActivationObject>
                            <FilterOptionsDefault>
                                <FilterDropDownStyle BackColor="White" CustomRules="overflow:auto;" Font-Names="Verdana,Arial,Helvetica,sans-serif"
                                    Font-Size="11px" Height="300px" Width="200px">
                                    <Padding Left="2px" />
                                </FilterDropDownStyle>
                                <FilterHighlightRowStyle BackColor="#151C55" ForeColor="White">
                                </FilterHighlightRowStyle>
                                <FilterOperandDropDownStyle BackColor="White" BorderColor="Silver" BorderStyle="Solid"
                                    BorderWidth="1px" CustomRules="overflow:auto;" Font-Names="Verdana,Arial,Helvetica,sans-serif"
                                    Font-Size="11px">
                                    <Padding Left="2px" />
                                </FilterOperandDropDownStyle>
                            </FilterOptionsDefault>
                        </DisplayLayout>
                    </igtbl:UltraWebGrid>
                </td>
            </tr>
        </table>
    </div>
    </form>
</body>
</html>
