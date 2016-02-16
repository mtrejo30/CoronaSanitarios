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

using LAMOSA.SCPP.Server.BusinessEntity.Server;
using LAMOSA.SCPP.Server.BusinessEntity;

namespace LAMOSA.SCPP.Client.View.Administrador.Etiquetas
{
    public partial class PiezasConResidencia : ReporteBase
    {
        protected string comilla = "'";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                Usuario user = (Usuario)Session["UserLogged"];
                if (user != null)
                {
                    LlenaCombosIniciales(user);
                    ddlAlertaResidencia_SelectedIndexChanged(null, null);
                    btnBaja.Visible = false;
                    btnReestablecer.Visible = false;
                    bool editar = false;
                    foreach (ScreenPermission sp in new Actions().GetActionBySreen(user.CodRol, Request.Url.LocalPath))
                    {
                        switch (sp.ActionCode)
                        {
                            case 4: //Editar
                                editar = true;
                                uwgPiezasConResidencia.DisplayLayout.AllowUpdateDefault = AllowUpdate.RowTemplateOnly;
                                break;
                        }
                    }
                    if (editar == false)
                    {
                        //uwgPiezasConResidencia.DisplayLayout.AllowUpdateDefault = AllowUpdate.No;
                    }
                }
            }
        }
        private void LlenaCombosIniciales(Usuario usuario)
        {
            try
            {
                DataTable dtAlerta = new svcSCPP.SCPPClient().ObtenerAlertaPlanta(-1, 1, -1, -1, usuario.CodEmpleado);//Se pone iCodigoTipoAlerta = 1 porque solo se buscan las de Residencia
                //Alerta
                ddlAlertaResidencia.DataSource = dtAlerta;
                ddlAlertaResidencia.DataTextField = "mensaje";
                ddlAlertaResidencia.DataValueField = "Codigo";
                ddlAlertaResidencia.DataBind();

                //Plantas por el Rol del Usuario
                ddlPlanta.DataSource = new Combos().Get_Planta_RolCbo(usuario.CodRol);
                ddlPlanta.DataTextField = "descripcionPlanta";
                ddlPlanta.DataValueField = "ClavePlanta";
                ddlPlanta.DataBind();
                ddlPlanta.Items.Insert(0, new System.Web.UI.WebControls.ListItem("Todos", "-1"));

                //Procesos
                ddlProceso.DataSource = new Combos().Get_ProcesoCbo();
                ddlProceso.DataTextField = "DescripcionProceso";
                ddlProceso.DataValueField = "ClaveProceso";
                ddlProceso.DataBind();
                ddlProceso.Items.Insert(0, new System.Web.UI.WebControls.ListItem("Todos", "-1"));

                //TipoArticulo
                ddlTipoArticulo.DataSource = new Combos().Get_TipoArticuloCbo();
                ddlTipoArticulo.DataTextField = "DesTipoArticulo";
                ddlTipoArticulo.DataValueField = "CodTipoArticulo";
                ddlTipoArticulo.DataBind();
                ddlTipoArticulo.Items.Insert(0, new System.Web.UI.WebControls.ListItem("Todos", "-1"));

                ddlTipoArticulo_SelectedIndexChanged(null, null); // Esto es Para Inicializar el Combo de Articulos

                LlenarMaquina_SelectedIndexChanged(null, null);// Esto es Para Inicializar el Combo de Maquinas

                //Color
                ddlColor.DataSource = new Combos().Get_ColorCbo();
                ddlColor.DataTextField = "DesColor";
                ddlColor.DataValueField = "CodColor";
                ddlColor.DataBind();
                ddlColor.Items.Insert(0, new System.Web.UI.WebControls.ListItem("Todos", "0"));

                //Turno
                ddlTurno.DataSource = new Combos().Get_TurnoCbo();
                ddlTurno.DataTextField = "Descripcion";
                ddlTurno.DataValueField = "Clave_turno";
                ddlTurno.DataBind();
                ddlTurno.Items.Insert(0, new System.Web.UI.WebControls.ListItem("Todos", "0"));

                ddlProceso.DataSource = new Combos().Get_ProcesoCbo();
            }
            catch (Exception ex) { throw new Exception(ex.Message); }
        }
        protected void ddlTipoArticulo_SelectedIndexChanged(object sender, EventArgs e)
        {
            ddlArticulo.Items.Clear();
            int cod_ta = Convert.ToInt32(ddlTipoArticulo.SelectedValue);
            if (cod_ta > 0)
            {
                ddlArticulo.DataSource = new Combos().ObtenerArticulo(cod_ta);
                ddlArticulo.DataTextField = "Descripcion";
                ddlArticulo.DataValueField = "Codigo";
                ddlArticulo.DataBind();
            }
            ddlArticulo.Items.Insert(0, new System.Web.UI.WebControls.ListItem("Todos", "-1"));
        }
        protected void btnLlenaGridEmp_Click(object sender, EventArgs e)
        {
            svcSCPP.SCPPClient svc = new svcSCPP.SCPPClient();
            int NumEmpleado = -1;
            int.TryParse(NumEmpleadoWD.Text, out NumEmpleado);
            GridView1.DataSource = svc.ObtenerEmpleadoBusqueda(NumEmpleado, NomEmpleadoWD.Text, -1);
            GridView1.DataBind();
        }
        protected void LlenarMaquina_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                ddlMaquina.Items.Clear();
                int? iCodigoProceso = null;
                if (Convert.ToInt32(ddlProceso.SelectedValue) > 0) iCodigoProceso = Convert.ToInt32(ddlProceso.SelectedValue);
                int? iCodigoPlanta = null;
                if (Convert.ToInt32(ddlPlanta.SelectedValue) > 0) iCodigoPlanta = Convert.ToInt32(ddlPlanta.SelectedValue);
                if (iCodigoPlanta.HasValue || iCodigoProceso.HasValue)
                {
                    ddlMaquina.DataSource = new Combos().ObtenerMaquinaCbo(-1, -1, iCodigoPlanta, iCodigoProceso);
                    ddlMaquina.DataTextField = "desMaquina";
                    ddlMaquina.DataValueField = "codMaquina";
                    ddlMaquina.DataBind();
                }
                ddlMaquina.Items.Insert(0, new System.Web.UI.WebControls.ListItem("Todos", "-1"));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        protected void ddlAlertaResidencia_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                int iCodigoAlerta = Convert.ToInt32(ddlAlertaResidencia.SelectedValue);
                /***************Variables para filtros***************/
                int iCodigoPlanta = 0;
                int iCodigoProceso = 0;
                int iCodigoTipoArticulo = 0;
                int iCodigoArticulo = 0;
                int iCodigoMaquina = 0;
                int iCodigoColor = 0;
                string sClaveEmpleado = string.Empty; ;
                int iCodigoTurno = 0;
                int iDiasResidencia = 0;
                /****************************************************/
                if (iCodigoAlerta > 1)
                {
                    DataTable dtAlerta = new svcSCPP.SCPPClient().ObtenerAlerta(iCodigoAlerta, 1, -1, -1);
                    if (dtAlerta != null && dtAlerta.Rows.Count > 0)
                    {
                        int.TryParse(dtAlerta.Rows[0]["CodPlanta"].ToString(), out iCodigoPlanta);
                        int.TryParse(dtAlerta.Rows[0]["CodProceso"].ToString(), out iCodigoProceso);
                        int.TryParse(dtAlerta.Rows[0]["CodTipoArticulo"].ToString(), out iCodigoTipoArticulo);
                        int.TryParse(dtAlerta.Rows[0]["CodModelo"].ToString(), out iCodigoArticulo);
                        int.TryParse(dtAlerta.Rows[0]["CodMaquina"].ToString(), out iCodigoMaquina);
                        int.TryParse(dtAlerta.Rows[0]["CodColor"].ToString(), out iCodigoColor);
                        int.TryParse(dtAlerta.Rows[0]["CodTurno"].ToString(), out iCodigoTurno);
                        sClaveEmpleado = dtAlerta.Rows[0]["CodOperador"].ToString();
                        int.TryParse(dtAlerta.Rows[0]["LimiteMaximo"].ToString(), out iDiasResidencia);
                    }
                }
                ddlPlanta.SelectedIndex = ddlPlanta.Items.IndexOf(ddlPlanta.Items.FindByValue(iCodigoPlanta.ToString()));
                ddlProceso.SelectedIndex = ddlProceso.Items.IndexOf(ddlProceso.Items.FindByValue(iCodigoProceso.ToString()));
                ddlTipoArticulo.SelectedIndex = ddlTipoArticulo.Items.IndexOf(ddlTipoArticulo.Items.FindByValue(iCodigoTipoArticulo.ToString()));

                if (iCodigoPlanta > 0 || iCodigoProceso > 0) LlenarMaquina_SelectedIndexChanged(null, null);
                if (iCodigoTipoArticulo > 0) ddlTipoArticulo_SelectedIndexChanged(null, null);

                ddlArticulo.SelectedIndex = ddlArticulo.Items.IndexOf(ddlArticulo.Items.FindByValue(iCodigoArticulo.ToString()));
                ddlMaquina.SelectedIndex = ddlMaquina.Items.IndexOf(ddlMaquina.Items.FindByValue(iCodigoMaquina.ToString()));
                ddlColor.SelectedIndex = ddlColor.Items.IndexOf(ddlColor.Items.FindByValue(iCodigoColor.ToString()));
                ddlTurno.SelectedIndex = ddlTurno.Items.IndexOf(ddlTurno.Items.FindByValue(iCodigoTurno.ToString()));
                txtEmpleado.Text = sClaveEmpleado;
                txtDiasResidencia.Text = iDiasResidencia > 0 ? iDiasResidencia.ToString() : string.Empty;

                ddlPlanta.Enabled = iCodigoPlanta < 1;
                ddlProceso.Enabled = iCodigoProceso < 1;
                ddlTipoArticulo.Enabled = iCodigoTipoArticulo < 1;
                ddlArticulo.Enabled = iCodigoArticulo < 1;
                ddlMaquina.Enabled = iCodigoMaquina < 1;
                ddlColor.Enabled = iCodigoColor < 1;
                ddlTurno.Enabled = iCodigoTurno < 1;
                txtEmpleado.Enabled = string.IsNullOrEmpty(sClaveEmpleado);
                txtEmpleado.Enabled = string.IsNullOrEmpty(sClaveEmpleado);
                txtDiasResidencia.Enabled = iDiasResidencia < 1;


            }
            catch (Exception ex) { throw new Exception(ex.Message); }
        }
        protected void btnBuscar_Click(object sender, EventArgs e)
        {
            try
            {
                int iCodigoAlerta = Convert.ToInt32(ddlAlertaResidencia.SelectedValue);
                /***************Variables para filtros***************/
                int iCodigoPlanta = Convert.ToInt32(ddlPlanta.SelectedValue);
                int iCodigoProceso = Convert.ToInt32(ddlProceso.SelectedValue);
                int iCodigoTipoArticulo = Convert.ToInt32(ddlTipoArticulo.SelectedValue);
                int iCodigoArticulo = (ddlArticulo.SelectedValue == null)? -1:Convert.ToInt32(ddlArticulo.SelectedValue);
                int iCodigoMaquina = Convert.ToInt32(ddlMaquina.SelectedValue);
                int iCodigoColor = Convert.ToInt32(ddlColor.SelectedValue);
                int iCodigoEmpleado;
                int.TryParse(txtEmpleado.Text, out iCodigoEmpleado);
                int iCodigoTurno = Convert.ToInt32(ddlTurno.SelectedValue);
                int iDiasResidencia = Convert.ToInt32(txtDiasResidencia.Text);
                /****************************************************/
                uwgPiezasConResidencia.Columns.Clear();
                uwgPiezasConResidencia.Rows.Clear();
                if (ddlTipoBusqueda.SelectedValue.Equals("0"))
                {
                    DataTable dtPiezasResidencia = new svcSCPP.SCPPClient().ObtenerPiezasConResidencia(iCodigoAlerta, iCodigoPlanta, iCodigoProceso, iCodigoTipoArticulo, iCodigoArticulo, iCodigoMaquina, iCodigoColor, iCodigoEmpleado, iCodigoTurno, iDiasResidencia);
                    uwgPiezasConResidencia.DataSource = dtPiezasResidencia;
                    uwgPiezasConResidencia.DataBind();

                    uwgPiezasConResidencia.Columns[2].Hidden = true;//CodigoProceso
                    uwgPiezasConResidencia.Columns[4].Format = "dd-MM-yyyy";
                    uwgPiezasConResidencia.Columns[1].Width = new Unit(250, UnitType.Pixel);//Descripcion de Articulo
                    /************************Agregar Columna de Tipo CheckBox********************/
                    uwgPiezasConResidencia.Columns.Add("Eliminar");
                    uwgPiezasConResidencia.Columns[uwgPiezasConResidencia.Columns.Count - 1].Header.Caption = "Eliminar";
                    /****************************************************************************/
                    btnBaja.Visible = true;
                    btnReestablecer.Visible = false;
                }
                else
                {
                    DataTable dtPiezasBajaResidencia = new svcSCPP.SCPPClient().ObtenerPiezasBajaResidencia(iCodigoAlerta, iCodigoPlanta, iCodigoProceso, iCodigoTipoArticulo, iCodigoArticulo, iCodigoMaquina, iCodigoColor, iCodigoEmpleado, iCodigoTurno, iDiasResidencia);
                    uwgPiezasConResidencia.DataSource = dtPiezasBajaResidencia;
                    uwgPiezasConResidencia.DataBind();

                    uwgPiezasConResidencia.Columns[2].Hidden = true;//CodigoProceso
                    uwgPiezasConResidencia.Columns[4].Format = "dd-MM-yyyy";
                    uwgPiezasConResidencia.Columns[1].Width = new Unit(250, UnitType.Pixel);//Descripcion de Articulo
                    /************************Agregar Columna de Tipo CheckBox********************/
                    uwgPiezasConResidencia.Columns.Add("Reestablecer");
                    uwgPiezasConResidencia.Columns[uwgPiezasConResidencia.Columns.Count - 1].Header.Caption = "Reestablecer";
                    /****************************************************************************/
                    btnBaja.Visible = false;
                    btnReestablecer.Visible = true;

                }
                uwgPiezasConResidencia.Columns[uwgPiezasConResidencia.Columns.Count - 1].Type = ColumnType.CheckBox;
                uwgPiezasConResidencia.Columns[uwgPiezasConResidencia.Columns.Count - 1].AllowUpdate = AllowUpdate.Yes;
                uwgPiezasConResidencia.Columns[uwgPiezasConResidencia.Columns.Count - 1].Header.Style.CssClass = "checkBoxEditar";
                uwgPiezasConResidencia.Columns[uwgPiezasConResidencia.Columns.Count - 1].Header.Column.AllowUpdate = AllowUpdate.Yes;
                for (int i = 0; i < uwgPiezasConResidencia.Columns.Count; i++)
                {
                    uwgPiezasConResidencia.Columns[i].Header.Style.Wrap = true;
                    uwgPiezasConResidencia.Columns[i].CellStyle.HorizontalAlign = HorizontalAlign.Left;
                    uwgPiezasConResidencia.Columns[i].Header.Style.HorizontalAlign = HorizontalAlign.Center;
                    uwgPiezasConResidencia.Columns[i].AllowResize = AllowSizing.Fixed;
                }
            }
            catch (Exception ex) { throw new Exception(ex.Message); }
        }
        protected void btnBaja_Click(object sender, EventArgs e)
        {
            try
            {
                Usuario user = (Usuario)Session["UserLogged"];
                if (user == null) return;
                DataTable dtPiezasParaBaja = new DataTable("PiezasParaBaja");
                dtPiezasParaBaja.Columns.Add("CodigoBarra", typeof(string));
                dtPiezasParaBaja.Columns.Add("CodigoAlerta", typeof(Int32));
                foreach (UltraGridRow rowPiezaResidencia in uwgPiezasConResidencia.Rows)
                {
                    if (Boolean.Parse(rowPiezaResidencia.Cells[uwgPiezasConResidencia.Columns.Count - 1].Value.ToString()))
                    {
                        DataRow drCodigoBarra = dtPiezasParaBaja.NewRow();
                        drCodigoBarra[0] = rowPiezaResidencia.Cells[0];
                        drCodigoBarra[1] = ddlAlertaResidencia.SelectedValue;
                        dtPiezasParaBaja.Rows.Add(drCodigoBarra);
                    }
                }
                bool bPiezaBaja = new svcSCPP.SCPPClient().PiezaBajaPorResidencia(dtPiezasParaBaja, user.CodUsuario);
                btnBuscar_Click(null, null);
            }
            catch (Exception ex) { throw new Exception(ex.Message); }
        }

        protected void btnReestablecer_Click(object sender, EventArgs e)
        {
            try
            {
                DataTable dtPiezasParaReestablecer = new DataTable("PiezasParaReestablecer");
                dtPiezasParaReestablecer.Columns.Add("CodigoBarra", typeof(string));
                dtPiezasParaReestablecer.Columns.Add("CodigoAlerta", typeof(Int32));
                foreach (UltraGridRow rowPiezaResidencia in uwgPiezasConResidencia.Rows)
                {
                    if (Boolean.Parse(rowPiezaResidencia.Cells[uwgPiezasConResidencia.Columns.Count - 1].Value.ToString()))
                    {
                        DataRow drCodigoBarra = dtPiezasParaReestablecer.NewRow();
                        drCodigoBarra[0] = rowPiezaResidencia.Cells[0];
                        drCodigoBarra[1] = ddlAlertaResidencia.SelectedValue;
                        dtPiezasParaReestablecer.Rows.Add(drCodigoBarra);
                    }
                }
                bool bPiezaBaja = new svcSCPP.SCPPClient().PriezaReestablecerResidencia(dtPiezasParaReestablecer);
                btnBuscar_Click(null, null);
            }
            catch (Exception ex) { throw new Exception(ex.Message); }
        }
    }
}
