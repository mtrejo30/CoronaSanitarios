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

namespace LAMOSA.SCPP.Client.View.Administrador.Planta
{
    public partial class Proceso : ReporteBase
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

                    llenargrid();
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

        protected void llenargrid()
        {
            svcSCPP.SCPPClient svc = new svcSCPP.SCPPClient();
            List<Common.SolutionEntityFramework.BaseSolutionEntity> Lista = svc.ObtenerProceso();
            UltraWebGrid1.DataSource = Lista;
            if (Lista.Count <= 0)
            {

                llenarvacio();
            }
            else
            {
                UltraWebGrid1.ResetColumns();
                UltraWebGrid1.DataBind();

                UltraWebGrid1.Columns[4].Hidden = true;
                UltraWebGrid1.Columns[5].Hidden = true;
                UltraWebGrid1.Columns[6].Hidden = true;
                UltraWebGrid1.Columns[8].Hidden = true;

                UltraWebGrid1.Columns[0].Header.Caption = "Clave Proceso";
                UltraWebGrid1.Columns[1].Header.Caption = "Descripción";
                UltraWebGrid1.Columns[2].Header.Caption = "MFG/PRO";
                UltraWebGrid1.Columns[3].Header.Caption = "Requerido";
                UltraWebGrid1.Columns[7].Header.Caption = "Activo";

                UltraWebGrid1.Columns[0].CellStyle.HorizontalAlign = HorizontalAlign.Center;
                UltraWebGrid1.Columns[2].CellStyle.HorizontalAlign = HorizontalAlign.Center;
                UltraWebGrid1.Columns[3].CellStyle.HorizontalAlign = HorizontalAlign.Center;
                UltraWebGrid1.Columns[7].CellStyle.HorizontalAlign = HorizontalAlign.Center;

                UltraWebGrid1.Columns[0].Header.Style.HorizontalAlign = HorizontalAlign.Center;
                UltraWebGrid1.Columns[2].Header.Style.HorizontalAlign = HorizontalAlign.Center;
                UltraWebGrid1.Columns[3].Header.Style.HorizontalAlign = HorizontalAlign.Center;
                UltraWebGrid1.Columns[7].Header.Style.HorizontalAlign = HorizontalAlign.Center;


            }

        }


        protected void llenarvacio()
        {
            UltraWebGrid1.ResetColumns();
            UltraWebGrid1.DataBind();
            UltraWebGrid1.Columns.Add("Clave Proceso");
            UltraWebGrid1.Columns.Add("Descripción");
            UltraWebGrid1.Columns.Add("MFG/PRO");
            UltraWebGrid1.Columns.Add("Requerido");
            UltraWebGrid1.Columns.Add("Activo");
            

            UltraWebGrid1.Columns[0].Header.Caption = "Clave Proceso";
            UltraWebGrid1.Columns[1].Header.Caption = "Descripción";
            UltraWebGrid1.Columns[2].Header.Caption = "MFG/PRO";
            UltraWebGrid1.Columns[3].Header.Caption = "Requerido";
            UltraWebGrid1.Columns[4].Header.Caption = "Activo";

            UltraWebGrid1.Columns[0].Header.Style.HorizontalAlign = HorizontalAlign.Center;
            UltraWebGrid1.Columns[1].Header.Style.HorizontalAlign = HorizontalAlign.Center;
            UltraWebGrid1.Columns[2].Header.Style.HorizontalAlign = HorizontalAlign.Center;
            UltraWebGrid1.Columns[3].Header.Style.HorizontalAlign = HorizontalAlign.Center;
            UltraWebGrid1.Columns[4].Header.Style.HorizontalAlign = HorizontalAlign.Center;
    
          
            UltraWebGrid1.Columns[2].Width = 70;
            UltraWebGrid1.Columns[3].Width = 70;
            UltraWebGrid1.Columns[4].Width = 70;
        }

        protected void btnExporta_Click(object sender, EventArgs e)
        {
            svcSCPP.SCPPClient svc = new svcSCPP.SCPPClient();

            DataSet dsReportXLS = new DataSet();
            dsReportXLS.Tables.Add();

            string[] colnames = LAMOSA.SCPP.Server.BusinessEntity.Proceso.GetPropertyNamesArray();
            foreach (string colname in colnames)
            {
                dsReportXLS.Tables[0].Columns.Add(colname);
            }
            List<Common.SolutionEntityFramework.BaseSolutionEntity> datos = svc.ObtenerProceso();
            foreach (Common.SolutionEntityFramework.BaseSolutionEntity item in datos)
            {
                dsReportXLS.Tables[0].Rows.Add(((LAMOSA.SCPP.Server.BusinessEntity.Proceso)item).ToObjectArray());
            }
            ExportToExcel(dsReportXLS, 0, Response, nombre.Value);
        }

          protected void cambio_pagina(object sender, Infragistics.WebUI.UltraWebGrid.PageEventArgs e)
        {
            UltraWebGrid1.DisplayLayout.Pager.CurrentPageIndex = e.NewPageIndex;
            llenargrid();
        }

        #endregion
        #endregion
    }
}
