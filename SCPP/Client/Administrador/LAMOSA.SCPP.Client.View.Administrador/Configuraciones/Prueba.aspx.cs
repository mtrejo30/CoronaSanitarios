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

namespace LAMOSA.SCPP.Client.View.Administrador.Configuraciones
{
    public partial class Prueba : ReporteBase
    {
        #region Constants
        protected string HTMLCboProceso = String.Empty;
        protected string HTMLCboProcesoFin = String.Empty;
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
               

                cmbProceso.Items.AddRange(GetItemsConSeleccioneTodos(svc.ObtenerProcesoCbo(int.Parse(cmbPlanta.SelectedValue)), "DescripcionProceso", "ClaveProceso"));
                cmbProcesoH.Items.AddRange(GetItems(svc.ObtenerProcesoCbo(int.Parse(cmbPlanta.SelectedValue)), "DescripcionProceso", "ClaveProceso"));
                llenarvacio();

                LExport.Visible = false;
                //LAddNew.Visible = false;
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
                    //UltraWebGrid1.DisplayLayout.AllowUpdateDefault = AllowUpdate.No;
                }
                }
               
            }
        }

        protected void btnExporta_Click(object sender, EventArgs e)
        {
            //Metodo para Generar el Reporte

            //////Reports.DataSet.dsUnidadAdmin dsUnidadA = new ControlPisoLamosa.CatalogosCommons.Reports.DataSet.dsUnidadAdmin();
            //////Reports.DataSet.dsUnidadAdminTableAdapters.PI_unidadadmin_sucursalTableAdapter ts = new ControlPisoLamosa.CatalogosCommons.Reports.DataSet.dsUnidadAdminTableAdapters.PI_unidadadmin_sucursalTableAdapter();
            //////ts.Fill(dsUnidadA.PI_unidadadmin_sucursal, Convert.ToInt32(cmbSucursal.SelectedValue));
            //GenerarReporte(ddlSeleccion.SelectedItem.Text.ToString(), null, new DataSet(), new Reportes.RPTurnos());
        }

        protected void cmbProceso_SelectedIndexChanged(object sender, EventArgs e)
        {
            svcSCPP.SCPPClient svc = new svcSCPP.SCPPClient();
            List<Common.SolutionEntityFramework.BaseSolutionEntity> Lista = svc.ObtenerPruebas(int.Parse(cmbProceso.SelectedValue));
            UltraWebGrid1.DataSource = Lista;
            if (Lista.Count <= 0)
            {
                llenarvacio();
            }
            else
            {
                
                UltraWebGrid1.ResetColumns();
                UltraWebGrid1.DataBind();

            
                UltraWebGrid1.Width = 682;
                UltraWebGrid1.Columns[0].Header.Caption = "Clave prueba";
                UltraWebGrid1.Columns[1].Header.Caption = "Descripción";
                UltraWebGrid1.Columns[2].Hidden = true;
                UltraWebGrid1.Columns[3].Header.Caption = "Proceso";
                UltraWebGrid1.Columns[4].Hidden = true;
                UltraWebGrid1.Columns[5].Header.Caption = "Proceso final"; 
                UltraWebGrid1.Columns[6].Header.Caption ="Fecha";
                UltraWebGrid1.Columns[7].Header.Caption = "Activo"; 
                UltraWebGrid1.Columns[8].Header.Caption = "Residencia Max";
                UltraWebGrid1.Columns[9].Hidden = true;
                UltraWebGrid1.Columns[10].Hidden = true;

                UltraWebGrid1.Columns[0].CellStyle.HorizontalAlign = HorizontalAlign.Center;
                UltraWebGrid1.Columns[6].CellStyle.HorizontalAlign = HorizontalAlign.Center;
                UltraWebGrid1.Columns[7].CellStyle.HorizontalAlign = HorizontalAlign.Center;
                UltraWebGrid1.Columns[8].CellStyle.HorizontalAlign = HorizontalAlign.Center;

                UltraWebGrid1.Columns[0].Header.Style.HorizontalAlign = HorizontalAlign.Center;
                UltraWebGrid1.Columns[1].Header.Style.HorizontalAlign = HorizontalAlign.Center;
                UltraWebGrid1.Columns[2].Header.Style.HorizontalAlign = HorizontalAlign.Center;
                UltraWebGrid1.Columns[3].Header.Style.HorizontalAlign = HorizontalAlign.Center;
                UltraWebGrid1.Columns[4].Header.Style.HorizontalAlign = HorizontalAlign.Center;
                UltraWebGrid1.Columns[5].Header.Style.HorizontalAlign = HorizontalAlign.Center;
                UltraWebGrid1.Columns[6].Header.Style.HorizontalAlign = HorizontalAlign.Center;
                UltraWebGrid1.Columns[7].Header.Style.HorizontalAlign = HorizontalAlign.Center;
                UltraWebGrid1.Columns[8].Header.Style.HorizontalAlign = HorizontalAlign.Center;
                UltraWebGrid1.Columns[9].Header.Style.HorizontalAlign = HorizontalAlign.Center;
                UltraWebGrid1.Columns[10].Header.Style.HorizontalAlign = HorizontalAlign.Center;
               
                UltraWebGrid1.Columns[6].Format = "dd-MM-yyyy";
                UltraWebGrid1.Columns[5].Header.Style.Wrap = true;

                UltraWebGrid1.Columns[1].Width = 150;
                UltraWebGrid1.Columns[3].Width = 80;
                UltraWebGrid1.Columns[4].Width = 80;
                UltraWebGrid1.Columns[5].Width = 100;
                UltraWebGrid1.Columns[6].Width = 80;
                UltraWebGrid1.Columns[7].Width = 70;
            }  
        }

        protected void llenarvacio()
        {
            UltraWebGrid1.ResetColumns();
            UltraWebGrid1.DataBind();
            UltraWebGrid1.Columns.Add("Clave prueba");
            UltraWebGrid1.Columns.Add("Descripción");
            UltraWebGrid1.Columns.Add("Proceso");
            UltraWebGrid1.Columns.Add("Proceso final");
            UltraWebGrid1.Columns.Add("Fecha");
            UltraWebGrid1.Columns.Add("Activo");
            UltraWebGrid1.Columns.Add("Residencia Max");
            

            UltraWebGrid1.Columns[0].Header.Caption = "Clave prueba";
            UltraWebGrid1.Columns[1].Header.Caption = "Descripción";
            UltraWebGrid1.Columns[2].Header.Caption = "Proceso";
            UltraWebGrid1.Columns[3].Header.Caption = "Proceso final";
            UltraWebGrid1.Columns[4].Header.Caption = "Fecha";
            UltraWebGrid1.Columns[5].Header.Caption = "Activo";
            UltraWebGrid1.Columns[6].Header.Caption = "Res. Max";
     
            UltraWebGrid1.Columns[0].Width = 100;
            UltraWebGrid1.Columns[1].Width = 150;
            UltraWebGrid1.Columns[2].Width = 90; 
            UltraWebGrid1.Columns[3].Width = 90;
            UltraWebGrid1.Columns[4].Width = 80;
            UltraWebGrid1.Columns[5].Width = 70;
            UltraWebGrid1.Columns[6].Width = 70;

            UltraWebGrid1.Columns[0].Header.Style.HorizontalAlign = HorizontalAlign.Center;
            UltraWebGrid1.Columns[1].Header.Style.HorizontalAlign = HorizontalAlign.Center;
            UltraWebGrid1.Columns[2].Header.Style.HorizontalAlign = HorizontalAlign.Center;
            UltraWebGrid1.Columns[3].Header.Style.HorizontalAlign = HorizontalAlign.Center;
            UltraWebGrid1.Columns[4].Header.Style.HorizontalAlign = HorizontalAlign.Center;
            UltraWebGrid1.Columns[5].Header.Style.HorizontalAlign = HorizontalAlign.Center;
            UltraWebGrid1.Columns[6].Header.Style.HorizontalAlign = HorizontalAlign.Center;
            
        }

        protected void BotonGuardar_click(object sender, EventArgs e)
        {
            svcSCPP.SCPPClient svc = new svcSCPP.SCPPClient();
            LAMOSA.SCPP.Server.BusinessEntity.Prueba p = new LAMOSA.SCPP.Server.BusinessEntity.Prueba();

            try
            {
                DropDownList cmbPlanta = ((DropDownList)Page.Master.FindControl("cmbPlanta"));
                int ClavePrueba = -1, iCodPlanta = -1;
                int.TryParse(cmbPlanta.SelectedValue, out iCodPlanta);
                int.TryParse(hddClavePrueba.Value, out ClavePrueba);
                p.ClavePrueba = ClavePrueba;
                p.DesPrueba = hddDesPrueba.Value;
                p.CodProceso = int.Parse(hddProceso.Value.ToString());
                p.CodProcesoFin = int.Parse(hddProcesoFin.Value.ToString());
                p.ResidenciaMax = int.Parse(hddResidenciaMax.Value.ToString());
              
                svc.GuardarPrueba(p,iCodPlanta);
                cmbProceso_SelectedIndexChanged(sender,e);
                WebAsyncRefreshPanel1.DataBind();
            }
            catch (Exception err)
            {

                throw err;
            }
        }

        protected void BotonEliminar_click(object sender, EventArgs e)
        {
            svcSCPP.SCPPClient svc = new svcSCPP.SCPPClient();
            LAMOSA.SCPP.Server.BusinessEntity.Prueba p = new LAMOSA.SCPP.Server.BusinessEntity.Prueba();

            try
            {
                svc.EliminaPrueba(int.Parse(hddClavePrueba.Value));
                cmbProceso_SelectedIndexChanged(sender,e);
                WebAsyncRefreshPanel1.DataBind();
            }
            catch (Exception err)
            {

                throw err;
            }
        }

        protected void cambio_pagina(object sender, Infragistics.WebUI.UltraWebGrid.PageEventArgs e)
        {
            UltraWebGrid1.DisplayLayout.Pager.CurrentPageIndex = e.NewPageIndex;
            cmbProceso_SelectedIndexChanged(sender, e);
        }

        #endregion
        #endregion


      }
    }
