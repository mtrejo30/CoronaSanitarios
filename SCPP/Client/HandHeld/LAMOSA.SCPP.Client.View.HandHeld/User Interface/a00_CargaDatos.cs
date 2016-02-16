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
    public partial class a00_CargaDatos : Form
    {

        #region Fields

        private c00_CargaDatos oDA = new c00_CargaDatos();
        private c00_Common oDA0 = new c00_Common();
        Timer tSync = new Timer();
        private int iPeriodoActualizacion = -1;

        private static Form frmSender = null;
        private int iCodPlanta = -1;
        private int iCodProceso = -1;
        private int iCodPantalla = -1;
        private Boolean isProcess = false;

        private Boolean esCatalogo = false;
        #endregion Fields

        #region Properties

        private Form FormCalling { get { return frmSender; } }

        #endregion Properties

        #region methods

        #region Constructors and Destructor

        public a00_CargaDatos()
        {
            InitializeComponent();
            //
            this.ConfigurarFormulario();
            this.ConfigurarPanelControles();
        }
        public a00_CargaDatos(int iCodPlanta, int iCodProceso, int iCodPantalla)
        {
            InitializeComponent();
            //
            this.ConfigurarFormulario();
            this.ConfigurarPanelControles();
            isProcess = true;
            this.ActivarControles(false);
            this.iCodPlanta = iCodPlanta;
            this.iCodProceso = iCodProceso;
            this.iCodPantalla = iCodPantalla;
        }
        public a00_CargaDatos(int iCodPlanta, int iCodProceso, bool esCatalogo)
        {
            InitializeComponent();
            //
            this.ConfigurarFormulario();
            this.ConfigurarPanelControles();
            isProcess = true;
            this.ActivarControles(false);
            this.iCodPlanta = iCodPlanta;
            this.iCodProceso = iCodProceso;
            this.esCatalogo = esCatalogo;
        }
        ~a00_CargaDatos()
        {

        }

        #endregion Constructors and Destructor

        #region Common

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

            this.cbxPlanta.SelectedIndexChanged += new EventHandler(this.cbxPlanta_SelectedIndexChanged);
            this.cbxPlanta.KeyPress += new KeyPressEventHandler(this.cbxPlanta_KeyPress);
            this.cbxProceso.SelectedIndexChanged += new EventHandler(this.cbxProceso_SelectedIndexChanged);
            this.cbxProceso.KeyPress += new KeyPressEventHandler(this.cbxProceso_KeyPress);

            this.btAceptar.Click += new EventHandler(this.btAceptar_Click);
            this.btCancelar.Click += new EventHandler(this.btCancelar_Click);

            // Configuracion del Timer.
        }
        #endregion ConfigurarPanelControles

        #region ActivarControles
        private void ActivarControles(bool bActivos)
        {
            this.cbxPlanta.Enabled = bActivos;
            this.cbxProceso.Enabled = bActivos;
            this.btAceptar.Enabled = bActivos;
            this.btCancelar.Enabled = bActivos;
        }
        #endregion ActivarControles
        #region SetFormCalling
        public void SetFormCalling(Form frmCalling)
        {
            frmSender = frmCalling;
        }
        #endregion SetFormCalling

        #endregion Common

        #region event handlers

        #region Form_Load
        private void Form_Load(object sender, EventArgs e)
        {
            try
            {
                tSync.Interval = 3000;
                tSync.Enabled = true;
                if (iCodProceso != -1)
                    tSync.Tick += new EventHandler(this.btAceptar_Click);
                else
                    tSync.Tick += new EventHandler(this.CargaPiezasRequeme);
            }
            catch (Exception)
            {
                MessageBox.Show("No hay conexion con el servidor, pero puede seguir adelante.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
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


        #region cbxPlanta_SelectedIndexChanged
        private void cbxPlanta_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                ComboBox cbxObj = (ComboBox)sender;

                this.iCodPlanta = Convert.ToInt32(cbxObj.SelectedValue);

                if (this.iCodPlanta == -1)
                {
                    this.encabezado.Mensaje = "Seleccione Planta";

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
        #endregion cbxPlanta_SelectedIndexChanged
        #region cbxPlanta_KeyPress
        private void cbxPlanta_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                ComboBox cbxObj = (ComboBox)sender;

                // Validar ingreso del Enter.
                if (e.KeyChar == 13)
                {
                    if (this.iCodPlanta == -1)
                    {
                        this.encabezado.Mensaje = "Seleccione Planta";
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
        #endregion cbxPlanta_KeyPress
        #region cbxProceso_SelectedIndexChanged
        private void cbxProceso_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                ComboBox cbxObj = (ComboBox)sender;

                this.iCodProceso = Convert.ToInt32(cbxObj.SelectedValue);

                if (this.iCodPlanta == -1)
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
                        this.encabezado.Mensaje = "Seleccione Proceso";

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
        #endregion cbxProceso_KeyPress

        #region btAceptar_Click
        private void btAceptar_Click(object sender, EventArgs e)
        {
            try
            {
                Button btObj = this.btAceptar;//(Button)sender;

                this.encabezado.Mensaje = "Recopilando informacion";

                btObj.Enabled = false;
                this.btCancelar.Enabled = false;
                Refresh();

                HHsvc.SCPP_HH proxy = DA.ConfigDataAccess.ObtenerServiceProxy();
                //iCodPlanta = isProcess ? iCodPlanta : Convert.ToInt32(cbxPlanta.SelectedValue);
                //iCodProceso = isProcess ? iCodProceso : Convert.ToInt32(cbxProceso.SelectedValue);

                DataTable dtTablasActualizar = new DataTable();

                // Carga de tablas Por Proceso
                if (!this.esCatalogo)
                    dtTablasActualizar = proxy.TablasProcesoHH(iCodPlanta, true,
                                                                1, true,
                                                                iCodPantalla, true);
                else
                    dtTablasActualizar = proxy.TablasProcesoHH(0, true,
                                                                0, true,
                                                                iCodPantalla, true);

                int cantRows = 0;
                if (dtTablasActualizar != null)
                    cantRows = dtTablasActualizar.Rows.Count;
                int contTables = 1;

                pbrProcesando.Minimum = 0;
                pbrProcesando.Maximum = cantRows;

                Boolean errorInserciones = false;
                String sRes = String.Empty;
                //Carga de Informacion Para los Catalogos
                foreach (DataRow dr in dtTablasActualizar.Rows)
                {
                    try
                    {
                        String table = dr[0].ToString();
                        this.encabezado.Mensaje = "Actualizando tabla: " + table.Replace("_", " ") + " " + contTables + "/" + cantRows;
                        Refresh();
                        Boolean error = esCatalogo ? oDA.ActualizarTablasCatalogos(table, iCodPlanta, iCodProceso) :
                                                     oDA.ActualizarTablasTransaccionales(table, iCodPlanta, iCodProceso);
                        pbrProcesando.Value = contTables++;
                        if (error)
                            errorInserciones = error;
                    }
                    catch (Exception er)
                    {
                        errorInserciones = true;
                        proxy.InsertaError("Tabla:" + dr[0].ToString(), er.Message);
                    }
                }


                // Actualizar fecha de ultima actualizacion de catalogos.
                //if (!errorInserciones)
                DateTime dtFecha = DateTime.Now;
                bool bFecha = false;
                proxy.ObtenerFechaServidor(out dtFecha, out bFecha);


                if (esCatalogo)
                    this.oDA0.EstablecerFechaUltimaActualizacion(0, iCodProceso, dtFecha);
                else
                {
                    this.oDA0.EstablecerFechaUltimaActualizacion(iCodProceso, 0, dtFecha);

                }
            }
            catch (Exception ex)
            {
                String m = iCodPantalla != -1 ? this.encabezado.Mensaje : "";
                this.encabezado.Mensaje = String.Empty;
                Refresh();
                this.btCancelar.Enabled = true;
                MessageBox.Show(ex.Message + "---" + m, "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
            }
            tSync.Enabled = false;
            tSync.Dispose();
            this.Close();
        }

        #endregion btAceptar_Click
        #region btAceptar_Click
        private void CargaPiezasRequeme(object sender, EventArgs e)
        {
            try
            {
                String table = "Pieza";
                this.encabezado.Mensaje = "Actualizando tabla: " + table.Replace("_", " ") + ", piezas de Requeme ";
                Refresh();
                c09_CapturaHornos capHornos = new c09_CapturaHornos();
                capHornos.InsertarPiezasRequeme(this.iCodPlanta);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
            }
            tSync.Enabled = false;
            tSync.Dispose();
            this.Close();
        }
        #endregion
        #region btCancelar_Click
        private void btCancelar_Click(object sender, EventArgs e)
        {
            try
            {
                a01_Login frmObj = new a01_Login();
                frmObj.Show();
                this.Hide();
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
