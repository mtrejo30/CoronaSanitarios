using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using LAMOSA.SCPP.Client.View.HandHeld.HHsvc;

namespace LAMOSA.SCPP.Client.View.HandHeld
{
    public partial class CambioPassword : Form
    {

        #region Fields
        private c01_Login oDA = new c01_Login();
        private c00_Common oDA0 = new c00_Common();

        #endregion Fields

        #region methods

        #region Constructors and Destructor

        public CambioPassword()
        {
            InitializeComponent();
            //
            this.ConfigurarFormulario();
            this.ConfigurarPanelControles();
        }
        ~CambioPassword()
        {

        }

        #endregion Constructors and Destructor

        #region Common

        #region ConfigurarFormulario
        private void ConfigurarFormulario()
        {
            // Appearance.
            this.BackColor = Color.White;
            this.ForeColor = Color.Black;
            this.FormBorderStyle = FormBorderStyle.None;
            this.Text = String.Empty;

            // Layout.
            this.WindowState = FormWindowState.Maximized;
            this.AutoScroll = false;

            // Window Style.
            this.ControlBox = false;
            this.MaximizeBox = false;
            this.MinimizeBox = false;

            this.KeyPreview = true;

            // Eventos.
            this.Load += new EventHandler(this.Form_Load);
            this.Resize += new EventHandler(this.Form_Resize);
            this.KeyUp += new KeyEventHandler(this.Form_KeyUp);
        }
        #endregion ConfigurarFormulario
        #region ConfigurarControles
        private void ConfigurarControles()
        {
            // Encabezado
            this.encabezado.Location = new Point(0, 0);

            // Panel
            int PosX = (int)((this.Width - this.pnControles.Width) / 2);
            int PosY = this.encabezado.Top + this.encabezado.Height;
            this.pnControles.Location = new Point(PosX, PosY);
        }
        #endregion ConfigurarControles
        #region ConfigurarPanelControles
        private void ConfigurarPanelControles()
        {
            this.pnControles.BackColor = this.BackColor;
            this.txPasswordOld.MaxLength = 10;
            this.txPasswordNew.MaxLength = 10;
            this.txPasswordNewRe.MaxLength = 10;
            this.txPasswordOld.PasswordChar = '*';
            this.txPasswordNew.PasswordChar = '*';
            this.txPasswordNewRe.PasswordChar = '*';
        }
        #endregion ConfigurarPanelControles

        #endregion Common

        #region event handlers

        #region Form_Load
        private void Form_Load(object sender, EventArgs e)
        {
            try
            {
                this.encabezado.Titulo = "Cambio de contraseña";

                // Obtener periodo del timer.
                this.encabezado.Conexion = EstadoConexion.Offline;

                this.txUsuario.Focus();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
            }
        }
        #endregion Form_Load
        #region Form_Resize
        private void Form_Resize(object sender, EventArgs e)
        {
            try
            {
                this.ConfigurarControles();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
            }
        }
        #endregion Form_Resize
        #region Form_KeyUp
        private void Form_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Escape)
            {
                DialogResult dr = MessageBox.Show("¿Salir de la Aplicación?", "SCPP", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
                if (dr == DialogResult.Yes)
                {
                    Application.Exit();


                }
            }
        }
        #endregion Form_KeyUp


        private void btAceptar_Click_1(object sender, EventArgs e)
        {
            try
            {
                this.encabezado.Mensaje = string.Empty;
                // Validar Contraseña.
                if (string.IsNullOrEmpty(this.txUsuario.Text))
                {
                    this.encabezado.Mensaje = "Capture su Usuario.";
                    this.txUsuario.SelectAll();
                    this.txUsuario.Focus();
                    return;
                }
                if (string.IsNullOrEmpty(this.txPasswordOld.Text))
                {
                    this.encabezado.Mensaje = "Capture Contraseña Actual.";
                    this.txPasswordOld.SelectAll();
                    this.txPasswordOld.Focus();
                    return;
                }
                if (string.IsNullOrEmpty(this.txPasswordNew.Text))
                {
                    this.encabezado.Mensaje = "Capture Contraseña Nueva.";
                    this.txPasswordNew.SelectAll();
                    this.txPasswordNew.Focus();
                    return;
                }
                if (string.IsNullOrEmpty(this.txPasswordNewRe.Text))
                {
                    this.encabezado.Mensaje = "Repita la Contraseña Nueva.";
                    this.txPasswordNewRe.SelectAll();
                    this.txPasswordNewRe.Focus();
                    return;
                }
                else if (!this.txPasswordNew.Text.Equals(this.txPasswordNewRe.Text))
                {
                    this.encabezado.Mensaje = "La contraseña no coinside con la Contraseña nueva.";
                    this.txPasswordNewRe.SelectAll();
                    this.txPasswordNewRe.Focus();
                    return;
                }

                //bool bValRes = false;
                //bool bValResSpecified = true;
                
                /*svcUsuario.UsuarioServiceManagerWM smUser = new svcUsuario.UsuarioServiceManagerWM();
                svcUsuario.Usuario u = new svcUsuario.Usuario();
                svcUsuario.Password pass = new svcUsuario.Password();*/
                
                /*u.Nombre = this.txUsuario.Text;
                pass.Clave = this.txPasswordOld.Text;
                u.Password = pass;*/

                LoginUsuario lu = this.oDA.Login(this.txUsuario.Text, this.txPasswordOld.Text);
                //smUser.Loguear(u, out bValRes, out bValResSpecified);
                if (lu.IsLogin || lu.Mensaje == "La contraseña ya expiró. Favor de cambiar contraseña.")//(bValRes)
                {
                    /*pass.Clave = this.txPasswordNew.Text;
                    u.Password = pass;
                    bValRes = false;
                    bValResSpecified = true;
                    smUser.CambiarPassword(u, out bValRes, out bValResSpecified);*/
                    string mensaje = this.oDA.CambiarPassword(this.txUsuario.Text, this.txPasswordOld.Text, this.txPasswordNew.Text);
                    
                    /*if (string.IsNullOrEmpty(mensaje))
                    {*/
                    MessageBox.Show(mensaje, "Cambio Contraseña", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
                    if(MessageBox.Show("¿Desea salir de la opción de cambio de contraseña?", "Cambio Contraseña", MessageBoxButtons.OKCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1)== DialogResult.OK)
                        this.Close();
                    /*}
                    else
                        MessageBox.Show(mensaje, "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
                    //"Ocurrio un error al cambiar su contraseña, intente nuevamente."*/
                }
                else
                    MessageBox.Show("No se pudo autentificar su usuario, verifique su Usuario/Contraseña o verifique si su usuario no ha sido dado de Baja.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
            }
        }

        #endregion event handlers

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            try
            {
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
            }
        }

        #endregion methods

        private void txUsuario_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                txPasswordOld.Focus();
            }
        }

        private void txPasswordOld_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                txPasswordNew.Focus();
            }
        }

        private void txPasswordNew_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                txPasswordNewRe.Focus();
            }
        }

        private void txPasswordNewRe_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                btAceptar.Focus();
            }
        }

    }
}