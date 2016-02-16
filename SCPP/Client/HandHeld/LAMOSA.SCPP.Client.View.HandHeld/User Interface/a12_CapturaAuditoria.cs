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
using LAMOSA.SCPP.Client.View.HandHeld.DataAccess;
namespace LAMOSA.SCPP.Client.View.HandHeld
{
    public partial class a12_CapturaAuditoria : Form
    {

        #region fields

        private LoginUsuario lu = null;
        private c00_Common oDA0 = new c00_Common();
        private c12_CapturaAuditoria oDA = new c12_CapturaAuditoria();

        private Timer trActualizarDatosServidor = new Timer();
        private int iPeriodoActualizacion = -1;

        private eTipoAuditoria TipoAuditoria = eTipoAuditoria.Indeterminado;
        private int iCodProcesoAct = -1;
        private int iCodProcesoAnt = -1;
        private int iCodTarima = -1;
        private int iCodPieza = -1;
        private int iCodArticulo = -1;
        private int iCodEstadoPieza = -1;
        private string sDesEstadoPieza = string.Empty;
        private int iCodUltimoProcesoPieza = -1;
        private string sDesUltimoProcesoPieza = string.Empty;

        private DataTable dtPiezasTarima = null;
        private frmProduccionOperador frmProduccion;
        #endregion fields

        #region properties
        public frmProduccionOperador FormaProduccion { get { return frmProduccion; } set { frmProduccion = value; } }


        #endregion properties

        #region methods

        #region constructors and destructor

        public a12_CapturaAuditoria(eTipoAuditoria TipoAuditoria, LoginUsuario lu)
        {
            InitializeComponent();
            //
            this.lu = lu;
            this.TipoAuditoria = TipoAuditoria;
            this.ConfigurarFormulario();
            this.ConfigurarPanelControles();
        }
        ~a12_CapturaAuditoria()
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

            this.txTarima.ReadOnly = true;
            this.txTarima.Enabled = false;
            this.txEtiqueta.ReadOnly = true;
            this.txEtiqueta.Enabled = false;
            this.txTipo.ReadOnly = true;
            this.txTipo.Enabled = false;
            this.txModelo.ReadOnly = true;
            this.txModelo.Enabled = false;
            this.txColor.ReadOnly = true;
            this.txColor.Enabled = false;
            this.txCalidad.ReadOnly = true;
            this.txCalidad.Enabled = false;

            this.btAceptar.Enabled = false;
            this.btRechazar.Enabled = false;
            this.btPaletizar.Enabled = false;
            this.btCancelar.Enabled = false;
            this.btTerminar.Enabled = false;

            this.txTarima.KeyPress += new KeyPressEventHandler(this.txTarima_KeyPress);
            this.txEtiqueta.KeyPress += new KeyPressEventHandler(this.txEtiqueta_KeyPress);

            this.btAceptar.Click += new EventHandler(this.btAceptar_Click);
            this.btRechazar.Click += new EventHandler(this.btRechazar_Click);
            this.btPaletizar.Click += new EventHandler(this.btPaletizar_Click);
            this.btCancelar.Click += new EventHandler(this.btCancelar_Click);
            this.btTerminar.Click += new EventHandler(this.btTerminar_Click);

            //// Configuracion del Timer.
            //this.trActualizarDatosServidor.Interval = 100;
            //this.trActualizarDatosServidor.Enabled = true;
            //this.trActualizarDatosServidor.Tick += new EventHandler(this.trActualizarDatosServidor_Tick);
        }
        #endregion ConfigurarPanelControles

        #region ExistePiezaEnTarima
        private bool ExistePiezaEnTarima(int iCodPieza)
        {
            bool bExistePiezaEnTarima = false;
            foreach (DataRow dr in this.dtPiezasTarima.Rows)
            {
                if (Convert.ToInt32(dr["CodPieza"]) == iCodPieza)
                {
                    bExistePiezaEnTarima = true;
                    break;
                }
            }
            return bExistePiezaEnTarima;
        }
        #endregion ExistePiezaEnTarima
        #region ValidarTarima
        private Validacion ValidarTarima(string sCodTarima)
        {
            Validacion val = new Validacion();
            DataTable dtObjSvr = null;

            // Validar el codigo de tarima no sea una cadena nula o vacia.
            if (string.IsNullOrEmpty(sCodTarima))
            {
                val.ValidacionExitosa = false;
                val.MensajeValidacion = "Capture Tarima";

                this.iCodTarima = -1;
                return val;
            }

            this.iCodTarima = Convert.ToInt32(sCodTarima);

            // Validar exista la tarima, tanto en servidor como en local.
            dtObjSvr = this.oDA0.ObtenerPiezasTarima(this.iCodTarima, false);

            if (dtObjSvr.Rows.Count > 0)
            {

                this.dtPiezasTarima = dtObjSvr;
                this.dtPiezasTarima.Columns.Add("Estado", typeof(int));
                this.dtPiezasTarima.Columns["Estado"].ReadOnly = false;
                foreach (DataRow dr in this.dtPiezasTarima.Rows)
                {
                    dr["Estado"] = -1;
                }
                val.ValidacionExitosa = true;
                val.MensajeValidacion = String.Empty;
            }
            else
            {
                val.ValidacionExitosa = false;
                val.MensajeValidacion = "Tarima no existe";

                this.iCodTarima = -1;
                this.dtPiezasTarima = null;
                return val;
            }
            return val;
        }
        #endregion ValidarTarima
        #region ValidarPieza
        private Validacion ValidarPieza(string sCodBarras)
        {
            Validacion val = new Validacion();
            DataTable dtObj = null;
            int iCodUltimioProceso = -1;
            string sDescUltimoProceso = string.Empty;
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
            oDA0.ObtenerUltimoProcesoPieza(this.iCodPieza, true, out iCodUltimioProceso, out sDescUltimoProceso);
            if (this.lu.CodProceso == iCodUltimioProceso)
            {
                val.MensajeValidacion = "Pieza Recien Procesada";
                val.ValidacionExitosa = false;
                return val;
            }
            else if(dtPiezasTarima != null && dtPiezasTarima.Rows.Count > 0){
                DataRow[] dr = dtPiezasTarima.Select("CodPieza = " + this.iCodPieza + " and Estado = 1");
                if (dr != null && dr.Length > 0) {
                    val.MensajeValidacion = "Pieza Recien Procesada";
                    val.ValidacionExitosa = false;
                    return val;
                }
            }


            // Obtener el estado de la pieza.
            dtObj = this.oDA0.ObtenerEstadoPieza(this.iCodPieza, false);
            this.iCodEstadoPieza = Convert.ToInt32(dtObj.Rows[0]["CodEstadoPieza"]);
            this.sDesEstadoPieza = Convert.ToString(dtObj.Rows[0]["DesEstadoPieza"]);

            if (this.iCodEstadoPieza == 2 || this.iCodEstadoPieza == 4) //En Reparacion o En Desperdicio.
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

            int iCodProcesoClasificado = this.oDA0.ObtenerCodProcesoClasificado();
            string sCalidad = Convert.ToString(dtObj.Rows[0]["Calidad"]);
            int iCodProcesoAnt = this.oDA0.ObtenerProcesoAnterior(this.iCodUltimoProcesoPieza);
            ////--Validar que la pieza este en clasificado tenga Calidad de Clasificado de Requeme y el proceso que solicita es Hornos
            //if (iCodUltimoProcesoPieza == iCodProcesoClasificado & sCalidad.ToLower() == "Requeme".ToLower() & iCodProcesoAnt == 5 & this.lu.CodProceso == 5)
            //{
            //    val.ValidacionExitosa = true;
            //    val.MensajeValidacion = string.Empty;
            //    return val;
            //}
            //--Validar que la pieza este en clasificado pero no tenga calidad de requeme para los procesos de empaque y auditoria
            if (iCodUltimoProcesoPieza == iCodProcesoClasificado & sCalidad.ToLower() == "Requeme".ToLower() & (this.lu.CodProceso == 7 | this.lu.CodProceso == 8))
            {
                val.ValidacionExitosa = false;
                val.MensajeValidacion = "Pieza " + sDesEstadoPieza + " con calidad " + sCalidad;
                return val;
            }

            if (this.TipoAuditoria == eTipoAuditoria.Al100PorCiento)
            {
                // Validar que la pieza solo haya pasado por el proceso 'Clasificacion' y 'Empaque'.
                if (this.iCodUltimoProcesoPieza == this.oDA0.ObtenerCodProcesoClasificado() | this.iCodUltimoProcesoPieza == this.oDA0.ObtenerCodProcesoEmpaque())
                {
                    val.ValidacionExitosa = true;
                    val.MensajeValidacion = String.Empty;
                }
                else
                {
                    val.ValidacionExitosa = false;
                    val.MensajeValidacion = "Pieza en proceso: " + this.sDesUltimoProcesoPieza;

                    this.iCodPieza = -1;
                    this.iCodEstadoPieza = -1;
                    this.sDesEstadoPieza = string.Empty;
                    this.iCodUltimoProcesoPieza = -1;
                    this.sDesUltimoProcesoPieza = string.Empty;
                }
            }
            else if (this.TipoAuditoria == eTipoAuditoria.PorMuestreo)
            {
                // Validar que la pieza solo haya pasado por el proceso 'Empaque'.
                if (this.iCodUltimoProcesoPieza == 7)
                {
                    // Validar que la pieza este asignada a la tarima capturada.
                    if (this.ExistePiezaEnTarima(this.iCodPieza))
                    {
                        val.ValidacionExitosa = true;
                        val.MensajeValidacion = String.Empty;
                    }
                    else
                    {
                        val.ValidacionExitosa = false;
                        val.MensajeValidacion = "Pieza no asignada a tarima";

                        this.iCodPieza = -1;
                        this.iCodEstadoPieza = -1;
                        this.sDesEstadoPieza = string.Empty;
                        this.iCodUltimoProcesoPieza = -1;
                        this.sDesUltimoProcesoPieza = string.Empty;
                    }
                }
                else
                {
                    val.ValidacionExitosa = false;
                    val.MensajeValidacion = "Pieza en proceso: " + this.sDesUltimoProcesoPieza;

                    this.iCodPieza = -1;
                    this.iCodEstadoPieza = -1;
                    this.sDesEstadoPieza = string.Empty;
                    this.iCodUltimoProcesoPieza = -1;
                    this.sDesUltimoProcesoPieza = string.Empty;
                }
            }
            return val;
        }
        #endregion ValidarPieza
        #region EstablecerPiezaAceptada
        private void EstablecerPiezaAceptada(int iCodPieza)
        {
            foreach (DataRow dr in this.dtPiezasTarima.Rows)
            {
                if (Convert.ToInt32(dr["CodPieza"]) == iCodPieza)
                {
                    dr["Estado"] = 1;
                    break;
                }
            }
        }
        #endregion EstablecerPiezaAceptada
        #region EstablecerPiezaRechazada
        private void EstablecerPiezaRechazada(int iCodPieza)
        {
            foreach (DataRow dr in this.dtPiezasTarima.Rows)
            {
                if (Convert.ToInt32(dr["CodPieza"]) == iCodPieza)
                {
                    dr["Estado"] = 2;
                    break;
                }
            }
        }
        #endregion EstablecerPiezaRechazada
        #region ExisteUnaPiezaRechazada
        private bool ExisteUnaPiezaRechazada()
        {
            bool bExisteUnaPiezaRechazada = false;

            foreach (DataRow dr in this.dtPiezasTarima.Rows)
            {
                if (Convert.ToInt32(dr["Estado"]) == 2)
                {
                    bExisteUnaPiezaRechazada = true;
                    break;
                }
            }

            return bExisteUnaPiezaRechazada;
        }
        #endregion ExisteUnaPiezaRechazada
        #region ExisteUnaPiezaAuditada
        private bool ExisteUnaPiezaAuditada()
        {
            bool bExisteUnaPiezaAuditada = false;

            foreach (DataRow dr in this.dtPiezasTarima.Rows)
            {
                if (Convert.ToBoolean(dr["Auditada"]) == true)
                {
                    bExisteUnaPiezaAuditada = true;
                    break;
                }
            }

            return bExisteUnaPiezaAuditada;
        }
        #endregion ExisteUnaPiezaAuditada
        #region ExisteUnaPiezaAceptada
        private bool ExisteUnaPiezaAceptada()
        {
            bool bExisteUnaPiezaAceptada = false;

            foreach (DataRow dr in this.dtPiezasTarima.Rows)
            {
                if (Convert.ToInt32(dr["Estado"]) == 1)
                {
                    bExisteUnaPiezaAceptada = true;
                    break;
                }
            }

            return bExisteUnaPiezaAceptada;
        }
        #endregion ExisteUnaPiezaRechazada
        #region EnviarDatosAlServidor
        private void EnviarDatosAlServidor()
        {
            this.encabezado.Mensaje = "Enviando datos...";
            this.Refresh();

            c00_Transacciones ct = new c00_Transacciones();
            bool bEnvioExitoso = ct.EnviarDatosAuditoria();
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
                this.encabezado.Titulo = "Captura " + this.lu.DesProceso;

                // Obtener periodo del timer.
                this.encabezado.Conexion = EstadoConexion.Offline;
                this.iPeriodoActualizacion = this.oDA0.ObtenerPeriodoActualizacion(this.lu.CodProceso, this.lu.CodPantalla);

                this.iCodProcesoAct = this.lu.CodProceso;
                this.iCodProcesoAnt = this.oDA0.ObtenerProcesoAnterior(this.iCodProcesoAct);

                if (this.TipoAuditoria == eTipoAuditoria.Al100PorCiento)
                {
                    this.txEtiqueta.ReadOnly = false;
                    this.txEtiqueta.Enabled = true;

                    this.btTerminar.Enabled = true;

                    this.txEtiqueta.Focus();
                }
                else if (this.TipoAuditoria == eTipoAuditoria.PorMuestreo)
                {
                    this.txTarima.ReadOnly = false;
                    this.txTarima.Enabled = true;

                    this.btTerminar.Enabled = false;

                    this.txTarima.Focus();
                }
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
                            frmObj.ShowDialog();
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
                    Validacion val = null;

                    val = this.ValidarTarima(txObj.Text);
                    this.encabezado.Mensaje = val.MensajeValidacion;

                    if (val.ValidacionExitosa)
                    {
                        this.txTarima.ReadOnly = true;
                        this.txTarima.Enabled = false;
                        this.txEtiqueta.ReadOnly = false;
                        this.txEtiqueta.Enabled = true;

                        this.btPaletizar.Enabled = true;
                        this.btCancelar.Enabled = true;

                        this.txEtiqueta.Focus();
                    }
                    else
                    {
                        this.txTarima.ReadOnly = false;
                        this.txTarima.Enabled = true;
                        this.txEtiqueta.ReadOnly = true;
                        this.txEtiqueta.Enabled = false;

                        this.btPaletizar.Enabled = false;
                        this.btCancelar.Enabled = false;

                        txObj.SelectAll();
                        txObj.Focus();
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
                    TextBox txObj = (TextBox)sender;
                    Validacion val = null;
                    DataTable dtObj = null;

                    val = this.ValidarPieza(txObj.Text);
                    this.encabezado.Mensaje = val.MensajeValidacion;

                    if (val.ValidacionExitosa)
                    {
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

                        this.btAceptar.Enabled = true;
                        this.btRechazar.Enabled = true;

                        this.btAceptar.Focus();
                    }
                    else
                    {
                        this.txTipo.Text = String.Empty;
                        this.txModelo.Text = String.Empty;
                        this.txColor.Text = String.Empty;
                        this.txCalidad.Text = String.Empty;

                        this.btAceptar.Enabled = false;
                        this.btRechazar.Enabled = false;

                        txObj.SelectAll();
                        txObj.Focus();
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

        #region btAceptar_Click
        private void btAceptar_Click(object sender, EventArgs e)
        {
            try
            {
                Validacion val = null;
                val = this.ValidarPieza(this.txEtiqueta.Text);
                this.encabezado.Mensaje = val.MensajeValidacion;

                if (val.ValidacionExitosa)
                {
                    if (this.TipoAuditoria == eTipoAuditoria.Al100PorCiento)
                    {
                        long lCodPiezaTransaccion = this.oDA0.InsertarPiezaTransaccion(DA.eTipoConexion.Local,
                                                                                        this.lu.CodConfigHandHeld,
                                                                                        this.iCodPieza,
                                                                                        this.lu.Fecha);
                        this.oDA.ActualizarPiezaAuditada(DA.eTipoConexion.Local, this.iCodPieza, true);
                        this.oDA0.ActulizarUltimoProcesoPieza(DA.eTipoConexion.Local, this.iCodPieza, this.lu.CodProceso);
                    }
                    else if (this.TipoAuditoria == eTipoAuditoria.PorMuestreo)
                    {
                        this.EstablecerPiezaAceptada(this.iCodPieza);
                    }

                    this.txEtiqueta.Text = String.Empty;
                }
                else
                {
                    this.txEtiqueta.SelectAll();
                }
                this.txTipo.Text = String.Empty;
                this.txModelo.Text = String.Empty;
                this.txColor.Text = String.Empty;
                this.txCalidad.Text = String.Empty;

                this.btAceptar.Enabled = false;
                this.btRechazar.Enabled = false;

                this.txEtiqueta.Focus();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
            }
        }
        #endregion btAceptar_Click
        #region btRechazar_Click
        private void btRechazar_Click(object sender, EventArgs e)
        {
            try
            {
                this.lu.CodPieza = this.iCodPieza;
                this.lu.CodBarras = this.txEtiqueta.Text;
                a04_Defectos frmObj = new a04_Defectos(this.lu, false);
                frmObj.ShowDialog();
                if (this.TipoAuditoria == eTipoAuditoria.Al100PorCiento)
                {
                    if (this.oDA0.ExistePiezaLocal(this.lu.CodPieza))
                    {
                        DataTable dt = this.oDA0.ObtenerEstadoPieza(this.lu.CodPieza, false);
                        DataTable dtPieza = this.oDA0.ObtenerPiezaLocal(this.lu.CodPieza);
                        if (dt != null && dtPieza != null)
                        {
                            int iCodigoEstadoReparacion = this.oDA0.ObtenerCodEstadoPiezaEnReparacion();
                            int iCodigoProceso = Convert.ToInt32(dtPieza.Rows[0]["cod_ultimo_proceso"]);
                            foreach (DataRow row in dt.Rows)
                            {
                                if (Convert.ToInt32(row["CodEstadoPieza"]) == iCodigoEstadoReparacion & (iCodigoProceso == this.oDA0.ObtenerCodProcesoEmpaque() | iCodigoProceso == this.oDA0.ObtenerCodProcesoClasificado()))
                                {
                                    int iResultado = this.oDA0.ActulizarUltimoProcesoPieza(eTipoConexion.Servicio, this.lu.CodPieza, this.oDA0.ObtenerCodProcesoClasificado());
                                    if (iResultado == 1)
                                    {
                                        this.oDA0.ActulizarUltimoProcesoPieza(eTipoConexion.Local, this.lu.CodPieza, this.oDA0.ObtenerCodProcesoClasificado());
                                        this.oDA0.ActualizarCalidadPieza(this.lu.CodPieza, string.Empty);
                                    }
                                }
                            }
                        }
                    }
                    /*else if (this.TipoAuditoria == eTipoAuditoria.PorMuestreo)
                    {
                        this.EstablecerPiezaRechazada(this.iCodPieza);
                    }*/
                }
                this.txTipo.Text = String.Empty;
                this.txModelo.Text = String.Empty;
                this.txColor.Text = String.Empty;
                this.txCalidad.Text = String.Empty;

                this.btAceptar.Enabled = false;
                this.btRechazar.Enabled = false;

                this.txEtiqueta.Text = String.Empty;
                this.txEtiqueta.Focus();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
            }
        }
        #endregion btRechazar_Click
        #region btPaletizar_Click
        private void btPaletizar_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.ExisteUnaPiezaRechazada())
                {
                    DialogResult drRes = MessageBox.Show("Existe al menos una pieza rechazada. ¿Acepta eliminar tarima?", "SCPP", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
                    if (drRes == DialogResult.Yes)
                    {
                        // Si se encuentra por lo menos una pieza rechazada:
                        // Se da de baja la tarima.
                        // Se establece el campo 'Auditada' en 'false' de la tabla 'pieza'.
                        // Se establece el campo 'UltimoProceso' en 'Clasificado'.
                        this.oDA.RechazarTarimaPieza(DA.eTipoConexion.Local, this.iCodTarima);
                        int iCodPieza = -1;
                        foreach (DataRow dr in this.dtPiezasTarima.Rows)
                        {
                            iCodPieza = Convert.ToInt32(dr["CodPieza"]);
                            this.oDA.ActualizarPiezaAuditada(DA.eTipoConexion.Local, iCodPieza, false);
                            this.oDA0.ActulizarUltimoProcesoPieza(DA.eTipoConexion.Local, iCodPieza, this.oDA0.ObtenerCodProcesoClasificado());
                        }
                        // Regresar a Configuracion Inicial.
                        a03_ConfiguracionInicial frmObj = new a03_ConfiguracionInicial(this.lu);
                        frmObj.ShowDialog();
                        this.Close();
                    }
                }
                else if (this.ExisteUnaPiezaAuditada() || this.ExisteUnaPiezaAceptada())
                {
                    DialogResult drRes = MessageBox.Show("Existe al menos una pieza auditada. ¿Acepta paletizar tarima?", "SCPP", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
                    if (drRes == DialogResult.Yes)
                    {
                        // Si se encuentra por lo menos una pieza rechazada:
                        // Se paletiza la tarima.
                        // Se establece el campo 'Auditada' en 'true' de la tabla 'pieza'.
                        // Se establece el campo 'UltimoProceso' en 'Auditado'.

                        this.oDA.ActualizarTarimaPaletizado(this.iCodTarima, true);

                        int iCodPieza = -1;
                        foreach (DataRow dr in this.dtPiezasTarima.Rows)
                        {
                            iCodPieza = Convert.ToInt32(dr["CodPieza"]);

                            long lCodPiezaTransaccion = this.oDA0.InsertarPiezaTransaccion(DA.eTipoConexion.Local,
                                                                                            this.lu.CodConfigHandHeld,
                                                                                            iCodPieza,
                                                                                            this.lu.Fecha);
                            this.oDA.ActualizarPiezaAuditada(DA.eTipoConexion.Local, iCodPieza, false);
                            this.oDA0.ActulizarUltimoProcesoPieza(DA.eTipoConexion.Local, iCodPieza, this.iCodProcesoAct);
                        }

                        this.EnviarDatosAlServidor();

                        // Regresar a Configuracion Inicial.
                        a03_ConfiguracionInicial frmObj = new a03_ConfiguracionInicial(this.lu);
                        frmObj.Show();
                        this.Close();
                    }
                }
                else
                {
                    this.encabezado.Mensaje = "No hay piezas auditadas en tarima";
                    this.txEtiqueta.Focus();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
            }
        }
        #endregion btPaletizar_Click
        #region btCancelar_Click
        private void btCancelar_Click(object sender, EventArgs e)
        {
            try
            {
                DialogResult dr = MessageBox.Show("¿Cancelar Captura de Auditoria?", "SCPP", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
                if (dr == DialogResult.Yes)
                {
                    // Regresar a Configuracion Inicial.
                    a03_ConfiguracionInicial frmObj = new a03_ConfiguracionInicial(this.lu);
                    frmObj.ShowDialog();
                    this.Close();
                }
                else
                {
                    this.txEtiqueta.Text = String.Empty;
                    this.txEtiqueta.Focus();
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
                this.EnviarDatosAlServidor();

                //Regresar a Configuracion Inicial.
                a03_ConfiguracionInicial frmObj = new a03_ConfiguracionInicial(this.lu);
                frmObj.ShowDialog();
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
        #endregion methods
    }

    public enum eTipoAuditoria
    {
        Indeterminado = -1,
        Al100PorCiento = 1,
        PorMuestreo = 2
    }
}