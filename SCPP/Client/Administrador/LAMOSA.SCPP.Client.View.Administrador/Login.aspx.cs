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
using BE = LAMOSA.SCPP.Server.BusinessEntity;
using LAMOSA.SCPP.Server.BusinessEntity;
using LAMOSA.SCPP.Server.BusinessEntity.Server;

namespace LAMOSA.SCPP.Client.View.Administrador
{
    public partial class Login : System.Web.UI.Page
    {
        private enum EnumEstadoPantalla : int { Login=1, CambioContrasena=2}
        protected void Page_Load(object sender, EventArgs e)
        {
            ConfigurarPantalla();
            lblfecha.InnerText = String.Format("{0:dd} {0:MMMM} {0:yyyy}", DateTime.Now);
            lblHora.InnerText = DateTime.Now.ToShortTimeString();
        }
        protected void LoginButton_Click(object sender, EventArgs e)
        {
            try
            {
                if(ValidarInicioSesion())
                {
                    svcSCPP.SCPPClient svc = new svcSCPP.SCPPClient();
                    BE.LoginUsuario lu = new BE.LoginUsuario();
                    lu.Login = this.txtUsuario.Text;
                    lu.Password = this.txtPassword.Text;
                    lu = svc.Login(lu);
                    if (lu.CodUsuario > 0)
                    {
                        Usuario user = new LoginU().GetUser(lu.CodUsuario);
                        Session["UserLogged"] = user;
                        FormsAuthentication.RedirectFromLoginPage(lu.Login, false);
                    }
                    else
                    {
                        string msj;
                        msj = lu.Mensaje;
                        if (msj == "La contrase�a ya expir�. Favor de cambiar contrase�a.")
                        {
                            Page.RegisterStartupScript("Error", "<script>alert('" + lu.Mensaje + "');</script>");
                            //btnReset.Visible = true;
                            hideEstadoPantalla.Value = "2";
                            this.ConfigurarCambioContrasena();
                        }
                        else
                        {
                            //btnReset.Visible = false;
                            hideEstadoPantalla.Value = "1";
                            this.ConfigurarInicioSesion();
                            Page.RegisterStartupScript("Error", "<script>alert('" + lu.Mensaje + "');</script>");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Page.RegisterStartupScript("Error", "<script>alert('" + ex.Message + "');</script>");
            }
        }
        protected void btnReset_Click(object sender, EventArgs e)
        {
            if (this.ValidarCambioContrasena())
                if (CambiarContrasena())
                    ConfigurarInicioSesion();
        }
        private bool CambiarContrasena()
        {
            svcSCPP.SCPPClient svc = null;
            LAMOSA.SCPP.Server.BusinessEntity.ContrasenaL contrasena = new LAMOSA.SCPP.Server.BusinessEntity.ContrasenaL();
            try
            {
                svc = new svcSCPP.SCPPClient();
                contrasena.Usuario = this.txtUsuario.Text.Trim();
                contrasena.Contrasena = this.txtPassword.Text.Trim();
                contrasena.ContrasenaNueva = this.tbContrasenaUsuarioNueva.Text.Trim();
                contrasena = svc.CambiarContrasenaLogin(contrasena);
                if (contrasena.ExceptionMessage == "La contrase�a ha sido cambiada exitosamente")
                {
                    Page.RegisterStartupScript("MensajeAviso", "<script>alert('" + contrasena.ExceptionMessage + "');</script>");
                    return true; 
                }
                if (contrasena.ExceptionMessage != null && contrasena.ExceptionMessage.Length > 30)
                {
                    Page.RegisterStartupScript("MensajeAviso", "<script>alert('" + contrasena.ExceptionMessage + "');</script>");
                    return false;
                }
                else
                {
                    Page.RegisterStartupScript("MensajeAviso", "<script>alert('" + contrasena.ExceptionMessage + "');</script>");
                    return false;
                }
            }
            catch (Exception err)
            {
                Page.RegisterStartupScript("MensajeAviso", "<script>alert('" + err.Message + "');</script>");
                return false;
            }
            finally
            {
                svc = null;
            }
        }
        private void ConfigurarPantalla()
        {
            switch (hideEstadoPantalla.Value)
            {
                case "1":
                    ConfigurarInicioSesion();
                    break;
                case "2":
                    ConfigurarCambioContrasena();
                    break;
            }
        }
        private void ConfigurarCambioContrasena()
        {
            //Label titulo
            this.lblTitulo.Text = "CAMBIO CONTRASE�A";
            //Label contrase�a
            this.lblContrasenaUsuario.Text = "Contrase�a Actual:";
            //Label contrase�a nueva
            this.lblContrasenaUsuarioNueva.Enabled = true;
            this.lblContrasenaUsuarioNueva.Visible = true;
            //Label confirmar contrase�a
            this.lblConfirmarContrasenaUsuario.Enabled = true;
            this.lblConfirmarContrasenaUsuario.Visible = true;
            //TextBox contrase�a nueva
            this.tbContrasenaUsuarioNueva.Enabled = true;
            this.tbContrasenaUsuarioNueva.Visible = true;
            //TextBox confirmar contrase�a
            this.tbConfirmarContrasenaUsuario.Enabled = true;
            this.tbConfirmarContrasenaUsuario.Visible = true;
            //Botton Cambiar contrase�a
            this.btnReset.Enabled = true;
            this.btnReset.Visible = true;
            //HiddenField estadopantalla
            this.hideEstadoPantalla.Value = "2";
            //Botton inicio de sesion
            this.btnLogin.Enabled = false;
            this.btnLogin.Visible = false;
        }
        private void ConfigurarInicioSesion()
        {
            //Label titulo
            this.lblTitulo.Text = "LOGIN";
            //Label contrase�a
            this.lblContrasenaUsuario.Text = "Contrase�a:";
            //Label contrase�a nueva
            this.lblContrasenaUsuarioNueva.Enabled = false;
            this.lblContrasenaUsuarioNueva.Visible = false;
            //Label confirmar contrase�a
            this.lblConfirmarContrasenaUsuario.Enabled = false;
            this.lblConfirmarContrasenaUsuario.Visible = false;
            //TextBox contrase�a nueva
            this.tbContrasenaUsuarioNueva.Enabled = false;
            this.tbContrasenaUsuarioNueva.Visible = false;
            //TextBox confirmar contrase�a
            this.tbConfirmarContrasenaUsuario.Enabled = false;
            this.tbConfirmarContrasenaUsuario.Visible = false;
            //Botton Cambiar contrase�a
            this.btnReset.Enabled = false;
            this.btnReset.Visible = false;
            //HiddenField estadopantalla
            this.hideEstadoPantalla.Value = "1";
            //Botton inicio de sesion
            this.btnLogin.Enabled = true;
            this.btnLogin.Visible = true;
        }
        private string ValidarContrasena()
        {
            svcSCPP.SCPPClient svc = null;
            string sMensaje = string.Empty;
            try
            {
                svc = new svcSCPP.SCPPClient();
                if (!svc.ValidarPoliticaContrasena(this.tbContrasenaUsuarioNueva.Text.Trim()))
                {
                    sMensaje = "La contrase�a no cumple con las politicas de seguridad, por favor verifique:\n";
                    sMensaje += "\t- Longitud minima de la contrase�a.\n";
                    sMensaje += "\t- La contrase�a debe incluir almenos uno de los caracteres entre A-Z, a-z, 0-9.";
                    return sMensaje;
                }
                if (this.tbContrasenaUsuarioNueva.Text.Trim() != this.tbConfirmarContrasenaUsuario.Text.Trim())
                {
                    sMensaje = "La confirmaci�n de la contrase�a no es correcta,\nfavor de ingresar la confirmaci�n de contrase�a nuevamente.";
                    return sMensaje;
                }
                return string.Empty;
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
            finally
            {
                svc = null;
            }
        }
        private bool ValidarInicioSesion()
        {
            if (this.txtUsuario.Text == "")
            {
                Page.RegisterStartupScript("Error", "<script>alert('Introduzca su Usuario.');</script>");
                return false;
            }
            else if (this.txtPassword.Text == "")
            {
                Page.RegisterStartupScript("Error", "<script>alert('Introduzca su Contrase�a.');</script>");
                return false;
            }
            return true;
        }
        private bool ValidarCambioContrasena()
        {
            if (this.txtUsuario.Text == "")
            {
                Page.RegisterStartupScript("Error", "<script>alert('Introduzca su Usuario.');</script>");
                return false;
            }
            else if (this.txtPassword.Text == "")
            {
                Page.RegisterStartupScript("Error", "<script>alert('Introduzca su Contrase�a.');</script>");
                return false;
            }
            else if (this.tbContrasenaUsuarioNueva.Text == "")
            {
                Page.RegisterStartupScript("Error", "<script>alert('Introduzca su Nueva Contrase�a.');</script>");
                return false;
            }
            else if (this.tbConfirmarContrasenaUsuario.Text == "")
            {
                Page.RegisterStartupScript("Error", "<script>alert('Introduzca su Confirmaci�n de Contrase�a.');</script>");
                return false;
            }
            string sMensaje = ValidarContrasena();
            if (sMensaje != string.Empty)
            {
                Page.RegisterStartupScript("Error", "<script>alert('" + sMensaje + "');</script>");
                return false;
            }
            return true;
        }
    }
}
