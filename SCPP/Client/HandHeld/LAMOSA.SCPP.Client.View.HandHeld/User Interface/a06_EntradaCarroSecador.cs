using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DA = LAMOSA.SCPP.Client.View.HandHeld.DataAccess;

namespace LAMOSA.SCPP.Client.View.HandHeld
{
    public partial class a06_EntradaCarroSecador : Form
    {

        #region fields

        private LoginUsuario lu = null;
        private c00_Common oDA0 = new c00_Common();
        private c06_EntradaCarroSecador oDA = new c06_EntradaCarroSecador();

        private Timer trActualizarDatosServidor = new Timer();
        private int iPeriodoActualizacion = -1;

        private DateTime dtHoraInicial = DateTime.MinValue;
        private DataTable dtCarroPiezas = null;
        
        private int iCodCarro = -1;
        private DateTime dtHoraEntrada = DateTime.MinValue;
        private double dTiempoSecado = -1;

        private int iCodProcesoAnt = -1;

        #endregion fields

        #region properties



        #endregion properties

        #region methods

        #region constructors and destructor

        public a06_EntradaCarroSecador(LoginUsuario lu)
        {
            InitializeComponent();
            //
            this.lu = lu;
            this.ConfigurarFormulario();
            this.ConfigurarPanelControles();
        }
        ~a06_EntradaCarroSecador()
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

            this.txCodCarro.KeyPress += new KeyPressEventHandler(this.txCodCarro_KeyPress);
            this.dtpHoraEntrada.ValueChanged += new EventHandler(this.dtpHoraEntrada_ValueChanged);
            this.dtpHoraEntrada.KeyPress += new KeyPressEventHandler(this.dtpHoraEntrada_KeyPress);
            this.txTiempoSecado.KeyPress += new KeyPressEventHandler(this.txTiempoSecado_KeyPress);

            this.btAceptar.Click += new EventHandler(this.btAceptar_Click);
            this.btTerminar.Click += new EventHandler(this.btTerminar_Click);

            // Configuracion del Timer.
            //this.trActualizarDatosServidor.Interval = 100;
            //this.trActualizarDatosServidor.Enabled = true;
            //this.trActualizarDatosServidor.Tick += new EventHandler(this.trActualizarDatosServidor_Tick);
        }
        #endregion ConfigurarPanelControles

        #region ValidarCarro
        private Validacion ValidarCarro(int iCodPlanta, int iCodProceso, string sCodCarro)
        {
            Validacion val = new Validacion();

            // Validar el codigo del carro no sea una cadena nula o vacia.
            if (string.IsNullOrEmpty(sCodCarro))
            {
                val.ValidacionExitosa = false;
                val.MensajeValidacion = "Capture número de Carro";

                this.iCodCarro = -1;
                return val;
            }

            this.iCodCarro = Convert.ToInt32(sCodCarro);

            // Validar que el Numero de Carro exista y tenga Piezas.
            this.dtCarroPiezas = this.oDA0.ObtenerPiezasCarro(iCodPlanta, iCodProceso, this.iCodCarro, false);
            if (this.dtCarroPiezas.Rows.Count == 0)
            {
                val.ValidacionExitosa = false;
                val.MensajeValidacion = "Carro " + this.iCodCarro.ToString() + " no existe";

                this.dtCarroPiezas = null;
                this.iCodCarro = -1;
                return val;
            }
            else
            {
                val.ValidacionExitosa = true;
                val.MensajeValidacion = String.Empty;
                return val;
            }
        }
        #endregion ValidarCarro
        #region ValidarTiempoSecado
        private Validacion ValidarTiempoSecado(string sTiempoSecado)
        {
            Validacion val = new Validacion();

            // Validar el Tiempo de secado no sea una cadena nula o vacia.
            if (string.IsNullOrEmpty(sTiempoSecado))
            {
                val.ValidacionExitosa = false;
                val.MensajeValidacion = "Capture Tiempo Secado";

                this.dTiempoSecado = -1;
                return val;
            }

            this.dTiempoSecado = Convert.ToDouble(sTiempoSecado);

            if (this.dTiempoSecado == 0)
            {
                val.ValidacionExitosa = false;
                val.MensajeValidacion = "Capture Tiempo Secado valido";

                this.dTiempoSecado = -1;
                return val;
            }
            else
            {
                val.ValidacionExitosa = true;
                val.MensajeValidacion = String.Empty;

                return val;
            }
        }
        #endregion ValidarTiempoSecado

        #region EnviarDatosAlServidor
        private void EnviarDatosAlServidor()
        {
            this.encabezado.Mensaje = "Enviando datos...";
            this.Refresh();

            c00_Transacciones ct = new c00_Transacciones();
            bool bEnvioExitoso = ct.EnviarDatosSecado();
            if (bEnvioExitoso)
            {
                MessageBox.Show("Envio exitoso", "SCPP", MessageBoxButtons.OK, MessageBoxIcon.None, MessageBoxDefaultButton.Button1);
            }
            else
            {
                MessageBox.Show("Envio incompleto, intentar nuevamente", "SCPP", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
            }

            this.encabezado.Mensaje = String.Empty;
            this.Refresh();
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
                this.encabezado.Titulo = "Entrada Carro Secador";

                // Obtener periodo del timer.
                this.encabezado.Conexion = EstadoConexion.Offline;
                this.iPeriodoActualizacion = this.oDA0.ObtenerPeriodoActualizacion(this.lu.CodProceso, this.lu.CodPantalla);

                this.iCodProcesoAnt = this.oDA0.ObtenerProcesoAnterior(this.lu.CodProceso);

                this.dtHoraInicial = new DateTime(this.lu.Fecha.Year, this.lu.Fecha.Month, this.lu.Fecha.Day, 0, 0, 0);
                this.dtpHoraEntrada.Value = this.dtHoraInicial;

                this.iCodCarro = -1;
                this.dtHoraEntrada = this.dtpHoraEntrada.Value;
                this.dTiempoSecado = -1;

                this.dtpHoraEntrada.Enabled = false;
                this.txTiempoSecado.Enabled = false;

                this.txCodCarro.Focus();
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

        #region txCodCarro_KeyPress
        private void txCodCarro_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                // Validar ingreso del Enter.
                if (e.KeyChar == 13)
                {
                    TextBox txObj = (TextBox)sender;
                    string sCodCarro = txObj.Text;
                    Validacion val = null;

                    val = this.ValidarCarro(this.lu.CodPlanta, this.iCodProcesoAnt, sCodCarro);
                    this.encabezado.Mensaje = val.MensajeValidacion;

                    if (!val.ValidacionExitosa)
                    {
                        txObj.SelectAll();
                        txObj.Focus();
                    }
                    else
                    {
                        txObj.Enabled = false;
                        this.dtpHoraEntrada.Enabled = true;
                        this.txTiempoSecado.Enabled = true;
                        this.dtpHoraEntrada.Focus();
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
        #endregion txCodCarro_KeyPress
        #region dtpHoraEntrada_ValueChanged
        private void dtpHoraEntrada_ValueChanged(object sender, EventArgs e)
        {
            try
            {
                DateTimePicker dtpObj = (DateTimePicker)sender;
                this.dtHoraEntrada = dtpObj.Value;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
            }
        }
        #endregion dtpHoraEntrada_ValueChanged
        #region dtpHoraEntrada_KeyPress
        private void dtpHoraEntrada_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                // Validar ingreso del Enter.
                if (e.KeyChar == 13)
                {
                    DateTimePicker dtpObj = (DateTimePicker)sender;
                    this.dtHoraEntrada = dtpObj.Value;

                    this.txTiempoSecado.SelectAll();
                    this.txTiempoSecado.Focus();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
            }
        }
        #endregion dtpHoraEntrada_KeyPress
        #region txTiempoSecado_KeyPress
        private void txTiempoSecado_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                // Validar ingreso del Enter.
                if (e.KeyChar == 13)
                {
                    TextBox txObj = (TextBox)sender;
                    string sTiempoSecado = txObj.Text;
                    
                    Validacion val = this.ValidarTiempoSecado(sTiempoSecado);
                    this.encabezado.Mensaje = val.MensajeValidacion;

                    if (!val.ValidacionExitosa)
                    {
                        txObj.SelectAll();
                        txObj.Focus();
                    }
                    else
                    {
                        this.btAceptar.Focus();
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
        #endregion txTiempoSecado_KeyPress

        #region btAceptar_Click
        private void btAceptar_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.iCodCarro == -1)
                {
                    this.encabezado.Mensaje = "Capture número de Carro";
                    this.txCodCarro.SelectAll();
                    this.txCodCarro.Focus();
                    return;
                }
                if (this.dtHoraEntrada == DateTime.MinValue)
                {
                    this.encabezado.Mensaje = "Capture Hora de Entrada";
                    this.dtpHoraEntrada.Focus();
                    return;
                }
                if (this.dTiempoSecado == -1)
                {
                    this.encabezado.Mensaje = "Capture Tiempo de Secado";
                    this.txTiempoSecado.SelectAll();
                    this.txTiempoSecado.Focus();
                    return;
                }

                this.encabezado.Mensaje = String.Empty;

                // Registrar la transaccion de las piezas del Carro.
                long lCodPiezaTransaccion = -1;
                int iCodPieza = -1;
                foreach (DataRow dr in this.dtCarroPiezas.Rows)
                {
                    iCodPieza = Convert.ToInt32(dr["CodPieza"]);
                    
                    lCodPiezaTransaccion = this.oDA0.InsertarPiezaTransaccion(  DA.eTipoConexion.Local, 
                                                                                this.lu.CodConfigHandHeld, 
                                                                                iCodPieza,
                                                                                this.lu.Fecha);

                    this.oDA0.ActulizarUltimoProcesoPieza(DA.eTipoConexion.Local, iCodPieza, this.lu.CodProceso);
                }

                // Registrar Hora de Entrada y Tiempo de Secado.
                this.oDA.InsertarPiezaTransaccionSecador(DA.eTipoConexion.Local, lCodPiezaTransaccion, this.dtHoraEntrada, this.dTiempoSecado);

                // Eliminar el Carro de Secado.
                //this.oDA0.EliminarCarro(DA.eTipoConexion.Local, this.lu.CodPlanta, this.iCodProcesoAnt, this.iCodCarro);
                this.oDA0.EliminarCarroTemp(this.lu.CodPlanta, this.iCodProcesoAnt, this.iCodCarro);

                this.txCodCarro.Text = String.Empty;
                this.txCodCarro.Enabled = true;
                this.dtpHoraEntrada.Value = this.dtHoraInicial;
                this.dtpHoraEntrada.Enabled = false;
                this.txTiempoSecado.Text = String.Empty;
                this.txTiempoSecado.Enabled = false;

                this.iCodCarro = -1;
                this.dtHoraEntrada = this.dtpHoraEntrada.Value;
                this.dTiempoSecado = -1;

                this.txCodCarro.Focus();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
            }
        }
        #endregion btAceptar_Click
        #region btTerminar_Click
        private void btTerminar_Click(object sender, EventArgs e)
        {
            try
            {
                this.EnviarDatosAlServidor();

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

        #endregion event handlers

        #endregion methods

    }
}