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
    public partial class a04_CapturaInicial : Form
    {

        #region fields

        private LoginUsuario lu = null;
        private cCapturaInicial oDA = new cCapturaInicial();
        private int iCodSupervisor = -1;

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

            this.txOperador.ReadOnly = true;
            this.txOperador.Enabled = false;

            this.txSupervisor.KeyPress += new KeyPressEventHandler(this.txSupervisor_KeyPress);
            this.txSupervisor.LostFocus += new EventHandler(this.txSupervisor_LostFocus);
            this.cbxMaquina.SelectedIndexChanged += new EventHandler(this.cbxMaquina_SelectedIndexChanged);
            this.txPosicionInicial.KeyPress += new KeyPressEventHandler(this.txPosicionInicial_KeyPress);
            this.txPosicionInicial.LostFocus += new EventHandler(this.txPosicionInicial_LostFocus);
            this.txCarro.KeyPress += new KeyPressEventHandler(this.txCarro_KeyPress);
            this.txCarro.LostFocus += new EventHandler(this.txCarro_LostFocus);
            this.rbAscendente.CheckedChanged += new EventHandler(this.rbDireccion_CheckedChanged);
            this.rbDescendente.CheckedChanged += new EventHandler(this.rbDireccion_CheckedChanged);
            this.btContinuar.Click += new EventHandler(this.btContinuar_Click);
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

        #region ValidarCodSupervisor
        private bool ValidarCodSupervisor(TextBox txObj)
        {
            if (string.IsNullOrEmpty(txObj.Text))
            {
                this.lu.CodSupervisor = -1;
                this.lbMensaje.Text = "Capture Clave Supervisor";
                return false;
            }
            else
            {
                int iClaveEmpleadoMFG = Convert.ToInt32(txObj.Text);
                DataTable dtObj = this.oDA.ValidarEmpleadoMFG(iClaveEmpleadoMFG);
                if (dtObj.Rows.Count == 0)
                {
                    this.lu.CodSupervisor = -1;
                    this.lbMensaje.Text = "Clave Supervisor invalida";
                    return false;
                }
                else
                {
                    this.lu.CodSupervisor = Convert.ToInt32(dtObj.Rows[0]["CodEmpleado"]);
                    this.lbMensaje.Text = "";
                    return true;
                }
            } 
        }
        #endregion ValidarCodSupervisor
        #region ValidarPosicionInicial
        private bool ValidarPosicionInicial(TextBox txObj)
        {
            if (string.IsNullOrEmpty(txObj.Text))
            {
                this.lu.PosInicial = -1;
                this.lbMensaje.Text = "Capture Posicion Inicial";
                return false;
            }
            else
            {
                // Validar Posicion Inicial.
                int iPosInicial = Convert.ToInt32(txObj.Text);
                int iCodConfigBanco = Convert.ToInt32(((DataRowView)this.cbxMaquina.SelectedItem)["CodConfigBanco"]);
                DataTable dtObj = this.oDA.ObtenerNumPosicionesBanco(iCodConfigBanco);
                int iNumPosiciones = Convert.ToInt32(dtObj.Rows[0]["NumPosBanco"]);
                if (iPosInicial >= 1 && iPosInicial <= iNumPosiciones)
                {
                    this.lu.PosInicial = iPosInicial;
                    this.lbMensaje.Text = "";
                    return true;
                }
                else
                {
                    this.lu.PosInicial = -1;
                    this.lbMensaje.Text = "Pos. Inicial entre 1 y " + iNumPosiciones.ToString();
                    return false;
                }
            }
        }
        #endregion ValidarPosicionInicial
        #region ValidarNumeroCarro
        private bool ValidarNumeroCarro(TextBox txObj)
        {
            if (string.IsNullOrEmpty(txObj.Text))
            {
                this.lu.CodCarro = -1;
                this.lbMensaje.Text = "Capture Numero de Carro";
                return false;
            }
            else
            {
                this.lu.CodCarro = Convert.ToInt32(txObj.Text); ;
                this.lbMensaje.Text = "";
                return true;
            }
        }
        #endregion ValidarNumeroCarro

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

                switch(this.lu.DesProceso)
                {
                    case "Vaciado":
                        this.lbProceso.Text = "Vaciado - Captura inicial";
                        this.lbMaquina.Text = "Banco:";
                        this.lbPosicionInicial.Visible = true;
                        this.txPosicionInicial.Visible = true;
                        this.lbColor.Visible = false;
                        this.cbxColor.Visible = false;
                        this.rbAscendente.Visible = true;
                        this.rbDescendente.Visible = true;
                        this.lbCarro.Visible = false;
                        this.txCarro.Visible = false;
                        break;
                    case "Secado":
                        this.lbProceso.Text = "Secado - Captura inicial";
                        this.lbMaquina.Text = "Secador:";
                        this.lbPosicionInicial.Visible = false;
                        this.txPosicionInicial.Visible = false;
                        this.lbColor.Visible = false;
                        this.cbxColor.Visible = false;
                        this.rbAscendente.Visible = false;
                        this.rbDescendente.Visible = false;
                        this.lbCarro.Visible = false;
                        this.txCarro.Visible = false;
                        break;
                    case "Revisado":
                        this.lbProceso.Text = "Revisado - Captura inicial";
                        this.lbMaquina.Text = "Caseta:";
                        this.lbPosicionInicial.Visible = false;
                        this.txPosicionInicial.Visible = false;
                        this.lbColor.Visible = false;
                        this.cbxColor.Visible = false;
                        this.rbAscendente.Visible = false;
                        this.rbDescendente.Visible = false;
                        this.lbCarro.Visible = false;
                        this.txCarro.Visible = false;
                        break;
                    case "Esmaltado":
                        this.lbProceso.Text = "Esmaltado - Captura inicial";
                        this.lbMaquina.Text = "Caseta:";
                        this.lbPosicionInicial.Visible = false;
                        this.txPosicionInicial.Visible = false;
                        this.lbColor.Visible = true;
                        this.cbxColor.Visible = true;
                        this.rbAscendente.Visible = false;
                        this.rbDescendente.Visible = false;
                        this.lbCarro.Visible = false;
                        this.txCarro.Visible = false;
                        break;
                    case "Quemado":
                        this.lbProceso.Text = "Quemado - Captura inicial";
                        this.lbMaquina.Text = "Horno:";
                        this.lbPosicionInicial.Visible = false;
                        this.txPosicionInicial.Visible = false;
                        this.lbColor.Visible = false;
                        this.cbxColor.Visible = false;
                        this.rbAscendente.Visible = false;
                        this.rbDescendente.Visible = false;
                        this.lbCarro.Visible = true;
                        this.txCarro.Visible = true;
                        break;
                }

                this.iCodSupervisor = this.lu.CodSupervisor;
                this.lu.CodSupervisor = -1;
                this.lu.CodCentroTrabajo = - 1;
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
                dtObj = this.oDA.ObtenerClaveEmpleadoMFG(this.lu.CodEmpleado);
                this.txOperador.Text = dtObj.Rows[0]["ClaveEmpleadoMFG"].ToString();

                // Obtener Centros Trabajo.
                dtObj = this.oDA.ObtenerCentrosTrabajo(this.lu.CodPlanta, this.lu.CodProceso);
                dr = dtObj.NewRow();
                dr["CodCentroTrabajo"] = -1;
                dr["DesCentroTrabajo"] = "Seleccionar...";
                dtObj.Rows.InsertAt(dr, 0);
                cbxObj = this.cbxCentroTrabajo;
                cbxObj.ValueMember = "CodCentroTrabajo";
                cbxObj.DisplayMember = "DesCentroTrabajo";
                cbxObj.DataSource = dtObj;
                cbxObj.SelectedIndexChanged += new EventHandler(this.cbxCentroTrabajo_SelectedIndexChanged);
                cbxObj.SelectedValue = -1;

                // si el proceso es Esmaltado, obtener Colores.
                if (this.lu.DesProceso == "Esmaltado")
                {
                    dtObj = this.oDA.ObtenerColores();
                    dr = dtObj.NewRow();
                    dr["CodColor"] = -1;
                    dr["ClaveColor"] = "";
                    dr["DesColor"] = "Seleccionar...";
                    dtObj.Rows.InsertAt(dr, 0);
                    cbxObj = this.cbxColor;
                    cbxObj.ValueMember = "CodColor";
                    cbxObj.DisplayMember = "DesColor";
                    cbxObj.DataSource = dtObj;
                    cbxObj.SelectedIndexChanged += new EventHandler(this.cbxColor_SelectedIndexChanged);
                    cbxObj.SelectedValue = -1;
                }

                this.txSupervisor.Focus();
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

        #region txSupervisor_KeyPress
        private void txSupervisor_KeyPress(object sender, KeyPressEventArgs e)
        {
            TextBox txObj = (TextBox)sender;

            try
            {
                // Validar ingreso del Enter.
                if (e.KeyChar == 13)
                {
                    if (this.ValidarCodSupervisor(txObj))
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
        #region txSupervisor_LostFocus
        private void txSupervisor_LostFocus(object sender, EventArgs e)
        {
            try
            {
                TextBox txObj = (TextBox)sender;
                this.ValidarCodSupervisor(txObj);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
            }
        }
        #endregion txSupervisor_LostFocus
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
                    this.lbMensaje.Text = "Seleccione Centro de Trabajo";
                    cbxObj.Focus();
                }
                else
                {
                    dtObj = this.oDA.ObtenerMaquinas(this.lu.CodPlanta, this.lu.CodProceso, this.lu.CodCentroTrabajo);
                    dr = dtObj.NewRow();
                    dr["CodConfigBanco"] = -1;
                    dr["CodMaquina"] = -1;
                    dr["ClaveMaquina"] = "";
                    dr["DesMaquina"] = "Seleccionar...";
                    dr["CodTipoMaquina"] = -1;
                    dr["DesTipoMaquina"] = "";
                    dtObj.Rows.InsertAt(dr, 0);
                    cbxObj = this.cbxMaquina;
                    cbxObj.ValueMember = "CodMaquina";
                    cbxObj.DisplayMember = "DesMaquina";
                    cbxObj.DataSource = dtObj;
                    cbxObj.SelectedValue = -1;

                    this.lbMensaje.Text = "";
                    cbxObj.Focus();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
            }
        }
        #endregion cbxCentroTrabajo_SelectedIndexChanged
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
                    this.lbMensaje.Text = "Seleccione " + this.lu.DesTipoMaquina;
                    cbxObj.Focus();
                }
                else
                {
                    this.lbMensaje.Text = "";
                    switch(this.lu.DesProceso)
                    {
                        case "Vaciado":
                            this.txPosicionInicial.SelectAll();
                            this.txPosicionInicial.Focus();
                            break;
                        case "Secado":
                            this.btContinuar.Focus();
                            break;
                        case "Revisado":
                            this.btContinuar.Focus();
                            break;
                        case "Esmaltado":
                            this.cbxColor.Focus();
                            break;
                        case "Quemado":
                            this.txCarro.SelectAll();
                            this.txCarro.Focus();
                            break;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
            }
        }
        #endregion cbxMaquina_SelectedIndexChanged
        #region txPosicionInicial_KeyPress
        private void txPosicionInicial_KeyPress(object sender, KeyPressEventArgs e)
        {
            TextBox txObj = (TextBox)sender;

            try
            {
                // Validar ingreso del Enter.
                if (e.KeyChar == 13)
                {
                    if (this.ValidarPosicionInicial(txObj))
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
        #region txPosicionInicial_LostFocus
        private void txPosicionInicial_LostFocus(object sender, EventArgs e)
        {
            try
            {
                TextBox txObj = (TextBox)sender;
                this.ValidarPosicionInicial(txObj);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
            }
        }
        #endregion txPosicionInicial_LostFocus
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
                    this.lbMensaje.Text = "Seleccione Color";
                    cbxObj.Focus();
                }
                else
                {
                    this.lbMensaje.Text = "";
                    this.btContinuar.Focus();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
            }
        }
        #endregion cbxColor_SelectedIndexChanged
        #region txCarro_KeyPress
        private void txCarro_KeyPress(object sender, KeyPressEventArgs e)
        {
            TextBox txObj = (TextBox)sender;

            try
            {
                // Validar ingreso del Enter.
                if (e.KeyChar == 13)
                {
                    if (this.ValidarNumeroCarro(txObj))
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
        #region txCarro_LostFocus
        private void txCarro_LostFocus(object sender, EventArgs e)
        {
            try
            {
                TextBox txObj = (TextBox)sender;
                this.ValidarNumeroCarro(txObj);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
            }
        }
        #endregion txCarro_LostFocus
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

        #region btContinuar_Click
        private void btContinuar_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.lu.CodSupervisor == -1)
                {
                    this.lbMensaje.Text = "Ingrese Clave Supervisor valida";
                    this.txSupervisor.SelectAll();
                    this.txSupervisor.Focus();
                    return;
                }
                if (this.lu.CodCentroTrabajo == -1)
                {
                    this.lbMensaje.Text = "Seleccione Centro Trab.";
                    this.cbxCentroTrabajo.Focus();
                    return;
                }
                if (this.lu.CodMaquina == -1)
                {
                    this.lbMensaje.Text = "Seleccione " + this.lu.DesTipoMaquina;
                    this.cbxMaquina.Focus();
                    return;
                }

                switch (this.lu.DesProceso)
                {
                    case "Vaciado":
                        if (this.lu.PosInicial == -1)
                        {
                            this.lbMensaje.Text = "Ingrese Pos. Inicial valida";
                            this.txPosicionInicial.SelectAll();
                            this.txPosicionInicial.Focus();
                            return;
                        }
                        if (!this.rbAscendente.Checked && !this.rbDescendente.Checked)
                        {
                            this.lbMensaje.Text = "Seleccione Direccion";
                            this.rbAscendente.Focus();
                            return;
                        }
                        break;
                    case "Secado":
                        break;
                    case "Revisado":
                        break;
                    case "Esmaltado":
                        if (this.lu.CodColor == -1)
                        {
                            this.lbMensaje.Text = "Seleccione Color";
                            this.cbxColor.Focus();
                            return;
                        }
                        break;
                    case "Quemado":
                        if (this.lu.CodCarro == -1)
                        {
                            this.lbMensaje.Text = "Ingrese Numero de Carro";
                            this.txCarro.SelectAll();
                            this.txCarro.Focus();
                            return;
                        }
                        break;
                }

                this.oDA.ActualizarConfigHandHeld(  this.lu.CodSupervisor, 
                                                    this.lu.CodConfigBanco, 
                                                    this.lu.CodConfigHandHeld);

                Form frmObj = null;
                switch (this.lu.DesProceso)
                {
                    case "Vaciado":
                        frmObj = new a05_CapturaVaciado(this.lu);
                        break;
                    case "Secado":
                        frmObj = new a06_EntradaCarroSecador(this.lu);
                        break;
                    case "Revisado":
                        frmObj = new a07_CapturaRevisado(this.lu);
                        break;
                    case "Esmaltado":
                        frmObj = new a08_CapturaEsmaltado(this.lu);
                        break;
                    case "Quemado":
                        frmObj = new a09_CapturaQuemado(this.lu);
                        break;
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
                DialogResult dr = MessageBox.Show("¿Cancelar Captura inicial?", "SCPP", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
                if (dr == DialogResult.Yes)
                {
                    // Obtener Supervisor por defecto.
                    this.lu.CodSupervisor = this.iCodSupervisor;

                    //Regresar a Configuracion Inicial.
                    a03_ConfiguracionInicial frmObj = new a03_ConfiguracionInicial(this.lu);
                    frmObj.Show();
                    this.Close();
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