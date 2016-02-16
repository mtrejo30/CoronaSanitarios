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
    public partial class a04_CapturaInicial : Form
    {

        #region fields

        private LoginUsuario lu = null;
        private c00_Common oDA0 = new c00_Common();
        private c04_CapturaInicial oDA = new c04_CapturaInicial();
        private int iTiempoEnMinutosCapturaColor = 20;
        private Timer trActualizarDatosServidor = new Timer();
        private int iPeriodoActualizacion = -1;

        private int iCodOperador = -1;
        private int iCodSupervisor = -1;
        private string sOperadorName = string.Empty;
        private bool bForzarOffline = false;

        #endregion fields

        #region properties



        #endregion properties

        #region methods

        #region constructors and destructor

        public a04_CapturaInicial(LoginUsuario lu)
        {
            InitializeComponent();
            //
            this.lu = lu;
            this.ConfigurarFormulario();
            this.ConfigurarPanelControles();
        }
        public a04_CapturaInicial(LoginUsuario lu, bool isReload)
        {
            InitializeComponent();
            //
            this.lu = lu;
            this.ConfigurarFormulario();
            this.ConfigurarPanelControles();
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
        }
        ~a04_CapturaInicial()
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

            this.txOperador.ReadOnly = true;
            this.txOperador.Enabled = false;

            this.txOperador.KeyPress += new KeyPressEventHandler(this.txOperador_KeyPress);
            this.txSupervisor.KeyPress += new KeyPressEventHandler(this.txSupervisor_KeyPress);
            this.cbxCentroTrabajo.SelectedIndexChanged += new EventHandler(this.cbxCentroTrabajo_SelectedIndexChanged);
            this.cbxCentroTrabajo.KeyPress += new KeyPressEventHandler(this.cbxCentroTrabajo_KeyPress);
            this.cbxMaquina.SelectedIndexChanged += new EventHandler(this.cbxMaquina_SelectedIndexChanged);
            this.cbxMaquina.KeyPress += new KeyPressEventHandler(this.cbxMaquina_KeyPress);
            this.txPosicionInicial.KeyPress += new KeyPressEventHandler(this.txPosicionInicial_KeyPress);
            this.cbxColor.SelectedIndexChanged += new EventHandler(this.cbxColor_SelectedIndexChanged);
            this.cbxColor.KeyPress += new KeyPressEventHandler(this.cbxColor_KeyPress);
            this.txCarro.KeyPress += new KeyPressEventHandler(this.txCarro_KeyPress);
            this.rbAscendente.CheckedChanged += new EventHandler(this.rbDireccion_CheckedChanged);
            this.rbDescendente.CheckedChanged += new EventHandler(this.rbDireccion_CheckedChanged);
            this.rbAscendente.KeyPress += new KeyPressEventHandler(this.rbDireccion_KeyPress);
            this.rbDescendente.KeyPress += new KeyPressEventHandler(this.rbDireccion_KeyPress);

            this.btContinuar.Click += new EventHandler(this.btContinuar_Click);
            this.btCancelar.Click += new EventHandler(this.btCancelar_Click);

            this.btnActRequeme.Visible = this.lu.CodProceso == oDA0.ObtenerCodProcesoHornos() ? true : false;
            // Configuracion del Timer.
            //this.trActualizarDatosServidor.Interval = 100;
            //this.trActualizarDatosServidor.Enabled = true;
            //this.trActualizarDatosServidor.Tick += new EventHandler(this.trActualizarDatosServidor_Tick);
        }
        #endregion ConfigurarPanelControles

        #region ValidarCodOperador
        private bool ValidarCodOperador(string sClaveEmpleadoMFG)
        {
            if (string.IsNullOrEmpty(sClaveEmpleadoMFG))
            {
                this.iCodOperador = -1;

                this.encabezado.Mensaje = "Capture Clave Operador";
                return false;
            }
            else
            {
                int iClaveEmpleadoMFG = Convert.ToInt32(sClaveEmpleadoMFG);
                this.iCodOperador = this.oDA.ValidarEmpleadoMFG(iClaveEmpleadoMFG, this.bForzarOffline);

                if (this.iCodOperador == -1)
                {
                    this.encabezado.Mensaje = "Clave Operador invalida";
                    sOperadorName = string.Empty;
                    return false;
                }
                else
                {
                    this.encabezado.Mensaje = String.Empty;
                    return true;
                }
            }
        }
        #endregion ValidarCodOperador
        #region ValidarCodSupervisor
        private bool ValidarCodSupervisor(string sClaveEmpleadoMFG)
        {
            if (string.IsNullOrEmpty(sClaveEmpleadoMFG))
            {
                this.iCodSupervisor = -1;

                this.encabezado.Mensaje = "Capture Clave Supervisor";
                return false;
            }
            else
            {
                int iClaveEmpleadoMFG = Convert.ToInt32(sClaveEmpleadoMFG);
                this.iCodSupervisor = this.oDA.ValidarEmpleadoMFG(iClaveEmpleadoMFG, this.bForzarOffline);

                if (this.iCodSupervisor == -1)
                {
                    this.encabezado.Mensaje = "Clave Supervisor invalida";
                    return false;
                }
                else
                {
                    this.encabezado.Mensaje = String.Empty;
                    return true;
                }
            }
        }
        #endregion ValidarCodSupervisor
        #region ValidarPosicionInicial
        private bool ValidarPosicionInicial(string sPosInicial)
        {
            if (string.IsNullOrEmpty(sPosInicial))
            {
                this.lu.PosInicial = -1;

                this.encabezado.Mensaje = "Capture Posicion Inicial";
                return false;
            }
            else
            {
                // Validar Posicion Inicial.
                int iPosInicial = Convert.ToInt32(sPosInicial);
                int iCodConfigBanco = Convert.ToInt32(((DataRowView)this.cbxMaquina.SelectedItem)["CodConfigBanco"]);
                int iNumPosiciones = this.oDA.ObtenerNumPosicionesBanco(iCodConfigBanco, this.bForzarOffline);
                if (iPosInicial >= 1 && iPosInicial <= iNumPosiciones)
                {
                    this.lu.PosInicial = iPosInicial;

                    this.encabezado.Mensaje = String.Empty;
                    return true;
                }
                else
                {
                    this.lu.PosInicial = -1;

                    this.encabezado.Mensaje = "Pos. Inicial entre 1 y " + iNumPosiciones.ToString();
                    return false;
                }
            }
        }
        #endregion ValidarPosicionInicial
        #region ValidarCarroHornos
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
                DataTable dtRes = this.oDA.ObtenerPiezasCarroHornos(this.lu.CodPlanta, this.lu.CodCarro);
                // Validacion para evitar un BUG de nivel BLOCKER; Se supone que este escenario no deberia ocurrir
                if (dtRes == null) throw new Exception("Captura de carro invalida, contacte al administrador\n o capture otro carro.");
                //Control de Cambio: Quitar validacion de carro Ocupado
                //if (dtRes.Rows.Count > 0)
                //{
                //    this.encabezado.Mensaje = "Carro " + this.lu.CodCarro.ToString() + " ocupado";

                //    this.lu.CodCarro = -1;
                //    return false;
                //}
                //else
                {
                    this.encabezado.Mensaje = String.Empty;
                    return true;
                }
            }
        }
        #endregion ValidarCarroHornos
        #region MoldeBaseControls
        private void MoldeBaseControls(bool bOcultarMoldeBase)
        {
            this.lbPosicionInicial.Visible = bOcultarMoldeBase;
            this.txPosicionInicial.Visible = bOcultarMoldeBase;
            this.rbAscendente.Visible = bOcultarMoldeBase;
            this.rbDescendente.Visible = bOcultarMoldeBase;
            //this.cbxMolde.Visible = !bOcultarMoldeBase;
            //this.cbxBase.Visible = !bOcultarMoldeBase;
            //this.lblBase.Visible = !bOcultarMoldeBase;
            //if (!bOcultarMoldeBase)
            //    this.lbPosicionInicial.Text = "Molde:";
            //else
            //    this.lbPosicionInicial.Text = "Pos. Inicial:";
        }
        #endregion
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
                this.encabezado.Titulo = this.lu.DesProceso + " - Captura inicial";
                this.tbxColor.Visible = false;
                // Obtener periodo del timer.
                this.encabezado.Conexion = EstadoConexion.Offline;
                this.iPeriodoActualizacion = this.oDA0.ObtenerPeriodoActualizacion(this.lu.CodProceso, this.lu.CodPantalla);
                
                // Verificar si es proceso 'Vaciado' y activar la bandera para forzar modo Offline.
                if (this.lu.CodProceso == this.oDA0.ObtenerCodProcesoVaciado())
                {
                    this.bForzarOffline = true;
                }
                else
                {
                    this.bForzarOffline = false;
                }

                if (this.lu.CodProceso == this.oDA0.ObtenerCodProcesoVaciado())
                {
                    this.txOperador.ReadOnly = false;
                    this.txOperador.Enabled = true;
                    //
                    this.lbMaquina.Text = "Banco:";
                    this.lbPosicionInicial.Visible = true;
                    this.txPosicionInicial.Visible = true;
                    this.lbColor.Visible = false;
                    this.cbxColor.Visible = false;
                    this.rbAscendente.Visible = true;
                    this.rbDescendente.Visible = true;
                    this.lbCarro.Visible = false;
                    this.txCarro.Visible = false;
                }
                else if (this.lu.CodProceso == this.oDA0.ObtenerCodProcesoSecado())
                {
                    this.lbMaquina.Text = "Secador:";
                    this.lbPosicionInicial.Visible = false;
                    this.txPosicionInicial.Visible = false;
                    this.lbColor.Visible = false;
                    this.cbxColor.Visible = false;
                    this.rbAscendente.Visible = false;
                    this.rbDescendente.Visible = false;
                    this.lbCarro.Visible = false;
                    this.txCarro.Visible = false;
                }
                else if (this.lu.CodProceso == this.oDA0.ObtenerCodProcesoRevisado())
                {
                    this.lbMaquina.Text = "Caseta:";
                    this.lbPosicionInicial.Visible = false;
                    this.txPosicionInicial.Visible = false;
                    this.lbColor.Visible = false;
                    this.cbxColor.Visible = false;
                    this.rbAscendente.Visible = false;
                    this.rbDescendente.Visible = false;
                    this.lbCarro.Visible = false;
                    this.txCarro.Visible = false;
                }
                else if (this.lu.CodProceso == this.oDA0.ObtenerCodProcesoEsmaltado())
                {
                    this.lbMaquina.Text = "Caseta:";
                    this.lbPosicionInicial.Visible = false;
                    this.txPosicionInicial.Visible = false;
                    this.lbColor.Visible = true;
                    this.cbxColor.Visible = true;
                    this.tbxColor.Visible = true;
                    this.rbAscendente.Visible = false;
                    this.rbDescendente.Visible = false;
                    this.lbCarro.Visible = false;
                    this.txCarro.Visible = false;
                    this.iTiempoEnMinutosCapturaColor = this.oDA0.ObtenerTiempoEnMinutosCapturaColor();
                    this.iTiempoEnMinutosCapturaColor = (this.iTiempoEnMinutosCapturaColor <= 0) ? 20 : this.iTiempoEnMinutosCapturaColor;
                }
                else if (this.lu.CodProceso == this.oDA0.ObtenerCodProcesoHornos())
                {
                    this.lbMaquina.Text = "Horno:";
                    this.lbPosicionInicial.Visible = false;
                    this.txPosicionInicial.Visible = false;
                    this.lbColor.Visible = false;
                    this.cbxColor.Visible = false;
                    this.rbAscendente.Visible = false;
                    this.rbDescendente.Visible = false;
                    this.lbCarro.Visible = true;
                    if ((this.lu.CodPlanta == 1 || this.lu.CodPlanta == 2 || this.lu.CodPlanta == 3))
                        this.txCarro.Visible = false;
                    else
                        this.txCarro.Visible = true;
                }
                else if (this.lu.CodProceso == this.oDA0.ObtenerCodProcesoEmpaque())
                {
                    this.lbMaquina.Text = "Caseta:";
                    this.lbPosicionInicial.Visible = false;
                    this.txPosicionInicial.Visible = false;
                    this.lbColor.Visible = false;
                    this.cbxColor.Visible = false;
                    this.rbAscendente.Visible = false;
                    this.rbDescendente.Visible = false;
                    this.lbCarro.Visible = false;
                    this.txCarro.Visible = false;
                }
                else if (this.lu.CodProceso == this.oDA0.ObtenerCodProcesoAuditoria())
                {
                    this.lbMaquina.Text = "Caseta:";
                    this.lbPosicionInicial.Visible = false;
                    this.txPosicionInicial.Visible = false;
                    this.lbColor.Visible = false;
                    this.cbxColor.Visible = false;
                    this.rbAscendente.Visible = false;
                    this.rbDescendente.Visible = false;
                    this.lbCarro.Visible = false;
                    this.txCarro.Visible = false;
                    ///----------------------------
                    //this.cbxCentroTrabajo.Visible = false;
                    //this.lbCentroTrabajo.Visible = false;
                }

                // iCodOperador, solo es temporal para el proceso 'Vaciado'.
                this.iCodOperador = this.lu.CodEmpleado;
                this.iCodSupervisor = this.lu.CodSupervisor;

                this.lu.CodSupervisor = -1;
                this.lu.CodCentroTrabajo = -1;
                this.lu.CodConfigBanco = -1;
                this.lu.CodMaquina = -1;
                this.lu.ClaveMaquina = string.Empty;
                this.lu.DesMaquina = string.Empty;
                this.lu.CodTipoMaquina = -1;
                this.lu.DesTipoMaquina = string.Empty;
                this.lu.PosInicial = -1;
                this.lu.Ascendente = true;
                this.lu.CodColor = -1;
                this.lu.ClaveColor = string.Empty;
                this.lu.DesColor = string.Empty;
                this.lu.CodCarro = -1;

                //
                DataTable dtObj = null;
                DataRow dr = null;
                ComboBox cbxObj = null;

                // Obtener Clave del Operador.
                int iClaveEmpleadoMFG = this.oDA.ObtenerClaveEmpleadoMFG(this.iCodOperador, this.bForzarOffline);
                this.txOperador.Text = iClaveEmpleadoMFG.ToString();

                // Obtener el Supervidor por defecto.
                dtObj = this.oDA.ObtenerSupervisorPorDefecto(this.lu.CodUsuario, this.bForzarOffline);
                if (dtObj != null && dtObj.Rows.Count > 0)
                    this.txSupervisor.Text = dtObj.Rows[0]["ClaveEmpleadoMFG"].ToString();
                else this.txSupervisor.Text = "";
                // Obtener Centros Trabajo.
                dtObj = this.oDA.ObtenerCentrosTrabajo(this.lu.CodPlanta, this.lu.CodProceso, this.bForzarOffline);
                dr = dtObj.NewRow();
                dr["CodCentroTrabajo"] = -1;
                dr["ClaveCentroTrabajo"] = "";
                dr["DesCentroTrabajo"] = "Seleccionar...";
                dtObj.Rows.InsertAt(dr, 0);
                cbxObj = this.cbxCentroTrabajo;
                cbxObj.ValueMember = "CodCentroTrabajo";
                cbxObj.DisplayMember = "DesCentroTrabajo";
                cbxObj.SelectedIndexChanged -= new EventHandler(this.cbxCentroTrabajo_SelectedIndexChanged);
                cbxObj.DataSource = dtObj;
                cbxObj.SelectedValue = -1;
                cbxObj.SelectedIndexChanged += new EventHandler(this.cbxCentroTrabajo_SelectedIndexChanged);

                // si el proceso es Esmaltado, obtener Colores.
                if (this.lu.CodProceso == this.oDA0.ObtenerCodProcesoEsmaltado())
                {
                    dtObj = this.oDA0.ObtenerColores();
                    dr = dtObj.NewRow();
                    dr["CodColor"] = -1;
                    dr["ClaveColor"] = "";
                    dr["DesColor"] = "Seleccionar...";
                    dtObj.Rows.InsertAt(dr, 0);
                    cbxObj = this.cbxColor;
                    cbxObj.ValueMember = "CodColor";
                    cbxObj.DisplayMember = "DesColor";
                    cbxObj.SelectedIndexChanged -= new EventHandler(this.cbxColor_SelectedIndexChanged);
                    cbxObj.DataSource = dtObj;
                    cbxObj.SelectedValue = -1;
                    cbxObj.SelectedIndexChanged += new EventHandler(this.cbxColor_SelectedIndexChanged);
                }

                if (this.lu.CodProceso == this.oDA0.ObtenerCodProcesoVaciado())
                {
                    this.txOperador.SelectAll();
                    this.txOperador.Focus();
                }
                else
                {
                    this.txSupervisor.SelectAll();
                    this.txSupervisor.Focus();
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
                Boolean isServiceAvailable = this.oDA0.EstaServicioDisponible();
                if (isServiceAvailable)
                {
                    this.encabezado.Conexion = EstadoConexion.Online;

                    // Verificar si hay actualizacion en datos.
                    DateTime dtFechaUltimaActualizacion = this.oDA0.ObtenerFechaUltimaActualizacion(this.lu.CodProceso, this.lu.CodPantalla);
                    Boolean isScreenChange = this.oDA0.ExisteCambioEnProcesoPantalla(this.lu.CodProceso, this.lu.CodPantalla, dtFechaUltimaActualizacion);
                    if (isScreenChange)
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

        #region txOperador_KeyPress
        private void txOperador_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                // Validar ingreso del Enter.
                if (e.KeyChar == 13)
                {
                    TextBox txObj = (TextBox)sender;

                    if (this.ValidarCodOperador(txObj.Text))
                    {
                        this.txSupervisor.Focus();
                    }
                    else
                    {
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
        #endregion txOperador_KeyPress
        #region txSupervisor_KeyPress
        private void txSupervisor_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                // Validar ingreso del Enter.
                if (e.KeyChar == 13)
                {
                    TextBox txObj = (TextBox)sender;

                    if (this.ValidarCodSupervisor(txObj.Text))
                    {
                        this.cbxCentroTrabajo.Focus();
                    }
                    else
                    {
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
        #endregion txSupervisor_KeyPress
        #region cbxCentroTrabajo_SelectedIndexChanged
        private void cbxCentroTrabajo_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                DataTable dtObj = null;
                DataRow dr = null;
                ComboBox cbxObj = null;

                cbxObj = (ComboBox)sender;
                this.lu.CodCentroTrabajo = Convert.ToInt32(cbxObj.SelectedValue);

                if (this.lu.CodCentroTrabajo == -1)
                {
                    this.encabezado.Mensaje = "Seleccione Centro de Trabajo";
                    cbxObj.Focus();
                }
                else
                {
                    dtObj = this.oDA.ObtenerMaquinas(this.lu.CodPlanta, this.lu.CodProceso, this.lu.CodCentroTrabajo, this.bForzarOffline);
                    dr = dtObj.NewRow();
                    dr["CodConfigBanco"] = -1;
                    dr["CodMaquina"] = -1;
                    dr["ClaveMaquina"] = "";
                    dr["DesMaquina"] = "Seleccionar...";
                    dr["CodTipoMaquina"] = -1;
                    dr["DesTipoMaquina"] = "Maquina";
                    dtObj.Rows.InsertAt(dr, 0);
                    cbxObj = this.cbxMaquina;
                    cbxObj.ValueMember = "CodMaquina";
                    cbxObj.DisplayMember = "DesMaquina";
                    cbxObj.SelectedIndexChanged -= new EventHandler(this.cbxMaquina_SelectedIndexChanged);
                    cbxObj.DataSource = dtObj;
                    cbxObj.SelectedValue = -1;
                    cbxObj.SelectedIndexChanged += new EventHandler(this.cbxMaquina_SelectedIndexChanged);

                    this.encabezado.Mensaje = String.Empty;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
            }
        }
        #endregion cbxCentroTrabajo_SelectedIndexChanged
        #region cbxCentroTrabajo_KeyPress
        private void cbxCentroTrabajo_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                // Validar ingreso del Enter.
                if (e.KeyChar == 13)
                {
                    ComboBox cbxObj = (ComboBox)sender;

                    if (this.lu.CodCentroTrabajo == -1)
                    {
                        cbxObj.Focus();
                    }
                    else
                    {
                        this.cbxMaquina.Focus();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
            }
        }
        #endregion cbxCentroTrabajo_KeyPress
        #region cbxMaquina_SelectedIndexChanged
        private void cbxMaquina_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                ComboBox cbxObj = (ComboBox)sender;
                this.lu.CodConfigBanco = Convert.ToInt32(((DataRowView)cbxObj.SelectedItem)["CodConfigBanco"]);
                this.lu.CodMaquina = Convert.ToInt32(cbxObj.SelectedValue);
                this.lu.ClaveMaquina = Convert.ToString(((DataRowView)cbxObj.SelectedItem)["ClaveMaquina"]);
                this.lu.DesMaquina = cbxObj.Text;
                this.lu.CodTipoMaquina = Convert.ToInt32(((DataRowView)cbxObj.SelectedItem)["CodTipoMaquina"]);
                this.lu.DesTipoMaquina = Convert.ToString(((DataRowView)cbxObj.SelectedItem)["DesTipoMaquina"]);
                if (this.lu.CodMaquina == -1)
                {
                    this.encabezado.Mensaje = "Seleccione " + this.lu.DesTipoMaquina;
                    cbxObj.Focus();
                }
                else
                {
                    // En el proceso 'Vaciado' mostrar mensaje de advertencia cuando se haya superado el limite de vaciadas.
                    if (this.lu.CodProceso == this.oDA0.ObtenerCodProcesoVaciado())
                    {
                        if (oDA.TieneConfiguracionCapturaVaciado(this.lu.CodPlanta, this.lu.CodMaquina))
                        {
                            MoldeBaseControls(false);
                            //this.cbxMolde.SelectedValue = -1;
                            //this.cbxBase.DataSource = null;
                            //this.cbxBase.Items.Clear();
                            this.lu.Ascendente = true;
                            this.lu.PosInicial = 1;
                        }
                        else
                        {
                            this.lu.CodMolde = -1;
                            this.lu.CodBase = -1;
                            MoldeBaseControls(true);
                        }
                        if (this.oDA.SuperoLimiteVaciadas(this.lu.CodConfigBanco, this.bForzarOffline))
                        {
                            MessageBox.Show("Se ha superado el limite de vaciadas", "Vaciado", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
                        }
                    }
                    this.encabezado.Mensaje = String.Empty;
                }
                //Validar si la caseta es de tanques
                //this.oDA.EsCasetaTanque()
                if (this.cbxMaquina.SelectedValue != null & this.lu != null & this.oDA0.ObtenerCodProcesoRevisado() == this.lu.CodProceso)
                {
                    if (this.oDA.TieneReglaPlanta(this.lu.CodPlanta) & this.oDA.EsCasetaTanque(((int)this.cbxMaquina.SelectedValue), 0, this.lu.CodProceso, this.lu.CodPlanta) & this.lu.DesProceso.IndexOf("Revisado", StringComparison.InvariantCultureIgnoreCase) >= 0)
                    {
                        DataTable dtObj = this.oDA0.ObtenerColores();
                        DataRow dr = dtObj.NewRow();
                        dr["CodColor"] = -1;
                        dr["ClaveColor"] = "";
                        dr["DesColor"] = "Seleccionar...";
                        dtObj.Rows.InsertAt(dr, 0);
                        cbxObj = this.cbxColor;
                        cbxObj.ValueMember = "CodColor";
                        cbxObj.DisplayMember = "DesColor";
                        cbxObj.SelectedIndexChanged -= new EventHandler(this.cbxColor_SelectedIndexChanged);
                        cbxObj.DataSource = dtObj;
                        cbxObj.SelectedValue = -1;
                        cbxObj.SelectedIndexChanged += new EventHandler(this.cbxColor_SelectedIndexChanged);
                        //Validar si la caseta es de tanques
                        this.lbColor.Visible = true;
                        this.cbxColor.Visible = true;
                    }
                    else
                    {
                        this.lbColor.Visible = false;
                        this.cbxColor.Visible = false;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
            }
        }
        #endregion cbxMaquina_SelectedIndexChanged
        #region cbxMaquina_KeyPress
        private void cbxMaquina_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                // Validar ingreso del Enter.
                if (e.KeyChar == 13)
                {
                    ComboBox cbxObj = (ComboBox)sender;
                    if (this.lu.CodMaquina == -1)
                    {
                        cbxObj.Focus();
                    }
                    else
                    {
                        if (this.lu.CodProceso == this.oDA0.ObtenerCodProcesoVaciado())
                        {
                            this.txPosicionInicial.SelectAll();
                            this.txPosicionInicial.Focus();
                        }
                        else if (this.lu.CodProceso == this.oDA0.ObtenerCodProcesoSecado())
                        {
                            this.btContinuar.Focus();
                        }
                        else if (this.lu.CodProceso == this.oDA0.ObtenerCodProcesoRevisado())
                        {
                            this.btContinuar.Focus();
                        }
                        else if (this.lu.CodProceso == this.oDA0.ObtenerCodProcesoEsmaltado())
                        {
                            //this.cbxColor.Focus();
                            this.tbxColor.Focus();
                        }
                        else if (this.lu.CodProceso == this.oDA0.ObtenerCodProcesoHornos())
                        {
                            if ((this.lu.CodPlanta == 1 || this.lu.CodPlanta == 2 || this.lu.CodPlanta == 3))
                                this.btContinuar.Focus();
                            else
                            {
                                this.txCarro.SelectAll();
                                this.txCarro.Focus();
                            }
                        }
                        else if (this.lu.CodProceso == this.oDA0.ObtenerCodProcesoClasificado())
                        {
                            //
                        }
                        else if (this.lu.CodProceso == this.oDA0.ObtenerCodProcesoEmpaque())
                        {
                            this.btContinuar.Focus();
                        }
                        else if (this.lu.CodProceso == this.oDA0.ObtenerCodProcesoAuditoria())
                        {
                            this.btContinuar.Focus();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
            }
        }
        #endregion cbxMaquina_KeyPress
        #region txPosicionInicial_KeyPress
        private void txPosicionInicial_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                // Validar ingreso del Enter.
                if (e.KeyChar == 13)
                {
                    TextBox txObj = (TextBox)sender;

                    if (this.ValidarPosicionInicial(txObj.Text))
                    {
                        this.rbAscendente.Focus();
                    }
                    else
                    {
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
        #endregion txPosicionInicial_KeyPress
        #region cbxColor_SelectedIndexChanged
        private void cbxColor_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                ComboBox cbxObj = (ComboBox)sender;

                this.lu.CodColor = Convert.ToInt32(cbxObj.SelectedValue);
                this.lu.ClaveColor = Convert.ToString(((DataRowView)cbxObj.SelectedItem)["ClaveColor"]);
                this.lu.DesColor = cbxObj.Text;

                if (this.lu.CodColor == -1)
                {
                    this.encabezado.Mensaje = "Seleccione Color";
                    cbxObj.Focus();
                }
                else
                {
                    this.encabezado.Mensaje = String.Empty;
                    this.btContinuar.Focus();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
            }
        }
        #endregion cbxColor_SelectedIndexChanged
        #region cbxColor_KeyPress
        private void cbxColor_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                // Validar ingreso del Enter.
                if (e.KeyChar == 13)
                {
                    ComboBox cbxObj = (ComboBox)sender;
                    if (this.lu.CodColor == -1)
                        cbxObj.Focus();
                    else 
                        this.btContinuar.Focus();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
            }
        }
        #endregion cbxColor_KeyPress
        #region txCarro_KeyPress
        private void txCarro_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                // Validar ingreso del Enter.
                if (e.KeyChar == 13)
                {
                    TextBox txObj = (TextBox)sender;

                    if (this.ValidarCarroHornos(txObj.Text))
                    {
                        this.btContinuar.Focus();
                    }
                    else
                    {
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
        #endregion txCarro_KeyPress
        #region rbDireccion_CheckedChanged
        private void rbDireccion_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (this.rbAscendente.Checked)
                {
                    this.lu.Ascendente = true;
                }
                else if (this.rbDescendente.Checked)
                {
                    this.lu.Ascendente = false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
            }
        }
        #endregion rbDireccion_CheckedChanged
        #region rbDireccion_KeyPress
        private void rbDireccion_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                // Validar ingreso del Enter.
                if (e.KeyChar == 13)
                {
                    this.btContinuar.Focus();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
            }
        }
        #endregion rbDireccion_KeyPress

        #region btContinuar_Click
        private void btContinuar_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.lu.CodProceso == this.oDA0.ObtenerCodProcesoVaciado())
                {
                    if (!this.ValidarCodOperador(this.txOperador.Text))
                    {
                        this.txOperador.SelectAll();
                        this.txOperador.Focus();
                        return;
                    }
                }
                if (!this.ValidarCodSupervisor(this.txSupervisor.Text))
                {
                    this.txSupervisor.SelectAll();
                    this.txSupervisor.Focus();
                    return;
                }
                if (this.lu.CodCentroTrabajo == -1 && this.cbxCentroTrabajo.Visible)
                {
                    this.encabezado.Mensaje = "Seleccione Centro Trab.";
                    this.cbxCentroTrabajo.Focus();
                    return;
                }
                if (this.lu.CodMaquina == -1)
                {
                    this.encabezado.Mensaje = "Seleccione " + this.lu.DesTipoMaquina;
                    this.cbxMaquina.Focus();
                    return;
                }

                if (this.lu.CodProceso == this.oDA0.ObtenerCodProcesoVaciado())
                {
                    DataTable dtRes = this.oDA.ObtenerOperador(iCodOperador, bForzarOffline);
                    if (dtRes != null && dtRes.Rows.Count > 0)
                        sOperadorName = dtRes.Rows[0]["nombre"].ToString() + " " + dtRes.Rows[0]["ap_paterno"].ToString();
                    if (MessageBox.Show("¿Desea Cargar la produccion al Empleado: " + sOperadorName + ", con numero: " + txOperador.Text + "? ", "Confirmación de operador.", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) == DialogResult.No)
                    {
                        this.txOperador.SelectAll();
                        this.txOperador.Focus();
                        return;
                    }
                    if (this.txPosicionInicial.Visible && !this.ValidarPosicionInicial(this.txPosicionInicial.Text))
                    {
                        this.txPosicionInicial.SelectAll();
                        this.txPosicionInicial.Focus();
                        return;
                    }
                    if (!this.rbAscendente.Checked && !this.rbDescendente.Checked && this.rbDescendente.Visible && this.rbAscendente.Visible)
                    {
                        this.encabezado.Mensaje = "Seleccione Direccion";
                        this.rbAscendente.Focus();
                        return;
                    }
                }
                if (this.lu.CodProceso == this.oDA0.ObtenerCodProcesoEsmaltado())
                {
                    if (this.lu.CodColor == -1)
                    {
                        this.encabezado.Mensaje = "Seleccione Color";
                        this.cbxColor.Focus();
                        return;
                    }
                }

                if (this.lu.CodProceso == this.oDA0.ObtenerCodProcesoHornos())
                {
                    if (!(this.lu.CodPlanta == 1 || this.lu.CodPlanta == 2 || this.lu.CodPlanta == 3))
                    {
                        if (!this.ValidarCarroHornos(this.txCarro.Text))
                        {
                            this.txCarro.SelectAll();
                            this.txCarro.Focus();
                            return;
                        }
                    }
                }
                if (this.lu.CodProceso == this.oDA0.ObtenerCodProcesoRevisado())
                {
                    if (this.cbxColor.SelectedValue != null & this.cbxMaquina.SelectedValue != null)
                        if (this.oDA.TieneReglaPlanta(this.lu.CodPlanta) & this.oDA.EsCasetaTanque(((int)this.cbxMaquina.SelectedValue), 0, this.lu.CodProceso, this.lu.CodPlanta) & this.lu.DesProceso.IndexOf("Revisado", StringComparison.InvariantCultureIgnoreCase) >= 0)
                            if (this.lu.CodColor == -1)
                            {
                                this.encabezado.Mensaje = "Seleccione Color";
                                this.cbxColor.Focus();
                                return;
                            }
                }

                //this.lu.CodEmpleado = this.iCodOperador;
                this.lu.CodSupervisor = this.iCodSupervisor;
                if (this.lu.CodConfigBanco < 1)
                {
                    oDA0.LogLocalIns("Configuracion de Banco", "Planta: " + this.lu.CodPlanta + ", Maquina: " + this.lu.CodMaquina + ", Centro de Tabajo: " + this.lu.CodCentroTrabajo +
                                                                ", Usuario: " + this.lu.CodUsuario + ", Operador: " + this.txOperador.Text + ", Supervisor: " + txSupervisor.Text);
                    this.lu.CodConfigBanco = this.oDA0.ObtenerConfigBaco(this.lu.CodPlanta, this.lu.CodProceso, Convert.ToInt32(this.cbxCentroTrabajo.SelectedValue), Convert.ToInt32(this.cbxMaquina.SelectedValue));
                    if (this.lu.CodConfigBanco == -1)
                    {
                        MessageBox.Show("No se ha podido crear configuracion valida para el proceso, salga de la pantalla e intente nuevamente. Si el problema persiste informe a su supervisor");
                        return;
                    }
                }
                this.oDA.ActualizarConfigHandHeld(DataAccess.eTipoConexion.Local,
                                                    this.iCodSupervisor,
                                                    this.iCodOperador,
                                                    this.lu.CodConfigBanco,
                                                    this.lu.CodConfigHandHeld);



                Form frmObj = null;

                if (this.lu.CodProceso == this.oDA0.ObtenerCodProcesoVaciado())
                {
                    frmObj = new a05_CapturaVaciado(this.lu);
                }
                else if (this.lu.CodProceso == this.oDA0.ObtenerCodProcesoSecado())
                {
                    frmObj = new a06_EntradaCarroSecador(this.lu);
                }
                else if (this.lu.CodProceso == this.oDA0.ObtenerCodProcesoRevisado())
                {
                    if (this.cbxColor.SelectedValue != null & this.cbxMaquina.SelectedValue != null)
                        if (this.oDA.TieneReglaPlanta(this.lu.CodPlanta) & this.oDA.EsCasetaTanque(((int)this.cbxMaquina.SelectedValue), 0, this.lu.CodProceso, this.lu.CodPlanta) & this.lu.DesProceso.IndexOf("Revisado", StringComparison.InvariantCultureIgnoreCase) >= 0)
                            frmObj = new a07_CapturaRevisado(this.lu, (int)this.cbxColor.SelectedValue, (string)this.cbxColor.Text);
                        else
                            frmObj = new a07_CapturaRevisado(this.lu);
                    else
                        frmObj = new a07_CapturaRevisado(this.lu);
                }
                else if (this.lu.CodProceso == this.oDA0.ObtenerCodProcesoEsmaltado())
                {
                    frmObj = new a08_CapturaEsmaltado(this.lu);
                    (frmObj as a08_CapturaEsmaltado).iTiempoEnMinutosCapturaColor = this.iTiempoEnMinutosCapturaColor;
                }
                else if (this.lu.CodProceso == this.oDA0.ObtenerCodProcesoHornos())
                {
                    frmObj = new a09_CapturaHornos(this.lu);
                }
                else if (this.lu.CodProceso == this.oDA0.ObtenerCodProcesoClasificado())
                {

                }
                else if (this.lu.CodProceso == this.oDA0.ObtenerCodProcesoEmpaque())
                {
                    frmObj = new a11_CapturaEmpaque(this.lu);
                }
                else if (this.lu.CodProceso == this.oDA0.ObtenerCodProcesoAuditoria())
                {
                    frmObj = new a12_CapturaAuditoria(this.lu.TipoAuditoria, this.lu);
                }

                frmObj.Show();
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
            }
        }
        #endregion btContinuar_Click
        #region btCancelar_Click
        private void btCancelar_Click(object sender, EventArgs e)
        {
            try
            {
                DialogResult dr = MessageBox.Show("¿Cancelar captura inicial?", "SCPP", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
                if (dr == DialogResult.Yes)
                {
                    //Regresar a Configuracion Inicial.
                    a03_ConfiguracionInicial frmObj = new a03_ConfiguracionInicial(this.lu);
                    frmObj.Show();
                    this.Close();
                }
                else
                {
                    if (this.lu.CodProceso == this.oDA0.ObtenerCodProcesoVaciado())
                    {
                        this.txOperador.SelectAll();
                        this.txOperador.Focus();
                    }
                    else
                    {
                        this.txSupervisor.SelectAll();
                        this.txSupervisor.Focus();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
            }
        }
        #endregion btCancelar_Click

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                a00_CargaDatos frmObj = new a00_CargaDatos(this.lu.CodPlanta, -1, true);
                frmObj.SetFormCalling(this);
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

        private void tbxColor_GotFocus(object sender, EventArgs e)
        {
            this.SetEnable(this.btContinuar);
        }

        #endregion methods

        private void tbxColor_LostFocus(object sender, EventArgs e)
        {
            if (Convert.ToString(this.cbxColor.SelectedValue).Trim() != string.Empty && Convert.ToString(this.cbxColor.SelectedValue).Trim() != "-1") return;
            this.SetDisEnable(this.btContinuar);
        }

        private void tbxColor_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (e.KeyChar == 13)
                {
                    DataTable dtColor = this.cbxColor.DataSource as DataTable;
                    bool flag = true;
                    do
                    {
                        if (!flag)
                        {
                            MessageBox.Show("No se encontró colores disponibles por favor contacte al administrador", "Captura Vaciado", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
                            a03_ConfiguracionInicial frmObj = new a03_ConfiguracionInicial(this.lu);
                            frmObj.Show();
                            this.Close();
                            return;
                        }
                        if (dtColor == null || dtColor.Rows.Count == 0)
                        {
                            SetComboColor();
                            dtColor = this.cbxColor.DataSource as DataTable;
                            flag = false;
                        }
                    } while (!flag);
                    DataRow[] rows = dtColor.Select("ClaveColor = '" + (sender as TextBox).Text + "'");
                    if (rows.Length == 0)
                        throw new Exception("No se encontró el color capturado.");
                    if (rows.Length > 0)
                        this.cbxColor.SelectedValue = rows[0]["CodColor"];
                }
                else
                    e.Handled = EsDidigo(e.KeyChar);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
                (sender as TextBox).SelectAll();
            }
        }
        private bool EsDidigo(char caracter)
        {
            return (((int)caracter) >= 48 && ((int)caracter) <= 57 || caracter == 8) ? false : true;
        }
        private void SetDisEnable(Control control)
        {
            control.Enabled = false;
        }
        private void SetEnable(Control control)
        {
            control.Enabled = true;
        }
        private void SetComboColor()
        {
            DataTable dtObj = null;
            DataRow drObj = null;
            ComboBox cbxObj = null;
            dtObj = this.oDA0.ObtenerColores();
            drObj = dtObj.NewRow();
            drObj["CodColor"] = -1;
            drObj["ClaveColor"] = "";
            drObj["DesColor"] = "Seleccionar...";
            dtObj.Rows.InsertAt(drObj, 0);
            cbxObj = this.cbxColor;
            cbxObj.ValueMember = "CodColor";
            cbxObj.DisplayMember = "DesColor";
            cbxObj.SelectedIndexChanged -= new EventHandler(this.cbxColor_SelectedIndexChanged);
            cbxObj.DataSource = dtObj;
            cbxObj.SelectedValue = -1;
            cbxObj.SelectedIndexChanged += new EventHandler(this.cbxColor_SelectedIndexChanged);
        }
    }
}