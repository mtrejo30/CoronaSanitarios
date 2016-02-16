


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



namespace LAMOSA.SCPP.Client.View.Administrador.CambioContrasena
{
    public partial class CambioContrasena : Page//ReporteBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Session.Keys.OfType<string>().Contains("UserLogged"))
                FormsAuthentication.RedirectToLoginPage();
            /*if (Session.AsQueryable().OfType<Usuario>().FirstOrDefault(usuario => usuario.CodUsuario != -100) == null)
                FormsAuthentication.RedirectToLoginPage();*/
            IEnumerable<string> sKeyUsuario = from usuario in Session.Keys.OfType<string>()
                                              where usuario == "UserLogged"
                                              select usuario;
            foreach (string usuario in sKeyUsuario)
            {
                if (Session[usuario].GetType() != typeof(Usuario))
                    FormsAuthentication.RedirectToLoginPage();
                if ((Session[usuario] as Usuario) == null || (Session[usuario] as Usuario).CodUsuario != -100)
                    FormsAuthentication.RedirectToLoginPage();
                return;
            }
            FormsAuthentication.RedirectToLoginPage();
        }

        protected void btnCambiaContrasena_Click(object sender, EventArgs e)
        {
            svcSCPP.SCPPClient svc = new svcSCPP.SCPPClient();
            string User = NomUsuario.Text;
            string ContraAnt = ContrasenaAnt.Text;
            string ContraNueva = ContrasenaNueva.Text;         
            LAMOSA.SCPP.Server.BusinessEntity.ContrasenaL C = new LAMOSA.SCPP.Server.BusinessEntity.ContrasenaL();
            try
            {
                C.Usuario = User;
                C.Contrasena = ContraAnt;
                C.ContrasenaNueva = ContraNueva;
                C = svc.CambiarContrasenaLogin(C);
                string msj = C.ExceptionMessage;
                if (C.ExceptionMessage != null && C.ExceptionMessage.Length > 30)
                {
                    CallBackManager.AddScriptBlock(this.Page, WebAsyncRefreshPanel1, "<script type='text/javascript'>alert('" + C.ExceptionMessage + "');</script>");
                    NomUsuario.Text = "";
                    //Page.RegisterStartupScript("script", "javascript:alert('"+ C.ExceptionMessage +" ')");
                }
                else
                {
                    CallBackManager.AddScriptBlock(this.Page, WebAsyncRefreshPanel1, "<script type='text/javascript'>alert('" + C.ExceptionMessage + "');</script>");
                }
            }
            catch (Exception err)
            {

                CallBackManager.AddScriptBlock(this.Page, WebAsyncRefreshPanel1, "<script type='text/javascript'>alert('" + err.Message + "');</script>");
            }       
        }
    }
}
