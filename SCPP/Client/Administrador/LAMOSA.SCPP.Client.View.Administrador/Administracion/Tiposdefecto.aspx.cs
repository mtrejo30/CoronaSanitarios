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

using LAMOSA.SCPP.Server.BusinessEntity.Server;
using LAMOSA.SCPP.Server.BusinessEntity;
using Infragistics.Shared;
using Infragistics.Excel;

namespace LAMOSA.SCPP.Client.View.Administrador.Administracion
{
    public partial class Tiposdefecto : ReporteBase
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
            svcSCPP.SCPPClient svc = new svcSCPP.SCPPClient();
            DropDownList cmbPlanta = ((DropDownList)Page.Master.FindControl("cmbPlanta"));

            DataSet dsReportXLS = new DataSet();
            dsReportXLS.Tables.Add();

            string[] colnames = LAMOSA.SCPP.Server.BusinessEntity.Defecto.GetPropertyNamesArray();
            foreach (string colname in colnames)
            {
                dsReportXLS.Tables[0].Columns.Add(colname);
            }
            List<Common.SolutionEntityFramework.BaseSolutionEntity> datos = svc.ObtenerDefectos(int.Parse(cmbProceso.SelectedValue), int.Parse(cmbPlanta.SelectedValue));
            foreach (Common.SolutionEntityFramework.BaseSolutionEntity item in datos)
            {
                dsReportXLS.Tables[0].Rows.Add(((LAMOSA.SCPP.Server.BusinessEntity.Defecto)item).ToObjectArray());
            }
            ExportToExcel(dsReportXLS, 0, Response, nombre.Value);
        }

        protected void cmbProceso_SelectedIndexChanged(object sender, EventArgs e)
        {
            svcSCPP.SCPPClient svc = new svcSCPP.SCPPClient();
            DropDownList cmbPlanta = ((DropDownList)Page.Master.FindControl("cmbPlanta"));
            List<Common.SolutionEntityFramework.BaseSolutionEntity> Lista = svc.ObtenerDefectos(int.Parse(cmbProceso.SelectedValue), int.Parse(cmbPlanta.SelectedValue));
            
              UltraWebGrid1.DataSource = Lista;
              if (Lista.Count <= 0)
              {
                  llenarvacio();
              }
              else
              {
                  UltraWebGrid1.ResetColumns();
                  UltraWebGrid1.DataBind();
            
                
;

                  UltraWebGrid1.Columns[0].Header.Caption = "Clave única";
                  UltraWebGrid1.Columns[1].Header.Caption = "Clave defecto";
                  UltraWebGrid1.Columns[2].Header.Caption = "Descripción";
                  UltraWebGrid1.Columns[3].Hidden = true;
                  UltraWebGrid1.Columns[4].Header.Caption = "Tipo defecto";
                  UltraWebGrid1.Columns[5].Hidden = true;
                  UltraWebGrid1.Columns[6].Header.Caption = "Proceso";
                  UltraWebGrid1.Columns[7].Hidden = true;
                  UltraWebGrid1.Columns[8].Header.Caption = "Responsable";
                  UltraWebGrid1.Columns[9].Hidden = true;
       
 
                  UltraWebGrid1.Columns[2].Width = 200;
                  UltraWebGrid1.Columns[4].Width = 120;

                  UltraWebGrid1.Columns[0].Header.Style.HorizontalAlign = HorizontalAlign.Center;
                  UltraWebGrid1.Columns[1].Header.Style.HorizontalAlign = HorizontalAlign.Center;
                  UltraWebGrid1.Columns[2].Header.Style.HorizontalAlign = HorizontalAlign.Center;
                  UltraWebGrid1.Columns[4].Header.Style.HorizontalAlign = HorizontalAlign.Center;
                  UltraWebGrid1.Columns[6].Header.Style.HorizontalAlign = HorizontalAlign.Center;
                  UltraWebGrid1.Columns[8].Header.Style.HorizontalAlign = HorizontalAlign.Center;
              
                  UltraWebGrid1.Columns[0].CellStyle.HorizontalAlign = HorizontalAlign.Center;
                  UltraWebGrid1.Columns[1].CellStyle.HorizontalAlign = HorizontalAlign.Center;
                
              }
        }

        protected void llenarvacio()
        {
            UltraWebGrid1.ResetColumns();
            UltraWebGrid1.DataBind();
            UltraWebGrid1.Columns.Add("Clave única");
            UltraWebGrid1.Columns.Add("Clave defecto");
            UltraWebGrid1.Columns.Add("Descripción");
            UltraWebGrid1.Columns.Add("Tipo defecto");
            UltraWebGrid1.Columns.Add("Proceso");
            UltraWebGrid1.Columns.Add("Responsable");


            UltraWebGrid1.Columns[0].Header.Caption = "Clave única";
            UltraWebGrid1.Columns[1].Header.Caption = "Clave defecto";
            UltraWebGrid1.Columns[2].Header.Caption = "Descripción";
            UltraWebGrid1.Columns[3].Header.Caption = "Tipo defecto";
            UltraWebGrid1.Columns[4].Header.Caption = "Proceso";
            UltraWebGrid1.Columns[5].Header.Caption = "Responsable";

        

            UltraWebGrid1.Columns[0].Header.Style.HorizontalAlign = HorizontalAlign.Center;
            UltraWebGrid1.Columns[1].Header.Style.HorizontalAlign = HorizontalAlign.Center;
            UltraWebGrid1.Columns[2].Header.Style.HorizontalAlign = HorizontalAlign.Center;
            UltraWebGrid1.Columns[3].Header.Style.HorizontalAlign = HorizontalAlign.Center;
            UltraWebGrid1.Columns[4].Header.Style.HorizontalAlign = HorizontalAlign.Center;
            UltraWebGrid1.Columns[5].Header.Style.HorizontalAlign = HorizontalAlign.Center;
           
     
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
