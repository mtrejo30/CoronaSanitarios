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
    public partial class a04_CapturaCodigoBarras : Form
    {

        #region fields

        private LoginUsuario lu = null;
        private c00_Common oDA0 = new c00_Common();

        private Timer trActualizarDatosServidor = new Timer();
        private int iPeriodoActualizacion = -1;

        private int iCodPieza = -1;
        private DataTable dtVaciado = null;
        private Boolean bDesperdicio = false;

        #endregion fields

        #region properties



        #endregion properties

        #region methods

        #region constructors and destructor

        public a04_CapturaCodigoBarras(LoginUsuario lu, DataTable dtVaciado)
        {
            InitializeComponent();
            //
            this.lu = lu;
            this.dtVaciado = dtVaciado;
            this.ConfigurarFormulario();
            this.ConfigurarPanelControles();
        }
        public a04_CapturaCodigoBarras(LoginUsuario lu)
        {
            InitializeComponent();
            bDesperdicio = true;
            this.lu = lu;
            this.ConfigurarFormulario();
            this.ConfigurarPanelControles();
        }
        ~a04_CapturaCodigoBarras()
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

            this.txEtiqueta.TextAlign = HorizontalAlignment.Center;
            this.txEtiqueta.MaxLength = 15;
            this.txEtiqueta.KeyPress += new KeyPressEventHandler(this.txEtiqueta_KeyPress);

            this.btCancelar.Click += new EventHandler(this.btCancelar_Click);

            // Configuracion del Timer.
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

                this.iCodPieza = -1;
            }
            else
            {
                if (!bDesperdicio)//Si fue mandado llamar desde Vaciado bDesperdicio = false
                {
                    // Buscar la pieza si ya fue capturada.
                    // No ha sido capturada se inserta.

                    int iCodConsecutivo = -1;
                    int iPosicion = -1;
                    int iCodArticulo = -1;
                    int iCodMolde = -1;
                    int iCodBase = -1;

                    foreach (DataRow dr in this.dtVaciado.Rows)
                    {
                        if (Convert.ToString(dr["CodBarras"]) == sCodBarras)
                        {
                            this.iCodPieza = Convert.ToInt32(dr["CodPieza"]);
                            if (this.iCodPieza == -1)
                            {
                                iCodConsecutivo = Convert.ToInt32(dr["CodConsecutivo"]);
                                iPosicion = Convert.ToInt32(dr["Posicion"]);
                                iCodArticulo = Convert.ToInt32(dr["CodTipoArticulo"]);
                                iCodMolde = Convert.ToInt32(dr["MoldeMaquina"]);
                                iCodBase = Convert.ToInt32(dr["CodBase"]);

                                this.iCodPieza = this.oDA0.InsertarPieza(DataAccess.eTipoConexion.Local,
                                                                            this.lu.CodPlanta,
                                                                            sCodBarras,
                                                                            this.lu.CodConfigBanco,
                                                                            iCodConsecutivo,
                                                                            iPosicion,
                                                                            iCodArticulo,
                                                                            this.lu.CodProceso,
                                                                            1,
                                                                            this.lu.Fecha,
                                                                            iCodMolde,
                                                                            iCodBase);
                                dr["CodPieza"] = this.iCodPieza;
                            }
                            break;
                        }
                    }
                    if (this.iCodPieza == -1)
                    {
                        val.ValidacionExitosa = false;
                        val.MensajeValidacion = "Etiqueta no capturada";
                    }
                    else
                    {
                        val.ValidacionExitosa = true;
                        val.MensajeValidacion = string.Empty;
                    }
                }//Inicia validacion Opcion Baja Pieza
                else
                {
                    this.iCodPieza = oDA0.ObtenerCodPiezaCodBarras(sCodBarras, false);
                    if (iCodPieza == -1)
                    {
                        val.ValidacionExitosa = false;
                        val.MensajeValidacion = "Pieza no existe";
                        return val;
                    }
                    DataTable dtProcesoPieza = oDA0.ObtenerUltimoProcesoPieza(this.iCodPieza, false);
                    if (dtProcesoPieza == null || dtProcesoPieza.Rows.Count == 0)
                    {
                        val.ValidacionExitosa = false;
                        val.MensajeValidacion = "Pieza no existe en ningun proceso.";
                        return val;
                    }
                    if(Convert.ToInt32(dtProcesoPieza.Rows[0]["CodProceso"]) != this.lu.CodProceso)
                    {
                        val.ValidacionExitosa = false;
                        val.MensajeValidacion = "Ingresar al proceso " + dtProcesoPieza.Rows[0]["DesProceso"].ToString() + ", para efectuar la baja de pieza.";
                        return val;
                    }
                    int iCodEstadoPieza = -1;
                    string sDesEstadoPieza = string.Empty;
                    DataTable dtObj = oDA0.ObtenerEstadoPieza(iCodPieza, false);
                    if (dtObj != null)
                    {
                        if (dtObj.Rows.Count > 0 & dtObj.Columns.Count > 0)
                        {
                            if (dtObj.Columns.Contains("CodEstadoPieza"))
                                iCodEstadoPieza = Convert.ToInt32(dtObj.Rows[0]["CodEstadoPieza"]);
                            if (dtObj.Columns.Contains("DesEstadoPieza"))
                                sDesEstadoPieza = Convert.ToString(dtObj.Rows[0]["DesEstadoPieza"]);
                        }
                    }
                    if (iCodEstadoPieza == oDA0.ObtenerCodEstadoPiezaEnDesperdicio())
                    {
                        val.ValidacionExitosa = false;
                        val.MensajeValidacion = "Esta pieza se encuentra en Desperdicio";
                    }
                    else
                    {
                        val.ValidacionExitosa = true;
                        val.MensajeValidacion = string.Empty;
                    }
                }
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
                this.encabezado.Titulo = "Captura de Etiqueta";

                // Obtener periodo del timer.
                this.encabezado.Conexion = EstadoConexion.Offline;
                this.iPeriodoActualizacion = this.oDA0.ObtenerPeriodoActualizacion(this.lu.CodProceso, this.lu.CodPantalla);

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

                    if (val.ValidacionExitosa)
                    {
                        this.encabezado.Mensaje = String.Empty;

                        this.lu.CodPieza = this.iCodPieza;
                        this.lu.CodBarras = this.txEtiqueta.Text;
                        a04_Defectos frmObj = bDesperdicio ? new a04_Defectos(this.lu, false, bDesperdicio) : frmObj = new a04_Defectos(this.lu, true);
                        txObj.Text = "";
                        frmObj.Show();
                        if (!bDesperdicio)
                            this.Close();
                    }
                    else
                    {
                        this.encabezado.Mensaje = val.MensajeValidacion;

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

        #region btCancelar_Click
        private void btCancelar_Click(object sender, EventArgs e)
        {
            try
            {
                if (bDesperdicio)
                {
                    a03_ConfiguracionInicial frmObj = new a03_ConfiguracionInicial(this.lu);
                    frmObj.Show();
                }
                this.Close();
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