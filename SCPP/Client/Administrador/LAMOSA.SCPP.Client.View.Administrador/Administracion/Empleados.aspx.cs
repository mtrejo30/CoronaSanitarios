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

namespace LAMOSA.SCPP.Client.View.Administrador.Administracion
{
    public partial class Empleados : ReporteBase
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
                    llenarvacio();
                    DropDownList cmbPlanta = ((DropDownList)Page.Master.FindControl("cmbPlanta"));
                    //Planta.Value = cmbPlanta.SelectedItem.Text;
                    svcSCPP.SCPPClient svc = new svcSCPP.SCPPClient();
                    cmbPlanta2.Items.AddRange(GetItemsConSeleccioneTodos(svc.ObtenerPlantaCbo(), "DescripcionPlanta", "ClavePlanta"));
                    cmbPuesto.Items.AddRange(GetItemsConSeleccioneTodos(svc.ObtenerPuesto(), "DesPuesto", "CodPuesto"));


                    
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

            DataSet dsReportXLS = new DataSet();
            dsReportXLS.Tables.Add();

            string[] colnames = LAMOSA.SCPP.Server.BusinessEntity.Empleado.GetPropertyNamesArray();
            foreach (string colname in colnames)
            {
                dsReportXLS.Tables[0].Columns.Add(colname);
            }
            List<Common.SolutionEntityFramework.BaseSolutionEntity> datos = svc.ObtenerEmpleado(int.Parse(cmbPlanta2.SelectedValue), int.Parse(cmbPuesto.SelectedValue));
            foreach (Common.SolutionEntityFramework.BaseSolutionEntity item in datos)
            {
                dsReportXLS.Tables[0].Rows.Add(((LAMOSA.SCPP.Server.BusinessEntity.Empleado)item).ToObjectArray());
            }
            ExportToExcel(dsReportXLS, 0, Response, nombre.Value);

        }

        protected void cmbPlanta_SelectedIndexChanged(object sender, EventArgs e)
        {

            if (cmbPlanta2.SelectedIndex != 0 & cmbPuesto.SelectedIndex != 0)
            {
                llenargrid();
            }
            else
            {
                llenarvacio();
            }
        }


        protected void cmbPuesto_SelectedIndexChanged(object sender, EventArgs e)
        {
            
            if (cmbPlanta2.SelectedIndex != 0 & cmbPuesto.SelectedIndex != 0)
            {
               llenargrid();
            }
            else
            {
               llenarvacio();
            }

        }
        #endregion

        protected void llenargrid()
       {
            svcSCPP.SCPPClient svc = new svcSCPP.SCPPClient();
          
            List<Common.SolutionEntityFramework.BaseSolutionEntity> Lista  = svc.ObtenerEmpleado(int.Parse(cmbPlanta2.SelectedValue), int.Parse(cmbPuesto.SelectedValue));
            UltraWebGrid1.DataSource = Lista;
            if (Lista.Count <= 0)
            {
                llenarvacio();
            }
            else
            {
                UltraWebGrid1.ResetColumns();
                UltraWebGrid1.DataBind();

                UltraWebGrid1.DisplayLayout.ScrollBar = ScrollBar.Always;
                UltraWebGrid1.Columns[8].Hidden = true;
                UltraWebGrid1.Columns[9].Hidden = true;

                UltraWebGrid1.Columns[0].Header.Caption = "Clave MFG";
                UltraWebGrid1.Columns[1].Header.Caption = "Clave nómina";
                UltraWebGrid1.Columns[2].Header.Caption = "Nombre";
                UltraWebGrid1.Columns[3].Header.Caption = "Apellido paterno";
                UltraWebGrid1.Columns[4].Header.Caption = "Apellido materno";
                UltraWebGrid1.Columns[5].Header.Caption = "Puesto";
                UltraWebGrid1.Columns[6].Header.Caption = "Centro de trabajo";
                UltraWebGrid1.Columns[7].Header.Caption = "Fecha";

                UltraWebGrid1.Columns[0].Header.Style.HorizontalAlign = HorizontalAlign.Center;
                UltraWebGrid1.Columns[1].Header.Style.HorizontalAlign = HorizontalAlign.Center;
                UltraWebGrid1.Columns[2].Header.Style.HorizontalAlign = HorizontalAlign.Center;
                UltraWebGrid1.Columns[3].Header.Style.HorizontalAlign = HorizontalAlign.Center;
                UltraWebGrid1.Columns[4].Header.Style.HorizontalAlign = HorizontalAlign.Center;
                UltraWebGrid1.Columns[5].Header.Style.HorizontalAlign = HorizontalAlign.Center;
                UltraWebGrid1.Columns[6].Header.Style.HorizontalAlign = HorizontalAlign.Center;
                UltraWebGrid1.Columns[7].Header.Style.HorizontalAlign = HorizontalAlign.Center;


                UltraWebGrid1.Columns[0].CellStyle.HorizontalAlign = HorizontalAlign.Center;
                UltraWebGrid1.Columns[1].CellStyle.HorizontalAlign = HorizontalAlign.Center;
                UltraWebGrid1.Columns[7].CellStyle.HorizontalAlign = HorizontalAlign.Center;

                UltraWebGrid1.Columns[0].AllowResize = AllowSizing.Fixed;
                UltraWebGrid1.Columns[1].AllowResize = AllowSizing.Fixed;
                UltraWebGrid1.Columns[2].AllowResize = AllowSizing.Fixed;
                UltraWebGrid1.Columns[3].AllowResize = AllowSizing.Fixed;
                UltraWebGrid1.Columns[4].AllowResize = AllowSizing.Fixed;
                UltraWebGrid1.Columns[5].AllowResize = AllowSizing.Fixed;
                UltraWebGrid1.Columns[6].AllowResize = AllowSizing.Fixed;
                UltraWebGrid1.Columns[7].AllowResize = AllowSizing.Fixed;
        


                UltraWebGrid1.Columns[0].Header.Style.Wrap = true;
                UltraWebGrid1.Columns[1].Header.Style.Wrap = true;
                UltraWebGrid1.Columns[2].Header.Style.Wrap = true;
                UltraWebGrid1.Columns[3].Header.Style.Wrap = true;
                UltraWebGrid1.Columns[4].Header.Style.Wrap = true;
                UltraWebGrid1.Columns[5].Header.Style.Wrap = true;
                UltraWebGrid1.Columns[6].Header.Style.Wrap = true;
                UltraWebGrid1.Columns[7].Header.Style.Wrap = true;
            
               

                UltraWebGrid1.Columns[7].Format = "dd-MM-yyyy";

                UltraWebGrid1.Columns[2].Width = 160;
                UltraWebGrid1.Columns[3].Width = 100;
                UltraWebGrid1.Columns[4].Width = 100;
                UltraWebGrid1.Columns[6].Width = 150;
                UltraWebGrid1.Width = 800;
            }  
        }

        protected void cambio_pagina(object sender, Infragistics.WebUI.UltraWebGrid.PageEventArgs e)
        {
            UltraWebGrid1.DisplayLayout.Pager.CurrentPageIndex = e.NewPageIndex;
            llenargrid();
        }

       protected void llenarvacio()
       {
     
           UltraWebGrid1.ResetColumns();
           UltraWebGrid1.DataBind();
           UltraWebGrid1.Columns.Add("Clave MFG");
           UltraWebGrid1.Columns.Add("Clave nómina");
           UltraWebGrid1.Columns.Add("Nombre");
           UltraWebGrid1.Columns.Add("Apellido paterno");
           UltraWebGrid1.Columns.Add("Apellido materno");
           UltraWebGrid1.Columns.Add("Puesto");
           UltraWebGrid1.Columns.Add("Centro de trabajo");
           UltraWebGrid1.Columns.Add("Fecha");

           UltraWebGrid1.Columns[0].Header.Caption = "Clave MFG";
           UltraWebGrid1.Columns[1].Header.Caption = "Clave nómina";
           UltraWebGrid1.Columns[2].Header.Caption = "Nombre";
           UltraWebGrid1.Columns[3].Header.Caption = "Apellido paterno";
           UltraWebGrid1.Columns[4].Header.Caption = "Apellido materno";
           UltraWebGrid1.Columns[5].Header.Caption = "Puesto";
           UltraWebGrid1.Columns[6].Header.Caption = "Centro de trabajo";
           UltraWebGrid1.Columns[7].Header.Caption = "Fecha";

           UltraWebGrid1.Columns[0].Header.Style.Wrap = true;
           UltraWebGrid1.Columns[1].Header.Style.Wrap = true;
           UltraWebGrid1.Columns[2].Header.Style.Wrap = true;
           UltraWebGrid1.Columns[3].Header.Style.Wrap = true;
           UltraWebGrid1.Columns[4].Header.Style.Wrap = true;
           UltraWebGrid1.Columns[5].Header.Style.Wrap = true;
           UltraWebGrid1.Columns[6].Header.Style.Wrap = true;
           UltraWebGrid1.Columns[7].Header.Style.Wrap = true;
            

           UltraWebGrid1.Columns[0].Header.Style.HorizontalAlign = HorizontalAlign.Center;
           UltraWebGrid1.Columns[1].Header.Style.HorizontalAlign = HorizontalAlign.Center;
           UltraWebGrid1.Columns[2].Header.Style.HorizontalAlign = HorizontalAlign.Center;
           UltraWebGrid1.Columns[3].Header.Style.HorizontalAlign = HorizontalAlign.Center;
           UltraWebGrid1.Columns[4].Header.Style.HorizontalAlign = HorizontalAlign.Center;
           UltraWebGrid1.Columns[5].Header.Style.HorizontalAlign = HorizontalAlign.Center;
           UltraWebGrid1.Columns[6].Header.Style.HorizontalAlign = HorizontalAlign.Center;
           UltraWebGrid1.Columns[7].Header.Style.HorizontalAlign = HorizontalAlign.Center;

           UltraWebGrid1.DisplayLayout.ScrollBar = ScrollBar.Never;

           UltraWebGrid1.Columns[3].Width = 110;
           UltraWebGrid1.Columns[4].Width = 110;
           UltraWebGrid1.Columns[6].Width = 110;
    }
        #endregion

       

    }
}
