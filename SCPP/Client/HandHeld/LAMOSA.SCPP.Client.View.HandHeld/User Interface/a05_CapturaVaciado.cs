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
    public partial class a05_CapturaVaciado : Form
    {

        #region fields

        private LoginUsuario lu = null;
        private c00_Common oDA0 = new c00_Common();
        private c05_CapturaVaciado oDA = new c05_CapturaVaciado();
        private Timer trActualizarDatosServidor = new Timer();
        private int iPeriodoActualizacion = -1;

        private DataTable dtVaciado = null;
        private int iPosicion = -1;
        private int iNumPiezasACapturar = -1;

        private int iCodMoldeActual = -1;
        private int iCodArticuloSel = -1;

        #endregion fields

        #region properties



        #endregion properties

        #region methods

        #region constructors and destructor

        public a05_CapturaVaciado(LoginUsuario lu)
        {
            InitializeComponent();
            //
            this.lu = lu;
            this.ConfigurarFormulario();
            this.ConfigurarPanelControles();
        }
        ~a05_CapturaVaciado()
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

            this.txBanco.ReadOnly = true;
            this.txBanco.Enabled = false;
            this.txPosicion.ReadOnly = true;
            this.txPosicion.Enabled = false;
            this.txTipo.ReadOnly = true;
            this.txTipo.Enabled = false;

            this.txEtiqueta.KeyPress += new KeyPressEventHandler(this.txEtiqueta_KeyPress);
            this.cbxModelo.SelectedIndexChanged += new EventHandler(this.cbxModelo_SelectedIndexChanged);
            this.cbxModelo.KeyPress += new KeyPressEventHandler(this.cbxModelo_KeyPress);

            this.btAceptar.Click += new EventHandler(this.btAceptar_Click);
            this.btSiguiente.Click += new EventHandler(this.btSiguiente_Click);
            this.btDefectos.Click += new EventHandler(this.btDefectos_Click);
            this.btProcesar.Click += new EventHandler(this.btProcesar_Click);
            this.btCancelar.Click += new EventHandler(this.btCancelar_Click);

            // Configuracion del Timer.
            //this.trActualizarDatosServidor.Interval = 100;
            //this.trActualizarDatosServidor.Enabled = true;
            //this.trActualizarDatosServidor.Tick += new EventHandler(this.trActualizarDatosServidor_Tick);
        }
        #endregion ConfigurarPanelControles

        #region ValidarExisteCodBarras
        private bool ValidarExisteCodBarras(string sCodBarras)
        {
            bool bExisteCodBarras = false;

            for (int iIndex = 0; iIndex < this.dtVaciado.Rows.Count; iIndex++)
            {
                if (Convert.ToString(this.dtVaciado.Rows[iIndex]["CodBarras"]) == sCodBarras)
                {
                    if (iIndex != (this.iPosicion - 1))
                    {
                        bExisteCodBarras = true;
                        break;
                    }
                }
            }

            return bExisteCodBarras;
        }
        #endregion ValidarExisteCodBarras
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

                return val;
            }

            // Validar exista la pieza.
            int iCodPieza = iCodPieza = this.oDA0.ObtenerCodPiezaCodBarras(sCodBarras, true);
            if (iCodPieza != -1)
            {
                // Obtener el estado de la pieza.
                dtObj = this.oDA0.ObtenerEstadoPieza(iCodPieza, true);
                string sDesEstadoPieza = Convert.ToString(dtObj.Rows[0]["DesEstadoPieza"]);

                val.ValidacionExitosa = false;
                val.MensajeValidacion = "Pieza  " + sDesEstadoPieza + " - ";

                // Obtener el ultimo proceso de la pieza.
                dtObj = this.oDA0.ObtenerUltimoProcesoPieza(iCodPieza, true);
                string sDesUltimoProcesoPieza = Convert.ToString(dtObj.Rows[0]["DesProceso"]);

                val.ValidacionExitosa = false;
                val.MensajeValidacion = "Pieza  " + sDesEstadoPieza + " - " + sDesUltimoProcesoPieza;

                return val;
            }
            else
            {
                // Validar no se haya capturado el mismo Codigo de Barras.
                if (this.ValidarExisteCodBarras(sCodBarras))
                {
                    val.ValidacionExitosa = false;
                    val.MensajeValidacion = "Pieza ya capturada";
                }
                else
                {
                    val.ValidacionExitosa = true;
                    val.MensajeValidacion = string.Empty;
                }

                return val;
            }
        }
        #endregion ValidarPieza
        #region ObtenerPiezasCapturadas
        private int ObtenerPiezasCapturadas()
        {
            int iNumPiezasCapturadas = 0;

            foreach (DataRow dr in this.dtVaciado.Rows)
            {
                if (Convert.ToBoolean(dr["Capturado"]))
                {
                    iNumPiezasCapturadas++;
                }
            }

            return iNumPiezasCapturadas;
        }
        #endregion ObtenerPiezasCapturadas
        #region ObtenerDatosSiguientePosicion
        private void ObtenerDatosSiguientePosicion()
        {
            // Ajustar Posicion.
            if (this.lu.Ascendente)
            {
                this.iPosicion++;
            }
            else
            {
                this.iPosicion--;
            }
            if (!this.lu.Ascendente && this.iPosicion == 0)
            {
                this.iPosicion = this.iNumPiezasACapturar;
            }
            if (this.lu.Ascendente && (this.iPosicion - 1 == this.iNumPiezasACapturar))
            {
                this.iPosicion = 1;
            }
            this.txPosicion.Text = this.iPosicion.ToString();

            // Llenar ComboBox 'Modelo' y TextBox 'Tipo'.
            DataTable dtObj = null;
            DataRow drObj = null;
            ComboBox cbxObj = null;

            int iCodMolde = Convert.ToInt32(this.dtVaciado.Rows[this.iPosicion - 1]["CodMolde"]);
            dtObj = this.oDA.ObtenerArticulosMolde(iCodMolde, true);
            drObj = dtObj.NewRow();
            drObj["CodArticulo"] = -1;
            drObj["ClaveArticulo"] = "";
            drObj["DesArticulo"] = "Seleccionar...";
            drObj["CodTipoArticulo"] = -1;
            drObj["ClaveTipoArticulo"] = "";
            drObj["DesTipoArticulo"] = "";
            dtObj.Rows.InsertAt(drObj, 0);
            cbxObj = this.cbxModelo;
            cbxObj.ValueMember = "CodArticulo";
            cbxObj.DisplayMember = "DesArticulo";
            cbxObj.SelectedIndexChanged -= new EventHandler(this.cbxModelo_SelectedIndexChanged);
            cbxObj.DataSource = dtObj;

            //  Si el Molde cambia, tambien los Modelos (Articulos) que permite el Molde.
            if (iCodMolde != this.iCodMoldeActual)
            {
                this.iCodMoldeActual = iCodMolde;
                cbxObj.SelectedValue = -1;
            }
            else
            {
                cbxObj.SelectedValue = this.iCodArticuloSel;
            }

            cbxObj.SelectedIndexChanged += new EventHandler(this.cbxModelo_SelectedIndexChanged);
        }
        #endregion ObtenerDatosSiguientePosicion
        #region InsertarPiezas
        private void InsertarPiezas()
        {
            int iCodConsecutivo = -1;
            int iPosicion = -1;
            string sCodBarras = string.Empty;
            int iCodArticulo = -1;
            int iCodPieza = -1;
            long lCodPiezaTransaccion = -1;
            int iCodMolde = -1;
            int iCodBase = -1;

            foreach (DataRow dr in this.dtVaciado.Rows)
            {
                if (Convert.ToBoolean(dr["Capturado"]))
                {
                    iCodConsecutivo = Convert.ToInt32(dr["CodConsecutivo"]);
                    iPosicion = Convert.ToInt32(dr["Posicion"]);
                    sCodBarras = Convert.ToString(dr["CodBarras"]);
                    iCodArticulo = Convert.ToInt32(dr["CodTipoArticulo"]);
                    iCodPieza = Convert.ToInt32(dr["CodPieza"]);
                    iCodMolde = Convert.ToInt32(dr["MoldeMaquina"]);
                    iCodBase = Convert.ToInt32(dr["CodBase"]);

                    if (iCodPieza == -1)
                    {
                        iCodPieza = this.oDA0.InsertarPieza(DA.eTipoConexion.Local,
                                                            this.lu.CodPlanta,
                                                            sCodBarras,
                                                            this.lu.CodConfigBanco,
                                                            iCodConsecutivo,
                                                            iPosicion,
                                                            iCodArticulo,
                                                            this.lu.CodProceso,
                                                            1,
                                                            lu.Fecha,
                                                            iCodMolde,
                                                            iCodBase);
                    }
                    if (!Convert.IsDBNull(dr["CodPrueba"]) && !dr["CodPrueba"].ToString().Equals("-1"))
                    {
                        this.oDA0.PruebaProcesoIns(this.lu.CodPlanta, Convert.ToInt32(dr["CodPrueba"]), this.oDA0.ObtenerCodProcesoVaciado(),
                                                    iCodPieza, DateTime.Now);
                    }


                    lCodPiezaTransaccion = this.oDA0.InsertarPiezaTransaccion(DA.eTipoConexion.Local,
                                                                                this.lu.CodConfigHandHeld,
                                                                                iCodPieza, 
                                                                                this.lu.Fecha);
                }
            }

            // Actualizar 'Vaciadas Acumuladas' en la tabla 'config_banco'.
            this.oDA.ActualizarVaciadasAcumuladas(DA.eTipoConexion.Local, this.lu.CodConfigBanco);
        }
        #endregion InsertarPiezas
        #region EliminarRegistros
        private void EliminarRegistros()
        {
            int iCodPieza;
            foreach (DataRow dr in this.dtVaciado.Rows)
            {
                iCodPieza = Convert.ToInt32(dr["CodPieza"]);
                if (iCodPieza != -1)
                {
                    this.oDA.EliminarDefectosPiezaLocal(iCodPieza, this.lu.CodProceso);
                    this.oDA.EliminarPiezaLocal(iCodPieza);
                }
            }
        }
        #endregion EliminarRegistros
        #region AjustarControlesMoldeBase
        private void AjustarControlesMoldeBase()
        {
            this.txPosicion.Visible = false;
            this.cbxMolde.Visible = true;
            this.cbxBase.Visible = true;
            this.lbBase.Visible = true;
            this.lbPosicion.Text = "Molde:";
            int iValY = 40;
            AjustePosicionY(this.lbEtiqueta, iValY);
            AjustePosicionY(this.txEtiqueta, iValY);
            AjustePosicionY(this.lbModelo, iValY);
            AjustePosicionY(this.cbxModelo, iValY);
            AjustePosicionY(this.lbTipo, iValY);
            AjustePosicionY(this.txTipo, iValY);
            AjustePosicionY(this.lbPrueba, iValY);
            AjustePosicionY(this.cbxPrueba, iValY);
            AjustePosicionY(this.btAceptar, iValY);
            AjustePosicionY(this.btDefectos, iValY);
            AjustePosicionY(this.btCancelar, iValY);
            AjustePosicionY(this.btSiguiente, iValY);
            AjustePosicionY(this.btProcesar, iValY);
        }

        private static void AjustePosicionY(Control cControl, int iValY)
        {
            cControl.Location = new System.Drawing.Point(cControl.Location.X, (cControl.Location.Y + iValY));
        }
        #endregion


        #region EnviarDatosAlServidor
        private void EnviarDatosAlServidor()
        {
            this.encabezado.Mensaje = "Enviando datos...";
            this.Refresh();

            c00_Transacciones ct = new c00_Transacciones();
            bool bEnvioExitoso = ct.EnviarDatosVaciado();
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
                this.txBanco.Text = this.lu.DesMaquina;
                this.txPosicion.Text = this.lu.PosInicial.ToString();
                this.iPosicion = this.lu.PosInicial;

                // Llenar ComboBox 'Prueba'.
                DataTable dtObj = null;
                DataRow drObj = null;
                ComboBox cbxObj = null;

                dtObj = this.oDA0.ObtenerPruebas(this.lu.CodPlanta, this.lu.CodProceso);
                drObj = dtObj.NewRow();
                drObj["CodPrueba"] = -1;
                drObj["DesPrueba"] = "Seleccionar...";
                dtObj.Rows.InsertAt(drObj, 0);
                cbxObj = this.cbxPrueba;
                cbxObj.DataSource = dtObj;
                cbxObj.ValueMember = "CodPrueba";
                cbxObj.DisplayMember = "DesPrueba";
                cbxObj.SelectedValue = -1;

                // Obtener posiciones del banco.
                this.dtVaciado = this.oDA.ObtenerPosicionesBanco(this.lu.CodConfigBanco, true);
                this.dtVaciado.Columns.Add("Capturado", typeof(bool));
                this.dtVaciado.Columns.Add("CodBarras", typeof(string));
                this.dtVaciado.Columns.Add("CodTipoArticulo", typeof(int));
                this.dtVaciado.Columns.Add("CodPrueba", typeof(int));
                this.dtVaciado.Columns.Add("CodPieza", typeof(int));
                this.dtVaciado.Columns.Add("MoldeMaquina", typeof(int));
                this.dtVaciado.Columns.Add("CodBase", typeof(int));
                foreach (DataRow dr in this.dtVaciado.Rows)
                {
                    dr["Capturado"] = false;
                    dr["CodBarras"] = string.Empty;
                    dr["CodTipoArticulo"] = -1;
                    dr["CodPrueba"] = -1;
                    dr["CodPieza"] = -1;
                    dr["MoldeMaquina"] = -1;
                    dr["CodBase"] = -1;
                }
                this.iNumPiezasACapturar = this.dtVaciado.Rows.Count;

                // Llenar el combo Modelos.
                int iCodMolde = Convert.ToInt32(this.dtVaciado.Rows[this.iPosicion - 1]["CodMolde"]);
                dtObj = this.oDA.ObtenerArticulosMolde(iCodMolde, true);
                drObj = dtObj.NewRow();
                drObj["CodArticulo"] = -1;
                drObj["ClaveArticulo"] = "";
                drObj["DesArticulo"] = "Seleccionar...";
                drObj["CodTipoArticulo"] = -1;
                drObj["ClaveTipoArticulo"] = "";
                drObj["DesTipoArticulo"] = "";
                dtObj.Rows.InsertAt(drObj, 0);
                cbxObj = this.cbxModelo;
                cbxObj.ValueMember = "CodArticulo";
                cbxObj.DisplayMember = "DesArticulo";
                cbxObj.SelectedIndexChanged -= new EventHandler(this.cbxModelo_SelectedIndexChanged);
                cbxObj.DataSource = dtObj;
                cbxObj.SelectedValue = -1;
                cbxObj.SelectedIndexChanged += new EventHandler(this.cbxModelo_SelectedIndexChanged);

                this.iCodMoldeActual = iCodMolde;
                if (new c04_CapturaInicial().TieneConfiguracionCapturaVaciado(this.lu.CodPlanta, this.lu.CodMaquina))
                {
                    AjustarControlesMoldeBase();
                    DataTable dt = this.oDA0.ObtenerMoldes();
                    DataRow drn = dt.NewRow();
                    drn["ClaveMolde"] = -1;
                    drn["Descripcion"] = "Seleccionar...";
                    dt.Rows.InsertAt(drn, 0);
                    this.cbxMolde.ValueMember = "ClaveMolde";
                    this.cbxMolde.DisplayMember = "Descripcion";
                    this.cbxMolde.DataSource = dt;
                    this.cbxMolde.SelectedValue = -1;
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
                    this.btAceptar_Click(sender, e);
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
                int iCodArticulo = Convert.ToInt32(cbxObj.SelectedValue);

                if (iCodArticulo == -1)
                {
                    this.encabezado.Mensaje = "Seleccione Modelo";

                    this.txTipo.Text = string.Empty;
                }
                else
                {
                    this.encabezado.Mensaje = String.Empty;
                    this.txTipo.Text = Convert.ToString(((DataRowView)cbxObj.SelectedItem)["DesTipoArticulo"]);
                    this.btAceptar_Click(sender, e);
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
                    int iCodArticulo = Convert.ToInt32(cbxObj.SelectedValue);

                    if (iCodArticulo == -1)
                    {
                        cbxObj.Focus();
                    }
                    else
                    {
                        this.btAceptar_Click(sender, e);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
            }
        }
        #endregion cbxModelo_KeyPress
        #region cbxMolde_SelectedIndexChanged
        private void cbxMolde_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                int iMolde = Convert.ToInt32(this.cbxMolde.SelectedValue);
                this.lu.CodMolde = iMolde;
                if (iMolde != -1)
                {
                    DataTable dt = this.oDA0.ObtenerBase(iMolde);
                    DataRow drn = dt.NewRow();
                    drn["IdBase"] = -1;
                    drn["ClaveBase"] = "...";
                    dt.Rows.InsertAt(drn, 0);
                    this.cbxBase.ValueMember = "IdBase";
                    this.cbxBase.DisplayMember = "ClaveBase";
                    this.cbxBase.DataSource = dt;
                    this.cbxBase.SelectedValue = -1;
                }
                else
                {
                    this.cbxBase.DataSource = null;
                    this.cbxBase.Items.Clear();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
            }
        }
        #endregion
        #region cbxBase_SelectedIndexChanged
        private void cbxBase_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                int iBase = Convert.ToInt32(this.cbxBase.SelectedValue);
                this.lu.CodBase = iBase;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
            }
        }
        #endregion
        #region btAceptar_Click
        private void btAceptar_Click(object sender, EventArgs e)
        {
            try
            {
                Validacion val = this.ValidarPieza(this.txEtiqueta.Text);

                this.encabezado.Mensaje = val.MensajeValidacion;

                if (val.ValidacionExitosa)
                {
                    if (this.lu.CodMolde == -1 && this.cbxMolde.Visible)
                    {
                        this.encabezado.Mensaje = "Seleccione Molde";
                        this.cbxMolde.Focus();
                        return;
                    }
                    if (this.lu.CodBase == -1 && this.cbxBase.Visible)
                    {
                        this.encabezado.Mensaje = "Seleccione Base";
                        this.cbxBase.Focus();
                        return;
                    }
                    if (Convert.ToInt32(this.cbxModelo.SelectedValue) == -1)
                    {
                        this.encabezado.Mensaje = "Seleccione Modelo";
                        //this.cbxModelo.Focus();
                        this.tbxModelo.Focus();
                    }
                    else
                    {
                        // Capturar Datos.
                        this.dtVaciado.Rows[this.iPosicion - 1]["Capturado"] = true;
                        this.dtVaciado.Rows[this.iPosicion - 1]["CodBarras"] = this.txEtiqueta.Text;
                        this.dtVaciado.Rows[this.iPosicion - 1]["CodTipoArticulo"] = Convert.ToInt32(this.cbxModelo.SelectedValue);
                        this.dtVaciado.Rows[this.iPosicion - 1]["CodPrueba"] = Convert.ToInt32(this.cbxPrueba.SelectedValue);
                        this.dtVaciado.Rows[this.iPosicion - 1]["MoldeMaquina"] = this.lu.CodMolde;
                        this.dtVaciado.Rows[this.iPosicion - 1]["CodBase"] = this.lu.CodBase;

                        this.iCodArticuloSel = Convert.ToInt32(this.cbxModelo.SelectedValue);

                        if (this.ObtenerPiezasCapturadas() == this.iNumPiezasACapturar && this.lu.CodMolde == -1 && this.lu.CodBase == -1)
                        {
                            MessageBox.Show("Todas las piezas han sido capturadas", "SCPP", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
                            return;
                        }
                        this.ObtenerDatosSiguientePosicion();
                        this.tbxModelo.Text = string.Empty;
                        this.txEtiqueta.Text = string.Empty;
                        this.txEtiqueta.Focus();
                    }
                }
                else
                {
                    this.txEtiqueta.SelectAll();
                    this.txEtiqueta.Focus();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
            }
        }
        #endregion btAceptar_Click
        #region btSiguiente_Click
        private void btSiguiente_Click(object sender, EventArgs e)
        {
            try
            {
                this.ObtenerDatosSiguientePosicion();

                // Mostrar datos capturados.
                if (Convert.ToBoolean(this.dtVaciado.Rows[this.iPosicion - 1]["Capturado"]))
                {
                    this.txEtiqueta.Text = Convert.ToString(this.dtVaciado.Rows[this.iPosicion - 1]["CodBarras"]);
                    this.cbxModelo.SelectedValue = Convert.ToInt32(this.dtVaciado.Rows[this.iPosicion - 1]["CodTipoArticulo"]);
                    this.cbxPrueba.SelectedValue = Convert.ToInt32(this.dtVaciado.Rows[this.iPosicion - 1]["CodPrueba"]);
                    //this.cbxModelo.Focus();
                    this.tbxModelo.Focus();
                    this.tbxModelo.SelectAll();
                }
                else
                {
                    this.txEtiqueta.Text = string.Empty;
                    this.txEtiqueta.Focus();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
            }
        }
        #endregion btSiguiente_Click
        #region btDefectos_Click
        private void btDefectos_Click(object sender, EventArgs e)
        {
            try
            {
                this.txEtiqueta.SelectAll();
                this.txEtiqueta.Focus();

                a04_CapturaCodigoBarras frmObj = new a04_CapturaCodigoBarras(this.lu, this.dtVaciado);
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
                if (this.ObtenerPiezasCapturadas() == this.iNumPiezasACapturar)
                {
                    MessageBox.Show("Banco Terminado!", "SCPP", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
                }
                else
                {
                    DialogResult dr = MessageBox.Show("Aún hay pendientes, ¿deseas terminar?", "SCPP", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
                    if (dr == DialogResult.No)
                    {
                        return;
                    }
                }

                // Insertar las Piezas.
                this.InsertarPiezas();

                // No se envian los datos aqui, sino hasta 'Configuracion Inicial' - Proceso 'Vaciado' - Opcion 'Enviar datos al servidor'.
                //this.EnviarDatosAlServidor();

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
        #endregion btProcesar_Click
        #region btCancelar_Click
        private void btCancelar_Click(object sender, EventArgs e)
        {
            try
            {
                DialogResult dr = MessageBox.Show("¿Cancelar Captura de Vaciado?", "SCPP", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
                if (dr == DialogResult.Yes)
                {
                    // Eliminar registros creados por la Captura de Defectos.
                    this.EliminarRegistros();

                    // Regresar a Configuracion Inicial.
                    a03_ConfiguracionInicial frmObj = new a03_ConfiguracionInicial(this.lu);
                    frmObj.Show();
                    this.Close();
                }
                else
                {
                    this.txEtiqueta.Text = string.Empty;
                    this.txEtiqueta.Focus();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
            }
        }
        #endregion btCancelar_Click

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
                            this.EliminarRegistros();
                            a03_ConfiguracionInicial frmObj = new a03_ConfiguracionInicial(this.lu);
                            frmObj.Show();
                            this.Close();
                            return;
                        }
                        if (dtModelo == null || dtModelo.Rows.Count == 0)
                        {
                            this.Form_Load(sender, e);
                            dtModelo = this.cbxModelo.DataSource as DataTable;
                            flag = false;
                        }
                    } while (!flag);
                    DataRow[] rows = dtModelo.Select("ClaveArticulo = '" + (sender as TextBox).Text + "'");
                    if (rows.Length == 0)
                        throw new Exception("No se encontró el modelo capturado.");
                    if (rows.Length > 0)
                        this.cbxModelo.SelectedValue = rows[0]["CodArticulo"];
                    //this.btAceptar_Click(sender, e);
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
        private bool EsDidigo(char caracter)
        {
            return (((int)caracter) >= 48 && ((int)caracter) <= 57 || caracter == 8) ? false : true;
        }
        #endregion methods

        private void tbxModelo_LostFocus(object sender, EventArgs e)
        {
            if (Convert.ToString(this.cbxModelo.SelectedValue).Trim() != string.Empty && Convert.ToString(this.cbxModelo.SelectedValue).Trim() != "-1") return;
            this.SetDisEnable(this.btAceptar);
            this.SetDisEnable(this.btDefectos);
            this.SetDisEnable(this.btProcesar);
            this.SetDisEnable(this.btSiguiente);
        }
        private void SetDisEnable(Control control)
        {
            control.Enabled = false;
        }
        private void SetEnable(Control control)
        {
            control.Enabled = true;
        }

        private void tbxModelo_GotFocus(object sender, EventArgs e)
        {
            this.SetEnable(this.btAceptar);
            this.SetEnable(this.btDefectos);
            this.SetEnable(this.btProcesar);
            this.SetEnable(this.btSiguiente);
        }
    }
}