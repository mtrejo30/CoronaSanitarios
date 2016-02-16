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
    public partial class CentroTrabajo : ReporteBase
    {
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
                    svcSCPP.SCPPClient svc = new svcSCPP.SCPPClient();
                    llenarvacio();
                    // Insertar los elementos: Selcciona... y Todos.

                    cmbProceso.Items.AddRange(GetItemsConSeleccioneTodos(svc.ObtenerProcesoCbo(int.Parse(cmbPlanta.SelectedValue)), "DescripcionProceso", "ClaveProceso"));
                    LExport.Visible = false;


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

                                break;
                            case 4: //Editar

                                break;
                        }


                    }
                }

            }
        }

        protected void btnExporta_Click(object sender, EventArgs e)
        {
            DropDownList cmbPlanta = ((DropDownList)Page.Master.FindControl("cmbPlanta"));
            svcSCPP.SCPPClient svc = new svcSCPP.SCPPClient();

            DataSet dsReportXLS = new DataSet();
            dsReportXLS.Tables.Add();

            string[] colnames = LAMOSA.SCPP.Server.BusinessEntity.CentroTrabajo.GetPropertyNamesArray();
            foreach (string colname in colnames)
            {
                dsReportXLS.Tables[0].Columns.Add(colname);
            }
            List<Common.SolutionEntityFramework.BaseSolutionEntity> datos = svc.ObtenerCentroTrabajo(int.Parse(cmbPlanta.SelectedValue), int.Parse(cmbProceso.SelectedValue));
            foreach (Common.SolutionEntityFramework.BaseSolutionEntity item in datos)
            {
                dsReportXLS.Tables[0].Rows.Add(((LAMOSA.SCPP.Server.BusinessEntity.CentroTrabajo)item).ToObjectArray());
            }
            ExportToExcel(dsReportXLS, 0, Response, nombre.Value);
        }

        protected void cmbProceso_SelectedIndexChanged(object sender, EventArgs e)
        {
            DropDownList cmbPlanta = ((DropDownList)Page.Master.FindControl("cmbPlanta"));
            svcSCPP.SCPPClient svc = new svcSCPP.SCPPClient();
            cmbProceso.DataSource = svc.ObtenerProcesoCbo(int.Parse(cmbPlanta.SelectedValue));
            List<Common.SolutionEntityFramework.BaseSolutionEntity> Lista = svc.ObtenerCentroTrabajo(int.Parse(cmbPlanta.SelectedValue), int.Parse(cmbProceso.SelectedValue));

            UltraWebGrid1.DataSource = Lista;
            if (Lista.Count <= 0)
            {
                llenarvacio();
            }
            else
            {
                UltraWebGrid1.ResetColumns();
                UltraWebGrid1.DataBind();

    
            

                UltraWebGrid1.Columns[0].Header.Caption = "Clave única";
                UltraWebGrid1.Columns[1].Header.Caption = "Clave centro de trabajo";
                UltraWebGrid1.Columns[2].Header.Caption = "Descripción";
                UltraWebGrid1.Columns[3].Hidden = true;
                UltraWebGrid1.Columns[4].Header.Caption = "Proceso";
                UltraWebGrid1.Columns[5].Header.Caption = "Línea de producción";
                UltraWebGrid1.Columns[6].Hidden = true;
                UltraWebGrid1.Columns[7].Hidden = true;
                UltraWebGrid1.Columns[8].Hidden = true;
               


                UltraWebGrid1.Columns[0].CellStyle.HorizontalAlign = HorizontalAlign.Center;
                UltraWebGrid1.Columns[1].CellStyle.HorizontalAlign = HorizontalAlign.Center;
                UltraWebGrid1.Columns[5].CellStyle.HorizontalAlign = HorizontalAlign.Center;

                UltraWebGrid1.Columns[0].Header.Style.HorizontalAlign = HorizontalAlign.Center;
                UltraWebGrid1.Columns[1].Header.Style.HorizontalAlign = HorizontalAlign.Center;
                UltraWebGrid1.Columns[2].Header.Style.HorizontalAlign = HorizontalAlign.Center;
                UltraWebGrid1.Columns[4].Header.Style.HorizontalAlign = HorizontalAlign.Center;
                UltraWebGrid1.Columns[5].Header.Style.HorizontalAlign = HorizontalAlign.Center;

                UltraWebGrid1.Columns[1].Header.Style.Wrap = true; 
                UltraWebGrid1.Columns[5].Header.Style.Wrap = true;

                UltraWebGrid1.Columns[2].Width = 200;
                UltraWebGrid1.Columns[4].Width = 150;
                UltraWebGrid1.Columns[5].Width = 150;
  
            }

        }

        protected void llenarvacio()
        {
           
            UltraWebGrid1.ResetColumns();
            UltraWebGrid1.DataBind();
            UltraWebGrid1.Columns.Add("Clave única");
            UltraWebGrid1.Columns.Add("Clave centro trabajo");
            UltraWebGrid1.Columns.Add("Descripción");
            UltraWebGrid1.Columns.Add("Proceso");
            UltraWebGrid1.Columns.Add("Línea de producción");

            UltraWebGrid1.Columns[0].Header.Caption = "Clave única";
            UltraWebGrid1.Columns[1].Header.Caption = "Clave centro de trabajo";
            UltraWebGrid1.Columns[2].Header.Caption = "Descripción";
            UltraWebGrid1.Columns[3].Header.Caption = "Proceso";
            UltraWebGrid1.Columns[4].Header.Caption = "Línea de producción";


            UltraWebGrid1.Columns[1].Header.Style.Wrap = true;
            UltraWebGrid1.Columns[4].Header.Style.Wrap = true;

            UltraWebGrid1.Columns[0].Header.Style.HorizontalAlign = HorizontalAlign.Center;
            UltraWebGrid1.Columns[1].Header.Style.HorizontalAlign = HorizontalAlign.Center;
            UltraWebGrid1.Columns[2].Header.Style.HorizontalAlign = HorizontalAlign.Center;
            UltraWebGrid1.Columns[3].Header.Style.HorizontalAlign = HorizontalAlign.Center;
           
            UltraWebGrid1.Columns[0].Width = 100;
            UltraWebGrid1.Columns[1].Width = 100;
            UltraWebGrid1.Columns[2].Width = 150;
            UltraWebGrid1.Columns[3].Width = 120;
            UltraWebGrid1.Columns[4].Width = 120;
            
        }


        protected void cambio_pagina(object sender, Infragistics.WebUI.UltraWebGrid.PageEventArgs e)
        {
            UltraWebGrid1.DisplayLayout.Pager.CurrentPageIndex = e.NewPageIndex;
            cmbProceso_SelectedIndexChanged(sender,e);
        }

        #endregion
        #endregion

    }
}