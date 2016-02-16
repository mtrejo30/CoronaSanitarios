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
    public partial class a11_CapturaEmpaque : Form
    {

        #region fields
        private int iCodigoTarima;
        private enum TipoEtiqueta:int
        { 
            Pieza = 1,
            Tarima = 2
        }
        private TipoEtiqueta enumTipoEtiqueta;
        private LoginUsuario lu = null;
        private c00_Common oDA0 = new c00_Common();
        private c11_CapturaEmpaque oDA = new c11_CapturaEmpaque();
        private Timer trActualizarDatosServidor = new Timer();
        private int iPeriodoActualizacion = -1;
        private int iCodPieza = -1;
        private int iCodMolde = -1;
        private int iCodModeloSel = -1;
        private string sClaveModeloSel = string.Empty;
        string sClaveColor = string.Empty;
        private int iCodCalidadSel = -1;
        private string sClaveCalidadSel = string.Empty;
        private string sCodBarras = string.Empty;
        private frmProduccionOperador frmProduccion;
        #endregion fields
        #region properties
        public frmProduccionOperador FormaProduccion { get { return frmProduccion; } set { frmProduccion = value; } }
        #endregion properties
        #region methods
        #region constructors and destructor
        public a11_CapturaEmpaque(LoginUsuario lu)
        {
            InitializeComponent();
            //
            this.lu = lu;
            this.ConfigurarFormulario();
            this.ConfigurarPanelControles();
        }
        ~a11_CapturaEmpaque()
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
            this.txColor.Enabled = false;
            this.txColor.ReadOnly = true;
            this.txSKU.Enabled = false;
            this.txSKU.ReadOnly = true;
            this.btnImprimir.Visible = false;

            this.txEtiqueta.KeyPress += new KeyPressEventHandler(this.txEtiqueta_KeyPress);
            this.cbxModelo.SelectedIndexChanged += new EventHandler(this.cbxModelo_SelectedIndexChanged);
            this.cbxModelo.KeyPress += new KeyPressEventHandler(this.cbxModelo_KeyPress);
            this.cbxCalidad.SelectedIndexChanged += new EventHandler(this.cbxCalidad_SelectedIndexChanged);
            this.cbxCalidad.KeyPress += new KeyPressEventHandler(this.cbxCalidad_KeyPress);

            this.btAceptar.Click += new EventHandler(this.btAceptar_Click);
            this.btDefectos.Click += new EventHandler(this.btDefectos_Click);
            this.btTerminar.Click += new EventHandler(this.btTerminar_Click);

            // Configuracion del Timer.
            //this.trActualizarDatosServidor.Interval = 100;
            //this.trActualizarDatosServidor.Enabled = true;
            //this.trActualizarDatosServidor.Tick += new EventHandler(this.trActualizarDatosServidor_Tick);
        }
        #endregion ConfigurarPanelControles
        #region CrearSKU
        private string CrearSKU(string sClaveModelo, string sClaveColor, string sClaveCalidad)
        {
            string sSKU = string.Empty;
            sSKU = sClaveModelo + "-" + sClaveColor + "-" + sClaveCalidad;
            return sSKU;
        }
        #endregion CrearSKU
        #region ValidarPieza
        private void ValidarPieza(string sCodBarras, int iCodProceso)
        {
            ValidacionPieza valOnLine = null;
            ValidacionPieza valFinal = null;
            DataTable dtObj = null;
            // Validar.
            valOnLine = this.oDA0.ValidarPieza(sCodBarras, iCodProceso, false);
            valFinal = valOnLine;
            if (valFinal.CodProceso == oDA0.ObtenerCodProcesoAuditoria())
            {
                valFinal.ValProcesoExitosa = true;
                valFinal.ValNoDefDespExitosa = true;
                valFinal.MensajeValidacion = "";
            }
            // logica de validacion adicional.
            if (valFinal.ValProcesoExitosa && valFinal.ValNoDefDespExitosa)
            {
                valFinal.ValidacionExitosa = true;
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
                    if (dtObj.Columns.Count > 0 & dtObj.Rows.Count > 0 & dtObj.Columns.Contains("CodModelo"))
                        this.iCodModeloSel = Convert.ToInt32(dtObj.Rows[0]["CodModelo"]);
                    else
                        this.iCodModeloSel = -1;
                    if (dtObj.Columns.Count > 0 & dtObj.Rows.Count > 0 & dtObj.Columns.Contains("ClaveModelo"))
                        this.sClaveModeloSel = Convert.ToString(dtObj.Rows[0]["ClaveModelo"]);
                    else
                        this.sClaveModeloSel = string.Empty;
                }
                // Obtener el Color de la Pieza.
                dtObj = this.oDA0.ObtenerColorPieza(this.iCodPieza);
                if (dtObj != null)
                {
                    if (dtObj.Rows.Count > 0 & dtObj.Columns.Count > 0 & dtObj.Columns.Contains("DesColor"))
                        this.txColor.Text = Convert.ToString(dtObj.Rows[0]["DesColor"]);
                    else
                        this.txColor.Text = string.Empty;
                    if (dtObj.Rows.Count > 0 & dtObj.Columns.Count > 0 & dtObj.Columns.Contains("ClaveColor"))
                        this.sClaveColor = Convert.ToString(dtObj.Rows[0]["ClaveColor"]);
                    else
                        this.sClaveColor = string.Empty;
                }
                // Obtener la Calidad de la pieza.
                dtObj = this.oDA0.ObtenerCalidadPieza(this.iCodPieza);
                if (dtObj != null)
                {
                    if (dtObj.Rows.Count > 0 & dtObj.Columns.Count > 0 & dtObj.Columns.Contains("CodCalidad"))
                        this.iCodCalidadSel = Convert.ToInt32(dtObj.Rows[0]["CodCalidad"]);
                    else
                        this.iCodCalidadSel = -1;
                    if (dtObj.Rows.Count > 0 & dtObj.Columns.Count > 0 & dtObj.Columns.Contains("ClaveCalidad"))
                        this.sClaveCalidadSel = Convert.ToString(dtObj.Rows[0]["ClaveCalidad"]);
                    else
                        this.sClaveCalidadSel = string.Empty;
                }
                // Obtener el CodMolde de la pieza.
                this.iCodMolde = this.oDA.ObtenerCodMoldePieza(this.iCodPieza);
                // Llenar ComboBox 'Modelo' y 'Calidad'.
                DataRow drObj = null;
                ComboBox cbxObj = null;
                dtObj = this.oDA.ObtenerModelos2(this.iCodMolde);
                drObj = dtObj.NewRow();
                drObj["CodModelo"] = -1;
                drObj["ClaveModelo"] = "";
                drObj["DesModelo"] = "Seleccionar...";
                drObj["CodTipoArticulo"] = -1;
                drObj["ClaveTipoArticulo"] = "";
                drObj["DesTipoArticulo"] = "";
                dtObj.Rows.InsertAt(drObj, 0);
                cbxObj = this.cbxModelo;
                cbxObj.ValueMember = "CodModelo";
                cbxObj.DisplayMember = "DesModelo";
                cbxObj.SelectedIndexChanged -= new EventHandler(this.cbxModelo_SelectedIndexChanged);
                cbxObj.DataSource = dtObj;
                cbxObj.SelectedValue = -1;
                cbxObj.SelectedIndexChanged += new EventHandler(this.cbxModelo_SelectedIndexChanged);
                if (this.cbxCalidad.DataSource == null)
                {
                    dtObj = this.oDA0.ObtenerCalidades();
                    drObj = dtObj.NewRow();
                    drObj["CodCalidad"] = -1;
                    drObj["ClaveCalidad"] = "";
                    drObj["DesCalidad"] = "Seleccionar...";
                    dtObj.Rows.InsertAt(drObj, 0);
                    cbxObj = this.cbxCalidad;
                    cbxObj.ValueMember = "CodCalidad";
                    cbxObj.DisplayMember = "DesCalidad";
                    cbxObj.SelectedIndexChanged -= new EventHandler(this.cbxCalidad_SelectedIndexChanged);
                    cbxObj.DataSource = dtObj;
                    cbxObj.SelectedValue = -1;
                    cbxObj.SelectedIndexChanged += new EventHandler(this.cbxCalidad_SelectedIndexChanged);
                }
                // Establecer valores obtenidos.
                this.cbxModelo.SelectedIndexChanged -= new EventHandler(this.cbxModelo_SelectedIndexChanged);
                this.cbxModelo.SelectedValue = this.iCodModeloSel;
                this.cbxModelo.SelectedIndexChanged += new EventHandler(this.cbxModelo_SelectedIndexChanged);
                if (this.iCodCalidadSel > 0)
                {
                    this.cbxCalidad.SelectedIndexChanged -= new EventHandler(this.cbxCalidad_SelectedIndexChanged);
                    this.cbxCalidad.SelectedValue = this.iCodCalidadSel;
                    this.cbxCalidad.SelectedIndexChanged += new EventHandler(this.cbxCalidad_SelectedIndexChanged);
                }
                else if (Convert.ToInt32(cbxCalidad.SelectedValue) > 0)
                {
                    this.iCodCalidadSel = Convert.ToInt32(cbxCalidad.SelectedValue);
                    this.sClaveCalidadSel = Convert.ToString(((DataRowView)cbxCalidad.SelectedItem)["ClaveCalidad"]);
                }
                this.txSKU.Text = this.CrearSKU(this.sClaveModeloSel, this.sClaveColor, this.sClaveCalidadSel);
                if (valFinal.CodProceso != this.lu.CodProceso)
                {
                    this.MostrarBotonImpresion(false);
                    this.btAceptar.Focus();
                }
                else
                    this.MostrarBotonImpresion(true);
            }
            else
            {
                this.txTipo.Text = string.Empty;
                this.cbxModelo.SelectedIndexChanged -= new EventHandler(this.cbxModelo_SelectedIndexChanged);
                this.cbxModelo.SelectedValue = -1;
                this.cbxModelo.SelectedIndexChanged += new EventHandler(this.cbxModelo_SelectedIndexChanged);
                this.txColor.Text = string.Empty;
                this.cbxCalidad.SelectedIndexChanged -= new EventHandler(this.cbxCalidad_SelectedIndexChanged);
                this.cbxCalidad.SelectedValue = -1;
                this.cbxCalidad.SelectedIndexChanged += new EventHandler(this.cbxCalidad_SelectedIndexChanged);
                this.txSKU.Text = string.Empty;
                this.cbxModelo.Enabled = false;
                this.cbxCalidad.Enabled = false;
                this.btAceptar.Enabled = false;
                this.txEtiqueta.SelectAll();
                this.txEtiqueta.Focus();
            }
        }
        #endregion ValidarPieza
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

            this.encabezado.Mensaje = string.Empty;
            this.Refresh();
        }
        #endregion EnviarDatosAlServidor
        private void MostrarBotonImpresion(bool bMostrarBotonImpresion)
        {
            this.btnImprimir.Visible = bMostrarBotonImpresion; //this.oDA.HabilitarImpresionEtiqueta(this.iCodPieza);
            this.cbxCalidad.Enabled = !bMostrarBotonImpresion;
            this.cbxModelo.Enabled = !bMostrarBotonImpresion;
            this.btAceptar.Enabled = !bMostrarBotonImpresion;
            this.btDefectos.Enabled = !bMostrarBotonImpresion;
        }
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
                this.cbxModelo.Enabled = false;
                this.cbxCalidad.Enabled = false;
                this.btAceptar.Enabled = false;
                this.btDefectos.Enabled = false;
                this.txEtiqueta.Focus();
                //if (this.lu.CodPlanta == 4)
                //    this.btnCerrarTarima.Visible = false;
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
                    this.sCodBarras = txObj.Text;
                    this.ValidarPieza(txObj.Text, this.lu.CodProceso);
                    //if (this.lu.CodPlanta != 4)
                    //{
                        this.iCodigoTarima = 0;
                        this.iCodigoTarima = this.ObtenerTarima(this.sCodBarras);
                        if (this.iCodigoTarima > 0)
                        {
                            this.btnCerrarTarima.Enabled = true;
                            this.encabezado.Titulo = "Tarima: " + this.iCodigoTarima.ToString();
                        }
                        else
                        {
                            this.btnCerrarTarima.Enabled = false;
                            this.encabezado.Titulo = "La pieza no ha sido entarimada.";
                        }
                    //}
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
        #region cbxModelo_SelectedIndexChanged
        private void cbxModelo_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                ComboBox cbxObj = (ComboBox)sender;
                this.iCodModeloSel = Convert.ToInt32(cbxObj.SelectedValue);
                this.sClaveModeloSel = Convert.ToString(((DataRowView)cbxObj.SelectedItem)["ClaveModelo"]);
                if (this.iCodModeloSel == -1)
                {
                    this.txSKU.Text = string.Empty;
                    this.encabezado.Mensaje = "Seleccione Modelo";
                    cbxObj.Focus();
                }
                else
                {
                    this.txSKU.Text = this.CrearSKU(this.sClaveModeloSel, this.sClaveColor, this.sClaveCalidadSel);
                    this.encabezado.Mensaje = String.Empty;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
            }
        }
        #endregion cbxModelo_SelectedIndexChanged
        #region cbxModelo_KeyPress
        private void cbxModelo_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                // Validar ingreso del Enter.
                if (e.KeyChar == 13)
                {
                    ComboBox cbxObj = (ComboBox)sender;

                    if (this.iCodModeloSel == -1)
                    {
                        this.encabezado.Mensaje = "Seleccione Modelo";

                        cbxObj.Focus();
                    }
                    else
                    {
                        this.encabezado.Mensaje = String.Empty;

                        this.cbxCalidad.Focus();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
            }
        }
        #endregion cbxModelo_KeyPress
        #region cbxCalidad_SelectedIndexChanged
        private void cbxCalidad_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                ComboBox cbxObj = (ComboBox)sender;

                this.iCodCalidadSel = Convert.ToInt32(cbxObj.SelectedValue);
                this.sClaveCalidadSel = Convert.ToString(((DataRowView)cbxObj.SelectedItem)["ClaveCalidad"]);

                if (this.iCodCalidadSel == -1)
                {
                    this.txSKU.Text = string.Empty;

                    this.encabezado.Mensaje = "Seleccione Calidad";

                    cbxObj.Focus();
                }
                else
                {
                    this.txSKU.Text = this.CrearSKU(this.sClaveModeloSel, this.sClaveColor, this.sClaveCalidadSel);

                    this.encabezado.Mensaje = String.Empty;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
            }
        }
        #endregion cbxCalidad_SelectedIndexChanged
        #region cbxCalidad_KeyPress
        private void cbxCalidad_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                // Validar ingreso del Enter.
                if (e.KeyChar == 13)
                {
                    ComboBox cbxObj = (ComboBox)sender;

                    if (this.iCodCalidadSel == -1)
                    {
                        this.encabezado.Mensaje = "Seleccione Calidad";

                        cbxObj.Focus();
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
        #endregion cbxCalidad_KeyPress
        #region btAceptar_Click
        private void btAceptar_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.iCodModeloSel == -1)
                {
                    this.encabezado.Mensaje = "Seleccione Modelo";
                    this.cbxModelo.Focus();
                    return;
                }
                if (this.iCodCalidadSel == -1)
                {
                    this.encabezado.Mensaje = "Seleccione Calidad";
                    this.cbxCalidad.Focus();
                    return;
                }
                // Validar existe combinacion MODELO-COLOR-CALIDAD en Articulos.
                string sClaveModelo = this.CrearSKU(this.sClaveModeloSel, this.sClaveColor, this.sClaveCalidadSel);
                int iCodModelo = this.oDA0.ExisteModelo(sClaveModelo);
                if (iCodModelo == -1)
                {
                    this.encabezado.Mensaje = "Modelo-Color-Calidad invalido";
                    this.cbxModelo.Focus();
                    return;
                }
                this.encabezado.Mensaje = String.Empty;
                if (this.lu.CodConfigHandHeld < 50000)
                    this.lu.CodConfigHandHeld = new c00_Transacciones().EnviarUnConfigHandHeld(this.lu.CodConfigHandHeld);
                if (this.lu.CodConfigHandHeld > 0)
                {
                    long lCodPiezaTransaccion = this.oDA0.InsertarPiezaTransaccion(DA.eTipoConexion.Servicio,
                                                                                    this.lu.CodConfigHandHeld,
                                                                                    this.iCodPieza,
                                                                                this.lu.Fecha);
                    this.oDA0.ActulizarUltimoProcesoPieza(DA.eTipoConexion.Servicio, this.iCodPieza, this.lu.CodProceso);
                    this.oDA.ActualizarModeloPieza(DA.eTipoConexion.Servicio, this.iCodPieza, this.iCodModeloSel);
                    this.oDA.ActualizarCalidadPieza(DA.eTipoConexion.Servicio, this.iCodPieza, this.iCodCalidadSel);
                    ImprimirEtiquetaPieza();
                    //if (this.lu.CodPlanta != 4)
                    //{
                        this.iCodigoTarima = EnTarimarPieza(this.sCodBarras, this.lu.CodMaquina, this.lu.CodCentroTrabajo);
                        if (this.iCodigoTarima > 0)
                            this.encabezado.Titulo = "Pieza entarimada, tarima: " + this.iCodigoTarima.ToString();
                        else
                        {
                            this.encabezado.Titulo = "No se puede entarimar ni procesar la pieza, consulte al administrador.";
                            return;
                        }
                    //}
                    this.iCodPieza = -1;
                    this.cbxModelo.Enabled = false;
                    this.cbxCalidad.Enabled = false;
                    this.btAceptar.Enabled = false;
                    this.txEtiqueta.Text = string.Empty;
                    this.cbxModelo.SelectedIndexChanged -= new EventHandler(this.cbxModelo_SelectedIndexChanged);
                    this.cbxModelo.DataSource = null;
                    this.cbxModelo.SelectedIndexChanged += new EventHandler(this.cbxModelo_SelectedIndexChanged);
                    this.txTipo.Text = string.Empty;
                    this.txColor.Text = string.Empty;
                    //this.cbxCalidad.SelectedIndexChanged -= new EventHandler(this.cbxCalidad_SelectedIndexChanged);
                    //this.cbxCalidad.DataSource = null;
                    //this.cbxCalidad.SelectedIndexChanged += new EventHandler(this.cbxCalidad_SelectedIndexChanged);
                    this.txSKU.Text = string.Empty;
                    this.txEtiqueta.Focus();
                }
                else
                {
                    MessageBox.Show("Operacion Cancelada, Compruebe su conexión con la red.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
                }
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
                this.EnviarDatosAlServidor();
                // Regresar a Configuracion Inicial.
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
        private void btnImprimir_Click(object sender, EventArgs e)
        {
            //Imprimir();
            //this.btnImprimir.Visible = this.oDA.HabilitarImpresionEtiqueta(this.iCodPieza);
            HHsvc.ConfigImpresora cfgImpresora = ObtenerConfiguracionImpresora();
            HHsvc.Etiqueta etiqueta = ObtenerConfiguracionEtiqueta();
            frmAutorizacionImpresion frmImpresion = new frmAutorizacionImpresion();
            frmImpresion.ConfiguracionImpresora = cfgImpresora;
            if (etiqueta.Tarima == "0")
                etiqueta.Tarima = Convert.ToString(ObtenerTarima(this.txEtiqueta.Text.Trim()));
            frmImpresion.Etiqueta = etiqueta;
            frmImpresion.CodigoRol = lu.CodRol;
            frmImpresion.LU = this.lu;
            this.Hide();
            frmImpresion.ShowDialog();
            frmImpresion.Dispose();
            this.Show();
        }
        private void ImprimirEtiquetaPieza()
        {
            HHsvc.Etiqueta eEtiqueta = null;
            HHsvc.ConfigImpresora cConfigImpresora = null;
            HHsvc.SCPP_HH proxy = null;
            try
            {
                eEtiqueta = ObtenerConfiguracionEtiqueta();
                cConfigImpresora = ObtenerConfiguracionImpresora();
                eEtiqueta.TipoEtiqueta = 1;
                eEtiqueta.TipoEtiquetaSpecified = true;
                proxy = new HHsvc.SCPP_HH();
                proxy.ImprimirEtiqueta(cConfigImpresora, eEtiqueta);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
            }
            finally
            {
                eEtiqueta = null;
                cConfigImpresora = null;
                proxy = null;
            }
        }
        private void ImprimirEtiquetaTarima()
        {
            HHsvc.Etiqueta eEtiqueta = null;
            HHsvc.ConfigImpresora cConfigImpresora = null;
            HHsvc.SCPP_HH proxy = null;
            try
            {
                eEtiqueta = ObtenerConfiguracionEtiqueta();
                cConfigImpresora = ObtenerConfiguracionImpresora();
                eEtiqueta.TipoEtiqueta = 2;
                eEtiqueta.TipoEtiquetaSpecified = true;
                proxy = new HHsvc.SCPP_HH();
                proxy.ImprimirEtiqueta(cConfigImpresora, eEtiqueta);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
            }
            finally
            {
                eEtiqueta = null;
                cConfigImpresora = null;
                proxy = null;
            }
        }
        private HHsvc.ConfigImpresora ObtenerConfiguracionImpresora()
        {
            HHsvc.ConfigImpresora cConfigImpresora = new HHsvc.ConfigImpresora();
            cConfigImpresora.CodPlanta = lu.CodPlanta;
            cConfigImpresora.CodMaquina = this.lu.CodMaquina;
            cConfigImpresora.CodCentroTrabajo = this.lu.CodCentroTrabajo;
            cConfigImpresora.CodCentroTrabajoSpecified = true;
            cConfigImpresora.CodMaquinaSpecified = true;
            cConfigImpresora.CodPlantaSpecified = true;
            return cConfigImpresora;
        }
        private HHsvc.Etiqueta ObtenerConfiguracionEtiqueta()
        {
            int iCodModelo = this.oDA0.ExisteModelo(this.txSKU.Text);
            HHsvc.Etiqueta eEtiqueta = new HHsvc.Etiqueta();
            eEtiqueta.Clave = this.txSKU.Text;
            eEtiqueta.Cod = iCodModelo;
            eEtiqueta.Pieza = this.sCodBarras;
            eEtiqueta.Tarima = this.iCodigoTarima.ToString();
            eEtiqueta.CodSpecified = true;
            return eEtiqueta;
        }
        #endregion event handlers
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
        #endregion methods
        private void btnCerrarTarima_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("¿Desea cerrar la tarima " + this.iCodigoTarima.ToString() + ", de la pieza escaneada.?", this.Text + ": Cierre de Tarima", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);
            if (result == DialogResult.No) return;
            this.CerrarTarima();
            ResetearCaptura();
            result = MessageBox.Show("¿Desea imprimir etiqueta de tarima.?", this.Text + ": Cierre de Tarima",MessageBoxButtons.YesNo,MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);
            if (result == DialogResult.Yes)
                this.ImprimirEtiquetaTarima();
        }
        private int ObtenerTarima(string sCodigoBarraPieza) 
        {
            if (string.IsNullOrEmpty(sCodigoBarraPieza)) return 0;
            HHsvc.SCPP_HH proxy = null;
            try
            {
                int iResult = 0;
                bool bResult = true;
                proxy = new HHsvc.SCPP_HH();
                proxy.ObtenerTarimaPieza(sCodigoBarraPieza, out iResult, out bResult);
                return iResult;
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
        private int EnTarimarPieza(string sCodigoBarraPieza, int iCodigoMaquina, int iCodigoCentroTrabajo)
        {
            if (string.IsNullOrEmpty(sCodigoBarraPieza)) return 0;
            HHsvc.SCPP_HH proxy = null;
            try
            {
                int iResultTarima = 0;
                bool bResultTarima = true;
                proxy = new HHsvc.SCPP_HH();
                proxy.EnTarimarPieza(sCodigoBarraPieza, iCodigoMaquina, true, iCodigoCentroTrabajo, true, true, true, out iResultTarima, out bResultTarima);
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
        private bool CerrarTarima()
        {
            if (this.iCodigoTarima == 0) return false;
            HHsvc.SCPP_HH proxy = null;
            try
            {
                bool bResult = false, bResultSpecified = true;
                proxy = new HHsvc.SCPP_HH();
                proxy.CerrarTarima(iCodigoTarima, true, out bResult, out bResultSpecified);
                return bResult;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
                return false;
            }
            finally
            {
                proxy = null;
            }
        }
        private void ResetearCaptura()
        {
            this.iCodPieza = -1;
            this.cbxModelo.Enabled = false;
            this.cbxCalidad.Enabled = false;
            this.btAceptar.Enabled = false;
            this.txEtiqueta.Text = string.Empty;
            this.cbxModelo.SelectedIndexChanged -= new EventHandler(this.cbxModelo_SelectedIndexChanged);
            this.cbxModelo.DataSource = null;
            this.cbxModelo.SelectedIndexChanged += new EventHandler(this.cbxModelo_SelectedIndexChanged);
            this.txTipo.Text = string.Empty;
            this.txColor.Text = string.Empty;
            //this.cbxCalidad.SelectedIndexChanged -= new EventHandler(this.cbxCalidad_SelectedIndexChanged);
            //this.cbxCalidad.DataSource = null;
            //this.cbxCalidad.SelectedIndexChanged += new EventHandler(this.cbxCalidad_SelectedIndexChanged);
            this.txSKU.Text = string.Empty;
            this.txEtiqueta.Focus();
            this.btnCerrarTarima.Enabled = false;
            this.btnImprimir.Visible = false;
        }
    }
}