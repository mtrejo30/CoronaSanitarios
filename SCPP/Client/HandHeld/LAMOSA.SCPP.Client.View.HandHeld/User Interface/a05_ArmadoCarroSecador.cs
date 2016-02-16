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
    public partial class a05_ArmadoCarroSecador : Form
    {

        #region fields

        private LoginUsuario lu = null;
        private c00_Common oDA0 = new c00_Common();
        private c05_ArmadoCarroSecado oDA = new c05_ArmadoCarroSecado();

        private Timer trActualizarDatosServidor = new Timer();
        private int iPeriodoActualizacion = -1;

        private int iCodCarro = -1;
        private int iCodPieza = -1;
        private int iCodEstadoPieza = -1;
        private string sDesEstadoPieza = string.Empty;
        private int iCodUltimoProcesoPieza = -1;
        private string sDesUltimoProcesoPieza = string.Empty;
        private int iCodProcesoAct = -1;

        #endregion fields

        #region properties



        #endregion properties

        #region methods

        #region constructors and destructor

        public a05_ArmadoCarroSecador(LoginUsuario lu)
        {
            InitializeComponent();
            //
            this.lu = lu;
            this.ConfigurarFormulario();
            this.ConfigurarPanelControles();
        }
        ~a05_ArmadoCarroSecador()
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

            this.txEtiqueta.Enabled = false;
            this.txEtiqueta.ReadOnly = true;

            this.txCarro.KeyPress += new KeyPressEventHandler(this.txCodCarro_KeyPress);
            this.txEtiqueta.KeyPress += new KeyPressEventHandler(this.txEtiqueta_KeyPress);

            this.btAceptar.Click += new EventHandler(this.btAceptar_Click);
            this.btCancelar.Click += new EventHandler(this.btCancelar_Click);
            this.btTerminar.Click += new EventHandler(this.btTerminar_Click);

            // Configuracion del Timer.
            //this.trActualizarDatosServidor.Interval = 100;
            //this.trActualizarDatosServidor.Enabled = true;
            //this.trActualizarDatosServidor.Tick += new EventHandler(this.trActualizarDatosServidor_Tick);
        }
        #endregion ConfigurarPanelControles

        #region ValidarCarro
        private Validacion ValidarCarro(int iCodPlanta, int iCodProceso, string sCodCarro)
        {
            Validacion val = new Validacion();
            DataTable dtObjLocal = null;
            DataTable dtObjSvr = null;

            // Validar el codigo del carro no sea una cadena nula o vacia.
            if (string.IsNullOrEmpty(sCodCarro))
            {
                val.ValidacionExitosa = false;
                val.MensajeValidacion = "Capture número de Carro";

                this.iCodCarro = -1;
            }
            try
            {
                this.iCodCarro = Convert.ToInt32(sCodCarro);
            }
            catch
            {
                val.ValidacionExitosa = false;
                val.MensajeValidacion = "El carro debe ser numerico y no mayor a 2147483647";
                return val;
            }

            // Validar no este ocupado el numero de carro, en local y en servidor.
            dtObjLocal = this.oDA0.ObtenerPiezasCarro(iCodPlanta, iCodProceso, this.iCodCarro, true);
            dtObjSvr = this.oDA0.ObtenerPiezasCarro(iCodPlanta, iCodProceso, this.iCodCarro, false);

            //if (dtObjLocal.Rows.Count > 0 || dtObjSvr.Rows.Count > 0)
            //{
            //    val.ValidacionExitosa = false;
            //    val.MensajeValidacion = "Carro " + iCodCarro.ToString() + " ocupado";

            //    this.iCodCarro = -1;
            //    return val;
            //}
            //else
            {
                val.ValidacionExitosa = true;
                val.MensajeValidacion = "Capture Piezas";
                return val;
            }
        }
        #endregion ValidarCarro
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

                this.iCodPieza = -1;
                return val;
            }

            // Validar exista la pieza.
            this.iCodPieza = this.oDA0.ObtenerCodPiezaCodBarras(sCodBarras, true);
            if (this.iCodPieza == -1)
            {
                val.ValidacionExitosa = false;
                val.MensajeValidacion = "Pieza no existe";

                return val;
            }

            // Obtener el estado de la pieza.
            dtObj = this.oDA0.ObtenerEstadoPieza(this.iCodPieza, true);
            if (dtObj != null && dtObj.Rows.Count > 0)
            {
                this.iCodEstadoPieza = Convert.ToInt32(dtObj.Rows[0]["CodEstadoPieza"]);
                this.sDesEstadoPieza = Convert.ToString(dtObj.Rows[0]["DesEstadoPieza"]);
            }
            else
            {
                val.ValidacionExitosa = false;
                val.MensajeValidacion = "No hay informacion suficiente para validar la pieza, Realice Sincronizacion.";
                return val;
            }
            // Si la pieza esta 'En Reparacion' o 'En Desperdicio'.
            if (this.iCodEstadoPieza == 2 || this.iCodEstadoPieza == 4)
            {
                val.ValidacionExitosa = false;
                val.MensajeValidacion = "Pieza " + this.sDesEstadoPieza;

                this.iCodPieza = -1;
                this.iCodEstadoPieza = -1;
                this.sDesEstadoPieza = string.Empty;
                return val;
            }

            // Obtener el ultimo proceso de la pieza.
            dtObj = this.oDA0.ObtenerUltimoProcesoPieza(this.iCodPieza, true);
            if (dtObj != null && dtObj.Rows.Count > 0)
            {
                this.iCodUltimoProcesoPieza = Convert.ToInt32(dtObj.Rows[0]["CodProceso"]);
                this.sDesUltimoProcesoPieza = Convert.ToString(dtObj.Rows[0]["DesProceso"]);
            }
            // Validar que la pieza solo este en el proceso de Vaciado.
            if (this.iCodUltimoProcesoPieza != this.iCodProcesoAct)
            {
                val.ValidacionExitosa = false;
                val.MensajeValidacion = "Pieza en proceso: " + this.sDesUltimoProcesoPieza;

                this.iCodPieza = -1;
                this.iCodEstadoPieza = -1;
                this.sDesEstadoPieza = string.Empty;
                this.iCodUltimoProcesoPieza = -1;
                this.sDesUltimoProcesoPieza = string.Empty;
                return val;
            }

            val.ValidacionExitosa = true;
            val.MensajeValidacion = "";
            return val;
        }
        #endregion ValidarPieza
        #region ValidarNoExistePiezaEnCarro
        private Validacion ValidarNoExistePiezaEnCarro(int iCodPlanta, int iCodProceso, int iCodPieza)
        {
            Validacion val = new Validacion();
            int iCodCarro = this.oDA.ExistePiezaEnCarro(iCodPlanta, iCodProceso, iCodPieza, true);
            if (iCodCarro == -1)
            {
                val.ValidacionExitosa = true;
                val.MensajeValidacion = "";
            }
            else
            {
                val.ValidacionExitosa = false;
                val.MensajeValidacion = "Pieza asignada a " + cmbTransporte.Text + ": " + iCodCarro.ToString();
            }

            return val;
        }
        #endregion ValidarNoExistePiezaEnCarro

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

            this.encabezado.Mensaje = String.Empty;
            this.Refresh();
        }
        #endregion EnviarDatosAlServidor
        private void tipoEnvio(Boolean bCancelar)
        {
            String sTras = cmbTransporte.Text.ToString().ToUpper();
            if (sTras.Equals("CANASTILLA") && !bCancelar)
            {
                btTerminar.Text = "Env. Secador";
                this.btTerminar.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            }
            else
            {
                btTerminar.Text = "Terminar";
                this.btTerminar.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Regular);
            }
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
                this.encabezado.Titulo = "Armado Carro Secador";

                // Obtener periodo del timer.
                this.encabezado.Conexion = EstadoConexion.Offline;
                this.iPeriodoActualizacion = this.oDA0.ObtenerPeriodoActualizacion(this.lu.CodProceso, this.lu.CodPantalla);

                this.iCodProcesoAct = this.lu.CodProceso;
                cmbTransporte_SelectedValueChanged(null, null);
                this.cmbTransporte.Focus();

                // Obtener Centros Trabajo.
                DataTable dtObj = this.oDA.ObtenerTransporte();
                ComboBox cbxObj = null;
                cbxObj = this.cmbTransporte;
                cbxObj.ValueMember = "Cod";
                cbxObj.DisplayMember = "Descripcion";
                cbxObj.DataSource = dtObj;
                DataTable dtConfigVaciado = new c00_Transacciones().ObtenerConfigVaciado(this.lu.CodPlanta);
                if ((dtConfigVaciado == null || dtConfigVaciado.Rows.Count <= 0) && cbxObj.Items.Count > 0)
                {
                    int count = cbxObj.Items.Count;
                    for (int i = 0; i < count; i++)
                    {
                        if (((System.Data.DataRowView)(cbxObj.Items[i])).Row.ItemArray[1].ToString().Equals("Canastilla"))
                        {
                            ((System.Data.DataRowView)(cbxObj.Items[i])).Delete();
                            break;
                        }
                    }
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

        #region txCodCarro_KeyPress
        private void txCodCarro_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                // Validar ingreso del Enter.
                if (e.KeyChar == 13)
                {
                    TextBox txObj = (TextBox)sender;
                    string sCarro = txObj.Text;

                    Validacion val = this.ValidarCarro(this.lu.CodPlanta, this.lu.CodProceso, sCarro);
                    this.encabezado.Mensaje = val.MensajeValidacion;

                    if (!val.ValidacionExitosa)
                    {
                        txObj.SelectAll();
                        txObj.Focus();
                    }
                    else
                    {
                        tipoEnvio(false);
                        txObj.Enabled = false;
                        txObj.ReadOnly = true;
                        cmbTransporte.Enabled = false;
                        this.txEtiqueta.Enabled = true;
                        this.txEtiqueta.ReadOnly = false;

                        this.txEtiqueta.SelectAll();
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
        #endregion txCodCarro_KeyPress
        #region txEtiqueta_KeyPress
        private void txEtiqueta_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                // Validar ingreso del Enter.
                if (e.KeyChar == 13)
                {
                    TextBox txObj = (TextBox)sender;
                    string sCodBarras = txObj.Text;
                    Validacion val = null;

                    val = this.ValidarPieza(sCodBarras);
                    if (!val.ValidacionExitosa)
                    {
                        this.encabezado.Mensaje = val.MensajeValidacion;

                        txObj.SelectAll();
                        txObj.Focus();
                        return;
                    }

                    val = this.ValidarNoExistePiezaEnCarro(this.lu.CodPlanta, this.lu.CodProceso, this.iCodPieza);
                    if (!val.ValidacionExitosa)
                    {
                        this.encabezado.Mensaje = val.MensajeValidacion;

                        txObj.SelectAll();
                        txObj.Focus();
                        return;
                    }

                    this.encabezado.Mensaje = String.Empty;
                    int iTransporte = Convert.ToInt32(cmbTransporte.SelectedValue);
                    // Asociar Pieza con el Carro.
                    this.oDA.InsertarCarroPieza(DA.eTipoConexion.Local, this.lu.CodPlanta, this.lu.CodProceso, this.iCodCarro, this.iCodPieza, null, iTransporte);
                    this.iCodPieza = -1;

                    txObj.Text = String.Empty;
                    txObj.Focus();
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
                if (this.iCodCarro == -1)
                {
                    this.encabezado.Mensaje = "Capture número de carro";
                    this.txCarro.SelectAll();
                    this.txCarro.Focus();
                    return;
                }

                DataTable dtRes = this.oDA0.ObtenerPiezasCarro(this.lu.CodPlanta, this.lu.CodProceso, this.iCodCarro, true);
                if (dtRes.Rows.Count == 0)
                {
                    this.encabezado.Mensaje = "Carro sin piezas registradas";
                    this.txEtiqueta.Text = String.Empty;
                    this.txEtiqueta.Focus();
                }
                else
                {
                    this.iCodCarro = -1;

                    this.encabezado.Mensaje = "Armado de carro completado";

                    this.txEtiqueta.Enabled = false;
                    this.txEtiqueta.ReadOnly = true;
                    this.txCarro.Enabled = true;
                    this.txCarro.ReadOnly = false;
                    this.cmbTransporte.Enabled = true;

                    this.txCarro.Text = String.Empty;
                    this.txEtiqueta.Text = String.Empty;
                    this.txCarro.Focus();
                }
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
                if (this.iCodCarro != -1)
                {
                    DialogResult dr = MessageBox.Show("¿Cancelar armado de Carro?", "SCPP", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
                    if (dr == DialogResult.Yes)
                    {
                        // Borrar el carro armado.
                        this.oDA0.EliminarCarro(DA.eTipoConexion.Local, this.lu.CodPlanta, this.iCodProcesoAct, this.iCodCarro);

                        this.iCodCarro = -1;

                        this.encabezado.Mensaje = String.Empty;

                        this.cmbTransporte.Enabled = true;
                        this.txCarro.Enabled = true;
                        this.txCarro.ReadOnly = false;
                        this.txEtiqueta.Enabled = false;
                        this.txEtiqueta.ReadOnly = true;

                        this.txCarro.Text = String.Empty;
                        this.txEtiqueta.Text = String.Empty;
                        this.txCarro.Focus();
                        tipoEnvio(true);
                    }
                    else
                    {
                        this.txEtiqueta.Text = String.Empty;
                        this.txEtiqueta.Focus();
                    }
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
                if (this.iCodCarro != -1)
                {
                    MessageBox.Show("Carro abierto todavia", "SCPP", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);

                    this.txEtiqueta.Text = String.Empty;
                    this.txEtiqueta.Focus();
                }
                else
                {
                    // No se envian los datos aqui, sino hasta 'Configuracion Inicial' - Proceso 'Vaciado' - Opcion 'Enviar datos al servidor'.
                    //this.EnviarDatosAlServidor();

                    //Regresar a Configuracion Inicial.
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
        #endregion btTerminar_Click

        private void cmbTransporte_SelectedValueChanged(object sender, EventArgs e)
        {
            lbCarro.Text = cmbTransporte.Text + ":";
        }


        #endregion event handlers

        #endregion methods

    }
}