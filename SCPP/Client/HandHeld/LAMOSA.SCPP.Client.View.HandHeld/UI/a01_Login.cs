using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DT.CE;

namespace LAMOSA.SCPP.Client.View.HandHeld
{
    public partial class a01_Login : Form
    {

        #region Fields



        #endregion Fields

        #region Properties



        #endregion Properties

        #region methods

        #region Constructors and Destructor

        public a01_Login()
        {
            InitializeComponent();
            //
            this.ConfigurarFormulario();
            this.ConfigurarPanelControles();
        }
        ~a01_Login()
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
            this.Text = "";

            // Layout.
            this.WindowState = FormWindowState.Maximized;

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
        #region ConfigurarPanelControles
        private void ConfigurarPanelControles()
        {
            this.pnControles.BackColor = this.BackColor;

            this.tbUsuario.KeyUp += new KeyEventHandler(this.tbUsuario_KeyUp);
            this.tbContrasena.KeyUp += new KeyEventHandler(this.tbContrasena_KeyUp);
            this.btAceptar.Click += new EventHandler(this.btAceptar_Click);
            this.btSalir.Click += new EventHandler(this.btSalir_Click);
        }
        #endregion ConfigurarPanelControles
        #region ConfigurarCabecera
        private void ConfigurarCabecera()
        {
            // Ajustar Logo.
            this.pbLogo.Top = 0;
            this.pbLogo.Left = this.Width - this.pbLogo.Width;
            // Ajustar Boton Salir.
            this.btSalir.Top = this.pbLogo.Height;
            this.btSalir.Left = this.Width - this.btSalir.Width;
            // Ajustar ProgressBar Procesando.
            this.pbrProcesando.Top = this.pbLogo.Height;
            this.pbrProcesando.Left = this.Width - this.pbLogo.Width;

            // Ajustar Panel.
            int PosX = (int)((this.Width - this.pnControles.Width) / 2);
            int PosY = (int)((this.Height - this.pnControles.Height - 55) / 2);
            this.pnControles.Location = new Point(PosX, PosY);
        }
        #endregion ConfigurarCabecera

        #endregion Common

        #region event handlers

        #region Form_Load
        private void Form_Load(object sender, EventArgs e)
        {
            try
            {

                this.lbProceso.Text = "Login";
                //clsConfig.getConection().conecta();
                this.tbUsuario.Focus();
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
                this.ConfigurarCabecera();
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

        #region tbUsuario_KeyUp
        private void tbUsuario_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                this.tbContrasena.Focus();
            }
        }
        #endregion tbUsuario_KeyUp
        #region tbContrasena_KeyUp
        private void tbContrasena_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                this.btAceptar.Focus();
            }
        }
        #endregion tbContrasena_KeyUp

        #region btAceptar_Click
        private void btAceptar_Click(object sender, EventArgs e)
        {
            try
            {
                // Validar cuadros de texto.
                if (string.IsNullOrEmpty(this.tbUsuario.Text))
                {
                    this.lbMensaje.Text = "Ingresar su Usuario.";
                    this.tbUsuario.Focus();
                }
                else if (string.IsNullOrEmpty(this.tbContrasena.Text))
                {
                    this.lbMensaje.Text = "Ingresar su Contraseña.";
                    this.tbContrasena.Focus();
                }
                else
                {
                    
  //                  DataAccess.DA da = new DataAccess.DA();
//                    DataAccess.LoginUsuario lu = da.Login(this.tbUsuario.Text, this.tbContrasena.Text);


                    clsLogin ologin = new clsLogin();
                    LoginUsuario lu = ologin.Login(this.tbUsuario.Text, this.tbContrasena.Text);



                    if (lu.IsLogin)
                    {
                        a02_SeleccionPlanta frmObj = new a02_SeleccionPlanta(lu);
                        frmObj.Show();
                        this.Hide();
                    }
                    else
                    {
                        this.lbMensaje.Text = lu.Mensaje;
                        this.tbUsuario.Text = "";
                        this.tbContrasena.Text = "";
                        this.tbUsuario.Focus();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
            }
        }
        #endregion btAceptar_Click
        #region btSalir_Click
        private void btSalir_Click(object sender, EventArgs e)
        {
            DialogResult dr = MessageBox.Show("¿Salir de la Aplicación?", "SCPP", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
            if (dr == DialogResult.Yes)
            {
                Application.Exit();
            }
        }
        #endregion btSalir_Click

        #endregion event handlers

        #endregion methods

    }
}