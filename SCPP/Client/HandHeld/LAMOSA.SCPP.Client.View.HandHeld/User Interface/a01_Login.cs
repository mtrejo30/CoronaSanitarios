using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using LAMOSA.SCPP.Client.View.HandHeld.User_Interface;

namespace LAMOSA.SCPP.Client.View.HandHeld
{
    public partial class a01_Login : Form
    {

        #region Fields

        private c01_Login oDA = new c01_Login();
        private c00_Common oDA0 = new c00_Common();

        private Timer trActualizarDatosServidor = new Timer();
        private int iPeriodoActualizacion = -1;

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
            this.Dispose(true);
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

            this.txUsuario.MaxLength = 10;
            this.txContrasena.MaxLength = 10;
            this.txContrasena.PasswordChar = '*';

            this.txUsuario.KeyPress += new KeyPressEventHandler(this.txUsuario_KeyPress);
            this.txContrasena.KeyPress += new KeyPressEventHandler(this.txContrasena_KeyPress);

            this.btAceptar.Click += new EventHandler(this.btAceptar_Click);

            // Configuracion del Timer.
            //this.trActualizarDatosServidor.Interval = 100;
            //this.trActualizarDatosServidor.Enabled = true;
            //this.trActualizarDatosServidor.Tick += new EventHandler(this.trActualizarDatosServidor_Tick);
        }
        #endregion ConfigurarPanelControles

        #endregion Common

        #region event handlers

        #region Form_Load
        private void Form_Load(object sender, EventArgs e)
        {
            try
            {
                frmBienvenida frmVersion = new frmBienvenida();
                frmVersion.ShowDialog();
                frmVersion.Dispose();
                // Obtener periodo del timer.
                this.encabezado.Conexion = EstadoConexion.Offline;
                this.iPeriodoActualizacion = this.oDA0.ObtenerPeriodoActualizacion(0, 1);

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

        #region txUsuario_KeyPress
        private void txUsuario_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                // Validar ingreso del Enter.
                if (e.KeyChar == 13)
                {
                    TextBox txObj = (TextBox)sender;

                    this.txContrasena.Text = String.Empty;

                    if (string.IsNullOrEmpty(txObj.Text))
                    {
                        this.encabezado.Mensaje = "Capture Usuario";

                        txObj.SelectAll();
                        txObj.Focus();
                    }
                    else
                    {
                        this.encabezado.Mensaje = String.Empty;

                        this.txContrasena.SelectAll();
                        this.txContrasena.Focus();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
            }
        }
        #endregion txUsuario_KeyPress
        #region txContrasena_KeyPress
        private void txContrasena_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                // Validar ingreso del Enter.
                if (e.KeyChar == 13)
                {
                    TextBox txObj = (TextBox)sender;

                    if (string.IsNullOrEmpty(txObj.Text))
                    {
                        this.encabezado.Mensaje = "Capture Contraseña";

                        txObj.SelectAll();
                        txObj.Focus();
                    }
                    else
                    {
                        this.encabezado.Mensaje = String.Empty;

                        this.btAceptar.Focus();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
            }
        }
        #endregion txContrasena_KeyPress
        #region btAceptar_Click
        private void btAceptar_Click(object sender, EventArgs e)
        {
            try
            {
                this.encabezado.Titulo = "Login";
                // Validar Usuario.
                if (string.IsNullOrEmpty(this.txUsuario.Text))
                {
                    this.encabezado.Mensaje = "Capture Usuario";

                    this.txUsuario.Focus();
                    return;
                }

                // Validar Contraseña.
                if (string.IsNullOrEmpty(this.txContrasena.Text))
                {
                    this.encabezado.Mensaje = "Capture Contraseña";

                    this.txContrasena.Focus();
                    return;
                }

                LoginUsuario lu = this.oDA.Login(this.txUsuario.Text, this.txContrasena.Text);

                if (lu.IsLogin)
                {
                    //a02_SeleccionPlanta frmObj = new a02_SeleccionPlanta(lu);
                    a03_ConfiguracionInicial frmObj = new a03_ConfiguracionInicial(lu);
                    frmObj.Show();
                    this.Hide();
                }
                else
                {
                    this.encabezado.Mensaje = lu.Mensaje;

                    this.txUsuario.Text = String.Empty;
                    this.txContrasena.Text = String.Empty;

                    this.txUsuario.Focus();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
            }
        }
        #endregion btAceptar_Click

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                CambioPassword frmObj = new CambioPassword();
                frmObj.ShowDialog();
                frmObj.Dispose();
                this.Show();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
            }
        }

        #endregion event handlers

        #endregion methods

    }
}