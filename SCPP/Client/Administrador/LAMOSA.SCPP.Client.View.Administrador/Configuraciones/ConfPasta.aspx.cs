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
using LAMOSA.SCPP.Server.BusinessEntity;
using LAMOSA.SCPP.Server.BusinessEntity.Server;

namespace LAMOSA.SCPP.Client.View.Administrador.Configuraciones
{
    public partial class ConfPasta : ReporteBase
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
                    DropDownList ddlPlanta = (DropDownList)Page.Master.FindControl("cmbPlanta");
                    hddPlanta.Value = ddlPlanta.SelectedValue;

                    ddlTurno.Items.AddRange(GetItemsConSeleccione(new svcSCPP.SCPPClient().ObtenerTurnos(), "DesTurno", "CodTurno"));
                    ddlProveedor.DataSource = new Combos().ObtenerProveedores();
                    ddlProveedor.DataTextField = "Nombre";
                    ddlProveedor.DataValueField = "Codigo";
                    ddlProveedor.DataBind();

                    Planta_SelectedIndexChange(null, null);

                    FechaIni.Value = DateTime.Today;
                    FechaFin.Value = DateTime.Today;
                    llenarvacio();

                    //igtbl_reBuscaBtn.Enabled = false;
                    //LExport.Visible = false;
                    //LAddNew.Visible = false;
                    bool editar = true; // Valor original False
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
            List<Common.SolutionEntityFramework.BaseSolutionEntity> Lista = svc.ObtenerCondicionPasta(int.Parse(cmbPlanta.SelectedValue), DateTime.Parse(FechaIni.Value.ToString()), DateTime.Parse(FechaFin.Value.ToString()));
            UltraWebGrid1.DataSource = Lista;
            if (Lista.Count <= 0)
            {
                llenarvacio();
            }
            else
            {
                UltraWebGrid1.ResetColumns();
                UltraWebGrid1.DataBind();
                UltraWebGrid1.Columns.Add("ListaAreas", "ListaAreas");

                UltraWebGrid1.Columns[0].Hidden = true;
                UltraWebGrid1.Columns[1].Hidden = true;
                UltraWebGrid1.Columns[5].Hidden = true;
                UltraWebGrid1.Columns[6].Hidden = true;
                UltraWebGrid1.Columns[10].Hidden = true;

                
                for (int i = 0; i < UltraWebGrid1.Rows.Count; i++)
                {
                    int iCodigoCondicionPasta = Convert.ToInt32(UltraWebGrid1.Rows[i].Cells[0].Text);
                    List<Common.SolutionEntityFramework.BaseSolutionEntity> ListaCondicionesPasta = Lista.Where(pasta => (pasta as CondicionPasta).CodCondicionPasta == iCodigoCondicionPasta).ToList();
                    if (ListaCondicionesPasta == null || ListaCondicionesPasta.Count < 0) continue;
                    string strListArea = string.Empty;
                    foreach (Area area in (ListaCondicionesPasta[0] as CondicionPasta).ListaArea)
                        strListArea += (string.IsNullOrEmpty(strListArea) ? "" : ",") + area.CodArea;
                    UltraWebGrid1.Rows[i].Cells[UltraWebGrid1.Columns.Count - 1].Text = strListArea;
                }
                UltraWebGrid1.Columns[15].Hidden = true; //CodigoProveedor
                UltraWebGrid1.Columns[17].Hidden = true; //ExceptionMessaje
                UltraWebGrid1.Columns[18].Hidden = true; //ListaAreas
                for (int i = 0; i < UltraWebGrid1.Columns.Count; i++)
                {
                    UltraWebGrid1.Columns[i].Width = 100;
                    UltraWebGrid1.Columns[i].AllowResize = AllowSizing.Fixed;
                    UltraWebGrid1.Columns[i].Header.Style.Wrap = true;
                    UltraWebGrid1.Columns[i].CellStyle.HorizontalAlign = HorizontalAlign.Center;
                    UltraWebGrid1.Columns[i].Header.Style.HorizontalAlign = HorizontalAlign.Center;
                }
                UltraWebGrid1.Columns[4].Header.Caption = "BU";
                UltraWebGrid1.Columns[7].Header.Caption = "Autorización";
                UltraWebGrid1.Columns[9].Header.Caption = "Baroi";
                UltraWebGrid1.Columns[11].Header.Caption = "Turno";
                UltraWebGrid1.Columns[16].Header.Caption = "Proveedor";

                UltraWebGrid1.Columns[2].Format = "dd-MM-yyyy";
                UltraWebGrid1.Columns[13].Format = "HH:mm"; // PerdidaBrillo
                UltraWebGrid1.Columns[7].Type = ColumnType.CheckBox;
                UltraWebGrid1.Columns[8].Type = ColumnType.CheckBox;
            }
        }
        protected void llenarvacio()
        {
            UltraWebGrid1.ResetColumns();
            UltraWebGrid1.DataBind();
            UltraWebGrid1.Columns.Add("Fecha");
            UltraWebGrid1.Columns.Add("Densidad");
            UltraWebGrid1.Columns.Add("BU");
            UltraWebGrid1.Columns.Add("Autorización");
            UltraWebGrid1.Columns.Add("Activo");
            UltraWebGrid1.Columns.Add("Baroi");
            UltraWebGrid1.Columns.Add("Turno");


            UltraWebGrid1.Columns[0].Header.Caption = "Fecha";
            UltraWebGrid1.Columns[1].Header.Caption = "Densidad";
            UltraWebGrid1.Columns[2].Header.Caption = "BU";
            UltraWebGrid1.Columns[3].Header.Caption = "Autorización";
            UltraWebGrid1.Columns[4].Header.Caption = "Activo";
            UltraWebGrid1.Columns[5].Header.Caption = "Baroi";
            UltraWebGrid1.Columns[6].Header.Caption = "Turno";

            UltraWebGrid1.Columns[0].Header.Style.HorizontalAlign = HorizontalAlign.Center;
            UltraWebGrid1.Columns[1].Header.Style.HorizontalAlign = HorizontalAlign.Center;
            UltraWebGrid1.Columns[2].Header.Style.HorizontalAlign = HorizontalAlign.Center;
            UltraWebGrid1.Columns[3].Header.Style.HorizontalAlign = HorizontalAlign.Center;
            UltraWebGrid1.Columns[4].Header.Style.HorizontalAlign = HorizontalAlign.Center;
            UltraWebGrid1.Columns[5].Header.Style.HorizontalAlign = HorizontalAlign.Center;
            UltraWebGrid1.Columns[6].Header.Style.HorizontalAlign = HorizontalAlign.Center;


            UltraWebGrid1.Columns[1].Width = 80;
            UltraWebGrid1.Columns[1].Width = 70;
        }
        protected void BotonGuardar_click(object sender, EventArgs e)
        {
            svcSCPP.SCPPClient svc = new svcSCPP.SCPPClient();
            LAMOSA.SCPP.Server.BusinessEntity.CondicionPasta cp = new LAMOSA.SCPP.Server.BusinessEntity.CondicionPasta();
            try
            {
                int CodCondicionPasta = -1;
                int.TryParse(hddCodCondicionPasta.Value, out CodCondicionPasta);
                string sAreas = hddArea.Value.ToString();
                string[] lstCodigosArea = sAreas.Split(',');
                IList<Area> lstArea = new List<Area>();
                foreach (string sArea in lstCodigosArea) lstArea.Add(new Area(int.Parse(sArea), ""));
                cp.CodCondicionPasta = CodCondicionPasta;
                cp.CodPlanta = int.Parse(hddPlanta.Value);
                cp.Densidad = double.Parse(hddDensidad.Value);
                cp.Bu = double.Parse(hddBu.Value);
                cp.CodigoBaroi = int.Parse(hddBaroi.Value);
                cp.CodigoTurno = int.Parse(hddTurno.Value);
                cp.ListaArea = lstArea;

                cp.Deposito = int.Parse(hddDeposito.Value);
                cp.PerdidaBrillo = DateTime.ParseExact(hddPerdidaBrillo.Value.ToString(), "HH:mm",null);
                cp.Viscosidad = int.Parse(hddViscosidad.Value);
                cp.CodigoProveedor = int.Parse(hddCodigoProveedor.Value);
                svc.GuardarCondicionPasta(cp);

                btnLlenaGrid_Click(sender, e);
                //WebAsyncRefreshPanel1.DataBind();
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
            LAMOSA.SCPP.Server.BusinessEntity.CondicionPastaAutoriza cp = new LAMOSA.SCPP.Server.BusinessEntity.CondicionPastaAutoriza();

            try
            {
                int CodCondicionPasta = -1;
                int.TryParse(hddCodCondicionPasta.Value, out CodCondicionPasta);
                cp.CodCondicionPasta = CodCondicionPasta;
                cp.UsuarioAutoriza = CodUser;
                svc.AutorizaCondicionPasta(cp);
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
            DropDownList cmbPlanta = ((DropDownList)Page.Master.FindControl("cmbPlanta"));
            DataTable dtPastaExportar = svc.ObtenerCondicionPastaExportar(int.Parse(cmbPlanta.SelectedValue), DateTime.Parse(FechaIni.Value.ToString()), DateTime.Parse(FechaFin.Value.ToString()));
            dsReportXLS.Tables.Add(dtPastaExportar);
            ExportToExcel(dsReportXLS, 0, Response, nombre.Value);
        }
        protected void cambio_pagina(object sender, Infragistics.WebUI.UltraWebGrid.PageEventArgs e)
        {
            UltraWebGrid1.DisplayLayout.Pager.CurrentPageIndex = e.NewPageIndex;
            btnLlenaGrid_Click(sender, e);
        }
        protected void Planta_SelectedIndexChange(object sender, EventArgs e)
        {
            try
            {
                DropDownList ddlPlanta = (DropDownList)Page.Master.FindControl("cmbPlanta");
                hddPlanta.Value = ddlPlanta.SelectedValue;
                ddlArea.DataSource = new Combos().ObtenerArea(-1, Convert.ToInt32(ddlPlanta.SelectedValue), 1);//Se mostraran solo las areas del proceso de Vaciado
                ddlArea.DataTextField = "AreaDesc";
                ddlArea.DataValueField = "CodArea";
                ddlArea.DataBind();
            }
            catch (Exception err)
            {
                throw err;
            }
        }

        #endregion


        #endregion

    }
}
