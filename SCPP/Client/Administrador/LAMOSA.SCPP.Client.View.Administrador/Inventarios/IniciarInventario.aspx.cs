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
using server = LAMOSA.SCPP.Server.BusinessEntity.Server;
using LAMOSA.SCPP.Server.BusinessEntity;

namespace LAMOSA.SCPP.Client.View.Administrador.Inventarios
{
    public partial class IniciarInventario : ReporteBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            FillGrid();
        }
        private void FillGrid()
        {
            DropDownList cmbPlanta = ((DropDownList)Page.Master.FindControl("cmbPlanta"));
            int iPlanta = Convert.ToInt32(cmbPlanta.SelectedValue);
            UltraWebGrid1.DataSource = new server.Inventarios().InventarioEnProceso(iPlanta);
            UltraWebGrid1.DataBind();
            if (UltraWebGrid1.Rows.Count > 0)
            {
                UltraWebGrid1.Columns[0].Hidden = true;
                UltraWebGrid1.Columns[1].Width = 130;
                UltraWebGrid1.Columns[2].Width = 110;
                UltraWebGrid1.Columns[3].Width = 110;
            }
        }
        protected void btnExporta_Click(object sender, EventArgs e)
        {

        }
        protected void bBuscar_Click(object sender, EventArgs e)
        {
            FillGrid();
        }
        protected void btnGenerarInventario_Click(object sender, EventArgs e)
        {
            String msg = "";
            try
            {
                Usuario user = (Usuario)Session["UserLogged"];
                if (user != null)
                {
                    DropDownList cmbPlanta = ((DropDownList)Page.Master.FindControl("cmbPlanta"));
                    int iPlanta = Convert.ToInt32(cmbPlanta.SelectedValue);
                    msg = new server.Inventarios().InventarioEnProcesoGenerar(user.CodUsuario, iPlanta);
                }
            }
            catch { msg = "Hubo un problema al generar el Inventario, intente nuevamente."; }
            CallBackManager.AddScriptBlock(this.Page, WebAsyncRefreshPanel1, "<script type='text/javascript'>alert('" + msg + "');</script>");
            FillGrid();
        }
        protected void btnBuscaDetalle_Click(object sender, EventArgs e)
        {
            String msg = "";
            try
            {
                int id = Convert.ToInt32(hIdInventario.Value);
                UltraWebGrid2.DataSource = new server.Inventarios().InventarioEnProcesoDetalle(id);
                UltraWebGrid2.DataBind();
                if (UltraWebGrid2.Rows.Count > 0)
                {
                    UltraWebGrid2.Columns[0].Hidden = true;
                }
            }
            catch
            {
                msg = "Hubo un problema al generar el Inventario, intente nuevamente.";
                CallBackManager.AddScriptBlock(this.Page, WebAsyncRefreshPanel2, "<script type='text/javascript'>alert('" + msg + "');</script>");
            }

        }
        protected void btnTerminaInventario_Click(object sender, EventArgs e)
        {
            String msg = "";
            try
            {
                Usuario user = (Usuario)Session["UserLogged"];
                if (user != null)
                {
                    DropDownList cmbPlanta = ((DropDownList)Page.Master.FindControl("cmbPlanta"));
                    int iPlanta = Convert.ToInt32(cmbPlanta.SelectedValue);
                    msg = new server.Inventarios().InventarioEnProcesoTerminar(user.CodUsuario, iPlanta);
                }
            }
            catch
            {
                msg = "Hubo un problema al generar el Inventario, intente nuevamente.";
            }
            CallBackManager.AddScriptBlock(this.Page, WebAsyncRefreshPanel1, "<script type='text/javascript'>alert('" + msg + "');</script>");
            FillGrid();
        }
        protected void btnAjusteAutomatico_Click(object sender, EventArgs e)
        {
            String msg = "";
            try
            {
                Usuario user = (Usuario)Session["UserLogged"];
                if (user != null)
                {
                    if (UltraWebGrid2.Rows.Count > 0)
                    {
                        int idInv = Convert.ToInt32(UltraWebGrid2.Rows[0].Cells[0].Value.ToString());
                        msg = new server.Inventarios().InventarioEnProcesoAjusteAutomatico(user.CodUsuario, idInv);
                    }
                }
            }
            catch
            {
                msg = "Hubo un problema al generar el Inventario, intente nuevamente.";
            }
            CallBackManager.AddScriptBlock(this.Page, WebAsyncRefreshPanel2, "<script type='text/javascript'>alert('" + msg + "');</script>");
            btnBuscaDetalle_Click(null, null);
        }
        protected void UltraWebGrid1_DeleteRow(object sender, RowEventArgs e)
        {
            String msg = "";
            try
            {
                msg = new server.Inventarios().InventarioEnProcesoEliminar(Convert.ToInt32(e.Row.Cells[0].Value.ToString()));
            }
            catch
            {
                msg = "Hubo un problema al procesar la informacion, intente nuevamente.";
            }
            CallBackManager.AddScriptBlock(this.Page, WebAsyncRefreshPanel1, "<script type='text/javascript'>alert('" + msg + "');</script>");
            FillGrid();
        }
        protected void btnExportaDetalle_Click(object sender, EventArgs e)
        {
            try
            {
                int id = Convert.ToInt32(hIdInventario.Value);
                DataTable dt = new server.Inventarios().InventarioEnProcesoDetalle(id);
                DataSet ds = new DataSet();
                ds.Tables.Add(dt);
                ExportToExcel(ds, 0, Response, "Inventario en Proceso");
            }
            catch(Exception er)
            {
                CallBackManager.AddScriptBlock(this.Page, WebAsyncRefreshPanel2, "<script type='text/javascript'>alert('Hubo un problema al Generar el Excel, intente nuevamente');</script>");
            }
        }
    }
}
