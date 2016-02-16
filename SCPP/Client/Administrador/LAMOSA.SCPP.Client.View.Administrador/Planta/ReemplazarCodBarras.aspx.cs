using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using LAMOSA.SCPP.Server.BusinessEntity.Server;
using LAMOSA.SCPP.Server.BusinessEntity;
using Infragistics.WebUI.Shared;

namespace LAMOSA.SCPP.Client.View.Administrador.Planta
{
    public partial class ReemplazarCodBarras : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                String uri = Request.Url.LocalPath.ToString();
                Usuario user = (Usuario)Session["UserLogged"];
                if (user != null)
                {
                    ddlProceso.DataSource = new Combos().Get_ProcesoCbo();
                    ddlProceso.DataTextField = "DescripcionProceso";
                    ddlProceso.DataValueField = "ClaveProceso";
                    ddlProceso.DataBind();

                    ddlTipoArticulo.DataSource = new Combos().Get_TipoArticuloCbo();
                    ddlTipoArticulo.DataTextField = "DesTipoArticulo";
                    ddlTipoArticulo.DataValueField = "CodTipoArticulo";
                    ddlTipoArticulo.DataBind();
                    ddlTipoArticulo.Items.Insert(0, new System.Web.UI.WebControls.ListItem("Todos", "-1"));

                    ddlTipoArticulo_SelectedIndexChanged(null, null);
                }

            }
        }
        protected void ddlTipoArticulo_SelectedIndexChanged(object sender, EventArgs e)
        {
            ddlModelo.Items.Clear();
            int cod_ta = Convert.ToInt32(ddlTipoArticulo.SelectedValue);
            if (cod_ta > 0)
            {
                ddlModelo.DataSource = new Combos().ObtenerArticulo(cod_ta);
                ddlModelo.DataTextField = "Descripcion";
                ddlModelo.DataValueField = "Codigo";
                ddlModelo.DataBind();
            }
            ddlModelo.Items.Insert(0, new System.Web.UI.WebControls.ListItem("Todos", "-1"));
        }

        private String fillUL(DataTable dt, Boolean isReplacement)
        {
            String li = "<li class=\"sliding-element title\" style=\"margin-left: 0px;\"><h3><table class=\"tblList\"><tbody><tr><td>C&oacute;digo</td><td>Tipo</td><td>Modelo</td><td>Vaciador</td></tr></tbody></table></h3></li>";
            try
            {
                foreach (DataRow dr in dt.Rows)
                {
                    String tbl = "<table class=\"tblList\"><tbody><tr>" +
                                "<td class=\"hidden\">" + dr["CodPieza"] + "</td>" +
                                "<td class=\"hidden\">" + (isReplacement ? "true" : "") + "</td>" +
                                "<td>" + dr["Codigo"] + "</td>" +
                                "<td>" + dr["Tipo"] + "</td>" +
                                "<td>" + dr["Modelo"] + "</td>" +
                                "<td>" + dr["Vaciador"] + "</td>" +
                                "<td class=\"hidden\">" + dr["Fecha Registro"] + "</td>" +
                                "<td class=\"hidden\">" + dr["Color"] + "</td>" +
                                "<td class=\"hidden\">" + dr["Calidad"] + "</td>" +
                                "</tr></tbody></table>";
                    li += "<li rel=\"" + dr["CodPieza"] + "\" class=\"sliding-element\"><a href=\"javascript:void(0)\">" + tbl + "</a></li>";
                }
            }
            catch { }
            return li;
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            try
            {
                Usuario user = (Usuario)Session["UserLogged"];
                if (user != null)
                {

                    DropDownList cmbPlanta = ((DropDownList)Page.Master.FindControl("cmbPlanta"));
                    int planta = Convert.ToInt32(cmbPlanta.SelectedValue);
                    int proceso = Convert.ToInt32(ddlProceso.SelectedValue);
                    int tipoArticulo = Convert.ToInt32(ddlTipoArticulo.SelectedValue);
                    int modelo = Convert.ToInt32(ddlModelo.SelectedValue);
                    CodsReemplazo.InnerHtml = fillUL(new CodigoReemplazo().GetReplacementCodes(planta, proceso, tipoArticulo, modelo), true);
                    CodsDetenidos.InnerHtml = fillUL(new CodigoReemplazo().GetDetaineesCodes(planta, proceso, tipoArticulo, modelo), false);
                }
            }
            catch { }
        }

        protected void btnSave_Click1(object sender, EventArgs e)
        {
            String msg = "";
            try
            {
                int cod_reemplazo = Convert.ToInt32(HCodReemplazo.Value);
                int cod_detenido = Convert.ToInt32(HCodDetenido.Value);
                msg = new CodigoReemplazo().GenerateReplacementCodes(cod_reemplazo, cod_detenido);
            }
            catch { msg = "Hubo un problema al generar la accion, intente nuevamente"; }
            CallBackManager.AddScriptBlock(this.Page, WebAsyncRefreshPanel1, "<script type='text/javascript'>alert('" + msg + "');</script>");
            Button1_Click(null, null);
        }
    }
}
