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

namespace LAMOSA.SCPP.Client.View.Administrador.Seguridad
{
    public partial class CambiarContraseña : ReporteBase
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
                DropDownList cmbPlanta = ((DropDownList)Page.Master.FindControl("cmbPlanta"));
      
          NomUsuario.InnerText = user.NombreUsuario;


            GuardarI.Disabled= false;
            foreach (ScreenPermission sp in new Actions().GetActionBySreen(user.CodRol, Request.Url.LocalPath))
            {
                switch (sp.ActionCode)
                {
                    case 1: //Buscar
                   
                        break;
                    case 2: //Exportar
                      
                        break;
                    case 3: //Nuevo
                      
                        break;
                    case 4: //Editar

                        break;
                    case 5: //Guardar
                        GuardarI.Disabled = false;
                        break;
                }

            }

           
                }


                
            }
        }



        protected void btnCambiaContrasena_Click(object sender, EventArgs e)
        {
            svcSCPP.SCPPClient svc = new svcSCPP.SCPPClient();
            string ContraAnt = ContrasenaAnt.Text;
            string ContraNueva = ContrasenaNueva.Text;
            Usuario user = (Usuario)Session["UserLogged"];
            var CodUser = user.CodUsuario;
            LAMOSA.SCPP.Server.BusinessEntity.ContrasenaC C = new LAMOSA.SCPP.Server.BusinessEntity.ContrasenaC();
            try
            {
                C.codUsuario = CodUser;
                if (string.IsNullOrEmpty(ContraAnt) & string.IsNullOrEmpty(ContraNueva))
                {
                    ContraAnt = ContraNueva = "Lamosa06";
                }
                C.Contrasena = ContraAnt;
                C.ContrasenaNueva = ContraNueva;
                C = svc.CambiarContrasena(C);
                string msj = C.ExceptionMessage;
                if (C.ExceptionMessage != null && C.ExceptionMessage.Length > 30)
                {
                    CallBackManager.AddScriptBlock(this.Page, WebAsyncRefreshPanel1, "<script type='text/javascript'>alert('" + C.ExceptionMessage + "');</script>");
                    //Page.ClientScript.RegisterStartupScript(this.GetType(), "ButtonClickScript", "<script>alert('" + C.ExceptionMessage + "');</script>", false);
                }
                else
                {
                    CallBackManager.AddScriptBlock(this.Page, WebAsyncRefreshPanel1, "<script type='text/javascript'>alert('" + C.ExceptionMessage + "');</script>");
                    //Page.ClientScript.RegisterStartupScript(this.GetType(), "ButtonClickScript", "<script>alert('" + C.ExceptionMessage + "');</script>", false);
                }
            }
            catch (Exception err)
            {

                CallBackManager.AddScriptBlock(this.Page, WebAsyncRefreshPanel1, "<script type='text/javascript'>alert('" + err.Message + "');</script>");
                //Page.ClientScript.RegisterStartupScript(this.GetType(), "ButtonClickScript", "<script>alert('" + err.Message + "');</script>", false);
            }
       
        }

     


        #endregion
        #endregion

    }
}
