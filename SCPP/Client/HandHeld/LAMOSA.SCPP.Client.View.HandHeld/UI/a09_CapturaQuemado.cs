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
    public partial class a09_CapturaQuemado : Form
    {

        #region fields

        private LoginUsuario lu = null;
        private c09_CapturaQuemado oDA = new c09_CapturaQuemado();

        #endregion fields

        #region properties



        #endregion properties

        #region methods

        #region constructors and destructor

        public a09_CapturaQuemado(LoginUsuario lu)
        {
            InitializeComponent();
            //
            this.lu = lu;
            this.ConfigurarFormulario();
            this.ConfigurarPanelControles();
        }
        ~a09_CapturaQuemado()
        {

        }

        #endregion constructors and destructor

        #region common

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
            int PosY = 55;
            this.pnControles.Location = new Point(PosX, PosY);
        }
        #endregion ConfigurarCabecera

        #endregion common

        #region event handlers

        #region Form_Load
        private void Form_Load(object sender, EventArgs e)
        {
            try
            {
                this.lbOperador.Text = lu.NomEmpleado;
                this.lbPuesto.Text = lu.DesPuesto;
                this.lbPlanta.Text = lu.DesPlanta;
                this.lbProceso.Text = "Captura Quemado";
                
                //this.lu.CodSupervisor = -1;

                //DataTable dtObj = null;
                //DataRow dr = null;
                //ComboBox cbxObj = null;

                //dtObj = this.oDA.ObtenerClaveEmpleadoMFG(this.lu.CodEmpleado);
                //this.txOperador.Text = dtObj.Rows[0]["ClaveEmpleadoMFG"].ToString();

                //dtObj = this.oDA.ObtenerCentrosTrabajo(this.lu.CodPlanta, this.lu.CodProceso);
                //dr = dtObj.NewRow();
                //dr["CodCentroTrabajo"] = -1;
                //dr["DesCentroTrabajo"] = "Seleccionar...";
                //dtObj.Rows.InsertAt(dr, 0);
                //cbxObj = this.cbxCentroTrabajo;
                //cbxObj.ValueMember = "CodCentroTrabajo";
                //cbxObj.DisplayMember = "DesCentroTrabajo";
                //cbxObj.DataSource = dtObj;
                //cbxObj.SelectedIndexChanged += new EventHandler(this.cbxCentroTrabajo_SelectedIndexChanged);
                //cbxObj.SelectedValue = -1;

                //this.txSupervisor.Focus();
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