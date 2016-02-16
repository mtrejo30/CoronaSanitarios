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


namespace LAMOSA.SCPP.Client.View.Administrador.Seguridad
{
    public partial class Configuracion : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            { 
                Usuario user = (Usuario)Session["UserLogged"];
                if (user != null)
                {
                    LlenaCampos();
                    btnGuardar.Enabled = false;
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
                                btnGuardar.Enabled = true;
                                break;
                        }


                    }
                }
            }
        }

        private void LlenaCampos()
        {
            svcSCPP.SCPPClient svc = new svcSCPP.SCPPClient();
            TxtIntentos.Text = ((LAMOSA.SCPP.Server.BusinessEntity.Configuracion)svc.ObtenerConfig(1)[0]).ValorConfiguracion.ToString();
            TxtDiasPass.Text = ((LAMOSA.SCPP.Server.BusinessEntity.Configuracion)svc.ObtenerConfig(2)[0]).ValorConfiguracion.ToString();
          
        }
        protected void BotonGuardar_click(object sender, EventArgs e)
        {
            svcSCPP.SCPPClient svc = new svcSCPP.SCPPClient();
            LAMOSA.SCPP.Server.BusinessEntity.Configuracion Conf = new LAMOSA.SCPP.Server.BusinessEntity.Configuracion();

            try
            {
                Conf.CodConfiguracion = 1;
                Conf.ValorConfiguracion = int.Parse(TxtIntentos.Text);

                svc.GuardarConfig(Conf);

                Conf.CodConfiguracion = 2;
                Conf.ValorConfiguracion = int.Parse(TxtDiasPass.Text);

                svc.GuardarConfig(Conf);
                WebAsyncRefreshPanel1.DataBind();
                LlenaCampos();

            }
            catch (Exception err)
            {

                throw err;
            }
        }
    }
}
