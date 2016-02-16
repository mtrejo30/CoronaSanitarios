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

    public partial class a04_Defectos : Form
    {

        #region fields

        private LoginUsuario lu = null;
        private c00_Common oDA0 = new c00_Common();
        private c04_Defectos oDA = new c04_Defectos();

        private Timer trActualizarDatosServidor = new Timer();
        private int iPeriodoActualizacion = -1;

        private DataTable dtDefectos = null;
        private bool bForzarOffline = false;
        private Boolean bOpcionDesperdicio = false;

        // Valores de Estados de Defectos.
        private int iCodEstadoDefecto_EnProceso = 1;
        //private string sDesEstadoDefecto_EnProceso = "En Proceso";
        private int iCodEstadoDefecto_EnReparacion = 2;
        //private string sDesEstadoDefecto_EnReparacion = "En Reparacion";
        private int iCodEstadoDefecto_Reparado = 3;
        private string sDesEstadoDefecto_Reparado = "Reparado";
        private int iCodEstadoDefecto_Desperdicio = 4;
        private string sDesEstadoDefecto_Desperdicio = "Desperdicio";

        // Valores seleccionados de los Combos.
        private int iCodDefecto = -1;
        private string sClaveDefecto = string.Empty;
        private string sDesDefecto = string.Empty;
        private int iCodZonaDefecto = -1;
        private string sClaveZonaDefecto = string.Empty;
        private string sDesZonaDefecto = string.Empty;
        private int iCodEstadoDefecto = -1;
        private string sDesEstadoDefecto = string.Empty;

        // Valores seleccionados del ListView.
        private int iCodDefectoLV = -1;
        private string sDesDefectoLV = string.Empty;
        private int iCodZonaDefectoLV = -1;
        private string sDesZonaDefectoLV = string.Empty;
        private int iCodEstadoDefectoLV = -1;
        private string sDesEstadoDefectoLV = string.Empty;


        #endregion fields

        #region properties



        #endregion properties

        #region methods

        #region constructors and destructor

        public a04_Defectos(LoginUsuario lu, bool bForzarOffline)
        {
            InitializeComponent();
            //
            this.lu = lu;
            this.bForzarOffline = bForzarOffline;
            this.ConfigurarFormulario();
            this.ConfigurarPanelControles();
            this.btReparar.Visible = true;
        }
        public a04_Defectos(LoginUsuario lu, bool bForzarOffline, Boolean bOpcionDesperdicio)
        {
            InitializeComponent();
            this.lu = lu;
            this.bForzarOffline = bForzarOffline;
            this.bOpcionDesperdicio = bOpcionDesperdicio;
            this.ConfigurarFormulario();
            this.ConfigurarPanelControles();
            this.btReparar.Visible = false;
        }
        ~a04_Defectos()
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

            this.lvwDefectos.View = System.Windows.Forms.View.Details;
            this.lvwDefectos.Activation = ItemActivation.OneClick;
            this.lvwDefectos.CheckBoxes = false;
            this.lvwDefectos.FullRowSelect = true;
            this.lvwDefectos.HeaderStyle = ColumnHeaderStyle.Nonclickable;
            this.lvwDefectos.SelectedIndexChanged += new EventHandler(this.lvwDefectos_SelectedIndexChanged);

            this.cbxDefecto.SelectedIndexChanged += new EventHandler(this.cbxDefecto_SelectedIndexChanged);
            this.cbxDefecto.KeyPress += new KeyPressEventHandler(this.cbxDefecto_KeyPress);
            this.cbxZona.SelectedIndexChanged += new EventHandler(this.cbxZona_SelectedIndexChanged);
            this.cbxZona.KeyPress += new KeyPressEventHandler(this.cbxZona_KeyPress);
            this.cbxEstado.SelectedIndexChanged += new EventHandler(this.cbxEstado_SelectedIndexChanged);
            this.cbxEstado.KeyPress += new KeyPressEventHandler(this.cbxEstado_KeyPress);

            this.btAceptar.Click += new EventHandler(this.btAceptar_Click);
            this.btEliminar.Click += new EventHandler(this.btEliminar_Click);
            this.btReparar.Click += new EventHandler(this.btReparar_Click);
            this.btDesperdicio.Click += new EventHandler(this.btDesperdicio_Click);
            this.btProcesar.Click += new EventHandler(this.btProcesar_Click);
            this.btCancelar.Click += new EventHandler(this.btCancelar_Click);

            // Configuracion del Timer.
            //this.trActualizarDatosServidor.Interval = 100;
            //this.trActualizarDatosServidor.Enabled = true;
            //this.trActualizarDatosServidor.Tick += new EventHandler(this.trActualizarDatosServidor_Tick);
        }
        #endregion ConfigurarPanelControles

        #region LlenarListViewDefectos
        private void LlenarListViewDefectos()
        {
            this.dtDefectos = this.oDA.ObtenerDefectosPiezaProceso(DA.eTipoConexion.Local, this.lu.CodPieza, this.lu.CodProceso);
            this.dtDefectos.Columns.Add("EstadoRegistro", typeof(eEstadoRegistro));
            this.dtDefectos.Columns["CodEstadoDefecto"].ReadOnly = false;
            this.dtDefectos.Columns["DesEstadoDefecto"].ReadOnly = false;
            foreach (DataRow dr in this.dtDefectos.Rows)
            {
                dr["EstadoRegistro"] = eEstadoRegistro.Original;
            }

            // Crear columnas del ListView.
            this.lvwDefectos.Columns.Add("", 0, HorizontalAlignment.Left);
            this.lvwDefectos.Columns.Add("Defecto", ((int)(this.lvwDefectos.Width * 0.4)), HorizontalAlignment.Left);
            this.lvwDefectos.Columns.Add("", 0, HorizontalAlignment.Left);
            this.lvwDefectos.Columns.Add("Zona", ((int)(this.lvwDefectos.Width * 0.3)), HorizontalAlignment.Left);
            this.lvwDefectos.Columns.Add("", 0, HorizontalAlignment.Left);
            this.lvwDefectos.Columns.Add("Estado", ((int)(this.lvwDefectos.Width * 0.3)), HorizontalAlignment.Left);

            // Agregar elementos al ListView.
            string[] SubItems = null;
            this.lvwDefectos.BeginUpdate();
            foreach (DataRow dr in this.dtDefectos.Rows)
            {
                SubItems = new string[6];
                SubItems[0] = Convert.ToInt32(dr["CodDefecto"]).ToString();
                SubItems[1] = Convert.ToString(dr["DesDefecto"]);
                SubItems[2] = Convert.ToInt32(dr["CodZonaDefecto"]).ToString();
                SubItems[3] = Convert.ToString(dr["DesZonaDefecto"]);
                SubItems[4] = Convert.ToInt32(dr["CodEstadoDefecto"]).ToString();
                SubItems[5] = Convert.ToString(dr["DesEstadoDefecto"]);
                //
                this.lvwDefectos.Items.Add(new ListViewItem(SubItems));
            }
            this.lvwDefectos.EndUpdate();
        }
        #endregion LlenarListViewDefectos
        #region ObtenerDataRowSiExisteDefectoZona
        private DataRow ObtenerDataRowSiExisteDefectoZona(int iCodDefecto, int iCodZonaDefecto)
        {
            DataRow drDefectoZona = null;

            foreach (DataRow dr in this.dtDefectos.Rows)
            {
                if ((Convert.ToInt32(dr["CodDefecto"]) == iCodDefecto) && (Convert.ToInt32(dr["CodZonaDefecto"]) == iCodZonaDefecto))
                {
                    drDefectoZona = dr;
                    break;
                }
            }

            return drDefectoZona;
        }
        #endregion ValidarExisteDefectoEnReparacion
        #region ValidarExisteDefectoDesperdicio
        private bool ValidarExisteDefectoDesperdicio()
        {
            bool bExisteDefectoDesperdicio = false;

            foreach (DataRow dr in this.dtDefectos.Rows)
            {
                if (((eEstadoRegistro)dr["EstadoRegistro"]) != eEstadoRegistro.Eliminado)
                {
                    if (Convert.ToInt32(dr["CodEstadoDefecto"]) == this.iCodEstadoDefecto_Desperdicio)
                    {
                        bExisteDefectoDesperdicio = true;
                        break;
                    }
                }
            }

            return bExisteDefectoDesperdicio;
        }
        #endregion ValidarExisteDefectoDesperdicio
        #region ValidarExisteDefectoEnReparacion
        private bool ValidarExisteDefectoEnReparacion()
        {
            bool bExisteDefectoEnReparacion = false;

            foreach (DataRow dr in this.dtDefectos.Rows)
            {
                if (((eEstadoRegistro)dr["EstadoRegistro"]) != eEstadoRegistro.Eliminado)
                {
                    if (Convert.ToInt32(dr["CodEstadoDefecto"]) == this.iCodEstadoDefecto_EnReparacion)
                    {
                        bExisteDefectoEnReparacion = true;
                        break;
                    }
                }
            }

            return bExisteDefectoEnReparacion;
        }
        #endregion ValidarExisteDefectoEnReparacion

        #region EnviarDatosAlServidor
        private void EnviarDatosAlServidor()
        {
            this.encabezado.Mensaje = "Enviando datos...";
            this.Refresh();

            c00_Transacciones ct = new c00_Transacciones();
            bool bEnvioExitoso = ct.EnviarDefectos(this.lu.CodProceso);
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
                this.encabezado.Titulo = "Defectos de Pieza: " + this.lu.CodBarras.ToString();

                // Obtener periodo del timer.
                this.encabezado.Conexion = EstadoConexion.Offline;
                this.iPeriodoActualizacion = this.oDA0.ObtenerPeriodoActualizacion(this.lu.CodProceso, this.lu.CodPantalla);

                // Llenar ComboBox 'Defectos', 'Zonas' y 'Estados'.
                DataTable dtObj = null;
                DataRow drObj = null;
                ComboBox cbxObj = null;
                DataTable dtTipoArticulo = this.oDA.ObtenerTipoArticuloPieza(this.lu.CodPieza, this.bForzarOffline);
                int iTipoArticulo = 0;
                if (dtTipoArticulo != null && dtTipoArticulo.Rows.Count > 0) iTipoArticulo = Convert.ToInt32(dtTipoArticulo.Rows[0]["cod_tipo_articulo"]);
                else throw new Exception("No se pueden cargar los defectos Y zonas, no se proporciono una pieza Valida.");

                dtObj = this.oDA.ObtenerDefectos(this.lu.CodProceso, this.bForzarOffline);
                drObj = dtObj.NewRow();
                drObj["CodDefecto"] = -1;
                drObj["ClaveDefecto"] = "";
                drObj["DesDefecto"] = "Seleccionar...";
                dtObj.Rows.InsertAt(drObj, 0);
                cbxObj = this.cbxDefecto;
                cbxObj.ValueMember = "CodDefecto";
                cbxObj.DisplayMember = "DesDefecto";
                cbxObj.SelectedIndexChanged -= new EventHandler(this.cbxDefecto_SelectedIndexChanged);
                cbxObj.DataSource = dtObj;
                cbxObj.SelectedValue = -1;
                cbxObj.SelectedIndexChanged += new EventHandler(this.cbxDefecto_SelectedIndexChanged);

                dtObj = this.oDA.ObtenerZonasDefecto(iTipoArticulo, this.bForzarOffline);
                drObj = dtObj.NewRow();
                drObj["CodZonaDefecto"] = -1;
                drObj["ClaveZonaDefecto"] = "";
                drObj["DesZonaDefecto"] = "Seleccionar...";
                dtObj.Rows.InsertAt(drObj, 0);
                cbxObj = this.cbxZona;
                cbxObj.ValueMember = "CodZonaDefecto";
                cbxObj.DisplayMember = "DesZonaDefecto";
                cbxObj.SelectedIndexChanged -= new EventHandler(this.cbxZona_SelectedIndexChanged);
                cbxObj.DataSource = dtObj;
                cbxObj.SelectedValue = -1;
                cbxObj.SelectedIndexChanged += new EventHandler(this.cbxZona_SelectedIndexChanged);

                //Mandar llamar solo la opcion de Desperdicion cuando la pantalla sea llamada desde ConfigInicial
                int iReparacion = bOpcionDesperdicio ? -1 : this.iCodEstadoDefecto_EnReparacion;
                dtObj = this.oDA.ObtenerEstadosDefecto(iReparacion, this.iCodEstadoDefecto_Desperdicio, this.bForzarOffline);
                drObj = dtObj.NewRow();
                drObj["CodEstadoDefecto"] = -1;
                drObj["DesEstadoDefecto"] = "Seleccionar...";
                dtObj.Rows.InsertAt(drObj, 0);
                cbxObj = this.cbxEstado;
                cbxObj.ValueMember = "CodEstadoDefecto";
                cbxObj.DisplayMember = "DesEstadoDefecto";
                cbxObj.SelectedIndexChanged -= new EventHandler(this.cbxEstado_SelectedIndexChanged);
                cbxObj.DataSource = dtObj;
                cbxObj.SelectedValue = -1;
                cbxObj.SelectedIndexChanged += new EventHandler(this.cbxEstado_SelectedIndexChanged);

                this.LlenarListViewDefectos();

                this.cbxDefecto.Focus();
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

        #region lvwDefectos_SelectedIndexChanged
        private void lvwDefectos_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                ListView lvwObj = (ListView)sender;

                if (lvwObj.SelectedIndices.Count == 1)
                {
                    int iIndex = lvwObj.SelectedIndices[0];
                    this.iCodDefectoLV = Convert.ToInt32(lvwObj.Items[iIndex].SubItems[0].Text);
                    this.sDesDefectoLV = lvwObj.Items[iIndex].SubItems[1].Text;
                    this.iCodZonaDefectoLV = Convert.ToInt32(lvwObj.Items[iIndex].SubItems[2].Text);
                    this.sDesZonaDefectoLV = lvwObj.Items[iIndex].SubItems[3].Text;
                    this.iCodEstadoDefectoLV = Convert.ToInt32(lvwObj.Items[iIndex].SubItems[4].Text);
                    this.sDesEstadoDefectoLV = lvwObj.Items[iIndex].SubItems[5].Text;
                }
                else
                {
                    this.iCodDefectoLV = -1;
                    this.sDesDefectoLV = string.Empty;
                    this.iCodZonaDefectoLV = -1;
                    this.sDesZonaDefectoLV = string.Empty;
                    this.iCodEstadoDefectoLV = -1;
                    this.sDesEstadoDefectoLV = string.Empty;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
            }
        }
        #endregion lvwDefectos_SelectedIndexChanged
        #region cbxDefecto_SelectedIndexChanged
        private void cbxDefecto_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                ComboBox cbxObj = (ComboBox)sender;

                this.iCodDefecto = Convert.ToInt32(cbxObj.SelectedValue);
                this.sClaveDefecto = Convert.ToString(((DataRowView)cbxObj.SelectedItem)["ClaveDefecto"]);
                this.sDesDefecto = cbxObj.Text;

                if (this.iCodDefecto == -1)
                {
                    this.encabezado.Mensaje = "Seleccione Defecto";

                    cbxObj.Focus();
                }
                else
                {
                    this.encabezado.Mensaje = string.Empty;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
            }
        }
        #endregion cbxDefecto_SelectedIndexChanged
        #region cbxDefecto_KeyPress
        private void cbxDefecto_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                ComboBox cbxObj = (ComboBox)sender;

                // Validar ingreso del Enter.
                if (e.KeyChar == 13)
                {
                    if (this.iCodDefecto == -1)
                    {
                        this.encabezado.Mensaje = "Seleccione Defecto";
                        cbxObj.Focus();
                    }
                    else
                    {
                        this.encabezado.Mensaje = string.Empty;

                        this.cbxZona.Focus();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
            }
        }
        #endregion cbxDefecto_KeyPress
        #region cbxZona_SelectedIndexChanged
        private void cbxZona_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                ComboBox cbxObj = (ComboBox)sender;

                this.iCodZonaDefecto = Convert.ToInt32(cbxObj.SelectedValue);
                this.sClaveZonaDefecto = Convert.ToString(((DataRowView)cbxObj.SelectedItem)["ClaveZonaDefecto"]);
                this.sDesZonaDefecto = cbxObj.Text;

                if (this.iCodZonaDefecto == -1)
                {
                    this.encabezado.Mensaje = "Seleccione Zona Defecto";

                    cbxObj.Focus();
                }
                else
                {
                    this.encabezado.Mensaje = string.Empty;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
            }
        }
        #endregion cbxZona_SelectedIndexChanged
        #region cbxZona_KeyPress
        private void cbxZona_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                ComboBox cbxObj = (ComboBox)sender;

                // Validar ingreso del Enter.
                if (e.KeyChar == 13)
                {
                    if (this.iCodZonaDefecto == -1)
                    {
                        this.encabezado.Mensaje = "Seleccione Zona Defecto";

                        cbxObj.Focus();
                    }
                    else
                    {
                        this.encabezado.Mensaje = string.Empty;

                        this.cbxEstado.Focus();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
            }
        }
        #endregion cbxZona_KeyPress
        #region cbxEstado_SelectedIndexChanged
        private void cbxEstado_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                ComboBox cbxObj = (ComboBox)sender;

                this.iCodEstadoDefecto = Convert.ToInt32(cbxObj.SelectedValue);
                this.sDesEstadoDefecto = cbxObj.Text;

                if (this.iCodZonaDefecto == -1)
                {
                    this.encabezado.Mensaje = "Seleccione Estado Defecto";

                    cbxObj.Focus();
                }
                else
                {
                    this.encabezado.Mensaje = string.Empty;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
            }
        }
        #endregion cbxEstado_SelectedIndexChanged
        #region cbxEstado_KeyPress
        private void cbxEstado_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                ComboBox cbxObj = (ComboBox)sender;

                // Validar ingreso del Enter.
                if (e.KeyChar == 13)
                {
                    if (this.iCodEstadoDefecto == -1)
                    {
                        this.encabezado.Mensaje = "Seleccione Estado Defecto";

                        cbxObj.Focus();
                    }
                    else
                    {
                        this.encabezado.Mensaje = string.Empty;

                        this.btAceptar.Focus();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
            }
        }
        #endregion cbxEstado_KeyPress

        #region btAceptar_Click
        private void btAceptar_Click(object sender, EventArgs e)
        {
            try
            {
                // Validacion: Seleccion de Combos.
                if (this.iCodDefecto == -1)
                {
                    this.encabezado.Mensaje = "Seleccione defecto";

                    this.cbxDefecto.Focus();
                    return;
                }
                if (this.iCodZonaDefecto == -1)
                {
                    this.encabezado.Mensaje = "Seleccione zona defecto";

                    this.cbxZona.Focus();
                    return;
                }
                if (this.iCodEstadoDefecto == -1)
                {
                    this.encabezado.Mensaje = "Seleccione estado defecto";

                    this.cbxEstado.Focus();
                    return;
                }

                // Validacion: Existe Defecto-Zona.
                DataRow dr = this.ObtenerDataRowSiExisteDefectoZona(this.iCodDefecto, this.iCodZonaDefecto);
                if (dr != null)
                {
                    if ((eEstadoRegistro)dr["EstadoRegistro"] != eEstadoRegistro.Eliminado)
                    {
                        // Reiniciar Combos.
                        this.cbxDefecto.SelectedValue = -1;
                        this.cbxZona.SelectedValue = -1;
                        this.cbxEstado.SelectedValue = -1;

                        this.encabezado.Mensaje = "Defecto-Zona ya capturada";

                        this.lvwDefectos.Focus();
                        return;
                    }
                }

                // Validacion: Existe Defecto con estado 'Desperdicio'.
                if (this.ValidarExisteDefectoDesperdicio())
                {
                    this.encabezado.Mensaje = "Pieza en desperdicio";

                    this.lvwDefectos.Focus();
                    return;
                }

                this.encabezado.Mensaje = String.Empty;

                // Actualizacion de DataTable.
                if (dr != null)
                {
                    // Reutilizar registro 'Eliminado' del DataTable.
                    dr["CodEstadoDefecto"] = this.iCodEstadoDefecto;
                    dr["DesEstadoDefecto"] = this.sDesEstadoDefecto;
                    dr["EstadoRegistro"] = eEstadoRegistro.Actualizado;
                }
                else
                {
                    // Agregar registro al DataTable.
                    dr = this.dtDefectos.NewRow();
                    dr["CodDefecto"] = this.iCodDefecto;
                    dr["DesDefecto"] = this.sDesDefecto;
                    dr["CodZonaDefecto"] = this.iCodZonaDefecto;
                    dr["DesZonaDefecto"] = this.sDesZonaDefecto;
                    dr["CodEstadoDefecto"] = this.iCodEstadoDefecto;
                    dr["DesEstadoDefecto"] = this.sDesEstadoDefecto;
                    dr["EstadoRegistro"] = eEstadoRegistro.Nuevo;
                    this.dtDefectos.Rows.Add(dr);
                }
                this.dtDefectos.AcceptChanges();

                // Agregar elemento al ListView.
                string[] SubItems = new string[6];
                SubItems[0] = Convert.ToInt32(dr["CodDefecto"]).ToString();
                SubItems[1] = Convert.ToString(dr["DesDefecto"]);
                SubItems[2] = Convert.ToInt32(dr["CodZonaDefecto"]).ToString();
                SubItems[3] = Convert.ToString(dr["DesZonaDefecto"]);
                SubItems[4] = Convert.ToInt32(dr["CodEstadoDefecto"]).ToString();
                SubItems[5] = Convert.ToString(dr["DesEstadoDefecto"]);
                this.lvwDefectos.Items.Add(new ListViewItem(SubItems));
                this.lvwDefectos.EnsureVisible(this.lvwDefectos.Items.Count - 1);
                this.lvwDefectos.Items[this.lvwDefectos.Items.Count - 1].Selected = true;

                // Reiniciar Combos.
                this.cbxDefecto.SelectedValue = -1;
                this.cbxZona.SelectedValue = -1;
                this.cbxEstado.SelectedValue = -1;

                this.cbxDefecto.Focus();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
            }
        }
        #endregion btAceptar_Click
        #region btEliminar_Click
        private void btEliminar_Click(object sender, EventArgs e)
        {
            try
            {
                // Validacion: Seleccion de ListView.
                if (this.iCodDefectoLV == -1)
                {
                    this.encabezado.Mensaje = "Seleccione defecto";

                    this.lvwDefectos.Focus();
                    return;
                }

                // Confirmacion de accion.
                DialogResult drRes = MessageBox.Show("¿Confirma eliminar defecto?", "SCPP", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
                if (drRes == DialogResult.No)
                {
                    return;
                }

                this.encabezado.Mensaje = String.Empty;

                // Si el registro en el DataTable es [Nuevo] se elimina.
                // Si el registro en el DataTable es [Original o Actualizado] se marca como [Eliminado].
                foreach (DataRow dr in this.dtDefectos.Rows)
                {
                    if ((Convert.ToInt32(dr["CodDefecto"]) == this.iCodDefectoLV) && (Convert.ToInt32(dr["CodZonaDefecto"]) == this.iCodZonaDefectoLV))
                    {
                        if (((eEstadoRegistro)dr["EstadoRegistro"]) == eEstadoRegistro.Nuevo)
                        {
                            this.dtDefectos.Rows.Remove(dr);
                            this.dtDefectos.AcceptChanges();
                        }
                        else
                        {
                            dr["EstadoRegistro"] = eEstadoRegistro.Eliminado;
                        }
                        break;
                    }
                }

                // Eliminar elemento del ListView.
                int iIndexEliminado = this.lvwDefectos.SelectedIndices[0];
                this.lvwDefectos.Items.RemoveAt(iIndexEliminado);
                if (this.lvwDefectos.Items.Count > 0)
                {
                    if (iIndexEliminado == 0)
                    {
                        this.lvwDefectos.Items[0].Selected = true;
                    }
                    else
                    {
                        this.lvwDefectos.Items[iIndexEliminado - 1].Selected = true;
                    }
                }
                else
                {
                    this.iCodDefectoLV = -1;
                    this.sDesDefectoLV = string.Empty;
                    this.iCodZonaDefectoLV = -1;
                    this.sDesZonaDefectoLV = string.Empty;
                    this.iCodEstadoDefectoLV = -1;
                    this.sDesEstadoDefectoLV = string.Empty;
                }

                this.lvwDefectos.Focus();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
            }
        }
        #endregion btEliminar_Click
        #region btReparar_Click
        private void btReparar_Click(object sender, EventArgs e)
        {
            try
            {
                // Validacion: Seleccion de ListView.
                if (this.iCodDefectoLV == -1)
                {
                    this.encabezado.Mensaje = "Seleccione defecto";

                    this.lvwDefectos.Focus();
                    return;
                }

                // Validacion: Existe Defecto con estado 'Desperdicio'.
                if (this.ValidarExisteDefectoDesperdicio())
                {
                    this.encabezado.Mensaje = "Pieza en desperdicio";

                    this.lvwDefectos.Focus();
                    return;
                }

                // Validacion: Si el Defecto ya esta liberada.
                if (this.iCodEstadoDefectoLV == this.iCodEstadoDefecto_Reparado)
                {
                    this.encabezado.Mensaje = "Pieza ya reparada";

                    this.lvwDefectos.Focus();
                    return;
                }

                this.encabezado.Mensaje = String.Empty;

                // Si el registro en el DataTable es [Original] se marca como [Actualizado].
                // Si el registro en el DataTable es [Nuevo] se queda como [Nuevo].
                foreach (DataRow dr in this.dtDefectos.Rows)
                {
                    if ((Convert.ToInt32(dr["CodDefecto"]) == this.iCodDefectoLV) && (Convert.ToInt32(dr["CodZonaDefecto"]) == this.iCodZonaDefectoLV))
                    {
                        dr["CodEstadoDefecto"] = this.iCodEstadoDefecto_Reparado;
                        dr["DesEstadoDefecto"] = this.sDesEstadoDefecto_Reparado;
                        if (((eEstadoRegistro)dr["EstadoRegistro"]) == eEstadoRegistro.Original)
                        {
                            dr["EstadoRegistro"] = eEstadoRegistro.Actualizado;
                        }
                        break;
                    }
                }

                // Actualizar elemento del ListView.
                ListViewItem lviObj = this.lvwDefectos.Items[this.lvwDefectos.SelectedIndices[0]];
                lviObj.SubItems[4].Text = this.iCodEstadoDefecto_Reparado.ToString();
                lviObj.SubItems[5].Text = this.sDesEstadoDefecto_Reparado;

                this.lvwDefectos.Focus();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
            }
        }
        #endregion btReparar_Click
        #region btDesperdicio_Click
        private void btDesperdicio_Click(object sender, EventArgs e)
        {
            try
            {
                // Validacion: Seleccion de ListView.
                if (this.iCodDefectoLV == -1)
                {
                    this.encabezado.Mensaje = "Seleccione defecto";

                    this.lvwDefectos.Focus();
                    return;
                }

                // Validacion: Existe Defecto con estado 'Desperdicio'.
                if (this.ValidarExisteDefectoDesperdicio())
                {
                    this.encabezado.Mensaje = "Pieza ya en desperdicio";

                    this.lvwDefectos.Focus();
                    return;
                }

                this.encabezado.Mensaje = String.Empty;

                // Si el registro en el DataTable es [Original] se marca como [Actualizado].
                // Si el registro en el DataTable es [Nuevo] se queda como [Nuevo].
                foreach (DataRow dr in this.dtDefectos.Rows)
                {
                    if ((Convert.ToInt32(dr["CodDefecto"]) == this.iCodDefectoLV) && (Convert.ToInt32(dr["CodZonaDefecto"]) == this.iCodZonaDefectoLV))
                    {
                        dr["CodEstadoDefecto"] = this.iCodEstadoDefecto_Desperdicio;
                        dr["DesEstadoDefecto"] = this.sDesEstadoDefecto_Desperdicio;
                        if (((eEstadoRegistro)dr["EstadoRegistro"]) == eEstadoRegistro.Original)
                        {
                            dr["EstadoRegistro"] = eEstadoRegistro.Actualizado;
                        }
                        break;
                    }
                }

                // Actualizar elemento del ListView.
                ListViewItem lviObj = this.lvwDefectos.Items[this.lvwDefectos.SelectedIndices[0]];
                lviObj.SubItems[4].Text = this.iCodEstadoDefecto_Desperdicio.ToString();
                lviObj.SubItems[5].Text = this.sDesEstadoDefecto_Desperdicio;

                this.lvwDefectos.Focus();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
            }
        }
        #endregion btDesperdicio_Click
        #region btProcesar_Click
        private void btProcesar_Click(object sender, EventArgs e)
        {
            try
            {
                // Recorrer DataTable y ejecutar sentencias a la Base de datos.
                int iCodDefectoDR = -1;
                int iCodZonaDefectoDR = -1;
                int iCodEstadoDefectoDR = -1;
                foreach (DataRow dr in this.dtDefectos.Rows)
                {
                    // Obtener datos del defecto.
                    iCodDefectoDR = Convert.ToInt32(dr["CodDefecto"]);
                    iCodZonaDefectoDR = Convert.ToInt32(dr["CodZonaDefecto"]);
                    iCodEstadoDefectoDR = Convert.ToInt32(dr["CodEstadoDefecto"]);

                    switch ((eEstadoRegistro)(dr["EstadoRegistro"]))
                    {
                        case eEstadoRegistro.Nuevo:
                            this.oDA.InsertarPiezaDefecto(DA.eTipoConexion.Local, this.lu.CodPieza, this.lu.CodProceso, iCodDefectoDR, iCodZonaDefectoDR, iCodEstadoDefectoDR, this.lu.CodEmpleado, this.lu.Fecha, this.lu.Fecha);
                            break;
                        case eEstadoRegistro.Eliminado:
                            this.oDA.MarcarEliminadaPiezaDefecto(this.lu.CodPieza, this.lu.CodProceso, iCodDefectoDR, iCodZonaDefectoDR);
                            //this.oDA.EliminarPiezaDefecto(DA.eTipoConexion.Local, this.lu.CodPieza, this.lu.CodProceso, iCodDefectoDR, iCodZonaDefectoDR);
                            break;
                        case eEstadoRegistro.Actualizado:
                            this.oDA.ActualizarPiezaDefecto(DA.eTipoConexion.Local, this.lu.CodPieza, this.lu.CodProceso, iCodDefectoDR, iCodZonaDefectoDR, iCodEstadoDefectoDR, this.lu.CodEmpleado, this.lu.Fecha);
                            break;
                    }
                }

                int iCodEstadoPieza = -1;
                if (this.ValidarExisteDefectoDesperdicio())
                {
                    iCodEstadoPieza = this.iCodEstadoDefecto_Desperdicio;

                    // Cuando la pieza se envia a desperdicio se establece su ultimo proceso al actual.
                    this.oDA0.ActulizarUltimoProcesoPieza(DA.eTipoConexion.Local, this.lu.CodPieza, this.lu.CodProceso);
                }
                else if (this.ValidarExisteDefectoEnReparacion())
                {
                    iCodEstadoPieza = this.iCodEstadoDefecto_EnReparacion;
                }
                else
                {
                    iCodEstadoPieza = this.iCodEstadoDefecto_EnProceso;
                }
                this.oDA.ActualizarPiezaUltimoEstado(DA.eTipoConexion.Local, this.lu.CodPieza, iCodEstadoPieza);

                if (!this.bForzarOffline)
                {
                    this.EnviarDatosAlServidor();
                }
                this.Close();
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
                // Confirmacion de accion.
                DialogResult dr = MessageBox.Show("¿Cancelar captura de defectos?", "SCPP", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
                if (dr == DialogResult.Yes)
                {
                    this.Close();
                }
                else
                {
                    this.lvwDefectos.Focus();
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

    public enum eEstadoRegistro
    {
        Indeterminado = -1,
        Original = 1,
        Nuevo = 2,
        Actualizado = 3,
        Eliminado = 4
    };

}