using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using Infragistics.WebUI.Shared;
using System.Web.UI;
using System.Drawing;
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
using BE = LAMOSA.SCPP.Server.BusinessEntity;
using SE = Common.SolutionEntityFramework;


namespace LAMOSA.SCPP.Client.View.Administrador.Reportes
{
    public partial class InventarioProceso : ReporteBase
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
                    txtFechaDesde.Value = DateTime.Today;
                    txtFechaHasta.Value = DateTime.Today;
                    llenarvacio();
                    DropDownList cmbPlanta = ((DropDownList)Page.Master.FindControl("cmbPlanta"));
                    svcSCPP.SCPPClient svc = new svcSCPP.SCPPClient();
                    cmbAlmacen.Items.AddRange(GetItemsConSeleccioneTodos(svc.ObtenerAlmacenCbo(), "Descripcion", "ClaveAlmacen"));
                    cmbAlmacen.DataBind();
                    cmbPlantaSel.Items.Clear();
                    cmbPlantaSel.Items.AddRange(GetItemsConSeleccioneTodos(svc.ObtenerPlanta(int.Parse(cmbAlmacen.SelectedValue)), "DescripcionPlanta", "ClavePlanta"));
                    //cmbPlantaSel.Items.Insert(0, new System.Web.UI.WebControls.ListItem("Seleccione...", "0"));
                  //  cmbPlantaSel.Items.Insert(1, new System.Web.UI.WebControls.ListItem("Todos", "-1"));
                    cmbTipoArticulo.Items.AddRange(GetItemsConSeleccioneTodos(svc.ObtenerTiposArticuloCbo(), "DesTipoArticulo", "CodTipoArticulo"));

                    cmbArticulo.Items.Clear();
                    //cmbArticulo.Items.Insert(0, new System.Web.UI.WebControls.ListItem("Seleccione...", "0"));
                   // cmbArticulo.Items.Insert(1, new System.Web.UI.WebControls.ListItem("Todos", "-1"));
                    cmbArticulo.Items.AddRange(GetItemsConSeleccioneTodos(svc.ObtenerArticulosCbo(int.Parse(cmbTipoArticulo.SelectedValue)), "DesArticulo", "CodArticulo"));
                    cmbProceso.Items.Clear();
                    //cmbProceso.Items.Insert(0, new System.Web.UI.WebControls.ListItem("Seleccione...", "0"));
                   // cmbProceso.Items.Insert(1, new System.Web.UI.WebControls.ListItem("Todos", "-1"));
                    cmbProceso.Items.AddRange(GetItemsConSeleccioneTodos(svc.ObtenerProcesoCbo(int.Parse(cmbPlantaSel.SelectedValue)), "DescripcionProceso", "ClaveProceso"));
                    CmbAgrupa.Items.Insert(0, new System.Web.UI.WebControls.ListItem("Detalle", "1"));
                    CmbAgrupa.Items.Insert(1, new System.Web.UI.WebControls.ListItem("Artículo", "2"));
                    CmbAgrupa.Items.Insert(2, new System.Web.UI.WebControls.ListItem("Proceso", "3"));
                    CmbAgrupa.Items.FindByValue("1").Selected = true;

                    LExport.Visible = false;
                    GenerarButton.Enabled = true;
                    bool editar = false;
                    foreach (ScreenPermission sp in new Actions().GetActionBySreen(user.CodRol, Request.Url.LocalPath))
                    {
                        switch (sp.ActionCode)
                        {
                            case 1: //Buscar
                                GenerarButton.Enabled = true;
                                break;
                            case 2: //Exportar
                                LExport.Visible = true;
                                break;
                            case 3: //Nuevo

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
        private void AgregaItemsSeleccioneTodos()
        {
            //cmbAlmacen.Items.Insert(0, new System.Web.UI.WebControls.ListItem("Seleccione...", "0"));
            cmbAlmacen.Items.Insert(1, new System.Web.UI.WebControls.ListItem("Todos", "-1"));
            //cmbTipoArticulo.Items.Insert(0, new System.Web.UI.WebControls.ListItem("Seleccione...", "0"));
            cmbTipoArticulo.Items.Insert(1, new System.Web.UI.WebControls.ListItem("Todos", "-1"));
        }

        protected void GenerarButton_click(object sender, EventArgs e)
        {
            svcSCPP.SCPPClient svc = new svcSCPP.SCPPClient();
            int almacen = int.Parse(cmbAlmacen.SelectedValue);
            int planta = int.Parse(cmbPlantaSel.SelectedValue);
            int proceso = int.Parse(cmbProceso.SelectedValue);
            int tipoart = int.Parse(cmbTipoArticulo.SelectedValue);
            int artticulo = int.Parse(cmbArticulo.SelectedValue);
            DateTime fechaini = txtFechaDesde.Value != null ? DateTime.Parse(txtFechaDesde.Value.ToString()) : new DateTime(1800, 1, 1);
            DateTime fechafin = txtFechaDesde.Value != null ? DateTime.Parse(txtFechaHasta.Value.ToString()) : DateTime.MaxValue;
            List<Common.SolutionEntityFramework.BaseSolutionEntity> Lista = svc.ObtenerInventarioEnProceso(almacen, planta, proceso, tipoart, artticulo, fechaini, fechafin, Convert.ToInt32(this.CmbAgrupa.SelectedItem.Value));
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
                UltraWebGrid1.Columns[0].Hidden = true;
                UltraWebGrid1.Columns[1].Hidden = true;
                UltraWebGrid1.Columns[2].Header.Caption = "Planta";
                UltraWebGrid1.Columns[3].Hidden = true;
                UltraWebGrid1.Columns[4].Header.Caption = "Proceso";
                UltraWebGrid1.Columns[5].Hidden = true;
                UltraWebGrid1.Columns[6].Hidden = true;
                UltraWebGrid1.Columns[7].Header.Caption = "Tipo de artículo";
                UltraWebGrid1.Columns[8].Hidden = true;
                UltraWebGrid1.Columns[9].Hidden = true;
                UltraWebGrid1.Columns[10].Header.Caption = "Código de barras";
                UltraWebGrid1.Columns[11].Header.Caption = "Artículo";
                UltraWebGrid1.Columns[12].Hidden = true;
                UltraWebGrid1.Columns[13].Hidden = true;
                UltraWebGrid1.Columns[14].Hidden = true;
                UltraWebGrid1.Columns[15].Header.Caption = "Color";
                UltraWebGrid1.Columns[16].Hidden = true;
                UltraWebGrid1.Columns[17].Header.Caption = "Calidad";
                UltraWebGrid1.Columns[18].Header.Caption = "Cantidad";
                UltraWebGrid1.Columns[19].Hidden = true;
               

                UltraWebGrid1.Columns[18].CellStyle.HorizontalAlign = HorizontalAlign.Center;

                UltraWebGrid1.Columns[2].Width = 120;
                UltraWebGrid1.Columns[4].Width = 100;
                UltraWebGrid1.Columns[7].Width = 130;
    
                UltraWebGrid1.Columns[10].Width = 150;
                UltraWebGrid1.Columns[11].Width = 200;
                UltraWebGrid1.Columns[15].Width = 100;
                UltraWebGrid1.Columns[17].Width = 100;
                UltraWebGrid1.Columns[18].Width = 80;
                UltraWebGrid1.Width = 800;

                UltraGridBand Band = UltraWebGrid1.Bands[0];
                Band.ColFootersVisible = ShowMarginInfo.Yes;
                Band.FooterStyle.BackColor = System.Drawing.Color.Gray;
                Band.Columns[0].Footer.Caption = "Total";
                Band.Columns[0].Footer.Style.HorizontalAlign = HorizontalAlign.Left;
                Band.Columns[18].Footer.Total = Infragistics.WebUI.UltraWebGrid.SummaryInfo.Sum;




            }
        }
        protected void btnExporta_Click(object sender, EventArgs e)
        {
            svcSCPP.SCPPClient svc = new svcSCPP.SCPPClient();

            DataSet dsReportXLS = new DataSet();
            dsReportXLS.Tables.Add();

            string[] colnames = LAMOSA.SCPP.Server.BusinessEntity.InventarioEnProceso.GetPropertyNamesArray();
            foreach (string colname in colnames)
            {
                dsReportXLS.Tables[0].Columns.Add(colname);
            }
            int almacen = int.Parse(cmbAlmacen.SelectedValue);
            int planta = int.Parse(cmbPlantaSel.SelectedValue);
            int proceso = int.Parse(cmbProceso.SelectedValue);
            int tipoart = int.Parse(cmbTipoArticulo.SelectedValue);
            int artticulo = int.Parse(cmbArticulo.SelectedValue);
            DateTime fechaini = txtFechaDesde.Value != null ? DateTime.Parse(txtFechaDesde.Value.ToString()) : new DateTime(1800, 1, 1);
            DateTime fechafin = txtFechaDesde.Value != null ? DateTime.Parse(txtFechaHasta.Value.ToString()) : DateTime.MaxValue;
            List<Common.SolutionEntityFramework.BaseSolutionEntity> datos = svc.ObtenerInventarioEnProceso(almacen, planta, proceso, tipoart, artticulo, fechaini, fechafin, Convert.ToInt32(this.CmbAgrupa.SelectedItem.Value));
            foreach (Common.SolutionEntityFramework.BaseSolutionEntity item in datos)
            {
                dsReportXLS.Tables[0].Rows.Add(((LAMOSA.SCPP.Server.BusinessEntity.InventarioEnProceso)item).ToObjectArray());
            }
            ExportToExcel(dsReportXLS, 0, Response, nombre.Value);
        }
        protected void cmbAlmacen_SelectedIndexChanged(object sender, EventArgs e)
        {
            svcSCPP.SCPPClient svc = new svcSCPP.SCPPClient();
            cmbPlantaSel.Items.Clear();
            cmbPlantaSel.Items.AddRange(GetItemsConSeleccioneTodos(svc.ObtenerPlanta(int.Parse(cmbAlmacen.SelectedValue)), "DescripcionPlanta", "ClavePlanta"));
      
      
        }
        protected void cmbPlantaSel_SelectedIndexChanged(object sender, EventArgs e)
        {

            svcSCPP.SCPPClient svc = new svcSCPP.SCPPClient();
            cmbProceso.Items.Clear();
            cmbProceso.Items.AddRange(GetItemsConSeleccioneTodos(svc.ObtenerProcesoCbo(int.Parse(cmbPlantaSel.SelectedValue)), "DescripcionProceso", "ClaveProceso"));

        }
        protected void cmbTipoArticulo_SelectedIndexChanged(object sender, EventArgs e)
        {
            svcSCPP.SCPPClient svc = new svcSCPP.SCPPClient();
            cmbArticulo.Items.Clear();
            cmbArticulo.Items.AddRange(GetItemsConSeleccioneTodos(svc.ObtenerArticulosCbo(int.Parse(cmbTipoArticulo.SelectedValue)), "DesArticulo", "CodArticulo"));

        }

        protected void llenarvacio()
        {
            UltraWebGrid1.ResetColumns();
            UltraWebGrid1.DataBind();

            int cuenta = UltraWebGrid1.Columns.Count;
            UltraWebGrid1.Columns.Add("Proceso");
            UltraWebGrid1.Columns.Add("Tipo de artículo");
            UltraWebGrid1.Columns.Add("Artículo");
            UltraWebGrid1.Columns.Add("Color");
            UltraWebGrid1.Columns.Add("Calidad");
            UltraWebGrid1.Columns.Add("Cantidad");

            UltraWebGrid1.Columns[0].Header.Caption = "Proceso";
            UltraWebGrid1.Columns[1].Header.Caption = "Tipo de artículo";
            UltraWebGrid1.Columns[2].Header.Caption = "Articulo";
            UltraWebGrid1.Columns[3].Header.Caption = "Color";
            UltraWebGrid1.Columns[4].Header.Caption = "Calidad";
            UltraWebGrid1.Columns[5].Header.Caption = "Cantidad";
            UltraWebGrid1.Columns[0].Width = 130;
            UltraWebGrid1.Columns[1].Width = 130;
            UltraWebGrid1.Columns[2].Width = 130;

            UltraWebGrid1.DisplayLayout.ScrollBar = ScrollBar.Never;


        }

        protected void cambio_pagina(object sender, Infragistics.WebUI.UltraWebGrid.PageEventArgs e)
        {
            UltraWebGrid1.DisplayLayout.Pager.CurrentPageIndex = e.NewPageIndex;
            GenerarButton_click(sender, e);
        }
        #endregion
        #endregion

    }
}
