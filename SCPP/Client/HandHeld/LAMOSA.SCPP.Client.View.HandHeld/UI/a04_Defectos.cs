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
    public partial class a04_Defectos : Form
    {

        LoginUsuario lu = null;
        //cVaciado ovaciado = null;


        #region Constructors and Destructor

        public a04_Defectos(LoginUsuario lu)
        {
            InitializeComponent();
            //
            this.lu = lu;
            this.ConfigurarFormulario();
            this.ConfigurarPanelControles();
        }
        ~a04_Defectos()
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
            int PosY = (int)((this.Height - this.pnControles.Height - this.pbLogo.Height) / 2);
            this.pnControles.Location = new Point(PosX, PosY);
        }
        #endregion ConfigurarCabecera

        #endregion Common


        private void Form_Load(object sender, EventArgs e)
        {
            try
            {
                this.lbOperador.Text = lu.NomEmpleado;
                this.lbPuesto.Text = lu.DesPuesto;
                this.lbPlanta.Text = lu.DesPlanta;
                this.lbProceso.Text = "Vaciado - Captura Defectos (working)";

                //DataSet ods = null;
                //DataRow dr = null;
                //ComboBox cbxObj = null;

                this.txtDefecto.Focus();

               
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
            }
        }

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

        private void btSalir_Click(object sender, EventArgs e)
        {
            DialogResult dr = MessageBox.Show("¿Salir de la Aplicación?", "SCPP", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
            if (dr == DialogResult.Yes)
            {
                Application.Exit();
            }
        }


        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnTerminar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void txtDefecto_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                this.txtZona.Focus();
            }
        }

        private void txtPosicion_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                this.ckbReparacion.Focus();
            }
        }

    }
}