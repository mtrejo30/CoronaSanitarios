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
    public partial class a03_ConfigInicial : Form
    {
        private LoginUsuario lu = null;


        #region Constructors and Destructor

        public a03_ConfigInicial(LoginUsuario lu)
        {
            InitializeComponent();
            //
            this.lu = lu;
            this.ConfigurarFormulario();
            this.ConfigurarPanelControles();
        }
        ~a03_ConfigInicial()
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

            //this.cbxProcesos.SelectedIndexChanged += new EventHandler(this.cbxProceso_SelectedIndexChanged);

            this.btContinuar.Click += new EventHandler(this.btContinuar_Click);
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
            int PosY = (int)((this.Height - this.pnControles.Height - this.pbLogo.Height) / 2);
            this.pnControles.Location = new Point(PosX, PosY);
        }
        #endregion ConfigurarCabecera

        #endregion Common


        #region Form_Resize
        private void Form_Resize(object sender, EventArgs e)
        {
            try
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
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
            }
        }
        #endregion Form_Resize

        private void Form_Load(object sender, EventArgs e)
        {
            try
            {
                this.lbOperador.Text = lu.NomEmpleado;
                this.lbPuesto.Text = lu.DesPuesto;
                this.lbPlanta.Text = lu.DesPlanta;
                this.lbProceso.Text = "Configuración Inicial";
                //
                this.dtpFecha.Value = DateTime.Today;
                //
                //DataAccess.DA da = new DataAccess.DA();
                clsConfigInicial oConfigInicial = new clsConfigInicial();
                DataTable dtObj = null;
                DataRow dr = null;
                ComboBox cbxObj = null;

                dtObj = oConfigInicial.getObtenerTurnos().Tables[0];
                //dtObj = da.ObtenerTurnos();
                dr = dtObj.NewRow();
                dr["CodTurno"] = -1;
                dr["DesTurno"] = "Seleccionar...";
                dtObj.Rows.InsertAt(dr, 0);
                cbxObj = this.cbxTurno;
                cbxObj.DataSource = dtObj;
                cbxObj.ValueMember = "CodTurno";
                cbxObj.DisplayMember = "DesTurno";
                cbxObj.SelectedValue = -1;

                dtObj = null;
                cbxObj = null;
                dtObj = oConfigInicial.getObtenerProcesos().Tables[0];
                //dtObj = da.ObtenerProcesos();
                dr = dtObj.NewRow();
                dr["CodProceso"] = "-1";
                dr["DesProceso"] = "Seleccionar...";
                dtObj.Rows.InsertAt(dr, 0);
                cbxObj = this.cbxProcesos;
                cbxObj.ValueMember = "CodProceso";
                cbxObj.DisplayMember = "DesProceso";
                cbxObj.DataSource = dtObj;
                
                this.cbxProcesos.SelectedIndexChanged += new EventHandler(this.cbxProceso_SelectedIndexChanged);
                cbxObj.SelectedValue = -1;
                //
                this.dtpFecha.Focus();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
            }
        }


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


        private void cbxProceso_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                DataTable dtObj = null;
                DataRow dr = null;
                ComboBox cbxObj = null;

                ComboBox cbxProceso = (ComboBox)sender;
                int iCodProceso = Convert.ToInt32(cbxProceso.SelectedValue);

                clsConfigInicial oConfigInicial = new clsConfigInicial();
                dtObj = oConfigInicial.getObtenerPantallaProcesos(iCodProceso).Tables[0];
                //dtObj = da.ObtenerPantallasProceso(iCodProceso);
                dr = dtObj.NewRow();
                dr["CodPantalla"] = -1;
                dr["DesPantalla"] = "Seleccionar...";
                dtObj.Rows.InsertAt(dr, 0);
                cbxObj = this.cbxOpcion;
                cbxObj.DataSource = dtObj;
                cbxObj.ValueMember = "CodPantalla";
                cbxObj.DisplayMember = "DesPantalla";
                cbxObj.SelectedValue = -1;

                if (iCodProceso != -1)
                {
                    this.cbxOpcion.Focus();
                }
                else
                {
                    this.cbxProcesos.Focus();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
            }
        }

 
        private void btContinuar_Click(object sender, EventArgs e)
        {
            try
            {
                if (Convert.ToInt32(this.cbxTurno.SelectedValue) == -1)
                {
                    this.lbMensaje.Text = "Seleccione un Turno.";
                    this.cbxTurno.Focus();
                }
                else if (Convert.ToInt32(this.cbxProcesos.SelectedValue) == -1)
                {
                    this.lbMensaje.Text = "Seleccione un Proceso.";
                    this.cbxProcesos.Focus();
                }
                else if (Convert.ToInt32(this.cbxOpcion.SelectedValue) == -1)
                {
                    this.lbMensaje.Text = "Seleccione una Opción.";
                    this.cbxOpcion.Focus();
                }
                else
                {
                    this.lu.Fecha = this.dtpFecha.Value;
                    this.lu.CodTurno = Convert.ToInt32(this.cbxTurno.SelectedValue);
                    this.lu.DesTurno = this.cbxTurno.Text;
                    this.lu.CodProceso = Convert.ToInt32(this.cbxProcesos.SelectedValue);
                    this.lu.DesProceso = this.cbxProcesos.Text;

                    Form frmObj = null;
                    switch (Convert.ToInt32(this.cbxOpcion.SelectedValue))
                    {
                        case 1:
                            frmObj = new a04_Vaciado01(this.lu);
                            break;
                        case 2:
                            frmObj = new a05_ArmadoCarroSecador(this.lu);
                            break;
                        case 3:
                            frmObj = new a06_EntradaCarroSecador(this.lu);
                            break;
                    }
                    frmObj.Show();
                    this.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
            }
        }
   

        private void btSalir_Click(object sender, EventArgs e)
        {
            DialogResult dr = MessageBox.Show("¿Salir de la Aplicación?", "SCPP", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
            if (dr == DialogResult.Yes)
            {
                Application.Exit();
            }
        }



    }
}