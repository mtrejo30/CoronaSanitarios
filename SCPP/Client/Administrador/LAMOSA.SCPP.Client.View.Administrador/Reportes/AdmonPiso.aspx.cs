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
    public partial class AdmonPiso : ReporteBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                Usuario user = (Usuario)Session["UserLogged"];
                if (user != null)
                {
                    ddlPlanta.DataSource = new Combos().Get_Planta_RolCbo(user.CodRol);
                    ddlPlanta.DataTextField = "descripcionPlanta";
                    ddlPlanta.DataValueField = "ClavePlanta";
                    ddlPlanta.DataBind();
                    //ddlPlanta.Items.Insert(0, new System.Web.UI.WebControls.ListItem("Seleccione...", "0")); //Control de Cambio: Quitar Opcion 'Seleccionje...'
                    ddlPlanta.Items.Insert(0, new System.Web.UI.WebControls.ListItem("Todos", "0"));

                    ddlProcesoOrigen.DataSource = new Combos().ObtenerProcesoOrigen();
                    ddlProcesoOrigen.DataTextField = "DescProceso";
                    ddlProcesoOrigen.DataValueField = "CodigoProceso";
                    ddlProcesoOrigen.DataBind();
                    //ddlProceso.Items.Insert(0, new System.Web.UI.WebControls.ListItem("Seleccione...", "0"));//Control de Cambio: Quitar Opcion 'Seleccionje...'
                    //ddlProcesoOrigen.Items.Insert(0, new System.Web.UI.WebControls.ListItem("Todos", "0"));

                    ddlTurno.DataSource = new Combos().Get_TurnoCbo();
                    ddlTurno.DataTextField = "Descripcion";
                    ddlTurno.DataValueField = "Clave_turno";
                    ddlTurno.DataBind();
                    //ddlTurno.Items.Insert(0, new System.Web.UI.WebControls.ListItem("Seleccione...", "0"));//Control de Cambio: Quitar Opcion 'Seleccionje...'
                    ddlTurno.Items.Insert(0, new System.Web.UI.WebControls.ListItem("Todos", "0"));


                    ddlTipoArticulo.DataSource = new Combos().Get_TipoArticuloCbo();
                    ddlTipoArticulo.DataTextField = "DesTipoArticulo";
                    ddlTipoArticulo.DataValueField = "CodTipoArticulo";
                    ddlTipoArticulo.DataBind();



                    //ddlTipoArticulo.Items.Insert(0, new System.Web.UI.WebControls.ListItem("Seleccione...", "0"));//Control de Cambio: Quitar Opcion 'Seleccionje...'
                    ddlTipoArticulo.Items.Insert(0, new System.Web.UI.WebControls.ListItem("Todos", "0"));

                    wdcFechaInicial.Value = DateTime.Now;
                    wdcFechaFinal.Value = DateTime.Now;

                    ddlTipoArticulo_SelectedIndexChanged(null, null);
                    ddlPlanta_SelectedIndexChanged(null, null);
                    LlenarProcesoDestino();
                    //LlenaTabla();

                    //igtbl_reBuscaBtn.Enabled = false;
                    //igtbl_reSelecciona.Disabled = true;
                    bool editar = false;
                    foreach (ScreenPermission sp in new Actions().GetActionBySreen(user.CodRol, Request.Url.LocalPath))
                    {
                        switch (sp.ActionCode)
                        {
                            case 1: //Buscar
                                igtbl_reBuscaBtn.Enabled = true;
                                //igtbl_reSelecciona.Disabled = false;
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
                //ddlModelo.Items.Insert(0, new System.Web.UI.WebControls.ListItem("Seleccione...", "0"));//Control de Cambio: Quitar Opcion 'Seleccionje...'
            }
            ddlModelo.Items.Insert(0, new System.Web.UI.WebControls.ListItem("Todos", "0"));
        }
        /*   protected void UltraWebGrid1_InitializeRow(object sender, RowEventArgs e)
           {
               for (int x = 0; x < e.Row.Cells.Count; x++)
               {
                   // work out how wide the cell span should be for this cell
                   int span = 1;
                   while (x + span < e.Row.Cells.Count - 1 &&
                    (int)e.Row.Cells[x + span].Value == (int)e.Row.Cells[x].Value)
                   {
                       span++;
                   }

                   // if we need to span this cell
                   if (span > 1)
                   {
                       // apply the span value to the first cell in the group
                       e.Row.Cells[x].ColSpan = span;
                       //    e.Row.Cells[x].Style.BackColor = Color.LightBlue;

                       // skip to the next cell past the end of the span
                       x += span;
                   }
               }
           }*/
        protected void bBuscar_Click(object sender, EventArgs e)
        {
            LlenaTabla();
        }
        protected void LlenaTabla()
        {
            int planta = Convert.ToInt32(ddlPlanta.SelectedValue);
            int turno = Convert.ToInt32(ddlTurno.SelectedValue);
            int procesoOrigen = Convert.ToInt32(ddlProcesoOrigen.SelectedValue);
            int procesoDestino = Convert.ToInt32(ddlProcesoDestino.SelectedValue);
            int tipoArrticulo = Convert.ToInt32(ddlTipoArticulo.SelectedValue);
            int modelo = Convert.ToInt32(ddlModelo.SelectedValue);
            int centroTrabajo = Convert.ToInt32(ddlCentroTrabajo.SelectedValue);
            DateTime dtFechaInicial = (DateTime)wdcFechaInicial.Value;
            DateTime dtFechaFinal = (DateTime)wdcFechaFinal.Value;

            UltraWebGrid1.Columns.Clear();
            this.UltraWebGrid1.DataSource = new ReportesB().ControlDePisos(planta, turno, procesoOrigen, procesoDestino, tipoArrticulo, modelo, centroTrabajo, dtFechaInicial, dtFechaFinal);
            this.UltraWebGrid1.DataBind();
            if (UltraWebGrid1.Rows.Count > 30)
                UltraWebGrid1.Height = new Unit(360);
            else
                UltraWebGrid1.Height = Unit.Empty;

            UltraWebGrid1.Columns[0].Header.Caption = "ClaveModelo";
            UltraWebGrid1.Columns[1].Header.Caption = "Clas de Art";
            UltraWebGrid1.Columns[2].Header.Caption = "# Operador";
            UltraWebGrid1.Columns[3].Header.Caption = "Operador";
            UltraWebGrid1.Columns[4].Header.Caption = "Pzas Procesadas Proceso Origen";
            UltraWebGrid1.Columns[5].Header.Caption = "Pzas Malas Proceso Origen";
            UltraWebGrid1.Columns[6].Header.Caption = "% Pzas Malas Proceso Origen";
            UltraWebGrid1.Columns[7].Header.Caption = "Pzas Procesadas Proceso Destino";
            UltraWebGrid1.Columns[8].Header.Caption = "Pzas Malas Proceso Destino";
            UltraWebGrid1.Columns[9].Header.Caption = "% Pzas Malas Proceso Destino";
            UltraWebGrid1.Columns[10].Header.Caption = "% Pzas Malas Totales";

            UltraWebGrid1.Columns[0].Hidden = true;
            UltraWebGrid1.Columns[2].Hidden = true;

            UltraWebGrid1.Columns[3].Width = new Unit(250, UnitType.Pixel);
            UltraWebGrid1.Columns[1].Width = new Unit(200, UnitType.Pixel);
            if (UltraWebGrid1.Rows.Count > 0)
            {
                for (int i = 0; i < UltraWebGrid1.Columns.Count; i++)
                {
                    UltraWebGrid1.Columns[i].Header.Style.Wrap = true;
                    UltraWebGrid1.Columns[i].CellStyle.HorizontalAlign = HorizontalAlign.Left;
                    UltraWebGrid1.Columns[i].Header.Style.HorizontalAlign = HorizontalAlign.Center;
                    UltraWebGrid1.Columns[i].AllowResize = AllowSizing.Fixed;
                }
            }
            else
            {
                for (int i = 0; i < UltraWebGrid1.Columns.Count; i++)
                {
                    UltraWebGrid1.Columns[i].Header.Style.Wrap = true;
                    UltraWebGrid1.Columns[i].CellStyle.HorizontalAlign = HorizontalAlign.Center;
                    UltraWebGrid1.Columns[i].Header.Style.HorizontalAlign = HorizontalAlign.Center;
                }
            }

            /*   UltraGridBand band = UltraWebGrid1.Bands[0];
               band.ColFootersVisible = ShowMarginInfo.Yes;
               band.Columns[0].Footer.Caption = "Total :";

               band.Columns[1].Footer.Total = Infragistics.WebUI.UltraWebGrid.SummaryInfo.Sum;*/
        }
        protected void UltraWebGrid1_InitializeLayout(object sender, Infragistics.WebUI.UltraWebGrid.LayoutEventArgs e)
        {
            //ColumnHeader colHead;
            //for (int i = 0; i < e.Layout.Bands[0].HeaderLayout.Count; i++)
            //{
            //    colHead = e.Layout.Bands[0].HeaderLayout[i] as ColumnHeader;
            //    colHead.RowLayoutColumnInfo.OriginY = 1;
            //}

            //ColumnHeader chInicial = new ColumnHeader(true);
            //chInicial.Caption = "";
            //chInicial.Style.HorizontalAlign = HorizontalAlign.Center;
            //chInicial.RowLayoutColumnInfo.OriginX = 0;
            //chInicial.RowLayoutColumnInfo.OriginY = 0;
            //chInicial.RowLayoutColumnInfo.SpanX = 2;
            //e.Layout.Bands[0].HeaderLayout.Add(chInicial);

            //ColumnHeader ch = new ColumnHeader(true);
            //ch.Caption = "Produccion de Proceso Origen";
            //ch.Style.HorizontalAlign = HorizontalAlign.Center;
            //ch.RowLayoutColumnInfo.OriginX = 2;
            //ch.RowLayoutColumnInfo.OriginY = 0;
            //ch.RowLayoutColumnInfo.SpanX = 3;
            //e.Layout.Bands[0].HeaderLayout.Add(ch);

            //ColumnHeader ch1 = new ColumnHeader(true);
            //ch1.Caption = "Produccion de Proceso Destino";
            //ch1.Style.HorizontalAlign = HorizontalAlign.Center;
            //ch1.RowLayoutColumnInfo.OriginX = 5;
            //ch1.RowLayoutColumnInfo.OriginY = 0;
            //ch1.RowLayoutColumnInfo.SpanX = 3;
            //e.Layout.Bands[0].HeaderLayout.Add(ch1);
        }
        protected void btnExporta_Click(object sender, EventArgs e)
        {
            int planta = Convert.ToInt32(ddlPlanta.SelectedValue);
            int turno = Convert.ToInt32(ddlTurno.SelectedValue);
            int procesoOrigen = Convert.ToInt32(ddlProcesoOrigen.SelectedValue);
            int procesoDestino = Convert.ToInt32(ddlProcesoDestino.SelectedValue);
            int tipoArrticulo = Convert.ToInt32(ddlTipoArticulo.SelectedValue);
            int modelo = Convert.ToInt32(ddlModelo.SelectedValue);
            int centroTrabajo = Convert.ToInt32(ddlCentroTrabajo.SelectedValue);
            DateTime dtFechaInicial = (DateTime)wdcFechaInicial.Value;
            DateTime dtFechaFinal = (DateTime)wdcFechaFinal.Value;
            DataTable dt = new ReportesB().ControlDePisos(planta, turno, procesoOrigen, procesoDestino, tipoArrticulo, modelo, centroTrabajo, dtFechaInicial, dtFechaFinal);
            DataSet dsReportXLS = new DataSet();
            dsReportXLS.Tables.Add(dt.Copy());
            ExportToExcel(dsReportXLS, 0, Response, nombre.Value);

        }
        protected void ddlPlanta_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (sender != null && ((DropDownList)sender).ClientID.Equals(ddlProcesoOrigen.ClientID))
                LlenarProcesoDestino();
            ddlCentroTrabajo.Items.Clear();
            int iPlanta = Convert.ToInt32(this.ddlPlanta.SelectedItem.Value);
            if (iPlanta > 0)
            {
                List<BE.CentroTrabajo> l_CTCbo = new List<BE.CentroTrabajo>();
                int iProceso = Convert.ToInt32(this.ddlProcesoOrigen.SelectedItem.Value);
                iProceso = iProceso < 1 ? -1 : iProceso;
                foreach (SE.BaseSolutionEntity bse in new svcSCPP.SCPPClient().ObtenerCentroTrabajoCbo(iPlanta, iProceso))
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
        private void LlenarProcesoDestino()
        {
            ddlProcesoDestino.Items.Clear();
            int iCodigoProceso = Convert.ToInt32(this.ddlProcesoOrigen.SelectedItem.Value);
            ddlProcesoDestino.DataSource = new Combos().ObtenerProcesoDestino(iCodigoProceso);
            ddlProcesoDestino.DataValueField = "CodigoProceso";
            ddlProcesoDestino.DataTextField = "DescProceso";
            ddlProcesoDestino.DataBind();
        }
    }
}
