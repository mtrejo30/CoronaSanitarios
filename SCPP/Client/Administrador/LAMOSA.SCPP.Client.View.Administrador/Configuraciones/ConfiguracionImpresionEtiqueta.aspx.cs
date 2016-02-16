using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Infragistics.WebUI.Shared;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using Infragistics.Web.UI.ListControls;
using Infragistics.WebUI.UltraWebGrid;
using System.IO;
using System.ComponentModel;
using Infragistics.Shared;
using Infragistics.Excel;
using System.Data;
using LAMOSA.SCPP.Server.BusinessComponent;
using LAMOSA.SCPP.Server.BusinessEntity.Server;
using LAMOSA.SCPP.Server.BusinessEntity;

namespace LAMOSA.SCPP.Client.View.Administrador.Configuraciones
{
    public partial class ConfiguracionImpresionEtiqueta : System.Web.UI.Page //ReporteBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                Usuario user = (Usuario)Session["UserLogged"];
                if (user != null)
                {
                    //bool editar = false;
                    LlenaCombosIniciales(user);
                    foreach (ScreenPermission sp in new Actions().GetActionBySreen(user.CodRol, Request.Url.LocalPath))
                    {
                        switch (sp.ActionCode)
                        {
                            case 4:
                                //editar = true;
                                break;
                        }
                    }
                    //if (editar == false)
                    //{
                    //    this.webNumImpresiones.Enabled = false;
                    //    this.ddlEtiqueta.Enabled = false;
                    //    this.btnActualizar.Enabled = false;
                    //    this.btnBuscar.Enabled = false;
                    //}
                }
                else
                {
                    this.webNumImpresiones.Enabled = false;
                    this.ddlEtiqueta.Enabled = false;
                    this.btnActualizar.Enabled = false;
                    this.btnBuscar.Enabled = false;
                }
            }
        }
        private void LlenaCombosIniciales(Usuario usuario)
        {
            SCPP.Server.BusinessComponent.SCPP businessComponent = new LAMOSA.SCPP.Server.BusinessComponent.SCPP();
            try
            {
                DataTable dtEtiqueta = businessComponent.ObtenerEtiqueta(string.Empty);
                //Etiqueta
                ddlEtiqueta.DataSource = dtEtiqueta;
                ddlEtiqueta.DataTextField = "Clave";
                ddlEtiqueta.DataValueField = "Clave";
                ddlEtiqueta.DataBind();
                ddlEtiqueta.Items.Insert(0, new System.Web.UI.WebControls.ListItem("Todos", "0"));
            }
            catch (Exception ex) { throw new Exception(ex.Message); }
            finally 
            {
                businessComponent = null;
            }
        }

        protected void btnBuscar_Click(object sender, EventArgs e)
        {
            SCPP.Server.BusinessComponent.SCPP businessComponent = new LAMOSA.SCPP.Server.BusinessComponent.SCPP();
            try
            {
                DataTable dtImpresionEtiqueta = ObtenerConfiguracionEtiqueta(((this.ddlEtiqueta.SelectedValue == null || Convert.ToString(ddlEtiqueta.SelectedValue) == "0") ? string.Empty : Convert.ToString(ddlEtiqueta.SelectedValue)));
                uwgConfiguracionImpresionEtiquetas.DataSource = dtImpresionEtiqueta;
                uwgConfiguracionImpresionEtiquetas.DataBind();
            }
            catch (Exception ex) { throw new Exception(ex.Message); }
            finally
            {
                businessComponent = null;
            }
        }

        private DataTable ObtenerEtiqueta(string sClave)
        {
            SCPP.Server.BusinessComponent.SCPP businessComponent = new LAMOSA.SCPP.Server.BusinessComponent.SCPP();
            DataTable dtImpresionEtiqueta = null;
            try
            {
                dtImpresionEtiqueta = businessComponent.ObtenerEtiqueta(((string.IsNullOrEmpty(sClave) || sClave == "0") ? string.Empty : sClave));
                return dtImpresionEtiqueta;
            }
            catch (Exception ex) { throw new Exception(ex.Message); }
            finally
            {
                businessComponent = null;
                if (dtImpresionEtiqueta != null) dtImpresionEtiqueta.Dispose();
            }
        }
        private DataTable ObtenerConfiguracionEtiqueta(string sClave)
        {
            SCPP.Server.BusinessComponent.SCPP businessComponent = new LAMOSA.SCPP.Server.BusinessComponent.SCPP();
            DataTable dtImpresionEtiqueta = null;
            try
            {
                dtImpresionEtiqueta = businessComponent.ObtenerConfiguracionImpresionEtiqueta(((string.IsNullOrEmpty(sClave) || sClave == "0") ? string.Empty : sClave));
                return dtImpresionEtiqueta;
            }
            catch (Exception ex) { throw new Exception(ex.Message); }
            finally
            {
                businessComponent = null;
                if (dtImpresionEtiqueta != null) dtImpresionEtiqueta.Dispose();
            }
        }
        protected void btnActualizar_Click(object sender, EventArgs e)
        {
            SCPP.Server.BusinessComponent.SCPP businessComponent = new LAMOSA.SCPP.Server.BusinessComponent.SCPP();
            bool bEstatus = false;
            try
            {
                if (ValidarCaptura())
                {
                    bEstatus = businessComponent.ActualizarConfiguracionImpresionEtiqueta(Convert.ToString(ddlEtiqueta.SelectedValue), Convert.ToInt32(this.webNumImpresiones.Value));
                    btnBuscar_Click(sender, e);
                }
            }
            catch (Exception ex) { throw new Exception(ex.Message); }
            finally
            {
                businessComponent = null;
            }
        }
        private bool ValidarCaptura()
        { 
            string sMensaje = "Favor de capturar el campo ", sCaptura;
            //Etiqueta
            sCaptura = (this.ddlEtiqueta.SelectedValue == null || Convert.ToString(ddlEtiqueta.SelectedValue) == "0") ? string.Empty : Convert.ToString(ddlEtiqueta.SelectedValue);
            if (string.IsNullOrEmpty(sCaptura))
            {
                sMensaje += "Etiqueta.";
                return false;
            }
            sCaptura = (string.IsNullOrEmpty(this.webNumImpresiones.Text)) ? string.Empty : this.webNumImpresiones.Text;
            if (string.IsNullOrEmpty(sCaptura))
            {
                sMensaje += "Impresiones.";
                return false;
            }
            return true;
        }
        protected void uwgConfiguracionImpresionEtiquetas_SelectedRowsChange(object sender, SelectedRowsEventArgs e)
        {
            if (e.SelectedRows == null) return;
            if (e.SelectedRows.Count < 1) return;
            if (e.SelectedRows[0] == null) return;
            UltraGridRow row = e.SelectedRows[0];
            string sClave = string.Empty, sImpresion = string.Empty;
            if (row.Cells.Exists("Modelo"))
                sClave = row.Cells.FromKey("Modelo").Value.ToString();
            if (row.Cells.Exists("Calidad"))
                sClave += "-" + row.Cells.FromKey("Calidad").Value.ToString();
            if (row.Cells.Exists("Impresiones"))
                sImpresion = row.Cells.FromKey("Impresiones").Value.ToString();
            InicializarCombo(this.ddlEtiqueta, sClave);
            InicializarTextBox(this.webNumImpresiones, sImpresion);
        }

        private void InicializarCombo(DropDownList cmb, object valor)
        {
            if (cmb.ClientID != this.ddlEtiqueta.ClientID) return;
            for (int i = 0; i < cmb.Items.Count; i++)
			{
                if (Convert.ToString(cmb.Items[i].Value) == Convert.ToString(valor))
                {
                    cmb.SelectedIndex = i;
                    break;
                }
			}
        }
        private void InicializarTextBox(Infragistics.WebUI.WebDataInput.WebNumericEdit tb, object valor)
        {
            if (tb.ClientID != this.webNumImpresiones.ClientID) return;
            tb.Value = Convert.ToInt32(valor);
        }
        protected void ddlEtiqueta_SelectIndexChanged(object sender, EventArgs e)
        {
            if(sender == null) return;
            DropDownList cmb = sender  as DropDownList;
            if (cmb.SelectedIndex < 0) return;
            if (cmb.SelectedIndex == 0)
            { 
                this.webNumImpresiones.Value = 0;
                return;
            }
            DataTable dtEtiqueta = ObtenerEtiqueta(cmb.SelectedItem.ToString());
            this.webNumImpresiones.Value = Convert.ToInt32(dtEtiqueta.Rows[0]["NumeroImpresiones"]);
            dtEtiqueta = null;
        }

        protected void uwgConfiguracionImpresionEtiquetas_InitializeRow(object sender, RowEventArgs e)
        {
            for (int i = (e.Row.Cells.Count - 1); i >= 3; i--)
                e.Row.Cells.RemoveAt(i);
            for (int i = ((sender as UltraWebGrid).Columns.Count - 1); i >= 3; i--)
                (sender as UltraWebGrid).Columns.RemoveAt(i);
        }
    }
}
