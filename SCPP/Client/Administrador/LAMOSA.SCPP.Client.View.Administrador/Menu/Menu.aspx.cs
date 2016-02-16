using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

namespace LAMOSA.SCPP.Client.View.Administrador
{
    public partial class Menu : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                svcSCPP.SCPPClient svc = new svcSCPP.SCPPClient();
                Page.ClientScript.RegisterStartupScript(this.GetType(), "ButtonClickScript", "<script>alert('" + svc.ObtenerMensajeInicioSesion() + "');</script>", false);
                if (Request.QueryString["msgError"] == null)
                    msgDenegar.Visible = false;
            }
        }
    }
}