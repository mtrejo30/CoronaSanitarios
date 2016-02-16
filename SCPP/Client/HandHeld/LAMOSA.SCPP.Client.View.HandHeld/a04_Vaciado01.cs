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
    public partial class a04_Vaciado01 : Form
    {

        #region fields

        private LoginUsuario lu = null;
        //private cConfiguracionInicial oDA = new cConfiguracionInicial();
        private String valOperador = "";

        #endregion fields

        #region properties



        #endregion properties

        #region Constructors and Destructor

        public a04_Vaciado01(LoginUsuario lu)
        {
            InitializeComponent();
            //
            this.lu = lu;
            this.ConfigurarFormulario();
            this.ConfigurarPanelControles();
        }
        ~a04_Vaciado01()
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

            this.txOperador.KeyUp += new KeyEventHandler(this.txOperador_KeyUp);
            this.txSupervisor.KeyUp += new KeyEventHandler(this.txSupervisor_KeyUp);
            this.btContinuar.Click += new EventHandler(this.btContinuar_Click);
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
            int PosY = (int)((this.Height - this.pnControles.Height - this.pbLogo.Height) / 2);
            this.pnControles.Location = new Point(PosX, PosY);
        }
        #endregion ConfigurarCabecera

        #endregion Common

        private void Form_Load(object sender, EventArgs e)
        {
            try
            {
                this.lbOperador.Text = lu.NomEmpleado;
                this.lbPuesto.Text = lu.DesPuesto;
                this.lbPlanta.Text = lu.DesPlanta;
                this.lbProceso.Text = "Vaciado - Captura Inicial";

                DataSet ods = null;
                DataRow dr = null;
                ComboBox cbxObj = null;

                clsVaciado oConfigInicial = new clsVaciado();
                ods = oConfigInicial.getCentroTrabajo(lu.CodPlanta.ToString());
                //dtObj = da.ObtenerPantallasProceso(iCodProceso);
                dr = ods.Tables[0].NewRow();
                dr["centrotrabajo"] = -1;
                dr["descentrotrabajo"] = "Seleccionar...";
                ods.Tables[0].Rows.InsertAt(dr, 0);
                cbxObj = this.cbxCentroTrabajos;
                cbxObj.ValueMember = "centrotrabajo";
                cbxObj.DisplayMember = "descentrotrabajo";
                cbxObj.DataSource = ods.Tables[0];
                cbxObj.SelectedValue = -1;

                this.txOperador.Focus();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
            }
        }
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
        private void txOperador_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                this.txSupervisor.Focus();
            }
        }
        private void txOperador_LostFocus(object sender, EventArgs e)
        {
            try
            {
                clsVaciado ovaciado = new clsVaciado();
                DataSet ods = ovaciado.getNumOperador(txOperador.Text);
                if (ods.Tables.Count == 0 || ods.Tables[0].Rows.Count == 0 || Convert.ToInt32(ods.Tables[0].Rows[0][0]) == 0)
                {
                    this.lbMensaje.Text = "Operador No Encotrado!";
                    return;
                }
                valOperador = txOperador.Text;
                this.lbMensaje.Text = "";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
            }
        }
        private void txSupervisor_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                this.cbxCentroTrabajos.Focus();
            }
        }
        private void txSupervisor_LostFocus(object sender, EventArgs e)
        {
            try
            {
                clsVaciado ovaciado = new clsVaciado();
                DataSet ods = ovaciado.getNumSupervisor(txOperador.Text,txSupervisor.Text);
                if (ods.Tables.Count == 0 || ods.Tables[0].Rows.Count == 0 || Convert.ToInt32(ods.Tables[0].Rows[0][0]) == 0)
                {
                    this.lbMensaje.Text = "Supervisor No Encotrado!";
                    return;
                }
                valOperador = txOperador.Text;
                this.lbMensaje.Text = "";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
            }
        }

        private void cbxCentroTrabajos_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                DataTable dtObj = null;
                DataRow dr = null;
                ComboBox cbxObj = null;

                ComboBox cbxProceso = (ComboBox)sender;
                int iCodProceso = Convert.ToInt32(cbxProceso.SelectedValue);

                clsVaciado oConfigInicial = new clsVaciado();
                dtObj = oConfigInicial.getBanco(lu.CodPlanta.ToString(), iCodProceso.ToString()).Tables[0];
                //dtObj = da.ObtenerPantallasProceso(iCodProceso);
                dr = dtObj.NewRow();
                dr["clavemaquina"] = "-1";
                dr["desmaquina"] = "Seleccionar...";
                dtObj.Rows.InsertAt(dr, 0);
                cbxObj = this.cbxBancos;
                cbxObj.DataSource = dtObj;
                cbxObj.ValueMember = "clavemaquina";
                cbxObj.DisplayMember = "desmaquina";
                cbxObj.SelectedValue = -1;

                if (iCodProceso != -1)
                {
                    this.txSupervisor.Focus();
                }
                else
                {
                    this.txPosicionInicial.Focus();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
            }
        }

        private void btContinuar_Click(object sender, EventArgs e)
        {
            try
            {
                if (lbMensaje.Text != "")
                {
                    return;
                }


                if (string.IsNullOrEmpty(this.txOperador.Text))
                {
                    this.lbMensaje.Text = "Ingrese el Operador";
                    this.txOperador.Focus();
                }
                else if (string.IsNullOrEmpty(this.txSupervisor.Text))
                {
                    this.lbMensaje.Text = "Ingrese el Supervisor";
                    this.txSupervisor.Focus();
                }
               else if (Convert.ToInt32(this.cbxCentroTrabajos.SelectedValue) == -1)
                {
                    this.lbMensaje.Text = "Seleccione un Centro trab.";
                    this.cbxCentroTrabajos.Focus();
                }
                else if (this.cbxBancos.SelectedValue.ToString() == "-1")
                {
                    this.lbMensaje.Text = "Seleccione un Banco";
                    this.cbxBancos.Focus();
                }
                else if (string.IsNullOrEmpty(this.txPosicionInicial.Text))
                {
                    this.lbMensaje.Text = "Ingrese una Pos. Inicial";
                    this.txPosicionInicial.Focus();
                }
                else
                {
                    clsVaciado ovaciado = new clsVaciado();
                    ovaciado.Operador = this.txOperador.Text;
                    ovaciado.Supervisor = this.txSupervisor.Text;
                    ovaciado.CentroTrabajo = Convert.ToInt32(this.cbxCentroTrabajos.SelectedValue);
                    ovaciado.Banco = this.cbxBancos.SelectedValue.ToString();
                    ovaciado.Posicion = Convert.ToInt32(this.txPosicionInicial.Text);
                    if (this.rbAscendente.Checked == true)
                        ovaciado.Asc = true;
                    else
                        ovaciado.Asc = false;

                    a04_Vaciado02 frmObj = new a04_Vaciado02(this.lu, ovaciado);
                    frmObj.Show();
                    this.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
            }
        }

        private void btSalir_Click_1(object sender, EventArgs e)
        {
            DialogResult dr = MessageBox.Show("¿Salir de la Aplicación?", "SCPP", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
            if (dr == DialogResult.Yes)
            {
                Application.Exit();
            }
        }











    }
}