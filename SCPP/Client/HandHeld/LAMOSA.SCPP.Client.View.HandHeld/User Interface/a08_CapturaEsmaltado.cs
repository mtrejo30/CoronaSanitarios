using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DA = LAMOSA.SCPP.Client.View.HandHeld.DataAccess;
using LAMOSA.SCPP.Client.View.HandHeld.User_Interface;

namespace LAMOSA.SCPP.Client.View.HandHeld
{
    public partial class a08_CapturaEsmaltado : Form
    {

        #region fields

        private LoginUsuario lu = null;
        private c00_Common oDA0 = new c00_Common();
        private c08_CapturaEsmaltado oDA = new c08_CapturaEsmaltado();
        public int iTiempoEnMinutosCapturaColor = 20;
        private Timer trActualizarDatosServidor = new Timer();
        private int iPeriodoActualizacion = -1;

        private int iCodPieza = -1;
        private int iCodPrueba = -1;
        private frmProduccionOperador frmProduccion;
        #endregion fields

        #region properties
        public frmProduccionOperador FormaProduccion { get { return frmProduccion; } set { frmProduccion = value; } }


        #endregion properties

        #region methods

        #region constructors and destructor

        public a08_CapturaEsmaltado(LoginUsuario lu)
        {
            InitializeComponent();
            //
            this.lu = lu;
            this.ConfigurarFormulario();
            this.ConfigurarPanelControles();
        }
        ~a08_CapturaEsmaltado()
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

            this.txTipo.Enabled = false;
            this.txTipo.ReadOnly = true;
            this.txModelo.Enabled = false;
            this.txModelo.ReadOnly = true;
            this.txColor.Enabled = false;
            this.txColor.ReadOnly = true;

            this.txEtiqueta.KeyPress += new KeyPressEventHandler(this.txEtiqueta_KeyPress);
            this.cbxPrueba.SelectedIndexChanged += new EventHandler(this.cbxPrueba_SelectedIndexChanged);

            this.btAceptar.Click += new EventHandler(this.btAceptar_Click);
            this.btDefectos.Click += new EventHandler(this.btDefectos_Click);
            this.btTerminar.Click += new EventHandler(this.btTerminar_Click);

            // Configuracion del Timer.
            //this.trActualizarDatosServidor.Interval = 100;
            //this.trActualizarDatosServidor.Enabled = true;
            //this.trActualizarDatosServidor.Tick += new EventHandler(this.trActualizarDatosServidor_Tick);
        }
        #endregion ConfigurarPanelControles

        #region ValidarPieza
        private void ValidarPieza(string sCodBarras, int iCodProceso)
        {
            ValidacionPieza valOnLine = null;
            ValidacionPieza valFinal = null;
            DataTable dtObj = null;

            int iCodUltimioProceso = -1;
            string sDescUltimoProceso = string.Empty;

            // Validar en linea.
            valOnLine = this.oDA0.ValidarPieza(sCodBarras, iCodProceso, false);
            valFinal = valOnLine;

            oDA0.ObtenerUltimoProcesoPieza(valFinal.CodPieza, true, out iCodUltimioProceso, out sDescUltimoProceso);
            if (this.lu.CodProceso == iCodUltimioProceso)
            {
                valFinal.MensajeValidacion = "Pieza Recien Procesada";
                valFinal.ValProcesoExitosa = false;
            }
            // logica de validacion adicional.
            if (valFinal.ValProcesoExitosa && valFinal.ValNoDefDespExitosa)
            {
                // Obtener ClaveModelo de la pieza.
                string sClaveModelo = string.Empty;
                dtObj = this.oDA0.ObtenerModeloTipoPieza2(valFinal.CodPieza);
                if (dtObj != null)
                {
                    if (dtObj.Columns.Count > 0 & dtObj.Rows.Count > 0 & dtObj.Columns.Contains("ClaveModelo"))
                        sClaveModelo = Convert.ToString(dtObj.Rows[0]["ClaveModelo"]);
                    else
                        sClaveModelo = string.Empty;
                }

                // Validar existe combinacion MODELO-COLOR en Articulos.
                string sSKU = sClaveModelo + "-" + this.lu.ClaveColor;
                int iCodModelo = this.oDA0.ExisteModelo(sSKU);
                if (iCodModelo == -1)
                {
                    valFinal.MensajeValidacion = "Modelo-Color invalido";
                    valFinal.ValidacionExitosa = false;
                }
                else
                {
                    valFinal.ValidacionExitosa = true;
                }
            }
            else
            {
                valFinal.ValidacionExitosa = false;
            }

            // Logica de validacion de controles.
            this.iCodPieza = valFinal.CodPieza;
            this.encabezado.Mensaje = valFinal.MensajeValidacion;

            // Defectos.
            if (valFinal.ValProcesoExitosa)
            {
                this.btDefectos.Enabled = true;
            }
            else
            {
                this.btDefectos.Enabled = false;
            }

            if (valFinal.ValidacionExitosa)
            {
                // Obtener el Tipo y Modelo de la pieza.
                dtObj = this.oDA0.ObtenerModeloTipoPieza2(this.iCodPieza);
                if (dtObj != null)
                {
                    if (dtObj.Columns.Count > 0 & dtObj.Rows.Count > 0 & dtObj.Columns.Contains("DesTipoModelo"))
                        this.txTipo.Text = Convert.ToString(dtObj.Rows[0]["DesTipoModelo"]);
                    else
                        this.txTipo.Text = string.Empty;

                    if (dtObj.Columns.Count > 0 & dtObj.Rows.Count > 0 & dtObj.Columns.Contains("DesModelo"))
                        this.txModelo.Text = Convert.ToString(dtObj.Rows[0]["DesModelo"]);
                    else
                        this.txModelo.Text = string.Empty;
                }

                this.cbxPrueba.Enabled = true;
                this.btAceptar.Enabled = true;

                this.btAceptar.Focus();
            }
            else
            {
                this.txTipo.Text = string.Empty;
                this.txModelo.Text = string.Empty;

                this.cbxPrueba.Enabled = false;
                this.btAceptar.Enabled = false;

                this.txEtiqueta.SelectAll();
                this.txEtiqueta.Focus();
            }
        }
        #endregion ValidarPieza

        #region EnviarDatosAlServidor
        private void EnviarDatosAlServidor()
        {
            if (oDA0.EstaServicioDisponible())
            {
                this.encabezado.Mensaje = "Enviando datos...";
                this.Refresh();

                c00_Transacciones ct = new c00_Transacciones();
                bool bEnvioExitoso = ct.EnviarDatosEsmaltado();
                if (!bEnvioExitoso)
                {
                    MessageBox.Show("Envio incompleto, intentar nuevamente", "SCPP", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
                }

                this.encabezado.Mensaje = String.Empty;
                this.Refresh();
            }
        }
        #endregion EnviarDatosAlServidor

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

                this.txColor.Text = this.lu.DesColor;

                // Obtener periodo del timer.
                this.encabezado.Conexion = EstadoConexion.Offline;
                this.iPeriodoActualizacion = this.oDA0.ObtenerPeriodoActualizacion(this.lu.CodProceso, this.lu.CodPantalla);
                this.timerTiempoColorEsmaltado.Enabled = true;
                this.timerTiempoColorEsmaltado.Interval = this.iTiempoEnMinutosCapturaColor * 60 * 1000;
                // Llenar ComboBox 'Prueba'.
                DataTable dtObj = null;
                DataRow drObj = null;
                ComboBox cbxObj = null;

                dtObj = this.oDA0.ObtenerPruebas(this.lu.CodPlanta, this.lu.CodProceso);
                drObj = dtObj.NewRow();
                drObj["CodPrueba"] = -1;
                drObj["DesPrueba"] = "Seleccionar...";
                dtObj.Rows.InsertAt(drObj, 0);
                cbxObj = this.cbxPrueba;
                cbxObj.ValueMember = "CodPrueba";
                cbxObj.DisplayMember = "DesPrueba";
                cbxObj.SelectedIndexChanged -= new EventHandler(this.cbxPrueba_SelectedIndexChanged);
                cbxObj.DataSource = dtObj;
                cbxObj.SelectedValue = -1;
                cbxObj.SelectedIndexChanged += new EventHandler(this.cbxPrueba_SelectedIndexChanged);

                this.cbxPrueba.Enabled = false;
                this.btAceptar.Enabled = false;
                this.btDefectos.Enabled = false;

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
                    TextBox txObj = (TextBox)sender;

                    this.ValidarPieza(txObj.Text, this.lu.CodProceso);
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
        #region cbxPrueba_SelectedIndexChanged
        private void cbxPrueba_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                ComboBox cbxobj = (ComboBox)sender;
                this.iCodPrueba = Convert.ToInt32(cbxobj.SelectedValue);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
            }
        }
        #endregion cbxPrueba_SelectedIndexChanged

        #region btAceptar_Click
        private void btAceptar_Click(object sender, EventArgs e)
        {
            try
            {
                this.encabezado.Mensaje = String.Empty;

                if (this.lu.CodConfigHandHeld < 50000)
                    this.lu.CodConfigHandHeld = new c00_Transacciones().EnviarUnConfigHandHeld(this.lu.CodConfigHandHeld);

                long lCodPiezaTransaccion = this.oDA0.InsertarPiezaTransaccion(DA.eTipoConexion.Local,
                                                                                this.lu.CodConfigHandHeld,
                                                                                this.iCodPieza,
                                                                                this.lu.Fecha);
                this.oDA0.ActulizarUltimoProcesoPieza(DA.eTipoConexion.Local, this.iCodPieza, this.lu.CodProceso);
                this.oDA.ActualizarColorPieza(DA.eTipoConexion.Local, this.iCodPieza, this.lu.CodColor);
                if (Convert.ToInt32(this.cbxPrueba.SelectedValue) != -1)
                    this.oDA0.PruebaProcesoIns( this.lu.CodPlanta, Convert.ToInt32(this.cbxPrueba.SelectedValue),
                                                this.lu.CodProceso, this.iCodPieza, DateTime.Now);

                this.EnviarDatosAlServidor();

                this.iCodPieza = -1;
                this.iCodPrueba = -1;

                this.cbxPrueba.Enabled = false;
                this.btAceptar.Enabled = false;

                this.txEtiqueta.Text = string.Empty;
                this.txTipo.Text = string.Empty;
                this.txModelo.Text = string.Empty;

                this.txEtiqueta.Focus();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
            }
        }
        #endregion btAceptar_Click
        #region btDefectos_Click
        private void btDefectos_Click(object sender, EventArgs e)
        {
            try
            {
                this.btAceptar.Enabled = false;
                this.txEtiqueta.SelectAll();
                this.txEtiqueta.Focus();

                this.lu.CodPieza = this.iCodPieza;
                this.lu.CodBarras = this.txEtiqueta.Text;
                a04_Defectos frmObj = new a04_Defectos(this.lu, false);
                frmObj.Show();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
            }
        }
        #endregion btDefectos_Click
        #region btTerminar_Click
        private void btTerminar_Click(object sender, EventArgs e)
        {
            try
            {
                //this.EnviarDatosAlServidor();

                //Regresar a Configuracion Inicial.
                a03_ConfiguracionInicial frmObj = new a03_ConfiguracionInicial(this.lu);
                frmObj.Show();
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
            }
        }
        #endregion btTerminar_Click

        private void btnProduccion_Click(object sender, EventArgs e)
        {
            try
            {
                frmProduccion = new frmProduccionOperador(this.lu);
                frmProduccion.FormaCaptura = this;
                frmProduccion.Show();
                //this.Hide();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
                this.Show();
            }
        }

        #endregion event handlers

        private void timerTiempoColorEsmaltado_Tick(object sender, EventArgs e)
        {
            this.timerTiempoColorEsmaltado.Enabled = false;
            MessageBox.Show("¡Continuar esmaltado de piezas con color " + this.txColor.Text + "!",this.Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
            this.timerTiempoColorEsmaltado.Enabled = true;
        }
        #endregion methods

    }
}