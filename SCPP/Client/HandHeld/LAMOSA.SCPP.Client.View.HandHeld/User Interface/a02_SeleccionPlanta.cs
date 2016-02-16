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
    public partial class a02_SeleccionPlanta : Form
    {

        #region fields

        private LoginUsuario lu = null;
        private c00_Common oDA0 = new c00_Common();
        private c02_SeleccionPlanta oDA = new c02_SeleccionPlanta();

        private Timer trActualizarDatosServidor = new Timer();
        private int iPeriodoActualizacion = -1;

        #endregion fields

        #region properties



        #endregion properties

        #region methods

        #region constructors and destructor

        public a02_SeleccionPlanta(LoginUsuario lu)
        {
            InitializeComponent();
            //
            this.lu = lu;
            this.ConfigurarFormulario();
            this.ConfigurarPanelControles();
        }
        ~a02_SeleccionPlanta()
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

            this.cbxPlanta.SelectedIndexChanged += new EventHandler(this.cbxPlanta_SelectedIndexChanged);
            this.cbxPlanta.KeyPress += new KeyPressEventHandler(this.cbxPlanta_KeyPress);

            this.btContinuar.Click += new EventHandler(this.btContinuar_Click);

            // Configuracion del Timer.
            //this.trActualizarDatosServidor.Interval = 100;
            //this.trActualizarDatosServidor.Enabled = true;
            //this.trActualizarDatosServidor.Tick += new EventHandler(this.trActualizarDatosServidor_Tick);
        }
        #endregion ConfigurarPanelControles

        #endregion common

        #region event handlers

        #region Form_Load
        private void Form_Load(object sender, EventArgs e)
        {
            try
            {
                this.encabezado.Operador = this.lu.NomEmpleado;
                this.encabezado.PuestoTurno = this.lu.DesPuesto + " - " + this.lu.DesTurno;
                this.encabezado.Planta = this.lu.DesPlanta;
                this.encabezado.Titulo = "Selección de planta";

                // Obtener periodo del timer.
                this.encabezado.Conexion = EstadoConexion.Offline;
                this.iPeriodoActualizacion = this.oDA0.ObtenerPeriodoActualizacion(0, 2);

                this.lu.CodPlanta = -1;
                this.lu.DesPlanta = String.Empty;

                DataTable dtObj = null;
                DataRow dr = null;
                ComboBox cbxObj = null;

                dtObj = this.oDA.ObtenerPlantasRol(this.lu.CodRol);
                dr = dtObj.NewRow();
                dr["CodPlanta"] = -1;
                dr["DesPlanta"] = "Seleccionar...";
                dtObj.Rows.InsertAt(dr, 0);
                cbxObj = this.cbxPlanta;
                cbxObj.DisplayMember = "DesPlanta";
                cbxObj.ValueMember = "CodPlanta";
                cbxObj.SelectedIndexChanged -= new EventHandler(this.cbxPlanta_SelectedIndexChanged);
                cbxObj.DataSource = dtObj;
                cbxObj.SelectedValue = -1;
                cbxObj.SelectedIndexChanged += new EventHandler(this.cbxPlanta_SelectedIndexChanged);
                
                this.cbxPlanta.Focus();
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

        #region trActualizarDatosServidor_Tick
        private void trActualizarDatosServidor_Tick(object sender, EventArgs e)
        {
            Timer trObj = (Timer)sender;
            try
            {
                trObj.Enabled = false;
                if (this.oDA0.EstaServicioDisponible())
                {
                    this.encabezado.Conexion = EstadoConexion.Online;
                }
                else
                {
                    this.encabezado.Conexion = EstadoConexion.Offline;
                }
            }
            catch (Exception)
            {
            }
            finally
            {
                trObj.Interval = this.iPeriodoActualizacion;
                trObj.Enabled = true;
            }
        }
        #endregion trActualizarDatosServidor_Tick

        #region cbxPlanta_SelectedIndexChanged
        private void cbxPlanta_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                ComboBox cbxObj = (ComboBox)sender;

                this.lu.CodPlanta = Convert.ToInt32(cbxObj.SelectedValue);
                this.lu.DesPlanta = cbxObj.Text;

                if (this.lu.CodPlanta == -1)
                {
                    this.encabezado.Mensaje = "Seleccione Planta";
                }
                else
                {
                    this.encabezado.Mensaje = String.Empty;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
            }
        }
        #endregion cbxPlanta_SelectedIndexChanged
        #region cbxPlanta_KeyPress
        private void cbxPlanta_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                // Validar ingreso del Enter.
                if (e.KeyChar == 13)
                {
                    ComboBox cbxObj = (ComboBox)sender;

                    if (this.lu.CodPlanta == -1)
                    {
                        this.encabezado.Mensaje = "Seleccione Planta";
                        
                        cbxObj.Focus();
                    }
                    else
                    {
                        this.encabezado.Mensaje = String.Empty;
                        
                        this.btContinuar.Focus();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
            }
        }
        #endregion cbxPlanta_KeyPress
        
        #region btContinuar_Click
        private void btContinuar_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.lu.CodPlanta == -1)
                {
                    this.encabezado.Mensaje = "Seleccione Planta";
                    
                    this.cbxPlanta.Focus();
                    return;
                }

                a03_ConfiguracionInicial frmObj = new a03_ConfiguracionInicial(this.lu);
                frmObj.Show();
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
            }
        }
        #endregion btContinuar_Click

        #endregion event handlers

        #endregion methods

    }
}