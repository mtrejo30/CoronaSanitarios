


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



namespace LAMOSA.SCPP.Client.View.Administrador
{
    public partial class CambioContraseña : ReporteBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!Page.IsPostBack && !Page.IsCallback)
            {
              /* Usuario user = (Usuario)Session["UserLogged"];
              /*  if (user != null)
                {*/
              /*  DropDownList cmbPlanta = ((DropDownList)Page.Master.FindControl("cmbPlanta"));
      
        /*    NomUsuario.InnerText = user.NombreUsuario;


            GuardarI.Disabled= true;
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

           
               /* }*/


                
            }
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
