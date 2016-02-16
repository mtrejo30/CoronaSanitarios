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
using LAMOSA.SCPP.Server.BusinessEntity;
using LAMOSA.SCPP.Server.BusinessEntity.Server;

namespace LAMOSA.SCPP.Client.View.Administrador
{
    public partial class ControlPisoLamosa : System.Web.UI.MasterPage
    {

        #region Fields

        //string demo = String.Empty;

        #endregion Fields

        #region Properties



        #endregion Properties

        #region Methods

        #region Constructors and Destructor

        public ControlPisoLamosa()
        {

        }

        #endregion Constructors and Destructor

        #region Common



        #endregion Common

        #region Event Handlers
        protected void Page_Init(object sender, EventArgs e)
        {
            try
            {
                String uri = Request.Url.LocalPath.ToString();
                uri = uri.Substring(1);
                uri = uri.Substring(uri.IndexOf("/"));

                Usuario user = (Usuario)Session["UserLogged"];
                if (user == null)
                {
                    LogOut(null, null);
                }
                else if (new LoginU().HasScreenPermision(user.CodRol, uri))
                {
                    cmbPlanta.DataSource = new Combos().Get_Planta_RolCbo(user.CodRol);//svc.ObtenerCentroTrabajo(int.Parse(cmbPlanta.SelectedValue), 1);
                    cmbPlanta.DataTextField = "descripcionPlanta";
                    cmbPlanta.DataValueField = "ClavePlanta";
                    cmbPlanta.DataBind();
                    lblfecha.InnerText = String.Format("{0:dd} {0:MMMM} {0:yyyy}", DateTime.Now);
                    lblHora.InnerText = DateTime.Now.ToShortTimeString();
                    lblNombre.InnerText = user.Nombre;
                    lblUsuario.InnerText = user.NombreUsuario;
                    lblRol.InnerText = user.DesRol;
                    //demo = "pro-line-down-fly/menu3.css";
                    this.DataBind();
                }
                else
                {
                    Response.Redirect("../Menu/Menu.aspx?msgError=0", true);
                }
            }
            catch
            {
                //Response.Redirect("../Login.aspx", false);
            }
        }
        #region Page_Load
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        protected void LogOut(object sender, EventArgs e)
        {
            try
            {
                FormsAuthentication.SignOut();
                String uri = Request.Url.LocalPath.ToString();
                Response.Redirect(uri, false);
            }
            catch
            {
                //Response.Redirect("../Login.aspx", false);
            }
        }
        protected string demo()
        {
            return "pro-line-down-fly/menu3.css";
        }
        #endregion Page_Load

        #endregion Event Handlers

        #endregion Methods

    }
}
