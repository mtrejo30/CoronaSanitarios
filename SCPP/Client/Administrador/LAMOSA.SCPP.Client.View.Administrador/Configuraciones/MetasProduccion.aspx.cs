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
    public partial class MetasProduccion : ReporteBase
    {  
        #region Constants
        protected string comilla = "'";
        protected string HTMLCboCalidad1 = String.Empty;
        protected string HTMLCboCalidad2 = String.Empty;
        protected string HTMLCboCalidad3 = String.Empty;
        protected string HTMLCboCalidad4 = String.Empty;
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

                hddPlanta.Value = ((DropDownList)Page.Master.FindControl("cmbPlanta")).SelectedValue;
                FechaIni.Value = DateTime.Today;
                FechaFin.Value = DateTime.Today;

                llenarvacio();
              
                svcSCPP.SCPPClient svc = new svcSCPP.SCPPClient();
                
                cmbCalidad.DataSource = svc.ObtenerCalidadesCbo();
                cmbCalidad.DataTextField = "DesCalidad";
                cmbCalidad.DataValueField = "CodCalidad";
                cmbCalidad.DataBind();

                btnLlenaGrid_Click(sender, e);

                llenaCombo();
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


                    

         

              
            }
        }

        protected void btnLlenaGrid_Click(object sender, EventArgs e)
        {
           svcSCPP.SCPPClient svc = new svcSCPP.SCPPClient();
            DropDownList cmbPlanta = ((DropDownList)Page.Master.FindControl("cmbPlanta"));
            List<Common.SolutionEntityFramework.BaseSolutionEntity> Lista = svc.ObtenerMetasProd(int.Parse(cmbPlanta.SelectedValue), DateTime.Parse(FechaIni.Value.ToString()), DateTime.Parse(FechaFin.Value.ToString()));
            UltraWebGrid1.DataSource = Lista;
             if (Lista.Count <= 0)
            {
                llenarvacio();
            }
            else
     
            {
                UltraWebGrid1.ResetColumns();
                UltraWebGrid1.DataBind();

                UltraWebGrid1.Columns[10].Hidden = true;
                UltraWebGrid1.Columns[11].Hidden = true;
                UltraWebGrid1.Columns[12].Hidden = true;
                UltraWebGrid1.Columns[13].Hidden = true;
                UltraWebGrid1.Columns[14].Hidden = true;
                UltraWebGrid1.Columns[15].Hidden = true;
                UltraWebGrid1.Columns[16].Hidden = true;
                UltraWebGrid1.Columns[17].Hidden = true;
                UltraWebGrid1.Columns[18].Hidden = true;
                UltraWebGrid1.Columns[19].Hidden = true;
                UltraWebGrid1.Columns[20].Hidden = true;
                UltraWebGrid1.Columns[21].Hidden = true;
                UltraWebGrid1.Columns[22].Hidden = true;
                UltraWebGrid1.Columns[23].Hidden = true;
                UltraWebGrid1.Columns[24].Hidden = true;
                UltraWebGrid1.Columns[25].Hidden = true;
                UltraWebGrid1.Columns[26].Hidden = true;
                UltraWebGrid1.Columns[27].Hidden = true;
                UltraWebGrid1.Columns[28].Hidden = true;
                UltraWebGrid1.Columns[29].Hidden = true;
                UltraWebGrid1.Columns[30].Hidden = true;
                UltraWebGrid1.Columns[31].Hidden = true;
                UltraWebGrid1.Columns[32].Hidden = true;
                UltraWebGrid1.Columns[33].Hidden = true;
                UltraWebGrid1.Columns[34].Hidden = true;
              

                UltraWebGrid1.Columns[0].Header.Caption = "Piezas procesadas";
                UltraWebGrid1.Columns[1].Header.Caption = "Cantidad inventarios";
                UltraWebGrid1.Columns[2].Header.Caption = "Piezas malas";
                UltraWebGrid1.Columns[3].Header.Caption = "Piezas malas verdes";
                UltraWebGrid1.Columns[4].Header.Caption = "Piezas malas quemado";
                UltraWebGrid1.Columns[5].Header.Caption = "% calidad de 1";
                UltraWebGrid1.Columns[6].Header.Caption = "% calidad de 2";
                UltraWebGrid1.Columns[7].Header.Caption = "% calidad de 3";
                UltraWebGrid1.Columns[8].Header.Caption = "% calidad de 4";
                UltraWebGrid1.Columns[9].Header.Caption = "Activo";

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

                UltraWebGrid1.Columns[0].Width = 100;
                UltraWebGrid1.Columns[1].Width = 100;
                UltraWebGrid1.Columns[3].Width = 100;
                UltraWebGrid1.Columns[4].Width = 100;
                UltraWebGrid1.Columns[5].Width = 120;
                UltraWebGrid1.Columns[6].Width = 120;
                UltraWebGrid1.Columns[7].Width = 120;
                UltraWebGrid1.Columns[8].Width = 120;
                UltraWebGrid1.Columns[9].Width = 70;

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

                llenaCombo();
                
            
            }
        }

        protected void llenarvacio()
        {
            UltraWebGrid1.ResetColumns();
            UltraWebGrid1.DataBind();

           
            UltraWebGrid1.Columns.Add("Piezas procesadas");
            UltraWebGrid1.Columns.Add("Cantidad inventarios");
            UltraWebGrid1.Columns.Add("Piezas malas");
            UltraWebGrid1.Columns.Add("Piezas malas verdes");
            UltraWebGrid1.Columns.Add("Piezas malas quemado");
            UltraWebGrid1.Columns.Add("% calidad de 1");
            UltraWebGrid1.Columns.Add("% calidad de 2");
            UltraWebGrid1.Columns.Add("% calidad de 3");
            UltraWebGrid1.Columns.Add("% calidad de 4");
            UltraWebGrid1.Columns.Add("Activo");
            UltraWebGrid1.Columns[0].Header.Caption = "Piezas procesadas";
            UltraWebGrid1.Columns[1].Header.Caption = "Cantidad inventarios";
            UltraWebGrid1.Columns[2].Header.Caption = "Piezas malas";
            UltraWebGrid1.Columns[3].Header.Caption = "Piezas malas verdes";
            UltraWebGrid1.Columns[4].Header.Caption = "Piezas malas quemado";
            UltraWebGrid1.Columns[5].Header.Caption = "% calidad de 1";
            UltraWebGrid1.Columns[6].Header.Caption = "% calidad de 2";
            UltraWebGrid1.Columns[7].Header.Caption = "% calidad de 3";
            UltraWebGrid1.Columns[8].Header.Caption = "% calidad de 4";
            UltraWebGrid1.Columns[9].Header.Caption = "Activo";

            UltraWebGrid1.Columns[0].Width = 100;
            UltraWebGrid1.Columns[1].Width = 100;
            UltraWebGrid1.Columns[3].Width = 100;
            UltraWebGrid1.Columns[4].Width = 100;
            UltraWebGrid1.Columns[5].Width = 120;
            UltraWebGrid1.Columns[6].Width = 120;
            UltraWebGrid1.Columns[7].Width = 120;
            UltraWebGrid1.Columns[8].Width = 120;
            UltraWebGrid1.Columns[9].Width = 70;

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

        }

        protected void llenaCombo()
        {
            System.Web.UI.WebControls.ListItemCollection cboitems = new System.Web.UI.WebControls.ListItemCollection();
            System.Web.UI.WebControls.ListItem[] li = new System.Web.UI.WebControls.ListItem[cmbCalidad.Items.Count];
            cmbCalidad.Items.CopyTo(li, 0);
            cboitems.AddRange(li);
            cboitems.RemoveAt(1);

            creaCombo(cboitems, "CboCalidad1", 0, out HTMLCboCalidad1);
            creaCombo(cboitems, "CboCalidad2", 0, out HTMLCboCalidad2);
            creaCombo(cboitems, "CboCalidad3", 0, out HTMLCboCalidad3);
            creaCombo(cboitems, "CboCalidad4", 0, out HTMLCboCalidad4);




        }

        protected void BotonGuardar_click(object sender, EventArgs e)
        {
            svcSCPP.SCPPClient svc = new svcSCPP.SCPPClient();
            LAMOSA.SCPP.Server.BusinessEntity.MetasProd m = new LAMOSA.SCPP.Server.BusinessEntity.MetasProd();

            try
            {
             
                m.Planta = int.Parse(hddPlanta.Value);
                m.CantProc = int.Parse(hddcant_procesadas.Value);
                m.CantInv = int.Parse(hddcant_inventario.Value);
                m.CantDesp = int.Parse(hddcant_desperdicio.Value);
                m.CantVerde = int.Parse(hddcant_desp_verde.Value);
                m.CantQuemado = int.Parse(hddcant_desp_quemado.Value);
                m.ICalidad1 = int.Parse(hddcalidad1.Value);
                m.PorcCal1= int.Parse(hddporcentaje_cal1.Value);
                m.ICalidad2 = int.Parse(hddcalidad2.Value);
                m.PorcCal2= int.Parse(hddporcentaje_cal2.Value);
                m.ICalidad3 = int.Parse(hddcalidad3.Value);
                m.PorcCal3= int.Parse(hddporcentaje_cal3.Value);
                m.ICalidad4 = int.Parse(hddcalidad4.Value);
                m.PorcCal4= int.Parse(hddporcentaje_cal4.Value);
                m.TipoProc = int.Parse(hddtipo_procesadas.Value);
                m.TipoInv = int.Parse(hddtipo_inventario.Value);
                m.TipoDesp = int.Parse(hddtipo_desperdicio.Value);
                m.TipoVerde = int.Parse(hddtipo_desp_verde.Value);
                m.TipoQuemado = int.Parse(hddtipo_desp_quemado.Value);
                m.TipoCal1= int.Parse(hddtipo_porcent_cal1.Value);
                m.TipoCal2 = int.Parse(hddtipo_porcent_cal2.Value);
                m.TipoCal3 = int.Parse(hddtipo_porcent_cal3.Value);
                m.TipoCal4 = int.Parse(hddtipo_porcent_cal4.Value);
           

              

                svc.GuardarMetasProd(m);
                btnLlenaGrid_Click(sender, e);
                WebAsyncRefreshPanel1.DataBind();
                if (m.ExceptionMessage != null && m.ExceptionMessage.Length > 1)
                    throw new Exception(m.ExceptionMessage);
            }
            catch (Exception err)
            {

                CallBackManager.AddScriptBlock(this.Page, WebAsyncRefreshPanel1, "<script type='text/javascript'>alert('" + err.Message + "');</script>");
            }
         
        }

        protected void Bloquear(object sender, EventArgs e)
        {
            UltraWebGrid1.DisplayLayout.Bands[0].AllowUpdate = AllowUpdate.No;
            }

        protected void Desbloquear(object sender, EventArgs e)
        {
            UltraWebGrid1.DisplayLayout.Bands[0].AllowUpdate = AllowUpdate.RowTemplateOnly;
        }

        protected void btnExporta_Click(object sender, EventArgs e)
        {
           svcSCPP.SCPPClient svc = new svcSCPP.SCPPClient();

            DataSet dsReportXLS = new DataSet();
            dsReportXLS.Tables.Add();

            string[] colnames = LAMOSA.SCPP.Server.BusinessEntity.MetasProd.GetPropertyNamesArray();
            foreach (string colname in colnames)
            {
                dsReportXLS.Tables[0].Columns.Add(colname);
            }
            DropDownList cmbPlanta = ((DropDownList)Page.Master.FindControl("cmbPlanta"));
            List<Common.SolutionEntityFramework.BaseSolutionEntity> datos = svc.ObtenerMetasProd(int.Parse(cmbPlanta.SelectedValue), DateTime.Parse(FechaIni.Value.ToString()), DateTime.Parse(FechaFin.Value.ToString()));
            foreach (Common.SolutionEntityFramework.BaseSolutionEntity item in datos)
            {
                dsReportXLS.Tables[0].Rows.Add(((LAMOSA.SCPP.Server.BusinessEntity.MetasProd)item).ToObjectArray());
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
