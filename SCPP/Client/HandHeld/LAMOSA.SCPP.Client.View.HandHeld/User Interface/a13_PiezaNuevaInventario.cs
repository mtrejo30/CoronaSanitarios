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
    public partial class a13_PiezaNuevaInventario : Form
    {

        #region fields

        private LoginUsuario lu = null;
        private c00_Common oDA0 = new c00_Common();
        private c13_CapturaInventario oDA = new c13_CapturaInventario();

        private Timer trActualizarDatosServidor = new Timer();
        private int iPeriodoActualizacion = -1;

        private int iCodProceso = -1;
        private int iCodTipoModelo = -1;
        private int iCodModelo = -1;
        private int iCodColor = -1;
        private int iCodCalidad = -1;
        private string sClaveModelo = string.Empty;
        private string sClaveColor = string.Empty;
        private string sClaveCalidad = string.Empty;
        private Boolean bNuevaPieza = false;

        #endregion fields

        #region properties



        #endregion properties

        #region methods

        #region constructors and destructor

        public a13_PiezaNuevaInventario(LoginUsuario lu)
        {
            InitializeComponent();
            //
            this.lu = lu;
            this.ConfigurarFormulario();
            this.ConfigurarPanelControles();
        }
        public a13_PiezaNuevaInventario(LoginUsuario lu, Boolean activarEtiqueta)
        {
            InitializeComponent();
            //
            this.lu = lu;
            this.ConfigurarFormulario();
            this.ConfigurarPanelControles();
            this.txEtiqueta.ReadOnly = activarEtiqueta;
            this.txEtiqueta.Enabled = !activarEtiqueta;
            bNuevaPieza = !activarEtiqueta;
            this.txEtiqueta.Text = String.Empty;
            this.txEtiqueta.Focus();
        }
        ~a13_PiezaNuevaInventario()
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

            this.txEtiqueta.ReadOnly = true;
            this.txEtiqueta.Enabled = false;
            this.txPlanta.ReadOnly = true;
            this.txPlanta.Enabled = false;

            this.cbxTipoModelo.Enabled = true;
            this.cbxModelo.Enabled = true;
            this.cbxColor.Enabled = false;
            this.cbxCalidad.Enabled = false;

            this.cbxProceso.SelectedIndexChanged += new EventHandler(this.cbxProceso_SelectedIndexChanged);
            this.cbxProceso.KeyPress += new KeyPressEventHandler(this.cbxProceso_KeyPress);
            this.cbxTipoModelo.SelectedIndexChanged += new EventHandler(this.cbxTipoModelo_SelectedIndexChanged);
            this.cbxTipoModelo.KeyPress += new KeyPressEventHandler(this.cbxTipoModelo_KeyPress);
            this.cbxModelo.SelectedIndexChanged += new EventHandler(this.cbxModelo_SelectedIndexChanged);
            this.cbxModelo.KeyPress += new KeyPressEventHandler(this.cbxModelo_KeyPress);
            this.cbxColor.SelectedIndexChanged += new EventHandler(this.cbxColor_SelectedIndexChanged);
            this.cbxColor.KeyPress += new KeyPressEventHandler(this.cbxColor_KeyPress);
            this.cbxCalidad.SelectedIndexChanged += new EventHandler(this.cbxCalidad_SelectedIndexChanged);
            this.cbxCalidad.KeyPress += new KeyPressEventHandler(this.cbxCalidad_KeyPress);

            this.btAceptar.Click += new EventHandler(this.btAceptar_Click);
            this.btCancelar.Click += new EventHandler(this.btCancelar_Click);

            //// Configuracion del Timer.
            //this.trActualizarDatosServidor.Interval = 100;
            //this.trActualizarDatosServidor.Enabled = true;
            //this.trActualizarDatosServidor.Tick += new EventHandler(this.trActualizarDatosServidor_Tick);
        }
        #endregion ConfigurarPanelControles

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
                this.encabezado.Titulo = "Registro Pieza Inventario";

                // Obtener periodo del timer.
                this.encabezado.Conexion = EstadoConexion.Offline;
                this.iPeriodoActualizacion = this.oDA0.ObtenerPeriodoActualizacion(this.lu.CodProceso, this.lu.CodPantalla);

                this.txEtiqueta.Text = this.lu.CodBarras;
                this.txPlanta.Text = this.lu.DesPlanta;

                DataTable dtObj = null;
                DataRow drObj = null;
                ComboBox cbxObj = null;

                // Llenar ComboBox 'Proceso'.
                dtObj = this.oDA0.ObtenerProcesos();
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

                if (String.IsNullOrEmpty(this.txEtiqueta.Text))
                    this.txEtiqueta.Focus();
                else
                    this.cbxProceso.Focus();
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
                    this.encabezado.Mensaje = String.Empty;

                    int codPieza = this.oDA0.ObtenerCodPiezaCodBarras(this.txEtiqueta.Text, false);
                    if (codPieza != -1 && bNuevaPieza)
                    {
                        this.encabezado.Mensaje = "Etiqueta ya existe";
                        this.txEtiqueta.Text = String.Empty;
                        this.txEtiqueta.Focus();
                        return;
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
        #region cbxProceso_SelectedIndexChanged
        private void cbxProceso_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                ComboBox cbxObj = (ComboBox)sender;

                this.iCodProceso = Convert.ToInt32(cbxObj.SelectedValue);

                // Borra seleccion a los ComboBox 'Color' y 'Calidad'.
                this.cbxColor.SelectedIndexChanged -= new EventHandler(this.cbxColor_SelectedIndexChanged);
                this.cbxColor.SelectedValue = -1;
                this.cbxColor.SelectedIndexChanged += new EventHandler(this.cbxColor_SelectedIndexChanged);
                this.cbxCalidad.SelectedIndexChanged -= new EventHandler(this.cbxCalidad_SelectedIndexChanged);
                this.cbxCalidad.SelectedValue = -1;
                this.cbxCalidad.SelectedIndexChanged += new EventHandler(this.cbxCalidad_SelectedIndexChanged);

                // Activa los ComboBox 'Color' y 'Calidad'.
                this.cbxColor.Enabled = false;
                this.cbxCalidad.Enabled = false;
                if (this.iCodProceso >= this.oDA0.ObtenerCodProcesoEsmaltado())
                {
                    this.cbxColor.Enabled = true;
                }
                if (this.iCodProceso >= this.oDA0.ObtenerCodProcesoClasificado())
                {
                    this.cbxCalidad.Enabled = true;
                }

                if (this.iCodProceso == -1)
                {
                    this.encabezado.Mensaje = "Seleccione Proceso";
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
        #endregion cbxProceso_SelectedIndexChanged
        #region cbxProceso_KeyPress
        private void cbxProceso_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                ComboBox cbxObj = (ComboBox)sender;

                // Validar ingreso del Enter.
                if (e.KeyChar == 13)
                {
                    if (this.iCodProceso == -1)
                    {
                        cbxObj.Focus();
                    }
                    else
                    {
                        this.cbxTipoModelo.Focus();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
            }
        }
        #endregion cbxProceso_KeyPress
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
                        this.cbxModelo.Focus();
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
                        if (this.iCodProceso >= this.oDA0.ObtenerCodProcesoEsmaltado())
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
                        if (this.iCodProceso >= this.oDA0.ObtenerCodProcesoClasificado())
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
                this.encabezado.Mensaje = String.Empty;

                int codPieza = this.oDA0.ObtenerCodPiezaCodBarras(this.txEtiqueta.Text, false);
                if (codPieza != -1 && bNuevaPieza)
                {
                    this.txEtiqueta.Text = String.Empty;
                    this.txEtiqueta.Focus();
                    return;
                }
                if (String.IsNullOrEmpty(this.txEtiqueta.Text.ToString().Trim()) && bNuevaPieza)
                {
                    this.encabezado.Mensaje = "Ingrese Etiqueta";
                    this.txEtiqueta.Focus();
                    return;
                }else
                {
                    this.lu.CodBarras = this.txEtiqueta.Text;
                }
                if (this.iCodProceso == -1)
                {
                    this.encabezado.Mensaje = "Seleccione Proceso";
                    this.cbxProceso.Focus();
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

                if (this.iCodProceso >= this.oDA0.ObtenerCodProcesoEsmaltado())
                {
                    if (this.iCodColor == -1)
                    {
                        this.encabezado.Mensaje = "Seleccione Color";
                        this.cbxColor.Focus();
                        return;
                    }
                }
                if (this.iCodProceso >= this.oDA0.ObtenerCodProcesoClasificado())
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
                if (this.iCodProceso <= this.oDA0.ObtenerCodProcesoRevisado())
                {
                    sClaveModelo = this.sClaveModelo;
                }
                else if (this.iCodProceso <= this.oDA0.ObtenerCodProcesoHornos())
                {
                    sClaveModelo = this.sClaveModelo + "-" + this.sClaveColor;
                }
                else
                {
                    sClaveModelo = this.sClaveModelo + "-" + this.sClaveColor + "-" + this.sClaveCalidad;
                }

                int iCodModelo = this.oDA0.ExisteModelo(sClaveModelo);
                if (iCodModelo == -1)
                {
                    this.encabezado.Mensaje = "Modelo-Color-Calidad invalido";
                    this.cbxModelo.Focus();
                    return;
                }

                // Insertar Pieza Inventario.
                this.oDA.InsertarPiezaInventario(this.lu.CodBarras, this.lu.CodPlanta, this.iCodProceso, this.iCodModelo, this.iCodColor, this.iCodCalidad, 1);
                if (bNuevaPieza)
                {
                    a03_ConfiguracionInicial frmObj = new a03_ConfiguracionInicial(this.lu);
                    frmObj.Show();
                    this.Close();
                }
                this.Close();
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
                DialogResult dr = MessageBox.Show("¿Desea Cancelar la Captura de esta Etiqueta Inventario?", "SCPP", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
                if (dr == DialogResult.Yes)
                {
                    // Regresar a Configuracion Inicial.
                    a03_ConfiguracionInicial frmObj = new a03_ConfiguracionInicial(this.lu);
                    frmObj.Show();
                    this.Close();
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