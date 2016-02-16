using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using Infragistics.WebUI.Shared;
using LAMOSA.SCPP.Server.BusinessEntity.Server;
using Infragistics.WebUI.UltraWebGrid;
using LAMOSA.SCPP.Server.BusinessEntity;

using Infragistics.Shared;
using Infragistics.Excel;


using BE = LAMOSA.SCPP.Server.BusinessEntity;
using SE = Common.SolutionEntityFramework;

namespace LAMOSA.SCPP.Client.View.Administrador.Administracion
{
    public partial class ExcedenteCodBarras : ReporteBase
    {
        #region Constants
        protected string comilla = "'";
        protected string HTMLCboRol = String.Empty;
        #endregion
        #region Methods
        #region Event Handlers
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack && !Page.IsCallback)
            {
                Usuario user = (Usuario)Session["UserLogged"];
                if (user != null)
                {
                    DropDownList cmbPlanta = ((DropDownList)Page.Master.FindControl("cmbPlanta"));
                    Planta.Value = cmbPlanta.SelectedItem.Text;
                    svcSCPP.SCPPClient svc = new svcSCPP.SCPPClient();

                    dllPlanta.DataSource = new Combos().Get_Planta_RolCbo(user.CodRol);//svc.ObtenerCentroTrabajo(int.Parse(cmbPlanta.SelectedValue), 1);
                    dllPlanta.DataTextField = "descripcionPlanta";
                    dllPlanta.DataValueField = "ClavePlanta";
                    dllPlanta.DataBind();
                    
                    txtPlanta.Text = dllPlanta.SelectedItem.Text;
                    txtPlanta.Enabled = false;

                    cmbCentroTrabajo.Items.AddRange(GetItemsConSeleccioneTodos(svc.ObtenerCentroTrabajoCbo(int.Parse(dllPlanta.SelectedValue), 1), "DesCentroTrabajo", "CodCentroTrabajo"));
                    cmbCentroTrabajo2.Items.AddRange(GetItems(svc.ObtenerCentroTrabajoCbo(int.Parse(dllPlanta.SelectedValue), 1), "DesCentroTrabajo", "CodCentroTrabajo"));
                    wdcFechaIni.Value = DateTime.Today;
                    wdcFechaFin.Value = DateTime.Today.Date.AddDays(1);
                    llenaGrid();

                    //Search.Enabled = false;
                    //LExport.Visible = false;
                    //LAddNew.Visible = false;
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

        protected void dllPlanta_SelectedIndexChanged(object sender, EventArgs e)
        {
            cmbCentroTrabajo.Items.Clear();
            cmbCentroTrabajo2.Items.Clear();
          
            if (int.Parse(dllPlanta.SelectedValue) != -1)
            {
                svcSCPP.SCPPClient svc = new svcSCPP.SCPPClient();
                cmbCentroTrabajo.Items.AddRange(GetItems(svc.ObtenerCentroTrabajoCbo(int.Parse(dllPlanta.SelectedValue), 1), "DesCentroTrabajo", "CodCentroTrabajo"));
                cmbCentroTrabajo.Items.Insert(0, new System.Web.UI.WebControls.ListItem("Todos", "-1"));
                cmbCentroTrabajo2.Items.AddRange(GetItems(svc.ObtenerCentroTrabajoCbo(int.Parse(dllPlanta.SelectedValue), 1), "DesCentroTrabajo", "CodCentroTrabajo"));
                txtPlanta.Text = dllPlanta.SelectedItem.Text;
                txtPlanta.Enabled = false;
            }
            cmbCentroTrabajo2.Items.Insert(0, new System.Web.UI.WebControls.ListItem("Todos", "-1"));

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
        protected void btnLlenaGrid_Click(object sender, EventArgs e)
        {
            llenaGrid();
        }
        protected void llenaGrid()
        {
            {
                Usuario user = (Usuario)Session["UserLogged"];
                svcSCPP.SCPPClient svc = new svcSCPP.SCPPClient();
                DropDownList cmbPlanta = ((DropDownList)Page.Master.FindControl("cmbPlanta"));
                int empleado = -1;
                try { empleado = Convert.ToInt32(txtEmpleado.Text); }
                catch { }
                DataTable dt = new CodigoBarras().ObtenerCodigoBarras(int.Parse(dllPlanta.SelectedValue), int.Parse(cmbCentroTrabajo.SelectedValue),-1, -1, empleado);
                uwgCodigBarras.DataSource = dt;
                uwgCodigBarras.DataBind();
                uwgCodigBarras.Columns[0].Hidden = true;
                uwgCodigBarras.Columns[1].Format = "dd-MM-yyyy";
                uwgCodigBarras.Columns[3].Hidden = true;
                uwgCodigBarras.Columns[4].Hidden = true;
                uwgCodigBarras.Columns[5].Hidden = true;
                uwgCodigBarras.Columns[7].Hidden = true;
                uwgCodigBarras.Columns[9].Hidden = true;
      


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
            }
        }

        protected void BotonGuardar_click(object sender, EventArgs e)
        {
            try
            {
                String id = hID.Value.ToString();
                String cod_ct = hCodCT.Value.ToString();
                String cod_empleado = hCodEmpleado.Value.ToString();
                String cod_desde = hCodDesde.Value.ToString();
                String cod_hasta = hCodHasta.Value.ToString();
                DropDownList cmbPlanta = ((DropDownList)Page.Master.FindControl("cmbPlanta"));
                String resp = new CodigoBarras().InsertCodigoBarras(id, dllPlanta.SelectedValue.ToString(), cod_ct, "1", cod_empleado, cod_desde, cod_hasta);
                if (resp.Length > 3)
                    CallBackManager.AddScriptBlock(this.Page, WebAsyncRefreshPanel1, "<script type='text/javascript'>alert('" + resp + "');</script>");
            }
            catch (Exception err)
            {
                CallBackManager.AddScriptBlock(this.Page, WebAsyncRefreshPanel1, "<script type='text/javascript'>alert('" + err.Message + "');</script>");
            }
            llenaGrid();
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
    }
}
