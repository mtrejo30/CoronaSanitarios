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
    public partial class a09_CapturaHornos : Form
    {

        #region fields

        private LoginUsuario lu = null;
        private c00_Common oDA0 = new c00_Common();
        private c09_CapturaHornos oDA = new c09_CapturaHornos();
        private c04_CapturaInicial oCI = new c04_CapturaInicial();

        private Timer trActualizarDatosServidor = new Timer();
        private int iPeriodoActualizacion = -1;

        private DataTable dtPiezas = null;
        private int iCodPieza = -1;
        private string sCodZona = string.Empty;
        private int iCodPrueba = -1;
        private frmProduccionOperador frmProduccion;
        #endregion fields

        #region properties
        public frmProduccionOperador FormaProduccion { get { return frmProduccion; } set { frmProduccion = value; } }


        #endregion properties

        #region methods

        #region constructors and destructor

        public a09_CapturaHornos(LoginUsuario lu)
        {
            InitializeComponent();
            //
            this.lu = lu;
            this.ConfigurarFormulario();
            this.ConfigurarPanelControles();
            if ((this.lu.CodPlanta == 1 || this.lu.CodPlanta == 2 || this.lu.CodPlanta == 3))
            { 
                this.txCarro.KeyPress += new KeyPressEventHandler(txCarro_KeyPress);
                this.txCarro.LostFocus += new EventHandler(txCarro_LostFocus);
            }
        }
        ~a09_CapturaHornos()
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

            this.txEtiqueta.MaxLength = 15;
            this.txZona.MaxLength = 10;
            if ((this.lu.CodPlanta == 1 || this.lu.CodPlanta == 2 || this.lu.CodPlanta == 3))
            {
                this.txCarro.Enabled = true;
                this.txCarro.ReadOnly = false;
            }
            else
            {
                this.txCarro.Enabled = false;
                this.txCarro.ReadOnly = true;
            }

            this.txTipo.Enabled = false;
            this.txTipo.ReadOnly = true;
            this.txModelo.Enabled = false;
            this.txModelo.ReadOnly = true;
            this.txColor.Enabled = false;
            this.txColor.ReadOnly = true;

            this.txEtiqueta.KeyPress += new KeyPressEventHandler(this.txEtiqueta_KeyPress);
            this.txZona.KeyPress += new KeyPressEventHandler(this.txZona_KeyPress);
            this.txZona.LostFocus += new EventHandler(this.txZona_LostFocus);
            this.cbxPrueba.SelectedIndexChanged += new EventHandler(this.cbxPrueba_SelectedIndexChanged);

            this.btAceptar.Click += new EventHandler(this.btAceptar_Click);
            this.btDefectos.Click += new EventHandler(this.btDefectos_Click);
            this.btProcesar.Click += new EventHandler(this.btProcesar_Click);
            this.btCancelar.Click += new EventHandler(this.btCancelar_Click);

            //// Configuracion del Timer.
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

            // Validar en linea.
            valOnLine = this.oDA0.ValidarPieza(sCodBarras, iCodProceso, false);
            valFinal = valOnLine;

            // logica de validacion adicional.
            if (valFinal.ValProcesoExitosa && valFinal.ValNoDefDespExitosa)
            {
                valFinal.ValidacionExitosa = true;
                // Validar no se haya capturado la misma Pieza durante la captura.
                for (int iIndex = 0; iIndex < this.dtPiezas.Rows.Count; iIndex++)
                {
                    if (Convert.ToInt32(this.dtPiezas.Rows[iIndex]["CodPieza"]) == valFinal.CodPieza)
                    {
                        valFinal.ValidacionExitosa = false;
                        valFinal.MensajeValidacion = "Pieza ya capturada";
                        valFinal.CodPieza = -1;
                        break;
                    }
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

                // Obtener el Color de la Pieza.
                dtObj = this.oDA0.ObtenerColorPieza(this.iCodPieza);
                if (dtObj != null)
                {
                    if (dtObj.Rows.Count > 0 & dtObj.Columns.Count > 0 & dtObj.Columns.Contains("DesColor"))
                        this.txColor.Text = Convert.ToString(dtObj.Rows[0]["DesColor"]);
                    else
                        this.txColor.Text = string.Empty;
                }

                this.cbxPrueba.Enabled = true;
                this.btAceptar.Enabled = true;

                this.txZona.SelectAll();
                this.txZona.Focus();
            }
            else
            {
                this.txTipo.Text = string.Empty;
                this.txModelo.Text = string.Empty;
                this.txColor.Text = string.Empty;

                this.cbxPrueba.Enabled = false;
                this.btAceptar.Enabled = false;

                this.txEtiqueta.SelectAll();
                this.txEtiqueta.Focus();
            }
        }
        #endregion ValidarPieza
        private bool ValidarCarroHornos(string sCodCarro)
        {
            if (string.IsNullOrEmpty(sCodCarro))
            {
                this.lu.CodCarro = -1;

                this.encabezado.Mensaje = "Capture número de Carro";
                return false;
            }
            else
            {
                this.lu.CodCarro = Convert.ToInt32(sCodCarro);
                // Validar que el Numero de Carro no este ocupado.
                DataTable dtRes = this.oCI.ObtenerPiezasCarroHornos(this.lu.CodPlanta, this.lu.CodCarro);
                // Validacion para evitar un BUG de nivel BLOCKER; Se supone que este escenario no deberia ocurrir
                if (dtRes == null)
                {
                    this.lu.CodCarro = -1;
                    throw new Exception("Captura de carro invalida, contacte al administrador\n o capture otro carro.");
                }
                this.encabezado.Mensaje = String.Empty;
                return true;
            }
        }
        #region ValidarZonaCarro
        private void ValidarZonaCarro(string sZonaCarro, bool IsLostFocus)
        {
            this.sCodZona = sZonaCarro;

            if (string.IsNullOrEmpty(this.sCodZona))
            {
                this.encabezado.Mensaje = "Capture Zona";

                this.txZona.SelectAll();
                this.txZona.Focus();
            }
            else
            {
                this.encabezado.Mensaje = String.Empty;

                if (!IsLostFocus)
                {
                    if (this.btAceptar.Enabled)
                    {
                        this.btAceptar.Focus();
                    }
                    else
                    {
                        this.txEtiqueta.SelectAll();
                        this.txEtiqueta.Focus();
                    }
                }
            }
        }
        #endregion ValidarZonaCarro

        #region EnviarDatosAlServidor
        private void EnviarDatosAlServidor()
        {
            this.encabezado.Mensaje = "Enviando datos...";
            this.Refresh();

            c00_Transacciones ct = new c00_Transacciones();
            bool bEnvioExitoso = ct.EnviarDatosHornos();
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
                if ((this.lu.CodPlanta == 1 || this.lu.CodPlanta == 2 || this.lu.CodPlanta == 3))
                    this.txCarro.Text = string.Empty;
                else
                    this.txCarro.Text = this.lu.CodCarro.ToString();

                // Obtener periodo del timer.
                this.encabezado.Conexion = EstadoConexion.Offline;
                this.iPeriodoActualizacion = this.oDA0.ObtenerPeriodoActualizacion(this.lu.CodProceso, this.lu.CodPantalla);

                this.dtPiezas = new DataTable();
                this.dtPiezas.Columns.Add("CodPieza", typeof(int));
                this.dtPiezas.Columns.Add("CodZona", typeof(string));
                this.dtPiezas.Columns.Add("CodPrueba", typeof(int));

                // Llenar ComboBox 'Prueba'.
                DataTable dtObj = null;
                DataRow drObj = null;
                ComboBox cbxObj = null;

                dtObj = this.oDA0.ObtenerPruebas(this.lu.CodPlanta, this.lu.CodProceso);
                if (dtObj == null) return;
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
                this.btProcesar.Enabled = false;

                if ((this.lu.CodPlanta == 1 || this.lu.CodPlanta == 2 || this.lu.CodPlanta == 3))
                    this.txCarro.Focus();
                else
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
        #region txCarro_LostFocus
        private void txCarro_LostFocus(object sender, EventArgs e)
        {
            try
            {
                TextBox txObj = (TextBox)sender;
                if (!ValidarCarroHornos(txObj.Text))
                {
                    txObj.Text = string.Empty;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
            }
        }
        #endregion
        #region txCarro_KeyPress
        private void txCarro_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                // Validar ingreso del Enter.
                if (e.KeyChar == 13)
                {
                    TextBox txObj = (TextBox)sender;
                    if (!ValidarCarroHornos(txObj.Text))
                    {
                        txObj.Text = string.Empty;
                        txObj.Focus();
                    }
                    else
                    {
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
        #endregion
        #region txZona_KeyPress
        private void txZona_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                // Validar ingreso del Enter.
                if (e.KeyChar == 13)
                {
                    TextBox txObj = (TextBox)sender;

                    this.ValidarZonaCarro(txObj.Text, false);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
            }
        }
        #endregion txZona_KeyPress
        #region txZona_LostFocus
        private void txZona_LostFocus(object sender, EventArgs e)
        {
            try
            {
                TextBox txObj = (TextBox)sender;

                this.ValidarZonaCarro(txObj.Text, true);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
            }
        }
        #endregion txZona_LostFocus
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

                DataRow dr = this.dtPiezas.NewRow();
                dr["CodPieza"] = this.iCodPieza;
                dr["CodZona"] = this.sCodZona;
                dr["CodPrueba"] = cbxPrueba.SelectedValue;// this.iCodPrueba;
                this.dtPiezas.Rows.Add(dr);
                this.dtPiezas.AcceptChanges();

                this.iCodPieza = -1;
                //this.sCodZona = string.Empty;
                this.iCodPrueba = -1;

                this.cbxPrueba.Enabled = false;
                this.btAceptar.Enabled = false;
                this.btProcesar.Enabled = true;

                this.txEtiqueta.Text = string.Empty;
                //this.txZona.Text = string.Empty;
                this.txTipo.Text = string.Empty;
                this.txModelo.Text = string.Empty;
                this.txColor.Text = string.Empty;

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
        #region btProcesar_Click
        private void btProcesar_Click(object sender, EventArgs e)
        {
            try
            {
                int iCodPieza = -1;
                string sCodZona = string.Empty;
                int iCodPrueba = -1;
                if ((this.lu.CodPlanta == 1 || this.lu.CodPlanta == 2 || this.lu.CodPlanta == 3))
                    if (!this.ValidarCarroHornos(txCarro.Text))
                    {
                        txCarro.Focus();
                        return;
                    }
                foreach (DataRow dr in this.dtPiezas.Rows)
                {
                    iCodPieza = Convert.ToInt32(dr["CodPieza"]);
                    sCodZona = Convert.ToString(dr["CodZona"]);
                    iCodPrueba = Convert.ToInt32(dr["CodPrueba"]);

                    long lCodPiezaTransaccion = this.oDA0.InsertarPiezaTransaccion(DA.eTipoConexion.Local,
                                                                                    this.lu.CodConfigHandHeld,
                                                                                    iCodPieza,
                                                                                this.lu.Fecha);

                    this.oDA0.ActulizarUltimoProcesoPieza(DA.eTipoConexion.Local, iCodPieza, this.lu.CodProceso);

                    this.oDA.InsertarCarroZonaPieza(DA.eTipoConexion.Local, this.lu.CodPlanta, iCodPieza, this.lu.CodCarro, sCodZona);
                    if (iCodPrueba != -1)
                    {
                        this.oDA0.PruebaProcesoIns(this.lu.CodPlanta, iCodPrueba, this.oDA0.ObtenerCodProcesoHornos(),
                                                    iCodPieza, DateTime.Now);
                    }
                }

                this.EnviarDatosAlServidor();
                if ((this.lu.CodPlanta == 1 || this.lu.CodPlanta == 2 || this.lu.CodPlanta == 3))
                {
                    this.encabezado.Mensaje = String.Empty;
                    this.iCodPieza = -1;
                    this.iCodPrueba = -1;
                    this.cbxPrueba.Enabled = false;
                    this.btAceptar.Enabled = false;
                    this.btProcesar.Enabled = true;
                    this.txCarro.Text = string.Empty;
                    this.txZona.Text = string.Empty;
                    this.txEtiqueta.Text = string.Empty;
                    this.txTipo.Text = string.Empty;
                    this.txModelo.Text = string.Empty;
                    this.txColor.Text = string.Empty;
                    this.txCarro.Focus();
                    this.Form_Load(sender, e);
                    this.lu.CodConfigHandHeld = new c03_ConfiguracionInicial().InsertarConfigHandHeld(eTipoConexion.Local,
                                                                            this.lu.CodUsuario,
                                                                            this.lu.CodEmpleado,
                                                                            this.lu.CodSupervisor,
                                                                            this.lu.Fecha,
                                                                            this.lu.CodTurno,
                                                                            this.lu.CodPlanta,
                                                                            this.lu.CodProceso,
                                                                            null,
                                                                            null);
                    this.oCI.ActualizarConfigHandHeld(DataAccess.eTipoConexion.Local,
                                                    this.lu.CodSupervisor,
                                                    this.lu.CodEmpleado,
                                                    this.lu.CodConfigBanco,
                                                    this.lu.CodConfigHandHeld);
                }
                else
                {
                    // Regresar a Configuracion Inicial.
                    //Limpiamos valores para volver a capturar configuracion para hornos
                    this.lu.CodCentroTrabajo = -1;
                    this.lu.CodMaquina = -1;
                    a04_CapturaInicial frmObj = new a04_CapturaInicial(this.lu, true);
                    //a03_ConfiguracionInicial frmObj = new a03_ConfiguracionInicial(this.lu);
                    frmObj.Show();
                    this.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
            }
        }
        #endregion btProcesar_Click
        #region btCancelar_Click
        private void btCancelar_Click(object sender, EventArgs e)
        {
            try
            {
                DialogResult dr = MessageBox.Show("¿Cancelar Captura de Hornos?", "SCPP", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
                if (dr == DialogResult.Yes)
                {
                    if ((this.lu.CodPlanta == 1 || this.lu.CodPlanta == 2 || this.lu.CodPlanta == 3))
                    {
                        this.lu.CodCarro = -1;
                    }
                    // Regresar a Configuracion Inicial.
                    a03_ConfiguracionInicial frmObj = new a03_ConfiguracionInicial(this.lu);
                    frmObj.Show();
                    this.Close();
                }
                else
                {
                    //this.txZona.Text = string.Empty;
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

        private void btnProduccion_Click(object sender, EventArgs e)
        {
            try
            {
                frmProduccion = new frmProduccionOperador(this.lu);
                frmProduccion.FormaCaptura = this;
                frmProduccion.Show();
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
}