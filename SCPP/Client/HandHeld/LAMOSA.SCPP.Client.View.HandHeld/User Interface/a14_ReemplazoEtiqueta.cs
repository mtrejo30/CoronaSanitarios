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
    public partial class a14_ReemplazoEtiqueta : Form
    {

        #region fields

        private LoginUsuario lu = null;
        private c00_Common oDA0 = new c00_Common();
        private c14_ReemplazoEtiqueta oDA = new c14_ReemplazoEtiqueta();

        private Timer trActualizarDatosServidor = new Timer();
        private int iPeriodoActualizacion = -1;

        private int iCodProcesoAct = -1;
        private int iCodProcesoAnt = -1;
        private string sCodBarras = string.Empty;
        private int iCodTipoModelo = -1;
        private int iCodModelo = -1;
        private int iCodColor = -1;
        private int iCodCalidad = -1;
        private string sClaveModelo = string.Empty;
        private string sClaveColor = string.Empty;
        private string sClaveCalidad = string.Empty;

        #endregion fields

        #region properties



        #endregion properties

        #region methods

        #region constructors and destructor

        public a14_ReemplazoEtiqueta(LoginUsuario lu)
        {
            InitializeComponent();
            //
            this.lu = lu;
            this.ConfigurarFormulario();
            this.ConfigurarPanelControles();
        }
        ~a14_ReemplazoEtiqueta()
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
            this.cbxTipoModelo.Enabled = true;
            this.cbxModelo.Enabled = true;
            this.cbxColor.Enabled = false;
            this.cbxCalidad.Enabled = false;
            this.txEtiqueta.KeyPress += new KeyPressEventHandler(this.txEtiqueta_KeyPress);
            this.cbxTipoModelo.SelectedIndexChanged += new EventHandler(this.cbxTipoModelo_SelectedIndexChanged);
            this.cbxTipoModelo.KeyPress += new KeyPressEventHandler(this.cbxTipoModelo_KeyPress);
            this.cbxModelo.SelectedIndexChanged += new EventHandler(this.cbxModelo_SelectedIndexChanged);
            this.cbxModelo.KeyPress += new KeyPressEventHandler(this.cbxModelo_KeyPress);
            this.cbxColor.SelectedIndexChanged += new EventHandler(this.cbxColor_SelectedIndexChanged);
            this.cbxColor.KeyPress += new KeyPressEventHandler(this.cbxColor_KeyPress);
            this.cbxCalidad.SelectedIndexChanged += new EventHandler(this.cbxCalidad_SelectedIndexChanged);
            this.cbxCalidad.KeyPress += new KeyPressEventHandler(this.cbxCalidad_KeyPress);

            this.btAceptar.Click += new EventHandler(this.btAceptar_Click);
            this.btTerminar.Click += new EventHandler(this.btTerminar_Click);

            //// Configuracion del Timer.
            //this.trActualizarDatosServidor.Interval = 100;
            //this.trActualizarDatosServidor.Enabled = true;
            //this.trActualizarDatosServidor.Tick += new EventHandler(this.trActualizarDatosServidor_Tick);
        }
        #endregion ConfigurarPanelControles

        #region ValidarPieza
        private Validacion ValidarPieza(string sCodBarras)
        {
            Validacion val = new Validacion();

            // Validar el codigo de barras no sea una cadena nula o vacia.
            if (string.IsNullOrEmpty(sCodBarras))
            {
                val.ValidacionExitosa = false;
                val.MensajeValidacion = "Capture Etiqueta";

                this.sCodBarras = string.Empty;
                
                return val;
            }

            // Validar no exista la pieza.
            if (this.oDA0.ObtenerCodPiezaCodBarras(sCodBarras, false) != -1)
            {
                val.ValidacionExitosa = false;
                val.MensajeValidacion = "Pieza en proceso";

                this.sCodBarras = string.Empty;
            }
            else
            {
                val.ValidacionExitosa = true;
                val.MensajeValidacion = String.Empty;

                this.sCodBarras = sCodBarras;
            }
            return val;
        }
        #endregion ValidarPieza

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
                this.encabezado.Titulo = "Reemplazo de Etiqueta";
                // Obtener periodo del timer.
                this.encabezado.Conexion = EstadoConexion.Offline;
                this.iPeriodoActualizacion = this.oDA0.ObtenerPeriodoActualizacion(this.lu.CodProceso, this.lu.CodPantalla);
                this.iCodProcesoAct = this.lu.CodProceso;
                this.iCodProcesoAnt = this.oDA0.ObtenerProcesoAnterior(this.iCodProcesoAct);
                this.txPlanta.Text = this.lu.DesPlanta;
                this.txProceso.Text = this.lu.DesProceso;
                DataTable dtObj = null;
                DataRow drObj = null;
                ComboBox cbxObj = null;
                // Llenar ComboBox 'Tipo Modelo'.
                dtObj = this.oDA0.ObtenerTiposModelo();
                drObj = dtObj.NewRow();
                drObj["CodTipoModelo"] = -1;
                drObj["ClaveTipoModelo"] = "";
                drObj["DesTipoModelo"] = "Seleccionar...";
                dtObj.Rows.InsertAt(drObj, 0);
                cbxObj = this.cbxTipoModelo;
                cbxObj.ValueMember = "CodTipoModelo";
                cbxObj.DisplayMember = "DesTipoModelo";
                cbxObj.SelectedIndexChanged -= new EventHandler(this.cbxTipoModelo_SelectedIndexChanged);
                cbxObj.DataSource = dtObj;
                cbxObj.SelectedValue = -1;
                cbxObj.SelectedIndexChanged += new EventHandler(this.cbxTipoModelo_SelectedIndexChanged);
                // Llenar ComboBox 'Color'.
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
                // Llenar ComboBox 'Calidad'.
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
                // Si el proceso es 'Secado' para adelante.
                if (this.lu.CodProceso >= this.oDA0.ObtenerCodProcesoSecado())
                {
                    this.cbxTipoModelo.Enabled = true;
                    this.cbxModelo.Enabled = true;
                }
                // Si el proceso es 'Esmaltado' para adelante.
                if (this.lu.CodProceso >= this.oDA0.ObtenerCodProcesoEsmaltado())
                {
                    this.cbxColor.Enabled = true;
                }
                // Si el proceso es 'Clasificacion' para adelante.
                if (this.lu.CodProceso >= this.oDA0.ObtenerCodProcesoClasificado())
                {
                    this.cbxCalidad.Enabled = true;
                }

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
                    Validacion val = null;

                    val = this.ValidarPieza(txObj.Text);
                    this.encabezado.Mensaje = val.MensajeValidacion;

                    if (val.ValidacionExitosa)
                    {
                        this.cbxTipoModelo.Focus();
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
        #endregion txEtiqueta_KeyPress

        #region cbxTipoModelo_SelectedIndexChanged
        private void cbxTipoModelo_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                ComboBox cbxObj = (ComboBox)sender;
                this.iCodTipoModelo = Convert.ToInt32(cbxObj.SelectedValue);
                DataTable dtObj = null;
                DataRow drObj = null;
                // Llenar ComboBox 'Modelo'.
                dtObj = this.oDA0.ObtenerModelos(this.iCodTipoModelo);
                drObj = dtObj.NewRow();
                drObj["CodModelo"] = -1;
                drObj["ClaveModelo"] = "";
                drObj["DesModelo"] = "Seleccionar...";
                dtObj.Rows.InsertAt(drObj, 0);
                cbxObj = this.cbxModelo;
                cbxObj.ValueMember = "CodModelo";
                cbxObj.DisplayMember = "DesModelo";
                cbxObj.SelectedIndexChanged -= new EventHandler(this.cbxModelo_SelectedIndexChanged);
                cbxObj.DataSource = dtObj;
                cbxObj.SelectedValue = -1;
                cbxObj.SelectedIndexChanged += new EventHandler(this.cbxModelo_SelectedIndexChanged);
                if (this.iCodTipoModelo == -1)
                {
                    this.encabezado.Mensaje = "Seleccione Tipo Modelo";
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
        #endregion cbxTipoModelo_SelectedIndexChanged
        #region cbxTipoModelo_KeyPress
        private void cbxTipoModelo_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                ComboBox cbxObj = (ComboBox)sender;

                // Validar ingreso del Enter.
                if (e.KeyChar == 13)
                {
                    if (this.iCodTipoModelo == -1)
                    {
                        cbxObj.Focus();
                    }
                    else
                    {
                        //this.cbxModelo.Focus();
                        this.tbxModelo.Focus();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
            }
        }
        #endregion cbxTipoModelo_KeyPress
        #region cbxModelo_SelectedIndexChanged
        private void cbxModelo_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                ComboBox cbxObj = (ComboBox)sender;
                this.iCodModelo = Convert.ToInt32(cbxObj.SelectedValue);
                this.sClaveModelo = Convert.ToString(((DataRowView)cbxObj.SelectedItem)["ClaveModelo"]);
                if (this.iCodModelo == -1)
                {
                    this.encabezado.Mensaje = "Seleccione Modelo";
                    cbxObj.Focus();
                }
                else
                {
                    this.encabezado.Mensaje = String.Empty;
                    if (this.lu.CodProceso >= this.oDA0.ObtenerCodProcesoEsmaltado())
                        //this.cbxColor.Focus();
                        this.tbxColor.Focus();
                    else
                        this.btAceptar.Focus();
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
                ComboBox cbxObj = (ComboBox)sender;

                // Validar ingreso del Enter.
                if (e.KeyChar == 13)
                {
                    if (this.iCodModelo == -1)
                    {
                        cbxObj.Focus();
                    }
                    else
                    {
                        if (this.lu.CodProceso >= this.oDA0.ObtenerCodProcesoEsmaltado())
                        {
                            this.cbxColor.Focus();
                        }
                        else
                        {
                            this.btAceptar.Focus();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
            }
        }
        #endregion cbxModelo_KeyPress
        #region cbxColor_SelectedIndexChanged
        private void cbxColor_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                ComboBox cbxObj = (ComboBox)sender;
                this.iCodColor = Convert.ToInt32(cbxObj.SelectedValue);
                this.sClaveColor = Convert.ToString(((DataRowView)cbxObj.SelectedItem)["ClaveColor"]);
                if (this.iCodColor == -1)
                {
                    this.encabezado.Mensaje = "Seleccione Color";
                    cbxObj.Focus();
                }
                else
                {
                    this.encabezado.Mensaje = String.Empty;
                    if (this.lu.CodProceso >= this.oDA0.ObtenerCodProcesoClasificado())
                        this.cbxCalidad.Focus();
                    else
                        this.btAceptar.Focus();
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
                ComboBox cbxObj = (ComboBox)sender;

                // Validar ingreso del Enter.
                if (e.KeyChar == 13)
                {
                    if (this.iCodColor == -1)
                    {
                        cbxObj.Focus();
                    }
                    else
                    {
                        if (this.lu.CodProceso >= this.oDA0.ObtenerCodProcesoClasificado())
                        {
                            this.cbxCalidad.Focus();
                        }
                        else
                        {
                            this.btAceptar.Focus();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
            }
        }
        #endregion cbxColor_KeyPress
        #region cbxCalidad_SelectedIndexChanged
        private void cbxCalidad_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                ComboBox cbxObj = (ComboBox)sender;

                this.iCodCalidad = Convert.ToInt32(cbxObj.SelectedValue);
                this.sClaveCalidad = Convert.ToString(((DataRowView)cbxObj.SelectedItem)["ClaveCalidad"]);

                if (this.iCodCalidad == -1)
                {
                    this.encabezado.Mensaje = "Seleccione Calidad";
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
        #endregion cbxCalidad_SelectedIndexChanged
        #region cbxCalidad_KeyPress
        private void cbxCalidad_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                ComboBox cbxObj = (ComboBox)sender;

                // Validar ingreso del Enter.
                if (e.KeyChar == 13)
                {
                    if (this.iCodCalidad == -1)
                    {
                        cbxObj.Focus();
                    }
                    else
                    {
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
                Validacion val = null;
                val = this.ValidarPieza(this.txEtiqueta.Text);
                this.encabezado.Mensaje = val.MensajeValidacion;
                if (!val.ValidacionExitosa)
                {
                    this.txEtiqueta.SelectAll();
                    this.txEtiqueta.Focus();
                    return;
                }
                if (this.iCodTipoModelo == -1)
                {
                    this.encabezado.Mensaje = "Seleccione Tipo Modelo";
                    this.cbxTipoModelo.Focus();
                    return;
                }
                if (this.iCodModelo == -1)
                {
                    this.encabezado.Mensaje = "Seleccione Modelo";
                    this.cbxModelo.Focus();
                    return;
                }
                if (this.lu.CodProceso >= this.oDA0.ObtenerCodProcesoEsmaltado())
                {
                    if (this.iCodColor == -1)
                    {
                        this.encabezado.Mensaje = "Seleccione Color";
                        this.cbxColor.Focus();
                        return;
                    }
                }
                if (this.lu.CodProceso >= this.oDA0.ObtenerCodProcesoClasificado())
                {
                    if (this.iCodCalidad == -1)
                    {
                        this.encabezado.Mensaje = "Seleccione Calidad";
                        this.cbxCalidad.Focus();
                        return;
                    }
                }
                // Validar existe combinacion MODELO-COLOR-CALIDAD en Articulos.
                string sClaveModelo = string.Empty;
                if (this.lu.CodProceso <= this.oDA0.ObtenerCodProcesoRevisado())
                {
                    sClaveModelo = this.sClaveModelo;
                }
                else if (this.lu.CodProceso <= this.oDA0.ObtenerCodProcesoHornos())
                {
                    sClaveModelo = this.sClaveModelo + "-" + this.sClaveColor;
                }
                else
                {
                    sClaveModelo = this.sClaveModelo + "-" + this.sClaveColor + "-" + this.sClaveCalidad;
                }

                int iCodModelo = -1;
                int procesoRevisado = this.oDA0.ObtenerCodProcesoRevisado();
                String msg = "lu.CodProceso:" + lu.CodProceso + "_procesoRevisado:" + procesoRevisado + "_sClaveModelo:"+sClaveModelo;
                if (this.lu.CodProceso <= procesoRevisado)
                {
                    iCodModelo = this.oDA0.ExisteModeloHastaRevisado(sClaveModelo);
                    msg+="_1";
                }
                else
                {
                    iCodModelo = this.oDA0.ExisteModeloDesdeEsmaltado(sClaveModelo);
                    msg+="_2";
                }
                if (iCodModelo == -1)
                {
                    this.encabezado.Mensaje = "Modelo-Color-Calidad invalido" + msg;
                    this.cbxModelo.Focus();
                    return;
                }
                // Ejecutar Transaccion.
                this.oDA.InsertarEtiquetaReemplazo(DA.eTipoConexion.Local,this.lu.CodPlanta, this.sCodBarras, this.iCodModelo, this.iCodColor, this.iCodCalidad,
                                                    this.iCodProcesoAnt, 1, this.lu.CodConfigHandHeld, this.lu.Fecha, this.iCodProcesoAct, this.lu.Fecha);

                // Limpiar Controles.
                this.txEtiqueta.Text = String.Empty;
                this.tbxModelo.Text = string.Empty;
                this.cbxTipoModelo.SelectedIndexChanged -= new EventHandler(this.cbxTipoModelo_SelectedIndexChanged);
                this.cbxTipoModelo.SelectedValue = -1;
                this.cbxTipoModelo.SelectedIndexChanged += new EventHandler(this.cbxTipoModelo_SelectedIndexChanged);

                this.cbxModelo.SelectedIndexChanged -= new EventHandler(this.cbxModelo_SelectedIndexChanged);
                this.cbxModelo.SelectedValue = -1;
                this.cbxModelo.SelectedIndexChanged += new EventHandler(this.cbxModelo_SelectedIndexChanged);

                this.cbxColor.SelectedIndexChanged -= new EventHandler(this.cbxColor_SelectedIndexChanged);
                this.cbxColor.SelectedValue = -1;
                this.cbxColor.SelectedIndexChanged += new EventHandler(this.cbxColor_SelectedIndexChanged);

                this.cbxCalidad.SelectedIndexChanged -= new EventHandler(this.cbxCalidad_SelectedIndexChanged);
                this.cbxCalidad.SelectedValue = -1;
                this.cbxCalidad.SelectedIndexChanged += new EventHandler(this.cbxCalidad_SelectedIndexChanged);

                this.txEtiqueta.Focus();
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
                if (new c00_Transacciones().EnviarDatosReemplazoEtiqueta()) 
                    MessageBox.Show("Informacion enviada correctamente", "Reemplazo", MessageBoxButtons.OK, MessageBoxIcon.None, MessageBoxDefaultButton.Button1);
                else
                    MessageBox.Show("Hubo un error al enviar tu informacion consulta a tu personal de sistemas.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
                
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

        private void tbxModelo_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (e.KeyChar == 13)
                {
                    DataTable dtModelo = this.cbxModelo.DataSource as DataTable;
                    bool flag = true;
                    do
                    {
                        if (!flag)
                        {
                            MessageBox.Show("No se encontró modelos disponibles por favor contacte al administrador", "Captura Vaciado", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
                            a03_ConfiguracionInicial frmObj = new a03_ConfiguracionInicial(this.lu);
                            frmObj.Show();
                            this.Close();
                            return;
                        }
                        if (dtModelo == null || dtModelo.Rows.Count == 0)
                        {
                            cbxTipoModelo_SelectedIndexChanged(this.cbxTipoModelo, e);
                            dtModelo = this.cbxModelo.DataSource as DataTable;
                            flag = false;
                        }
                    } while (!flag);
                    DataRow[] rows = dtModelo.Select("ClaveModelo = '" + (sender as TextBox).Text + "'");
                    if (rows.Length == 0)
                        throw new Exception("No se encontró el modelo capturado.");
                    if (rows.Length > 0)
                        this.cbxModelo.SelectedValue = rows[0]["CodModelo"];
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

        #endregion event handlers

        private void tbxModelo_GotFocus(object sender, EventArgs e)
        {
            this.SetEnable(this.btAceptar);
            this.SetEnable(this.btTerminar);
        }

        #endregion methods

        private void tbxModelo_LostFocus(object sender, EventArgs e)
        {
            if (Convert.ToString(this.cbxModelo.SelectedValue).Trim() != string.Empty && Convert.ToString(this.cbxModelo.SelectedValue).Trim() != "-1") return;
            this.SetDisEnable(this.btAceptar);
            this.SetDisEnable(this.btTerminar);
        }
        private void SetDisEnable(Control control)
        {
            control.Enabled = false;
        }
        private void SetEnable(Control control)
        {
            control.Enabled = true;
        }
        private bool EsDidigo(char caracter)
        {
            return (((int)caracter) >= 48 && ((int)caracter) <= 57 || caracter == 8) ? false : true;
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
        private void tbxColor_GotFocus(object sender, EventArgs e)
        {
            this.SetEnable(this.btAceptar);
            this.SetEnable(this.btTerminar);
        }
        private void tbxColor_LostFocus(object sender, EventArgs e)
        {
            if (Convert.ToString(this.cbxColor.SelectedValue).Trim() != string.Empty && Convert.ToString(this.cbxColor.SelectedValue).Trim() != "-1") return;
            this.SetDisEnable(this.btAceptar);
            this.SetDisEnable(this.btTerminar);
        }
        private void SetComboColor()
        {
            DataTable dtObj = null;
            DataRow drObj = null;
            ComboBox cbxObj = null;
            // Llenar ComboBox 'Color'.
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