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
    public partial class a05_CapturaVaciado : Form
    {

        #region fields

        private LoginUsuario lu = null;
        private cCapturaVaciado oDA = new cCapturaVaciado();
        private DataTable dtVaciado = null;
        private int iPosicion = -1;
        private int iNumPiezasACapturar = -1;

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
            this.Text = "";

            // Layout.
            this.WindowState = FormWindowState.Maximized;

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

            this.txPieza.KeyUp += new KeyEventHandler(this.txPieza_KeyUp);
            this.cbxModelo.SelectedIndexChanged += new EventHandler(this.cbxModelo_SelectedIndexChanged);

            this.btCapturar.Click += new EventHandler(this.btCapturar_Click);
            this.btSiguiente.Click += new EventHandler(this.btSiguiente_Click);
            this.btDefectos.Click += new EventHandler(this.btDefectos_Click);
            this.btTerminar.Click += new EventHandler(this.btTerminar_Click);
            this.btCancelar.Click += new EventHandler(this.btCancelar_Click);
            this.btSalir.Click += new EventHandler(this.btSalir_Click);
        }
        #endregion ConfigurarPanelControles
        #region ConfigurarCabecera
        private void ConfigurarCabecera()
        {
            // Ajustar Logo.
            this.pbLogo.Top = 0;
            this.pbLogo.Left = this.Width - this.pbLogo.Width;
            // Ajustar Boton Salir.
            this.btSalir.Top = this.pbLogo.Height;
            this.btSalir.Left = this.Width - this.btSalir.Width;
            // Ajustar ProgressBar Procesando.
            this.pbrProcesando.Top = this.pbLogo.Height;
            this.pbrProcesando.Left = this.Width - this.pbLogo.Width;

            // Ajustar Panel.
            int PosX = (int)((this.Width - this.pnControles.Width) / 2);
            int PosY = 55;
            this.pnControles.Location = new Point(PosX, PosY);
        }
        #endregion ConfigurarCabecera

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
            // Ajusta Posicion.
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
            dtObj = this.oDA.ObtenerArticulosMolde(iCodMolde);
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
            cbxObj.DataSource = dtObj;
            cbxObj.SelectedValue = -1;
        }
        #endregion ObtenerDatosSiguientePosicion
        #region InsertarPiezas
        private void InsertarPiezas()
        {
            DataTable dtObj = null;
            int iCodPieza = -1;
            string sCodBarras = string.Empty;
            int iCodConsecutivo = -1;
            int iPosicion = -1;
            int iCodArticulo = -1;

            foreach (DataRow dr in this.dtVaciado.Rows)
            {
                if (Convert.ToBoolean(dr["Capturado"]))
                {
                    dtObj = this.oDA.ObtenerSigCodPieza();
                    iCodPieza = Convert.ToInt32(dtObj.Rows[0]["CodPieza"]);
                    sCodBarras = Convert.ToString(dr["CodBarras"]);
                    iCodArticulo = Convert.ToInt32(dr["CodTipoArticulo"]);
                    iCodConsecutivo = Convert.ToInt32(dr["CodConsecutivo"]);
                    iPosicion = Convert.ToInt32(dr["Posicion"]);

                    this.oDA.InsertarPieza(this.lu.CodPlanta, iCodPieza, sCodBarras, this.lu.CodConfigBanco, iCodConsecutivo, iPosicion, iCodArticulo);
                }
            }
        }
        #endregion InsertarPiezas

        #endregion common

        #region event handlers

        #region Form_Load
        private void Form_Load(object sender, EventArgs e)
        {
            try
            {
                this.lbOperador.Text = lu.NomEmpleado;
                this.lbPuesto.Text = lu.DesPuesto;
                this.lbPlanta.Text = lu.DesPlanta;
                this.lbProceso.Text = "Captura Vaciado";

                this.txBanco.Text = this.lu.DesMaquina;
                this.txPosicion.Text = this.lu.PosInicial.ToString();
                this.iPosicion = this.lu.PosInicial;

                // Llenar ComboBox 'Prueba'.
                DataTable dtObj = null;
                DataRow drObj = null;
                ComboBox cbxObj = null;

                dtObj = this.oDA.ObtenerPruebas(this.lu.CodPlanta, this.lu.CodProceso);
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
                this.dtVaciado = this.oDA.ObtenerPosicionesBanco(this.lu.CodConfigBanco);
                this.dtVaciado.Columns.Add("Capturado", typeof(bool));
                this.dtVaciado.Columns.Add("CodBarras", typeof(string));
                this.dtVaciado.Columns.Add("CodTipoArticulo", typeof(int));
                this.dtVaciado.Columns.Add("CodPrueba", typeof(int));
                foreach (DataRow dr in this.dtVaciado.Rows)
                {
                    dr["Capturado"] = false;
                    dr["CodBarras"] = "";
                    dr["CodBarras"] = -1;
                    dr["CodBarras"] = -1;
                }
                this.iNumPiezasACapturar = this.dtVaciado.Rows.Count;

                // Llenar el combo Modelos.
                int iCodMolde = Convert.ToInt32(this.dtVaciado.Rows[this.iPosicion - 1]["CodMolde"]);
                dtObj = this.oDA.ObtenerArticulosMolde(iCodMolde);
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
                cbxObj.DataSource = dtObj;
                cbxObj.SelectedValue = -1;
                
                this.cbxModelo.Focus();
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
                this.ConfigurarCabecera();
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

        #region txPieza_KeyUp
        private void txPieza_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                this.btCapturar_Click(sender, e);
            }
        }
        #endregion txPieza_KeyUp
        #region cbxModelo_SelectedIndexChanged
        private void cbxModelo_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                ComboBox cbxObj = (ComboBox)sender;
                int iCodArticulo = Convert.ToInt32(cbxObj.SelectedValue);

                if (iCodArticulo == -1)
                {
                    this.txTipo.Text = "";
                    cbxObj.Focus();
                }
                else
                {
                    this.txTipo.Text = Convert.ToString(((DataRowView)cbxObj.SelectedItem)["DesTipoArticulo"]);
                    this.btCapturar.Focus();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
            }
        }
        #endregion cbxModelo_SelectedIndexChanged

        #region btCapturar_Click
        private void btCapturar_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(this.txPieza.Text))
                {
                    this.lbMensaje.Text = "Capture Pieza";
                    this.txPieza.SelectAll();
                    this.txPieza.Focus();
                }
                else
                {
                    // Validar no se haya capturado el mismo Codigo de Barras.
                    if (this.ValidarExisteCodBarras(this.txPieza.Text))
                    {
                        this.lbMensaje.Text = "Pieza ya capturada";
                        this.txPieza.Text = "";
                        this.txPieza.Focus();
                    }
                    else if(Convert.ToInt32(this.cbxModelo.SelectedValue) == -1)
                    {
                        this.lbMensaje.Text = "Seleccione Modelo";
                        this.cbxModelo.Focus();
                    }
                    else
                    {
                        // Capturar Datos.
                        this.dtVaciado.Rows[this.iPosicion - 1]["Capturado"] = true;
                        this.dtVaciado.Rows[this.iPosicion - 1]["CodBarras"] = this.txPieza.Text;
                        this.dtVaciado.Rows[this.iPosicion - 1]["CodTipoArticulo"] = Convert.ToInt32(this.cbxModelo.SelectedValue);
                        this.dtVaciado.Rows[this.iPosicion - 1]["CodPrueba"] = Convert.ToInt32(this.cbxPrueba.SelectedValue);

                        if (this.ObtenerPiezasCapturadas() == this.iNumPiezasACapturar)
                        {
                            MessageBox.Show("Todas las Piezas han sido capturadas", "SCPP", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
                            return;
                        }

                        this.ObtenerDatosSiguientePosicion();

                        this.lbMensaje.Text = "";
                        this.txPieza.Text = "";
                        this.txPieza.Focus();
                    }

                    
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
            }
        }
        #endregion btCapturar_Click
        #region btSiguiente_Click
        private void btSiguiente_Click(object sender, EventArgs e)
        {
            try
            {
                this.ObtenerDatosSiguientePosicion();

                // Mostrar datos capturados.
                if (Convert.ToBoolean(this.dtVaciado.Rows[this.iPosicion - 1]["Capturado"]))
                {
                    this.cbxModelo.SelectedValue = Convert.ToInt32(this.dtVaciado.Rows[this.iPosicion - 1]["CodTipoArticulo"]);
                    this.txPieza.Text = Convert.ToString(this.dtVaciado.Rows[this.iPosicion - 1]["CodBarras"]);
                    this.cbxPrueba.SelectedValue = Convert.ToInt32(this.dtVaciado.Rows[this.iPosicion - 1]["CodPrueba"]);
                    this.cbxModelo.Focus();
                }
                else
                {
                    this.txPieza.Text = "";
                    this.txPieza.Focus();
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
            a04_Defectos frmObj = new a04_Defectos(this.lu);
            frmObj.Show();
        }
        #endregion btDefectos_Click
        #region btTerminar_Click
        private void btTerminar_Click(object sender, EventArgs e)
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

            // Regresar a Configuracion Inicial.
            a03_ConfiguracionInicial frmObj = new a03_ConfiguracionInicial(this.lu);
            frmObj.Show();
            this.Close();
        }
        #endregion btTerminar_Click
        #region btCancelar_Click
        private void btCancelar_Click(object sender, EventArgs e)
        {
            try
            {
                DialogResult dr = MessageBox.Show("¿Cancelar Captura de Vaciado?", "SCPP", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
                if (dr == DialogResult.Yes)
                {
                    // Regresar a Configuracion Inicial.
                    a03_ConfiguracionInicial frmObj = new a03_ConfiguracionInicial(this.lu);
                    frmObj.Show();
                    this.Close();
                }
                else
                {
                    this.txPieza.Text = "";
                    this.txPieza.Focus();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
            }
        }
        #endregion btCancelar_Click
        #region btSalir_Click
        private void btSalir_Click(object sender, EventArgs e)
        {
            DialogResult dr = MessageBox.Show("¿Salir de la Aplicación?", "SCPP", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
            if (dr == DialogResult.Yes)
            {
                Application.Exit();
            }
        }
        #endregion btSalir_Click

        #endregion event handlers

        #endregion methods

    }
}