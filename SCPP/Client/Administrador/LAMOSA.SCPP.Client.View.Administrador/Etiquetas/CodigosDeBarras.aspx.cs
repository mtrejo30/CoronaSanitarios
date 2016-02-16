using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Infragistics.WebUI.Shared;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using Infragistics.Web.UI.ListControls;
using Infragistics.WebUI.UltraWebGrid;
using System.IO;
using System.ComponentModel;

using Infragistics.Shared;
using Infragistics.Excel;
using System.Data;

using LAMOSA.SCPP.Server.BusinessEntity.Server;
using LAMOSA.SCPP.Server.BusinessEntity;

namespace LAMOSA.SCPP.Client.View.Administrador.Etiquetas
{
    public partial class CodigosDeBarras : ReporteBase
    {

        #region Constants
        protected string comilla = "'";
        protected string HTMLCboRol = String.Empty;
        #endregion
        #region Methods

        #region Event Handlers
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                Usuario user = (Usuario)Session["UserLogged"];
                if (user != null)
                {
                    DropDownList cmbPlanta = ((DropDownList)Page.Master.FindControl("cmbPlanta"));
                    Planta.Value = cmbPlanta.SelectedItem.Text;
                    svcSCPP.SCPPClient svc = new svcSCPP.SCPPClient();
                    //Planta
                    dllPlanta.DataSource = new Combos().Get_Planta_RolCbo(user.CodRol);//svc.ObtenerCentroTrabajo(int.Parse(cmbPlanta.SelectedValue), 1);
                    dllPlanta.DataTextField = "descripcionPlanta";
                    dllPlanta.DataValueField = "ClavePlanta";
                    dllPlanta.DataBind();
                    txtPlanta.Text = dllPlanta.SelectedItem.Text;
                    txtPlanta.Enabled = false;
                    //Proceso
                    dllProceso.DataSource = new Combos().Get_ProcesoCbo();
                    dllProceso.DataTextField = "DescripcionProceso";
                    dllProceso.DataValueField = "ClaveProceso";
                    dllProceso.DataBind();
                    dllProceso.SelectedValue = "1";
                    txtProceso.Text = dllProceso.SelectedItem.Text;
                    txtProceso.Enabled = false;


                    cmbCentroTrabajo.Items.AddRange(GetItems(svc.ObtenerCentroTrabajoCbo(int.Parse(dllPlanta.SelectedValue), int.Parse(dllProceso.SelectedValue)), "DesCentroTrabajo", "CodCentroTrabajo"));
                    cmbCentroTrabajo.Items.Insert(0, new System.Web.UI.WebControls.ListItem("Todos", "-1"));
                    cmbCentroTrabajo2.Items.AddRange(GetItems(svc.ObtenerCentroTrabajoCbo(int.Parse(dllPlanta.SelectedValue), int.Parse(dllProceso.SelectedValue)), "DesCentroTrabajo", "CodCentroTrabajo"));
                    cmbBanco.Items.AddRange(GetItems(svc.ObtenerMaquinaCbo(-1, int.Parse(cmbCentroTrabajo.SelectedValue)), "DesMaquina", "codMaquina"));
                    cmbBanco.Items.Insert(0, new System.Web.UI.WebControls.ListItem("Todos", "-1"));
                    llenaGrid();
                    bool editar = false;
                    foreach (ScreenPermission sp in new Actions().GetActionBySreen(user.CodRol, Request.Url.LocalPath))
                    {
                        switch (sp.ActionCode)
                        {
                            case 1: //Buscar
                                Search.Enabled = true;
                                break;
                            case 2: //Exportar
                                LExport.Visible = true;
                                break;
                            case 3: //Nuevo
                                LAddNew.Visible = true;
                                break;
                            case 4: //Editar
                                editar = true;
                                uwgCodigBarras.DisplayLayout.AllowUpdateDefault = AllowUpdate.RowTemplateOnly;
                                break;
                        }

                    }

                    if (editar == false)
                    {
                        //uwgCodigBarras.DisplayLayout.AllowUpdateDefault = AllowUpdate.No;
                    }
                }
            }

        }
        protected void btnLlenaGridEmp_Click(object sender, EventArgs e)
        {

            svcSCPP.SCPPClient svc = new svcSCPP.SCPPClient();
            int NumEmpleado = -1;
            int.TryParse(NumEmpleadoWD.Text, out NumEmpleado);
            GridView1.DataSource = svc.ObtenerEmpleadoBusqueda(NumEmpleado, NomEmpleadoWD.Text, -1);
            GridView1.DataBind();
            //UltraWebGrid3.Columns[0].CellStyle.HorizontalAlign = HorizontalAlign.Center;

        }
        protected void dllProceso_SelectedIndexChanged(object sender, EventArgs e)
        {
            cmbCentroTrabajo.Items.Clear();
            cmbCentroTrabajo2.Items.Clear();
            cmbBanco.Items.Clear();
            if (int.Parse(dllPlanta.SelectedValue) != -1)
            {
                svcSCPP.SCPPClient svc = new svcSCPP.SCPPClient();
                cmbCentroTrabajo.Items.AddRange(GetItems(svc.ObtenerCentroTrabajoCbo(int.Parse(dllPlanta.SelectedValue), int.Parse(dllProceso.SelectedValue)), "DesCentroTrabajo", "CodCentroTrabajo"));
                cmbCentroTrabajo.Items.Insert(0, new System.Web.UI.WebControls.ListItem("Todos", "-1"));
                cmbCentroTrabajo2.Items.AddRange(GetItems(svc.ObtenerCentroTrabajoCbo(int.Parse(dllPlanta.SelectedValue), int.Parse(dllProceso.SelectedValue)), "DesCentroTrabajo", "CodCentroTrabajo"));
                txtPlanta.Text = dllPlanta.SelectedItem.Text;
                txtPlanta.Enabled = false;
                txtProceso.Text = dllProceso.SelectedItem.Text;
                txtProceso.Enabled = false;
            }
            cmbCentroTrabajo.Items.Insert(0, new System.Web.UI.WebControls.ListItem("Todos", "-1"));
            cmbBanco.Items.Insert(0, new System.Web.UI.WebControls.ListItem("Todos", "-1"));
        }
        protected void dllPlanta_SelectedIndexChanged(object sender, EventArgs e)
        {
            cmbCentroTrabajo.Items.Clear();
            cmbCentroTrabajo2.Items.Clear();
            cmbBanco.Items.Clear();
            if (int.Parse(dllPlanta.SelectedValue) != -1)
            {
                svcSCPP.SCPPClient svc = new svcSCPP.SCPPClient();
                cmbCentroTrabajo.Items.AddRange(GetItems(svc.ObtenerCentroTrabajoCbo(int.Parse(dllPlanta.SelectedValue), int.Parse(dllProceso.SelectedValue)), "DesCentroTrabajo", "CodCentroTrabajo"));
                cmbCentroTrabajo.Items.Insert(0, new System.Web.UI.WebControls.ListItem("Todos", "-1"));
                cmbCentroTrabajo2.Items.AddRange(GetItems(svc.ObtenerCentroTrabajoCbo(int.Parse(dllPlanta.SelectedValue), int.Parse(dllProceso.SelectedValue)), "DesCentroTrabajo", "CodCentroTrabajo"));
                txtPlanta.Text = dllPlanta.SelectedItem.Text;
                txtPlanta.Enabled = false;
                txtProceso.Text = dllProceso.SelectedItem.Text;
                txtProceso.Enabled = false;
            }
            cmbCentroTrabajo.Items.Insert(0, new System.Web.UI.WebControls.ListItem("Todos", "-1"));
            cmbBanco.Items.Insert(0, new System.Web.UI.WebControls.ListItem("Todos", "-1"));
        }
        protected void cmbCentroTrabajo_SelectedIndexChanged(object sender, EventArgs e)
        {
            cmbBanco.Items.Clear();
            if (int.Parse(cmbCentroTrabajo.SelectedValue) != -1)
            {
                svcSCPP.SCPPClient svc = new svcSCPP.SCPPClient();
                cmbBanco.Items.AddRange(GetItems(svc.ObtenerMaquinas(-1, int.Parse(cmbCentroTrabajo.SelectedValue), int.Parse(dllPlanta.SelectedValue), int.Parse(dllProceso.SelectedValue)), "DesMaquina", "codMaquina"));
            }
            cmbBanco.Items.Insert(0, new System.Web.UI.WebControls.ListItem("Todos", "-1"));
        }
        protected void cmbPlanta_SelectedIndexChanged(object sender, EventArgs e)
        {
            llenaGrid();
        }
        protected void cmbRol_SelectedIndexChanged(object sender, EventArgs e)
        {
            llenaGrid();
        }
        protected void btnLlenaGrid_Click(object sender, EventArgs e)
        {
            llenaGrid();
        }
        protected void llenaGrid()
        {
            //if (Convert.ToInt32(cmbCentroTrabajo.SelectedValue) > 0)
            {
                Usuario user = (Usuario)Session["UserLogged"];
                svcSCPP.SCPPClient svc = new svcSCPP.SCPPClient();
                DropDownList cmbPlanta = ((DropDownList)Page.Master.FindControl("cmbPlanta"));
                int empleado = -1;
                try { empleado = Convert.ToInt32(txtEmpleado.Text); }
                catch { }
                DataTable dt = new CodigoBarras().ObtenerCodigoBarras(int.Parse(dllPlanta.SelectedValue), int.Parse(cmbCentroTrabajo.SelectedValue), int.Parse(dllProceso.SelectedValue), int.Parse(cmbBanco.SelectedValue), empleado);
                uwgCodigBarras.DataSource = dt;
                uwgCodigBarras.DataBind();
                uwgCodigBarras.Columns[0].Hidden = true;
                uwgCodigBarras.Columns[3].Hidden = true;
                uwgCodigBarras.Columns[4].Hidden = true;
                uwgCodigBarras.Columns[5].Hidden = true;
                uwgCodigBarras.Columns[7].Hidden = true;
                uwgCodigBarras.Columns[9].Hidden = true;
                uwgCodigBarras.Columns[1].Format = "dd-MM-yyyy";
                uwgCodigBarras.Columns[1].CellStyle.HorizontalAlign = HorizontalAlign.Center;
                uwgCodigBarras.Columns[10].CellStyle.HorizontalAlign = HorizontalAlign.Center;
                uwgCodigBarras.Columns[11].CellStyle.HorizontalAlign = HorizontalAlign.Center;
                uwgCodigBarras.Columns[12].CellStyle.HorizontalAlign = HorizontalAlign.Center;
                uwgCodigBarras.Columns[2].Width = 125;
                uwgCodigBarras.Columns[6].Width = 120;
                uwgCodigBarras.Columns[8].Width = 220;
                uwgCodigBarras.Columns[10].Width = 80;
                uwgCodigBarras.Columns[11].Width = 80;
                uwgCodigBarras.Columns[12].Width = 80;
                uwgCodigBarras.Columns[10].Header.Caption = "Código Desde";
                uwgCodigBarras.Columns[11].Header.Caption = "Código Hasta";
                uwgCodigBarras.Columns[2].Header.Style.Wrap = true;
                uwgCodigBarras.Columns[10].Header.Style.Wrap = true;
                uwgCodigBarras.Columns[11].Header.Style.Wrap = true;
                /*UltraGridColumn colEliminar = new UltraGridColumn(true);
                colEliminar.Key = "colEliminar"; 
                colEliminar.Type = ColumnType.CheckBox;
                colEliminar.DefaultValue = true;
                colEliminar.Header.Title = "Eliminar asignación de codigos de barra";
                colEliminar.Header.Caption = "Eliminar";
                uwgCodigBarras.Columns.Add(colEliminar);*/
            }
        }
        protected void llenaCombo()
        {
            //System.Web.UI.WebControls.ListItemCollection cboitems = new System.Web.UI.WebControls.ListItemCollection();
            //System.Web.UI.WebControls.ListItem[] li = new System.Web.UI.WebControls.ListItem[cmbRol.Items.Count];
            //cmbRol.Items.CopyTo(li, 0);
            //cboitems.AddRange(li);
            //cboitems.RemoveAt(1);
            //creaCombo(cboitems, "cboRol", 0, out HTMLCboRol);
        }
        protected void btnLlenaGridSup_Click(object sender, EventArgs e)
        {
            //svcSCPP.SCPPClient svc = new svcSCPP.SCPPClient();
            //int NumEmpleado = -1;
            //int.TryParse(NumSup.Text, out NumEmpleado);
            //GridView2.DataSource = svc.ObtenerEmpleadoBusqueda(NumEmpleado, NomSup.Text, 2);
            //GridView2.DataBind();
            ////UltraWebGrid3.Columns[0].CellStyle.HorizontalAlign = HorizontalAlign.Center;
        }
        protected void btnExporta_Click(object sender, EventArgs e)
        {
            //svcSCPP.SCPPClient svc = new svcSCPP.SCPPClient();
            //DataSet dsReportXLS = new DataSet();
            //dsReportXLS.Tables.Add();
            //string[] colnames = LAMOSA.SCPP.Server.BusinessEntity.Usuario.GetPropertyNamesArray();
            //foreach (string colname in colnames)
            //{
            //    dsReportXLS.Tables[0].Columns.Add(colname);
            //}
            //List<Common.SolutionEntityFramework.BaseSolutionEntity> datos = svc.ObtenerUsuarios(int.Parse(cmbPlanta2.SelectedValue), int.Parse(cmbRol.SelectedValue), txtUsuario.Text);
            //foreach (Common.SolutionEntityFramework.BaseSolutionEntity item in datos)
            //{
            //    dsReportXLS.Tables[0].Rows.Add(((LAMOSA.SCPP.Server.BusinessEntity.Usuario)item).ToObjectArray());
            //}
            //ExportToExcel(dsReportXLS, 0, Response, nombre.Value);
        }
        #endregion
        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            Response.Redirect("Usuarios.aspx", false);
        }
        protected void uwgCodigBarras_InitializeRow(object sender, RowEventArgs e)
        {
            int codDesde = Convert.ToInt32(e.Row.Cells[10].Text);
            int codHasta = Convert.ToInt32(e.Row.Cells[11].Text);
            UltraGridCell ugc = new UltraGridCell();
            ugc.Text = (codHasta - codDesde + 1).ToString();
            e.Row.Cells.Add(ugc);
        }
        #endregion
        protected void uwgCodigBarras_DeleteRow(object sender, RowEventArgs e)
        {
            String id = e.Row.Cells[0].ToString();
            String resp = new CodigoBarras().DeleteCodigoBarras(id);
            CallBackManager.AddScriptBlock(this.Page, WebAsyncRefreshPanel1, "<script type='text/javascript'>alert('" + resp + "');</script>");
        }
        protected void BotonGuardar_Click(object sender, EventArgs e)
        {
            try
            {
                String id = hID.Value.ToString();
                String cod_ct = hCodCT.Value.ToString();
                String cod_maquina = hCodMaquina.Value.ToString();
                String cod_empleado = hCodEmpleado.Value.ToString();
                String cod_desde = hCodDesde.Value.ToString();
                String cod_hasta = hCodHasta.Value.ToString();
                DropDownList cmbPlanta = ((DropDownList)Page.Master.FindControl("cmbPlanta"));
                String resp = new CodigoBarras().InsertCodigoBarras(id, dllPlanta.SelectedValue.ToString(), cod_ct, cod_maquina, cod_empleado, cod_desde, cod_hasta);
                if (resp.Length > 3)
                    CallBackManager.AddScriptBlock(this.Page, WebAsyncRefreshPanel1, "<script type='text/javascript'>alert('" + resp + "');</script>");
            }
            catch (Exception err)
            {
                CallBackManager.AddScriptBlock(this.Page, WebAsyncRefreshPanel1, "<script type='text/javascript'>alert('" + err.Message + "');</script>");
            }
            llenaGrid();
        }

        protected void btnEliminar_Click(object sender, EventArgs e)
        {
            foreach (UltraGridRow row in this.uwgCodigBarras.Rows)
            {
                if (row.Selected)
                {
                    String id = row.Cells[0].ToString();
                    String resp = new CodigoBarras().DeleteCodigoBarras(id);
                    CallBackManager.AddScriptBlock(this.Page, WebAsyncRefreshPanel1, "<script type='text/javascript'>alert('" + resp + "');</script>");
                    llenaGrid();
                    break;
                }
            }
        }

        protected void uwgCodigBarras_PageIndexChanged(object sender, Infragistics.WebUI.UltraWebGrid.PageEventArgs e)
        {
            uwgCodigBarras.DisplayLayout.Pager.CurrentPageIndex = e.NewPageIndex;
            llenaGrid();
        }
    }
}
