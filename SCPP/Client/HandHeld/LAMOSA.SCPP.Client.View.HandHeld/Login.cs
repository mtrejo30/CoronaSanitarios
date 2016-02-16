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
    public partial class Login : Form
    {

        #region Fields



        #endregion Fields

        #region Properties



        #endregion Properties

        #region methods

        #region Constructors and Destructor

        public Login()
        {
            InitializeComponent();
            this.ConfiguracionInicial();
        }
        ~Login()
        {

        }

        #endregion Constructors and Destructor

        #region Common

        #region ConfiguracionInicial
        private void ConfiguracionInicial()
        {
            // Appearance.
            this.BackColor = Color.White;
            this.ForeColor = Color.Black;

            this.Text = "Login";
            //this.FormBorderStyle = FormBorderStyle.None;
            //this.Size = new Size(324, 298);
            //this.WindowState = FormWindowState.Maximized;
            //this.MaximizeBox = false;
            //this.MinimizeBox = false;
        }
        #endregion ConfiguracionInicial

        

        #endregion Common

        #region event handlers



        #endregion event handlers

        #endregion methods

    }
}