
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
    public partial class MasterOut : System.Web.UI.MasterPage
    {

        #region Fields

        //string demo = String.Empty;

        #endregion Fields

        #region Properties



        #endregion Properties

        #region Methods

        #region Constructors and Destructor

        public MasterOut()
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
                lblfecha.InnerText = String.Format("{0:dd} {0:MMMM} {0:yyyy}", DateTime.Now);
                lblHora.InnerText = DateTime.Now.ToShortTimeString();
            }
            catch
            {
                //Response.Redirect("../Login.aspx", false);
            }
        }
        #region Page_Load
        protected void Page_Load(object sender, EventArgs e)
        {}
        protected void LogOut(object sender, EventArgs e)
        {
            try
            {
              FormsAuthentication.SignOut();
                String uri = Request.Url.LocalPath.ToString();
                Response.Redirect("../Login.aspx", false);
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
