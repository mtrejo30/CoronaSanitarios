using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DA = LAMOSA.SCPP.Client.View.HandHeld.DataAccess;
using System.Threading;
using LAMOSA.SCPP.Client.View.HandHeld.User_Interface;

namespace LAMOSA.SCPP.Client.View.HandHeld
{
    public partial class a03_ConfiguracionInicial : Form
    {

        #region fields

        private LoginUsuario lu = null;
        private c00_Common oDA0 = new c00_Common();
        private c03_ConfiguracionInicial oDA = new c03_ConfiguracionInicial();

        private bool isThreadActive = false;
        private System.Windows.Forms.Timer trActualizarDatosServidor = new System.Windows.Forms.Timer();
        private int iPeriodoActualizacion = -1;
        private bool bFechaServidor = false;
        #endregion fields

        #region properties



        #endregion properties

        #region methods

        #region constructors and destructor

        public a03_ConfiguracionInicial(LoginUsuario lu)
        {
            InitializeComponent();
            //
            this.lu = lu;
            this.ConfigurarFormulario();
            this.ConfigurarPanelControles();
        }
        ~a03_ConfiguracionInicial()
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

            this.dtpFecha.ValueChanged += new EventHandler(this.dtpFecha_ValueChanged);
            this.dtpFecha.KeyPress += new KeyPressEventHandler(this.dtpFecha_KeyPress);
            this.cbxTurno.SelectedIndexChanged += new EventHandler(this.cbxTurno_SelectedIndexChanged);
            this.cbxTurno.KeyPress += new KeyPressEventHandler(this.cbxTurno_KeyPress);
            this.cbxProceso.SelectedIndexChanged += new EventHandler(this.cbxProceso_SelectedIndexChanged);
            this.cbxProceso.KeyPress += new KeyPressEventHandler(this.cbxProceso_KeyPress);
            this.cbxOpcion.SelectedIndexChanged += new EventHandler(this.cbxOpcion_SelectedIndexChanged);
            this.cbxOpcion.KeyPress += new KeyPressEventHandler(this.cbxOpcion_KeyPress);

            this.btContinuar.Click += new EventHandler(this.btContinuar_Click);

            // Configuracion del Timer.
            //this.trActualizarDatosServidor.Interval = 100;
            //this.trActualizarDatosServidor.Enabled = true;
            //this.trActualizarDatosServidor.Tick += new EventHandler(this.trActualizarDatosServidor_Tick);
        }
        #endregion ConfigurarPanelControles

        #region EnviarDatosProceso
        private bool EnviarDatosProceso(int iCodProceso)
        {
            c00_Transacciones ct = null;
            try
            {
                ct = new c00_Transacciones(lu);
                if (iCodProceso == oDA0.ObtenerCodProcesoAuditoria())
                {
                    return ct.EnviarDatosAuditoria();
                }
                //else if (iCodProceso == oDA0.ObtenerCodProcesoClasificado())
                //{ 
                //}
                else if (iCodProceso == oDA0.ObtenerCodProcesoEmpaque())
                {
                    return ct.EnviarDatosEmpaque();
                }
                else if (iCodProceso == oDA0.ObtenerCodProcesoEsmaltado())
                {
                    return ct.EnviarDatosEsmaltado();
                }
                else if (iCodProceso == oDA0.ObtenerCodProcesoHornos())
                {
                    return ct.EnviarDatosHornos();
                }
                else if (iCodProceso == oDA0.ObtenerCodProcesoInventario())
                {
                    return ct.EnviarDatosInventario();
                }
                else if (iCodProceso == oDA0.ObtenerCodProcesoRevisado())
                {
                    return ct.EnviarDatosRevisado();
                }
                else if (iCodProceso == oDA0.ObtenerCodProcesoSecado())
                {
                    return ct.EnviarDatosSecado();
                }
                else if (iCodProceso == oDA0.ObtenerCodProcesoVaciado())
                {
                    return ct.EnviarDatosVaciado();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                ct = null;
            }
            return false;
        }
        #endregion EnviarDatosProceso
        #region ExisteDatosPendienteEnviar
        private bool ExisteDatosPendienteEnviar(int iCodProceso)
        {
            c00_Transacciones ct = null;
            DataTable dtDatosProceso = null;
            try
            {
                ct = new c00_Transacciones(lu);
                dtDatosProceso = ct.ObtenerPiezaTransaccionLocal();
                if (dtDatosProceso != null)
                    if (dtDatosProceso.Rows.Count > 0) return true;
                dtDatosProceso = ct.ObtenerActualizacionPieza();
                if (dtDatosProceso != null)
                    if (dtDatosProceso.Rows.Count > 0) return true;
                if (iCodProceso == oDA0.ObtenerCodProcesoAuditoria())
                {
                    dtDatosProceso = ct.ObtenerActualizacionTarimaPieza();
                    if (dtDatosProceso != null)
                        if (dtDatosProceso.Rows.Count > 0) return true;
                    //ct.EnviarDatosAuditoria()
                }
                //else if (iCodProceso == oDA0.ObtenerCodProcesoClasificado())
                //{ 
                //}
                else if (iCodProceso == oDA0.ObtenerCodProcesoEmpaque())
                {
                    dtDatosProceso = ct.ObtenerTarimaPiezaLocal();
                    if (dtDatosProceso != null)
                        if (dtDatosProceso.Rows.Count > 0) return true;
                    //ct.EnviarDatosEmpaque();
                }
                else if (iCodProceso == oDA0.ObtenerCodProcesoEsmaltado())
                {
                    //ct.EnviarDatosEsmaltado();
                }
                else if (iCodProceso == oDA0.ObtenerCodProcesoHornos())
                {
                    dtDatosProceso = ct.ObtenerCarroPiezaQuemadoLocal();
                    if (dtDatosProceso != null)
                        if (dtDatosProceso.Rows.Count > 0) return true;
                    //ct.EnviarDatosHornos();
                }
                else if (iCodProceso == oDA0.ObtenerCodProcesoInventario())
                {
                    dtDatosProceso = ct.ObtenerInventarioProceso();
                    if (dtDatosProceso != null)
                        if (dtDatosProceso.Rows.Count > 0) return true;
                    dtDatosProceso = ct.ObtenerInsertarPiezaInventario();
                    if (dtDatosProceso != null)
                        if (dtDatosProceso.Rows.Count > 0) return true;
                    //ct.EnviarDatosInventario();
                }
                else if (iCodProceso == oDA0.ObtenerCodProcesoRevisado())
                {
                    //ct.EnviarDatosRevisado();
                }
                else if (iCodProceso == oDA0.ObtenerCodProcesoSecado())
                {
                    dtDatosProceso = ct.ObtenerPiezaTransaccionSecadorLocal();
                    if (dtDatosProceso != null)
                        if (dtDatosProceso.Rows.Count > 0) return true;
                    dtDatosProceso = ct.ObtenerCarroPiezaEliminados();
                    if (dtDatosProceso != null)
                        if (dtDatosProceso.Rows.Count > 0) return true;
                    //ct.EnviarDatosSecado();
                }
                else if (iCodProceso == oDA0.ObtenerCodProcesoVaciado())
                {
                    dtDatosProceso = ct.ObtenerPiezaLocal();
                    if (dtDatosProceso != null)
                        if (dtDatosProceso.Rows.Count > 0) return true;
                    dtDatosProceso = ct.ObtenerActualizacionVaciadasAcumuladas();
                    if (dtDatosProceso != null)
                        if (dtDatosProceso.Rows.Count > 0) return true;
                    dtDatosProceso = ct.ObtenerCarroPiezaLocal();
                    if (dtDatosProceso != null)
                        if (dtDatosProceso.Rows.Count > 0) return true;
                    //ct.EnviarDatosVaciado();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                ct = null;
                if (dtDatosProceso != null) dtDatosProceso.Dispose();
                dtDatosProceso = null;
            }
            return false;
        }
        #endregion ExisteDatosPendienteEnviar

        #region Form_Load
        private void CargaProcesoPorRol()
        {
            DataTable dtObj = null;
            DataRow drObj = null;
            ComboBox cbxObj = null;
            dtObj = this.oDA.ObtenerProcesosPorRol(this.lu.CodRol);
            drObj = dtObj.NewRow();
            drObj["CodProceso"] = -1;
            drObj["DesProceso"] = "Seleccionar...";
            dtObj.Rows.InsertAt(drObj, 0);
            cbxObj = this.cbxProceso;
            cbxObj.ValueMember = "CodProceso";
            cbxObj.DisplayMember = "DesProceso";
            cbxObj.SelectedIndexChanged -= new EventHandler(this.cbxProceso_SelectedIndexChanged);
            cbxObj.DataSource = dtObj;
            cbxObj.SelectedValue = -1;
            cbxObj.SelectedIndexChanged += new EventHandler(this.cbxProceso_SelectedIndexChanged);

        }
        #endregion common
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
                this.encabezado.Titulo = "Configuración Inicial";

                // Obtener periodo del timer.
                this.encabezado.Conexion = EstadoConexion.Offline;
                this.iPeriodoActualizacion = this.oDA0.ObtenerPeriodoActualizacion(0, 3);

                this.lu.Fecha = DateTime.Now;
                this.lu.CodTurno = -1;
                this.lu.DesTurno = string.Empty;
                this.lu.CodProceso = -1;
                this.lu.DesProceso = string.Empty;
                this.lu.CodPantalla = -1;
                this.lu.CodConfigHandHeld = -1;

                DateTime dtFechaServidor = oDA0.ObtenerFechaServidor();
                this.lu.Fecha = this.dtpFecha.Value = (dtFechaServidor == DateTime.MinValue) ? DateTime.Now : dtFechaServidor;
                //this.bFechaServidor = dtFechaServidor == DateTime.MinValue ? false : true;

                DataTable dtObj = null;
                DataRow drObj = null;
                ComboBox cbxObj = null;

                dtObj = this.oDA.ObtenerTurnos();
                drObj = dtObj.NewRow();
                drObj["CodTurno"] = -1;
                drObj["DesTurno"] = "Seleccionar...";
                dtObj.Rows.InsertAt(drObj, 0);
                cbxObj = this.cbxTurno;
                cbxObj.ValueMember = "CodTurno";
                cbxObj.DisplayMember = "DesTurno";
                cbxObj.SelectedIndexChanged -= new EventHandler(this.cbxTurno_SelectedIndexChanged);
                cbxObj.DataSource = dtObj;
                cbxObj.SelectedValue = -1;
                cbxObj.SelectedIndexChanged += new EventHandler(this.cbxTurno_SelectedIndexChanged);

                CargaProcesoPorRol();
                //dtObj = this.oDA.ObtenerProcesosPorRol(this.lu.CodRol);
                //drObj = dtObj.NewRow();
                //drObj["CodProceso"] = -1;
                //drObj["DesProceso"] = "Seleccionar...";
                //dtObj.Rows.InsertAt(drObj, 0);
                //cbxObj = this.cbxProceso;
                //cbxObj.ValueMember = "CodProceso";
                //cbxObj.DisplayMember = "DesProceso";
                //cbxObj.SelectedIndexChanged -= new EventHandler(this.cbxProceso_SelectedIndexChanged);
                //cbxObj.DataSource = dtObj;
                //cbxObj.SelectedValue = -1;
                //cbxObj.SelectedIndexChanged += new EventHandler(this.cbxProceso_SelectedIndexChanged);

                this.dtpFecha.Focus();
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
            System.Windows.Forms.Timer trObj = (System.Windows.Forms.Timer)sender;
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

        #region dtpFecha_ValueChanged
        private void dtpFecha_ValueChanged(object sender, EventArgs e)
        {
            try
            {
                DateTimePicker dtpObj = (DateTimePicker)sender;
                this.lu.Fecha = dtpObj.Value;
                this.bFechaServidor = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
            }
        }
        #endregion dtpFecha_ValueChanged
        #region dtpFecha_KeyPress
        private void dtpFecha_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                // Validar ingreso del Enter.
                if (e.KeyChar == 13)
                {
                    this.cbxTurno.Focus();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
            }
        }
        #endregion dtpFecha_KeyPress
        #region cbxTurno_SelectedIndexChanged
        private void cbxTurno_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                ComboBox cbxObj = (ComboBox)sender;

                this.lu.CodTurno = Convert.ToInt32(cbxObj.SelectedValue);
                this.lu.DesTurno = cbxObj.Text;

                if (this.lu.CodTurno == -1)
                {
                    this.encabezado.Mensaje = "Seleccione Turno";

                    cbxObj.Focus();
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
        #endregion cbxTurno_SelectedIndexChanged
        #region cbxTurno_KeyPress
        private void cbxTurno_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                // Validar ingreso del Enter.
                if (e.KeyChar == 13)
                {
                    ComboBox cbxObj = (ComboBox)sender;

                    if (this.lu.CodTurno == -1)
                    {
                        this.encabezado.Mensaje = "Seleccione Turno";

                        cbxObj.Focus();
                    }
                    else
                    {
                        this.encabezado.Mensaje = String.Empty;

                        this.cbxProceso.Focus();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
            }
        }
        #endregion cbxTurno_KeyPress
        #region cbxProceso_SelectedIndexChanged
        private void cbxProceso_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                ComboBox cbxObj = (ComboBox)sender;

                this.lu.CodProceso = Convert.ToInt32(cbxObj.SelectedValue);
                this.lu.DesProceso = cbxObj.Text;

                if (this.lu.CodProceso == -1)
                {
                    this.encabezado.Mensaje = "Seleccione Proceso";

                    cbxObj.Focus();
                }
                else
                {
                    this.encabezado.Mensaje = String.Empty;

                    DataTable dtObj = null;
                    DataRow drObj = null;

                    dtObj = this.oDA.ObtenerPantallasProceso(this.lu.CodProceso);
                    drObj = dtObj.NewRow();
                    drObj["CodPantalla"] = -1;
                    drObj["DesPantalla"] = "Seleccionar...";
                    dtObj.Rows.InsertAt(drObj, 0);

                    // Agregar la opcion de enviar Datos de Vaciado.
                    if (this.lu.CodProceso == this.oDA0.ObtenerCodProcesoVaciado())
                    {
                        drObj = dtObj.NewRow();
                        drObj["CodPantalla"] = 500;
                        drObj["DesPantalla"] = "Enviar Datos Vaciado";
                        dtObj.Rows.Add(drObj);
                    }
                    // Control de Cambio 311, Agregar Opcion Consulta de Carros Pendientes Por entrar al secador, solo al proceso de Secado.
                    if (this.lu.CodProceso == this.oDA0.ObtenerCodProcesoSecado())
                    {
                        drObj = dtObj.NewRow();
                        drObj["CodPantalla"] = 502;
                        drObj["DesPantalla"] = "Carros Pendientes";
                        dtObj.Rows.Add(drObj);
                    }
                    // Control de Cambio 312, Agregar Opcion Consulta de Pieza(Kardex Sintetisado), con exception de Vaciado.
                    if (this.lu.CodProceso != this.oDA0.ObtenerCodProcesoVaciado())
                    {
                        drObj = dtObj.NewRow();
                        drObj["CodPantalla"] = 501;
                        drObj["DesPantalla"] = "Consulta de Pieza";
                        dtObj.Rows.Add(drObj);
                    }
                    // Agregar la opcion de Baja de Pieza.
                    drObj = dtObj.NewRow();
                    drObj["CodPantalla"] = 6;
                    drObj["DesPantalla"] = "Baja de Pieza";
                    dtObj.Rows.Add(drObj);

                    cbxObj = this.cbxOpcion;
                    cbxObj.ValueMember = "CodPantalla";
                    cbxObj.DisplayMember = "DesPantalla";
                    cbxObj.SelectedIndexChanged -= new EventHandler(this.cbxOpcion_SelectedIndexChanged);
                    cbxObj.DataSource = dtObj;
                    cbxObj.SelectedValue = -1;
                    cbxObj.SelectedIndexChanged += new EventHandler(this.cbxOpcion_SelectedIndexChanged);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
            }
        }
        #endregion cbxProceso_SelectedIndexChanged
        #region cbxProceso_KeyPress
        private void cbxProceso_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                // Validar ingreso del Enter.
                if (e.KeyChar == 13)
                {
                    ComboBox cbxObj = (ComboBox)sender;

                    if (this.lu.CodProceso == -1)
                    {
                        this.encabezado.Mensaje = "Seleccione Proceso";

                        cbxObj.Focus();
                    }
                    else
                    {
                        this.encabezado.Mensaje = String.Empty;

                        this.cbxOpcion.Focus();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
            }
        }
        #endregion cbxProceso_KeyPress
        #region cbxOpcion_SelectedIndexChanged
        private void cbxOpcion_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                ComboBox cbxObj = (ComboBox)sender;

                this.lu.CodPantalla = Convert.ToInt32(cbxObj.SelectedValue);

                if (this.lu.CodPantalla == -1)
                {
                    this.encabezado.Mensaje = "Seleccione Opción";

                    cbxObj.Focus();
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
        #endregion cbxOpcion_SelectedIndexChanged
        #region cbxOpcion_KeyPress
        private void cbxOpcion_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                // Validar ingreso del Enter.
                if (e.KeyChar == 13)
                {
                    ComboBox cbxObj = (ComboBox)sender;

                    if (this.lu.CodPantalla == -1)
                    {
                        this.encabezado.Mensaje = "Seleccione Opción";

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
        #endregion cbxOpcion_KeyPress

        #region btContinuar_Click
        private void btContinuar_Click(object sender, EventArgs e)
        {
            try
            {
                /*
                if (this.lu.Fecha == DateTime.MinValue)
                {
                    this.encabezado.Mensaje = "Seleccione Fecha valida";
                    this.dtpFecha.Focus();
                    return;
                }
                */
                if (this.lu.CodTurno == -1)
                {
                    this.encabezado.Mensaje = "Seleccione Turno";
                    this.cbxTurno.Focus();
                    return;
                }
                if (this.lu.CodProceso == -1)
                {
                    this.encabezado.Mensaje = "Seleccione Proceso";
                    this.cbxProceso.Focus();
                    return;
                }
                if (this.lu.CodPantalla == -1)
                {
                    this.encabezado.Mensaje = "Seleccione Opción";
                    this.cbxOpcion.Focus();
                    return;
                }

                int inventarioProceso = this.oDA.ExisteInventarioProcesoActivo();
                if (inventarioProceso != -1 && this.lu.CodProceso != this.oDA0.ObtenerCodProcesoInventario())
                {
                    this.encabezado.Mensaje = "Existe Inventario en proceso";
                    this.cbxProceso.Focus();
                    return;
                }
                if (inventarioProceso == -1 && this.lu.CodProceso == this.oDA0.ObtenerCodProcesoInventario())
                {
                    this.encabezado.Mensaje = "No hay Inventario en proceso";
                    this.cbxProceso.Focus();
                    //return;
                }

                this.encabezado.Mensaje = String.Empty;

                //if (this.ExisteDatosPendienteEnviar(this.lu.CodProceso))
                //{
                //    if (MessageBox.Show("¿Desea enviarlos? ", "Existen datos pendientes por enviar.", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) == DialogResult.Yes)
                //    {
                //        this.EnviarDatosProceso(this.lu.CodProceso);
                //    }
                //}

                // Si Tiene datos pendientes por enviar ó se eligio  'Enviar datos de Vaciado'. 
                if (this.ExisteDatosPendienteEnviar(this.lu.CodProceso) || this.lu.CodPantalla == 500)
                {
                    if (MessageBox.Show("¿Desea enviarlos? ", "Existen datos pendientes por enviar.", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) == DialogResult.Yes)
                    {
                        this.encabezado.Mensaje = "Enviando datos " + cbxProceso.SelectedText + "...";
                        this.Refresh();
                        bool bEnvioExitoso = this.EnviarDatosProceso(this.lu.CodProceso);
                        if (bEnvioExitoso)
                        {
                            MessageBox.Show("Envio exitoso", "SCPP", MessageBoxButtons.OK, MessageBoxIcon.None, MessageBoxDefaultButton.Button1);
                        }
                        else
                        {
                            MessageBox.Show("Envio fallido, intentar nuevamente", "SCPP", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
                        }

                        this.encabezado.Mensaje = String.Empty;
                        this.Refresh();
                    }
                    if (this.lu.CodPantalla == 500)
                        return;
                }
                // Insertar la Configuracion de la HandHeld.
                this.lu.CodConfigHandHeld = this.oDA.InsertarConfigHandHeld(DA.eTipoConexion.Local,
                                                                            this.lu.CodUsuario,
                                                                            this.lu.CodEmpleado,
                                                                            this.lu.CodSupervisor,
                                                                            this.lu.Fecha,
                                                                            this.lu.CodTurno,
                                                                            this.lu.CodPlanta,
                                                                            this.lu.CodProceso,
                                                                            null,
                                                                            null);


                //Inicia la actualizacion asincrona de tablas transaccionales
                try
                {
                    Thread tActualizacion = null;
                    tActualizacion = new Thread(new ThreadStart(ExecuteThread));
                    tActualizacion.Start();
                    isThreadActive = true;
                }
                catch { }




                // Abrir el formulario segun el proceso y opcion.
                Form frmObj = null;

                if (this.lu.CodPantalla == 8 || this.lu.CodPantalla == 10 || this.lu.CodPantalla == 11 ||
                    this.lu.CodPantalla == 12 || this.lu.CodPantalla == 13 || this.lu.CodPantalla == 15 ||
                    this.lu.CodPantalla == 17 || this.lu.CodPantalla == 18)
                {
                    frmObj = new a04_CapturaInicial(this.lu);
                }
                else if (this.lu.CodPantalla == 9)
                {
                    frmObj = new a05_ArmadoCarroSecador(this.lu);
                }
                else if (this.lu.CodPantalla == 16)
                {
                    //frmObj = new a11_ArmadoTarima(this.lu);
                    frmObj = new frmSetTarimaPieza(this.lu);
                    //(frmObj as frmSetTarimaPieza).CapturaConfiguracionInicial = this;
                    this.Hide();
                    (frmObj as frmSetTarimaPieza).ShowDialog();
                    this.Show();
                    return;
                }
                else if (this.lu.CodPantalla == 19)
                {
                    frmObj = new a13_CapturaInventario(this.lu);
                }
                else if (this.lu.CodPantalla == 7)
                {
                    frmObj = new a14_ReemplazoEtiqueta(this.lu);
                }
                else if (this.lu.CodPantalla == 20)
                {
                    frmObj = new a13_PiezaNuevaInventario(this.lu, false);
                }
                else if (this.lu.CodPantalla == 6) // Opcion de 'Baja de Pieza'
                {
                    frmObj = new a04_CapturaCodigoBarras(this.lu);
                }

                // Para proceso 'Auditoria'.
                if (this.lu.CodPantalla == 17)
                {
                    this.lu.TipoAuditoria = eTipoAuditoria.Al100PorCiento;
                }
                else if (this.lu.CodPantalla == 18)
                {
                    this.lu.TipoAuditoria = eTipoAuditoria.PorMuestreo;
                }
                /****************************************************************/
                else if (this.lu.CodPantalla == 501)
                {
                    frmObj = new frmKardex(lu);
                }
                else if (this.lu.CodPantalla == 502)
                {
                    frmObj = new frmCarroPendienteSecador(lu);
                }
                /****************************************************************/
                /*
                if (!bFechaServidor)
                {
                    if (MessageBox.Show("¿Esta es fecha con la que desea registrar las piezas: " + dtpFecha.Value.ToString("dd/MMM/yyyy") + "?", "Fecha Modificada.", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) == DialogResult.No)
                    {
                        dtpFecha.Focus();
                        return;
                    }
                }
                */
                frmObj.Show();
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
            }
        }
        #endregion btContinuar_Click
        private void ExecuteThread()
        {
            try
            {
                this.Invoke(new EventHandler(btnTransaccional_Click));
                Thread.Sleep(1000);
            }
            catch (ObjectDisposedException e) { }
        }
        private void btnCatalogo_Click(object sender, EventArgs e)
        {
            if (cbxProceso.SelectedValue.Equals(-1))
            {
                this.encabezado.Mensaje = "Seleccione Proceso";
                cbxProceso.Focus();
            }
            else
            {
                a00_CargaDatos frmObj = new a00_CargaDatos(this.lu.CodPlanta, this.lu.CodProceso, true);
                frmObj.SetFormCalling(this);
                frmObj.ShowDialog();
                frmObj.Dispose();
                this.Show();
                CargaProcesoPorRol();
            }
        }
        private void btnTransaccional_Click(object sender, EventArgs e)
        {
            if (cbxProceso.SelectedValue.Equals(-1))
            {
                this.encabezado.Mensaje = "Seleccione Proceso";
                cbxProceso.Focus();
            }
            else
            {
                a00_CargaDatos frmObj = new a00_CargaDatos(this.lu.CodPlanta, this.lu.CodProceso, false);
                if (!isThreadActive)
                {
                    frmObj.SetFormCalling(this);
                    frmObj.ShowDialog();
                    frmObj.Dispose();
                    this.Show();
                }
            }
        }

        #endregion event handlers



        #endregion methods

    }
}