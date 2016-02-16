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
    public partial class a13_CapturaInventario : Form
    {

        #region fields

        private LoginUsuario lu = null;
        private c00_Common oDA0 = new c00_Common();
        private c13_CapturaInventario oDA = new c13_CapturaInventario();

        private Timer trActualizarDatosServidor = new Timer();
        private int iPeriodoActualizacion = -1;

        private string sCodBarras = string.Empty;
        private int iCodPieza = -1;
        private int iCodArticulo = -1;
        private int iCodProceso = -1;

        #endregion fields

        #region properties



        #endregion properties

        #region methods

        #region constructors and destructor

        public a13_CapturaInventario(LoginUsuario lu)
        {
            InitializeComponent();
            //
            this.lu = lu;
            this.ConfigurarFormulario();
            this.ConfigurarPanelControles();
        }
        ~a13_CapturaInventario()
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

            this.txEtiqueta.ReadOnly = false;
            this.txEtiqueta.Enabled = true;
            this.txPlanta.ReadOnly = true;
            this.txPlanta.Enabled = false;
            this.txProceso.ReadOnly = true;
            this.txProceso.Enabled = false;
            this.txTipo.ReadOnly = true;
            this.txTipo.Enabled = false;
            this.txModelo.ReadOnly = true;
            this.txModelo.Enabled = false;
            this.txColor.ReadOnly = true;
            this.txColor.Enabled = false;
            this.txCalidad.ReadOnly = true;
            this.txCalidad.Enabled = false;

            this.txEtiqueta.KeyPress += new KeyPressEventHandler(this.txEtiqueta_KeyPress);

            this.btCancelar.Click += new EventHandler(this.btCancelar_Click);

            //// Configuracion del Timer.
            //this.trActualizarDatosServidor.Interval = 100;
            //this.trActualizarDatosServidor.Enabled = true;
            //this.trActualizarDatosServidor.Tick += new EventHandler(this.trActualizarDatosServidor_Tick);
        }
        #endregion ConfigurarPanelControles

        #region ValidarEtiqueta
        private Validacion ValidarEtiqueta(string sCodBarras)
        {
            Validacion val = new Validacion();

            // Validar el codigo de barras no sea una cadena nula o vacia.
            if (string.IsNullOrEmpty(sCodBarras))
            {
                val.ValidacionExitosa = false;
                val.MensajeValidacion = "Capture Etiqueta";

                this.sCodBarras = string.Empty;
            }
            else
            {
                val.ValidacionExitosa = true;
                val.MensajeValidacion = "";

                this.sCodBarras = sCodBarras;
            }
            return val;
        }
        #endregion ValidarEtiqueta
        #region ValidarPieza
        private Validacion ValidarPieza(string sCodBarras)
        {
            Validacion val = new Validacion();

            // Validar exista la pieza.
            this.iCodPieza = this.oDA0.ObtenerCodPiezaCodBarras(sCodBarras, false);
            if (this.iCodPieza != -1)
            {
                val.ValidacionExitosa = true;
                val.MensajeValidacion = "";
            }
            else
            {
                val.ValidacionExitosa = false;
                val.MensajeValidacion = "Pieza no existe";
            }
            return val;
        }
        #endregion ValidarPieza
        #region EstaEnInventarioPocesoPieza
        private Validacion EstaEnInventarioPocesoPieza(string sCodBarras)
        {
            Validacion val = new Validacion();

            // Validar exista la pieza.
            this.iCodPieza = this.oDA0.EstaEnInventarioPocesoPieza(sCodBarras);
            if (this.iCodPieza != -1)
            {
                val.ValidacionExitosa = true;
                val.MensajeValidacion = "Pieza ya capturada";
            }
            return val;
        }
        #endregion EstaEnInventarioPocesoPieza

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
                this.encabezado.Titulo = "Captura " + this.lu.DesProceso;

                // Obtener periodo del timer.
                this.encabezado.Conexion = EstadoConexion.Offline;
                this.iPeriodoActualizacion = this.oDA0.ObtenerPeriodoActualizacion(this.lu.CodProceso, this.lu.CodPantalla);

                this.txEtiqueta.Focus();
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

                    // Verificar si hay actualizacion en datos.
                    DateTime dtFechaUltimaActualizacion = this.oDA0.ObtenerFechaUltimaActualizacion(this.lu.CodProceso, this.lu.CodPantalla);
                    if (this.oDA0.ExisteCambioEnProcesoPantalla(this.lu.CodProceso, this.lu.CodPantalla, dtFechaUltimaActualizacion))
                    {
                        DialogResult drRes = MessageBox.Show("¿Desea actualizar?", "Existen datos actualizados", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
                        if (drRes == DialogResult.Yes)
                        {
                            a00_CargaDatos frmObj = new a00_CargaDatos(this.lu.CodPlanta, this.lu.CodProceso, this.lu.CodPantalla);
                            frmObj.SetFormCalling(this);
                            frmObj.ShowDialog();
                            frmObj.Dispose();
                            this.Show();
                        }
                    }
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

        #region txEtiqueta_KeyPress
        private void txEtiqueta_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {


                // Validar ingreso del Enter.
                if (e.KeyChar == 13)
                {
                    this.txPlanta.Text = String.Empty;
                    this.txProceso.Text = String.Empty;
                    this.txTipo.Text = String.Empty;
                    this.txModelo.Text = String.Empty;
                    this.txColor.Text = String.Empty;
                    this.txCalidad.Text = String.Empty;

                    TextBox txObj = (TextBox)sender;
                    Validacion val = null;
                    DataTable dtObj = null;

                    val = this.ValidarEtiqueta(txObj.Text);
                    this.encabezado.Mensaje = val.MensajeValidacion;
                    if (!val.ValidacionExitosa)
                    {
                        return;
                    }
                    else
                    {
                        // Obtener el CodPieza.
                        val = this.ValidarPieza(this.sCodBarras);
                        if (!val.ValidacionExitosa)
                        {
                            val = this.EstaEnInventarioPocesoPieza(this.sCodBarras);
                            if (val.ValidacionExitosa)
                            {
                                MessageBox.Show(val.MensajeValidacion, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
                            }
                            else
                            {
                                // Preguntar al usuario si desea agregar la pieza.
                                DialogResult drRes = MessageBox.Show("Pieza fuera de inventario ¿Desea registrarla?", "SCPP", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
                                if (drRes == DialogResult.Yes)
                                {
                                    this.lu.CodBarras = this.txEtiqueta.Text;
                                    a13_PiezaNuevaInventario frmObj = new a13_PiezaNuevaInventario(this.lu);
                                    frmObj.Show();
                                }
                            }
                        }
                        else
                        {
                            // Obtener el proceso de la pieza en inventario.
                            DataTable dtUltProcesoPza = this.oDA0.ObtenerUltimoProcesoPieza(this.iCodPieza, false);
                            if (dtUltProcesoPza != null && dtUltProcesoPza.Rows.Count > 0)
                            {
                                this.iCodProceso = Convert.ToInt32(dtUltProcesoPza.Rows[0][0]);
                                this.txProceso.Text = dtUltProcesoPza.Rows[0][1].ToString();
                            }
                            // Obtener la Planta de la pieza.
                            this.txPlanta.Text = this.lu.DesPlanta;
                            //this.txProceso.Text = this.oDA0.ObtenerDesProceso(this.iCodProceso);

                            // Obtener el Modelo y Tipo de la pieza.
                            this.iCodArticulo = this.oDA0.ObtenerCodModeloPieza(this.iCodPieza);
                            dtObj = this.oDA0.ObtenerModeloTipoPieza(this.iCodArticulo);
                            this.txTipo.Text = Convert.ToString(dtObj.Rows[0]["DesTipoModelo"]);
                            this.txModelo.Text = Convert.ToString(dtObj.Rows[0]["DesModelo"]);

                            // Obtener el Color de la pieza.
                            dtObj = this.oDA0.ObtenerColorPieza(this.iCodPieza);
                            if (dtObj.Rows.Count > 0)
                            {
                                this.txColor.Text = Convert.ToString(dtObj.Rows[0]["DesColor"]);
                            }
                            else
                            {
                                this.txColor.Text = String.Empty;
                            }

                            // Obtener la Calidad de la pieza.
                            dtObj = this.oDA0.ObtenerCalidadPieza(this.iCodPieza);
                            if (dtObj.Rows.Count > 0)
                            {
                                this.txCalidad.Text = Convert.ToString(dtObj.Rows[0]["DesCalidad"]);
                            }
                            else
                            {
                                this.txCalidad.Text = String.Empty;
                            }

                            // Actualizar la pieza de inventario.
                            this.oDA.ActualizarPiezaInventario(this.iCodPieza);

                            txObj.Text = String.Empty;
                            txObj.Focus();
                        }
                    }
                }
                else
                {
                    // Validar ingreso de digitos y el retroceso.
                    if (((int)e.KeyChar) >= 48 && ((int)e.KeyChar) <= 57 || e.KeyChar == 8)
                        e.Handled = false;
                    else
                        e.Handled = true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
            }
        }
        #endregion txEtiqueta_KeyPress

        #region btCancelar_Click
        private void btCancelar_Click(object sender, EventArgs e)
        {
            try
            {
                DialogResult dr = MessageBox.Show("¿Desea Terminar la Captura de Inventario?", "SCPP", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
                if (dr == DialogResult.Yes)
                {
                    // Regresar a Configuracion Inicial.
                    a03_ConfiguracionInicial frmObj = new a03_ConfiguracionInicial(this.lu);
                    frmObj.Show();
                    this.Close();
                }
                else
                {
                    this.txEtiqueta.SelectAll();
                    this.txEtiqueta.Focus();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
            }
        }
        #endregion btCancelar_Click

        #endregion event handlers

        #endregion methods

    }
}