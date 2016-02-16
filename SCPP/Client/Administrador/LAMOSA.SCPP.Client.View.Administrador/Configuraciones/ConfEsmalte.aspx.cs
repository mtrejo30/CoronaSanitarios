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
    public partial class ConfEsmalte : ReporteBase
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
                    FechaIni.ReadOnly = true;
                    FechaFin.ReadOnly = true;
                    FechaIni.Value = DateTime.Today;
                    FechaFin.Value = DateTime.Today;
                    ddlTurno.Items.AddRange(GetItemsConSeleccione(new svcSCPP.SCPPClient().ObtenerTurnos(), "DesTurno", "CodTurno"));
                    ddlColor.DataSource = new Combos().Get_ColorCbo();
                    ddlColor.DataTextField = "DesColor";
                    ddlColor.DataValueField = "CodColor";
                    ddlColor.DataBind();
                    ddlColor.Items.Insert(0, new System.Web.UI.WebControls.ListItem("Seleccione...", "0"));
                    Planta_SelectedIndexChange(null, null);
                    llenarvacio();

                    //igtbl_reBuscaBtn.Enabled = false;// Descomentarizar la linea se inhabilitaron para pruebas
                    //LExport.Visible = false;// Descomentarizar la linea se inhabilitaron para pruebas
                    //LAddNew.Visible = false;// Descomentarizar la linea se inhabilitaron para pruebas
                    bool editar = true; // Valor original false
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
            List<Common.SolutionEntityFramework.BaseSolutionEntity> Lista = svc.ObtenerCondicionEsmalte(int.Parse(cmbPlanta.SelectedValue), DateTime.Parse(FechaIni.Value.ToString()), DateTime.Parse(FechaFin.Value.ToString()));
            UltraWebGrid1.DataSource = Lista;
            if (Lista.Count <= 0)
            {
                llenarvacio();
            }
            else
            {
                UltraWebGrid1.ResetColumns();
                UltraWebGrid1.DataBind();
                UltraWebGrid1.Columns.Add("ListaMaquinas", "ListaMaquinas");

                for (int i = 0; i < UltraWebGrid1.Rows.Count; i++)
                {
                    int iCodigoCondicionEsmalte = Convert.ToInt32(UltraWebGrid1.Rows[i].Cells[0].Text);
                    List<Common.SolutionEntityFramework.BaseSolutionEntity> ListaCondicionesEsmalte = Lista.Where(esmalte => (esmalte as CondicionEsmalte).CodCondicionEsmalte == iCodigoCondicionEsmalte).ToList();
                    if (ListaCondicionesEsmalte == null || ListaCondicionesEsmalte.Count < 0) continue;
                    string strListMaquina = string.Empty;
                    foreach (Maquina maquina in (ListaCondicionesEsmalte[0] as CondicionEsmalte).ListaMaquina)
                        strListMaquina += (string.IsNullOrEmpty(strListMaquina) ? "" : ",") + maquina.CodMaquina;
                    UltraWebGrid1.Rows[i].Cells[UltraWebGrid1.Columns.Count - 1].Text = strListMaquina;
                }

                UltraWebGrid1.Columns[0].Hidden = true;
                UltraWebGrid1.Columns[1].Hidden = true;
                UltraWebGrid1.Columns[7].Hidden = true;
                UltraWebGrid1.Columns[8].Hidden = true;
                UltraWebGrid1.Columns[11].Hidden = true;//CodigoTurno
                UltraWebGrid1.Columns[13].Hidden = true;//CodigoColor
                UltraWebGrid1.Columns[UltraWebGrid1.Columns.Count - 2].Hidden = true;//ExceptionMessage
                UltraWebGrid1.Columns[UltraWebGrid1.Columns.Count - 1].Hidden = true;//Lista de Maquinas

                UltraWebGrid1.Columns[0].Width = 100;
                UltraWebGrid1.Columns[1].Width = 100;
                UltraWebGrid1.Columns[2].Width = 100;
                UltraWebGrid1.Columns[3].Width = 100;
                UltraWebGrid1.Columns[4].Width = 100;
                UltraWebGrid1.Columns[5].Width = 100;
                UltraWebGrid1.Columns[6].Width = 100;
                UltraWebGrid1.Columns[7].Width = 100;
                UltraWebGrid1.Columns[8].Width = 100;
                UltraWebGrid1.Columns[9].Width = 100;
                UltraWebGrid1.Columns[10].Width = 100;
                UltraWebGrid1.Columns[14].Width = 120;//DescripcionColor

                UltraWebGrid1.Columns[3].Header.Caption = "Tiempo espejo";
                UltraWebGrid1.Columns[9].Header.Caption = "Autorización";
                UltraWebGrid1.Columns[12].Header.Caption = "Turno";
                UltraWebGrid1.Columns[14].Header.Caption = "Color";

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


                UltraWebGrid1.Columns[2].Format = "dd-MM-yyyy";
                UltraWebGrid1.Columns[9].Type = ColumnType.CheckBox;
                UltraWebGrid1.Columns[10].Type = ColumnType.CheckBox;


            }
        }
        protected void llenarvacio()
        {
            UltraWebGrid1.ResetColumns();
            UltraWebGrid1.DataBind();
            UltraWebGrid1.Columns.Add("Fecha");
            UltraWebGrid1.Columns.Add("Tiempo espejo");
            UltraWebGrid1.Columns.Add("Viscosidad");
            UltraWebGrid1.Columns.Add("Densidad");
            UltraWebGrid1.Columns.Add("Espesor");
            UltraWebGrid1.Columns.Add("Autorización");
            UltraWebGrid1.Columns.Add("Activo");


            UltraWebGrid1.Columns[0].Header.Caption = "Fecha";
            UltraWebGrid1.Columns[1].Header.Caption = "Tiempo espejo";
            UltraWebGrid1.Columns[2].Header.Caption = "Viscosidad";
            UltraWebGrid1.Columns[3].Header.Caption = "Densidad";
            UltraWebGrid1.Columns[4].Header.Caption = "Espesor";
            UltraWebGrid1.Columns[5].Header.Caption = "Autorización";
            UltraWebGrid1.Columns[6].Header.Caption = "Activo";

            UltraWebGrid1.Columns[0].Header.Style.HorizontalAlign = HorizontalAlign.Center;
            UltraWebGrid1.Columns[1].Header.Style.HorizontalAlign = HorizontalAlign.Center;
            UltraWebGrid1.Columns[2].Header.Style.HorizontalAlign = HorizontalAlign.Center;
            UltraWebGrid1.Columns[3].Header.Style.HorizontalAlign = HorizontalAlign.Center;
            UltraWebGrid1.Columns[4].Header.Style.HorizontalAlign = HorizontalAlign.Center;
            UltraWebGrid1.Columns[5].Header.Style.HorizontalAlign = HorizontalAlign.Center;
            UltraWebGrid1.Columns[6].Header.Style.HorizontalAlign = HorizontalAlign.Center;


            UltraWebGrid1.Columns[1].Width = 100;

        }
        protected void BotonGuardar_click(object sender, EventArgs e)
        {
            svcSCPP.SCPPClient svc = new svcSCPP.SCPPClient();
            LAMOSA.SCPP.Server.BusinessEntity.CondicionEsmalte ce = new LAMOSA.SCPP.Server.BusinessEntity.CondicionEsmalte();

            try
            {
                int CodCondicionEsmalte = -1;
                int.TryParse(hddCodCondicionEsmalte.Value, out CodCondicionEsmalte);
                ce.CodCondicionEsmalte = CodCondicionEsmalte;
                ce.CodPlanta = int.Parse(hddPlanta.Value);
                ce.TiempoEspejo = double.Parse(hddTiempoEspejo.Value);
                ce.Viscosidad = double.Parse(hddViscosidad.Value);
                ce.Densidad = double.Parse(hddDensidad.Value);
                ce.Espesor = double.Parse(hddEspesor.Value);

                ce.CodigoTurno = int.Parse(hddTurno.Value);
                ce.CodigoColor = int.Parse(hddColor.Value);
                string sMaquinas = hddMaquinas.Value.ToString();
                string[] lstCodigosMaquina = sMaquinas.Split(',');
                IList<Maquina> lstMaquina = new List<Maquina>();
                foreach (string sMaquina in lstCodigosMaquina) lstMaquina.Add(new Maquina(int.Parse(sMaquina), "", "", -1, "", -1, "", -1, ""));
                ce.ListaMaquina = lstMaquina;
                ce.NumeroLote = hddNumeroLote.Value;
                ce.TamanoLote = double.Parse(hddTamanoLote.Value);
                ce.CantidadGoma = double.Parse(hddCantidadGoma.Value);
                ce.Molino = int.Parse(hddMolino.Value);
                ce.Granulometria = double.Parse(hddGranulometria.Value);

                svc.GuardarCondicionEsmalte(ce);
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
            LAMOSA.SCPP.Server.BusinessEntity.CondicionEsmalteAutoriza ce = new LAMOSA.SCPP.Server.BusinessEntity.CondicionEsmalteAutoriza();

            try
            {
                int CodCondicionEsmalte = -1;
                int.TryParse(hddCodCondicionEsmalte.Value, out CodCondicionEsmalte);
                ce.CodCondicionEsmalte = CodCondicionEsmalte;
                ce.UsuarioAutoriza = CodUser;
                svc.AutorizaCondicionEsmalte(ce);
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
            DataTable dt = svc.ObtenerCondicionEsmalteExportar(int.Parse(cmbPlanta.SelectedValue), DateTime.Parse(FechaIni.Value.ToString()), DateTime.Parse(FechaFin.Value.ToString()));
            dsReportXLS.Tables.Add(dt);
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
                ddlMaquina.DataSource = new Combos().ObtenerMaquinaCbo(-1, -1, Convert.ToInt32(ddlPlanta.SelectedValue), 4); //Se manda un 4 porque es el codigo que corresponde a Esmaltado.
                ddlMaquina.DataTextField = "desMaquina";
                ddlMaquina.DataValueField = "codMaquina";
                ddlMaquina.DataBind();
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
