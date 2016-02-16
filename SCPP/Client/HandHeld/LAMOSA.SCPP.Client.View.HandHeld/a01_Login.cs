using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

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
            this.ConfiguracionInicial();
        }
        ~a01_Login()
        {

        }

        #endregion Constructors and Destructor

        #region Common

        #region ConfiguracionInicial
        private void ConfiguracionInicial()
        {
            #region Form

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

            // Eventos.
            this.Load += new EventHandler(this.a01_Login_Load);
            this.Resize += new EventHandler(this.a01_Login_Resize);

            #endregion Form

            #region Panel

            this.pnLogin.BackColor = this.BackColor;

            #endregion Panel

            this.btAceptar.Click += new EventHandler(this.btAceptar_Click);
            this.btSalir.Click += new EventHandler(this.btSalir_Click);
        }
        #endregion ConfiguracionInicial

        #endregion Common

        #region event handlers

        #region a01_Login_Load
        private void a01_Login_Load(object sender, EventArgs e)
        {
            this.tbUsuario.Focus();
        }
        #endregion a01_Login_Load
        #region a01_Login_Resize
        private void a01_Login_Resize(object sender, EventArgs e)
        {
            // Ajustar Logo.
            this.pbLogo.Top = 0;
            this.pbLogo.Left = this.Width - this.pbLogo.Width;

            // Ajustar Panel.
            int PosX = (int)((this.Width - this.pnLogin.Width) / 2);
            int PosY = (int)((this.Height - this.pnLogin.Height - this.pbLogo.Height) / 2);
            this.pnLogin.Location = new Point(PosX, PosY);
        }
        #endregion a01_Login_Resize

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
                    DataAccess.DA da = new DataAccess.DA();
                    DataAccess.LoginUsuario lu = da.Login(this.tbUsuario.Text, this.tbContrasena.Text);

                    if (lu.IsLogin)
                    {
                        this.Hide();
                        a02_SelPlanta frmObj = new a02_SelPlanta(lu);
                        frmObj.Show();
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
            Application.Exit();
        }
        #endregion btSalir_Click

        #endregion event handlers

        #endregion methods

    }
}