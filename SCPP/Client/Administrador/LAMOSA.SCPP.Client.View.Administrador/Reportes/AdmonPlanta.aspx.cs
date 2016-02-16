using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using Infragistics.WebUI.Shared;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using Infragistics.Web.UI.ListControls;
using System.Collections.Generic;
using Infragistics.WebUI.UltraWebGrid;
using System.IO;
using System.ComponentModel;

using Infragistics.Shared;
using Infragistics.Excel;
using LAMOSA.SCPP.Server.BusinessEntity.Server;
using LAMOSA.SCPP.Server.BusinessEntity;
using BE = LAMOSA.SCPP.Server.BusinessEntity;
using SE = Common.SolutionEntityFramework;

namespace LAMOSA.SCPP.Client.View.Administrador.Reportes
{
    public partial class AdmonPlanta : ReporteBase
    {
        public DataTable workTable;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {

                Usuario user = (Usuario)Session["UserLogged"];
                if (user != null)
                {
                    String uri = Request.Url.LocalPath.ToString();
                    //CallBackManager.AddScriptBlock(this.Page, WebAsyncRefreshPanel1, "<script type='text/javascript'>ig_shared.getCBManager()._timeLimit = 60000;alert('ig_shared.getCBManager()._timeLimit');</script>");
                    
                    ddlPlanta.DataSource = new Combos().Get_Planta_RolCbo(user.CodRol);
                    ddlPlanta.DataTextField = "descripcionPlanta";
                    ddlPlanta.DataValueField = "ClavePlanta";
                    ddlPlanta.DataBind();
                    ddlPlanta.Items.Insert(0, new System.Web.UI.WebControls.ListItem("Todos", "0"));

                    ddlTipoArticulo.DataSource = new Combos().Get_TipoArticuloCbo();
                    ddlTipoArticulo.DataTextField = "DesTipoArticulo";
                    ddlTipoArticulo.DataValueField = "CodTipoArticulo";
                    ddlTipoArticulo.DataBind();
                    ddlTipoArticulo.Items.Insert(0, new System.Web.UI.WebControls.ListItem("Todos", "0"));


                    wdcFechaIni.Value = DateTime.Now;
                    wdcFechaFin.Value = DateTime.Now;

                    ddlTipoArticulo_SelectedIndexChanged(null, null);
                    ddlPlanta_SelectedIndexChanged(null, null);

                    //LlenaTabla();

                    bBuscar.Enabled = true;
                    //LExport.Visible = false;

                    bool editar = false;
                    foreach (ScreenPermission sp in new Actions().GetActionBySreen(user.CodRol, Request.Url.LocalPath))
                    {
                        switch (sp.ActionCode)
                        {
                            case 1: //Buscar
                                bBuscar.Enabled = true;
                                break;
                            case 2: //Exportar
                                LExport.Visible = true;
                                break;
                            case 3: //Nuevo

                                break;
                            case 4: //Editar
                                editar = true;
                                UltraWebGrid1.DisplayLayout.AllowUpdateDefault = AllowUpdate.RowTemplateOnly;
                                break;
                        }

                    }

                    if (editar == false)
                    {
                        UltraWebGrid1.DisplayLayout.AllowUpdateDefault = AllowUpdate.No;
                    }
                }

            }
        }
        protected void ddlTipoArticulo_SelectedIndexChanged(object sender, EventArgs e)
        {
            ddlModelo.Items.Clear();
            int cod_ta = Convert.ToInt32(ddlTipoArticulo.SelectedValue);
            if (cod_ta > 0)
            {
                ddlModelo.DataSource = new Combos().ObtenerArticulo(cod_ta);
                ddlModelo.DataTextField = "Descripcion";
                ddlModelo.DataValueField = "Codigo";
                ddlModelo.DataBind();
            }
            ddlModelo.Items.Insert(0, new System.Web.UI.WebControls.ListItem("Todos", "0"));
        }
        protected void LlenaTabla()
        {

            try
            {
                int planta = Convert.ToInt32(ddlPlanta.SelectedValue);
                int tipoArrticulo = Convert.ToInt32(ddlTipoArticulo.SelectedValue);
                int modelo = Convert.ToInt32(ddlModelo.SelectedValue);
                int centroTrabajo = Convert.ToInt32(ddlCentroTrabajo.SelectedValue);
                DateTime dtFechaInicial = (DateTime)wdcFechaIni.Value;
                DateTime dtFechaFinal = (DateTime)wdcFechaFin.Value;

                //UltraWebGrid1.Columns.Clear();
                this.UltraWebGrid1.DataSource = new ReportesB().Pisos(planta, tipoArrticulo, modelo, centroTrabajo, dtFechaInicial, dtFechaFinal);
                this.UltraWebGrid1.DataBind();
                if (UltraWebGrid1.Rows.Count > 30)
                {
                    UltraWebGrid1.Height = new Unit(360);
                }
                else
                {
                    UltraWebGrid1.Height = Unit.Empty;

                    UltraWebGrid1.Columns[16].Width = 100;
                    //UltraWebGrid1.Columns[17].Width = 100;
                }
                UltraWebGrid1.Columns[16].Width = 120;
                //UltraWebGrid1.Columns[17].Width = 135;
                //UltraWebGrid1.Columns[29].Width = 120;
                //UltraWebGrid1.Columns[30].Width = 135;
                //UltraWebGrid1.Columns[16].Header.Caption = "% PV Mes Ant.";
                //UltraWebGrid1.Columns[29].Header.Caption = "% PV Mes Ant.";


                //UltraGridBand band = UltraWebGrid1.Bands[0];
                //band.FooterStyle.BackColor = System.Drawing.Color.Gray;
                //band.ColFootersVisible = ShowMarginInfo.Yes;
                //band.Columns[0].Footer.Caption = "Total :";

                //band.Columns[1].Footer.Total = Infragistics.WebUI.UltraWebGrid.SummaryInfo.Sum;


            }
            catch { }

        }
        protected void UltraWebGrid1_InitializeLayout(object sender, Infragistics.WebUI.UltraWebGrid.LayoutEventArgs e)
        {

            ColumnHeader colHead;
            for (int i = 0; i < e.Layout.Bands[0].HeaderLayout.Count; i++)
            {
                colHead = e.Layout.Bands[0].HeaderLayout[i] as ColumnHeader;
                colHead.RowLayoutColumnInfo.OriginY = 1;
            }


            ColumnHeader ch = new ColumnHeader(true);
            ch.Caption = "Planta ___";
            ch.Style.HorizontalAlign = HorizontalAlign.Center;
            ch.RowLayoutColumnInfo.OriginX = 0;

            // This will set it on the first row of the HeaderLayout object 
            // The columns created by the control are all on the first row by default.
            ch.RowLayoutColumnInfo.OriginY = 0;
            // Add the newly created Header object to the HeaderLayout object
            e.Layout.Bands[0].HeaderLayout.Add(ch);

            // Expand the new column header to cover two columns.
            ch.RowLayoutColumnInfo.SpanX = 1;




            ColumnHeader ch2 = new ColumnHeader(true);
            ch2.Caption = "Vaciado";
            ch2.Style.HorizontalAlign = HorizontalAlign.Center;

            ch2.RowLayoutColumnInfo.OriginX = 1;

            // This will set it on the first row of the HeaderLayout object 
            // The columns created by the control are all on the first row by default.
            ch2.RowLayoutColumnInfo.OriginY = 0;
            // Add the newly created Header object to the HeaderLayout object
            e.Layout.Bands[0].HeaderLayout.Add(ch2);

            // Expand the new column header to cover two columns.
            /*ch.RowLayoutColumnInfo.SpanX = 3;*/
            ch2.RowLayoutColumnInfo.SpanX = 4;

            ColumnHeader ch3 = new ColumnHeader(true);
            ch3.Caption = "Revisado";
            ch3.Style.HorizontalAlign = HorizontalAlign.Center;

            ch3.RowLayoutColumnInfo.OriginX = 5;

            // This will set it on the first row of the HeaderLayout object 
            // The columns created by the control are all on the first row by default.
            ch3.RowLayoutColumnInfo.OriginY = 0;
            // Add the newly created Header object to the HeaderLayout object
            e.Layout.Bands[0].HeaderLayout.Add(ch3);

            // Expand the new column header to cover two columns.
            /*ch.RowLayoutColumnInfo.SpanX = 3;*/
            ch3.RowLayoutColumnInfo.SpanX = 3;

            ColumnHeader ch4 = new ColumnHeader(true);
            ch4.Caption = "Esmaltado";
            ch4.Style.HorizontalAlign = HorizontalAlign.Center;

            ch4.RowLayoutColumnInfo.OriginX = 8;

            // This will set it on the first row of the HeaderLayout object 
            // The columns created by the control are all on the first row by default.
            ch4.RowLayoutColumnInfo.OriginY = 0;
            // Add the newly created Header object to the HeaderLayout object
            e.Layout.Bands[0].HeaderLayout.Add(ch4);

            // Expand the new column header to cover two columns.
            /*ch.RowLayoutColumnInfo.SpanX = 3;*/
            ch4.RowLayoutColumnInfo.SpanX = 3;

            ColumnHeader ch5 = new ColumnHeader(true);
            ch5.Caption = "Hornos";
            ch5.Style.HorizontalAlign = HorizontalAlign.Center;

            ch5.RowLayoutColumnInfo.OriginX = 11;

            // This will set it on the first row of the HeaderLayout object 
            // The columns created by the control are all on the first row by default.
            ch5.RowLayoutColumnInfo.OriginY = 0;
            // Add the newly created Header object to the HeaderLayout object
            e.Layout.Bands[0].HeaderLayout.Add(ch5);

            // Expand the new column header to cover two columns.
            /*ch.RowLayoutColumnInfo.SpanX = 3;*/
            ch5.RowLayoutColumnInfo.SpanX = 3;

            ColumnHeader ch6 = new ColumnHeader(true);
            ch6.Caption = "Perdida en Verde";
            ch6.Style.HorizontalAlign = HorizontalAlign.Center;

            ch6.RowLayoutColumnInfo.OriginX = 14;

            // This will set it on the first row of the HeaderLayout object 
            // The columns created by the control are all on the first row by default.
            ch6.RowLayoutColumnInfo.OriginY = 0;
            // Add the newly created Header object to the HeaderLayout object
            e.Layout.Bands[0].HeaderLayout.Add(ch6);

            // Expand the new column header to cover two columns.
            /*ch.RowLayoutColumnInfo.SpanX = 3;*/
            ch6.RowLayoutColumnInfo.SpanX = 6;

            ColumnHeader ch7 = new ColumnHeader(true);
            ch7.Caption = "Clasificación";
            ch7.Style.HorizontalAlign = HorizontalAlign.Center;

            ch7.RowLayoutColumnInfo.OriginX = 20;

            // This will set it on the first row of the HeaderLayout object 
            // The columns created by the control are all on the first row by default.
            ch7.RowLayoutColumnInfo.OriginY = 0;
            // Add the newly created Header object to the HeaderLayout object
            e.Layout.Bands[0].HeaderLayout.Add(ch7);

            // Expand the new column header to cover two columns.
            /*ch.RowLayoutColumnInfo.SpanX = 3;*/
            ch7.RowLayoutColumnInfo.SpanX = 1;

            ColumnHeader ch8 = new ColumnHeader(true);
            ch8.Caption = "Perdida en Quemado";
            ch8.Style.HorizontalAlign = HorizontalAlign.Center;

            ch8.RowLayoutColumnInfo.OriginX = 21;

            // This will set it on the first row of the HeaderLayout object 
            // The columns created by the control are all on the first row by default.
            ch8.RowLayoutColumnInfo.OriginY = 0;
            // Add the newly created Header object to the HeaderLayout object
            e.Layout.Bands[0].HeaderLayout.Add(ch8);

            // Expand the new column header to cover two columns.
            /*ch.RowLayoutColumnInfo.SpanX = 3;*/
            ch8.RowLayoutColumnInfo.SpanX = 6;

            ColumnHeader ch9 = new ColumnHeader(true);
            ch9.Caption = "Entrega";
            ch9.Style.HorizontalAlign = HorizontalAlign.Center;

            ch9.RowLayoutColumnInfo.OriginX = 27;

            // This will set it on the first row of the HeaderLayout object 
            // The columns created by the control are all on the first row by default.
            ch9.RowLayoutColumnInfo.OriginY = 0;
            // Add the newly created Header object to the HeaderLayout object
            e.Layout.Bands[0].HeaderLayout.Add(ch9);

            // Expand the new column header to cover two columns.
            /*ch.RowLayoutColumnInfo.SpanX = 3;*/
            ch9.RowLayoutColumnInfo.SpanX = 4;



        }
        protected void bBuscar_Click(object sender, EventArgs e)
        {
            LlenaTabla();
        }
        protected void btnExporta_Click(object sender, EventArgs e)
        {
            //Metodo para Generar el Reporteint planta = Convert.ToInt32(ddlPlanta.SelectedValue);
            int planta = Convert.ToInt32(ddlPlanta.SelectedValue);
            int tipoArrticulo = Convert.ToInt32(ddlTipoArticulo.SelectedValue);
            int modelo = Convert.ToInt32(ddlModelo.SelectedValue);
            int centroTrabajo = Convert.ToInt32(ddlCentroTrabajo.SelectedValue);
            DateTime dtFechaInicial = (DateTime)wdcFechaIni.Value;
            DateTime dtFechaFinal = (DateTime)wdcFechaFin.Value;

            DataTable dt = new ReportesB().Pisos(planta, tipoArrticulo, modelo, centroTrabajo, dtFechaInicial, dtFechaFinal);
            DataSet dsReportXLS = new DataSet();
            dsReportXLS.Tables.Add(dt.Copy());
            ExportToExcel(dsReportXLS, 0, Response, nombre.Value);
        }
        protected void ddlPlanta_SelectedIndexChanged(object sender, EventArgs e)
        {
            ddlCentroTrabajo.Items.Clear();
            int iPlanta = Convert.ToInt32(this.ddlPlanta.SelectedItem.Value);
            if (iPlanta > 0)
            {
                List<BE.CentroTrabajo> l_CTCbo = new List<BE.CentroTrabajo>();
                foreach (SE.BaseSolutionEntity bse in new svcSCPP.SCPPClient().ObtenerCentroTrabajoCbo(iPlanta, -1))//Se esta mandando -1 porque no hay filtro por proceso.
                {
                    l_CTCbo.Add(bse as BE.CentroTrabajo);
                }
                // Enlazar datos al control.
                ddlCentroTrabajo.DataSource = l_CTCbo;
                ddlCentroTrabajo.DataValueField = "CodCentroTrabajo";
                ddlCentroTrabajo.DataTextField = "DesCentroTrabajo";
                ddlCentroTrabajo.DataBind();
            }
            ddlCentroTrabajo.Items.Insert(0, new System.Web.UI.WebControls.ListItem("Todos", "0"));
        }
    }
}
