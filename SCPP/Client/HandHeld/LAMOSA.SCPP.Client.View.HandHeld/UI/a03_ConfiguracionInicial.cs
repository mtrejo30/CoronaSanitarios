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
    public partial class a03_ConfiguracionInicial : Form
    {

        #region fields

        private LoginUsuario lu = null;
        private c03_ConfiguracionInicial oDA = new c03_ConfiguracionInicial();

        #endregion fields

        #region properties



        #endregion properties

        #region methods

        #region constructors and destructor

        public a03_ConfiguracionInicial(LoginUsuario lu)
        {
            InitializeComponent();
            //
            this.lu = lu;
            this.ConfigurarFormulario();
            this.ConfigurarPanelControles();
        }
        ~a03_ConfiguracionInicial()
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

            this.dtpFecha.ValueChanged += new EventHandler(this.dtpFecha_ValueChanged);
            this.cbxOpcion.SelectedIndexChanged += new EventHandler(this.cbxOpcion_SelectedIndexChanged);

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
                this.lbProceso.Text = "Configuración Inicial";

                this.lu.Fecha = DateTime.MinValue;
                this.lu.CodTurno = -1;
                this.lu.DesTurno = string.Empty;
                this.lu.CodProceso = -1;
                this.lu.DesProceso = string.Empty;
                this.lu.CodConfigHandHeld = -1;

                this.dtpFecha.Value = DateTime.Today;
                
                DataTable dtObj = null;
                DataRow drObj = null;
                ComboBox cbxObj = null;

                dtObj = this.oDA.ObtenerTurnos();
                drObj = dtObj.NewRow();
                drObj["CodTurno"] = -1;
                drObj["DesTurno"] = "Seleccionar...";
                dtObj.Rows.InsertAt(drObj, 0);
                cbxObj = this.cbxTurno;
                cbxObj.ValueMember = "CodTurno";
                cbxObj.DisplayMember = "DesTurno";
                cbxObj.DataSource = dtObj;
                cbxObj.SelectedIndexChanged += new EventHandler(this.cbxTurno_SelectedIndexChanged);
                cbxObj.SelectedValue = -1;

                dtObj = this.oDA.ObtenerProcesos();
                drObj = dtObj.NewRow();
                drObj["CodProceso"] = -1;
                drObj["DesProceso"] = "Seleccionar...";
                dtObj.Rows.InsertAt(drObj, 0);
                cbxObj = this.cbxProceso;
                cbxObj.ValueMember = "CodProceso";
                cbxObj.DisplayMember = "DesProceso";
                cbxObj.DataSource = dtObj;
                cbxObj.SelectedIndexChanged += new EventHandler(this.cbxProceso_SelectedIndexChanged);
                cbxObj.SelectedValue = -1;
                
                this.dtpFecha.Focus();
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

        #region dtpFecha_ValueChanged
        private void dtpFecha_ValueChanged(object sender, EventArgs e)
        {
            try
            {
                DateTimePicker dtpObj = (DateTimePicker)sender;

                this.lu.Fecha = dtpObj.Value;

                this.cbxTurno.Focus();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
            }
        }
        #endregion dtpFecha_ValueChanged
        #region cbxTurno_SelectedIndexChanged
        private void cbxTurno_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                ComboBox cbxObj = (ComboBox)sender;

                this.lu.CodTurno = Convert.ToInt32(cbxObj.SelectedValue);
                this.lu.DesTurno = cbxObj.Text;

                if (this.lu.CodTurno == -1)
                {
                    this.lbMensaje.Text = "Seleccione Turno";
                    cbxObj.Focus();
                }
                else
                {
                    this.lbMensaje.Text = "";
                    this.cbxProceso.Focus();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
            }
        }
        #endregion cbxTurno_SelectedIndexChanged
        #region cbxProceso_SelectedIndexChanged
        private void cbxProceso_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                DataTable dtObj = null;
                DataRow drObj = null;
                ComboBox cbxObj = null;

                cbxObj = (ComboBox)sender;

                this.lu.CodProceso = Convert.ToInt32(cbxObj.SelectedValue);
                this.lu.DesProceso = cbxObj.Text;

                if (this.lu.CodProceso == -1)
                {
                    this.lbMensaje.Text = "Seleccione Proceso";
                    cbxObj.Focus();
                }
                else
                {
                    this.lbMensaje.Text = "";

                    dtObj = this.oDA.ObtenerPantallasProceso(this.lu.CodProceso);
                    drObj = dtObj.NewRow();
                    drObj["CodPantalla"] = -1;
                    drObj["DesPantalla"] = "Seleccionar...";
                    dtObj.Rows.InsertAt(drObj, 0);
                    cbxObj = this.cbxOpcion;
                    cbxObj.ValueMember = "CodPantalla";
                    cbxObj.DisplayMember = "DesPantalla";
                    cbxObj.DataSource = dtObj;
                    cbxObj.SelectedValue = -1;

                    cbxObj.Focus();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
            }
        }
        #endregion cbxProceso_SelectedIndexChanged
        #region cbxOpcion_SelectedIndexChanged
        private void cbxOpcion_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                ComboBox cbxObj = (ComboBox)sender;

                this.lu.CodPantalla = Convert.ToInt32(cbxObj.SelectedValue);

                if (this.lu.CodPantalla == -1)
                {
                    this.lbMensaje.Text = "Seleccione Opción";
                    cbxObj.Focus();
                }
                else
                {
                    this.lbMensaje.Text = "";
                    this.btContinuar.Focus();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
            }
        }
        #endregion cbxOpcion_SelectedIndexChanged

        #region btContinuar_Click
        private void btContinuar_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.lu.Fecha == DateTime.MinValue)
                {
                    this.lbMensaje.Text = "Seleccione Fecha valida";
                    this.dtpFecha.Focus();
                }
                else if (this.lu.CodTurno == -1)
                {
                    this.lbMensaje.Text = "Seleccione Turno";
                    this.cbxTurno.Focus();
                }
                else if (this.lu.CodProceso == -1)
                {
                    this.lbMensaje.Text = "Seleccione Proceso";
                    this.cbxProceso.Focus();
                }
                else if (this.lu.CodPantalla == -1)
                {
                    this.lbMensaje.Text = "Seleccione Opción";
                    this.cbxOpcion.Focus();
                }
                else
                {
                    DataTable dtObj = this.oDA.ObtenerSigCodConfigHandHeld();
                    this.lu.CodConfigHandHeld = Convert.ToInt64(dtObj.Rows[0]["CodConfigHandHeld"]);
                    this.oDA.InsertarConfigHandHeld(this.lu.CodConfigHandHeld, 
                                                    this.lu.CodUsuario, 
                                                    this.lu.CodEmpleado, 
                                                    this.lu.CodSupervisor, 
                                                    this.lu.Fecha, 
                                                    this.lu.CodTurno, 
                                                    this.lu.CodPlanta, 
                                                    this.lu.CodProceso);

                    Form frmObj = null;
                    switch (this.lu.CodPantalla)
                    {
                        case 1:
                        case 4:
                        case 5:
                        case 6:
                            frmObj = new a04_CapturaInicial(this.lu);
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
        #endregion btContinuar_Click
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