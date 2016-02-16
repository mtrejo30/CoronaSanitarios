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
    public partial class a11_ArmadoTarima : Form
    {

        #region fields
        private LoginUsuario lu = null;
        private c00_Common oDA0 = new c00_Common();
        private c11_ArmadoTarima oDA = new c11_ArmadoTarima();
        private c12_CapturaAuditoria oDA1 = new c12_CapturaAuditoria();
        private Timer trActualizarDatosServidor = new Timer();
        private int iPeriodoActualizacion = -1;

        private int iCodTarima = -1;
        private int iCodPieza = -1;
        private int iCodEstadoPieza = -1;
        private string sDesEstadoPieza = string.Empty;
        private int iCodUltimoProcesoPieza = -1;
        private string sDesUltimoProcesoPieza = string.Empty;
        private int iCodProcesoAct = -1;
        private static int iTarimaInsertada = -1;
        private static int iPiezaTarimaInsertada = -1;
        private static Boolean bInsertarPza = true;


        #endregion fields

        #region properties



        #endregion properties

        #region methods

        #region constructors and destructor

        public a11_ArmadoTarima(LoginUsuario lu)
        {
            InitializeComponent();
            //
            this.lu = lu;
            this.ConfigurarFormulario();
            this.ConfigurarPanelControles();
        }
        ~a11_ArmadoTarima()
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

            this.txEtiqueta.Enabled = false;
            this.txEtiqueta.ReadOnly = true;

            this.txTipo.ReadOnly = true;
            this.txTipo.Enabled = false;
            this.txModelo.ReadOnly = true;
            this.txModelo.Enabled = false;
            this.txColor.ReadOnly = true;
            this.txColor.Enabled = false;
            this.txCalidad.ReadOnly = true;
            this.txCalidad.Enabled = false;

            this.txTarima.KeyPress += new KeyPressEventHandler(this.txTarima_KeyPress);
            this.txEtiqueta.KeyPress += new KeyPressEventHandler(this.txEtiqueta_KeyPress);

            this.btAceptar.Click += new EventHandler(this.btAceptar_Click);
            this.btCancelar.Click += new EventHandler(this.btCancelar_Click);
            this.btTerminar.Click += new EventHandler(this.btTerminar_Click);

            // Configuracion del Timer.
            //this.trActualizarDatosServidor.Interval = 100;
            //this.trActualizarDatosServidor.Enabled = true;
            //this.trActualizarDatosServidor.Tick += new EventHandler(this.trActualizarDatosServidor_Tick);
        }
        #endregion ConfigurarPanelControles

        #region ValidarTarima
        private Validacion ValidarTarima(string sCodTarima)
        {
            Validacion val = new Validacion();

            // Validar el codigo del carro no sea una cadena nula o vacia.
            if (string.IsNullOrEmpty(sCodTarima))
            {
                val.ValidacionExitosa = false;
                val.MensajeValidacion = "Capture número de Tarima";

                this.iCodTarima = -1;
                return val;
            }
            val = oDA0.ValidarEntero(sCodTarima);
            if (!val.ValidacionExitosa)
                return val;
            this.iCodTarima = Convert.ToInt32(sCodTarima);

            val.ValidacionExitosa = true;
            val.MensajeValidacion = "Capture Piezas";
            return val;

        }
        #endregion ValidarTarima
        #region ValidarPieza
        private Validacion ValidarPieza(string sCodBarras)
        {
            Validacion val = new Validacion();
            DataTable dtObj = null;

            // Validar el codigo de barras no sea una cadena nula o vacia.
            if (string.IsNullOrEmpty(sCodBarras))
            {
                val.ValidacionExitosa = false;
                val.MensajeValidacion = "Capture Etiqueta";

                this.iCodPieza = -1;
                return val;
            }

            // Validar exista la pieza.
            this.iCodPieza = this.oDA0.ObtenerCodPiezaCodBarras(sCodBarras, false);
            if (this.iCodPieza == -1)
            {
                val.ValidacionExitosa = false;
                val.MensajeValidacion = "Pieza no existe";

                return val;
            }

            // Obtener el estado de la pieza.
            dtObj = this.oDA0.ObtenerEstadoPieza(this.iCodPieza, false);
            this.iCodEstadoPieza = Convert.ToInt32(dtObj.Rows[0]["CodEstadoPieza"]);
            this.sDesEstadoPieza = Convert.ToString(dtObj.Rows[0]["DesEstadoPieza"]);

            // Si la pieza esta 'En Reparacion' o 'En Desperdicio'.
            if (this.iCodEstadoPieza == 2 || this.iCodEstadoPieza == 4)
            {
                val.ValidacionExitosa = false;
                val.MensajeValidacion = "Pieza " + this.sDesEstadoPieza;

                this.iCodPieza = -1;
                this.iCodEstadoPieza = -1;
                this.sDesEstadoPieza = string.Empty;
                return val;
            }

            // Obtener el ultimo proceso de la pieza.
            dtObj = this.oDA0.ObtenerUltimoProcesoPieza(this.iCodPieza, false);
            this.iCodUltimoProcesoPieza = Convert.ToInt32(dtObj.Rows[0]["CodProceso"]);
            this.sDesUltimoProcesoPieza = Convert.ToString(dtObj.Rows[0]["DesProceso"]);
            dtObj = this.oDA0.ObtenerCalidadPieza(this.iCodPieza);
            string sCalidadPieza = string.Empty;
            if (dtObj.Rows.Count > 0)
                sCalidadPieza = Convert.ToString(dtObj.Rows[0]["DesCalidad"]);
            // Validar que la pieza solo este en el proceso de Empaque.
            if ((this.iCodUltimoProcesoPieza != this.iCodProcesoAct && iCodUltimoProcesoPieza != oDA0.ObtenerCodProcesoAuditoria()) || string.IsNullOrEmpty(sCalidadPieza))
            {
                val.ValidacionExitosa = false;
                if (string.IsNullOrEmpty(sCalidadPieza))
                    val.MensajeValidacion = "La pieza no tiene asignada calidad\n(pieza en proceso " + this.sDesUltimoProcesoPieza + ").";
                else
                    val.MensajeValidacion = "Pieza en proceso: " + this.sDesUltimoProcesoPieza;

                this.iCodPieza = -1;
                this.iCodEstadoPieza = -1;
                this.sDesEstadoPieza = string.Empty;
                this.iCodUltimoProcesoPieza = -1;
                this.sDesUltimoProcesoPieza = string.Empty;
                return val;
            }

            val.ValidacionExitosa = true;
            val.MensajeValidacion = string.Empty;
            return val;
        }
        #endregion ValidarPieza
        #region ValidarNoExistePiezaEnTarima
        private Validacion ValidarTarimaPieza(int iCodPieza)
        {
            bInsertarPza = true;
            Validacion val = new Validacion();
            int iCodTarima = this.oDA.ExistePiezaEnTarima(iCodPieza);
            if (iCodTarima == -1)
            {
                val.ValidacionExitosa = true;
                val.MensajeValidacion = string.Empty;
            }
            else
            {
                val.ValidacionExitosa = false;
                val.MensajeValidacion = "Pieza asignada a la Tarima: " + iCodTarima.ToString();
            }
            int iTarima = Convert.ToInt32(txTarima.Text);
            int iCodTarimaPadre = oDA.ObtenerPiezaEnTarima(iTarima);
            if (val.ValidacionExitosa)
            {
                ValidacionPieza valPza = this.oDA0.ValidarTarimaPieza(iTarima, iCodPieza, iCodTarimaPadre);
                if (!valPza.MensajeValidacion.ToString().Trim().Equals("OK"))
                {
                    val.ValidacionExitosa = false;
                    val.MensajeValidacion = valPza.MensajeValidacion;

                }
                else
                {
                    val.ValidacionExitosa = true;
                    val.MensajeValidacion = string.Empty;
                    if (valPza.ValNoDefDespExitosa)
                    {
                        bInsertarPza = false;
                        iTarimaInsertada = iTarima;
                        iPiezaTarimaInsertada = iCodPieza;
                    }
                }
            }
            return val;
        }
        #endregion ValidarNoExistePiezaEnTarima
        #region ValidarNoExistePiezaEnTarima
        private Validacion ValidarPiezaEnTarima(int iCodPieza)
        {
            Validacion val = new Validacion();
            int iCodTarima = this.oDA.ExistePiezaEnTarima(iCodPieza);
            if (iCodTarima == -1)
            {
                val.ValidacionExitosa = true;
                val.MensajeValidacion = string.Empty;
            }
            else
            {
                val.ValidacionExitosa = false;
                val.MensajeValidacion = "Pieza asignada a la Tarima: " + iCodTarima.ToString();
            }

            return val;
        }
        #endregion
        #region EnviarDatosAlServidor
        private void EnviarDatosAlServidor()
        {
            this.encabezado.Mensaje = "Enviando datos...";
            this.Refresh();

            c00_Transacciones ct = new c00_Transacciones();
            bool bEnvioExitoso = ct.EnviarDatosEmpaque();
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
                this.encabezado.Titulo = "Armado Tarima";
                // Obtener periodo del timer.
                this.encabezado.Conexion = EstadoConexion.Offline;
                this.iPeriodoActualizacion = this.oDA0.ObtenerPeriodoActualizacion(this.lu.CodProceso, this.lu.CodPantalla);
                this.iCodProcesoAct = this.lu.CodProceso;
                this.txTarima.Focus();
                this.btnDesAsignar.Enabled = false;
                //if (this.lu.CodPlanta != 4)
                //{
                    this.btCancelar.Enabled = false;
                    this.btTerminar.Enabled = false;
                //}
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

        #region txTarima_KeyPress
        private void txTarima_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                // Validar ingreso del Enter.
                if (e.KeyChar == 13)
                {
                    TextBox txObj = (TextBox)sender;
                    string sCodTarima = txObj.Text;
                    Validacion val = null;
                    val = this.ValidarTarima(sCodTarima);
                    this.encabezado.Mensaje = val.MensajeValidacion;
                    //if (val.ValidacionExitosa && this.lu.CodPlanta != 4)
                    if (val.ValidacionExitosa)
                        if (!EsTarimaValida(iCodTarima))
                            val.ValidacionExitosa = false;
                    if (!val.ValidacionExitosa)
                    {
                        txObj.SelectAll();
                        txObj.Focus();
                    }
                    else
                    {
                        txObj.Enabled = false;
                        txObj.ReadOnly = true;
                        this.txEtiqueta.Enabled = true;
                        this.txEtiqueta.ReadOnly = false;
                        this.txEtiqueta.SelectAll();
                        this.txEtiqueta.Focus();
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
        #endregion txTarima_KeyPress
        #region txEtiqueta_KeyPress
        private void txEtiqueta_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                // Validar ingreso del Enter.
                if (e.KeyChar == 13)
                {
                    this.txCalidad.Text = string.Empty;
                    this.txColor.Text = string.Empty;
                    this.txTipo.Text = string.Empty;
                    this.txModelo.Text = string.Empty;
                    TextBox txObj = (TextBox)sender;
                    string sCodBarrasPieza = txObj.Text;
                    Validacion val = null;
                    this.encabezado.Mensaje = "ValidarPieza";
                    val = this.ValidarPieza(sCodBarrasPieza);
                    this.encabezado.Mensaje = val.MensajeValidacion;
                    if (!val.ValidacionExitosa)
                    {
                        txObj.SelectAll();
                        txObj.Focus();
                        return;
                    }
                    this.encabezado.Mensaje = "ValidarTarimaPieza";
                    val = ValidarTarimaPieza(this.iCodPieza);
                    this.encabezado.Mensaje = val.MensajeValidacion;
                    if (!val.ValidacionExitosa)
                    {
                        //if (this.lu.CodPlanta != 4)
                            this.btnDesAsignar.Enabled = true;
                        txObj.SelectAll();
                        txObj.Focus();
                        return;
                    }
                    this.encabezado.Mensaje = String.Empty;
                    DataTable dtObj = null;
                    // Obtener el Modelo y Tipo de la pieza.
                    int iCodArticulo = this.oDA0.ObtenerCodModeloPieza(this.iCodPieza);
                    dtObj = this.oDA0.ObtenerModeloTipoPieza(iCodArticulo);
                    this.txTipo.Text = Convert.ToString(dtObj.Rows[0]["DesTipoModelo"]);
                    this.txModelo.Text = Convert.ToString(dtObj.Rows[0]["DesModelo"]);
                    // Obtener el Color de la pieza.
                    dtObj = this.oDA0.ObtenerColorPieza(this.iCodPieza);
                    if (dtObj.Rows.Count > 0)
                        this.txColor.Text = Convert.ToString(dtObj.Rows[0]["DesColor"]);
                    else
                        this.txColor.Text = string.Empty;
                    // Obtener la Calidad de la pieza.
                    dtObj = this.oDA0.ObtenerCalidadPieza(this.iCodPieza);
                    if (dtObj.Rows.Count > 0)
                        this.txCalidad.Text = Convert.ToString(dtObj.Rows[0]["DesCalidad"]);
                    else
                        this.txCalidad.Text = string.Empty;
                    // Asociar Pieza con la Tarima.
                    this.oDA.InsertarTarimaPieza(DA.eTipoConexion.Local, this.iCodTarima, this.iCodPieza);
                    if (this.iCodPieza == iPiezaTarimaInsertada)
                        new c00_Transacciones().EstablecerActualizacionTarimaPieza(this.iCodTarima, iPiezaTarimaInsertada,1,0);
                    this.iCodPieza = -1;
                    txObj.Text = string.Empty;
                    txObj.Focus();
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

        #region btAceptar_Click
        private void btAceptar_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.iCodTarima == -1)
                {
                    this.encabezado.Mensaje = "Capture número de tarima";
                    this.txTarima.SelectAll();
                    this.txTarima.Focus();
                    return;
                }
                //if (this.lu.CodPlanta != 4)
                //{
                    int iCodigoTarima = EnTarimarPieza(Convert.ToInt32(this.txTarima.Text), this.txEtiqueta.Text, this.lu.CodMaquina, this.lu.CodCentroTrabajo);
                    this.txTarima.Text = iCodTarima.ToString();
                    this.encabezado.Titulo = "Pieza entarimada, tarima: " + iCodigoTarima.ToString();
                    ResetearCaptura();
                    return;
                //}
                //DataTable dtRes = this.oDA0.ObtenerPiezasTarima(this.iCodTarima, true);
                //if (dtRes.Rows.Count == 0)
                //{
                //    this.encabezado.Mensaje = "Tarima sin piezas registradas";
                //    this.txEtiqueta.Text = string.Empty;
                //    this.txEtiqueta.Focus();
                //}
                //else
                //{
                //    this.iCodTarima = -1;
                //    this.encabezado.Mensaje = "Armado de tarima completado";
                //    oDA0.LimpiarControl(this);
                //    this.txEtiqueta.Enabled = false;
                //    this.txEtiqueta.ReadOnly = true;
                //    this.txTarima.Enabled = true;
                //    this.txTarima.ReadOnly = false;
                //    this.txTarima.Text = string.Empty;
                //    this.txEtiqueta.Text = string.Empty;
                //    this.txTarima.Focus();
                //}
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
            }
        }
        #endregion btAceptar_Click
        #region btCancelar_Click
        private void btCancelar_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.iCodTarima != -1)
                {
                    DialogResult dr = MessageBox.Show("¿Cancelar armado de Tarima?", "SCPP", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
                    if (dr == DialogResult.Yes)
                    {
                        // Borrar la Tarima armada.
                        this.oDA.EliminarTarima(DA.eTipoConexion.Local, this.iCodTarima, iPiezaTarimaInsertada);
                        this.iCodTarima = -1;
                        this.encabezado.Mensaje = String.Empty;
                        oDA0.LimpiarControl(this);
                        this.txTarima.Enabled = true;
                        this.txTarima.ReadOnly = false;
                        this.txEtiqueta.Enabled = false;
                        this.txEtiqueta.ReadOnly = true;
                        this.txTarima.Text = string.Empty;
                        this.txEtiqueta.Text = string.Empty;
                        this.txTarima.Focus();
                    }
                    else
                    {
                        this.txEtiqueta.Text = string.Empty;
                        this.txEtiqueta.Focus();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
            }
        }
        #endregion btCancelar_Click
        #region btTerminar_Click
        private void btTerminar_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.iCodTarima != -1)
                {
                    MessageBox.Show("Tarima abierta todavia", "SCPP", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
                    this.txEtiqueta.Text = string.Empty;
                    this.txEtiqueta.Focus();
                }
                else
                {
                    this.EnviarDatosAlServidor();
                    //Regresar a Configuracion Inicial.
                    //a03_ConfiguracionInicial frmObj = new a03_ConfiguracionInicial(this.lu);
                    //frmObj.Show();
                    //this.Close();
                    oDA0.LimpiarControl(this);
                    this.iCodTarima = -1;
                    this.encabezado.Mensaje = String.Empty;
                    this.txTarima.Enabled = true;
                    this.txTarima.ReadOnly = false;
                    this.txEtiqueta.Enabled = false;
                    this.txEtiqueta.ReadOnly = true;
                    this.txTarima.Focus();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
            }
        }
        #endregion btTerminar_Click

        private void btnMenu_Click(object sender, EventArgs e)
        {
            btCancelar_Click(null, null);

            //Regresar a Configuracion Inicial.
            if (this.iCodTarima == -1)
            {
                a03_ConfiguracionInicial frmObj = new a03_ConfiguracionInicial(this.lu);
                frmObj.Show();
                this.Close();
            }
        }

        #endregion event handlers
        private void Paletizar()
        {
            /*
            this.oDA1.ActualizarTarimaPaletizado(this.iCodTarima, true);
            int iCodPieza = -1;
            foreach (DataRow dr in this.dtPiezasTarima.Rows)
            {
                iCodPieza = Convert.ToInt32(dr["CodPieza"]);
                long lCodPiezaTransaccion = this.oDA0.InsertarPiezaTransaccion(DA.eTipoConexion.Local, this.lu.CodConfigHandHeld, iCodPieza, this.lu.Fecha);
                this.oDA1.ActualizarPiezaAuditada(DA.eTipoConexion.Local, iCodPieza, false);
                this.oDA0.ActulizarUltimoProcesoPieza(DA.eTipoConexion.Local, iCodPieza, this.iCodProcesoAct);
            }
            */ 
        }
        #endregion methods
        private bool EsTarimaValida(int iCodigoTarima)
        {
            if (iCodigoTarima == 0) return false;
            HHsvc.SCPP_HH proxy = null;
            bool bResult = false, bResultSpecified = true;
            try
            {
                proxy = new HHsvc.SCPP_HH();
                proxy.EsTarimaValida(iCodigoTarima, true, out bResult, out bResultSpecified);
                return bResult;
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("Tarima cerrada.") || ex.Message.Contains("La tarima ya tiene su capacidad maxima de piezas"))
                { 
                    DialogResult result = MessageBox.Show(ex.Message + "\n ¿Desea entarimar piezas?", "Armado de Tarima", MessageBoxButtons.YesNo, MessageBoxIcon.None, MessageBoxDefaultButton.Button1); 
                    bResultSpecified = true;
                    bResult = false;
                    proxy = new HHsvc.SCPP_HH();
                    if (result == DialogResult.Yes)
                        proxy.AbrirTarima(iCodigoTarima, true, out bResult, out bResultSpecified);
                    return (bResult) ? true : false;
                }
                else
                    MessageBox.Show(ex.Message, "Armado de Tarima", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
                return false;
            }
            finally
            {
                proxy = null;
            }
        }
        private bool DesEntarimar(int iCodigoPieza)
        {
            HHsvc.SCPP_HH proxy = null;
            bool bResult = false, bResultSpecified = true;
            try
            {
                proxy = new HHsvc.SCPP_HH();
                proxy.DesEnTarimar(iCodPieza, true, out bResult, out bResultSpecified);
                return bResult;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Desentarimar pieza.", MessageBoxButtons.OK, MessageBoxIcon.Asterisk, MessageBoxDefaultButton.Button1);
                return false;
            }
        }
        private void btnDesAsignar_Click(object sender, EventArgs e)
        {
            if(this.iCodPieza <= 0)
            {
                MessageBox.Show("Capture etiqueta.", "Desentarimar pieza.", MessageBoxButtons.OK, MessageBoxIcon.Asterisk, MessageBoxDefaultButton.Button1);
                return;
            }
            if (DesEntarimar(this.iCodPieza))
                MessageBox.Show("La pieza se desasigno de la tarima.", "Desentarimar pieza.", MessageBoxButtons.OK, MessageBoxIcon.None, MessageBoxDefaultButton.Button1);
        }
        private void ResetearCaptura()
        {
            oDA0.LimpiarControl(this);
            this.iCodTarima = -1;
            this.encabezado.Mensaje = String.Empty;
            this.txTarima.Enabled = true;
            this.txTarima.ReadOnly = false;
            this.txEtiqueta.Enabled = false;
            this.txEtiqueta.ReadOnly = true;
            this.txTarima.Focus();
            this.btnDesAsignar.Enabled = false;
        }
        private int EnTarimarPieza(int iCodigoTarima, string sCodigoBarraPieza, int iCodigoMaquina, int iCodigoCentroTrabajo)
        {
            if (string.IsNullOrEmpty(sCodigoBarraPieza)) return 0;
            HHsvc.SCPP_HH proxy = null;
            try
            {
                int iResultTarima = 0;
                bool bResultTarima = true;
                proxy = new HHsvc.SCPP_HH();
                proxy.EnTarimarPieza(sCodigoBarraPieza, iCodigoMaquina, true, iCodigoCentroTrabajo, true, false, true, out iResultTarima, out bResultTarima);
                return iResultTarima;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
                return 0;
            }
            finally
            {
                proxy = null;
            }
        }
    }
}