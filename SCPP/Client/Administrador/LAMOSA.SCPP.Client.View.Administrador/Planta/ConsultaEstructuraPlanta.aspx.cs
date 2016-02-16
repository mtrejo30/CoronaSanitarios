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
    public partial class ConsultaEstructuraPlanta : ReporteBase
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
            DropDownList cmbPlanta = ((DropDownList)Page.Master.FindControl("cmbPlanta"));
            TxtPlanta.Text = cmbPlanta.SelectedItem.Text;
            svcSCPP.SCPPClient svc = new svcSCPP.SCPPClient();
            List<Common.SolutionEntityFramework.BaseSolutionEntity> Lista = svc.ObtenerEstructuraPlanta(int.Parse(cmbPlanta.SelectedValue));
            UltraWebGrid1.DataSource = Lista;
            if (Lista.Count <= 0)
            {
                llenarvacio();
            }
            else
            {
                UltraWebGrid1.ResetColumns();
                UltraWebGrid1.DataBind();

                UltraWebGrid1.Columns[7].Hidden = true;

                UltraWebGrid1.Columns[0].Header.Caption = "Almacén";
                UltraWebGrid1.Columns[2].CellStyle.HorizontalAlign = HorizontalAlign.Center;

                UltraWebGrid1.Columns[3].Header.Caption = "Linea de producción";
                UltraWebGrid1.Columns[4].Header.Caption = "Centro de trabajo";
                UltraWebGrid1.Columns[5].Header.Caption = "Área";
                UltraWebGrid1.Columns[6].Header.Caption = "Máquina";

                UltraWebGrid1.Columns[3].Header.Style.Wrap = true;

                UltraWebGrid1.Columns[0].Header.Style.HorizontalAlign = HorizontalAlign.Center;
                UltraWebGrid1.Columns[1].Header.Style.HorizontalAlign = HorizontalAlign.Center;
                UltraWebGrid1.Columns[2].Header.Style.HorizontalAlign = HorizontalAlign.Center;
                UltraWebGrid1.Columns[3].Header.Style.HorizontalAlign = HorizontalAlign.Center;
                UltraWebGrid1.Columns[4].Header.Style.HorizontalAlign = HorizontalAlign.Center;
                UltraWebGrid1.Columns[5].Header.Style.HorizontalAlign = HorizontalAlign.Center;
                UltraWebGrid1.Columns[6].Header.Style.HorizontalAlign = HorizontalAlign.Center;
                UltraWebGrid1.Columns[7].Header.Style.HorizontalAlign = HorizontalAlign.Center;
                UltraWebGrid1.Columns[0].Width = 150;
                UltraWebGrid1.Columns[1].Width = 85;
                UltraWebGrid1.Columns[4].Width = 130;
                UltraWebGrid1.Columns[5].Width = 160;
                UltraWebGrid1.Columns[6].Width = 130;

            }

        }

        protected void llenarvacio()
        {
            UltraWebGrid1.ResetColumns();
            UltraWebGrid1.DataBind();
            UltraWebGrid1.Columns.Add("Almacén");
            UltraWebGrid1.Columns.Add("Planta");
            UltraWebGrid1.Columns.Add("Linea de producción");
            UltraWebGrid1.Columns.Add("Proceso");
            UltraWebGrid1.Columns.Add("Centro de trabajo");
            UltraWebGrid1.Columns.Add("Área");
            UltraWebGrid1.Columns.Add("Máquina");
            

            UltraWebGrid1.Columns[0].Header.Caption = "Almacén";
            UltraWebGrid1.Columns[1].Header.Caption = "Planta";
            UltraWebGrid1.Columns[2].Header.Caption = "Linea de prod.";
            UltraWebGrid1.Columns[3].Header.Caption = "Proceso";
            UltraWebGrid1.Columns[4].Header.Caption = "Centro de trabajo";
            UltraWebGrid1.Columns[5].Header.Caption = "Área";
            UltraWebGrid1.Columns[6].Header.Caption = "Máquina";

            UltraWebGrid1.Columns[2].Width = 135;
            UltraWebGrid1.Columns[4].Width = 120;

            UltraWebGrid1.Columns[2].CellStyle.HorizontalAlign = HorizontalAlign.Center;

            UltraWebGrid1.Columns[0].Header.Style.HorizontalAlign = HorizontalAlign.Center;
            UltraWebGrid1.Columns[1].Header.Style.HorizontalAlign = HorizontalAlign.Center;
            UltraWebGrid1.Columns[2].Header.Style.HorizontalAlign = HorizontalAlign.Center;
            UltraWebGrid1.Columns[3].Header.Style.HorizontalAlign = HorizontalAlign.Center;
            UltraWebGrid1.Columns[4].Header.Style.HorizontalAlign = HorizontalAlign.Center;
            UltraWebGrid1.Columns[5].Header.Style.HorizontalAlign = HorizontalAlign.Center;
            UltraWebGrid1.Columns[6].Header.Style.HorizontalAlign = HorizontalAlign.Center;
  
        }

        protected void btnExporta_Click(object sender, EventArgs e)
        {
            DropDownList cmbPlanta = ((DropDownList)Page.Master.FindControl("cmbPlanta"));
            svcSCPP.SCPPClient svc = new svcSCPP.SCPPClient();

            DataSet dsReportXLS = new DataSet();
            dsReportXLS.Tables.Add();

            string[] colnames = LAMOSA.SCPP.Server.BusinessEntity.EstructuraPlanta.GetPropertyNamesArray();
            foreach (string colname in colnames)
            {
                dsReportXLS.Tables[0].Columns.Add(colname);
            }
            List<Common.SolutionEntityFramework.BaseSolutionEntity> datos = svc.ObtenerEstructuraPlanta(int.Parse(cmbPlanta.SelectedValue));
            foreach (Common.SolutionEntityFramework.BaseSolutionEntity item in datos)
            {
                dsReportXLS.Tables[0].Rows.Add(((LAMOSA.SCPP.Server.BusinessEntity.EstructuraPlanta)item).ToObjectArray());
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
