using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using LAMOSA.SCPP.Client.View.HandHeld.DataAccess;

namespace LAMOSA.SCPP.Client.View.HandHeld
{
    public partial class a02_SelPlanta : Form
    {

        #region Fields

        private LoginUsuario lu = null;

        #endregion Fields

        #region Properties



        #endregion Properties

        #region methods

        #region Constructors and Destructor

        public a02_SelPlanta(LoginUsuario lu)
        {
            InitializeComponent();
            this.lu = lu;
            this.ConfiguracionInicial();
        }
        ~a02_SelPlanta()
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
            //this.Load += new EventHandler(this.a01_Login_Load);
            //this.Resize += new EventHandler(this.a01_Login_Resize);

            #endregion Form

            #region Panel

            //this.pnLogin.BackColor = this.BackColor;

            #endregion Panel

            //this.btAceptar.Click += new EventHandler(this.btAceptar_Click);
            this.btSalir.Click += new EventHandler(this.btSalir_Click);
        }
        #endregion ConfiguracionInicial

        #endregion Common

        #region event handlers

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