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
    public partial class Defectos : ReporteBase
    {
        protected string comilla = "'";
        protected string HTMLCboRol = String.Empty;

        public DataTable workTable;
        public DataTable workTable2;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                String uri = Request.Url.LocalPath.ToString();
                Usuario user = (Usuario)Session["UserLogged"];
                if (user != null)
                {
                    FechaIni.Value = DateTime.Today;
                    FechaFin.Value = DateTime.Today;

                    ddlEdoDefecto.DataSource = new Combos().Get_EdoDefecto();
                    ddlEdoDefecto.DataTextField = "Descripcion";
                    ddlEdoDefecto.DataValueField = "CodEstadoDefecto";
                    ddlEdoDefecto.DataBind();
                    //ddlEdoDefecto.Items.Insert(0, new System.Web.UI.WebControls.ListItem("Seleccione...", "0"));
                    ddlEdoDefecto.Items.Insert(0, new System.Web.UI.WebControls.ListItem("Todos", "0"));

                    ddlPlanta.DataSource = new Combos().Get_Planta_RolCbo(user.CodRol);
                    ddlPlanta.DataTextField = "descripcionPlanta";
                    ddlPlanta.DataValueField = "ClavePlanta";
                    ddlPlanta.DataBind();
                    //ddlPlanta.Items.Insert(0, new System.Web.UI.WebControls.ListItem("Seleccione...", "0"));
                    ddlPlanta.Items.Insert(0, new System.Web.UI.WebControls.ListItem("Todos", "0"));

                    ddlTipoArticulo.DataSource = new Combos().Get_TipoArticuloCbo();
                    ddlTipoArticulo.DataTextField = "DesTipoArticulo";
                    ddlTipoArticulo.DataValueField = "CodTipoArticulo";
                    ddlTipoArticulo.DataBind();
                    //ddlTipoArticulo.Items.Insert(0, new System.Web.UI.WebControls.ListItem("Seleccione...", "0"));
                    ddlTipoArticulo.Items.Insert(0, new System.Web.UI.WebControls.ListItem("Todos", "0"));

                    ddlTipoArticulo_SelectedIndexChanged(null, null);

                    ddlProceso.DataSource = new Combos().Get_ProcesoCbo();
                    ddlProceso.DataTextField = "DescripcionProceso";
                    ddlProceso.DataValueField = "ClaveProceso";
                    ddlProceso.DataBind();
                    //ddlProceso.Items.Insert(0, new System.Web.UI.WebControls.ListItem("Seleccione...", "0"));
                    ddlProceso.Items.Insert(0, new System.Web.UI.WebControls.ListItem("Todos", "0"));

                    ddlColor.DataSource = new Combos().Get_ColorCbo();
                    ddlColor.DataTextField = "DesColor";
                    ddlColor.DataValueField = "CodColor";
                    ddlColor.DataBind();
                    //ddlColor.Items.Insert(0, new System.Web.UI.WebControls.ListItem("Seleccione...", "0"));
                    ddlColor.Items.Insert(0, new System.Web.UI.WebControls.ListItem("Todos", "0"));

                    CargaCentroTrabajo_SelectedIndexChanged(null, null);

                    //LlenaTabla();
                    //LExport.Visible = true;
                    bBuscar.Enabled = true;
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

        protected void btnLlenaGridEmp_Click(object sender, EventArgs e)
        {

            svcSCPP.SCPPClient svc = new svcSCPP.SCPPClient();
            int NumEmpleado = -1;
            int.TryParse(NumEmpleadoWD.Text, out NumEmpleado);
            GridView1.DataSource = svc.ObtenerEmpleadoBusqueda(NumEmpleado, NomEmpleadoWD.Text, 3);
            GridView1.DataBind();
            //UltraWebGrid3.Columns[0].CellStyle.HorizontalAlign = HorizontalAlign.Center;

        }
        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            Response.Redirect("Defectos.aspx", false);
        }
        protected void UltraWebGrid1_InitializeRow(object sender, RowEventArgs e)
        {
            //for (int x = 0; x < e.Row.Cells.Count; x++)
            //{
            //    // work out how wide the cell span should be for this cell
            //    int span = 1;
            //    while (x + span < e.Row.Cells.Count - 1 &&
            //        (int)e.Row.Cells[x + span].Value == (int)e.Row.Cells[x].Value)
            //    {
            //        span++;
            //    }

            //    // if we need to span this cell
            //    if (span > 1)
            //    {
            //        // apply the span value to the first cell in the group
            //        e.Row.Cells[x].ColSpan = span;
            //        //    e.Row.Cells[x].Style.BackColor = Color.LightBlue;

            //        // skip to the next cell past the end of the span
            //        x += span;
            //    }
            //}
        }
        protected void UltraWebGrid1_InitializeLayout(object sender, Infragistics.WebUI.UltraWebGrid.LayoutEventArgs e)
        {

            foreach (Infragistics.WebUI.UltraWebGrid.UltraGridColumn c in e.Layout.Bands[0].Columns)
            {
                c.Header.RowLayoutColumnInfo.OriginY = 1;
            }



            ColumnHeader ch = new ColumnHeader(true);
            ch.Caption = "Desperdicio";
            ch.Style.HorizontalAlign = HorizontalAlign.Center;
            //ch.RowLayoutColumnInfo.OriginX = 0;
            ch.RowLayoutColumnInfo.OriginX = 1;
            ch.RowLayoutColumnInfo.OriginY = 0;

            // extend the newly added header over 3 columns
            ch.RowLayoutColumnInfo.SpanX = 1;

            e.Layout.Bands[0].HeaderLayout.Add(ch);

            ColumnHeader ch2 = new ColumnHeader(true);
            ch2.Caption = "Proceso";
            ch2.Style.HorizontalAlign = HorizontalAlign.Center;

            ch2.RowLayoutColumnInfo.OriginX = 3;

            // This will set it on the first row of the HeaderLayout object 
            // The columns created by the control are all on the first row by default.
            ch2.RowLayoutColumnInfo.OriginY = 0;
            // Add the newly created Header object to the HeaderLayout object
            e.Layout.Bands[0].HeaderLayout.Add(ch2);

            // Expand the new column header to cover two columns.
            /*ch.RowLayoutColumnInfo.SpanX = 3;*/
            ch2.RowLayoutColumnInfo.SpanX = 8;




        }
        protected void LlenaTabla()
        {
            int planta = Convert.ToInt32(ddlPlanta.SelectedValue);
            int proceso = Convert.ToInt32(ddlProceso.SelectedValue);
            int tipoArrticulo = Convert.ToInt32(ddlTipoArticulo.SelectedValue);
            int modelo = Convert.ToInt32(ddlModelo.SelectedValue);
            int color = Convert.ToInt32(ddlColor.SelectedValue);
            int empleado = -1;
            try { empleado = Convert.ToInt32(txtEmpleado.Text); }
            catch { }
            int estado = Convert.ToInt32(ddlEdoDefecto.SelectedValue);
            int iCentroTrabajo = Convert.ToInt32(ddlCentroTrabajo.SelectedValue);

            this.UltraWebGrid1.DataSource = new ReportesB().ListDefectos(planta, proceso, tipoArrticulo, modelo, color, DateTime.Parse(FechaIni.Value.ToString()), DateTime.Parse(FechaFin.Value.ToString()), empleado, estado, iCentroTrabajo);
            this.UltraWebGrid1.DataBind();
            if (UltraWebGrid1.Rows.Count > 30)
                UltraWebGrid1.Height = new Unit(360);
            else
                UltraWebGrid1.Height = Unit.Empty;
            UltraWebGrid1.Columns[0].Hidden = true;
            UltraWebGrid1.Columns[2].Hidden = true;
            UltraWebGrid1.Columns[4].Hidden = true;
            UltraWebGrid1.Columns[6].Hidden = true;
            UltraWebGrid1.Columns[8].Hidden = true;
            UltraWebGrid1.Columns[10].Hidden = true;
            UltraWebGrid1.Columns[12].Hidden = true;
            UltraWebGrid1.Columns[14].Hidden = true;
            UltraWebGrid1.Columns[16].Hidden = true;
            int width = 80;
            UltraWebGrid1.Columns[5].Width = width;
            UltraWebGrid1.Columns[7].Width = width;
            UltraWebGrid1.Columns[9].Width = width;
            UltraWebGrid1.Columns[11].Width = width;
            UltraWebGrid1.Columns[13].Width = width;
            UltraWebGrid1.Columns[15].Width = width;
            UltraWebGrid1.Columns[17].Width = width;
            UltraWebGrid1.Columns[1].Width = 120;
            UltraWebGrid1.Columns[3].Width = 120;

            UltraWebGrid1.Columns[4].CellStyle.HorizontalAlign = HorizontalAlign.Center;
            UltraWebGrid1.Columns[5].CellStyle.HorizontalAlign = HorizontalAlign.Center;
            UltraWebGrid1.Columns[6].CellStyle.HorizontalAlign = HorizontalAlign.Center;
            UltraWebGrid1.Columns[7].CellStyle.HorizontalAlign = HorizontalAlign.Center;
            UltraWebGrid1.Columns[8].CellStyle.HorizontalAlign = HorizontalAlign.Center;
            UltraWebGrid1.Columns[9].CellStyle.HorizontalAlign = HorizontalAlign.Center;
            UltraWebGrid1.Columns[10].CellStyle.HorizontalAlign = HorizontalAlign.Center;
            UltraWebGrid1.Columns[11].CellStyle.HorizontalAlign = HorizontalAlign.Center;
            UltraWebGrid1.Columns[12].CellStyle.HorizontalAlign = HorizontalAlign.Center;
            UltraWebGrid1.Columns[13].CellStyle.HorizontalAlign = HorizontalAlign.Center;
            UltraWebGrid1.Columns[14].CellStyle.HorizontalAlign = HorizontalAlign.Center;
            UltraWebGrid1.Columns[15].CellStyle.HorizontalAlign = HorizontalAlign.Center;
            UltraWebGrid1.Columns[16].CellStyle.HorizontalAlign = HorizontalAlign.Center;
            UltraWebGrid1.Columns[17].CellStyle.HorizontalAlign = HorizontalAlign.Center;


        }
        protected void Llenatabla2()
        {
            int planta = Convert.ToInt32(ddlPlanta.SelectedValue);
            int tipoArrticulo = Convert.ToInt32(ddlTipoArticulo.SelectedValue);
            int modelo = Convert.ToInt32(ddlModelo.SelectedValue);
            int color = Convert.ToInt32(ddlColor.SelectedValue);
            int vaciador = -1;

            int estado = Convert.ToInt32(ddlEdoDefecto.SelectedValue);

            int cod_defecto = Convert.ToInt32(hIdDefecto.Value);
            int cod_zona = Convert.ToInt32(hIdZona.Value);
            int cod_proceso = Convert.ToInt32(hIdProceso.Value);
            this.UltraWebGrid2.DataSource = new ReportesB().ListDefectosDetalles(planta, cod_proceso, tipoArrticulo, modelo, color, DateTime.Parse(FechaIni.Value.ToString()), DateTime.Parse(FechaFin.Value.ToString()), vaciador, estado, cod_defecto, cod_zona);
            this.UltraWebGrid2.DataBind();
            int rows = UltraWebGrid1.Rows.Count;
            UltraWebGrid2.Columns[0].Hidden = true;
            UltraWebGrid2.Columns[1].Hidden = true;
            UltraWebGrid2.Columns[2].Hidden = true;
            UltraWebGrid2.Columns[3].Hidden = true;
            UltraWebGrid2.Columns[4].Hidden = true;
            UltraWebGrid2.Columns[5].Hidden = true;

            UltraWebGrid2.Columns[10].Width = 250;
            //UltraWebGrid2.Columns[11].Width = 80;

        }
        protected void LlenaModal(object sender, EventArgs e)
        {
            Llenatabla2();
        }
        protected void btnExporta_Click(object sender, EventArgs e)
        {
            int planta = Convert.ToInt32(ddlPlanta.SelectedValue);
            int proceso = Convert.ToInt32(ddlProceso.SelectedValue);
            int tipoArticulo = Convert.ToInt32(ddlTipoArticulo.SelectedValue);
            int modelo = Convert.ToInt32(ddlModelo.SelectedValue);
            int color = Convert.ToInt32(ddlColor.SelectedValue);
            int iCodEstadoDefecto = Convert.ToInt32(ddlEdoDefecto.SelectedValue);
            int iCentroTrabajo = Convert.ToInt32(ddlCentroTrabajo.SelectedValue);
            DateTime dtFechaIni = DateTime.Parse(FechaIni.Value.ToString());
            DateTime dtFechaFin = DateTime.Parse(FechaFin.Value.ToString());
            DataSet ds = new DataSet();
            DataTable dt = new ReportesB().ListDefectosExcel(planta, proceso, tipoArticulo, modelo, color, iCodEstadoDefecto, iCentroTrabajo, dtFechaIni, dtFechaFin);
            int dtColCount = dt.Columns.Count - 1;
            /* for (int i = dtColCount; i >= 0; i--)
             {
                 if ((i % 2) == 0)
                 {
                     dt.Columns.RemoveAt(i);
                 }
             }*/
            ds.Tables.Add(dt);
            ExportToExcel(ds, 0, Response, nombre.Value);
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
            //ddlModelo.Items.Insert(0, new System.Web.UI.WebControls.ListItem("Seleccione...", "0"));
            ddlModelo.Items.Insert(0, new System.Web.UI.WebControls.ListItem("Todos", "0"));
        }
        protected void bBuscar_Click(object sender, EventArgs e)
        {
            LlenaTabla();
        }
        protected void CargaCentroTrabajo_SelectedIndexChanged(object sender, EventArgs e)
        {
            ddlCentroTrabajo.Items.Clear();
            int iPlanta = Convert.ToInt32(this.ddlPlanta.SelectedItem.Value);
            if (iPlanta > 0)
            {
                List<BE.CentroTrabajo> l_CTCbo = new List<BE.CentroTrabajo>();
                int iProceso = Convert.ToInt32(this.ddlProceso.SelectedItem.Value);
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
    }
}
