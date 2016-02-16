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
    public partial class CondicionOperacion : ReporteBase
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
                hddPlanta.Value = ((DropDownList)Page.Master.FindControl("cmbPlanta")).SelectedValue;
                FechaIni.Value = DateTime.Today;
                FechaFin.Value = DateTime.Today;
                llenarvacio();

                igtbl_reBuscaBtn.Enabled = false;
                LExport.Visible = false;
                LAddNew.Visible = false;
                bool editar = false;
                foreach (ScreenPermission sp in new Actions().GetActionBySreen(user.CodRol, Request.Url.LocalPath))
                {
                    switch (sp.ActionCode)
                    {
                        case 1: //Buscar
                            igtbl_reBuscaBtn.Enabled = true;
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

                DropDownList cmbPlanta = ((DropDownList)Page.Master.FindControl("cmbPlanta"));
                svcSCPP.SCPPClient svc = new svcSCPP.SCPPClient();
               

                //   UltraWebGrid1.Columns[0].CellStyle.HorizontalAlign = HorizontalAlign.Center;
                //   UltraWebGrid1.Columns[1].CellStyle.HorizontalAlign = HorizontalAlign.Center;

                cmbProceso.Items.AddRange(GetItemsConSeleccioneTodos(svc.ObtenerProcesoCbo(int.Parse(cmbPlanta.SelectedValue)), "DescripcionProceso", "ClaveProceso"));
                cmbCentroT.Items.AddRange(GetItemsConSeleccioneTodos(svc.ObtenerCentroTrabajoCbo(-1, -1), "DesCentroTrabajo", "CodCentroTrabajo"));
                cmbArea.Items.AddRange(GetItemsConSeleccioneTodos(svc.ObtenerArea(-1), "AreaDesc", "CodArea"));
                cmbAreaH.Items.AddRange(GetItems(svc.ObtenerArea(-1), "AreaDesc", "CodArea"));
            }
        }

        protected void cmbProceso_SelectedIndexChanged(object sender, EventArgs e)
        {
            DropDownList cmbPlanta = ((DropDownList)Page.Master.FindControl("cmbPlanta"));
            svcSCPP.SCPPClient svc = new svcSCPP.SCPPClient();
            cmbCentroT.Items.Clear();
            cmbArea.Items.Clear();
            cmbCentroT.Items.AddRange(GetItemsConSeleccioneTodos(svc.ObtenerCentroTrabajoCbo(int.Parse(cmbPlanta.SelectedValue), int.Parse(cmbProceso.SelectedValue)), "DesCentroTrabajo", "CodCentroTrabajo"));
        }

        protected void cmbCentroT_SelectedIndexChanged(object sender, EventArgs e)
        {
            svcSCPP.SCPPClient svc = new svcSCPP.SCPPClient();
         cmbArea.Items.Clear();
         cmbArea.Items.AddRange(GetItemsConSeleccioneTodos(svc.ObtenerArea(int.Parse(cmbCentroT.SelectedValue)), "AreaDesc", "CodArea"));

        }

        protected void btnLlenaGrid_Click(object sender, EventArgs e)
       
        {
            svcSCPP.SCPPClient svc = new svcSCPP.SCPPClient();
            DropDownList cmbPlanta = ((DropDownList)Page.Master.FindControl("cmbPlanta"));
            List<Common.SolutionEntityFramework.BaseSolutionEntity> Lista = svc.ObtenerCondicionOperacion(int.Parse(cmbPlanta.SelectedValue), int.Parse(cmbProceso.SelectedValue), int.Parse(cmbArea.SelectedValue), DateTime.Parse(FechaIni.Value.ToString()), DateTime.Parse(FechaFin.Value.ToString()));
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
                UltraWebGrid1.Columns[2].Hidden = true;
                UltraWebGrid1.Columns[3].Hidden = true;
                UltraWebGrid1.Columns[4].Hidden = true;
                UltraWebGrid1.Columns[5].Width = 80;
                UltraWebGrid1.Columns[6].Width = 100;
                UltraWebGrid1.Columns[7].Width = 100;
                UltraWebGrid1.Columns[8].Hidden = true;
                UltraWebGrid1.Columns[9].Hidden = true;
                UltraWebGrid1.Columns[10].Width = 100;
                UltraWebGrid1.Columns[11].Width = 100;
                UltraWebGrid1.Columns[12].Hidden = true;

             

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
                UltraWebGrid1.Columns[10].AllowResize = AllowSizing.Fixed;
                UltraWebGrid1.Columns[11].AllowResize = AllowSizing.Fixed;

                UltraWebGrid1.Columns[0].Header.Style.Wrap = true;
                UltraWebGrid1.Columns[1].Header.Style.Wrap = true;
                UltraWebGrid1.Columns[2].Header.Style.Wrap = true;
                UltraWebGrid1.Columns[3].Header.Style.Wrap = true;
                UltraWebGrid1.Columns[4].Header.Style.Wrap = true;
                UltraWebGrid1.Columns[5].Header.Style.Wrap = true;
                UltraWebGrid1.Columns[6].Header.Style.Wrap = true;
                UltraWebGrid1.Columns[7].Header.Style.Wrap = true;
                UltraWebGrid1.Columns[8].Header.Style.Wrap = true;
                UltraWebGrid1.Columns[9].Header.Style.Wrap = true;
                UltraWebGrid1.Columns[10].Header.Style.Wrap = true;
                UltraWebGrid1.Columns[11].Header.Style.Wrap = true;


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
                UltraWebGrid1.Columns[11].Header.Style.HorizontalAlign = HorizontalAlign.Center;
              

                UltraWebGrid1.Columns[0].CellStyle.HorizontalAlign = HorizontalAlign.Center;
                UltraWebGrid1.Columns[1].CellStyle.HorizontalAlign = HorizontalAlign.Center;
                UltraWebGrid1.Columns[2].CellStyle.HorizontalAlign = HorizontalAlign.Center;
                UltraWebGrid1.Columns[3].CellStyle.HorizontalAlign = HorizontalAlign.Center;
                UltraWebGrid1.Columns[4].CellStyle.HorizontalAlign = HorizontalAlign.Center;
                UltraWebGrid1.Columns[5].CellStyle.HorizontalAlign = HorizontalAlign.Center;
                UltraWebGrid1.Columns[6].CellStyle.HorizontalAlign = HorizontalAlign.Center;
                UltraWebGrid1.Columns[7].CellStyle.HorizontalAlign = HorizontalAlign.Center;
                UltraWebGrid1.Columns[8].CellStyle.HorizontalAlign = HorizontalAlign.Center;
                UltraWebGrid1.Columns[9].CellStyle.HorizontalAlign = HorizontalAlign.Center;
                UltraWebGrid1.Columns[10].CellStyle.HorizontalAlign = HorizontalAlign.Center;
                UltraWebGrid1.Columns[11].CellStyle.HorizontalAlign = HorizontalAlign.Center;

                UltraWebGrid1.Columns[5].Format = "dd-MM-yyyy";
                UltraWebGrid1.Columns[10].Type = ColumnType.CheckBox;
                UltraWebGrid1.Columns[11].Type = ColumnType.CheckBox;

               
            }
        }

        protected void llenarvacio()
        {
            UltraWebGrid1.ResetColumns();
            UltraWebGrid1.DataBind();
            UltraWebGrid1.Columns.Add("Fecha");
            UltraWebGrid1.Columns.Add("Temperatura");
            UltraWebGrid1.Columns.Add("Humedad");
            UltraWebGrid1.Columns.Add("Autorización");
            UltraWebGrid1.Columns.Add("Activo");
            

            UltraWebGrid1.Columns[0].Header.Caption = "Fecha";
            UltraWebGrid1.Columns[1].Header.Caption = "Temperatura";
            UltraWebGrid1.Columns[2].Header.Caption = "Humedad";
            UltraWebGrid1.Columns[3].Header.Caption = "Autorización";
            UltraWebGrid1.Columns[4].Header.Caption = "Activo";

            UltraWebGrid1.Columns[0].Header.Style.HorizontalAlign = HorizontalAlign.Center;
            UltraWebGrid1.Columns[1].Header.Style.HorizontalAlign = HorizontalAlign.Center;
            UltraWebGrid1.Columns[2].Header.Style.HorizontalAlign = HorizontalAlign.Center;
            UltraWebGrid1.Columns[3].Header.Style.HorizontalAlign = HorizontalAlign.Center;
            UltraWebGrid1.Columns[4].Header.Style.HorizontalAlign = HorizontalAlign.Center;

            
            UltraWebGrid1.Columns[1].Width = 100;
         
        }
        protected void BotonGuardar_click(object sender, EventArgs e)
        {
            svcSCPP.SCPPClient svc = new svcSCPP.SCPPClient();
            LAMOSA.SCPP.Server.BusinessEntity.CondicionOperacionGuarda co = new LAMOSA.SCPP.Server.BusinessEntity.CondicionOperacionGuarda();

            try
            {
                int CodCondicionOperacion = -1;
                int.TryParse(hddCodCondicionOperacion.Value, out CodCondicionOperacion);
                co.CodCondicionOperacion = CodCondicionOperacion;
                co.CodArea= int.Parse(hddCodArea.Value);
                co.Temperatura = double.Parse(hddTemperatura.Value);
                co.Humedad = double.Parse(hddHumedad.Value);
                svc.GuardarCondicionOperacion(co);
             
                btnLlenaGrid_Click(sender, e);
                WebAsyncRefreshPanel1.DataBind();
            }
            catch (Exception err)
            {

                throw err;
            }
        }

        protected void BotonAutorizar_click(object sender, EventArgs e)
        {
            Usuario user = (Usuario)Session["UserLogged"];
            var CodUser = user.CodUsuario;
            svcSCPP.SCPPClient svc = new svcSCPP.SCPPClient();
            LAMOSA.SCPP.Server.BusinessEntity.CondicionOperacionAutoriza co = new LAMOSA.SCPP.Server.BusinessEntity.CondicionOperacionAutoriza();

            try
            {
                int CodCondicionOperacion = -1;
                int.TryParse(hddCodCondicionOperacion.Value, out CodCondicionOperacion);
                co.CodCondicionOperacion = CodCondicionOperacion;
                co.UsuarioAutoriza = CodUser;
                svc.AutorizaCondicionOperacion(co);
                btnLlenaGrid_Click(sender, e);
                WebAsyncRefreshPanel1.DataBind();
                btnLlenaGrid_Click(sender, e);
            }
            catch (Exception err)
            {

                throw err;
            }
        }

        

        protected void btnExporta_Click(object sender, EventArgs e)
        {
            svcSCPP.SCPPClient svc = new svcSCPP.SCPPClient();

            DataSet dsReportXLS = new DataSet();
            dsReportXLS.Tables.Add();

            string[] colnames = LAMOSA.SCPP.Server.BusinessEntity.CondicionOperacion.GetPropertyNamesArray();
            foreach (string colname in colnames)
            {
                dsReportXLS.Tables[0].Columns.Add(colname);
            }
            DropDownList cmbPlanta = ((DropDownList)Page.Master.FindControl("cmbPlanta"));
            List<Common.SolutionEntityFramework.BaseSolutionEntity> datos = svc.ObtenerCondicionOperacion(int.Parse(cmbPlanta.SelectedValue), int.Parse(cmbProceso.SelectedValue), int.Parse(cmbArea.SelectedValue), DateTime.Parse(FechaIni.Value.ToString()), DateTime.Parse(FechaFin.Value.ToString()));
            foreach (Common.SolutionEntityFramework.BaseSolutionEntity item in datos)
            {
                dsReportXLS.Tables[0].Rows.Add(((LAMOSA.SCPP.Server.BusinessEntity.CondicionOperacion)item).ToObjectArray());
            }
            ExportToExcel(dsReportXLS, 0, Response, nombre.Value);

        }

        protected void cambio_pagina(object sender, Infragistics.WebUI.UltraWebGrid.PageEventArgs e)
        {
            UltraWebGrid1.DisplayLayout.Pager.CurrentPageIndex = e.NewPageIndex;
            btnLlenaGrid_Click(sender, e);
        }

        #endregion


        #endregion
    }
}
