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

namespace LAMOSA.SCPP.Client.View.Administrador.Planta
{
    public partial class RutaProceso : ReporteBase
    {
        #region Constants
        protected string HTMLCboProceso = String.Empty;
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
                    llenaGrid();
                    LExport.Visible = false;
                    LAddNew.Visible = false;
                    bool editar = false;
                    foreach (ScreenPermission sp in new Actions().GetActionBySreen(user.CodRol, Request.Url.LocalPath))
                    {
                        switch (sp.ActionCode)
                        {
                            case 1: //Buscar

                                break;
                            case 2: //Exportar
                                LExport.Visible = true;
                                break;
                            case 3: //Nuevo
                                LAddNew.Visible = true;
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

        private void llenaGrid()
        {
            DropDownList cmbPlanta = ((DropDownList)Page.Master.FindControl("cmbPlanta"));
            svcSCPP.SCPPClient svc = new svcSCPP.SCPPClient();
            cmbProceso.Items.Clear();
            cmbProceso.Items.AddRange(GetItems(svc.ObtenerProceso(), "DesProceso", "CodProceso"));

            List<Common.SolutionEntityFramework.BaseSolutionEntity> Lista = svc.ObtenerRutaProceso(int.Parse(cmbPlanta.SelectedValue));
            UltraWebGrid1.DataSource = Lista;
            if (Lista.Count <= 0)
            {
                llenarvacio();
            }
            else
            {
                UltraWebGrid1.ResetColumns();
                UltraWebGrid1.DataBind();

                UltraWebGrid1.Columns[0].Hidden = true;
                UltraWebGrid1.Columns[1].Hidden = true;
                UltraWebGrid1.Columns[4].Hidden = true;
                UltraWebGrid1.Columns[9].Hidden = true;

                UltraWebGrid1.Columns[2].Header.Caption = "Código";
                UltraWebGrid1.Columns[3].Header.Caption = "Proceso";
                UltraWebGrid1.Columns[5].Header.Caption = "Proceso Padre";
                UltraWebGrid1.Columns[7].Header.Caption = "Requerido";
                UltraWebGrid1.Columns[8].Header.Caption = "Orden";
               
               
                UltraWebGrid1.Columns[1].Width = 100;
                UltraWebGrid1.Columns[2].Width = 100;
                UltraWebGrid1.Columns[3].Width = 100;
                UltraWebGrid1.Columns[4].Width = 100;
                UltraWebGrid1.Columns[5].Width = 100;

                UltraWebGrid1.Columns[2].CellStyle.HorizontalAlign = HorizontalAlign.Center;
                UltraWebGrid1.Columns[6].CellStyle.HorizontalAlign = HorizontalAlign.Center;
                UltraWebGrid1.Columns[7].CellStyle.HorizontalAlign = HorizontalAlign.Center;
                UltraWebGrid1.Columns[8].CellStyle.HorizontalAlign = HorizontalAlign.Center;


                UltraWebGrid1.Columns[0].AllowResize = AllowSizing.Fixed;
                UltraWebGrid1.Columns[1].AllowResize = AllowSizing.Fixed;
                UltraWebGrid1.Columns[2].AllowResize = AllowSizing.Fixed;
                UltraWebGrid1.Columns[3].AllowResize = AllowSizing.Fixed;
                UltraWebGrid1.Columns[4].AllowResize = AllowSizing.Fixed;
                UltraWebGrid1.Columns[5].AllowResize = AllowSizing.Fixed;
                UltraWebGrid1.Columns[6].AllowResize = AllowSizing.Fixed;
                UltraWebGrid1.Columns[7].AllowResize = AllowSizing.Fixed;
                UltraWebGrid1.Columns[8].AllowResize = AllowSizing.Fixed;
                UltraWebGrid1.Columns[9].AllowResize = AllowSizing.Fixed;

                UltraWebGrid1.Columns[0].Header.Style.Wrap = true;
                UltraWebGrid1.Columns[1].Header.Style.Wrap = true;
                UltraWebGrid1.Columns[2].Header.Style.Wrap = true;
                UltraWebGrid1.Columns[3].Header.Style.Wrap = true;
                UltraWebGrid1.Columns[4].Header.Style.Wrap = true;
                UltraWebGrid1.Columns[5].Header.Style.Wrap = true;

    
                UltraWebGrid1.Columns[2].Header.Style.HorizontalAlign = HorizontalAlign.Center;
                UltraWebGrid1.Columns[3].Header.Style.HorizontalAlign = HorizontalAlign.Center;
                UltraWebGrid1.Columns[5].Header.Style.HorizontalAlign = HorizontalAlign.Center;
                UltraWebGrid1.Columns[6].Header.Style.HorizontalAlign = HorizontalAlign.Center;
                UltraWebGrid1.Columns[7].Header.Style.HorizontalAlign = HorizontalAlign.Center;
                UltraWebGrid1.Columns[8].Header.Style.HorizontalAlign = HorizontalAlign.Center;
         
            }
        }
        protected void BotonGuardar_click(object sender, EventArgs e)
        {
            svcSCPP.SCPPClient svc = new svcSCPP.SCPPClient();
            LAMOSA.SCPP.Server.BusinessEntity.RutaProceso rp = new LAMOSA.SCPP.Server.BusinessEntity.RutaProceso();

            try
            {
                DropDownList cmbPlanta = ((DropDownList)Page.Master.FindControl("cmbPlanta"));
                rp.CodPlanta = int.Parse(cmbPlanta.SelectedValue);
                rp.CodProceso = int.Parse(hddProcesoT.Value);
                rp = svc.GuardaRutaProceso(rp);
                llenaGrid();
                if (rp.ExceptionMessage != null && rp.ExceptionMessage.Length > 1)
                    throw new Exception( rp.ExceptionMessage);
            }
            catch (Exception err)
            {
                CallBackManager.AddScriptBlock(this.Page, WebAsyncRefreshPanel1, "<script type='text/javascript'>alert('" + err.Message + "');</script>");
            }
        }

       protected void llenarvacio()
       {
           UltraWebGrid1.ResetColumns();
           UltraWebGrid1.DataBind();
           UltraWebGrid1.Columns.Add("Código");
           UltraWebGrid1.Columns.Add("Proceso");
           UltraWebGrid1.Columns.Add("Proceso Padre");
           UltraWebGrid1.Columns.Add("Manufacturing");
           UltraWebGrid1.Columns.Add("Requerido");
           UltraWebGrid1.Columns.Add("Orden");
       
           UltraWebGrid1.Columns[0].Header.Caption = "Código";
           UltraWebGrid1.Columns[1].Header.Caption = "Proceso";
           UltraWebGrid1.Columns[2].Header.Caption = "Proceso Padre";
           UltraWebGrid1.Columns[3].Header.Caption = "MFG";
           UltraWebGrid1.Columns[4].Header.Caption = "Requerido";
           UltraWebGrid1.Columns[5].Header.Caption = "Orden";

           UltraWebGrid1.Columns[0].Header.Style.HorizontalAlign = HorizontalAlign.Center;
           UltraWebGrid1.Columns[1].Header.Style.HorizontalAlign = HorizontalAlign.Center;
           UltraWebGrid1.Columns[2].Header.Style.HorizontalAlign = HorizontalAlign.Center;
           UltraWebGrid1.Columns[3].Header.Style.HorizontalAlign = HorizontalAlign.Center;
           UltraWebGrid1.Columns[4].Header.Style.HorizontalAlign = HorizontalAlign.Center;
           UltraWebGrid1.Columns[5].Header.Style.HorizontalAlign = HorizontalAlign.Center;


           UltraWebGrid1.Columns[3].Width = 70;
           UltraWebGrid1.Columns[4].Width = 70;
           UltraWebGrid1.Columns[5].Width = 70;
    }
         

        protected void BotonEliminar_click(object sender, EventArgs e)
        {
            svcSCPP.SCPPClient svc = new svcSCPP.SCPPClient();
            LAMOSA.SCPP.Server.BusinessEntity.RutaProceso rp = new LAMOSA.SCPP.Server.BusinessEntity.RutaProceso();

            try
            {
                DropDownList cmbPlanta = ((DropDownList)Page.Master.FindControl("cmbPlanta"));
                rp.CodPlanta = int.Parse(cmbPlanta.SelectedValue);
                rp.CodProceso = int.Parse(hddProcesoT.Value);
                rp = svc.EliminaRutaProceso(rp);
                llenaGrid();
                if (rp.ExceptionMessage != null && rp.ExceptionMessage.Length > 1)
                    CallBackManager.AddScriptBlock(this.Page, WebAsyncRefreshPanel1, "<script type='text/javascript'>alert('" + rp.ExceptionMessage + "');</script>");
            }
            catch (Exception err)
            {

                throw err;
            }
        }
        protected void btnExporta_Click(object sender, EventArgs e)
        {
            DropDownList cmbPlanta = ((DropDownList)Page.Master.FindControl("cmbPlanta"));
            svcSCPP.SCPPClient svc = new svcSCPP.SCPPClient();

            DataSet dsReportXLS = new DataSet();
            dsReportXLS.Tables.Add();

            string[] colnames = LAMOSA.SCPP.Server.BusinessEntity.RutaProceso.GetPropertyNamesArray();
            foreach (string colname in colnames)
            {
                dsReportXLS.Tables[0].Columns.Add(colname);
            }
            List<Common.SolutionEntityFramework.BaseSolutionEntity> datos = svc.ObtenerRutaProceso(int.Parse(cmbPlanta.SelectedValue));
            foreach (Common.SolutionEntityFramework.BaseSolutionEntity item in datos)
            {
                dsReportXLS.Tables[0].Rows.Add(((LAMOSA.SCPP.Server.BusinessEntity.RutaProceso)item).ToObjectArray());
            }
            ExportToExcel(dsReportXLS, 0, Response, nombre.Value);
        }

        protected void cambio_pagina(object sender, Infragistics.WebUI.UltraWebGrid.PageEventArgs e)
        {
            UltraWebGrid1.DisplayLayout.Pager.CurrentPageIndex = e.NewPageIndex;
            llenaGrid();
        }

        #endregion
        #endregion
    }
}
