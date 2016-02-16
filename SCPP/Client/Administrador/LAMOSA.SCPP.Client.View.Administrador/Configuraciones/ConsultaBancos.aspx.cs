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
    public partial class ConfiguraBancos : ReporteBase
    {
        public DataTable workTable;
        public int ban;

        protected void Page_Load(object sender, EventArgs e)
        {
            AutWD.Checked = bool.Parse(hddAut.Value);

            if (!AutWD.Checked)
            {
                BlockControls(true, true);
            }
            if (!Page.IsPostBack)
            {
                Usuario user = (Usuario)Session["UserLogged"];
                if (user != null)
                {
                    llenaCombo();
                    llenarvacio();

                    //btnBuscar.Enabled = false;
                    //LExport.Visible = false;
                    //LAddNew.Visible = false;
                    //bool editar = false;
                    foreach (ScreenPermission sp in new Actions().GetActionBySreen(user.CodRol, Request.Url.LocalPath))
                    {
                        switch (sp.ActionCode)
                        {
                            case 1: //Buscar

                                btnBuscar.Enabled = true;
                                break;
                            case 2: //Exportar
                                LExport.Visible = true;
                                break;
                            case 3: //Nuevo
                                LAddNew.Visible = true;
                                break;
                            case 4: //Editar
                                //editar = true;
                                UltraWebGrid2.DisplayLayout.AllowUpdateDefault = AllowUpdate.RowTemplateOnly;
                                break;
                        }

                    }

                    //if (editar == false)
                    //{
                    //    UltraWebGrid2.DisplayLayout.AllowUpdateDefault = AllowUpdate.No;
                    //}
                }

            }
        }
        private void llenaCombo()
        {
            TxtPlanta1.Text = ((DropDownList)Page.Master.FindControl("cmbplanta")).SelectedItem.Text;
            DropDownList cmbPlanta = ((DropDownList)Page.Master.FindControl("cmbPlanta"));
            svcSCPP.SCPPClient svc = new svcSCPP.SCPPClient();
            cmbCentroTrabajo.Items.Clear();
            cmbCentroTrabajo.Items.AddRange(GetItemsConSeleccioneTodos(svc.ObtenerCentroTrabajo(int.Parse(cmbPlanta.SelectedValue), 1), "DesCentroTrabajo", "CodCentroTrabajo"));

            CentroTrabWD.Items.AddRange(GetItemsConSeleccione(svc.ObtenerCentroTrabajo(int.Parse(cmbPlanta.SelectedValue), 1), "DesCentroTrabajo", "CodCentroTrabajo"));

            TipoArticuloWD.Items.AddRange(GetItemsConSeleccione(svc.ObtenerTiposArticuloCbo(), "DesTipoArticulo", "CodTipoArticulo"));
   

            cmbMaquina.Items.Clear();
           cmbMaquina.Items.Insert(0, new System.Web.UI.WebControls.ListItem("Seleccione...", "0"));
           cmbMaquina.Items.Insert(1, new System.Web.UI.WebControls.ListItem("Todos", "-1"));

            MaquinaWD.Items.Clear();
            MaquinaWD.Items.Insert(0, new System.Web.UI.WebControls.ListItem("Seleccione...", "0"));

            MoldeWD.Items.Clear();
            MoldeWD.Items.Insert(0, new System.Web.UI.WebControls.ListItem("Seleccione...", "0"));
            //   CentroTrabWD_SelectedIndexChanged(null, null);
         //   TipoArticuloWD_SelectedIndexChanged(null, null);
            //            cmbCentro_SelectedIndexChanged(null, null);*/
        }
        protected void cmbCentro_SelectedIndexChanged(object sender, EventArgs e)
        {
            svcSCPP.SCPPClient svc = new svcSCPP.SCPPClient();
            cmbMaquina.Items.Clear();
            cmbMaquina.Items.AddRange(GetItemsConSeleccioneTodos(svc.ObtenerMaquinaCbo(-1, int.Parse(cmbCentroTrabajo.SelectedValue)), "DesMaquina", "codMaquina"));
        }
        protected void CentroTrabWD_SelectedIndexChanged(object sender, EventArgs e)
        {
            svcSCPP.SCPPClient svc = new svcSCPP.SCPPClient();
            MaquinaWD.Items.Clear();
            MaquinaWD.Items.AddRange(GetItemsConSeleccione(svc.ObtenerMaquinaCbo(-1, int.Parse(CentroTrabWD.SelectedValue)), "DesMaquina", "codMaquina"));
           
           
        }

        protected void bandera(object sender, EventArgs e)
        {
            

            int valor = int.Parse(hddBan.Value);
            if (valor > 0)
            {
                btnAutorizaWD.Enabled = false;
                btnEliminarBancoWD.Enabled = false;
                btnDesactivarConfigBancoWD.Enabled = false;
                btnAutorizaWD.Visible = false;
                btnEliminarBancoWD.Visible = false;
                btnDesactivarConfigBancoWD.Visible = false;
            }
            else
            {
                btnAutorizaWD.Enabled = true;
                btnEliminarBancoWD.Enabled = true;
                btnDesactivarConfigBancoWD.Enabled = true;
                btnAutorizaWD.Visible = true;
                btnEliminarBancoWD.Visible = true;
                btnDesactivarConfigBancoWD.Visible = true;
            }
        }
        protected void TipoArticuloWD_SelectedIndexChanged(object sender, EventArgs e)
        {
            svcSCPP.SCPPClient svc = new svcSCPP.SCPPClient();
            LAMOSA.SCPP.Server.BusinessEntity.ArticuloPars ap = new LAMOSA.SCPP.Server.BusinessEntity.ArticuloPars(int.Parse(TipoArticuloWD.SelectedValue), -1);
            MoldeWD.Items.Clear();
            MoldeWD.Items.AddRange(GetItemsConSeleccione(svc.ObtenerMoldesCbo(ap), "DesMolde", "CodMolde"));
     
        }
        protected void MoldeWD_SelectedIndexChanged(object sender, EventArgs e)
        {
            int numImpresiones = new Actions().ObtenerNumImpresionesMolde(Convert.ToInt32(MoldeWD.SelectedValue));
            txtNumImpresiones.Text = numImpresiones.ToString();
            //CantidadMoldesWD.Text = "";
            //VaciadasDiaWD.Text = "";
            //txtNumImpresiones.Text = "";

            int valor = -1;
            try { valor = int.Parse(hddConfiguracionBanco.Value); }
            catch { }
            if (valor < 0)
            {
                CentroTrabWD.Enabled = true;
                MaquinaWD.Enabled = true;
                TipoArticuloWD.Enabled = true;

            }
            else
            {
                CentroTrabWD.Enabled = false;
                MaquinaWD.Enabled = false;
                TipoArticuloWD.Enabled = false;

            }

        }
        protected void btnBuscar_click(object sender, EventArgs e)
        {
            try
            {
                DropDownList cmbPlanta = ((DropDownList)Page.Master.FindControl("cmbPlanta"));
                svcSCPP.SCPPClient svc = new svcSCPP.SCPPClient();
                int planta = int.Parse(cmbPlanta.SelectedValue);
                int centroTrab = int.Parse(cmbCentroTrabajo.SelectedValue);
                int maquina = int.Parse(cmbMaquina.SelectedValue);
                int active = Convert.ToInt32(ddlActivos.SelectedValue);
                List<Common.SolutionEntityFramework.BaseSolutionEntity> Lista = svc.ObtenerConfiguracionBanco(planta, centroTrab, maquina, active);
                UltraWebGrid2.DataSource = Lista;
                if (Lista.Count <= 0)
                {
                    llenarvacio();
                }
                else
                {
                    UltraWebGrid2.ResetColumns();
                    UltraWebGrid2.DataBind();
                    UltraWebGrid2.DisplayLayout.ScrollBar = ScrollBar.Auto;

                    UltraWebGrid2.Columns[0].Header.Caption = "Centro de trabajo";
                    UltraWebGrid2.Columns[1].Header.Caption = "Banco";
                    UltraWebGrid2.Columns[2].Header.Caption = "Límite de vaciadas";
                    UltraWebGrid2.Columns[3].Header.Caption = "Vaciadas acumuladas";
                    UltraWebGrid2.Columns[4].Header.Caption = "Fecha inicio";
                    UltraWebGrid2.Columns[5].Header.Caption = "Fecha fin";
                    UltraWebGrid2.Columns[6].Header.Caption = "Autorizado";
                    UltraWebGrid2.Columns[7].Header.Caption = "Autoriza";
                    UltraWebGrid2.Columns[8].Header.Caption = "Activo";
                    UltraWebGrid2.Columns[9].Header.Caption = "CodCT";
                    UltraWebGrid2.Columns[10].Header.Caption = "CodMaquina";
                    UltraWebGrid2.Columns[11].Header.Caption = "CodUsuarioAutoriza";
                    UltraWebGrid2.Columns[12].Header.Caption = "CodUsuarioAlta";
                    UltraWebGrid2.Columns[13].Header.Caption = "CodConfigBanco";
                    UltraWebGrid2.Columns[14].Header.Caption = "ExceptionMessage";

                    UltraWebGrid2.Columns[0].Header.Style.Wrap = true;
                    UltraWebGrid2.Columns[2].Header.Style.Wrap = true;
                    UltraWebGrid2.Columns[3].Header.Style.Wrap = true;


                    UltraWebGrid2.Columns[6].Width = 80;
                    UltraWebGrid2.Columns[7].Width = 130;
                    UltraWebGrid2.Columns[8].Width = 70;

                    UltraWebGrid2.Columns[0].Width = 150;
                    UltraWebGrid2.Columns[1].Width = 150;

                    UltraWebGrid2.Columns[4].Width = 90;
                    UltraWebGrid2.Columns[5].Width = 90;


                    UltraWebGrid2.Columns[0].AllowResize = AllowSizing.Fixed;
                    UltraWebGrid2.Columns[1].AllowResize = AllowSizing.Fixed;
                    UltraWebGrid2.Columns[2].AllowResize = AllowSizing.Fixed;
                    UltraWebGrid2.Columns[3].AllowResize = AllowSizing.Fixed;
                    UltraWebGrid2.Columns[4].AllowResize = AllowSizing.Fixed;
                    UltraWebGrid2.Columns[5].AllowResize = AllowSizing.Fixed;
                    UltraWebGrid2.Columns[6].AllowResize = AllowSizing.Fixed;
                    UltraWebGrid2.Columns[7].AllowResize = AllowSizing.Fixed;
                    UltraWebGrid2.Columns[8].AllowResize = AllowSizing.Fixed;

                    UltraWebGrid2.Columns[0].CellStyle.HorizontalAlign = HorizontalAlign.Left;
                    UltraWebGrid2.Columns[1].CellStyle.HorizontalAlign = HorizontalAlign.Left;
                    UltraWebGrid2.Columns[2].CellStyle.HorizontalAlign = HorizontalAlign.Center;
                    UltraWebGrid2.Columns[3].CellStyle.HorizontalAlign = HorizontalAlign.Center;
                    UltraWebGrid2.Columns[4].CellStyle.HorizontalAlign = HorizontalAlign.Center;
                    UltraWebGrid2.Columns[5].CellStyle.HorizontalAlign = HorizontalAlign.Center;
                    UltraWebGrid2.Columns[6].CellStyle.HorizontalAlign = HorizontalAlign.Center;
                    UltraWebGrid2.Columns[7].CellStyle.HorizontalAlign = HorizontalAlign.Left;
                    UltraWebGrid2.Columns[8].CellStyle.HorizontalAlign = HorizontalAlign.Center;

                    UltraWebGrid2.Columns[0].Header.Style.HorizontalAlign = HorizontalAlign.Center;
                    UltraWebGrid2.Columns[1].Header.Style.HorizontalAlign = HorizontalAlign.Center;
                    UltraWebGrid2.Columns[2].Header.Style.HorizontalAlign = HorizontalAlign.Center;
                    UltraWebGrid2.Columns[3].Header.Style.HorizontalAlign = HorizontalAlign.Center;
                    UltraWebGrid2.Columns[4].Header.Style.HorizontalAlign = HorizontalAlign.Center;
                    UltraWebGrid2.Columns[5].Header.Style.HorizontalAlign = HorizontalAlign.Center;
                    UltraWebGrid2.Columns[6].Header.Style.HorizontalAlign = HorizontalAlign.Center;
                    UltraWebGrid2.Columns[7].Header.Style.HorizontalAlign = HorizontalAlign.Center;
                    UltraWebGrid2.Columns[8].Header.Style.HorizontalAlign = HorizontalAlign.Center;

                    UltraWebGrid2.Columns[4].Format = "dd-MM-yyyy";
                    UltraWebGrid2.Columns[5].Format = "dd-MM-yyyy";

                    UltraWebGrid2.Columns[2].Hidden = true;
                    UltraWebGrid2.Columns[3].Hidden = true;
                    UltraWebGrid2.Columns[9].Hidden = true;
                    UltraWebGrid2.Columns[10].Hidden = true;
                    UltraWebGrid2.Columns[11].Hidden = true;
                    UltraWebGrid2.Columns[12].Hidden = true;
                    UltraWebGrid2.Columns[13].Hidden = true;
                    UltraWebGrid2.Columns[14].Hidden = true;
                    UltraWebGrid2.Width = 800;

                }
            }
            catch (Exception ex)
            {
                CallBackManager.AddScriptBlock(this.Page, WebAsyncRefreshPanel1, "<script type='text/javascript'>alert('No se encontraron datos');</script>");
            }
        }
        protected void btnEliminarWD_click(object sendder, EventArgs e)
        {
            try
            {
                svcSCPP.SCPPClient svc = new svcSCPP.SCPPClient();
                LAMOSA.SCPP.Server.BusinessEntity.ConfigBancos cbr = new LAMOSA.SCPP.Server.BusinessEntity.ConfigBancos();
                cbr.CodConfigBanco = int.Parse(hddConfiguracionBanco.Value);
                cbr = svc.EliminaConfigBanco(cbr);
                if (cbr.ExceptionMessage.Length > 1)
                {
                    UltraWebGrid1.DataSource = svc.ObtenerConfiguracionBancoDetalle(int.Parse(hddConfiguracionBanco.Value));
                    UltraWebGrid1.DataBind();

                    ConfigTablaWD();

                    throw new Exception("El banco se eliminó con éxito");
                }

            }
            catch (Exception err)
            {
                CallBackManager.AddScriptBlock(this.Page, WebAsyncRefreshPanel2, "<script type='text/javascript'>alert('" + err.Message + "');</script>");
            }
        }
        protected void btnDesactivarConfigBancoWD_click(object sendder, EventArgs e)
        {
            Boolean combos = true;
            if (int.Parse(hddConfiguracionBanco.Value) > 0)
            {
                combos = false;
            }
            AutWD.Checked = bool.Parse(hddAut.Value);
            BlockControls(!AutWD.Checked, combos);
            string msg = "";
            try
            {
                msg = new Actions().DesactivarConfigBanco(Convert.ToInt32(hddConfiguracionBanco.Value));
            }
            catch (Exception err)
            {
                msg = err.Message;
            }
            CallBackManager.AddScriptBlock(this.Page, WebAsyncRefreshPanel2, "<script type='text/javascript'>alert('" + msg + "');</script>");
        }
        protected void btnAutorizaWD_click(object sendder, EventArgs e)
        {
            try
            {
                String msg = "";
                Usuario user = (Usuario)Session["UserLogged"];
                if (user != null)
                {
                    svcSCPP.SCPPClient svc = new svcSCPP.SCPPClient();
                    LAMOSA.SCPP.Server.BusinessEntity.ConfigBancos cbr = new LAMOSA.SCPP.Server.BusinessEntity.ConfigBancos();
                    cbr.CodConfigBanco = int.Parse(hddConfiguracionBanco.Value);
                    cbr.CodUsuarioAutoriza = user.CodUsuario;
                    svc.AutorizaConfigBanco(cbr);
                    msg = "Banco autorizado";
                    BlockControls(false, false);
                }
                else
                    msg = "No hay usuario en session, favor de autentificarse primero";
                CallBackManager.AddScriptBlock(this.Page, WebAsyncRefreshPanel2, "<script type='text/javascript'>alert('" + msg + "');</script>");
            }
            catch (Exception err)
            {
                CallBackManager.AddScriptBlock(this.Page, WebAsyncRefreshPanel2, "<script type='text/javascript'>alert('" + err.Message + "');</script>");
            }

            LlenaGridWD();
        }
        protected void btnAgregarWD_click(object sender, EventArgs e)
        {
            try
            {
                try
                {
                    Boolean combos = false;
                    //if (int.Parse(hddConfiguracionBanco.Value) > 0)
                    //{
                    //    combos = false;
                    //}
                    AutWD.Checked = bool.Parse(hddAut.Value);
                    BlockControls(!AutWD.Checked, combos);
                }
                catch { }

                Usuario user = (Usuario)Session["UserLogged"];

                svcSCPP.SCPPClient svc = new svcSCPP.SCPPClient();
                LAMOSA.SCPP.Server.BusinessEntity.ConfigBancoResgistro cbr = new LAMOSA.SCPP.Server.BusinessEntity.ConfigBancoResgistro();
                cbr.Activo = true;
                cbr.CantMoldes = int.Parse(CantidadMoldesWD.Text);
                cbr.CodConfigBanco = !hddConfiguracionBanco.Value.Equals("") ? int.Parse(hddConfiguracionBanco.Value) : -1;
                cbr.CodMaquina = int.Parse(MaquinaWD.SelectedValue);
                cbr.CodMolde = int.Parse(MoldeWD.SelectedValue);
                cbr.CodUsuarioAlta = user.CodUsuario;
                cbr.Limitevaciadas = int.Parse(LimVaciadasWD.Text);
                cbr.Vaciadasdia = int.Parse(VaciadasDiaWD.Text);
                cbr.NumeroImpresiones = int.Parse(txtNumImpresiones.Text);

                String msg = !hddConfiguracionBanco.Value.Equals("") ? "Modificacion" : "Creacion";
                int res = svc.GuardarConfigBanco(cbr);
                if (res > 0)
                {
                    hddConfiguracionBanco.Value = res.ToString();
                    BlockControls(true, false);
                }
                else
                    throw new Exception("No se pudo realizar la operacion");
                UltraWebGrid1.DataSource = svc.ObtenerConfiguracionBancoDetalle(res);
                UltraWebGrid1.DataBind();



                ConfigTablaWD();
                CallBackManager.AddScriptBlock(this.Page, WebAsyncRefreshPanel2, "<script type='text/javascript'>alert('La " + msg + " se ha completado con exito');</script>");
            }
            catch (Exception err)
            {
                CallBackManager.AddScriptBlock(this.Page, WebAsyncRefreshPanel2, "<script type='text/javascript'>alert('" + err.Message.Replace("'", "\"") + "');</script>");
            }

            CantidadMoldesWD.Text = "";
            VaciadasDiaWD.Text = "";
            txtNumImpresiones.Text = "";

        }
        protected void btnExporta_Click(object sender, EventArgs e)
        {
            //Metodo para Generar el Reporte
            if (ddlSeleccion.SelectedItem.Text.ToString() == "MS Excel (XLS)")
            {
                Workbook Reporte = new Workbook();
                uwgConsBancos.Export(UltraWebGrid2, Reporte, 0, 0);
            }
            else
            {
            }
        }
        protected void ConsultaBancos_InicializeLayout(object sender, Infragistics.WebUI.UltraWebGrid.LayoutEventArgs e)
        {
            e.Layout.Bands[0].CellClickAction = CellClickAction.Edit;

            ValueList articulo = UltraWebGrid1.Columns[1].ValueList;
            ValueList modelo = UltraWebGrid1.Columns[4].ValueList;
            ValueList Impresiones = UltraWebGrid1.Columns[5].ValueList;
            ValueList Capacidad = UltraWebGrid1.Columns[6].ValueList;
            ValueList rango = UltraWebGrid1.Columns[7].ValueList;
            ValueList real = UltraWebGrid1.Columns[8].ValueList;
        }
        protected void btnLlenaGridWD_click(object sender, EventArgs e)
        {

            ban = 0;
            hddBan.Value = ban.ToString();
             Boolean combos = true;
            if (int.Parse(hddConfiguracionBanco.Value) > 0)
            {
                combos = false;
            }
            AutWD.Checked = bool.Parse(hddAut.Value);
            BlockControls(!AutWD.Checked, combos);
            LlenaGridWD();
        }
        protected void botonEnabled(object sender, EventArgs e)
        {


            CentroTrabWD.Enabled = true;
            MaquinaWD.Enabled = true;

            if (int.Parse(hddConfiguracionBanco.Value) > 0)
            {

                CentroTrabWD.Enabled = false;
                MaquinaWD.Enabled = false;
            }

            btnAgregarWD.Enabled = true;
            btnAutorizaWD.Enabled = true;
            btnEliminarBancoWD.Enabled = true;

            AutWD.Checked = bool.Parse(hddAut.Value);

            if (AutWD.Checked)
            {

                btnAgregarWD.Enabled = false;
                btnAutorizaWD.Enabled = false;
                btnEliminarBancoWD.Enabled = false;
            }


        }
        private void LlenaGridWD()
        {


            svcSCPP.SCPPClient svc = new svcSCPP.SCPPClient();
            LimVaciadasWD.Text = hddVacAcu.Value;
            AutWD.Checked = bool.Parse(hddAut.Value);
            int res = int.Parse(hddConfiguracionBanco.Value);
            UltraWebGrid1.DataSource = svc.ObtenerConfiguracionBancoDetalle(res);
            UltraWebGrid1.DataBind();

            if (UltraWebGrid1.Rows.Count > 0)
            {
                bandera(null,null);
                ConfigTablaWD();
            }
            else
            {
                CallBackManager.AddScriptBlock(this.Page, WebAsyncRefreshPanel2, "<script type='text/javascript'>alert('El banco no tiene modelos configurados');</script>");
            }
        }

        protected void limpiar_Cbs(object sender, EventArgs e)
        {
            /*svcSCPP.SCPPClient svc = new svcSCPP.SCPPClient();
            TxtPlanta1.Text = ((DropDownList)Page.Master.FindControl("cmbplanta")).SelectedItem.Text;
            DropDownList cmbPlanta = ((DropDownList)Page.Master.FindControl("cmbPlanta"));

            btnAgregarWD.Enabled = true;
            btnAutorizaWD.Enabled = false;
            btnEliminarBancoWD.Enabled = false;
            btnDesactivarConfigBancoWD.Enabled = false;
            btnAutorizaWD.Visible = false;
            btnEliminarBancoWD.Visible = false;
            btnDesactivarConfigBancoWD.Visible = false;

            CentroTrabWD.Items.Clear();
            TipoArticuloWD.Items.Clear();
            CentroTrabWD.Items.AddRange(GetItemsConSeleccione(svc.ObtenerCentroTrabajo(int.Parse(cmbPlanta.SelectedValue), 1), "DesCentroTrabajo", "CodCentroTrabajo"));

            TipoArticuloWD.Items.AddRange(GetItemsConSeleccione(svc.ObtenerTiposArticuloCbo(), "DesTipoArticulo", "CodTipoArticulo"));
     
            */
           
        }
        private void ConfigTablaWD()
        {
             
            var ct = hddCT.Value;
            if (ct == "")
            ct= CentroTrabWD.SelectedValue;
            CentroTrabWD.SelectedIndex = CentroTrabWD.Items.IndexOf(CentroTrabWD.Items.FindByValue(ct));
            bandera(null, null);
            var bc = hddBanco.Value;
            if (bc == "")
                bc = MaquinaWD.SelectedValue;
            MaquinaWD.SelectedIndex = MaquinaWD.Items.IndexOf(MaquinaWD.Items.FindByValue(bc));
            String ta = UltraWebGrid1.Rows[0].Cells[1].Value.ToString();
            try
            {
                TipoArticuloWD.SelectedIndex = TipoArticuloWD.Items.IndexOf(TipoArticuloWD.Items.FindByValue(ta));
            }
            catch { }
            TipoArticuloWD_SelectedIndexChanged(null, null);

           
            UltraWebGrid1.Columns[0].Header.Caption = "CodConfigBanco";
            UltraWebGrid1.Columns[1].Header.Caption = "CodTipoArticulo";
            UltraWebGrid1.Columns[2].Header.Caption = "Tipo artículo";
            UltraWebGrid1.Columns[3].Header.Caption = "CodArticulo";
            UltraWebGrid1.Columns[4].Header.Caption = "Clave artículo";
            UltraWebGrid1.Columns[5].Header.Caption = "Artículo";
            UltraWebGrid1.Columns[6].Header.Caption = "CodMolde";
            UltraWebGrid1.Columns[7].Header.Caption = "Molde";
            UltraWebGrid1.Columns[8].Header.Caption = "Impresiones";
            UltraWebGrid1.Columns[9].Header.Caption = "Cantidad moldes";
            UltraWebGrid1.Columns[10].Header.Caption = "Posiciones";
            UltraWebGrid1.Columns[11].Header.Caption = "Limite vaciadas";
            UltraWebGrid1.Columns[12].Header.Caption = "Vaciadas diarias";
            UltraWebGrid1.Columns[13].Header.Caption = "Vaciadas acumuladas";
            UltraWebGrid1.Columns[14].Header.Caption = "Núm. de Impresiones";
            UltraWebGrid1.Columns[15].Header.Caption = "ExceptionMessage";

            UltraWebGrid1.Columns[2].Header.Style.Wrap = true;
            UltraWebGrid1.Columns[4].Header.Style.Wrap = true;
            UltraWebGrid1.Columns[5].Header.Style.Wrap = true;
            UltraWebGrid1.Columns[7].Header.Style.Wrap = true;
            UltraWebGrid1.Columns[8].Header.Style.Wrap = true;
            UltraWebGrid1.Columns[9].Header.Style.Wrap = true;
            UltraWebGrid1.Columns[10].Header.Style.Wrap = true;
            UltraWebGrid1.Columns[11].Header.Style.Wrap = true;
            UltraWebGrid1.Columns[12].Header.Style.Wrap = true;
            UltraWebGrid1.Columns[13].Header.Style.Wrap = true;
            UltraWebGrid1.Columns[14].Header.Style.Wrap = true;

            UltraWebGrid1.Columns[2].Width = 70;
            UltraWebGrid1.Columns[4].Width = 70;
            UltraWebGrid1.Columns[5].Width = 200;
            UltraWebGrid1.Columns[7].Width = 50;
            UltraWebGrid1.Columns[8].Width = 100;
            UltraWebGrid1.Columns[9].Width = 70;
            UltraWebGrid1.Columns[10].Width = 80;
            UltraWebGrid1.Columns[11].Width = 70;
            UltraWebGrid1.Columns[12].Width = 70;
            UltraWebGrid1.Columns[13].Width = 90;
            UltraWebGrid1.Columns[14].Width = 90;

            UltraWebGrid1.Columns[0].Hidden = true;
            UltraWebGrid1.Columns[1].Hidden = true;
            UltraWebGrid1.Columns[3].Hidden = true;
            UltraWebGrid1.Columns[6].Hidden = true;
            UltraWebGrid1.Columns[11].Hidden = true;//Limite vaciadas
            UltraWebGrid1.Columns[13].Hidden = true;//Vaciadas acumuladas
            UltraWebGrid1.Columns[15].Hidden = true;

            UltraWebGrid1.Columns[2].AllowResize = AllowSizing.Fixed;
            UltraWebGrid1.Columns[4].AllowResize = AllowSizing.Fixed;
            UltraWebGrid1.Columns[5].AllowResize = AllowSizing.Fixed;
            UltraWebGrid1.Columns[7].AllowResize = AllowSizing.Fixed;
            UltraWebGrid1.Columns[8].AllowResize = AllowSizing.Fixed;
            UltraWebGrid1.Columns[9].AllowResize = AllowSizing.Fixed;
            UltraWebGrid1.Columns[10].AllowResize = AllowSizing.Fixed;
            UltraWebGrid1.Columns[11].AllowResize = AllowSizing.Fixed;
            UltraWebGrid1.Columns[12].AllowResize = AllowSizing.Fixed;
            UltraWebGrid1.Columns[13].AllowResize = AllowSizing.Fixed;
            UltraWebGrid1.Columns[14].AllowResize = AllowSizing.Fixed;

            UltraWebGrid1.Columns[2].CellStyle.HorizontalAlign = HorizontalAlign.Left;
            UltraWebGrid1.Columns[4].CellStyle.HorizontalAlign = HorizontalAlign.Center;
            UltraWebGrid1.Columns[5].CellStyle.HorizontalAlign = HorizontalAlign.Left;
            UltraWebGrid1.Columns[7].CellStyle.HorizontalAlign = HorizontalAlign.Left;
            UltraWebGrid1.Columns[8].CellStyle.HorizontalAlign = HorizontalAlign.Center;
            UltraWebGrid1.Columns[9].CellStyle.HorizontalAlign = HorizontalAlign.Center;
            UltraWebGrid1.Columns[10].CellStyle.HorizontalAlign = HorizontalAlign.Center;
            UltraWebGrid1.Columns[11].CellStyle.HorizontalAlign = HorizontalAlign.Center;
            UltraWebGrid1.Columns[12].CellStyle.HorizontalAlign = HorizontalAlign.Center;
            UltraWebGrid1.Columns[13].CellStyle.HorizontalAlign = HorizontalAlign.Center;
            UltraWebGrid1.Columns[14].CellStyle.HorizontalAlign = HorizontalAlign.Center;

            UltraWebGrid1.Columns[2].Header.Style.HorizontalAlign = HorizontalAlign.Center;
            UltraWebGrid1.Columns[4].Header.Style.HorizontalAlign = HorizontalAlign.Center;
            UltraWebGrid1.Columns[5].Header.Style.HorizontalAlign = HorizontalAlign.Center;
            UltraWebGrid1.Columns[7].Header.Style.HorizontalAlign = HorizontalAlign.Center;
            UltraWebGrid1.Columns[8].Header.Style.HorizontalAlign = HorizontalAlign.Center;
            UltraWebGrid1.Columns[9].Header.Style.HorizontalAlign = HorizontalAlign.Center;
            UltraWebGrid1.Columns[10].Header.Style.HorizontalAlign = HorizontalAlign.Center;
            UltraWebGrid1.Columns[11].Header.Style.HorizontalAlign = HorizontalAlign.Center;
            UltraWebGrid1.Columns[12].Header.Style.HorizontalAlign = HorizontalAlign.Center;
            UltraWebGrid1.Columns[13].Header.Style.HorizontalAlign = HorizontalAlign.Center;
            UltraWebGrid1.Columns[14].Header.Style.HorizontalAlign = HorizontalAlign.Center;

        }
        protected void llenarvacio()
        {
            UltraWebGrid2.ResetColumns();
            UltraWebGrid2.DataBind();

            UltraWebGrid2.DisplayLayout.ScrollBar = ScrollBar.Never;
            UltraWebGrid2.Columns.Add("Centro trabajo");
            UltraWebGrid2.Columns.Add("Banco");
           
            UltraWebGrid2.Columns.Add("Fecha inicio");
            UltraWebGrid2.Columns.Add("Fecha fin");
            UltraWebGrid2.Columns.Add("Autorizado");
            UltraWebGrid2.Columns.Add("Autoriza");
            UltraWebGrid2.Columns.Add("Activo");


            UltraWebGrid2.Columns[0].Header.Caption = "Centro de trabajo";
            UltraWebGrid2.Columns[1].Header.Caption = "Banco";
          
            UltraWebGrid2.Columns[2].Header.Caption = "Fecha inicio";
            UltraWebGrid2.Columns[3].Header.Caption = "Fecha fin";
            UltraWebGrid2.Columns[4].Header.Caption = "Autorizado";
            UltraWebGrid2.Columns[5].Header.Caption = "Autoriza";
            UltraWebGrid2.Columns[6].Header.Caption = "Activo";

            UltraWebGrid2.Columns[0].Header.Style.Wrap = true;
     




            UltraWebGrid2.Columns[0].Header.Style.HorizontalAlign = HorizontalAlign.Center;
            UltraWebGrid2.Columns[1].Header.Style.HorizontalAlign = HorizontalAlign.Center;
            UltraWebGrid2.Columns[2].Header.Style.HorizontalAlign = HorizontalAlign.Center;
            UltraWebGrid2.Columns[3].Header.Style.HorizontalAlign = HorizontalAlign.Center;
            UltraWebGrid2.Columns[4].Header.Style.HorizontalAlign = HorizontalAlign.Center;
            UltraWebGrid2.Columns[5].Header.Style.HorizontalAlign = HorizontalAlign.Center;
            UltraWebGrid2.Columns[6].Header.Style.HorizontalAlign = HorizontalAlign.Center;
      


            UltraWebGrid2.Columns[0].Width = 100;
            UltraWebGrid2.Columns[1].Width = 65;
            UltraWebGrid2.Columns[4].Width = 75;
            UltraWebGrid2.Columns[5].Width = 90;
            UltraWebGrid2.Columns[6].Width = 70;
        



        }
        protected void cambio_pagina(object sender, Infragistics.WebUI.UltraWebGrid.PageEventArgs e)
        {
            UltraWebGrid1.DisplayLayout.Pager.CurrentPageIndex = e.NewPageIndex;
            LlenaGridWD();
        }
        protected void cambio_pagina2(object sender, Infragistics.WebUI.UltraWebGrid.PageEventArgs e)
        {
            UltraWebGrid2.DisplayLayout.Pager.CurrentPageIndex = e.NewPageIndex;
            btnBuscar_click(sender, e);
        }
        private void BlockControls(Boolean buttons, Boolean combos)
        {
            //Controles del detalle
            //DropDownLists
            CentroTrabWD.Enabled = combos;
            TipoArticuloWD.Enabled = combos;
            MaquinaWD.Enabled = combos;
            LimVaciadasWD.Enabled = combos;
            //Buttons
            btnAgregarWD.Enabled = buttons;
            btnAutorizaWD.Enabled = buttons;
            btnEliminarBancoWD.Enabled = buttons;
            //El boton de desactivar solo se va a mostrar cuando la configuracion de Banco este autorizada


        }

        protected void Nuevo(object sender, EventArgs e)
        {
            svcSCPP.SCPPClient svc = new svcSCPP.SCPPClient();
            TxtPlanta1.Text = ((DropDownList)Page.Master.FindControl("cmbplanta")).SelectedItem.Text;
            DropDownList cmbPlanta = ((DropDownList)Page.Master.FindControl("cmbPlanta"));

            try
            {
                ban = 1;
                hddBan.Value = ban.ToString();
                LimVaciadasWD.Enabled = true;
                LimVaciadasWD.Text = "";
                CantidadMoldesWD.Text = "";
                VaciadasDiaWD.Text = "";
                txtNumImpresiones.Text = "";
                btnAgregarWD.Enabled = true;
                btnAutorizaWD.Enabled = false;
                btnEliminarBancoWD.Enabled = false;
                btnDesactivarConfigBancoWD.Enabled = false;
                btnAutorizaWD.Visible = false;
                btnEliminarBancoWD.Visible = false;
                btnDesactivarConfigBancoWD.Visible = false;
                CentroTrabWD.Enabled = true;
                MaquinaWD.Enabled = true;
                TipoArticuloWD.Enabled = true;

                CentroTrabWD.Items.Clear();
                TipoArticuloWD.Items.Clear();
                MaquinaWD.Items.Clear();
                MoldeWD.Items.Clear();
                CentroTrabWD.Items.AddRange(GetItemsConSeleccione(svc.ObtenerCentroTrabajo(int.Parse(cmbPlanta.SelectedValue), 1), "DesCentroTrabajo", "CodCentroTrabajo"));

                TipoArticuloWD.Items.AddRange(GetItemsConSeleccione(svc.ObtenerTiposArticuloCbo(), "DesTipoArticulo", "CodTipoArticulo"));

                MaquinaWD.Items.Insert(0, new System.Web.UI.WebControls.ListItem("Seleccione...", "0"));

                MoldeWD.Items.Insert(0, new System.Web.UI.WebControls.ListItem("Seleccione...", "0"));
            }
            catch (Exception err)
            {

                throw err;
            }
        }

        protected void Grid(object sender, EventArgs e)
        {

            try
            {

                ban = 0;
                hddBan.Value = ban.ToString();
                LimVaciadasWD.Enabled = false;

                CantidadMoldesWD.Text = "";
                VaciadasDiaWD.Text = "";
                txtNumImpresiones.Text = "";
                btnAgregarWD.Enabled = true;
                btnAutorizaWD.Enabled = true;
                btnEliminarBancoWD.Enabled = true;
                btnDesactivarConfigBancoWD.Enabled = true;
                btnAutorizaWD.Visible = true;
                btnEliminarBancoWD.Visible = true;
                btnDesactivarConfigBancoWD.Visible = true;
                CentroTrabWD.Enabled = false;
                MaquinaWD.Enabled = false;
                TipoArticuloWD.Enabled = false;
            }
            catch (Exception err)
            {

                throw err;
            }
        }


    }
}
